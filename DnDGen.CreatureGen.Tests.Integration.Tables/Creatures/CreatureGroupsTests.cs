using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class CreatureGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.CreatureGroups;

        private ICreaturePrototypeFactory prototypeFactory;
        private ICollectionTypeAndAmountSelector typeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            prototypeFactory = GetNewInstanceOf<ICreaturePrototypeFactory>();
            typeAndAmountSelector = GetNewInstanceOf<ICollectionTypeAndAmountSelector>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            var templates = CreatureConstants.Templates.GetAll();

            var entries = new[]
            {
                GroupConstants.All,
                GroupConstants.Characters,
            };

            var names = entries
                .Union(templates.Select(t => t + bool.FalseString))
                .Union(templates.Select(t => t + bool.TrueString));

            AssertCollectionNames(names);
        }

        [Test]
        public void CreatureGroup_All()
        {
            var allCreatures = CreatureConstants.GetAll();
            AssertDistinctCollection(GroupConstants.All, [.. allCreatures]);
        }

        [Test]
        public void CreatureGroup_Characters()
        {
            var allCharacters = CreatureConstants.GetAllCharacters();
            AssertDistinctCollection(GroupConstants.Characters, [.. allCharacters]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CreatureGroup_Template(string template)
        {
            var allCreatures = CreatureConstants.GetAll();
            AssertTemplateGroup(template, allCreatures, false);
        }

        private void AssertTemplateGroup(string template, IEnumerable<string> source, bool asCharacter)
        {
            var sourcePrototypes = GetTemplatePrototypes(template, source, asCharacter);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, asCharacter);
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            AssertDistinctCollection(template + asCharacter.ToString(), [.. templateCreatures]);
        }

        private CreaturePrototype[] GetTemplatePrototypes(string template, IEnumerable<string> source, bool asCharacter)
        {
            var prototypes = prototypeFactory.Build(source, asCharacter).ToArray();
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);

            //INFO: Since ability compatibility with templates is based on the ability randomizer,
            //we don't want to exclude potentially-low-ability creatures if the ability randomizer allows high rolls (such as for characters)
            if (applicator.MinimumAbility != null)
            {
                foreach (var prototype in prototypes)
                {
                    if (prototype.Abilities[applicator.MinimumAbility.Name].HasScore)
                        prototype.Abilities[applicator.MinimumAbility.Name].BaseScore += applicator.MinimumAbility.FullScore;
                }
            }

            return prototypes;
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CreatureGroup_TemplateAsCharacter(string template)
        {
            //INFO: We can do this as a shortcut, because if asCharacter = true, then our set of base creatures is only characters.
            //Setting asCharacter = true and generating a creature that can't be a character produces a compatibility error.
            var allCharacters = CreatureConstants.GetAllCharacters();
            AssertTemplateGroup(template, allCharacters, true);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CreatureGroup_Template_HasNonEmptyVariation(string template)
        {
            Assert.That(table, Contains.Key(template + bool.TrueString)
                .And.ContainKey(template + bool.FalseString));

            var allVariations = table[template + bool.FalseString]
                .Union(table[template + bool.TrueString]);
            Assert.That(allVariations, Is.Not.Empty);
        }

        [TestCase(AbilityConstants.Charisma, -9)]
        [TestCase(AbilityConstants.Charisma, -8)]
        [TestCase(AbilityConstants.Charisma, -7)]
        [TestCase(AbilityConstants.Charisma, -6)]
        [TestCase(AbilityConstants.Charisma, -5)]
        [TestCase(AbilityConstants.Charisma, -4)]
        [TestCase(AbilityConstants.Charisma, -3)]
        [TestCase(AbilityConstants.Charisma, -2)]
        [TestCase(AbilityConstants.Charisma, -1)]
        [TestCase(AbilityConstants.Charisma, 0)]
        [TestCase(AbilityConstants.Charisma, 1)]
        [TestCase(AbilityConstants.Charisma, 2)]
        [TestCase(AbilityConstants.Charisma, 3)]
        [TestCase(AbilityConstants.Charisma, 4)]
        [TestCase(AbilityConstants.Charisma, 5)]
        public void CreatureGroup_MinimumAbilityAdjustment(string ability, int adjustment)
        {
            //INFO: Sticking to Charisma (for Ghost) right now for validation, but will expand to Intelligence (Half-Celestial, Half-Fiend) eventually

            var allCreatures = CreatureConstants.GetAll();
            var abilityAdjustments = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToDictionary(d => d.Type, d => d.Amount));
            var abilityCreatures = allCreatures
                .Where(c => abilityAdjustments[c].ContainsKey(ability) && abilityAdjustments[c][ability] >= adjustment);

            var groupName = ability + adjustment;
            AssertDistinctCollection(groupName, [.. abilityCreatures]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Types))]
        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Subtypes))]
        public void CreatureGroup_Template_ResultsInType(string type)
        {
            //INFO: Sticking to Ghost right now for validation, but will expand to all templates eventually
            var allCreatures = CreatureConstants.GetAll();
            var template = CreatureConstants.Templates.Ghost;
            var sourcePrototypes = GetTemplatePrototypes(template, allCreatures, false);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);

            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, false, new() { Type = type });
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            var groupName = template + type;
            AssertDistinctCollection(groupName, [.. templateCreatures]);
        }

        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        public void CreatureGroup_Template_ResultsInAlignment(string alignment)
        {
            //INFO: Sticking to Ghost right now for validation, but will expand to all templates eventually
            var allCreatures = CreatureConstants.GetAll();
            var template = CreatureConstants.Templates.Ghost;
            var sourcePrototypes = GetTemplatePrototypes(template, allCreatures, false);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);

            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, false, new() { Alignment = alignment });
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            var groupName = template + alignment;
            AssertDistinctCollection(groupName, [.. templateCreatures]);
        }

        private static IEnumerable ChallengeRatings => ChallengeRatingConstants.GetOrdered().Select(cr => new TestCaseData(cr));

        [TestCaseSource(nameof(ChallengeRatings))]
        public void CreatureGroup_Template_ResultsInChallengeRating(string cr)
        {
            //INFO: Sticking to Ghost right now for validation, but will expand to all templates eventually
            var allCreatures = CreatureConstants.GetAll();
            var template = CreatureConstants.Templates.Ghost;
            var sourcePrototypes = GetTemplatePrototypes(template, allCreatures, false);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);

            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, false, new() { ChallengeRating = cr });
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            var groupName = template + bool.FalseString + cr;
            AssertDistinctCollection(groupName, [.. templateCreatures]);
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void CreatureGroup_TemplateAsCharacter_ResultsInChallengeRating(string cr)
        {
            //INFO: Sticking to Ghost right now for validation, but will expand to all templates eventually
            var allCharacters = CreatureConstants.GetAllCharacters();
            var template = CreatureConstants.Templates.Ghost;
            var sourcePrototypes = GetTemplatePrototypes(template, allCharacters, true);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);

            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, true, new() { ChallengeRating = cr });
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            var groupName = template + bool.TrueString + cr;
            AssertDistinctCollection(groupName, [.. templateCreatures]);
        }
    }
}
