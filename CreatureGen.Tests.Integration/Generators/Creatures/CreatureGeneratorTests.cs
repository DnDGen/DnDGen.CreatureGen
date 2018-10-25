using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Generators.Creatures;
using CreatureGen.Tests.Integration.TestData;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Generators.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : IntegrationTests
    {
        [Inject]
        public ICreatureGenerator CreatureGenerator { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        private CreatureAsserter creatureAsserter;

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);

            creatureAsserter = new CreatureAsserter();
        }

        [Test]
        public void DoSpellsForThoseWhoCastAsSpellcaster()
        {
            Assert.Fail("TODO");
        }

        [Test]
        public void DoEquipment()
        {
            Assert.Fail("TODO");
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CanGenerateCreature(string creatureName)
        {
            var creature = CreatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), "Templates")]
        public void CanGenerateTemplate(string template)
        {
            var creature = CreatureGenerator.Generate(CreatureConstants.Human, template);
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void BUG_DestrachanDoesNotHaveSight()
        {
            var destrachan = CreatureGenerator.Generate(CreatureConstants.Destrachan, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(destrachan);

            Assert.That(destrachan.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = destrachan.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.Blindsight));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.AllAroundVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.Darkvision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision_Superior));
        }
    }
}
