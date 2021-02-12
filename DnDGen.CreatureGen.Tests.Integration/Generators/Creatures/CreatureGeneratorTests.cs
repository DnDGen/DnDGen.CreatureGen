using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.TreasureGen.Items;
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
            Assert.That(creature.Magic.CastingAbility, Is.Not.Null);
            Assert.That(creature.Abilities.Values, Contains.Item(creature.Magic.CastingAbility), creature.Magic.CastingAbility.Name);
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.InRange(0, 100));
            Assert.That(creature.Magic.Domains, Is.Not.Null);

            if (creature.Magic.CastingAbility.FullScore > 10)
            {
                Assert.That(creature.Magic.KnownSpells, Is.Not.Empty.And.All.Not.Null, $"{creature.Magic.CastingAbility.Name}: {creature.Magic.CastingAbility.FullScore}");
                Assert.That(creature.Magic.SpellsPerDay, Is.Not.Empty.And.All.Not.Null, $"{creature.Magic.CastingAbility.Name}: {creature.Magic.CastingAbility.FullScore}");
            }
            else
            {
                Assert.That(creature.Magic.KnownSpells, Is.Empty, $"{creature.Magic.CastingAbility.Name}: {creature.Magic.CastingAbility.FullScore}");
                Assert.That(creature.Magic.SpellsPerDay, Is.Empty, $"{creature.Magic.CastingAbility.Name}: {creature.Magic.CastingAbility.FullScore}");
            }
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
        [TestCase(CreatureConstants.Titan)]
        [TestCase(CreatureConstants.Giant_Cloud)]
        public void CanGenerateWeapons(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamage(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);

            var oversizedFeat = creature.SpecialQualities.FirstOrDefault(sq => sq.Name == FeatConstants.SpecialQualities.OversizedWeapon);
            Assert.That(oversizedFeat, Is.Not.Null);
            Assert.That(oversizedFeat.Foci, Is.Not.Empty);
            Assert.That(oversizedFeat.Foci.Count(), Is.EqualTo(1));

            var oversizedSize = oversizedFeat.Foci.First();

            var weaponNames = WeaponConstants.GetAllWeapons(true, false);
            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural && weaponNames.Contains(a.Name));

            foreach (var attack in unnaturalAttacks)
            {
                var weapon = creature.Equipment.Weapons.FirstOrDefault(w => w.Name == attack.Name);
                Assert.That(weapon, Is.Not.Null, $"{creature.Summary}: {attack.Name}");
                Assert.That(weapon.DamageDescription, Is.Not.Empty, $"{creature.Summary}: {weapon.Description}");
                Assert.That(weaponNames, Contains.Item(weapon.Name), $"{creature.Summary}: {weapon.Description}");

                Assert.That(attack.Damages, Is.Not.Empty.And.Count.EqualTo(weapon.Damages.Count), $"{creature.Summary}; Weapon: {weapon.Description}");

                for (var i = 0; i < weapon.Damages.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Roll), $"{creature.Summary}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Type), $"{creature.Summary}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Condition), $"{creature.Summary}; Weapon: {weapon.Description}");
                    }
                    else
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Description), $"{creature.Summary}; Weapon: {weapon.Description}");
                    }
                }

                if (weapon.Attributes.Contains(AttributeConstants.Melee))
                {
                    Assert.That(attack.AttackType, Contains.Substring("melee"), $"{creature.Summary} ({creature.Size}): {weapon.Description} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
                else if (weapon.Attributes.Contains(AttributeConstants.Ranged))
                {
                    Assert.That(attack.AttackType, Contains.Substring("ranged"), $"{creature.Summary} ({creature.Size}): {weapon.Description} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
            }
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CanGenerateCreature(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Aasimar)]
        [TestCase(CreatureConstants.Azer)]
        [TestCase(CreatureConstants.Bugbear)]
        [TestCase(CreatureConstants.Centaur)]
        [TestCase(CreatureConstants.Derro)]
        [TestCase(CreatureConstants.Derro_Sane)]
        [TestCase(CreatureConstants.Doppelganger)]
        [TestCase(CreatureConstants.Dwarf_Deep)]
        [TestCase(CreatureConstants.Dwarf_Duergar)]
        [TestCase(CreatureConstants.Dwarf_Hill)]
        [TestCase(CreatureConstants.Dwarf_Mountain)]
        [TestCase(CreatureConstants.Eagle_Giant)]
        [TestCase(CreatureConstants.Elf_Aquatic)]
        [TestCase(CreatureConstants.Elf_Drow)]
        [TestCase(CreatureConstants.Elf_Gray)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Elf_Wild)]
        [TestCase(CreatureConstants.Elf_Wood)]
        [TestCase(CreatureConstants.Gargoyle)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth)]
        [TestCase(CreatureConstants.Giant_Cloud)]
        [TestCase(CreatureConstants.Giant_Fire)]
        [TestCase(CreatureConstants.Giant_Frost)]
        [TestCase(CreatureConstants.Giant_Hill)]
        [TestCase(CreatureConstants.Giant_Stone)]
        [TestCase(CreatureConstants.Giant_Stone_Elder)]
        [TestCase(CreatureConstants.Giant_Storm)]
        [TestCase(CreatureConstants.Githyanki)]
        [TestCase(CreatureConstants.Githzerai)]
        [TestCase(CreatureConstants.Gnoll)]
        [TestCase(CreatureConstants.Gnome_Forest)]
        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin)]
        [TestCase(CreatureConstants.Goblin)]
        [TestCase(CreatureConstants.Grimlock)]
        [TestCase(CreatureConstants.Halfling_Deep)]
        [TestCase(CreatureConstants.Halfling_Lightfoot)]
        [TestCase(CreatureConstants.Halfling_Tallfellow)]
        [TestCase(CreatureConstants.Harpy)]
        [TestCase(CreatureConstants.Hobgoblin)]
        [TestCase(CreatureConstants.HoundArchon)]
        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Janni)]
        [TestCase(CreatureConstants.Kobold)]
        [TestCase(CreatureConstants.KuoToa)]
        [TestCase(CreatureConstants.Lizardfolk)]
        [TestCase(CreatureConstants.Locathah)]
        [TestCase(CreatureConstants.Mephit_Air)]
        [TestCase(CreatureConstants.Mephit_Dust)]
        [TestCase(CreatureConstants.Mephit_Earth)]
        [TestCase(CreatureConstants.Mephit_Fire)]
        [TestCase(CreatureConstants.Mephit_Ice)]
        [TestCase(CreatureConstants.Mephit_Magma)]
        [TestCase(CreatureConstants.Mephit_Ooze)]
        [TestCase(CreatureConstants.Mephit_Salt)]
        [TestCase(CreatureConstants.Mephit_Steam)]
        [TestCase(CreatureConstants.Mephit_Water)]
        [TestCase(CreatureConstants.Merfolk)]
        [TestCase(CreatureConstants.MindFlayer)]
        [TestCase(CreatureConstants.Minotaur)]
        [TestCase(CreatureConstants.Mummy)]
        [TestCase(CreatureConstants.Ogre)]
        [TestCase(CreatureConstants.OgreMage)]
        [TestCase(CreatureConstants.Ogre_Merrow)]
        [TestCase(CreatureConstants.Orc)]
        [TestCase(CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Owl_Giant)]
        [TestCase(CreatureConstants.Pixie)]
        [TestCase(CreatureConstants.Pixie_WithIrresistibleDance)]
        [TestCase(CreatureConstants.Pseudodragon)]
        [TestCase(CreatureConstants.Rakshasa)]
        [TestCase(CreatureConstants.Sahuagin)]
        [TestCase(CreatureConstants.Sahuagin_Malenti)]
        [TestCase(CreatureConstants.Sahuagin_Mutant)]
        [TestCase(CreatureConstants.Satyr)]
        [TestCase(CreatureConstants.Satyr_WithPipes)]
        [TestCase(CreatureConstants.Slaad_Blue)]
        [TestCase(CreatureConstants.Slaad_Gray)]
        [TestCase(CreatureConstants.Slaad_Green)]
        [TestCase(CreatureConstants.Slaad_Red)]
        [TestCase(CreatureConstants.Tiefling)]
        [TestCase(CreatureConstants.Troglodyte)]
        [TestCase(CreatureConstants.Troll)]
        [TestCase(CreatureConstants.Troll_Scrag)]
        [TestCase(CreatureConstants.Unicorn)]
        [TestCase(CreatureConstants.YuanTi_Abomination)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeArms)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeHead)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTail)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs)]
        [TestCase(CreatureConstants.YuanTi_Pureblood)]
        public void CanGenerateCreatureAsCharacter(string creatureName)
        {
            var creature = creatureGenerator.GenerateAsCharacter(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CanGenerateHumanTemplate(string template)
        {
            var creature = creatureGenerator.Generate(CreatureConstants.Human, template);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CanGenerateHumanTemplateAsCharacter(string template)
        {
            var creature = creatureGenerator.GenerateAsCharacter(CreatureConstants.Human, template);
            creatureAsserter.AssertCreatureAsCharacter(creature);
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(CreatureConstants.Ape, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Ape_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Badger, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Badger_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Basilisk, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Basilisk_Greater, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bat_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bat_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bat_Swarm, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bat_Swarm, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Bear_Black, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Brown, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Polar, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bear_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bee_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bison, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Boar, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Boar_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Bugbear, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Cat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Cat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Chimera_Black, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Chimera_Blue, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Chimera_Green, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Chimera_Red, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Chimera_White, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Crocodile, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Crocodile_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Deinonychus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Dog, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Dog_Riding, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Dragon_Red_Young, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Eagle, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Eagle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elasmosaurus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Elephant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Templates.Vampire)]
        [TestCase(CreatureConstants.Ettin, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.FireBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Giant_Cloud, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Giant_Hill, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted)]
        [TestCase(CreatureConstants.Giant_Hill, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural)]
        [TestCase(CreatureConstants.Girallon, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.GrayRender, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Griffon, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Grig, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Grig_WithFiddle, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Hippogriff, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Heavy, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Heavy_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Light, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Horse_Light_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Ghost)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.HalfDragon_Black)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lich)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Vampire)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Kobold, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Lammasu, CreatureConstants.Templates.HalfDragon_Gold)]
        [TestCase(CreatureConstants.Lammasu, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lion, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lion_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Megaraptor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Megaraptor, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Minotaur, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Monkey, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Octopus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Octopus_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Ogre, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Owl_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Owlbear, CreatureConstants.Templates.Skeleton)]
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
        [TestCase(CreatureConstants.Shrieker, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Small, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Large, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, CreatureConstants.Templates.CelestialCreature)]
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
        [TestCase(CreatureConstants.Squid_Giant, CreatureConstants.Templates.HalfDragon_Blue)]
        [TestCase(CreatureConstants.Squid_Giant, CreatureConstants.Templates.HalfDragon_Green)]
        [TestCase(CreatureConstants.StagBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Tiger, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Tiger_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Triceratops, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Troglodyte, CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Troll, CreatureConstants.Templates.Skeleton)]
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
        [TestCase(CreatureConstants.Wolf, CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Wolf_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wolverine, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wolverine_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Wyvern, CreatureConstants.Templates.Zombie)]
        public void CanGenerateTemplate(string creatureName, string template)
        {
            var creature = creatureGenerator.Generate(creatureName, template);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Template, Is.EqualTo(template));
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
