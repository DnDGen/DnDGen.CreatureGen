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

        [TestCase(CreatureConstants.Angel_Planetar)]
        [TestCase(CreatureConstants.Angel_Solar)]
        [TestCase(CreatureConstants.Aranea)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm)]
        [TestCase(CreatureConstants.Rakshasa)]
        public void CanGenerateSpellsForThoseWhoCastAsSpellcaster(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.Magic, Is.Not.Null);
            Assert.That(creature.Magic.Caster, Is.Not.Empty);
            Assert.That(creature.Magic.CasterLevel, Is.Positive);
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.InRange(0, 100));
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
        public void CanGenerateHumanTemplate(string template)
        {
            var creature = creatureGenerator.Generate(CreatureConstants.Human, template);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Ape, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Ape_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Badger, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Badger_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bat_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bear_Black, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Brown, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Polar, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bee_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bison, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Boar, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Boar_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Cat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Cat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Crocodile, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Crocodile_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Deinonychus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Dog, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Dog_Riding, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Eagle, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Eagle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elasmosaurus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Elephant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.FireBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Giant_Hill, CreatureConstants.Templates.Wereboar)]
        [TestCase(CreatureConstants.Girallon, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Griffon, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Hippogriff, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Heavy, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Heavy_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Light, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Light_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lammasu, CreatureConstants.Templates.HalfDragon)]
        [TestCase(CreatureConstants.Lammasu, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lion, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lion_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Megaraptor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Monkey, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Octopus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Octopus_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Owl_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Pony, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Pony_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Porpoise, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Rat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Rat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Rat_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Raven, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Raven, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Rhinoceras, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Roc, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.SeaCat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Shark_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Shark_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Shark_Large, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Shark_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Shark_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Squid, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Squid_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.StagBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Tiger, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Tiger_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Triceratops, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Tyrannosaurus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Unicorn, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Wasp_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Weasel, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Weasel, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Weasel_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Whale_Baleen, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Whale_Cachalot, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Whale_Orca, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Wolf, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wolf_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wolverine, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wolverine_Dire, CreatureConstants.Templates.FiendishCreature)]
        public void CanGenerateTemplate(string creatureName, string template)
        {
            var creature = creatureGenerator.Generate(creatureName, template);
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
