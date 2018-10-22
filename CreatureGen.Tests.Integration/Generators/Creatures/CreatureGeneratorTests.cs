using CreatureGen.Creatures;
using CreatureGen.Generators.Creatures;
using CreatureGen.Tests.Integration.TestData;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;

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
    }
}
