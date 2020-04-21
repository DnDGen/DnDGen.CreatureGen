using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class EquipmentGeneratorTests
    {
        private IEquipmentGenerator equipmentGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IItemsGenerator> mockItemGenerator;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private List<Feat> feats;
        private List<Attack> attacks;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockItemGenerator = new Mock<IItemsGenerator>();
            mockPercentileSelector = new Mock<IPercentileSelector>();

            equipmentGenerator = new EquipmentGenerator(
                mockCollectionSelector.Object,
                mockItemGenerator.Object,
                mockPercentileSelector.Object);

            abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength) { BaseScore = 42 };
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 600 };

            feats = new List<Feat>();
            feats.Add(new Feat { Name = "random feat" });
            feats.Add(new Feat { Name = "other random feat", Foci = new[] { "random focus", "other random focus" } });

            attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "natural melee attack", IsNatural = true, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = "natural ranged attack", IsNatural = true, IsMelee = false, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = "unnatural melee attack", IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Dexterity] });
            attacks.Add(new Attack { Name = "unnatural ranged attack", IsNatural = false, IsMelee = false, BaseAbility = abilities[AbilityConstants.Dexterity] });
            attacks.Add(new Attack { Name = "special attack", IsNatural = false, IsMelee = true, IsSpecial = true });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(new[]
                {
                    FeatConstants.WeaponProficiency_Exotic,
                    FeatConstants.WeaponProficiency_Martial,
                    FeatConstants.WeaponProficiency_Simple
                });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
                .Returns(new[]
                {
                    FeatConstants.ArmorProficiency_Light,
                    FeatConstants.ArmorProficiency_Medium,
                    FeatConstants.ArmorProficiency_Heavy,
                    FeatConstants.ShieldProficiency,
                    FeatConstants.ShieldProficiency_Tower,
                });
        }

        [Test]
        public void GenerateNoEquipment_WhenCannotUseEquipment()
        {
            var equipment = equipmentGenerator.Generate("creature", false, feats, 9266, attacks, abilities);
            Assert.That(equipment, Is.Not.Null);
            Assert.That(equipment.Armor, Is.Null);
            Assert.That(equipment.Shield, Is.Null);
            Assert.That(equipment.Weapons, Is.Empty);
            Assert.That(equipment.Items, Is.Empty);
        }

        [Test]
        public void GenerateNoWeapons_WhenNoUnnaturalAttacks()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoWeapons_WhenNoWeaponProficiencies()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoMeleeWeapons_WhenNoMeleeAttacks()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoRangedWeapons_WhenNoRangedAttacks()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_NonProficiencyFocus()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Club };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(foci).Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci)),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Club);

            var weapon = new Weapon();
            weapon.Name = "my club";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my club"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleNonProficiencyFoci_SingleFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Club, WeaponConstants.Dagger };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(foci).Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci)),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger);

            var weapon = new Weapon();
            weapon.Name = "my dagger";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my dagger"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleNonProficiencyFoci_MultipleFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Club, WeaponConstants.Dagger };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });
            var otherFoci = new[] { WeaponConstants.Club, WeaponConstants.LightMace };
            feats.Add(new Feat { Name = "other weapon feat", Foci = otherFoci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(foci).Except(otherFoci).Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci.Union(otherFoci))),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.LightMace);

            var weapon = new Weapon();
            weapon.Name = "my mace";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my mace"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_Focus_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Club };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Club);

            var weapon = new Weapon();
            weapon.Name = "my club";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my club"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_MultipleFoci_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Club, WeaponConstants.Dagger };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger);

            var weapon = new Weapon();
            weapon.Name = "my dagger";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my dagger"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_Focus_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Greataxe };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Greataxe);

            var weapon = new Weapon();
            weapon.Name = "my greataxe";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Greataxe))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my greataxe"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_MultipleFoci_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Greataxe, WeaponConstants.Greatsword };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Greatsword);

            var weapon = new Weapon();
            weapon.Name = "my greatsword";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Greatsword))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my greatsword"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_Focus_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Whip };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Whip);

            var weapon = new Weapon();
            weapon.Name = "my whip";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Whip))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my whip"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_MultipleFoci_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            var foci = new[] { WeaponConstants.Whip, WeaponConstants.SpikedChain };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var melee = WeaponConstants.GetAllMelee(false, false);
            var non = melee.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.SpikedChain);

            var weapon = new Weapon();
            weapon.Name = "my spiked chain";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.SpikedChain))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my spiked chain"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_All_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my simple weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_All_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });

            var martial = WeaponConstants.GetAllMartial(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var martialMelee = martial.Intersect(melee);
            var non = melee.Except(martial);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(martialMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("martial weapon");

            var weapon = new Weapon();
            weapon.Name = "my martial weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "martial weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my martial weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_Proficient_All_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { GroupConstants.All } });

            var exotic = WeaponConstants.GetAllExotic(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var exoticMelee = exotic.Intersect(melee);
            var non = melee.Except(exotic);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(exoticMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("exotic weapon");

            var weapon = new Weapon();
            weapon.Name = "my exotic weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "exotic weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my exotic weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_NotProficient()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("not proficient weapon");

            var weapon = new Weapon();
            weapon.Name = "my weird weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "not proficient weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my weird weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-4));
        }

        [Test]
        public void GenerateMeleeWeapon_TwoHandedWeapon_TakesUp2Hands_CannotGenerateOtherWeapons()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyBonusFromFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { "my simple weapon" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my simple weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(90210));
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyBonusFromFeat_BaseNameMatch()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { "simple base name" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my simple weapon";
            weapon.Damage = "my damage";
            weapon.BaseNames = new[] { "my base name", "simple base name" };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(90210));
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyBonusesFromMultipleFeats()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { "my simple weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 42, Foci = new[] { "my simple weapon", "my other weapon" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my simple weapon";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(90210)
                .And.Contains(42));
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_MagicBonus()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my simple weapon";
            weapon.Damage = "my damage";
            weapon.Magic.Bonus = 1337;
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(1337));
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyDexterityForMeleeInsteadOfStrengthForLightWeaponsIfWeaponFinesse()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponFinesse });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dagger;
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateMeleeWeapon_WeaponFinesse_DoesNotChangeOneHandedWeapon()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponFinesse });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.LightMace);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.LightMace;
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightMace))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.LightMace));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateMeleeWeapon_WeaponFinesse_DoesNotChangeTwoHandedWeapon()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponFinesse });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Quarterstaff);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Quarterstaff;
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Quarterstaff))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Quarterstaff));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleWeapons_FromSingleAttack_WithoutTwoWeaponFightingFeat_OneHanded()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var oneHanded = WeaponConstants.GetAllOneHandedMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var oneHandedSimpleMelee = oneHanded.Intersect(simpleMelee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .SetupSequence(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(oneHandedSimpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger)
                .Returns(WeaponConstants.LightMace);

            var mace = new Weapon();
            mace.Name = WeaponConstants.LightMace;
            mace.Damage = "my mace damage";

            var dagger = new Weapon();
            dagger.Name = WeaponConstants.Dagger;
            dagger.Damage = "my dagger damage";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightMace))
                .Returns(mace);
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(dagger);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(2));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(dagger));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(mace));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my dagger damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-6));
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
            Assert.That(attacks[6].Name, Is.EqualTo(WeaponConstants.LightMace));
            Assert.That(attacks[6].DamageRoll, Is.EqualTo("my mace damage"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);
            Assert.That(attacks[6].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-10));
            Assert.That(attacks[6].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            //rare, to choose two-weapon (or multi-weapon) if not chosen feat
            //verify penalties
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleWeapons_FromSingleAttack_WithoutTwoWeaponFightingFeat_Light()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var oneHanded = WeaponConstants.GetAllOneHandedMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var oneHandedSimpleMelee = oneHanded.Intersect(simpleMelee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .SetupSequence(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(oneHandedSimpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger)
                .Returns(WeaponConstants.LightMace);

            var mace = new Weapon();
            mace.Name = WeaponConstants.LightMace;
            mace.Damage = "my mace damage";

            var dagger = new Weapon();
            dagger.Name = WeaponConstants.Dagger;
            dagger.Damage = "my dagger damage";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightMace))
                .Returns(mace);
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(dagger);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(2));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(dagger));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(mace));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my dagger damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(2).And.Contains(-6).And.Contains(2));
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
            Assert.That(attacks[6].Name, Is.EqualTo(WeaponConstants.LightMace));
            Assert.That(attacks[6].DamageRoll, Is.EqualTo("my mace damage"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);
            Assert.That(attacks[6].AttackBonuses, Has.Count.EqualTo(2).And.Contains(-10).And.Contains(2));
            Assert.That(attacks[6].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            //rare, to choose two-weapon (or multi-weapon) if not chosen feat
            //verify penalties
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleWeapons_FromSingleAttack_WhenTwoWeaponFightingSelected()
        {
            //chosen feat, so generate two
            //verify penalties
            //verify only one-handed or light weapons
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_MultipleWeapons_FromSingleAttack_WhenMultiWeaponFightingSelected()
        {
            //chosen feat, so generate = number of hands
            //verify penalties
            //verify only one-handed or light weapons
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyTwoWeaponFightingPenalties_WithBonuses()
        {
            //two-weapon fighting
            //multiweapon fighting
            //verify penalties
            //verify only one-handed or light weapons
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyTwoWeaponFightingPenalties_ExtraOffhandAttack()
        {
            //improved two-weapon fighting
            //improved multiweapon fighting
            //verify penalties
            //verify only one-handed or light weapons
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyTwoWeaponFightingPenalties_MultipleExtraOffhandAttack()
        {
            //greater two-weapon fighting
            //greater multiweapon fighting
            //verify penalties
            //verify only one-handed or light weapons
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyImprovedCriticalFeat()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMultipleMeleeWeapons()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_NonProficiencyFocus()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_MultipleNonProficiencyFoci()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Simple()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Simple()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Martial()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Martial()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Exotic()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Exotic()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Simple()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Martial()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Exotic()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_NotProficient()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusFromFeat()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusFromRelevantFeat()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusesFromFeats()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_WithAmmunition()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_Shuriken()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_CompositeLongbow_ReceivesStrength()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_CompositeShortbow_ReceivesStrength()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_ApplyImprovedCriticalFeat()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateRangedWeapon_MagicBonus()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMultipleRangedWeapons()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateNoArmor_IfNotProficient()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateNoShield_IfNotProficient()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateNoShield_IfNotEnoughHands_TwoHandedWeapon()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateNoShield_IfNotEnoughHands_MultipleMeleeAttacks()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateShield_IfEnoughHands_MultipleMeleeAttacks()
        {
            //two attacks, but 3 hands
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateArmor_Light()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateArmor_Medium()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateArmor_Heavy()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateShield()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateShield_Tower()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateArmor_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateShield_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateItem_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateCustomItem_Predetermined()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMultipleItems()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateAllEquipment()
        {
            Assert.Fail("not yet written");
        }
    }

    public static class LinqExtensions
    {
        public static bool IsEquivalentTo<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return source.Count() == target.Count()
                && !source.Except(target).Any();
        }
    }
}
