using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTemplateGroupsTests : CreatureGroupsTableTests
    {
        private ICreaturePrototypeFactory prototypeFactory;

        [SetUp]
        public void Setup()
        {
            prototypeFactory = GetNewInstanceOf<ICreaturePrototypeFactory>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
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
            AssertCollection(template, [.. templateCreatures]);
        }
    }
}
