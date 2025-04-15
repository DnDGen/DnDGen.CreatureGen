using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Selectors;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(new[]
                {
                    FeatConstants.WeaponProficiency_Exotic,
                    FeatConstants.WeaponProficiency_Martial,
                    FeatConstants.WeaponProficiency_Simple
                });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
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
            var equipment = equipmentGenerator.Generate("creature", false, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment, Is.Not.Null);
            Assert.That(equipment.Armor, Is.Null);
            Assert.That(equipment.Shield, Is.Null);
            Assert.That(equipment.Weapons, Is.Empty);
            Assert.That(equipment.Items, Is.Empty);
        }

        [Test]
        public void GenerateNoWeapons_WhenNoUnnaturalAttacks()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = true, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = true, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoWeapons_WhenNoWeaponProficiencies()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoWeapons_WhenLevelZero()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 0, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoMeleeWeapons_WhenNoEquipmentMeleeAttacks()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Empty);
        }

        [Test]
        public void GenerateNoRangedWeapons_WhenNoEquipmentRangedAttacks()
        {
            attacks.Add(new Attack { Name = "my attack", IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Dagger;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.LightMace;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightMace, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.LightMace));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Dagger;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Greataxe;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Greataxe, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Greataxe));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Greatsword;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Greatsword, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Greatsword));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Whip;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Whip, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Whip));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.SpikedChain;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.SpikedChain, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.SpikedChain));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Longsword;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "martial weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Longsword));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.GnomeHookedHammer;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "exotic weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.GnomeHookedHammer));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.GnomeHookedHammer, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my gnome hooked hammer"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-4));
        }

        //Example here being the Sun Blade, a short sword and a bastard sword
        [Test]
        public void GenerateMeleeWeapon_NotProficient_ButProficientWithSome()
        {
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true
            });
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
                .Returns(WeaponConstants.ShortSword);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.SunBlade;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.BaseNames = new[] { WeaponConstants.ShortSword, WeaponConstants.BastardSword };
            weapon.Attributes = new[] { "my attribute", AttributeConstants.Melee };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.ShortSword, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.SunBlade));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        //Example here being the Sun Blade, a short sword and a bastard sword
        [Test]
        public void GenerateMeleeWeapon_NotProficient_ButProficientWithSome_Predetermined()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.SunBlade,
                BaseNames = new[] { WeaponConstants.ShortSword, WeaponConstants.BastardSword },
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                    .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.SunBlade));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void BUG_GenerateMeleeWeapon_WithOversizedWeapon()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "oversized"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_WithMultipleDamages()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Damages.Add(new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "oversized"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type + my other roll my other damage type (my other condition)"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyBonusFromFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.Club } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.Club })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(90210));
        }

        [Test]
        public void GenerateMeleeWeapon_ApplyBonusFromFeat_BaseNameMatch()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.ShortSword } });

            var martial = WeaponConstants.GetAllMartial(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var martialMelee = martial.Intersect(melee);
            var non = melee.Except(martial);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.ShortSword })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(martialMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.SunBlade;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.BaseNames = new[] { WeaponConstants.BastardSword, WeaponConstants.ShortSword };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.SunBlade));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.Club } });
            feats.Add(new Feat { Name = "bonus feat", Power = 42, Foci = new[] { WeaponConstants.Club, "my other weapon" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var simpleMelee = simple.Intersect(melee);
            var non = melee.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.Club })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleMelee)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(90210)
                .And.Contains(42));
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Mundane_WithSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                Traits = new HashSet<string> { SizeConstants.Medium }
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Mundane_WithoutSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("size"));
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Mundane_WithoutSize_Oversized()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("oversized"));
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Magic_WithSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
                Traits = new HashSet<string> { SizeConstants.Medium }
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Magic_WithoutSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("size"));
        }

        [Test]
        public void GenerateMeleeWeapon_Predetermined_Magic_WithoutSize_Oversized()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Club,
                Attributes = new[] { AttributeConstants.Melee }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("oversized"));
        }

        [Test]
        public void GenerateMeleeWeapon_OfCreatureSize()
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Size = "size";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));
            Assert.That(weapon.Size, Is.EqualTo("size"));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Magic.Bonus = 1337;
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(weapon.Summary));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Traits.Add(TraitConstants.Masterwork);
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(1));
        }

        [Test]
        public void GenerateMeleeWeapon_MasterworkBonus_DoesNotStackWithMagicBonus()
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
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Traits.Add(TraitConstants.Masterwork);
            weapon.Magic.Bonus = 2;

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(weapon.Summary));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(2));
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
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dagger));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
                .Returns(WeaponConstants.Club);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Club;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
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
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Quarterstaff, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Quarterstaff));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [TestCase(WeaponConstants.Club, WeaponConstants.Club, 0, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, 2, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, 0, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2)]
        public void GenerateTwoMeleeWeapons(string primary, string secondary, int bonus1, int bonus2)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

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
                .Returns(primary)
                .Returns(secondary);

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, It.IsAny<string>(), "size"))
                .Returns((int l, string t, string n, string[] tt) => new Weapon
                {
                    Name = n,
                    Damages = new List<Damage>
                    {
                       new Damage { Roll = $"my {n} roll", Type = $"my {n} damage type" }
                    }
                });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));

            var weapons = equipment.Weapons.ToArray();
            Assert.That(weapons[0].Name, Is.EqualTo(primary));
            Assert.That(weapons[0].DamageDescription, Is.EqualTo($"my {primary} roll my {primary} damage type"));
            Assert.That(weapons[1].Name, Is.EqualTo(secondary));
            Assert.That(weapons[1].DamageDescription, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo(primary));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo($"my {primary} roll my {primary} damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);

            if (bonus1 > 0)
                Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus1));
            else
                Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[6].Name, Is.EqualTo(secondary));
            Assert.That(attacks[6].DamageSummary, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);

            if (bonus2 > 0)
                Assert.That(attacks[6].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus2));
            else
                Assert.That(attacks[6].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Dagger, 2, 0, 2)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Dagger, 2, 0, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2)]
        public void GenerateMultipleMeleeWeapons(string primary, string secondary, string third, int bonus1, int bonus2, int bonus3)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

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
                .Returns(primary)
                .Returns(secondary)
                .Returns(third);

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, It.IsAny<string>(), "size"))
                .Returns((int l, string t, string n, string[] tt) => new Weapon
                {
                    Name = n,
                    Damages = new List<Damage>
                    {
                       new Damage { Roll = $"my {n} roll", Type = $"my {n} damage type" }
                    }
                });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(3));

            var weapons = equipment.Weapons.ToArray();
            Assert.That(weapons[0].Name, Is.EqualTo(primary));
            Assert.That(weapons[0].DamageDescription, Is.EqualTo($"my {primary} roll my {primary} damage type"));
            Assert.That(weapons[1].Name, Is.EqualTo(secondary));
            Assert.That(weapons[1].DamageDescription, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));
            Assert.That(weapons[2].Name, Is.EqualTo(third));
            Assert.That(weapons[2].DamageDescription, Is.EqualTo($"my {third} roll my {third} damage type"));

            Assert.That(attacks, Has.Count.EqualTo(8));
            Assert.That(attacks[5].Name, Is.EqualTo(primary));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo($"my {primary} roll my {primary} damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);

            if (bonus1 > 0)
                Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus1));
            else
                Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[6].Name, Is.EqualTo(secondary));
            Assert.That(attacks[6].DamageSummary, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);

            if (bonus2 > 0)
                Assert.That(attacks[6].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus2));
            else
                Assert.That(attacks[6].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[7].Name, Is.EqualTo(third));
            Assert.That(attacks[7].DamageSummary, Is.EqualTo($"my {third} roll my {third} damage type"));
            Assert.That(attacks[7].IsMelee, Is.True);
            Assert.That(attacks[7].IsNatural, Is.False);
            Assert.That(attacks[7].IsSpecial, Is.False);

            if (bonus3 > 0)
                Assert.That(attacks[7].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus3));
            else
                Assert.That(attacks[7].AttackBonuses, Is.Empty);

            Assert.That(attacks[7].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Dagger, 2, 2, 0, 2)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 2, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2, 2)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Dagger, 2, 2, 0, 2)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 2, 0)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Club, WeaponConstants.Dagger, 2, 2, 0, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 2, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Club, 0, 0, 0, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Club, WeaponConstants.Dagger, 2, 2, 0, 2)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Club, 2, 2, 2, 0)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Dagger, WeaponConstants.Dagger, 2, 2, 2, 2)]
        public void GenerateMultipleMeleeWeapons_MultiplePrimary(string primary1, string primary2, string secondary, string third, int bonus1, int bonus2, int bonus3, int bonus4)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true, IsPrimary = false, BaseAbility = abilities[AbilityConstants.Strength] });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

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
                .Returns(primary1)
                .Returns(primary2)
                .Returns(secondary)
                .Returns(third);

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, It.IsAny<string>(), "size"))
                .Returns((int l, string t, string n, string[] tt) => new Weapon
                {
                    Name = n,
                    Damages = new List<Damage>
                    {
                       new Damage { Roll = $"my {n} roll", Type = $"my {n} damage type" }
                    }
                });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(4));

            var weapons = equipment.Weapons.ToArray();
            Assert.That(weapons[0].Name, Is.EqualTo(primary1));
            Assert.That(weapons[0].DamageDescription, Is.EqualTo($"my {primary1} roll my {primary1} damage type"));
            Assert.That(weapons[1].Name, Is.EqualTo(primary2));
            Assert.That(weapons[1].DamageDescription, Is.EqualTo($"my {primary2} roll my {primary2} damage type"));
            Assert.That(weapons[2].Name, Is.EqualTo(secondary));
            Assert.That(weapons[2].DamageDescription, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));
            Assert.That(weapons[3].Name, Is.EqualTo(third));
            Assert.That(weapons[3].DamageDescription, Is.EqualTo($"my {third} roll my {third} damage type"));

            Assert.That(attacks, Has.Count.EqualTo(9));
            Assert.That(attacks[5].Name, Is.EqualTo(primary1));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo($"my {primary1} roll my {primary1} damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);

            if (bonus1 > 0)
                Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus1));
            else
                Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[5].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[6].Name, Is.EqualTo(primary2));
            Assert.That(attacks[6].DamageSummary, Is.EqualTo($"my {primary2} roll my {primary2} damage type"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);

            if (bonus2 > 0)
                Assert.That(attacks[6].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus2));
            else
                Assert.That(attacks[6].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[7].Name, Is.EqualTo(secondary));
            Assert.That(attacks[7].DamageSummary, Is.EqualTo($"my {secondary} roll my {secondary} damage type"));
            Assert.That(attacks[7].IsMelee, Is.True);
            Assert.That(attacks[7].IsNatural, Is.False);
            Assert.That(attacks[7].IsSpecial, Is.False);

            if (bonus3 > 0)
                Assert.That(attacks[7].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus3));
            else
                Assert.That(attacks[7].AttackBonuses, Is.Empty);

            Assert.That(attacks[7].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));

            Assert.That(attacks[8].Name, Is.EqualTo(third));
            Assert.That(attacks[8].DamageSummary, Is.EqualTo($"my {third} roll my {third} damage type"));
            Assert.That(attacks[8].IsMelee, Is.True);
            Assert.That(attacks[8].IsNatural, Is.False);
            Assert.That(attacks[8].IsSpecial, Is.False);

            if (bonus4 > 0)
                Assert.That(attacks[8].AttackBonuses, Has.Count.EqualTo(1).And.Contains(bonus4));
            else
                Assert.That(attacks[8].AttackBonuses, Is.Empty);

            Assert.That(attacks[8].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateRangedWeapon()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_WithMultipleDamages()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Damages.Add(new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type + my other roll my other damage type (my other condition)"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void BUG_GenerateRangedWeapon_Oversized()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "oversized"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_NonProficiencyFocus()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Dart };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci)),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dart);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dart, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_MultipleNonProficiencyFoci()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Dart, WeaponConstants.Javelin };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci)),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Javelin);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Javelin;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Javelin, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Javelin));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_MultipleNonProficiencyFoci_MultipleFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            var foci = new[] { WeaponConstants.Dart, WeaponConstants.Javelin };
            feats.Add(new Feat { Name = "weapon feat", Foci = foci });
            var otherFoci = new[] { WeaponConstants.Dart, WeaponConstants.LightCrossbow };
            feats.Add(new Feat { Name = "other weapon feat", Foci = otherFoci });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(foci).Except(otherFoci).Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(foci.Union(otherFoci))),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.LightCrossbow);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.LightCrossbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightCrossbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.LightCrossbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Dart };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dart);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dart, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Javelin, WeaponConstants.Dart };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Dart);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dart, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Longbow };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Longbow);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Longbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Longbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Longbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [TestCase(WeaponConstants.CompositeShortbow, WeaponConstants.CompositeShortbow_StrengthPlus0)]
        [TestCase(WeaponConstants.CompositeShortbow, WeaponConstants.CompositeShortbow_StrengthPlus1)]
        [TestCase(WeaponConstants.CompositeShortbow, WeaponConstants.CompositeShortbow_StrengthPlus2)]
        [TestCase(WeaponConstants.CompositeLongbow, WeaponConstants.CompositeLongbow_StrengthPlus0)]
        [TestCase(WeaponConstants.CompositeLongbow, WeaponConstants.CompositeLongbow_StrengthPlus1)]
        [TestCase(WeaponConstants.CompositeLongbow, WeaponConstants.CompositeLongbow_StrengthPlus2)]
        [TestCase(WeaponConstants.CompositeLongbow, WeaponConstants.CompositeLongbow_StrengthPlus3)]
        [TestCase(WeaponConstants.CompositeLongbow, WeaponConstants.CompositeLongbow_StrengthPlus4)]
        public void GenerateRangedWeapon_Proficient_Focus_Martial_CompositeBow(string compositeBow, string compositeTemplate)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { compositeBow };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = foci });

            var templates = GetBowTemplates(compositeBow);
            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(templates);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(templates)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(compositeTemplate);

            var weapon = new Weapon();
            weapon.Name = compositeTemplate;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, compositeTemplate, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(compositeTemplate));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Shortbow, WeaponConstants.Longbow };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Longbow);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Longbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Longbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Longbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_Focus_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Shuriken };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.Shuriken);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Shuriken;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Shuriken, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Shuriken));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_MultipleFoci_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            var foci = new[] { WeaponConstants.Shuriken, WeaponConstants.HandCrossbow };
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = foci });

            var ranged = GetRangedWithBowTemplates();
            var non = ranged.Except(foci);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(foci)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.HandCrossbow);

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.HandCrossbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.HandCrossbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.HandCrossbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Simple()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Martial()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });

            var bowTemplates = GetBowTemplates();
            var martial = WeaponConstants.GetAllMartial(false, false).Union(bowTemplates);
            var ranged = GetRangedWithBowTemplates();
            var martialRanged = martial.Intersect(ranged);
            var non = ranged.Except(martial);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(martialRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("martial weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Longbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "martial weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Longbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Proficient_All_Exotic()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { GroupConstants.All } });

            var exotic = WeaponConstants.GetAllExotic(false, false);
            var ranged = GetRangedWithBowTemplates();
            var exoticRanged = exotic.Intersect(ranged);
            var non = ranged.Except(exotic);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(exoticRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("exotic weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Shuriken;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "exotic weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Shuriken));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_NotProficient()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(WeaponConstants.HandCrossbow);

            var weapon = new Weapon();
            weapon.Name = "my hand crossbow";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.HandCrossbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my hand crossbow"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(-4));
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusFromFeat()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.Dart } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.Dart })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(90210));
        }

        private List<string> GetRangedWithBowTemplates()
        {
            var ranged = WeaponConstants.GetAllRanged(false, true, false).ToList();
            var ammo = WeaponConstants.GetAllAmmunition(false, false).Except(new[] { WeaponConstants.Shuriken });

            ranged.Remove(WeaponConstants.CompositeShortbow);
            ranged.Remove(WeaponConstants.CompositeLongbow);
            ranged = ranged.Except(ammo).ToList();

            return ranged;
        }

        private IEnumerable<string> GetBowTemplates(string compositeBow = null)
        {
            var shortBows = new[]
            {
                WeaponConstants.CompositeShortbow_StrengthPlus0,
                WeaponConstants.CompositeShortbow_StrengthPlus1,
                WeaponConstants.CompositeShortbow_StrengthPlus2,
            };

            var longBows = new[]
            {
                WeaponConstants.CompositeLongbow_StrengthPlus0,
                WeaponConstants.CompositeLongbow_StrengthPlus1,
                WeaponConstants.CompositeLongbow_StrengthPlus2,
                WeaponConstants.CompositeLongbow_StrengthPlus3,
                WeaponConstants.CompositeLongbow_StrengthPlus4
            };

            if (compositeBow == WeaponConstants.CompositeLongbow)
            {
                return longBows;
            }
            else if (compositeBow == WeaponConstants.CompositeShortbow)
            {
                return shortBows;
            }

            return shortBows.Union(longBows);
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusFromFeat_BaseNameMatch()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.Longbow } });

            var bowTemplates = GetBowTemplates();
            var martial = WeaponConstants.GetAllMartial(false, false).Union(bowTemplates);
            var ranged = GetRangedWithBowTemplates();
            var martialRanged = martial.Intersect(ranged);
            var non = ranged.Except(martialRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.Longbow })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(martialRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("martial weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Oathbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.BaseNames = new[] { WeaponConstants.Longbow };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "martial weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Oathbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(90210));
        }

        [Test]
        public void GenerateRangedWeapon_ApplyBonusesFromMultipleFeats()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = "wrong feat", Power = 666, Foci = new[] { "wrong weapon" } });
            feats.Add(new Feat { Name = "bonus feat", Power = 90210, Foci = new[] { WeaponConstants.Javelin } });
            feats.Add(new Feat { Name = "bonus feat", Power = 42, Foci = new[] { WeaponConstants.Javelin, "my other weapon" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { WeaponConstants.Javelin })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Javelin;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Javelin));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(90210)
                .And.Contains(42));
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_Mundane_WithSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                Traits = new HashSet<string> { SizeConstants.Medium }
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Dart,
                Attributes = new[] { AttributeConstants.Ranged }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_Mundane_WithoutSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Dart,
                Attributes = new[] { AttributeConstants.Ranged }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("size"));
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_Mundane_WithoutSize_Oversized()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.OversizedWeapon, Foci = new[] { "oversized" } });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Dart,
                Attributes = new[] { AttributeConstants.Ranged }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("oversized"));
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_WithAmmunition()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = "my projectile weapon",
                Attributes = new[] { AttributeConstants.Ranged },
                Ammunition = "my ammo",
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            var ammo = new Weapon
            {
                Name = "my ammo",
                Attributes = new[] { AttributeConstants.Ranged },
            };
            ammo.Damages.Add(new Damage { Roll = "my ammo roll", Type = "my ammo damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "my ammo", "size"))
                .Returns(ammo);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(2));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(weapon));
            Assert.That(equipment.Weapons.Last(), Is.EqualTo(ammo));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my projectile weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_WithPredeterminedAmmunition()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template", "my ammo template" });

            var weaponTemplate = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(weaponTemplate);

            var ammoTemplate = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my ammo template"
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my ammo template"))
                .Returns(ammoTemplate);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMundaneItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = "my projectile weapon",
                Attributes = new[] { AttributeConstants.Ranged },
                Ammunition = "my ammo"
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(weaponTemplate, false))
                .Returns(weapon);

            var ammo = new Weapon
            {
                Name = "my ammo",
                Attributes = new[] { AttributeConstants.Ranged }
            };
            ammo.Damages.Add(new Damage { Roll = "my ammo roll", Type = "my ammo damage type" });

            mockMundaneItemGenerator
                .Setup(g => g.Generate(ammoTemplate, false))
                .Returns(ammo);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(2));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(weapon));
            Assert.That(equipment.Weapons.Last(), Is.EqualTo(ammo));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my projectile weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_Magical_WithSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
                Traits = new HashSet<string> { SizeConstants.Medium }
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Dart,
                Attributes = new[] { AttributeConstants.Ranged }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void GenerateRangedWeapon_Predetermined_Magical_WithoutSize()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my weapon template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Weapon,
                Name = "my weapon template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my weapon template"))
                .Returns(template);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon))
                .Returns(mockMagicalItemGenerator.Object);

            var weapon = new Weapon
            {
                Name = WeaponConstants.Dart,
                Attributes = new[] { AttributeConstants.Ranged }
            };
            weapon.Damages.Add(new Damage { Roll = "my predetermined roll", Type = "my predetermined damage type" });

            mockMagicalItemGenerator
                .Setup(g => g.Generate(template, false))
                .Returns(weapon);

            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my predetermined roll my predetermined damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            mockCollectionSelector.Verify(
                s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);

            mockItemGenerator.Verify(
                g => g.GenerateAtLevel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            Assert.That(template.Traits, Contains.Item("size"));
        }

        [Test]
        public void GenerateRangedWeapon_OfCreatureSize()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simple);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Dart;
            weapon.Damages.Add(new Damage { Roll = "my sized roll", Type = "my damage type" });
            weapon.Size = "size";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));
            Assert.That(weapon.Size, Is.EqualTo("size"));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my sized roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_WithAmmunition()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Martial, Foci = new[] { GroupConstants.All } });

            var bowTemplates = GetBowTemplates();
            var martial = WeaponConstants.GetAllMartial(false, false).Union(bowTemplates);
            var ranged = GetRangedWithBowTemplates();
            var martialRanged = martial.Intersect(ranged);
            var non = ranged.Except(martial);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(martialRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("martial weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Longbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Ammunition = "my ammo";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "martial weapon", "size"))
                .Returns(weapon);

            var ammo = new Weapon();
            ammo.Name = "my ammo";
            ammo.Damages.Add(new Damage { Roll = "my ammo roll", Type = "my ammo damage type" });
            ammo.Quantity = 42;

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "my ammo", "size"))
                .Returns(ammo);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(2));
            Assert.That(equipment.Weapons.First(), Is.EqualTo(weapon));
            Assert.That(equipment.Weapons.Last(), Is.EqualTo(ammo));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Longbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_MagicBonus()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Javelin;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Magic.Bonus = 1337;
            weapon.Traits = new HashSet<string> { TraitConstants.Masterwork };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(weapon.Summary));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(1337));
        }

        [Test]
        public void GenerateRangedWeapon_MasterworkBonus()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = WeaponConstants.Javelin;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Traits = new HashSet<string> { TraitConstants.Masterwork };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Javelin));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Has.Count.EqualTo(1).And.Contains(1));
        }

        [Test]
        public void GenerateMultipleRangedWeapons()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = true });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ammo = WeaponConstants.GetAllAmmunition(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleRanged = simple.Intersect(ranged);
            var non = ranged.Except(simpleRanged);

            mockCollectionSelector
                .SetupSequence(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(simpleRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon")
                .Returns("other simple weapon");

            var dart = new Weapon();
            dart.Name = WeaponConstants.Dart;
            dart.Damages.Add(new Damage { Roll = "my dart roll", Type = "my dart damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(dart);

            var javelin = new Weapon();
            javelin.Name = WeaponConstants.Javelin;
            javelin.Damages.Add(new Damage { Roll = "my javelin roll", Type = "my javelin damage type" });
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "other simple weapon", "size"))
                .Returns(javelin);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));
            Assert.That(equipment.Weapons, Contains.Item(dart)
                .And.Contains(javelin));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my dart roll my dart damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].Name, Is.EqualTo(WeaponConstants.Javelin));
            Assert.That(attacks[6].DamageSummary, Is.EqualTo("my javelin roll my javelin damage type"));
            Assert.That(attacks[6].IsMelee, Is.True);
            Assert.That(attacks[6].IsNatural, Is.False);
            Assert.That(attacks[6].IsSpecial, Is.False);
            Assert.That(attacks[6].AttackBonuses, Is.Empty);
        }

        [Test]
        public void GenerateRangedWeapon_Thrown()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my ranged weapon");

            var weapon = new Weapon();
            weapon.Name = "my ranged weapon";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Thrown };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "my ranged weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my ranged weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(4));
        }

        [Test]
        public void GenerateRangedWeapon_Projectile()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my ranged weapon");

            var weapon = new Weapon();
            weapon.Name = "my ranged weapon";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "my ranged weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my ranged weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(4));
        }

        [Test]
        public void GenerateRangedWeapon_NeitherThrownNorProjectile()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("my ranged weapon");

            var weapon = new Weapon();
            weapon.Name = "my ranged weapon";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { "not thrown", "not projectile" };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "my ranged weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my ranged weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(1));
        }

        [TestCase(WeaponConstants.HandCrossbow)]
        [TestCase(WeaponConstants.LightCrossbow)]
        [TestCase(WeaponConstants.HeavyCrossbow)]
        public void GenerateRangedWeapon_Crossbow(string crossbow)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns(crossbow);

            var weapon = new Weapon();
            weapon.Name = crossbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, crossbow, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(crossbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(1));
        }

        [TestCase(WeaponConstants.HandCrossbow)]
        [TestCase(WeaponConstants.LightCrossbow)]
        [TestCase(WeaponConstants.HeavyCrossbow)]
        public void GenerateRangedWeapon_Crossbow_BaseName(string crossbow)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my weapon";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };
            weapon.BaseNames = new[] { "my base name", crossbow };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(1));
        }

        [TestCase(WeaponConstants.HandCrossbow, 4)]
        [TestCase(WeaponConstants.LightCrossbow, 4)]
        [TestCase(WeaponConstants.HeavyCrossbow, 1)]
        public void GenerateRangedWeapon_Crossbow_WithRapidReload(string crossbow, int maxAttacks)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });
            feats.Add(new Feat { Name = FeatConstants.RapidReload, Foci = new[] { crossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { crossbow })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = crossbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(crossbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(maxAttacks));
        }

        [TestCase(WeaponConstants.HandCrossbow, 4)]
        [TestCase(WeaponConstants.LightCrossbow, 4)]
        [TestCase(WeaponConstants.HeavyCrossbow, 1)]
        public void GenerateRangedWeapon_Crossbow_BaseName_WithRapidReload(string crossbow, int maxAttacks)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });
            feats.Add(new Feat { Name = FeatConstants.RapidReload, Foci = new[] { crossbow } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { crossbow })),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = "my weapon";
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };
            weapon.BaseNames = new[] { "my base name", crossbow };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo("my weapon"));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(maxAttacks));
        }

        [TestCase(WeaponConstants.HandCrossbow)]
        [TestCase(WeaponConstants.LightCrossbow)]
        [TestCase(WeaponConstants.HeavyCrossbow)]
        public void GenerateRangedWeapon_Crossbow_WithRapidReload_WrongCrossbow(string crossbow)
        {
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false, MaxNumberOfAttacks = 4 });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Exotic, Foci = new[] { WeaponConstants.HandCrossbow } });
            feats.Add(new Feat { Name = FeatConstants.RapidReload, Foci = new[] { "wrong crossbow" } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var ranged = GetRangedWithBowTemplates();
            var proficientRanged = simple.Intersect(ranged).Union(new[] { WeaponConstants.HandCrossbow });
            var non = ranged.Except(proficientRanged);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => !cc.Any()),
                    It.Is<IEnumerable<string>>(uu => uu.IsEquivalentTo(proficientRanged)),
                    null,
                    It.Is<IEnumerable<string>>(nn => nn.IsEquivalentTo(non))))
                .Returns("simple weapon");

            var weapon = new Weapon();
            weapon.Name = crossbow;
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Projectile };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple weapon", "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons, Is.Not.Empty.And.Count.EqualTo(1));
            Assert.That(equipment.Weapons.Single(), Is.EqualTo(weapon));

            Assert.That(attacks, Has.Count.EqualTo(6));
            Assert.That(attacks[5].Name, Is.EqualTo(crossbow));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my roll my damage type"));
            Assert.That(attacks[5].IsMelee, Is.False);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);
            Assert.That(attacks[5].MaxNumberOfAttacks, Is.EqualTo(1));
        }

        [Test]
        public void GenerateNoArmor_IfNotProficient()
        {
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()))
                .Returns("my armor");

            var armor = new Armor { Name = "my armor" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.Null);
        }

        [Test]
        public void GenerateNoShield_IfNotProficient()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()))
                .Returns("my shield");

            var shield = new Armor { Name = "my shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield"))
                .Returns(shield);

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

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Shield, Is.Null);
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor", "size"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor", "size"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor", "size"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield", "size"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Shield, Is.EqualTo(shield));
        }

        [Test]
        public void GenerateShield_Tower()
        {
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency });
            feats.Add(new Feat { Name = FeatConstants.ShieldProficiency_Tower });

            var shields = ArmorConstants.GetAllShields(false);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(shields)),
                    null,
                    null,
                    It.Is<IEnumerable<string>>(nn => !nn.Any())))
                .Returns("my tower shield");

            var shield = new Armor { Name = "my tower shield" };
            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my tower shield", "size"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
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
            weapon.Damages.Add(new Damage { Roll = "my roll", Type = "my damage type" });
            weapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.TwoHanded };

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Quarterstaff, "size"))
                .Returns(weapon);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");

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
            dagger.Damages.Add(new Damage { Roll = "my dagger roll", Type = "my dagger damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(dagger);

            var club = new Weapon();
            club.Name = WeaponConstants.Club;
            club.Damages.Add(new Damage { Roll = "my club roll", Type = "my club damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club, "size"))
                .Returns(club);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");

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
            var ranged = GetRangedWithBowTemplates();
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
            dagger.Damages.Add(new Damage { Roll = "my dagger roll", Type = "my dagger damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(dagger);

            var crossbow = new Weapon();
            crossbow.Name = WeaponConstants.LightCrossbow;
            crossbow.Damages.Add(new Damage { Roll = "my crossbow roll", Type = "my crossbow damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightCrossbow, "size"))
                .Returns(crossbow);

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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield", "size"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");

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
            dagger.Damages.Add(new Damage { Roll = "my dagger roll", Type = "my dagger damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Dagger, "size"))
                .Returns(dagger);

            var club = new Weapon();
            club.Name = WeaponConstants.Club;
            club.Damages.Add(new Damage { Roll = "my club roll", Type = "my club damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.Club, "size"))
                .Returns(club);

            var pick = new Weapon();
            pick.Name = WeaponConstants.LightPick;
            pick.Damages.Add(new Damage { Roll = "my pick roll", Type = "my pick damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, WeaponConstants.LightPick, "size"))
                .Returns(pick);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");

            Assert.That(equipment.Weapons.Count(), Is.EqualTo(3));
            Assert.That(equipment.Weapons, Contains.Item(dagger)
                .And.Contains(club)
                .And.Contains(pick));
            Assert.That(equipment.Shield, Is.Null);
            mockItemGenerator.Verify(g => g.GenerateAtLevel(It.IsAny<int>(), ItemTypeConstants.Armor, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GenerateArmor_Predetermined_Mundane_WithSize()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my armor template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Armor,
                Name = "my armor template",
                Traits = new HashSet<string> { SizeConstants.Medium }
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
                .Setup(g => g.Generate(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_Predetermined_Mundane_WithoutSize()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));

            Assert.That(template.Traits, Contains.Item("size"));
        }

        [Test]
        public void GenerateArmor_Predetermined_Magical_WithSize()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my armor template" });

            var template = new Item
            {
                ItemType = ItemTypeConstants.Armor,
                Name = "my armor template",
                IsMagical = true,
                Traits = new HashSet<string> { SizeConstants.Medium }
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
                .Setup(g => g.Generate(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_Predetermined_Magical_WithoutSize()
        {
            feats.Add(new Feat { Name = FeatConstants.ArmorProficiency_Light });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));

            Assert.That(template.Traits, Contains.Item("size"));
        }

        //Example is Nessian Warhound with its barding
        [Test]
        public void GenerateArmor_Predetermined_EvenIfCannotUseEquipment()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", false, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void GenerateArmor_OfCreatureSize()
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
            armor.Size = "size";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor", "size"))
                .Returns(armor);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Armor, Is.EqualTo(armor));
            Assert.That(equipment.Armor.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateShield_OfCreatureSize()
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
            shield.Size = "size";

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield", "size"))
                .Returns(shield);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Shield, Is.EqualTo(shield));
            Assert.That(equipment.Shield.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateItem_Predetermined_Mundane()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));
        }

        [Test]
        public void GenerateItem_Predetermined_Magical()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));
        }

        [Test]
        public void GenerateMultipleItems_Predetermined()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
                .Returns(new[] { "my mundane item template", "my magical item template" });

            var mundaneTemplate = new Item
            {
                ItemType = "my mundane item type",
                Name = "my mundane item template",
            };
            var magicalTemplate = new Item
            {
                ItemType = "my magical item type",
                Name = "my magical item template",
                IsMagical = true,
            };
            mockItemSelector
                .Setup(s => s.SelectFrom("my mundane item template"))
                .Returns(mundaneTemplate);
            mockItemSelector
                .Setup(s => s.SelectFrom("my magical item template"))
                .Returns(magicalTemplate);

            var mockMundaneItemGenerator = new Mock<MundaneItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MundaneItemGenerator>("my mundane item type"))
                .Returns(mockMundaneItemGenerator.Object);

            var mundaneItem = new Item { Name = "my mundane item" };
            mockMundaneItemGenerator
                .Setup(g => g.Generate(mundaneTemplate, false))
                .Returns(mundaneItem);

            var mockMagicalItemGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<MagicalItemGenerator>("my magical item type"))
                .Returns(mockMagicalItemGenerator.Object);

            var magicalItem = new Item { Name = "my magical item" };
            mockMagicalItemGenerator
                .Setup(g => g.Generate(magicalTemplate, false))
                .Returns(magicalItem);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Items.Count(), Is.EqualTo(2));
            Assert.That(equipment.Items, Contains.Item(mundaneItem)
                .And.Contains(magicalItem));
        }

        [Test]
        public void GenerateAllEquipment()
        {
            attacks.Add(new Attack { Name = AttributeConstants.Melee, IsNatural = false, IsMelee = true });
            attacks.Add(new Attack { Name = AttributeConstants.Ranged, IsNatural = false, IsMelee = false });
            feats.Add(new Feat { Name = FeatConstants.WeaponProficiency_Simple, Foci = new[] { GroupConstants.All } });

            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var ammunition = WeaponConstants.GetAllAmmunition(false, false);
            var ranged = GetRangedWithBowTemplates();
            var simpleMelee = simple.Intersect(melee);
            var simpleRanged = simple.Intersect(ranged);
            var nonMelee = melee.Except(simpleMelee);
            var nonRanged = ranged.Except(simpleRanged);

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
            meleeWeapon.Name = WeaponConstants.Club;
            meleeWeapon.Damages.Add(new Damage { Roll = "my club roll", Type = "my club damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple melee weapon", "size"))
                .Returns(meleeWeapon);

            var rangedWeapon = new Weapon();
            rangedWeapon.Name = WeaponConstants.Dart;
            rangedWeapon.Damages.Add(new Damage { Roll = "my dart roll", Type = "my dart damage type" });

            mockItemGenerator
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Weapon, "simple ranged weapon", "size"))
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my armor", "size"))
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
                .Setup(g => g.GenerateAtLevel(9266, ItemTypeConstants.Armor, "my shield", "size"))
                .Returns(shield);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.PredeterminedItems, "creature"))
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
                .Setup(g => g.Generate(template, false))
                .Returns(item);

            var equipment = equipmentGenerator.Generate("creature", true, feats, 9266, attacks, abilities, "size");
            Assert.That(equipment.Weapons.Count(), Is.EqualTo(2));
            Assert.That(equipment.Weapons, Contains.Item(meleeWeapon)
                .And.Contains(rangedWeapon));

            Assert.That(equipment.Armor, Is.EqualTo(armor));
            Assert.That(equipment.Shield, Is.EqualTo(shield));
            Assert.That(equipment.Items.Count(), Is.EqualTo(1));
            Assert.That(equipment.Items, Contains.Item(item));

            Assert.That(attacks, Has.Count.EqualTo(7));
            Assert.That(attacks[5].Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(attacks[5].DamageSummary, Is.EqualTo("my club roll my club damage type"));
            Assert.That(attacks[5].IsMelee, Is.True);
            Assert.That(attacks[5].IsNatural, Is.False);
            Assert.That(attacks[5].IsSpecial, Is.False);
            Assert.That(attacks[5].AttackBonuses, Is.Empty);

            Assert.That(attacks[6].Name, Is.EqualTo(WeaponConstants.Dart));
            Assert.That(attacks[6].DamageSummary, Is.EqualTo("my dart roll my dart damage type"));
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

        [Test]
        public void BUG_AddAttacks_UnnaturalEquipmentAttacks_MeleeAndRanged()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(2));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.False);
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [Test]
        public void BUG_AddAttacks_UnnaturalEquipmentAttacks_MultipleMeleeAndRanged()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(3));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.True);
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.False);
            Assert.That(updatedAttacks[2].IsPrimary, Is.True);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [Test]
        public void BUG_AddAttacks_UnnaturalEquipmentAttacks_MeleeAndMultipleRanged()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(3));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.False);
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.False);
            Assert.That(updatedAttacks[2].IsPrimary, Is.True);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
        }

        [Test]
        public void BUG_AddAttacks_UnnaturalEquipmentAttacks_MultipleMeleeAndMultipleRanged()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Ranged,
                IsNatural = false,
                IsMelee = false,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 2).ToArray();
            Assert.That(updatedAttacks, Is.EqualTo(attacks).And.Length.EqualTo(4));
            Assert.That(updatedAttacks[0].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.True);
            Assert.That(updatedAttacks[1].IsPrimary, Is.True);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.False);
            Assert.That(updatedAttacks[2].IsPrimary, Is.True);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[3].Name, Is.EqualTo(AttributeConstants.Ranged));
            Assert.That(updatedAttacks[3].IsNatural, Is.False);
            Assert.That(updatedAttacks[3].IsMelee, Is.False);
            Assert.That(updatedAttacks[3].IsPrimary, Is.True);
            Assert.That(updatedAttacks[3].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[3].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
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

        //INFO: This is based of Athach
        [Test]
        public void BUG_AddAttacks_UnnaturalEquippedAttack_FillHands_WithDistinctDamages()
        {
            attacks.Clear();
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = true,
                Frequency = new Frequency
                {
                    Quantity = 1,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });
            attacks.Add(new Attack
            {
                Name = AttributeConstants.Melee,
                IsNatural = false,
                IsMelee = true,
                IsPrimary = false,
                Frequency = new Frequency
                {
                    Quantity = 2,
                    TimePeriod = FeatConstants.Frequencies.Round
                },
            });

            feats.Add(new Feat { Name = FeatConstants.Monster.MultiweaponFighting });

            var updatedAttacks = equipmentGenerator.AddAttacks(feats, attacks, 3).ToArray();
            Assert.That(updatedAttacks, Has.Length.EqualTo(3));

            Assert.That(updatedAttacks[0].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[0].IsNatural, Is.False);
            Assert.That(updatedAttacks[0].IsMelee, Is.True);
            Assert.That(updatedAttacks[0].IsPrimary, Is.True);
            Assert.That(updatedAttacks[0].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[0].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[0].Damages, Is.Not.SameAs(updatedAttacks[1].Damages)
                .And.Not.SameAs(updatedAttacks[2].Damages));
            Assert.That(updatedAttacks[1].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[1].IsNatural, Is.False);
            Assert.That(updatedAttacks[1].IsMelee, Is.True);
            Assert.That(updatedAttacks[1].IsPrimary, Is.False);
            Assert.That(updatedAttacks[1].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[1].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[1].Damages, Is.Not.SameAs(updatedAttacks[0].Damages)
                .And.Not.SameAs(updatedAttacks[2].Damages));
            Assert.That(updatedAttacks[2].Name, Is.EqualTo(AttributeConstants.Melee));
            Assert.That(updatedAttacks[2].IsNatural, Is.False);
            Assert.That(updatedAttacks[2].IsMelee, Is.True);
            Assert.That(updatedAttacks[2].IsPrimary, Is.False);
            Assert.That(updatedAttacks[2].Frequency.Quantity, Is.EqualTo(1));
            Assert.That(updatedAttacks[2].Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(updatedAttacks[2].Damages, Is.Not.SameAs(updatedAttacks[1].Damages)
                .And.Not.SameAs(updatedAttacks[0].Damages));
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
                .Setup(s => s.SelectFrom(.99))
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
}
