using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureGroupsTestBase : CollectionTests
    {
        protected override string TableName => TableNameConstants.Collection.CreatureGroups;

        private ICreaturePrototypeFactory prototypeFactory;
        private CreaturePrototype[] creaturePrototypeSource;
        private CreaturePrototype[] characterPrototypeSource;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var creatures = CreatureConstants.GetAll();
            var characters = CreatureConstants.GetAllCharacters();
            var randomizer = new AbilityRandomizer(AbilityConstants.RandomizerRolls.BestOfFour);

            prototypeFactory = GetNewInstanceOf<ICreaturePrototypeFactory>();
            creaturePrototypeSource = prototypeFactory.Build(creatures, false, randomizer).ToArray();
            characterPrototypeSource = prototypeFactory.Build(characters, true, randomizer).ToArray();
        }

        protected void AssertAllCreatureGroupNames()
        {
            var names = GetAllExpectedGroupNames();
            AssertCollectionNames(names);
        }

        private IEnumerable<string> GetAllExpectedGroupNames()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();

            var entries = new[]
            {
                GroupConstants.All,
                GroupConstants.Characters,
            };

            var names = entries
                .Union(templates.Select(t => t + bool.FalseString))
                .Union(templates.Select(t => t + bool.TrueString))
                .Union(Alignments.SelectMany(a => templates.Select(t => t + a)))
                .Union(types.SelectMany(ty => templates.Select(t => t + ty)))
                .Union(subtypes.SelectMany(sty => templates.Select(t => t + sty)))
                .Union(ChallengeRatings.SelectMany(cr => templates.Select(t => t + bool.FalseString + cr)))
                .Union(ChallengeRatings.SelectMany(cr => templates.Select(t => t + bool.TrueString + cr)));

            const int LowestAdjustment = -10;
            const int MinimumAbilityScore = 1;
            foreach (var template in templates)
            {
                var applicator = GetNewInstanceOf<TemplateApplicator>(template);
                if (applicator.MinimumAbility == null)
                    continue;

                var adjustmentCount = (applicator.MinimumAbility.FullScore - MinimumAbilityScore) - LowestAdjustment + 1;
                var adjustments = Enumerable.Range(LowestAdjustment, adjustmentCount);
                names = names.Union(adjustments.Select(adj => applicator.MinimumAbility.Name + adj));
            }

            return names;
        }

        protected void AssertSubsetCreatureGroupNames(IEnumerable<string> subset)
        {
            Assert.That(subset, Is.SubsetOf(GetAllExpectedGroupNames()));
            Assert.That(table.Keys, Is.SupersetOf(subset));
        }

        protected static string[] Alignments =>
        [
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.TrueNeutral,
        ];

        protected static IEnumerable<string> ChallengeRatings => ChallengeRatingConstants.GetOrdered();

        protected void AssertTemplateGroup(string template, IEnumerable<string> source, bool asCharacter)
        {
            //INFO: We explicitly do not want to use the creatureVerifier or its cached lookups, as this test is what populates those caches
            var sourcePrototypes = GetPrototypes(source, asCharacter);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var templateCreatures = sourcePrototypes
                .Where(p => applicator.IsCompatible(p))
                .Select(p => p.Name);

            AssertDistinctCollection(template + asCharacter.ToString(), [.. templateCreatures]);
        }

        internal CreaturePrototype[] GetPrototypes(IEnumerable<string> source, bool asCharacter)
        {
            var prototypeSource = asCharacter ? characterPrototypeSource : creaturePrototypeSource;
            return prototypeSource
                .Where(p => source.Contains(p.Name))
                .Select(prototypeFactory.Clone)
                .ToArray();
        }

        internal IEnumerable<CreaturePrototype> GetTemplatePrototypes(string template)
        {
            var prototypes = GetTemplatePrototypes(template, false);
            var characters = GetTemplatePrototypes(template, true);

            return prototypes.Concat(characters);
        }

        internal IEnumerable<CreaturePrototype> GetTemplatePrototypes(string template, bool asCharacter) => GetPrototypes(table[template + asCharacter], asCharacter);

        protected static IEnumerable<string[]> TemplateChallengeRatingPairs
        {
            get
            {
                var templates = CreatureConstants.Templates.GetAll();

                foreach (var template in templates)
                    foreach (var cr in ChallengeRatings)
                        yield return [template, cr];
            }
        }

        protected static IEnumerable TemplatesWithChallengeRatingFilter => TemplateChallengeRatingPairs.Select(p => new TestCaseData(p[0], p[1]));
    }
}
