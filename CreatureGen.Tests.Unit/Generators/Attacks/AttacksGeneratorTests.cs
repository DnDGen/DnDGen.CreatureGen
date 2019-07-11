using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Attacks;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Attacks
{
    [TestFixture]
    public class AttacksGeneratorTests
    {
        private IAttacksGenerator attacksGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<IAttackSelector> mockAttackSelector;
        private CreatureType creatureType;
        private HitPoints hitPoints;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockAttackSelector = new Mock<IAttackSelector>();

            attacksGenerator = new AttacksGenerator(mockCollectionSelector.Object, mockAdjustmentSelector.Object, mockAttackSelector.Object);

            creatureType = new CreatureType();
            creatureType.Name = "creature type";

            hitPoints = new HitPoints();

            abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
        }

        [TestCase(0, GroupConstants.GoodBaseAttack, 0)]
        [TestCase(1, GroupConstants.GoodBaseAttack, 1)]
        [TestCase(2, GroupConstants.GoodBaseAttack, 2)]
        [TestCase(3, GroupConstants.GoodBaseAttack, 3)]
        [TestCase(4, GroupConstants.GoodBaseAttack, 4)]
        [TestCase(5, GroupConstants.GoodBaseAttack, 5)]
        [TestCase(6, GroupConstants.GoodBaseAttack, 6)]
        [TestCase(7, GroupConstants.GoodBaseAttack, 7)]
        [TestCase(8, GroupConstants.GoodBaseAttack, 8)]
        [TestCase(9, GroupConstants.GoodBaseAttack, 9)]
        [TestCase(10, GroupConstants.GoodBaseAttack, 10)]
        [TestCase(11, GroupConstants.GoodBaseAttack, 11)]
        [TestCase(12, GroupConstants.GoodBaseAttack, 12)]
        [TestCase(13, GroupConstants.GoodBaseAttack, 13)]
        [TestCase(14, GroupConstants.GoodBaseAttack, 14)]
        [TestCase(15, GroupConstants.GoodBaseAttack, 15)]
        [TestCase(16, GroupConstants.GoodBaseAttack, 16)]
        [TestCase(17, GroupConstants.GoodBaseAttack, 17)]
        [TestCase(18, GroupConstants.GoodBaseAttack, 18)]
        [TestCase(19, GroupConstants.GoodBaseAttack, 19)]
        [TestCase(20, GroupConstants.GoodBaseAttack, 20)]
        [TestCase(9266, GroupConstants.GoodBaseAttack, 9266)]
        [TestCase(0, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(1, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(2, GroupConstants.AverageBaseAttack, 1)]
        [TestCase(3, GroupConstants.AverageBaseAttack, 2)]
        [TestCase(4, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(5, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(6, GroupConstants.AverageBaseAttack, 4)]
        [TestCase(7, GroupConstants.AverageBaseAttack, 5)]
        [TestCase(8, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(9, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(10, GroupConstants.AverageBaseAttack, 7)]
        [TestCase(11, GroupConstants.AverageBaseAttack, 8)]
        [TestCase(12, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(13, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(14, GroupConstants.AverageBaseAttack, 10)]
        [TestCase(15, GroupConstants.AverageBaseAttack, 11)]
        [TestCase(16, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(17, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(18, GroupConstants.AverageBaseAttack, 13)]
        [TestCase(19, GroupConstants.AverageBaseAttack, 14)]
        [TestCase(20, GroupConstants.AverageBaseAttack, 15)]
        [TestCase(9266, GroupConstants.AverageBaseAttack, 6949)]
        [TestCase(0, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(1, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(2, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(3, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(4, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(5, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(6, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(7, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(8, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(9, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(10, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(11, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(12, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(13, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(14, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(15, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(16, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(17, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(18, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(19, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(20, GroupConstants.PoorBaseAttack, 10)]
        [TestCase(9266, GroupConstants.PoorBaseAttack, 4633)]
        public void GenerateBaseAttackBonus(int hitDiceQuantity, string bonusQuality, int bonus)
        {
            hitPoints.HitDiceQuantity = hitDiceQuantity;
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Collection.CreatureGroups, creatureType.Name,
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(bonusQuality);

            var baseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creatureType, hitPoints);
            Assert.That(baseAttackBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void GenerateGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "size")).Returns(42);

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("size", 9266, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.EqualTo(9266 + abilities[AbilityConstants.Strength].Modifier + 42));
        }

        [Test]
        public void GenerateNegativeGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1;

            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "size")).Returns(-1);

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("size", 0, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.EqualTo(0 + abilities[AbilityConstants.Strength].Modifier - 1));
            Assert.That(grappleBonus, Is.EqualTo(-6));
        }

        [Test]
        public void NoGrappleBonusIfNoStrengthScore()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;

            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "size")).Returns(42);

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("size", 9266, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.Null);
        }

        [TestCase(false, false, false, false)]
        [TestCase(false, false, false, true)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, true, true)]
        [TestCase(false, true, false, false)]
        [TestCase(false, true, false, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, true, true, true)]
        [TestCase(true, false, false, false)]
        [TestCase(true, false, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, true, false, true)]
        [TestCase(true, true, true, false)]
        [TestCase(true, true, true, true)]
        public void GenerateAttack(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = "damage", IsMelee = isMelee, IsNatural = isNatural, IsPrimary = isPrimary, IsSpecial = isSpecial });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo("damage"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
        }

        [Test]
        public void GenerateAttacks()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = "damage" });
            attacks.Add(new AttackSelection() { Name = "other attack", Damage = "other damage" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(2));

            var attack = generatedAttacks.First();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo("damage"));

            attack = generatedAttacks.Last();
            Assert.That(attack.Name, Is.EqualTo("other attack"));
            Assert.That(attack.Damage, Is.EqualTo("other damage"));
        }

        [Test]
        public void GenerateNoAttacks()
        {
            var attacks = new List<AttackSelection>();

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Empty);
        }

        [Test]
        public void GenerateAttackBaseAttackBonus()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbility()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1", IsMelee = true });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateRangedAttackBaseAbility()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1", IsMelee = false });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbilityWithNoStrength()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1", IsMelee = true });

            abilities[AbilityConstants.Strength].BaseScore = 0;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateAttackSizeModifier()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(90210);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SizeModifierForAttackBonus, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateSpecialAttack()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection { Name = "attack 1", IsSpecial = true });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(90210);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.Zero);
            Assert.That(generatedAttack.BaseAbility, Is.Null);
            Assert.That(generatedAttack.SizeModifierForAttackBonus, Is.Zero);
        }

        [TestCase(AbilityConstants.Strength, true, true)]
        [TestCase(AbilityConstants.Strength, true, false)]
        [TestCase(AbilityConstants.Strength, false, true)]
        [TestCase(AbilityConstants.Strength, false, false)]
        [TestCase(AbilityConstants.Constitution, true, true)]
        [TestCase(AbilityConstants.Constitution, true, false)]
        [TestCase(AbilityConstants.Constitution, false, true)]
        [TestCase(AbilityConstants.Constitution, false, false)]
        [TestCase(AbilityConstants.Dexterity, true, true)]
        [TestCase(AbilityConstants.Dexterity, true, false)]
        [TestCase(AbilityConstants.Dexterity, false, true)]
        [TestCase(AbilityConstants.Dexterity, false, false)]
        [TestCase(AbilityConstants.Intelligence, true, true)]
        [TestCase(AbilityConstants.Intelligence, true, false)]
        [TestCase(AbilityConstants.Intelligence, false, true)]
        [TestCase(AbilityConstants.Intelligence, false, false)]
        [TestCase(AbilityConstants.Wisdom, true, true)]
        [TestCase(AbilityConstants.Wisdom, true, false)]
        [TestCase(AbilityConstants.Wisdom, false, true)]
        [TestCase(AbilityConstants.Wisdom, false, false)]
        [TestCase(AbilityConstants.Charisma, true, true)]
        [TestCase(AbilityConstants.Charisma, true, false)]
        [TestCase(AbilityConstants.Charisma, false, true)]
        [TestCase(AbilityConstants.Charisma, false, false)]
        public void GenerateAttackWithAbilityInDamage_Primary_Sole(string ability, bool isMelee, bool isNatural)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = isMelee, IsNatural = isNatural });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo("damage + 67650 + effect"));
        }

        [TestCase(1, -7)]
        [TestCase(2, -6)]
        [TestCase(3, -6)]
        [TestCase(4, -4)]
        [TestCase(5, -4)]
        [TestCase(6, -3)]
        [TestCase(7, -3)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 3)]
        [TestCase(15, 3)]
        [TestCase(16, 4)]
        [TestCase(17, 4)]
        [TestCase(18, 6)]
        [TestCase(19, 6)]
        [TestCase(20, 7)]
        [TestCase(21, 7)]
        [TestCase(22, 9)]
        [TestCase(23, 9)]
        [TestCase(24, 10)]
        [TestCase(25, 10)]
        [TestCase(26, 12)]
        [TestCase(27, 12)]
        [TestCase(28, 13)]
        [TestCase(29, 13)]
        [TestCase(30, 15)]
        [TestCase(31, 15)]
        [TestCase(32, 16)]
        [TestCase(33, 16)]
        [TestCase(34, 18)]
        [TestCase(35, 18)]
        [TestCase(36, 19)]
        [TestCase(37, 19)]
        [TestCase(38, 21)]
        [TestCase(39, 21)]
        [TestCase(40, 22)]
        [TestCase(41, 22)]
        [TestCase(42, 24)]
        [TestCase(43, 24)]
        [TestCase(44, 25)]
        [TestCase(45, 25)]
        [TestCase(46, 27)]
        [TestCase(47, 27)]
        [TestCase(48, 28)]
        [TestCase(49, 28)]
        [TestCase(50, 30)]
        public void GenerateAttackWithAbilityInDamage_Primary_Sole(int value, int bonus)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {AbilityConstants.Strength.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo($"damage + {bonus} + effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void GenerateAttackWithAbilityInDamage_Primary_AllSole(string ability)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "nat melee attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "nat range attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = false, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "melee attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = false });
            attackSelections.Add(new AttackSelection() { Name = "range attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = false, IsNatural = false });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(4));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("nat melee attack"));
            Assert.That(attacks[0].IsMelee, Is.True);
            Assert.That(attacks[0].IsNatural, Is.True);
            Assert.That(attacks[0].Damage, Is.EqualTo("damage + 67650 + effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("nat range attack"));
            Assert.That(attacks[1].IsMelee, Is.False);
            Assert.That(attacks[1].IsNatural, Is.True);
            Assert.That(attacks[1].Damage, Is.EqualTo("damage + 67650 + effect"));
            Assert.That(attacks[2].Name, Is.EqualTo("melee attack"));
            Assert.That(attacks[2].IsMelee, Is.True);
            Assert.That(attacks[2].IsNatural, Is.False);
            Assert.That(attacks[2].Damage, Is.EqualTo("damage + 67650 + effect"));
            Assert.That(attacks[3].Name, Is.EqualTo("range attack"));
            Assert.That(attacks[3].IsMelee, Is.False);
            Assert.That(attacks[3].IsNatural, Is.False);
            Assert.That(attacks[3].Damage, Is.EqualTo("damage + 67650 + effect"));
        }

        [TestCase(AbilityConstants.Strength, true, true)]
        [TestCase(AbilityConstants.Strength, true, false)]
        [TestCase(AbilityConstants.Strength, false, true)]
        [TestCase(AbilityConstants.Strength, false, false)]
        [TestCase(AbilityConstants.Constitution, true, true)]
        [TestCase(AbilityConstants.Constitution, true, false)]
        [TestCase(AbilityConstants.Constitution, false, true)]
        [TestCase(AbilityConstants.Constitution, false, false)]
        [TestCase(AbilityConstants.Dexterity, true, true)]
        [TestCase(AbilityConstants.Dexterity, true, false)]
        [TestCase(AbilityConstants.Dexterity, false, true)]
        [TestCase(AbilityConstants.Dexterity, false, false)]
        [TestCase(AbilityConstants.Intelligence, true, true)]
        [TestCase(AbilityConstants.Intelligence, true, false)]
        [TestCase(AbilityConstants.Intelligence, false, true)]
        [TestCase(AbilityConstants.Intelligence, false, false)]
        [TestCase(AbilityConstants.Wisdom, true, true)]
        [TestCase(AbilityConstants.Wisdom, true, false)]
        [TestCase(AbilityConstants.Wisdom, false, true)]
        [TestCase(AbilityConstants.Wisdom, false, false)]
        [TestCase(AbilityConstants.Charisma, true, true)]
        [TestCase(AbilityConstants.Charisma, true, false)]
        [TestCase(AbilityConstants.Charisma, false, true)]
        [TestCase(AbilityConstants.Charisma, false, false)]
        public void GenerateAttackWithAbilityInDamage_Primary_Multiple(string ability, bool isMelee, bool isNatural)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = isMelee, IsNatural = isNatural });
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = isMelee, IsNatural = isNatural });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].Damage, Is.EqualTo("damage + 45100 + effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("attack"));
            Assert.That(attacks[1].Damage, Is.EqualTo("damage + 45100 + effect"));
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(21, 5)]
        [TestCase(22, 6)]
        [TestCase(23, 6)]
        [TestCase(24, 7)]
        [TestCase(25, 7)]
        [TestCase(26, 8)]
        [TestCase(27, 8)]
        [TestCase(28, 9)]
        [TestCase(29, 9)]
        [TestCase(30, 10)]
        [TestCase(31, 10)]
        [TestCase(32, 11)]
        [TestCase(33, 11)]
        [TestCase(34, 12)]
        [TestCase(35, 12)]
        [TestCase(36, 13)]
        [TestCase(37, 13)]
        [TestCase(38, 14)]
        [TestCase(39, 14)]
        [TestCase(40, 15)]
        [TestCase(41, 15)]
        [TestCase(42, 16)]
        [TestCase(43, 16)]
        [TestCase(44, 17)]
        [TestCase(45, 17)]
        [TestCase(46, 18)]
        [TestCase(47, 18)]
        [TestCase(48, 19)]
        [TestCase(49, 19)]
        [TestCase(50, 20)]
        public void GenerateAttackWithAbilityInDamage_Primary_Multiple(int value, int bonus)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {AbilityConstants.Strength.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {AbilityConstants.Strength.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].Damage, Is.EqualTo($"damage + {bonus} + effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("attack"));
            Assert.That(attacks[1].Damage, Is.EqualTo($"damage + {bonus} + effect"));
        }

        [TestCase(AbilityConstants.Strength, true, true)]
        [TestCase(AbilityConstants.Strength, true, false)]
        [TestCase(AbilityConstants.Strength, false, true)]
        [TestCase(AbilityConstants.Strength, false, false)]
        [TestCase(AbilityConstants.Constitution, true, true)]
        [TestCase(AbilityConstants.Constitution, true, false)]
        [TestCase(AbilityConstants.Constitution, false, true)]
        [TestCase(AbilityConstants.Constitution, false, false)]
        [TestCase(AbilityConstants.Dexterity, true, true)]
        [TestCase(AbilityConstants.Dexterity, true, false)]
        [TestCase(AbilityConstants.Dexterity, false, true)]
        [TestCase(AbilityConstants.Dexterity, false, false)]
        [TestCase(AbilityConstants.Intelligence, true, true)]
        [TestCase(AbilityConstants.Intelligence, true, false)]
        [TestCase(AbilityConstants.Intelligence, false, true)]
        [TestCase(AbilityConstants.Intelligence, false, false)]
        [TestCase(AbilityConstants.Wisdom, true, true)]
        [TestCase(AbilityConstants.Wisdom, true, false)]
        [TestCase(AbilityConstants.Wisdom, false, true)]
        [TestCase(AbilityConstants.Wisdom, false, false)]
        [TestCase(AbilityConstants.Charisma, true, true)]
        [TestCase(AbilityConstants.Charisma, true, false)]
        [TestCase(AbilityConstants.Charisma, false, true)]
        [TestCase(AbilityConstants.Charisma, false, false)]
        public void GenerateAttackWithAbilityInDamage_Primary_WithSecondary(string ability, bool isMelee, bool isNatural)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = isMelee, IsNatural = isNatural });
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = false, IsMelee = isMelee, IsNatural = isNatural });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].Damage, Is.EqualTo("damage + 45100 + effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("attack"));
            Assert.That(attacks[1].Damage, Is.EqualTo("damage + 22550 + effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void GenerateAttackWithAbilityInDamage_Primary_SoleAndMultiple(string ability)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()}", IsPrimary = true, IsMelee = true, IsNatural = false });
            attackSelections.Add(new AttackSelection() { Name = "nat attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "nat attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(3));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].Damage, Is.EqualTo("damage + 67650"));
            Assert.That(attacks[1].Name, Is.EqualTo("nat attack"));
            Assert.That(attacks[1].Damage, Is.EqualTo("damage + 45100 + effect"));
            Assert.That(attacks[2].Name, Is.EqualTo("nat attack"));
            Assert.That(attacks[2].Damage, Is.EqualTo("damage + 45100 + effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void GenerateAttackWithAbilityInDamage_Secondary(string ability)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability.ToUpper()} + effect", IsPrimary = false });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo("damage + 22550 + effect"));
        }

        [TestCase(1, -2)]
        [TestCase(2, -2)]
        [TestCase(3, -2)]
        [TestCase(4, -1)]
        [TestCase(5, -1)]
        [TestCase(6, -1)]
        [TestCase(7, -1)]
        [TestCase(8, 0)]
        [TestCase(9, 0)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 0)]
        [TestCase(13, 0)]
        [TestCase(14, 1)]
        [TestCase(15, 1)]
        [TestCase(16, 1)]
        [TestCase(17, 1)]
        [TestCase(18, 2)]
        [TestCase(19, 2)]
        [TestCase(20, 2)]
        [TestCase(21, 2)]
        [TestCase(22, 3)]
        [TestCase(23, 3)]
        [TestCase(24, 3)]
        [TestCase(25, 3)]
        [TestCase(26, 4)]
        [TestCase(27, 4)]
        [TestCase(28, 4)]
        [TestCase(29, 4)]
        [TestCase(30, 5)]
        [TestCase(31, 5)]
        [TestCase(32, 5)]
        [TestCase(33, 5)]
        [TestCase(34, 6)]
        [TestCase(35, 6)]
        [TestCase(36, 6)]
        [TestCase(37, 6)]
        [TestCase(38, 7)]
        [TestCase(39, 7)]
        [TestCase(40, 7)]
        [TestCase(41, 7)]
        [TestCase(42, 8)]
        [TestCase(43, 8)]
        [TestCase(44, 8)]
        [TestCase(45, 8)]
        [TestCase(46, 9)]
        [TestCase(47, 9)]
        [TestCase(48, 9)]
        [TestCase(49, 9)]
        [TestCase(50, 10)]
        public void GenerateAttackWithAbilityInDamage_Secondary(int value, int bonus)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {AbilityConstants.Strength.ToUpper()} + effect", IsPrimary = false, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo($"damage + {bonus} + effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void GenerateAttackWithAbilityEffectInDamage(string ability)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", Damage = $"damage + {ability} drain" });

            abilities[ability].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damage, Is.EqualTo($"damage + {ability} drain"));
        }

        [Test]
        public void ApplyPrimaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.Zero);
        }

        [Test]
        public void ApplySecondaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = false });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackModifiers, Is.EqualTo(-2));
        }

        [Test]
        public void ApplySpecialAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", Damage = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", Damage = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", Damage = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", Damage = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", Damage = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", Damage = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", Damage = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = FeatConstants.Monster.Multiattack }
            };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);

            foreach (var attack in generatedAttacks)
                Assert.That(attack.SecondaryAttackModifiers, Is.Zero);
        }

        [Test]
        [Ignore("Need to have equipment stuff done first")]
        public void ApplyDexterityForMeleeInsteadOfStrengthForLightWeaponsIfWeaponFinesse()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyDexterityForMeleeInsteadOfStrengthForNaturalAttacksIfWeaponFinesse()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });

            var feats = new[] { new Feat { Name = FeatConstants.WeaponFinesse } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void ApplyStrengthForMeleeForNaturalAttacksIfNotWeaponFinesse()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength] });

            var feats = new[] { new Feat { Name = "not " + FeatConstants.WeaponFinesse } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }
    }
}
