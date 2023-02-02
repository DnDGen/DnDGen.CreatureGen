using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Abilities;
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
            creatureAsserter = GetNewInstanceOf<CreatureAsserter>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
        }

        [TestCase(CreatureConstants.Androsphinx)]
        [TestCase(CreatureConstants.Angel_Planetar)]
        [TestCase(CreatureConstants.Angel_Solar)]
        [TestCase(CreatureConstants.Aranea)]
        [TestCase(CreatureConstants.Couatl)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_White_Old)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld)]
        [TestCase(CreatureConstants.Dragon_White_Ancient)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Brass_Young)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult)]
        [TestCase(CreatureConstants.Dragon_Brass_Old)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm)]
        [TestCase(CreatureConstants.Drider)]
        [TestCase(CreatureConstants.FormianQueen)]
        [TestCase(CreatureConstants.Lammasu)]
        [TestCase(CreatureConstants.Lillend)]
        [TestCase(CreatureConstants.Naga_Dark)]
        [TestCase(CreatureConstants.Naga_Guardian)]
        [TestCase(CreatureConstants.Naga_Spirit)]
        [TestCase(CreatureConstants.Naga_Water)]
        [TestCase(CreatureConstants.Nymph)]
        [TestCase(CreatureConstants.Rakshasa)]
        [TestCase(CreatureConstants.TrumpetArchon)]
        public void CanGenerateSpellsForThoseWhoCastAsSpellcaster(string creatureName)
        {
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.Magic, Is.Not.Null);
            Assert.That(creature.Magic.Caster, Is.Not.Empty);
            creatureAsserter.VerifyMagic(creature, creature.Summary);
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
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamage(string creatureName)
        {
            var creature = creatureGenerator.Generate(false, creatureName);
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
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CanGenerateCreature(string creatureName)
        {
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Characters))]
        public void CanGenerateCreatureAsCharacter(string creatureName)
        {
            var creature = creatureGenerator.Generate(true, creatureName);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CanGenerateHumanTemplate(string template)
        {
            var creature = creatureGenerator.Generate(false, CreatureConstants.Human, null, template);
            creatureAsserter.AssertCreature(creature);

            if (template != CreatureConstants.Templates.None)
                Assert.That(creature.Templates, Has.Count.EqualTo(1).And.Contains(template));
            else
                Assert.That(creature.Templates, Is.Empty);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void CanGenerateHumanTemplateAsCharacter(string template)
        {
            if (template == CreatureConstants.Templates.Skeleton || template == CreatureConstants.Templates.Zombie)
            {
                Assert.Pass($"Template {template} cannot be a character");
            }

            var creature = creatureGenerator.Generate(true, CreatureConstants.Human, null, template);
            creatureAsserter.AssertCreatureAsCharacter(creature);

            if (template != CreatureConstants.Templates.None)
                Assert.That(creature.Templates, Has.Count.EqualTo(1).And.Contains(template));
            else
                Assert.That(creature.Templates, Is.Empty);
        }

        [TestCase(false, CreatureConstants.Aboleth)]
        [TestCase(true, CreatureConstants.Aboleth)]
        [TestCase(false, CreatureConstants.Ape, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Ape_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Badger, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Badger_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Basilisk, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Basilisk_Greater, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Bat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Bat_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bat_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Bat_Swarm, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bat_Swarm, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Bear_Black, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bear_Brown, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bear_Polar, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bear_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bee_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Bison, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Boar, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Boar_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.BombardierBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(true, CreatureConstants.Bugbear)]
        [TestCase(false, CreatureConstants.Bugbear, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Cat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Cat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Centipede_Monstrous_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Centipede_Monstrous_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Centipede_Monstrous_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Centipede_Monstrous_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Centipede_Monstrous_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Chimera_Black, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Chimera_Blue, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Chimera_Green, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Chimera_Red, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Chimera_White, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Crocodile, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Crocodile_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Deinonychus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Dog, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Dog_Riding, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Dragon_Black_Young, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Dragon_Red_Wyrmling, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Dragon_Red_VeryYoung, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Dragon_Red_Young, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Dragon_Red_Juvenile, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Dragon_Red_YoungAdult, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Dragon_White_Old, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Eagle, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Eagle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Elasmosaurus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Elephant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Elf_Half)]
        [TestCase(true, CreatureConstants.Elf_Half)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.Lich)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.Lich)]
        [TestCase(false, CreatureConstants.Elf_Half, CreatureConstants.Templates.Vampire)]
        [TestCase(true, CreatureConstants.Elf_Half, CreatureConstants.Templates.Vampire)]
        [TestCase(false, CreatureConstants.Elf_High, CreatureConstants.Templates.Lich)]
        [TestCase(true, CreatureConstants.Elf_High, CreatureConstants.Templates.Lich)]
        [TestCase(false, CreatureConstants.Elf_High, CreatureConstants.Templates.Vampire)]
        [TestCase(true, CreatureConstants.Elf_High, CreatureConstants.Templates.Vampire)]
        [TestCase(false, CreatureConstants.Ettin, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.FireBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Gargoyle)]
        [TestCase(true, CreatureConstants.Gargoyle)]
        [TestCase(false, CreatureConstants.Gargoyle, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Gargoyle_Kapoacinth, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Giant_Cloud, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Giant_Frost)]
        [TestCase(true, CreatureConstants.Giant_Frost)]
        [TestCase(false, CreatureConstants.Giant_Hill, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted)]
        [TestCase(false, CreatureConstants.Giant_Hill, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural)]
        [TestCase(false, CreatureConstants.Girallon, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.GrayRender, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Griffon, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Grig, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Grig_WithFiddle, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Harpy)]
        [TestCase(true, CreatureConstants.Harpy)]
        [TestCase(false, CreatureConstants.Hawk, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Hawk, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Hieracosphinx, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Hippogriff, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Horse_Heavy, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Horse_Heavy_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Horse_Light, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Horse_Light_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Human)]
        [TestCase(true, CreatureConstants.Human)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Ghost)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.HalfDragon_Black)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.HalfDragon_Black)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.HalfFiend)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.HalfDragon_Black, CreatureConstants.Templates.HalfFiend)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.HalfDragon_Black, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lich)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lich)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Boar_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Tiger_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, CreatureConstants.Templates.HalfDragon_White)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, CreatureConstants.Templates.HalfDragon_White)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Vampire)]
        [TestCase(true, CreatureConstants.Human, CreatureConstants.Templates.Vampire)]
        [TestCase(false, CreatureConstants.Human,
            CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted,
            CreatureConstants.Templates.HalfDragon_Blue,
            CreatureConstants.Templates.Ghost)]
        [TestCase(true, CreatureConstants.Human,
            CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted,
            CreatureConstants.Templates.HalfDragon_Blue,
            CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Human, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Kobold)]
        [TestCase(true, CreatureConstants.Kobold)]
        [TestCase(false, CreatureConstants.Kobold, CreatureConstants.Templates.Zombie)]
        [TestCase(true, CreatureConstants.Lammasu)]
        [TestCase(false, CreatureConstants.Lammasu)]
        [TestCase(false, CreatureConstants.Lammasu, CreatureConstants.Templates.HalfDragon_Gold)]
        [TestCase(false, CreatureConstants.Lammasu, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(true, CreatureConstants.Lammasu, CreatureConstants.Templates.CelestialCreature, CreatureConstants.Templates.HalfDragon_Gold)]
        [TestCase(false, CreatureConstants.Lammasu, CreatureConstants.Templates.CelestialCreature, CreatureConstants.Templates.HalfDragon_Gold)]
        [TestCase(true, CreatureConstants.Lammasu, CreatureConstants.Templates.HalfDragon_Gold, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Lammasu, CreatureConstants.Templates.HalfDragon_Gold, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Lion, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Lion_Dire, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Lizard, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Lizard, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(true, CreatureConstants.Medusa, CreatureConstants.Templates.HalfDragon_Red)]
        [TestCase(false, CreatureConstants.Megaraptor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Megaraptor, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Minotaur)]
        [TestCase(true, CreatureConstants.Minotaur)]
        [TestCase(false, CreatureConstants.Minotaur, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Monkey, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Mummy)]
        [TestCase(true, CreatureConstants.Mummy)]
        [TestCase(false, CreatureConstants.Octopus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Octopus_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Ogre)]
        [TestCase(true, CreatureConstants.Ogre)]
        [TestCase(false, CreatureConstants.Ogre, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Orc_Half)]
        [TestCase(true, CreatureConstants.Orc_Half)]
        [TestCase(false, CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(true, CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Orc_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(true, CreatureConstants.Orc_Half, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(true, CreatureConstants.Orc_Half, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Orc_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(true, CreatureConstants.Orc_Half, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Otyugh, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Otyugh, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Owl, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Owl, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Owl_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Owlbear, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Pony, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Pony_War, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Porpoise, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.PrayingMantis_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Rakshasa)]
        [TestCase(true, CreatureConstants.Rakshasa)]
        [TestCase(false, CreatureConstants.Rat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Rat, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Rat_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Raven, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Raven, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.RazorBoar, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Rhinoceras, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Roc, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Scorpion_Monstrous_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Scorpion_Monstrous_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Scorpion_Monstrous_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Scorpion_Monstrous_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Scorpion_Monstrous_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.SeaCat, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.ShamblingMound)]
        [TestCase(true, CreatureConstants.ShamblingMound)]
        [TestCase(false, CreatureConstants.ShamblingMound, CreatureConstants.Templates.HalfFiend)]
        [TestCase(true, CreatureConstants.ShamblingMound, CreatureConstants.Templates.HalfFiend)]
        [TestCase(false, CreatureConstants.Shark_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Shark_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Shark_Large, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Shark_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Shark_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Shrieker, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Skum)]
        [TestCase(true, CreatureConstants.Skum)]
        [TestCase(false, CreatureConstants.Skum, CreatureConstants.Templates.Ghost)]
        [TestCase(true, CreatureConstants.Skum, CreatureConstants.Templates.Ghost)]
        [TestCase(false, CreatureConstants.Snake_Constrictor, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Constrictor, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Small, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Medium, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Large, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Huge, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Snake_Viper_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Small, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Medium, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Large, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Huge, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_Hunter_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Squid, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Squid_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Squid_Giant, CreatureConstants.Templates.HalfDragon_Blue)]
        [TestCase(false, CreatureConstants.Squid_Giant, CreatureConstants.Templates.HalfDragon_Green)]
        [TestCase(false, CreatureConstants.StagBeetle_Giant, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Tiger, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Tiger_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Toad, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Toad, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Triceratops, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Troglodyte, CreatureConstants.Templates.HalfCelestial)]
        [TestCase(false, CreatureConstants.Troglodyte, CreatureConstants.Templates.Zombie)]
        [TestCase(false, CreatureConstants.Troll)]
        [TestCase(true, CreatureConstants.Troll)]
        [TestCase(false, CreatureConstants.Troll, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Tyrannosaurus, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Unicorn, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Wasp_Giant, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Weasel, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Weasel, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Weasel_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Whale_Baleen, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Whale_Cachalot, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Whale_Orca, CreatureConstants.Templates.CelestialCreature)]
        [TestCase(false, CreatureConstants.Wolf, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Wolf, CreatureConstants.Templates.Skeleton)]
        [TestCase(false, CreatureConstants.Wolf_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Wolverine, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Wolverine_Dire, CreatureConstants.Templates.FiendishCreature)]
        [TestCase(false, CreatureConstants.Wyvern, CreatureConstants.Templates.Zombie)]
        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.ProblematicCreaturesTestCases))]
        public void CanGenerateTemplate(bool asCharacter, string creatureName, params string[] templates)
        {
            var creature = creatureGenerator.Generate(asCharacter, creatureName, null, templates);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Templates, Is.EqualTo(templates));

            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Destrachan)]
        [TestCase(CreatureConstants.Grimlock)]
        [TestCase(CreatureConstants.Yrthak)]
        public void BUG_DoesNotHaveSight(string creatureName)
        {
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = creature.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.Blind));
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
            var elf = creatureGenerator.Generate(false, elfName);
            creatureAsserter.AssertCreature(elf);

            Assert.That(elf.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = elf.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.ShieldProficiency));
        }

        [Test]
        public void BUG_HalfOrcIsNotSensitiveToLight()
        {
            var halfOrc = creatureGenerator.Generate(false, CreatureConstants.Orc_Half);
            creatureAsserter.AssertCreature(halfOrc);

            Assert.That(halfOrc.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = halfOrc.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LightSensitivity));
        }

        [Test]
        public void BUG_NightcrawlerHasConcentration()
        {
            var nightcrawler = creatureGenerator.Generate(false, CreatureConstants.Nightcrawler);
            creatureAsserter.AssertCreature(nightcrawler);

            Assert.That(nightcrawler.Skills, Is.Not.Empty);

            var skillNames = nightcrawler.Skills.Select(q => q.Name);
            Assert.That(skillNames, Contains.Item(SkillConstants.Concentration));
        }

        [TestCase(AbilityConstants.RandomizerRolls.Best, 16, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Default, 10, 11)]
        [TestCase(AbilityConstants.RandomizerRolls.Good, 12, 15)]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes, 6, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Poor, 4, 9)]
        [TestCase(AbilityConstants.RandomizerRolls.Raw, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Wild, 2, 20)]
        [TestCase("42d600+9266", 42 + 9266, 42 * 600 + 9266)]
        public void Generate_HumanWithAbilityRandomizer(string roll, int lower, int upper)
        {
            var randomizer = new AbilityRandomizer();
            randomizer.Roll = roll;

            var creature = creatureGenerator.Generate(false, CreatureConstants.Human, randomizer);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Abilities.Values.Select(v => v.BaseScore), Is.All.InRange(lower, upper));
        }

        [Test]
        public void Generate_HumanWithSetRolls()
        {
            var randomizer = new AbilityRandomizer();
            randomizer.SetRolls[AbilityConstants.Charisma] = 9266;
            randomizer.SetRolls[AbilityConstants.Constitution] = 90210;
            randomizer.SetRolls[AbilityConstants.Dexterity] = 42;
            randomizer.SetRolls[AbilityConstants.Intelligence] = 600;
            randomizer.SetRolls[AbilityConstants.Strength] = 1337;
            randomizer.SetRolls[AbilityConstants.Wisdom] = 1336;

            var creature = creatureGenerator.Generate(false, CreatureConstants.Human, randomizer);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(9266));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90210));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600));
            Assert.That(creature.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(1337));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1336));
        }

        [Test]
        public void Generate_HumanWithAdvancementBonuses()
        {
            var randomizer = new AbilityRandomizer();
            randomizer.AbilityAdvancements[AbilityConstants.Charisma] = 9266;
            randomizer.AbilityAdvancements[AbilityConstants.Constitution] = 90210;
            randomizer.AbilityAdvancements[AbilityConstants.Dexterity] = 42;
            randomizer.AbilityAdvancements[AbilityConstants.Intelligence] = 600;
            randomizer.AbilityAdvancements[AbilityConstants.Strength] = 1337;
            randomizer.AbilityAdvancements[AbilityConstants.Wisdom] = 1336;

            var creature = creatureGenerator.Generate(false, CreatureConstants.Human, randomizer);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment, Is.EqualTo(9266));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(90210));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment, Is.EqualTo(600));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(1337));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].AdvancementAdjustment, Is.EqualTo(1336));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void Generate_HumanWithPriorityAbility(string ability)
        {
            var randomizer = new AbilityRandomizer();
            randomizer.PriorityAbility = ability;
            randomizer.Roll = AbilityConstants.RandomizerRolls.Wild;

            var creature = creatureGenerator.Generate(false, CreatureConstants.Human, randomizer);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Abilities.Values.Max(v => v.BaseScore), Is.EqualTo(creature.Abilities[ability].BaseScore));
        }

        [Test]
        [Repeat(10)] //INFO: We have to repeat to ensure we get males, since gender is random
        public void BUG_MaleSpiderEaterDoesNotHaveImplantAbility()
        {
            var spiderEater = creatureGenerator.Generate(false, CreatureConstants.SpiderEater);
            creatureAsserter.AssertCreature(spiderEater);

            Assert.That(spiderEater.Attacks, Is.Not.Empty);
            var attackNames = spiderEater.Attacks.Select(q => q.Name);

            if (spiderEater.Demographics.Gender == GenderConstants.Male)
            {
                Assert.That(attackNames, Does.Not.Contain("Implant"));
            }
            else if (spiderEater.Demographics.Gender == GenderConstants.Female)
            {
                Assert.That(attackNames, Contains.Item("Implant"));
            }
        }

        [TestCase(CreatureConstants.Human, true, false)]
        [TestCase(CreatureConstants.Snake_Constrictor, false, true)]
        [TestCase(CreatureConstants.Wolf, true, true)]
        public void SnakeHasLength(string creatureName, bool hasHeight, bool hasLength)
        {
            var creature = creatureGenerator.Generate(false, creatureName);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.Demographics.Height, Is.Not.Null);
            Assert.That(creature.Demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(creature.Demographics.Height.Description, Is.Not.Empty);
            Assert.That(creature.Demographics.Length, Is.Not.Null);
            Assert.That(creature.Demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(creature.Demographics.Length.Description, Is.Not.Empty);

            if (hasHeight)
            {
                Assert.That(creature.Demographics.Height.Value, Is.Positive);
            }
            else
            {
                Assert.That(creature.Demographics.Height.Value, Is.Zero);
            }

            if (hasLength)
            {
                Assert.That(creature.Demographics.Length.Value, Is.Positive);
            }
            else
            {
                Assert.That(creature.Demographics.Length.Value, Is.Zero);
            }
        }
    }
}
