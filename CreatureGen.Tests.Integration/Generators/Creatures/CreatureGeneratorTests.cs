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

        [TestCase(CreatureConstants.Destrachan)]
        [TestCase(CreatureConstants.Grimlock)]
        public void BUG_DoesNotHaveSight(string creatureName)
        {
            var creature = CreatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = creature.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.Blindsight));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.AllAroundVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.Darkvision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision_Superior));
        }

        [TestCase(CreatureConstants.Elf_Aquatic)]
        [TestCase(CreatureConstants.Elf_Drow)]
        [TestCase(CreatureConstants.Elf_Gray)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Elf_Wild)]
        [TestCase(CreatureConstants.Elf_Wood)]
        public void BUG_ElfCanUseShield(string elfName)
        {
            var elf = CreatureGenerator.Generate(elfName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(elf);

            Assert.That(elf.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = elf.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.ShieldProficiency));
        }

        [Test]
        public void BUG_GiantOwlDOesNotDoubleUpOnLowLightVision()
        {
            var owl = CreatureGenerator.Generate(CreatureConstants.Owl_Giant, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(owl);

            Assert.That(owl.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = owl.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.LowLightVision_Superior));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision));
        }
    }
}
