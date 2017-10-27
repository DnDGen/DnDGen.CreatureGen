using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Items;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Items.Magical;
using TreasureGen.Items.Mundane;

namespace CreatureGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class WeaponGeneratorTests
    {
        private IWeaponGenerator weaponGenerator;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<MundaneItemGenerator> mockMundaneWeaponGenerator;
        private Mock<MagicalItemGenerator> mockMagicalWeaponGenerator;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Weapon magicalWeapon;
        private List<Feat> feats;
        private List<string> proficiencyFeats;
        private CharacterClass characterClass;
        private List<string> allProficientWeapons;
        private Race race;
        private string powerTableName;
        private string power;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneWeaponGenerator = new Mock<MundaneItemGenerator>();
            mockMagicalWeaponGenerator = new Mock<MagicalItemGenerator>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            var generator = new ConfigurableIterationGenerator(3);
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            weaponGenerator = new WeaponGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object, generator, mockJustInTimeFactory.Object);
            magicalWeapon = new Weapon();
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            proficiencyFeats = new List<string>();
            allProficientWeapons = new List<string>();
            race = new Race();

            race.Size = "size";
            race.BaseRace = "base race";
            magicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Club);
            magicalWeapon.IsMagical = true;
            characterClass.Name = "class name";
            characterClass.Level = 9266;
            feats.Add(new Feat { Name = "all proficiency", Foci = new[] { FeatConstants.Foci.All } });
            proficiencyFeats.Add(feats[0].Name);

            powerTableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 9266);
            power = "power";
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(magicalWeapon);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feats[0].Name)).Returns(allProficientWeapons);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));

            mockJustInTimeFactory.Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon)).Returns(mockMundaneWeaponGenerator.Object);
            mockJustInTimeFactory.Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon)).Returns(mockMagicalWeaponGenerator.Object);
        }

        private IEnumerable<string> ProficientWeaponSet(IEnumerable<string> weapons)
        {
            return It.Is<IEnumerable<string>>(ss => ss.Intersect(weapons).Count() == weapons.Count() && weapons.Count() == ss.Count());
        }

        private IEnumerable<string> ProficientWeaponSet()
        {
            return ProficientWeaponSet(allProficientWeapons);
        }

        [Test]
        public void GenerateNoWeapon()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { FeatConstants.Foci.UnarmedStrike } });

            proficiencyFeats.Clear();
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
        }

        [Test]
        public void GenerateMundaneWeapon()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Club);
            allProficientWeapons.Remove(magicalWeapon.Name);

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(), race.Size)).Returns(mundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        private Weapon CreateWeapon(string name)
        {
            var weapons = WeaponConstants.GetBaseNames();
            if (!weapons.Contains(name))
                throw new ArgumentException($"{name} is not a valid weapon");

            var weapon = new Weapon();
            weapon.Name = name;
            weapon.ItemType = ItemTypeConstants.Weapon;
            weapon.Size = race.Size;

            allProficientWeapons.Add(name);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(It.Is<Weapon>(w => w.Name == weapon.Name), false)).Returns(weapon);

            return weapon;
        }

        private Weapon CreateOneHandedMeleeWeapon(string name)
        {
            var weapon = CreateWeapon(name);
            weapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.Ranged };

            return weapon;
        }

        private Weapon CreateTwoHandedMeleeWeapon(string name)
        {
            var weapon = CreateWeapon(name);
            weapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.Ranged, AttributeConstants.TwoHanded };

            return weapon;
        }

        private Weapon CreateRangedWeapon(string name)
        {
            var weapon = CreateWeapon(name);
            weapon.Attributes = new[] { AttributeConstants.Ranged };

            return weapon;
        }

        private Weapon CreateAmmunition(string name)
        {
            var weapon = CreateWeapon(name);
            weapon.Attributes = new[] { AttributeConstants.Ranged, AttributeConstants.Ammunition };

            return weapon;
        }

        [Test]
        public void IfNotAMundaneWeapon_Regenerate()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = new Item { Name = WeaponConstants.Glaive, ItemType = ItemTypeConstants.Weapon };

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.GenerateFrom(ProficientWeaponSet(), race.Size)).Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void CanWieldSpecificMundaneWeaponProficiency()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            mockMundaneWeaponGenerator.SetupSequence(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon).Returns(wrongMundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            var wrongSpecialties = new[] { wrongMundaneWeapon.Name };
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void DoNotPreferMundaneWeaponPickedAsFocusForWeaponFamiliarity()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            var wrongSpecialties = new[] { wrongMundaneWeapon.Name };
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = FeatConstants.WeaponFamiliarity, Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon), weapon.Name);
        }

        [Test]
        public void PreferAnyMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var otherMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Spear);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            var wrongSpecialties = new[] { wrongMundaneWeapon.Name };
            var multipleSpecialties = new[] { mundaneWeapon.Name, otherMundaneWeapon.Name };
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(multipleSpecialties), race.Size)).Returns(otherMundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = multipleSpecialties });
            feats.Add(new Feat { Name = "feat4", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMundaneWeapon), weapon.Name);
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(), race.Size)).Returns(wrongMundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void PreferAnyMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var otherMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Spear);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var specialties = new[] { mundaneWeapon.Name };
            var multipleSpecialties = new[] { mundaneWeapon.Name, otherMundaneWeapon.Name };
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(), race.Size)).Returns(wrongMundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(specialties), race.Size)).Returns(mundaneWeapon);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(multipleSpecialties), race.Size)).Returns(otherMundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = multipleSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMundaneWeapon));
        }

        [Test]
        public void NoPreferenceForMundaneWeapons()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.GenerateFrom(ProficientWeaponSet(), race.Size))
                .Returns(mundaneWeapon).Returns(wrongMundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void GenerateMagicalWeapon()
        {
            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfMagicalWeaponIsNotWeapon_Regenerate()
        {
            var wrongMagicalWeapon = new Item { Name = WeaponConstants.Halberd, ItemType = ItemTypeConstants.Weapon };
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void CanWieldSpecificMagicalWeaponProficiency()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);
            var specialties = new[] { magicalWeapon.Name };
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon).Returns(wrongMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { magicalWeapon.Name } });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);

            var specialties = new[] { magicalWeapon.Name };
            var wrongSpecialties = new[] { wrongMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void DoNotPreferMagicalWeaponsPickedAsFocusForWeaponFamiliarity()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);

            var specialties = new[] { magicalWeapon.Name };
            var wrongSpecialties = new[] { wrongMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon);

            feats.Add(new Feat { Name = FeatConstants.WeaponFamiliarity, Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void PreferAnyMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var otherMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.LightCrossbow);
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);

            var specialties = new[] { magicalWeapon.Name };
            var multipleSpecialties = new[] { magicalWeapon.Name, otherMagicalWeapon.Name };
            var wrongSpecialties = new[] { wrongMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(wrongMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(multipleSpecialties), race.Size)).Returns(otherMagicalWeapon);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(wrongMagicalWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = multipleSpecialties });
            feats.Add(new Feat { Name = "feat4", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon));
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);

            var specialties = new[] { magicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(wrongMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void PreferAnyMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            var otherMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.LightCrossbow);
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);

            var specialties = new[] { magicalWeapon.Name };
            var multipleSpecialties = new[] { magicalWeapon.Name, otherMagicalWeapon.Name };
            var wrongSpecialties = new[] { wrongMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(wrongMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(magicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(multipleSpecialties), race.Size)).Returns(otherMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = multipleSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon));
        }

        [Test]
        public void NoPreferenceForMagicalWeapons()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(magicalWeapon).Returns(wrongMagicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void MundaneWeaponMustFitCharacter()
        {
            var mundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            var otherWrongMundaneWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Flail);

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.GenerateFrom(ProficientWeaponSet(), race.Size))
                .Returns(wrongMundaneWeapon).Returns(otherWrongMundaneWeapon).Returns(mundaneWeapon);

            wrongMundaneWeapon.Size = "bigger size";
            otherWrongMundaneWeapon.Size = "smaller size";

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon), weapon.Name);
        }

        [Test]
        public void MagicalWeaponMustFitCharacter()
        {
            var magicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longsword);
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Rapier);
            var otherWrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Flail);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(wrongMagicalWeapon).Returns(otherWrongMagicalWeapon).Returns(magicalWeapon);

            wrongMagicalWeapon.Size = "bigger size";
            otherWrongMagicalWeapon.Size = "smaller size";

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void SaveBonusesOfAllDoNotCountAsProficiencyFeats()
        {
            feats.Add(new Feat { Name = FeatConstants.SaveBonus, Foci = new[] { FeatConstants.Foci.All } });
            feats[0].Foci = new[] { magicalWeapon.Name };

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void GeneralWeaponCannotBeAmmunition()
        {
            var ammunition = CreateAmmunition(WeaponConstants.Arrow);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(ammunition).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        //INFO: Example here is Shurikens
        [Test]
        public void ThrownAmmunitionIsAllowed()
        {
            var shuriken = CreateAmmunition(WeaponConstants.Shuriken);
            shuriken.Attributes = shuriken.Attributes.Union(new[] { AttributeConstants.Thrown });
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(shuriken).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(shuriken), weapon.Name);
        }

        [Test]
        public void GenerateMundaneDefaultWeapon()
        {
            var wrongWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Sai);
            wrongWeapon.Size = "wrong size";

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(ProficientWeaponSet(), race.Size)).Returns(wrongWeapon);

            var defaultWeapon = new Weapon();
            mockMundaneWeaponGenerator.Setup(g => g.GenerateFrom(It.Is<Weapon>(i => i.ItemType == ItemTypeConstants.Weapon && i.Name == allProficientWeapons[0] && i.Size == race.Size), true))
                .Returns(defaultWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(defaultWeapon), weapon.Name);
        }

        [Test]
        public void GenerateMagicalDefaultWeapon()
        {
            var wrongMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Halberd);
            wrongMagicalWeapon.Size = "wrong size";

            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(wrongMagicalWeapon);

            var defaultWeapon = new Weapon();
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(It.Is<Weapon>(i => i.ItemType == ItemTypeConstants.Weapon && i.Name == allProficientWeapons[0] && i.Magic.Bonus == 1), true))
                .Returns(defaultWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(defaultWeapon));
        }

        [Test]
        public void GenerateAmmunition()
        {
            var ammunition = CreateAmmunition(WeaponConstants.Arrow);

            var ammunitions = new[] { "ammo" };
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(ammunitions), race.Size))
                .Returns(magicalWeapon).Returns(ammunition);

            var generatedAmmunition = weaponGenerator.GenerateAmmunition(characterClass, race, "ammo");
            Assert.That(generatedAmmunition, Is.EqualTo(ammunition), generatedAmmunition.Name);
        }

        [Test]
        public void MeleeWeaponMustBeMelee()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(rangedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfMeleeWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.LightCrossbow);

            var wrongSpecialties = new[] { rangedWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(rangedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(otherMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfMeleeWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateRangedWeapon(WeaponConstants.LightCrossbow);

            var wrongSpecialties = new[] { rangedWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties))).Returns(rangedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties))).Returns(otherMagicalWeapon);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(rangedWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void GenerateNoMeleeWeapon()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);

            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { rangedWeapon.Name } });
            proficiencyFeats.Add("feat2");

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
        }

        [Test]
        public void IfMeleeWeaponsPossible_ReturnWeapon()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherRangedWeapon = CreateRangedWeapon(WeaponConstants.Shortbow);

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfNoMeleeWeaponsPossible_ReturnNothing()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherRangedWeapon = CreateRangedWeapon(WeaponConstants.Shortbow);
            allProficientWeapons.Remove(magicalWeapon.Name);

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
            mockMagicalWeaponGenerator.Verify(g => g.GenerateFrom(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void OneHandedMeleeWeaponMustBeMelee()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(rangedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void OneHandedMeleeWeaponMustBeOneHanded()
        {
            var twoHandedWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Greataxe);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(twoHandedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfOneHandedMeleeWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.LightCrossbow);

            var wrongSpecialties = new[] { rangedWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(rangedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(otherMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfOneHandedMeleeWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.LightCrossbow);

            var wrongSpecialties = new[] { rangedWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties))).Returns(rangedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties))).Returns(otherMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void DoNotGenerateOneHandedMeleeWeapon()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var twoHandedWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Greataxe);

            var wrongSpecialties = new[] { rangedWeapon.Name };
            var specialties = new[] { twoHandedWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties))).Returns(rangedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties))).Returns(twoHandedWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(twoHandedWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
        }

        [Test]
        public void IfOneHandedMeleeWeaponsPossible_ReturnWeapon()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var twoHandedWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Greataxe);

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfNoOneHandedMeleeWeaponsPossible_ReturnNothing()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var twoHandedWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Greataxe);
            allProficientWeapons.Remove(magicalWeapon.Name);

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
            mockMagicalWeaponGenerator.Verify(g => g.GenerateFrom(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void RangedWeaponMustNotBeMelee()
        {
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(magicalWeapon).Returns(rangedWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfRangedWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var meleeWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateRangedWeapon(WeaponConstants.LightCrossbow);

            var wrongSpecialties = new[] { meleeWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties), race.Size)).Returns(meleeWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties), race.Size)).Returns(otherMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfRangedWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var meleeWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Longbow);
            var otherMagicalWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.LightCrossbow);
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Bolas);

            var wrongSpecialties = new[] { meleeWeapon.Name };
            var specialties = new[] { otherMagicalWeapon.Name };
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(wrongSpecialties))).Returns(meleeWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(specialties))).Returns(otherMagicalWeapon);
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = wrongSpecialties });
            feats.Add(new Feat { Name = "feat3", Foci = specialties });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        [Test]
        public void RangedWeaponCannotBeAmmunition()
        {
            var ammunition = CreateAmmunition(WeaponConstants.Arrow);
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(ammunition).Returns(rangedWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        //INFO: Example here is Shurikens
        [Test]
        public void RangedThrownAmmunitionIsAllowed()
        {
            var shuriken = CreateAmmunition(WeaponConstants.Shuriken);
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            shuriken.Attributes = shuriken.Attributes.Union(new[] { AttributeConstants.Thrown });
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size))
                .Returns(shuriken).Returns(rangedWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(shuriken), weapon.Name);
        }

        [Test]
        public void GenerateNoRangedWeapon()
        {
            feats.Add(new Feat { Name = "feat2", Foci = new[] { magicalWeapon.Name } });
            proficiencyFeats.Add("feat2");

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
        }

        [Test]
        public void IfRangedWeaponsPossible_ReturnWeapon()
        {
            var meleeWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Scythe);
            var twoHandedMeleeWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Sickle);
            var rangedWeapon = CreateRangedWeapon(WeaponConstants.Longbow);
            var ammunition = CreateAmmunition(WeaponConstants.SlingBullet);

            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom(power, ProficientWeaponSet(), race.Size)).Returns(rangedWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        [Test]
        public void IfNoRangedWeaponsPossible_ReturnNothing()
        {
            var meleeWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Scythe);
            var otherMeleeWeapon = CreateTwoHandedMeleeWeapon(WeaponConstants.Sickle);
            var ammunition = CreateAmmunition(WeaponConstants.SlingBullet);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Null);
            mockMagicalWeaponGenerator.Verify(g => g.GenerateFrom(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 4)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 6)]
        [TestCase(14, 7)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(17, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 9)]
        [TestCase(20, 10)]
        public void NPCIsHalfLevel(int npcLevel, int effectiveLevel)
        {
            characterClass.Level = npcLevel;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var npcWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Glaive);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom("npc power", ProficientWeaponSet(), race.Size)).Returns(npcWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(npcWeapon));
        }

        [Test]
        public void LevelAdjustmentAffectsNPCLevelForWeapon()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var playerWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Glaive);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom("npc power", ProficientWeaponSet(), race.Size)).Returns(playerWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerWeapon));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        public void PlayerCharacterIsFullLevel(int level, int effectiveLevel)
        {
            characterClass.Level = level;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Glaive);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom("player power", ProficientWeaponSet(), race.Size)).Returns(playerWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerWeapon));
        }

        [Test]
        public void LevelAdjustmentAffectsPlayerLevelForWeapon()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerWeapon = CreateOneHandedMeleeWeapon(WeaponConstants.Glaive);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateFrom("player power", ProficientWeaponSet(), race.Size)).Returns(playerWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerWeapon));
        }
    }
}
