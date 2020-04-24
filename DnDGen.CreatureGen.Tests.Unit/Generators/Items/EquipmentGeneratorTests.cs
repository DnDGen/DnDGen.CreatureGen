using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Selectors;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using DnDGen.TreasureGen.Items.Mundane;
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
        private Mock<IItemSelector> mockItemSelector;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private List<Feat> feats;
        private List<Attack> attacks;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockItemGenerator = new Mock<IItemsGenerator>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockItemSelector = new Mock<IItemSelector>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();

            equipmentGenerator = new EquipmentGenerator(
                mockCollectionSelector.Object,
                mockItemGenerator.Object,
                mockPercentileSelector.Object,
                mockItemSelector.Object,
                mockJustInTimeFactory.Object);

            abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength) { BaseScore = 42 };
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 600 };

            feats = new List<Feat>();
            feats.Add(new Feat { Name = "random feat" });
            feats.Add(new Feat { Name = "other random feat", Foci = new[] { "random focus", "other random focus" } });

            attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "natural melee attack", IsNatural = true, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = "natural ranged attack", IsNatural = true, IsMelee = false, BaseAbility = abilities[AbilityConstants.Dexterity] });
            attacks.Add(new Attack { Name = "unnatural melee attack", IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightMace))
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
                .Returns(WeaponConstants.GnomeHookedHammer);

            var weapon = new Weapon();
            weapon.Name = "my gnome hooked hammer";
            weapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.GnomeHookedHammer))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my gnome hooked hammer"));
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
        public void GenerateMeleeWeapon_MasterworkBonus()
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
            weapon.Traits.Add(TraitConstants.Masterwork);
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
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(1));
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
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(2));
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
        public void GenerateRangedWeapon_MasterworkBonus()
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
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            var light = ArmorConstants.GetAllLightArmors(false);
            var medium = ArmorConstants.GetAllMediumArmors(false);
            var heavy = ArmorConstants.GetAllHeavyArmors(false);
            var armors = light;
            var non = medium.Union(heavy);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(armors)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my armor");

            var armor = new Armor { Name = "my armor" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_Medium()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Medium });

            var light = ArmorConstants.GetAllLightArmors(false);
            var medium = ArmorConstants.GetAllMediumArmors(false);
            var heavy = ArmorConstants.GetAllHeavyArmors(false);
            var armors = light.Union(medium);
            var non = heavy;

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(armors)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my armor");

            var armor = new Armor { Name = "my armor" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_Heavy()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Medium });
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Heavy });

            var light = ArmorConstants.GetAllLightArmors(false);
            var medium = ArmorConstants.GetAllMediumArmors(false);
            var heavy = ArmorConstants.GetAllHeavyArmors(false);
            var armors = light.Union(medium).Union(heavy);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(armors)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => !nn.Any())))
                .Returns("my armor");

            var armor = new Armor { Name = "my armor" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateShield()
        {
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var shields = ArmorConstants.GetAllShields(false);
            var non = new[] { ArmorConstants.TowerShield };
            var proficientShields = shields.Except(non);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(proficientShields)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my shield");

            var shield = new Armor { Name = "my shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Shield, Is.EqualTo(shield));
        }

        [Test]
        public void GenerateShield_Tower()
        {
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var shields = ArmorConstants.GetAllShields(false);
            var non = new[] { ArmorConstants.TowerShield };
            var proficientShields = shields.Except(non);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(shields)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => !nn.Any())))
                .Returns("my shield");

            var shield = new Armor { Name = "my shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Shield, Is.EqualTo(shield));
        }

        [Test]
        public void DoNotGenerateShield_IfNoFreeHands_TwoHandedWeapon()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

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
            weapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.TwoHanded };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Quarterstaff))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);

            Assert.That(equipment.Weapons.Count(), Is.EqualTo(1));
            Assert.That(equipment.Weapons, Contains.Item(weapon));
            Assert.That(equipment.Shield, Is.Null);
            mockItemGenerator.Verify(g => g.GenerateAtLevel(It.IsAny<int>(), ItemTypeConstants.Armor, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void DoNotGenerateShield_IfNoFreeHands_TwoWeaponAttacks()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var twoHanded = WeaponConstants.GetAllTwoHandedMelee(false, false);
            var simpleMelee = simple.Intersect(melee).Except(twoHanded);
            var non = melee.Except(simple).Except(twoHanded);

            mockCollectionSelector
                .SetupSequence(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger)
                .Returns(WeaponConstants.Club);

            var dagger = new Weapon();
            dagger.Name = WeaponConstants.Dagger;
            dagger.Damage = "my dagger damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(dagger);

            var club = new Weapon();
            club.Name = WeaponConstants.Club;
            club.Damage = "my club damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club))
                .Returns(club);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);

            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));
            Assert.That(equipment.Weapons, Contains.Item(dagger)
                .And.Contains(club));
            Assert.That(equipment.Shield, Is.Null);
            mockItemGenerator.Verify(g => g.GenerateAtLevel(It.IsAny<int>(), ItemTypeConstants.Armor, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GenerateShield_IfFreeHands_MeleeAndRangedWeaponAttacks()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Dexterity] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var ranged = WeaponConstants.GetAllRanged(false, false);
            var simpleMelee = simple.Intersect(melee);
            var simpleRanged = simple.Intersect(ranged);
            var nonMelee = melee.Except(simple);
            var nonRanged = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(nonMelee))))
                .Returns(WeaponConstants.Dagger);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(nonRanged))))
                .Returns(WeaponConstants.LightCrossbow);

            var dagger = new Weapon();
            dagger.Name = WeaponConstants.Dagger;
            dagger.Damage = "my dagger damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(dagger);

            var crossbow = new Weapon();
            crossbow.Name = WeaponConstants.LightCrossbow;
            crossbow.Damage = "my crossbow damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightCrossbow))
                .Returns(crossbow);

            var shields = ArmorConstants.GetAllShields(false);
            var non = new[] { ArmorConstants.TowerShield };
            var proficientShields = shields.Except(non);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(shields)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => !nn.Any())))
                .Returns("my shield");

            var shield = new Armor { Name = "my shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);

            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));
            Assert.That(equipment.Weapons, Contains.Item(dagger)
                .And.Contains(crossbow));
            Assert.That(equipment.Shield, Is.EqualTo(shield));
        }

        [Test]
        public void DoNotGenerateShield_IfNoFreeHands_MultiweaponAttacks()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var twoHanded = WeaponConstants.GetAllTwoHandedMelee(false, false);
            var simpleOneHandedMelee = simple.Intersect(melee).Except(twoHanded);
            var non = melee.Except(simple).Except(twoHanded);

            mockCollectionSelector
                .SetupSequence(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleOneHandedMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dagger)
                .Returns(WeaponConstants.Club)
                .Returns(WeaponConstants.LightPick);

            var dagger = new Weapon();
            dagger.Name = WeaponConstants.Dagger;
            dagger.Damage = "my dagger damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger))
                .Returns(dagger);

            var club = new Weapon();
            club.Name = WeaponConstants.Club;
            club.Damage = "my club damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club))
                .Returns(club);

            var pick = new Weapon();
            pick.Name = WeaponConstants.LightPick;
            pick.Damage = "my pick damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightPick))
                .Returns(pick);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);

            Assert.That(equipment.Weapons.Count(), Is.EqualTo(3));
            Assert.That(equipment.Weapons, Contains.Item(dagger)
                .And.Contains(club)
                .And.Contains(pick));
            Assert.That(equipment.Shield, Is.Null);
            mockItemGenerator.Verify(g => g.GenerateAtLevel(It.IsAny<int>(), ItemTypeConstants.Armor, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GenerateArmor_Predetermined_Mundane()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my armor template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Armor,
                Name = "my armor template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my armor template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Armor))
                .Returns(mockMundaneItemGenerator.Object);

            var armor = new Armor { Name = "my armor" };
            mockMundaneItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_Predetermined_Magical()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my armor template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Armor,
                Name = "my armor template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my armor template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Armor))
                .Returns(mockMagicalItemGenerator.Object);

            var armor = new Armor { Name = "my armor" };
            mockMagicalItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        //Example is Nessian Warhound with its barding
        [Test]
        public void GenerateArmor_Predetermined_EvenIfCannotUseEquipment()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my armor template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Armor,
                Name = "my armor template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my armor template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Armor))
                .Returns(mockMagicalItemGenerator.Object);

            var armor = new Armor { Name = "my armor" };
            mockMagicalItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", false, feats, 9266, attacks, abilities);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateItem_Predetermined_Mundane()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my item template" });

            var template = new Item
            {
                ItemType = "my item type",
                Name = "my item template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my item template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>("my item type"))
                .Returns(mockMundaneItemGenerator.Object);

            var item = new Item { Name = "my item" };
            mockMundaneItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));
        }

        [Test]
        public void GenerateItem_Predetermined_Magical()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my item template" });

            var template = new Item
            {
                ItemType = "my item type",
                Name = "my item template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my item template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>("my item type"))
                .Returns(mockMagicalItemGenerator.Object);

            var item = new Item { Name = "my item" };
            mockMagicalItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));
        }

        [Test]
        public void GenerateMultipleItems()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateMultipleItems_Predetermined()
        {
            //armor
            //shield (3 hands)
            //2 melee weapons, 2 ranged weapon
            //item
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateAllEquipment()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var ranged = WeaponConstants.GetAllRanged(false, false);
            var ammunition = WeaponConstants.GetAllAmmunition(false, false);
            var simpleMelee = simple.Intersect(melee);
            var simpleRanged = simple.Intersect(ranged).Except(ammunition);
            var nonMelee = melee.Except(simple);
            var nonRanged = ranged.Except(simple).Except(ammunition);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(nonMelee))))
                .Returns("simple melee weapon");

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(nonRanged))))
                .Returns("simple ranged weapon");

            var meleeWeapon = new Weapon();
            meleeWeapon.Name = "my simple melee weapon";
            meleeWeapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple melee weapon"))
                .Returns(meleeWeapon);

            var rangedWeapon = new Weapon();
            rangedWeapon.Name = "my simple ranged weapon";
            rangedWeapon.Damage = "my damage";
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple ranged weapon"))
                .Returns(rangedWeapon);

            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            var light = ArmorConstants.GetAllLightArmors(false);
            var allArmors = ArmorConstants.GetAllArmors(false);
            var non = allArmors.Except(light);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(light)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my armor");

            var armor = new Armor { Name = "my armor" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor"))
                .Returns(armor);

            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });

            var shields = ArmorConstants.GetAllShields(false);
            var tower = new[] { ArmorConstants.TowerShield };
            var proficientShields = shields.Except(tower);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(proficientShields)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(tower))))
                .Returns("my shield");

            var shield = new Armor { Name = "my shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield"))
                .Returns(shield);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my item template" });

            var template = new Item
            {
                ItemType = "my item type",
                Name = "my item template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my item template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>("my item type"))
                .Returns(mockMagicalItemGenerator.Object);

            var item = new Item { Name = "my item" };
            mockMagicalItemGenerator
                .Setup(g => g.GenerateFrom(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities);
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));
            Assert.That(equipment.Weapons, Contains.Item(meleeWeapon)
                .And.Contains(rangedWeapon));

            Assert.That(equipment.Armor, Is.EqualTo(armor));
            Assert.That(equipment.Shield, Is.EqualTo(shield));
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo("my simple melee weapon"));
            Assert.That(attacks[5].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].Name, Is.EqualTo("my simple ranged weapon"));
            Assert.That(attacks[6].DamageRoll, Is.EqualTo("my damage"));
            Assert.That(attacks[6].IsMelee, Is.False);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);
            Assert.That(attacks[6].AttackBonuses, Is.Empty);
        }

        [Test]
        public void AddAttacks_NoUnnaturalAttacks()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = true,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 2,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(1));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.True);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(2));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalAttack_FrequencyOf1(bool melee)
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(1));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalAttacks_FrequencyOf1(bool melee)
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = "my other attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalAttack_FrequencyGreaterThan1(bool melee)
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 2,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(2));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalAttacks_FrequencyGreaterThan1(bool melee)
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 2,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = "my other attack",
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 3,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(5));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[2].IsPrimary, Is.True);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[3].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[3].IsNatural, Is.False);
            Assert.That(updatedAttacks[3].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[3].IsPrimary, Is.True);
            Assert.That(updatedAttacks[3].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[3].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[4].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[4].IsNatural, Is.False);
            Assert.That(updatedAttacks[4].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[4].IsPrimary, Is.True);
            Assert.That(updatedAttacks[4].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[4].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [Test]
        public void AddAttacks_MixedUnnaturalAttacks_FrequencyGreaterThan1()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = "my attack",
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 2,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = "my other attack",
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 3,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(5));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo("my attack"));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.True);
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.False);
            Assert.That(updatedAttacks[2].IsPrimary, Is.True);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[3].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[3].IsNatural, Is.False);
            Assert.That(updatedAttacks[3].IsMelee, Is.False);
            Assert.That(updatedAttacks[3].IsPrimary, Is.True);
            Assert.That(updatedAttacks[3].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[3].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[4].Name, Is.EqualTo("my other attack"));
            Assert.That(updatedAttacks[4].IsNatural, Is.False);
            Assert.That(updatedAttacks[4].IsMelee, Is.False);
            Assert.That(updatedAttacks[4].IsPrimary, Is.True);
            Assert.That(updatedAttacks[4].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[4].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [TestCase(true, FeatConstants.TwoWeaponFighting, 2)]
        [TestCase(true, FeatConstants.Monster.MultiweaponFighting, 3)]
        [TestCase(false, FeatConstants.TwoWeaponFighting, 2)]
        [TestCase(false, FeatConstants.Monster.MultiweaponFighting, 3)]
        public void AddAttacks_UnnaturalEquippedAttack_FillHands(bool melee, string feat, int numberOfHands)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = feat });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, numberOfHands).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(numberOfHands));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));

            for (var i = 1; i < numberOfHands; i++)
            {
                Assert.That(updatedAttacks[i].Name, Is.EqualTo(name));
                Assert.That(updatedAttacks[i].IsNatural, Is.False);
                Assert.That(updatedAttacks[i].IsMelee, Is.EqualTo(melee));
                Assert.That(updatedAttacks[i].IsPrimary, Is.False);
                Assert.That(updatedAttacks[i].Frequency.Quantity, Is.EqualTo(1));
                Assert.That(updatedAttacks[i].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalEquippedAttacks_Untrained(bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });
            mockPercentileSelector
                .Setup(s => s.SelectFrom(.01))
                .Returns(true);

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-6));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.EqualTo(-6));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-10));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.EqualTo(-10));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddAttacks_UnnaturalEquippedAttacks_Preset(bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-6));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.EqualTo(-6));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-10));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.EqualTo(-10));
        }

        [TestCase(FeatConstants.TwoWeaponFighting, true)]
        [TestCase(FeatConstants.TwoWeaponFighting, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting, true)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting, false)]
        public void AddAttacks_UnnaturalEquippedAttacks_TwoWeapon(string feat, bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });
            feats.Add(new Feat { Name = feat });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-6)
                .And.Contains(2));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.EqualTo(-4));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-10)
                .And.Contains(6));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.EqualTo(-4));
        }

        [TestCase(FeatConstants.TwoWeaponFighting_Improved, true)]
        [TestCase(FeatConstants.TwoWeaponFighting_Improved, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Improved, true)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Improved, false)]
        public void AddAttacks_UnnaturalEquippedAttacks_TwoWeapon_Improved(string feat, bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });
            feats.Add(new Feat { Name = feat });
            feats.Add(new Feat { Name = FeatConstants.TwoWeaponFighting });
            feats.Add(new Feat { Name = FeatConstants.Monster.MultiweaponFighting });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-6)
                .And.Contains(2));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.EqualTo(-4));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(2));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-10)
                .And.Contains(6));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.EqualTo(-4));
        }

        [TestCase(FeatConstants.TwoWeaponFighting_Greater, true)]
        [TestCase(FeatConstants.TwoWeaponFighting_Greater, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Greater, true)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Greater, false)]
        public void AddAttacks_UnnaturalEquippedAttacks_TwoWeapon_Greater(string feat, bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });
            feats.Add(new Feat { Name = feat });
            feats.Add(new Feat { Name = FeatConstants.TwoWeaponFighting });
            feats.Add(new Feat { Name = FeatConstants.Monster.MultiweaponFighting });
            feats.Add(new Feat { Name = FeatConstants.TwoWeaponFighting_Improved });
            feats.Add(new Feat { Name = FeatConstants.Monster.MultiweaponFighting_Improved });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-6)
                .And.Contains(2));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.EqualTo(-4));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(3));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-10)
                .And.Contains(6));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.EqualTo(-4));
        }

        [TestCase(FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, true)]
        [TestCase(FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, false)]
        [TestCase(FeatConstants.SpecialQualities.MultiweaponFighting_Superior, true)]
        [TestCase(FeatConstants.SpecialQualities.MultiweaponFighting_Superior, false)]
        public void AddAttacks_UnnaturalEquippedAttacks_TwoWeapon_Superior(string feat, bool melee)
        {
            var name = melee ? AttributeConstants.Melee : AttributeConstants.Ranged;

            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = name,
                IsNatural = false,
                IsMelee = melee,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = "my feat" });
            feats.Add(new Feat { Name = feat });
            feats.Add(new Feat { Name = FeatConstants.TwoWeaponFighting });
            feats.Add(new Feat { Name = FeatConstants.Monster.MultiweaponFighting });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].MaxNumberOfAttacks, Is.EqualTo(4));
            Assert.That(updatedAttacks[0].AttackBonuses, Has.Count.EqualTo(3)
                .And.Contains(-6)
                .And.Contains(2)
                .And.Contains(4));
            Assert.That(updatedAttacks[0].TotalAttackBonus, Is.Zero);
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(name));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.EqualTo(melee));
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].MaxNumberOfAttacks, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].AttackBonuses, Has.Count.EqualTo(3)
                .And.Contains(-10)
                .And.Contains(6)
                .And.Contains(4));
            Assert.That(updatedAttacks[1].TotalAttackBonus, Is.Zero);
        }
    }

    public static class LinqExtensions
    {
        public static bool IsEquivalentTo<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            if (source == null || target == null)
            {
                return source == target;
            }

            return source.Count() == target.Count()
                && !source.Except(target).Any();
        }
    }
}
