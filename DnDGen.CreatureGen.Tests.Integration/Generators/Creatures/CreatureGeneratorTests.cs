using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Generators.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : IntegrationTests
    {
        private CreatureAsserter creatureAsserter;
        private ICreatureGenerator creatureGenerator;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = new CreatureAsserter();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
        }

        [TestCase(CreatureConstants.Rakshasa)]
        public void CanGenerateSpellsForThoseWhoCastAsSpellcaster(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.Magic, Is.Not.Null);
            Assert.That(creature.Magic.Caster, Is.Not.Empty);
            Assert.That(creature.Magic.CasterLevel, Is.Positive);
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.Not.Negative);
            Assert.That(creature.Magic.Domains, Is.Not.Null);
            Assert.That(creature.Magic.KnownSpells, Is.Not.Empty.And.All.Not.Null);
            Assert.That(creature.Magic.SpellsPerDay, Is.Not.Empty.And.All.Not.Null);
        }

        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Dwarf_Hill)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Halfling_Lightfoot)]
        [TestCase(CreatureConstants.Orc)]
        [TestCase(CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Goblin)]
        [TestCase(CreatureConstants.Ogre)]
        [TestCase(CreatureConstants.Balor)]
        public void CanGenerateWeapons(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);
        }

        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Dwarf_Hill)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Halfling_Lightfoot)]
        [TestCase(CreatureConstants.Orc)]
        [TestCase(CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Goblin)]
        [TestCase(CreatureConstants.Ogre)]
        public void CanGenerateArmor(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CanGenerateCreature(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), "Templates")]
        public void CanGenerateTemplate(string template)
        {
            var creature = creatureGenerator.Generate(CreatureConstants.Human, template);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Destrachan)]
        [TestCase(CreatureConstants.Grimlock)]
        [TestCase(CreatureConstants.Yrthak)]
        public void BUG_DoesNotHaveSight(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = creature.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.Blindsight));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.AllAroundVision)
                .And.Not.Contain(FeatConstants.SpecialQualities.Darkvision)
                .And.Not.Contain(FeatConstants.SpecialQualities.LowLightVision)
                .And.Not.Contain(FeatConstants.SpecialQualities.LowLightVision_Superior));
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
            var elf = creatureGenerator.Generate(elfName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(elf);

            Assert.That(elf.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = elf.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.ShieldProficiency));
        }

        [Test]
        public void BUG_HalfOrcIsNotSensitiveToLight()
        {
            var halfOrc = creatureGenerator.Generate(CreatureConstants.Orc_Half, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(halfOrc);

            Assert.That(halfOrc.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = halfOrc.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LightSensitivity));
        }

        [Test]
        public void BUG_NightcrawlerHasConcentration()
        {
            var nightcrawler = creatureGenerator.Generate(CreatureConstants.Nightcrawler, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(nightcrawler);

            Assert.That(nightcrawler.Skills, Is.Not.Empty);

            var skillNames = nightcrawler.Skills.Select(q => q.Name);
            Assert.That(skillNames, Contains.Item(SkillConstants.Concentration));
        }
    }
}
