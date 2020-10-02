using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Attacks
{
    [TestFixture]
    public class AttackTests
    {
        private Attack attack;

        [SetUp]
        public void Setup()
        {
            attack = new Attack();
        }

        [Test]
        public void AttackInitialized()
        {
            Assert.That(attack.DamageRoll, Is.Empty);
            Assert.That(attack.DamageEffect, Is.Empty);
            Assert.That(attack.IsMelee, Is.False);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.False);
            Assert.That(attack.Name, Is.Empty);
            Assert.That(attack.TotalAttackBonus, Is.Zero);
            Assert.That(attack.BaseAbility, Is.Null);
            Assert.That(attack.BaseAttackBonus, Is.Zero);
            Assert.That(attack.SizeModifier, Is.Zero);
            Assert.That(attack.AttackBonuses, Is.Empty);
            Assert.That(attack.MaxNumberOfAttacks, Is.EqualTo(1));
            Assert.That(attack.AttackType, Is.Empty);
            Assert.That(attack.DamageBonus, Is.Zero);
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Save, Is.Null);
        }

        [TestCase(6, 0, -2)]
        [TestCase(6, 0, -2, -1)]
        [TestCase(6, 0, -2, -2, 3)]
        [TestCase(6, 0, -1)]
        [TestCase(6, 0, -1, -1)]
        [TestCase(6, 0, -1, -2, 3)]
        [TestCase(6, 0, 0)]
        [TestCase(6, 0, 0, -1)]
        [TestCase(6, 0, 0, -2, 3)]
        [TestCase(6, 0, 1)]
        [TestCase(6, 0, 1, -1)]
        [TestCase(6, 0, 1, -2, 3)]
        [TestCase(6, 0, 2)]
        [TestCase(6, 0, 2, -1)]
        [TestCase(6, 0, 2, -2, 3)]
        [TestCase(6, 1, -2)]
        [TestCase(6, 1, -2, -1)]
        [TestCase(6, 1, -2, -2, 3)]
        [TestCase(6, 1, -1)]
        [TestCase(6, 1, -1, -1)]
        [TestCase(6, 1, -1, -2, 3)]
        [TestCase(6, 1, 0)]
        [TestCase(6, 1, 0, -1)]
        [TestCase(6, 1, 0, -2, 3)]
        [TestCase(6, 1, 1)]
        [TestCase(6, 1, 1, -1)]
        [TestCase(6, 1, 1, -2, 3)]
        [TestCase(6, 1, 2)]
        [TestCase(6, 1, 2, -1)]
        [TestCase(6, 1, 2, -2, 3)]
        [TestCase(6, 2, -2)]
        [TestCase(6, 2, -2, -1)]
        [TestCase(6, 2, -2, -2, 3)]
        [TestCase(6, 2, -1)]
        [TestCase(6, 2, -1, -1)]
        [TestCase(6, 2, -1, -2, 3)]
        [TestCase(6, 2, 0)]
        [TestCase(6, 2, 0, -1)]
        [TestCase(6, 2, 0, -2, 3)]
        [TestCase(6, 2, 1)]
        [TestCase(6, 2, 1, -1)]
        [TestCase(6, 2, 1, -2, 3)]
        [TestCase(6, 2, 2)]
        [TestCase(6, 2, 2, -1)]
        [TestCase(6, 2, 2, -2, 3)]
        [TestCase(8, 0, -2)]
        [TestCase(8, 0, -2, -1)]
        [TestCase(8, 0, -2, -2, 3)]
        [TestCase(8, 0, -1)]
        [TestCase(8, 0, -1, -1)]
        [TestCase(8, 0, -1, -2, 3)]
        [TestCase(8, 0, 0)]
        [TestCase(8, 0, 0, -1)]
        [TestCase(8, 0, 0, -2, 3)]
        [TestCase(8, 0, 1)]
        [TestCase(8, 0, 1, -1)]
        [TestCase(8, 0, 1, -2, 3)]
        [TestCase(8, 0, 2)]
        [TestCase(8, 0, 2, -1)]
        [TestCase(8, 0, 2, -2, 3)]
        [TestCase(8, 1, -2)]
        [TestCase(8, 1, -2, -1)]
        [TestCase(8, 1, -2, -2, 3)]
        [TestCase(8, 1, -1)]
        [TestCase(8, 1, -1, -1)]
        [TestCase(8, 1, -1, -2, 3)]
        [TestCase(8, 1, 0)]
        [TestCase(8, 1, 0, -1)]
        [TestCase(8, 1, 0, -2, 3)]
        [TestCase(8, 1, 1)]
        [TestCase(8, 1, 1, -1)]
        [TestCase(8, 1, 1, -2, 3)]
        [TestCase(8, 1, 2)]
        [TestCase(8, 1, 2, -1)]
        [TestCase(8, 1, 2, -2, 3)]
        [TestCase(8, 2, -2)]
        [TestCase(8, 2, -2, -1)]
        [TestCase(8, 2, -2, -2, 3)]
        [TestCase(8, 2, -1)]
        [TestCase(8, 2, -1, -1)]
        [TestCase(8, 2, -1, -2, 3)]
        [TestCase(8, 2, 0)]
        [TestCase(8, 2, 0, -1)]
        [TestCase(8, 2, 0, -2, 3)]
        [TestCase(8, 2, 1)]
        [TestCase(8, 2, 1, -1)]
        [TestCase(8, 2, 1, -2, 3)]
        [TestCase(8, 2, 2)]
        [TestCase(8, 2, 2, -1)]
        [TestCase(8, 2, 2, -2, 3)]
        [TestCase(10, 0, -2)]
        [TestCase(10, 0, -2, -1)]
        [TestCase(10, 0, -2, -2, 3)]
        [TestCase(10, 0, -1)]
        [TestCase(10, 0, -1, -1)]
        [TestCase(10, 0, -1, -2, 3)]
        [TestCase(10, 0, 0)]
        [TestCase(10, 0, 0, -1)]
        [TestCase(10, 0, 0, -2, 3)]
        [TestCase(10, 0, 1)]
        [TestCase(10, 0, 1, -1)]
        [TestCase(10, 0, 1, -2, 3)]
        [TestCase(10, 0, 2)]
        [TestCase(10, 0, 2, -1)]
        [TestCase(10, 0, 2, -2, 3)]
        [TestCase(10, 1, -2)]
        [TestCase(10, 1, -2, -1)]
        [TestCase(10, 1, -2, -2, 3)]
        [TestCase(10, 1, -1)]
        [TestCase(10, 1, -1, -1)]
        [TestCase(10, 1, -1, -2, 3)]
        [TestCase(10, 1, 0)]
        [TestCase(10, 1, 0, -1)]
        [TestCase(10, 1, 0, -2, 3)]
        [TestCase(10, 1, 1)]
        [TestCase(10, 1, 1, -1)]
        [TestCase(10, 1, 1, -2, 3)]
        [TestCase(10, 1, 2)]
        [TestCase(10, 1, 2, -1)]
        [TestCase(10, 1, 2, -2, 3)]
        [TestCase(10, 2, -2)]
        [TestCase(10, 2, -2, -1)]
        [TestCase(10, 2, -2, -2, 3)]
        [TestCase(10, 2, -1)]
        [TestCase(10, 2, -1, -1)]
        [TestCase(10, 2, -1, -2, 3)]
        [TestCase(10, 2, 0)]
        [TestCase(10, 2, 0, -1)]
        [TestCase(10, 2, 0, -2, 3)]
        [TestCase(10, 2, 1)]
        [TestCase(10, 2, 1, -1)]
        [TestCase(10, 2, 1, -2, 3)]
        [TestCase(10, 2, 2)]
        [TestCase(10, 2, 2, -1)]
        [TestCase(10, 2, 2, -2, 3)]
        [TestCase(12, 0, -2)]
        [TestCase(12, 0, -2, -1)]
        [TestCase(12, 0, -2, -2, 3)]
        [TestCase(12, 0, -1)]
        [TestCase(12, 0, -1, -1)]
        [TestCase(12, 0, -1, -2, 3)]
        [TestCase(12, 0, 0)]
        [TestCase(12, 0, 0, -1)]
        [TestCase(12, 0, 0, -2, 3)]
        [TestCase(12, 0, 1)]
        [TestCase(12, 0, 1, -1)]
        [TestCase(12, 0, 1, -2, 3)]
        [TestCase(12, 0, 2)]
        [TestCase(12, 0, 2, -1)]
        [TestCase(12, 0, 2, -2, 3)]
        [TestCase(12, 1, -2)]
        [TestCase(12, 1, -2, -1)]
        [TestCase(12, 1, -2, -2, 3)]
        [TestCase(12, 1, -1)]
        [TestCase(12, 1, -1, -1)]
        [TestCase(12, 1, -1, -2, 3)]
        [TestCase(12, 1, 0)]
        [TestCase(12, 1, 0, -1)]
        [TestCase(12, 1, 0, -2, 3)]
        [TestCase(12, 1, 1)]
        [TestCase(12, 1, 1, -1)]
        [TestCase(12, 1, 1, -2, 3)]
        [TestCase(12, 1, 2)]
        [TestCase(12, 1, 2, -1)]
        [TestCase(12, 1, 2, -2, 3)]
        [TestCase(12, 2, -2)]
        [TestCase(12, 2, -2, -1)]
        [TestCase(12, 2, -2, -2, 3)]
        [TestCase(12, 2, -1)]
        [TestCase(12, 2, -1, -1)]
        [TestCase(12, 2, -1, -2, 3)]
        [TestCase(12, 2, 0)]
        [TestCase(12, 2, 0, -1)]
        [TestCase(12, 2, 0, -2, 3)]
        [TestCase(12, 2, 1)]
        [TestCase(12, 2, 1, -1)]
        [TestCase(12, 2, 1, -2, 3)]
        [TestCase(12, 2, 2)]
        [TestCase(12, 2, 2, -1)]
        [TestCase(12, 2, 2, -2, 3)]
        [TestCase(14, 0, -2)]
        [TestCase(14, 0, -2, -1)]
        [TestCase(14, 0, -2, -2, 3)]
        [TestCase(14, 0, -1)]
        [TestCase(14, 0, -1, -1)]
        [TestCase(14, 0, -1, -2, 3)]
        [TestCase(14, 0, 0)]
        [TestCase(14, 0, 0, -1)]
        [TestCase(14, 0, 0, -2, 3)]
        [TestCase(14, 0, 1)]
        [TestCase(14, 0, 1, -1)]
        [TestCase(14, 0, 1, -2, 3)]
        [TestCase(14, 0, 2)]
        [TestCase(14, 0, 2, -1)]
        [TestCase(14, 0, 2, -2, 3)]
        [TestCase(14, 1, -2)]
        [TestCase(14, 1, -2, -1)]
        [TestCase(14, 1, -2, -2, 3)]
        [TestCase(14, 1, -1)]
        [TestCase(14, 1, -1, -1)]
        [TestCase(14, 1, -1, -2, 3)]
        [TestCase(14, 1, 0)]
        [TestCase(14, 1, 0, -1)]
        [TestCase(14, 1, 0, -2, 3)]
        [TestCase(14, 1, 1)]
        [TestCase(14, 1, 1, -1)]
        [TestCase(14, 1, 1, -2, 3)]
        [TestCase(14, 1, 2)]
        [TestCase(14, 1, 2, -1)]
        [TestCase(14, 1, 2, -2, 3)]
        [TestCase(14, 2, -2)]
        [TestCase(14, 2, -2, -1)]
        [TestCase(14, 2, -2, -2, 3)]
        [TestCase(14, 2, -1)]
        [TestCase(14, 2, -1, -1)]
        [TestCase(14, 2, -1, -2, 3)]
        [TestCase(14, 2, 0)]
        [TestCase(14, 2, 0, -1)]
        [TestCase(14, 2, 0, -2, 3)]
        [TestCase(14, 2, 1)]
        [TestCase(14, 2, 1, -1)]
        [TestCase(14, 2, 1, -2, 3)]
        [TestCase(14, 2, 2)]
        [TestCase(14, 2, 2, -1)]
        [TestCase(14, 2, 2, -2, 3)]
        [TestCase(9266, 90210, 42, 600)]
        [TestCase(1337, 1336, 96, 783)]
        [TestCase(8245, 0, 1, 2, 3, -4, 5)]
        public void TotalAttackBonus(int abilityValue, int attackBonus, int sizeModifier, params int[] bonuses)
        {
            attack.BaseAbility = new Ability("ability");
            attack.BaseAbility.BaseScore = abilityValue;
            attack.BaseAttackBonus = attackBonus;
            attack.SizeModifier = sizeModifier;
            attack.AttackBonuses.AddRange(bonuses);

            Assert.That(attack.TotalAttackBonus, Is.EqualTo(attackBonus + attack.BaseAbility.Modifier + sizeModifier + bonuses.Sum()));
        }

        [TestCase(0, -2)]
        [TestCase(0, -2, -1)]
        [TestCase(0, -2, -2, 3)]
        [TestCase(0, -1)]
        [TestCase(0, -1, -1)]
        [TestCase(0, -1, -2, 3)]
        [TestCase(0, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(0, 0, -2, 3)]
        [TestCase(0, 1)]
        [TestCase(0, 1, -1)]
        [TestCase(0, 1, -2, 3)]
        [TestCase(0, 2)]
        [TestCase(0, 2, -1)]
        [TestCase(0, 2, -2, 3)]
        [TestCase(1, -2)]
        [TestCase(1, -2, -1)]
        [TestCase(1, -2, -2, 3)]
        [TestCase(1, -1)]
        [TestCase(1, -1, -1)]
        [TestCase(1, -1, -2, 3)]
        [TestCase(1, 0)]
        [TestCase(1, 0, -1)]
        [TestCase(1, 0, -2, 3)]
        [TestCase(1, 1)]
        [TestCase(1, 1, -1)]
        [TestCase(1, 1, -2, 3)]
        [TestCase(1, 2)]
        [TestCase(1, 2, -1)]
        [TestCase(1, 2, -2, 3)]
        [TestCase(2, -2)]
        [TestCase(2, -2, -1)]
        [TestCase(2, -2, -2, 3)]
        [TestCase(2, -1)]
        [TestCase(2, -1, -1)]
        [TestCase(2, -1, -2, 3)]
        [TestCase(2, 0)]
        [TestCase(2, 0, -1)]
        [TestCase(2, 0, -2, 3)]
        [TestCase(2, 1)]
        [TestCase(2, 1, -1)]
        [TestCase(2, 1, -2, 3)]
        [TestCase(2, 2)]
        [TestCase(2, 2, -1)]
        [TestCase(2, 2, -2, 3)]
        [TestCase(9266, 90210, 42, 600, -1337)]
        [TestCase(1336, 96, -783, 8245)]
        public void TotalAttackBonusWithNullAbility(int attackBonus, int sizeModifier, params int[] bonuses)
        {
            attack.BaseAttackBonus = attackBonus;
            attack.SizeModifier = sizeModifier;
            attack.AttackBonuses.AddRange(bonuses);

            Assert.That(attack.TotalAttackBonus, Is.EqualTo(attackBonus + sizeModifier + bonuses.Sum()));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6, 1)]
        [TestCase(7, 7, 2)]
        [TestCase(8, 8, 3)]
        [TestCase(9, 9, 4)]
        [TestCase(10, 10, 5)]
        [TestCase(11, 11, 6, 1)]
        [TestCase(12, 12, 7, 2)]
        [TestCase(13, 13, 8, 3)]
        [TestCase(14, 14, 9, 4)]
        [TestCase(15, 15, 10, 5)]
        [TestCase(16, 16, 11, 6, 1)]
        [TestCase(17, 17, 12, 7, 2)]
        [TestCase(18, 18, 13, 8, 3)]
        [TestCase(19, 19, 14, 9, 4)]
        [TestCase(20, 20, 15, 10, 5)]
        [TestCase(21, 21, 16, 11, 6)]
        [TestCase(42, 42, 37, 32, 27)]
        [TestCase(9266, 9266, 9261, 9256, 9251)]
        public void AttackBonuses_Primary_Manufactured(int baseAttackBonus, params int[] bonuses)
        {
            attack.BaseAttackBonus = baseAttackBonus;
            attack.IsPrimary = true;
            attack.IsNatural = false;
            attack.MaxNumberOfAttacks = 4;

            Assert.That(attack.FullAttackBonuses, Is.EqualTo(bonuses));
        }

        [TestCase(0, 1, -5)]
        [TestCase(0, 2, -5)]
        [TestCase(0, 3, -5)]
        [TestCase(1, 1, -4)]
        [TestCase(1, 2, -4)]
        [TestCase(1, 3, -4)]
        [TestCase(2, 1, -3)]
        [TestCase(2, 2, -3)]
        [TestCase(2, 3, -3)]
        [TestCase(3, 1, -2)]
        [TestCase(3, 2, -2)]
        [TestCase(3, 3, -2)]
        [TestCase(4, 1, -1)]
        [TestCase(4, 2, -1)]
        [TestCase(4, 3, -1)]
        [TestCase(5, 1, 0)]
        [TestCase(5, 2, 0)]
        [TestCase(5, 3, 0)]
        [TestCase(6, 1, 1)]
        [TestCase(6, 2, 1, -4)]
        [TestCase(6, 3, 1, -4)]
        [TestCase(7, 1, 2)]
        [TestCase(7, 2, 2, -3)]
        [TestCase(7, 3, 2, -3)]
        [TestCase(8, 1, 3)]
        [TestCase(8, 2, 3, -2)]
        [TestCase(8, 3, 3, -2)]
        [TestCase(9, 1, 4)]
        [TestCase(9, 2, 4, -1)]
        [TestCase(9, 3, 4, -1)]
        [TestCase(10, 1, 5)]
        [TestCase(10, 2, 5, 0)]
        [TestCase(10, 3, 5, 0)]
        [TestCase(11, 1, 6)]
        [TestCase(11, 2, 6, 1)]
        [TestCase(11, 3, 6, 1, -4)]
        [TestCase(12, 1, 7)]
        [TestCase(12, 2, 7, 2)]
        [TestCase(12, 3, 7, 2, -3)]
        [TestCase(13, 1, 8)]
        [TestCase(13, 2, 8, 3)]
        [TestCase(13, 3, 8, 3, -2)]
        [TestCase(14, 1, 9)]
        [TestCase(14, 2, 9, 4)]
        [TestCase(14, 3, 9, 4, -1)]
        [TestCase(15, 1, 10)]
        [TestCase(15, 2, 10, 5)]
        [TestCase(15, 3, 10, 5, 0)]
        [TestCase(16, 1, 11)]
        [TestCase(16, 2, 11, 6)]
        [TestCase(16, 3, 11, 6, 1)]
        [TestCase(17, 1, 12)]
        [TestCase(17, 2, 12, 7)]
        [TestCase(17, 3, 12, 7, 2)]
        [TestCase(18, 1, 13)]
        [TestCase(18, 2, 13, 8)]
        [TestCase(18, 3, 13, 8, 3)]
        [TestCase(19, 1, 14)]
        [TestCase(19, 2, 14, 9)]
        [TestCase(19, 3, 14, 9, 4)]
        [TestCase(20, 1, 15)]
        [TestCase(20, 2, 15, 10)]
        [TestCase(20, 3, 15, 10, 5)]
        [TestCase(21, 1, 16)]
        [TestCase(21, 2, 16, 11)]
        [TestCase(21, 3, 16, 11, 6)]
        [TestCase(42, 1, 37)]
        [TestCase(42, 2, 37, 32)]
        [TestCase(42, 3, 37, 32, 27)]
        [TestCase(9266, 1, 9261)]
        [TestCase(9266, 2, 9261, 9256)]
        [TestCase(9266, 3, 9261, 9256, 9251)]
        public void AttackBonuses_Secondary_Manufactured(int baseAttackBonus, int numberOfAttacks, params int[] bonuses)
        {
            attack.BaseAttackBonus = baseAttackBonus;
            attack.IsPrimary = false;
            attack.IsNatural = false;
            attack.AttackBonuses.Add(-5);
            attack.MaxNumberOfAttacks = numberOfAttacks;

            Assert.That(attack.FullAttackBonuses, Is.EqualTo(bonuses));
        }

        [TestCase(0, 0)]
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
        [TestCase(21, 21)]
        [TestCase(42, 42)]
        [TestCase(9266, 9266)]
        public void AttackBonuses_Primary_Natural(int baseAttackBonus, params int[] bonuses)
        {
            attack.BaseAttackBonus = baseAttackBonus;
            attack.IsPrimary = true;
            attack.IsNatural = true;
            attack.MaxNumberOfAttacks = 1;

            Assert.That(attack.FullAttackBonuses, Is.EqualTo(bonuses));
        }

        [TestCase(0, -5)]
        [TestCase(1, -4)]
        [TestCase(2, -3)]
        [TestCase(3, -2)]
        [TestCase(4, -1)]
        [TestCase(5, 0)]
        [TestCase(6, 1)]
        [TestCase(7, 2)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 6)]
        [TestCase(12, 7)]
        [TestCase(13, 8)]
        [TestCase(14, 9)]
        [TestCase(15, 10)]
        [TestCase(16, 11)]
        [TestCase(17, 12)]
        [TestCase(18, 13)]
        [TestCase(19, 14)]
        [TestCase(20, 15)]
        [TestCase(21, 16)]
        [TestCase(42, 37)]
        [TestCase(9266, 9261)]
        public void AttackBonuses_Secondary_Natural(int baseAttackBonus, params int[] bonuses)
        {
            attack.BaseAttackBonus = baseAttackBonus;
            attack.IsPrimary = false;
            attack.IsNatural = true;
            attack.AttackBonuses.Add(-5);
            attack.MaxNumberOfAttacks = 1;

            Assert.That(attack.FullAttackBonuses, Is.EqualTo(bonuses));
        }

        [Test]
        public void AttackDamage()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = 0;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.Damage, Is.EqualTo("1d8"));
        }

        [Test]
        public void AttackDamage_WithPositiveDamageBonus()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = 1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.Damage, Is.EqualTo("1d8+1"));
        }

        [Test]
        public void AttackDamage_WithNegativeDamageBonus()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = -1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.Damage, Is.EqualTo("1d8-1"));
        }

        [Test]
        public void AttackDamage_WithEffect()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = 0;
            attack.DamageEffect = "poison";

            Assert.That(attack.Damage, Is.EqualTo("1d8 plus poison"));
        }

        [Test]
        public void AttackDamage_WithEffectAndPositiveBonus()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = 1;
            attack.DamageEffect = "poison";

            Assert.That(attack.Damage, Is.EqualTo("1d8+1 plus poison"));
        }

        [Test]
        public void AttackDamage_WithEffectAndNegativeBonus()
        {
            attack.DamageRoll = "1d8";
            attack.DamageBonus = -2;
            attack.DamageEffect = "poison";

            Assert.That(attack.Damage, Is.EqualTo("1d8-2 plus poison"));
        }

        [Test]
        public void AttackDamage_WithOnlyEffect()
        {
            attack.DamageRoll = string.Empty;
            attack.DamageBonus = 0;
            attack.DamageEffect = "1d4 Wisdom drain";

            Assert.That(attack.Damage, Is.EqualTo("1d4 Wisdom drain"));
        }
    }
}