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
        public void GenerateAttack_WithoutSave(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection()
            {
                Name = "attack",
                DamageRoll = "damage roll",
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 9266,
                FrequencyTimePeriod = "time period",
                IsMelee = isMelee,
                IsNatural = isNatural,
                IsPrimary = isPrimary,
                IsSpecial = isSpecial
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage roll"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Null);
        }

        [TestCase(false, false, false, false, AbilityConstants.Charisma)]
        [TestCase(false, false, false, false, AbilityConstants.Constitution)]
        [TestCase(false, false, false, false, AbilityConstants.Dexterity)]
        [TestCase(false, false, false, false, AbilityConstants.Intelligence)]
        [TestCase(false, false, false, false, AbilityConstants.Strength)]
        [TestCase(false, false, false, false, AbilityConstants.Wisdom)]
        [TestCase(false, false, false, true, AbilityConstants.Charisma)]
        [TestCase(false, false, false, true, AbilityConstants.Constitution)]
        [TestCase(false, false, false, true, AbilityConstants.Dexterity)]
        [TestCase(false, false, false, true, AbilityConstants.Intelligence)]
        [TestCase(false, false, false, true, AbilityConstants.Strength)]
        [TestCase(false, false, false, true, AbilityConstants.Wisdom)]
        [TestCase(false, false, true, false, AbilityConstants.Charisma)]
        [TestCase(false, false, true, false, AbilityConstants.Constitution)]
        [TestCase(false, false, true, false, AbilityConstants.Dexterity)]
        [TestCase(false, false, true, false, AbilityConstants.Intelligence)]
        [TestCase(false, false, true, false, AbilityConstants.Strength)]
        [TestCase(false, false, true, false, AbilityConstants.Wisdom)]
        [TestCase(false, false, true, true, AbilityConstants.Charisma)]
        [TestCase(false, false, true, true, AbilityConstants.Constitution)]
        [TestCase(false, false, true, true, AbilityConstants.Dexterity)]
        [TestCase(false, false, true, true, AbilityConstants.Intelligence)]
        [TestCase(false, false, true, true, AbilityConstants.Strength)]
        [TestCase(false, false, true, true, AbilityConstants.Wisdom)]
        [TestCase(false, true, false, false, AbilityConstants.Charisma)]
        [TestCase(false, true, false, false, AbilityConstants.Constitution)]
        [TestCase(false, true, false, false, AbilityConstants.Dexterity)]
        [TestCase(false, true, false, false, AbilityConstants.Intelligence)]
        [TestCase(false, true, false, false, AbilityConstants.Strength)]
        [TestCase(false, true, false, false, AbilityConstants.Wisdom)]
        [TestCase(false, true, false, true, AbilityConstants.Charisma)]
        [TestCase(false, true, false, true, AbilityConstants.Constitution)]
        [TestCase(false, true, false, true, AbilityConstants.Dexterity)]
        [TestCase(false, true, false, true, AbilityConstants.Intelligence)]
        [TestCase(false, true, false, true, AbilityConstants.Strength)]
        [TestCase(false, true, false, true, AbilityConstants.Wisdom)]
        [TestCase(false, true, true, false, AbilityConstants.Charisma)]
        [TestCase(false, true, true, false, AbilityConstants.Constitution)]
        [TestCase(false, true, true, false, AbilityConstants.Dexterity)]
        [TestCase(false, true, true, false, AbilityConstants.Intelligence)]
        [TestCase(false, true, true, false, AbilityConstants.Strength)]
        [TestCase(false, true, true, false, AbilityConstants.Wisdom)]
        [TestCase(false, true, true, true, AbilityConstants.Charisma)]
        [TestCase(false, true, true, true, AbilityConstants.Constitution)]
        [TestCase(false, true, true, true, AbilityConstants.Dexterity)]
        [TestCase(false, true, true, true, AbilityConstants.Intelligence)]
        [TestCase(false, true, true, true, AbilityConstants.Strength)]
        [TestCase(false, true, true, true, AbilityConstants.Wisdom)]
        [TestCase(true, false, false, false, AbilityConstants.Charisma)]
        [TestCase(true, false, false, false, AbilityConstants.Constitution)]
        [TestCase(true, false, false, false, AbilityConstants.Dexterity)]
        [TestCase(true, false, false, false, AbilityConstants.Intelligence)]
        [TestCase(true, false, false, false, AbilityConstants.Strength)]
        [TestCase(true, false, false, false, AbilityConstants.Wisdom)]
        [TestCase(true, false, false, true, AbilityConstants.Charisma)]
        [TestCase(true, false, false, true, AbilityConstants.Constitution)]
        [TestCase(true, false, false, true, AbilityConstants.Dexterity)]
        [TestCase(true, false, false, true, AbilityConstants.Intelligence)]
        [TestCase(true, false, false, true, AbilityConstants.Strength)]
        [TestCase(true, false, false, true, AbilityConstants.Wisdom)]
        [TestCase(true, false, true, false, AbilityConstants.Charisma)]
        [TestCase(true, false, true, false, AbilityConstants.Constitution)]
        [TestCase(true, false, true, false, AbilityConstants.Dexterity)]
        [TestCase(true, false, true, false, AbilityConstants.Intelligence)]
        [TestCase(true, false, true, false, AbilityConstants.Strength)]
        [TestCase(true, false, true, false, AbilityConstants.Wisdom)]
        [TestCase(true, false, true, true, AbilityConstants.Charisma)]
        [TestCase(true, false, true, true, AbilityConstants.Constitution)]
        [TestCase(true, false, true, true, AbilityConstants.Dexterity)]
        [TestCase(true, false, true, true, AbilityConstants.Intelligence)]
        [TestCase(true, false, true, true, AbilityConstants.Strength)]
        [TestCase(true, false, true, true, AbilityConstants.Wisdom)]
        [TestCase(true, true, false, false, AbilityConstants.Charisma)]
        [TestCase(true, true, false, false, AbilityConstants.Constitution)]
        [TestCase(true, true, false, false, AbilityConstants.Dexterity)]
        [TestCase(true, true, false, false, AbilityConstants.Intelligence)]
        [TestCase(true, true, false, false, AbilityConstants.Strength)]
        [TestCase(true, true, false, false, AbilityConstants.Wisdom)]
        [TestCase(true, true, false, true, AbilityConstants.Charisma)]
        [TestCase(true, true, false, true, AbilityConstants.Constitution)]
        [TestCase(true, true, false, true, AbilityConstants.Dexterity)]
        [TestCase(true, true, false, true, AbilityConstants.Intelligence)]
        [TestCase(true, true, false, true, AbilityConstants.Strength)]
        [TestCase(true, true, false, true, AbilityConstants.Wisdom)]
        [TestCase(true, true, true, false, AbilityConstants.Charisma)]
        [TestCase(true, true, true, false, AbilityConstants.Constitution)]
        [TestCase(true, true, true, false, AbilityConstants.Dexterity)]
        [TestCase(true, true, true, false, AbilityConstants.Intelligence)]
        [TestCase(true, true, true, false, AbilityConstants.Strength)]
        [TestCase(true, true, true, false, AbilityConstants.Wisdom)]
        [TestCase(true, true, true, true, AbilityConstants.Charisma)]
        [TestCase(true, true, true, true, AbilityConstants.Constitution)]
        [TestCase(true, true, true, true, AbilityConstants.Dexterity)]
        [TestCase(true, true, true, true, AbilityConstants.Intelligence)]
        [TestCase(true, true, true, true, AbilityConstants.Strength)]
        [TestCase(true, true, true, true, AbilityConstants.Wisdom)]
        public void GenerateAttack_WithSave(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial, string saveAbility)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection()
            {
                Name = "attack",
                DamageRoll = "damage roll",
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 9266,
                FrequencyTimePeriod = "time period",
                IsMelee = isMelee,
                IsNatural = isNatural,
                IsPrimary = isPrimary,
                IsSpecial = isSpecial,
                Save = "save",
                SaveAbility = saveAbility,
                BaseSave = 90210,
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage roll"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(90210));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[saveAbility]));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void GenerateAttacks()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = "damage" });
            attacks.Add(new AttackSelection() { Name = "other attack", DamageRoll = "other damage" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(2));

            var attack = generatedAttacks.First();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));

            attack = generatedAttacks.Last();
            Assert.That(attack.Name, Is.EqualTo("other attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("other damage"));
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

            Assert.That(generatedAttack.SizeModifier, Is.EqualTo(90210));
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
            Assert.That(generatedAttack.SizeModifier, Is.Zero);
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
        public void GenerateAttack_Primary_Melee_Sole(int value, int bonus)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.EqualTo(bonus));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Breath()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 0, FrequencyQuantity = 1, IsPrimary = true, IsMelee = false, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.Zero);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Thrown()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = false, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.EqualTo(67650));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_AllSole()
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "nat melee attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = true, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "nat range attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = false, IsNatural = true });
            attackSelections.Add(new AttackSelection() { Name = "melee attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = true, IsNatural = false });
            attackSelections.Add(new AttackSelection() { Name = "range attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = false, IsNatural = false });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(4));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("nat melee attack"));
            Assert.That(attacks[0].IsMelee, Is.True);
            Assert.That(attacks[0].IsNatural, Is.True);
            Assert.That(attacks[0].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("nat range attack"));
            Assert.That(attacks[1].IsMelee, Is.False);
            Assert.That(attacks[1].IsNatural, Is.True);
            Assert.That(attacks[1].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("effect"));
            Assert.That(attacks[2].Name, Is.EqualTo("melee attack"));
            Assert.That(attacks[2].IsMelee, Is.True);
            Assert.That(attacks[2].IsNatural, Is.False);
            Assert.That(attacks[2].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[2].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[2].DamageEffect, Is.EqualTo("effect"));
            Assert.That(attacks[3].Name, Is.EqualTo("range attack"));
            Assert.That(attacks[3].IsMelee, Is.False);
            Assert.That(attacks[3].IsNatural, Is.False);
            Assert.That(attacks[3].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[3].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[3].DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_Multiple(bool isNatural)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1, FrequencyQuantity = 2, IsPrimary = true, IsMelee = true, IsNatural = isNatural });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(1));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
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
        public void GenerateAttack_Primary_Multiple(int value, int bonus)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1, FrequencyQuantity = 2, IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(1));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(bonus));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_WithSecondary(bool isNatural)
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "primary attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1, FrequencyQuantity = 1, IsPrimary = true, IsMelee = true, IsNatural = isNatural });
            attackSelections.Add(new AttackSelection() { Name = "secondary attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 0.5, FrequencyQuantity = 1, IsPrimary = false, IsMelee = true, IsNatural = isNatural });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary attack"));
            Assert.That(attacks[0].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("secondary attack"));
            Assert.That(attacks[1].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(22550));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_SoleAndMultiple()
        {
            var attackSelections = new List<AttackSelection>();
            attackSelections.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageBonusMultiplier = 1.5, FrequencyQuantity = 1, IsPrimary = true, IsMelee = true, IsNatural = false });
            attackSelections.Add(new AttackSelection() { Name = "nat attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 1, FrequencyQuantity = 2, IsPrimary = true, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(3));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.Null);
            Assert.That(attacks[1].Name, Is.EqualTo("nat attack"));
            Assert.That(attacks[1].DamageRoll, Is.EqualTo("damage"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Secondary()
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 0.5, FrequencyQuantity = 1, IsPrimary = false, IsMelee = true });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
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
        public void GenerateAttack_Secondary(int value, int bonus)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageEffect = "effect", DamageBonusMultiplier = 0.5, FrequencyQuantity = 1, IsPrimary = false, IsMelee = true, IsNatural = true });

            abilities[AbilityConstants.Strength].BaseScore = value;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.EqualTo(bonus));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void GenerateAttackWithAbilityEffect(string ability)
        {
            var attacks = new List<AttackSelection>();
            attacks.Add(new AttackSelection() { Name = "attack", DamageRoll = $"damage", DamageBonusMultiplier = 0.5, DamageEffect = $"1d4 {ability} drain", IsMelee = true });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageRoll, Is.EqualTo("damage"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo($"1d4 {ability} drain"));
        }

        [Test]
        public void ApplyPrimaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.Zero);
        }

        [Test]
        public void ApplySecondaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = false });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SecondaryAttackPenalty, Is.EqualTo(-2));
        }

        [Test]
        public void ApplySpecialAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", DamageRoll = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", DamageRoll = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", DamageRoll = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", DamageRoll = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", DamageRoll = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", DamageRoll = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", DamageRoll = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", DamageRoll = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = FeatConstants.Monster.Multiattack }
            };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);

            foreach (var attack in generatedAttacks)
                Assert.That(attack.SecondaryAttackPenalty, Is.Zero);
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
