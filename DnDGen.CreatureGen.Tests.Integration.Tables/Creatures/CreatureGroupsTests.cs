using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class CreatureGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.CreatureGroups;

        private ICreaturePrototypeFactory prototypeFactory;

        [SetUp]
        public void Setup()
        {
            prototypeFactory = GetNewInstanceOf<ICreaturePrototypeFactory>();
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
        public void AllCreatureGroup()
        {
            var allCreatures = CreatureConstants.GetAll();
            AssertDistinctCollection(GroupConstants.All, [.. allCreatures]);
        }

        [Test]
        public void CharacterGroup()
        {
            var allCharacters = CreatureConstants.GetAllCharacters();
            AssertDistinctCollection(GroupConstants.Characters, [.. allCharacters]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateGroup(string template)
        {
            var allCreatures = CreatureConstants.GetAll();
            var allPrototypes = prototypeFactory.Build(allCreatures, false);

            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var templatePrototypes = applicator.GetCompatiblePrototypes(allPrototypes, false);
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            Assert.That(templateCreatures, Is.Not.Empty);
            AssertCollection(template + bool.FalseString, [.. templateCreatures]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateGroup_AsCharacter(string template)
        {
            var allCreatures = CreatureConstants.GetAll();
            var allPrototypes = prototypeFactory.Build(allCreatures, true);

            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var templatePrototypes = applicator.GetCompatiblePrototypes(allPrototypes, true);
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            Assert.That(templateCreatures, Is.Not.Empty);
            AssertCollection(template + bool.TrueString, [.. templateCreatures]);
        }

        [Test]
        public void TemplateGroups_DifferBasedOnAsCharacter()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var asCharacter = new List<string>();
            var notAsCharacter = new List<string>();

            foreach (var template in templates)
            {
                Assert.That(table, Contains.Key(template + bool.TrueString)
                    .And.ContainKey(template + bool.FalseString));

                asCharacter.AddRange(table[template + bool.TrueString]);
                notAsCharacter.AddRange(table[template + bool.FalseString]);
            }

            Assert.That(asCharacter, Is.Not.EquivalentTo(notAsCharacter));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateGroup_HasNonEmptyVariation(string template)
        {
            Assert.That(table, Contains.Key(template + bool.TrueString)
                .And.ContainKey(template + bool.FalseString));

            var allVariations = table[template + bool.FalseString]
                .Union(table[template + bool.TrueString]);
            Assert.That(allVariations, Is.Not.Empty);
        }
    }
}
