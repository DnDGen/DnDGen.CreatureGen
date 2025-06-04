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
            AssertTemplateGroup(template, allCreatures, false);
        }

        private void AssertTemplateGroup(string template, IEnumerable<string> source, bool asCharacter)
        {
            var sourcePrototypes = prototypeFactory.Build(source, asCharacter);

            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var templatePrototypes = applicator.GetCompatiblePrototypes(sourcePrototypes, asCharacter);
            var templateCreatures = templatePrototypes.Select(p => p.Name);

            AssertDistinctCollection(template + asCharacter.ToString(), [.. templateCreatures]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateGroup_AsCharacter(string template)
        {
            //INFO: We can do this as a shortcut, because if asCharacter = true, then our set of base creatures is only characters.
            //Setting asCharacter = true and generating a creature that can't be a character produces a compatibility error.
            var allCharacters = CreatureConstants.GetAllCharacters();
            AssertTemplateGroup(template, allCharacters, true);
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
