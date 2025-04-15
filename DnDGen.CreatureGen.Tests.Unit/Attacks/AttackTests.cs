using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Attacks
{
    [TestFixture]
    public class AttackTests
    {
        private Attack attack;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            attack = new Attack();
            abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };
        }

        [Test]
        public void AttackInitialized()
        {
            Assert.That(attack.Damages, Is.Empty);
            Assert.That(attack.DamageSummary, Is.Empty);
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
        public void DamageSummary_NoDamages()
        {
            attack.Damages.Clear();

            Assert.That(attack.DamageSummary, Is.Empty);
        }

        [Test]
        public void DamageSummary_Damage()
        {
            attack.Damages.Add(new Damage { Roll = "9266d90210", Type = "emotional" });

            Assert.That(attack.DamageSummary, Is.EqualTo("9266d90210 emotional"));
        }

        [Test]
        public void DamageSummary_Damage_WithCondition()
        {
            attack.Damages.Add(new Damage { Roll = "9266d90210", Type = "emotional", Condition = "on occasion" });

            Assert.That(attack.DamageSummary, Is.EqualTo("9266d90210 emotional (on occasion)"));
        }

        [Test]
        public void DamageSummary_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "9266d90210", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "42d600", Type = "spiritual" });
            attack.Damages.Add(new Damage { Roll = "1337d1336", Type = "psychic", Condition = "to stupid people" });

            Assert.That(attack.DamageSummary, Is.EqualTo("9266d90210 emotional + 42d600 spiritual + 1337d1336 psychic (to stupid people)"));
        }

        [Test]
        public void DamageSummary_AttackDamage()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = 0;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8 emotional"));
        }

        [Test]
        public void DamageSummary_AttackDamage_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = 0;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8 emotional + 2d6 spiritual"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithPositiveDamageBonus()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = 1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8+1 emotional"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithPositiveDamageBonus_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = 1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8+1 emotional + 2d6 spiritual"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithPositiveDamageBonus_MultipleSameDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "spiritual" });
            attack.DamageBonus = 1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8+1 emotional + 1d8 spiritual"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithNegativeDamageBonus()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = -1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8-1 emotional"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithNegativeDamageBonus_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = -1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8-1 emotional + 2d6 spiritual"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithNegativeDamageBonus_MultipleSameDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "spiritual" });
            attack.DamageBonus = -1;
            attack.DamageEffect = string.Empty;

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8-1 emotional + 1d8 spiritual"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffect()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = 0;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8 emotional plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffect_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = 0;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8 emotional + 2d6 spiritual plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffectAndPositiveBonus()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = 1;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8+1 emotional plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffectAndPositiveBonus_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = 1;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8+1 emotional + 2d6 spiritual plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffectAndNegativeBonus()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.DamageBonus = -2;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8-2 emotional plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithEffectAndNegativeBonus_MultipleDamages()
        {
            attack.Damages.Add(new Damage { Roll = "1d8", Type = "emotional" });
            attack.Damages.Add(new Damage { Roll = "2d6", Type = "spiritual" });
            attack.DamageBonus = -2;
            attack.DamageEffect = "poison";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d8-2 emotional + 2d6 spiritual plus poison"));
        }

        [Test]
        public void DamageSummary_AttackDamage_WithOnlyEffect()
        {
            attack.Damages.Clear();
            attack.DamageBonus = 0;
            attack.DamageEffect = "1d4 Wisdom drain";

            Assert.That(attack.DamageSummary, Is.EqualTo("1d4 Wisdom drain"));
        }

        private AttackDataSelection GetTestSelection() => new()
        {
            Name = "attack",
            Damages =
            [
                new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
            ],
            DamageEffect = "damage effect",
            AttackType = "attack type",
            FrequencyQuantity = 600,
            FrequencyTimePeriod = "time period",
            IsMelee = true,
            IsNatural = false,
            IsPrimary = true,
            IsSpecial = false,
            Save = "save",
            SaveAbility = abilities.Keys.First(),
            SaveDcBonus = 1337
        };

        [Test]
        public void From_ReturnsAttack()
        {
            var selection = GetTestSelection();

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.EqualTo(true));
            Assert.That(attack.IsNatural, Is.EqualTo(false));
            Assert.That(attack.IsPrimary, Is.EqualTo(true));
            Assert.That(attack.IsSpecial, Is.EqualTo(false));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 300 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Melee(bool melee)
        {
            var selection = GetTestSelection();
            selection.IsMelee = melee;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.IsMelee, Is.EqualTo(melee));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Natural(bool natural)
        {
            var selection = GetTestSelection();
            selection.IsNatural = natural;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.IsNatural, Is.EqualTo(natural));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Primary(bool primary)
        {
            var selection = GetTestSelection();
            selection.IsPrimary = primary;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.IsPrimary, Is.EqualTo(primary));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Special(bool special)
        {
            var selection = GetTestSelection();
            selection.IsSpecial = special;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.IsSpecial, Is.EqualTo(special));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void From_ReturnsAttack_Save_WithSaveAbility_Natural(string saveAbility)
        {
            var selection = GetTestSelection();
            selection.SaveAbility = saveAbility;
            selection.IsNatural = true;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337 + 9266 / 2)
                .And.EqualTo(10 + selection.SaveDcBonus + 9266 / 2));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[saveAbility]));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void From_ReturnsAttack_Save_WithSaveAbility_Unatural(string saveAbility)
        {
            var selection = GetTestSelection();
            selection.SaveAbility = saveAbility;
            selection.IsNatural = false;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337)
                .And.EqualTo(10 + selection.SaveDcBonus));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void From_ReturnsAttack_Save_WithoutSaveAbility_Null()
        {
            var selection = GetTestSelection();
            selection.SaveAbility = null;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void From_ReturnsAttack_Save_WithoutSave_Empty()
        {
            var selection = GetTestSelection();
            selection.Save = string.Empty;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
            Assert.That(attack.Save.Save, Is.Empty);
        }

        [Test]
        public void From_ReturnsAttack_Save_WithoutSave_Null()
        {
            var selection = GetTestSelection();
            selection.Save = null;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
            Assert.That(attack.Save.Save, Is.Empty);
        }

        [Test]
        public void From_ReturnsAttack_Save_WithoutSaveAbility_Empty()
        {
            var selection = GetTestSelection();
            selection.SaveAbility = string.Empty;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void From_ReturnsAttack_NoSave_Null()
        {
            var selection = GetTestSelection();
            selection.SaveAbility = null;
            selection.Save = null;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Null);
        }

        [Test]
        public void From_ReturnsAttack_NoSave_Empty()
        {
            var selection = GetTestSelection();
            selection.SaveAbility = null;
            selection.Save = string.Empty;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Save, Is.Null);
        }

        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 96)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(2, 96)]
        [TestCase(3, 0)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(3, 96)]
        [TestCase(4, 0)]
        [TestCase(4, 1)]
        [TestCase(4, 2)]
        [TestCase(4, 96)]
        [TestCase(5, 0)]
        [TestCase(5, 1)]
        [TestCase(5, 2)]
        [TestCase(5, 96)]
        [TestCase(6, 0)]
        [TestCase(6, 1)]
        [TestCase(6, 2)]
        [TestCase(6, 96)]
        [TestCase(7, 0)]
        [TestCase(7, 1)]
        [TestCase(7, 2)]
        [TestCase(7, 96)]
        [TestCase(8, 0)]
        [TestCase(8, 1)]
        [TestCase(8, 2)]
        [TestCase(8, 96)]
        [TestCase(9, 0)]
        [TestCase(9, 1)]
        [TestCase(9, 2)]
        [TestCase(9, 96)]
        [TestCase(10, 0)]
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        [TestCase(10, 96)]
        [TestCase(11, 0)]
        [TestCase(11, 1)]
        [TestCase(11, 2)]
        [TestCase(11, 96)]
        [TestCase(12, 0)]
        [TestCase(12, 1)]
        [TestCase(12, 2)]
        [TestCase(12, 96)]
        [TestCase(13, 0)]
        [TestCase(13, 1)]
        [TestCase(13, 2)]
        [TestCase(13, 96)]
        [TestCase(14, 0)]
        [TestCase(14, 1)]
        [TestCase(14, 2)]
        [TestCase(14, 96)]
        [TestCase(15, 0)]
        [TestCase(15, 1)]
        [TestCase(15, 2)]
        [TestCase(15, 96)]
        [TestCase(16, 0)]
        [TestCase(16, 1)]
        [TestCase(16, 2)]
        [TestCase(16, 96)]
        [TestCase(17, 0)]
        [TestCase(17, 1)]
        [TestCase(17, 2)]
        [TestCase(17, 96)]
        [TestCase(18, 0)]
        [TestCase(18, 1)]
        [TestCase(18, 2)]
        [TestCase(18, 96)]
        [TestCase(19, 0)]
        [TestCase(19, 1)]
        [TestCase(19, 2)]
        [TestCase(19, 96)]
        [TestCase(20, 0)]
        [TestCase(20, 1)]
        [TestCase(20, 2)]
        [TestCase(20, 96)]
        [TestCase(21, 0)]
        [TestCase(21, 1)]
        [TestCase(21, 2)]
        [TestCase(21, 96)]
        [TestCase(22, 0)]
        [TestCase(22, 1)]
        [TestCase(22, 2)]
        [TestCase(22, 96)]
        [TestCase(23, 0)]
        [TestCase(23, 1)]
        [TestCase(23, 2)]
        [TestCase(23, 96)]
        [TestCase(24, 0)]
        [TestCase(24, 1)]
        [TestCase(24, 2)]
        [TestCase(24, 96)]
        [TestCase(25, 0)]
        [TestCase(25, 1)]
        [TestCase(25, 2)]
        [TestCase(25, 96)]
        [TestCase(26, 0)]
        [TestCase(26, 1)]
        [TestCase(26, 2)]
        [TestCase(26, 96)]
        [TestCase(27, 0)]
        [TestCase(27, 1)]
        [TestCase(27, 2)]
        [TestCase(27, 96)]
        [TestCase(28, 0)]
        [TestCase(28, 1)]
        [TestCase(28, 2)]
        [TestCase(28, 96)]
        [TestCase(29, 0)]
        [TestCase(29, 1)]
        [TestCase(29, 2)]
        [TestCase(29, 96)]
        [TestCase(30, 0)]
        [TestCase(30, 1)]
        [TestCase(30, 2)]
        [TestCase(30, 96)]
        [TestCase(31, 0)]
        [TestCase(31, 1)]
        [TestCase(31, 2)]
        [TestCase(31, 96)]
        [TestCase(32, 0)]
        [TestCase(32, 1)]
        [TestCase(32, 2)]
        [TestCase(32, 96)]
        [TestCase(33, 0)]
        [TestCase(33, 1)]
        [TestCase(33, 2)]
        [TestCase(33, 96)]
        [TestCase(34, 0)]
        [TestCase(34, 1)]
        [TestCase(34, 2)]
        [TestCase(34, 96)]
        [TestCase(35, 0)]
        [TestCase(35, 1)]
        [TestCase(35, 2)]
        [TestCase(35, 96)]
        [TestCase(36, 0)]
        [TestCase(36, 1)]
        [TestCase(36, 2)]
        [TestCase(36, 96)]
        [TestCase(37, 0)]
        [TestCase(37, 1)]
        [TestCase(37, 2)]
        [TestCase(37, 96)]
        [TestCase(38, 0)]
        [TestCase(38, 1)]
        [TestCase(38, 2)]
        [TestCase(38, 96)]
        [TestCase(39, 0)]
        [TestCase(39, 1)]
        [TestCase(39, 2)]
        [TestCase(39, 96)]
        [TestCase(40, 0)]
        [TestCase(40, 1)]
        [TestCase(40, 2)]
        [TestCase(40, 96)]
        [TestCase(41, 0)]
        [TestCase(41, 1)]
        [TestCase(41, 2)]
        [TestCase(41, 96)]
        [TestCase(42, 0)]
        [TestCase(42, 1)]
        [TestCase(42, 2)]
        [TestCase(42, 96)]
        [TestCase(43, 0)]
        [TestCase(43, 1)]
        [TestCase(43, 2)]
        [TestCase(43, 96)]
        [TestCase(44, 0)]
        [TestCase(44, 1)]
        [TestCase(44, 2)]
        [TestCase(44, 96)]
        [TestCase(45, 0)]
        [TestCase(45, 1)]
        [TestCase(45, 2)]
        [TestCase(45, 96)]
        [TestCase(46, 0)]
        [TestCase(46, 1)]
        [TestCase(46, 2)]
        [TestCase(46, 96)]
        [TestCase(47, 0)]
        [TestCase(47, 1)]
        [TestCase(47, 2)]
        [TestCase(47, 96)]
        [TestCase(48, 0)]
        [TestCase(48, 1)]
        [TestCase(48, 2)]
        [TestCase(48, 96)]
        [TestCase(49, 0)]
        [TestCase(49, 1)]
        [TestCase(49, 2)]
        [TestCase(49, 96)]
        [TestCase(50, 0)]
        [TestCase(50, 1)]
        [TestCase(50, 2)]
        [TestCase(50, 96)]
        public void From_ReturnsAttack_Save_WithBonus_Natural(int hitDice, int bonus)
        {
            var selection = GetTestSelection();
            selection.SaveDcBonus = bonus;
            selection.IsNatural = true;

            var attack = Attack.From(selection, abilities, hitDice, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + bonus + hitDice / 2));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities.Values.First()));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 96)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(2, 96)]
        [TestCase(3, 0)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(3, 96)]
        [TestCase(4, 0)]
        [TestCase(4, 1)]
        [TestCase(4, 2)]
        [TestCase(4, 96)]
        [TestCase(5, 0)]
        [TestCase(5, 1)]
        [TestCase(5, 2)]
        [TestCase(5, 96)]
        [TestCase(6, 0)]
        [TestCase(6, 1)]
        [TestCase(6, 2)]
        [TestCase(6, 96)]
        [TestCase(7, 0)]
        [TestCase(7, 1)]
        [TestCase(7, 2)]
        [TestCase(7, 96)]
        [TestCase(8, 0)]
        [TestCase(8, 1)]
        [TestCase(8, 2)]
        [TestCase(8, 96)]
        [TestCase(9, 0)]
        [TestCase(9, 1)]
        [TestCase(9, 2)]
        [TestCase(9, 96)]
        [TestCase(10, 0)]
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        [TestCase(10, 96)]
        [TestCase(11, 0)]
        [TestCase(11, 1)]
        [TestCase(11, 2)]
        [TestCase(11, 96)]
        [TestCase(12, 0)]
        [TestCase(12, 1)]
        [TestCase(12, 2)]
        [TestCase(12, 96)]
        [TestCase(13, 0)]
        [TestCase(13, 1)]
        [TestCase(13, 2)]
        [TestCase(13, 96)]
        [TestCase(14, 0)]
        [TestCase(14, 1)]
        [TestCase(14, 2)]
        [TestCase(14, 96)]
        [TestCase(15, 0)]
        [TestCase(15, 1)]
        [TestCase(15, 2)]
        [TestCase(15, 96)]
        [TestCase(16, 0)]
        [TestCase(16, 1)]
        [TestCase(16, 2)]
        [TestCase(16, 96)]
        [TestCase(17, 0)]
        [TestCase(17, 1)]
        [TestCase(17, 2)]
        [TestCase(17, 96)]
        [TestCase(18, 0)]
        [TestCase(18, 1)]
        [TestCase(18, 2)]
        [TestCase(18, 96)]
        [TestCase(19, 0)]
        [TestCase(19, 1)]
        [TestCase(19, 2)]
        [TestCase(19, 96)]
        [TestCase(20, 0)]
        [TestCase(20, 1)]
        [TestCase(20, 2)]
        [TestCase(20, 96)]
        [TestCase(21, 0)]
        [TestCase(21, 1)]
        [TestCase(21, 2)]
        [TestCase(21, 96)]
        [TestCase(22, 0)]
        [TestCase(22, 1)]
        [TestCase(22, 2)]
        [TestCase(22, 96)]
        [TestCase(23, 0)]
        [TestCase(23, 1)]
        [TestCase(23, 2)]
        [TestCase(23, 96)]
        [TestCase(24, 0)]
        [TestCase(24, 1)]
        [TestCase(24, 2)]
        [TestCase(24, 96)]
        [TestCase(25, 0)]
        [TestCase(25, 1)]
        [TestCase(25, 2)]
        [TestCase(25, 96)]
        [TestCase(26, 0)]
        [TestCase(26, 1)]
        [TestCase(26, 2)]
        [TestCase(26, 96)]
        [TestCase(27, 0)]
        [TestCase(27, 1)]
        [TestCase(27, 2)]
        [TestCase(27, 96)]
        [TestCase(28, 0)]
        [TestCase(28, 1)]
        [TestCase(28, 2)]
        [TestCase(28, 96)]
        [TestCase(29, 0)]
        [TestCase(29, 1)]
        [TestCase(29, 2)]
        [TestCase(29, 96)]
        [TestCase(30, 0)]
        [TestCase(30, 1)]
        [TestCase(30, 2)]
        [TestCase(30, 96)]
        [TestCase(31, 0)]
        [TestCase(31, 1)]
        [TestCase(31, 2)]
        [TestCase(31, 96)]
        [TestCase(32, 0)]
        [TestCase(32, 1)]
        [TestCase(32, 2)]
        [TestCase(32, 96)]
        [TestCase(33, 0)]
        [TestCase(33, 1)]
        [TestCase(33, 2)]
        [TestCase(33, 96)]
        [TestCase(34, 0)]
        [TestCase(34, 1)]
        [TestCase(34, 2)]
        [TestCase(34, 96)]
        [TestCase(35, 0)]
        [TestCase(35, 1)]
        [TestCase(35, 2)]
        [TestCase(35, 96)]
        [TestCase(36, 0)]
        [TestCase(36, 1)]
        [TestCase(36, 2)]
        [TestCase(36, 96)]
        [TestCase(37, 0)]
        [TestCase(37, 1)]
        [TestCase(37, 2)]
        [TestCase(37, 96)]
        [TestCase(38, 0)]
        [TestCase(38, 1)]
        [TestCase(38, 2)]
        [TestCase(38, 96)]
        [TestCase(39, 0)]
        [TestCase(39, 1)]
        [TestCase(39, 2)]
        [TestCase(39, 96)]
        [TestCase(40, 0)]
        [TestCase(40, 1)]
        [TestCase(40, 2)]
        [TestCase(40, 96)]
        [TestCase(41, 0)]
        [TestCase(41, 1)]
        [TestCase(41, 2)]
        [TestCase(41, 96)]
        [TestCase(42, 0)]
        [TestCase(42, 1)]
        [TestCase(42, 2)]
        [TestCase(42, 96)]
        [TestCase(43, 0)]
        [TestCase(43, 1)]
        [TestCase(43, 2)]
        [TestCase(43, 96)]
        [TestCase(44, 0)]
        [TestCase(44, 1)]
        [TestCase(44, 2)]
        [TestCase(44, 96)]
        [TestCase(45, 0)]
        [TestCase(45, 1)]
        [TestCase(45, 2)]
        [TestCase(45, 96)]
        [TestCase(46, 0)]
        [TestCase(46, 1)]
        [TestCase(46, 2)]
        [TestCase(46, 96)]
        [TestCase(47, 0)]
        [TestCase(47, 1)]
        [TestCase(47, 2)]
        [TestCase(47, 96)]
        [TestCase(48, 0)]
        [TestCase(48, 1)]
        [TestCase(48, 2)]
        [TestCase(48, 96)]
        [TestCase(49, 0)]
        [TestCase(49, 1)]
        [TestCase(49, 2)]
        [TestCase(49, 96)]
        [TestCase(50, 0)]
        [TestCase(50, 1)]
        [TestCase(50, 2)]
        [TestCase(50, 96)]
        public void From_ReturnsAttack_Save_WithBonus_Unatural(int hitDice, int bonus)
        {
            var selection = GetTestSelection();
            selection.SaveDcBonus = bonus;
            selection.IsNatural = false;

            var attack = Attack.From(selection, abilities, hitDice, 90210, 42);
            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + bonus));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void From_ReturnsAttack_BaseAttackBonus()
        {
            var selection = GetTestSelection();

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.BaseAttackBonus, Is.EqualTo(90210));
        }

        [Test]
        public void From_ReturnsAttack_MeleeAttackBaseAbility()
        {
            var selection = GetTestSelection();
            selection.IsMelee = true;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void From_ReturnsAttack_MeleeAttackBaseAbility_NoStrength()
        {
            var selection = GetTestSelection();
            selection.IsMelee = true;

            abilities[AbilityConstants.Strength].BaseScore = 0;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void From_ReturnsAttack_RangedAttackBaseAbility()
        {
            var selection = GetTestSelection();
            selection.IsMelee = false;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void From_ReturnsAttack_SizeModifier()
        {
            var selection = GetTestSelection();

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.SizeModifier, Is.EqualTo(42));
        }

        [Test]
        public void From_ReturnsAttack_SpecialAttack()
        {
            var selection = GetTestSelection();
            selection.IsMelee = true;
            selection.IsSpecial = true;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.BaseAttackBonus, Is.EqualTo(90210));
            Assert.That(attack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]), attack.BaseAbility?.Name);
            Assert.That(attack.SizeModifier, Is.EqualTo(42));
        }

        [TestCase(1, 0.5, -5)]
        [TestCase(2, 0.5, -4)]
        [TestCase(3, 0.5, -4)]
        [TestCase(4, 0.5, -3)]
        [TestCase(5, 0.5, -3)]
        [TestCase(6, 0.5, -2)]
        [TestCase(7, 0.5, -2)]
        [TestCase(8, 0.5, -1)]
        [TestCase(9, 0.5, -1)]
        [TestCase(10, 0.5, 0)]
        [TestCase(11, 0.5, 0)]
        [TestCase(12, 0.5, 0)]
        [TestCase(13, 0.5, 0)]
        [TestCase(14, 0.5, 1)]
        [TestCase(15, 0.5, 1)]
        [TestCase(16, 0.5, 1)]
        [TestCase(17, 0.5, 1)]
        [TestCase(18, 0.5, 2)]
        [TestCase(19, 0.5, 2)]
        [TestCase(20, 0.5, 2)]
        [TestCase(21, 0.5, 2)]
        [TestCase(22, 0.5, 3)]
        [TestCase(23, 0.5, 3)]
        [TestCase(24, 0.5, 3)]
        [TestCase(25, 0.5, 3)]
        [TestCase(26, 0.5, 4)]
        [TestCase(27, 0.5, 4)]
        [TestCase(28, 0.5, 4)]
        [TestCase(29, 0.5, 4)]
        [TestCase(30, 0.5, 5)]
        [TestCase(31, 0.5, 5)]
        [TestCase(32, 0.5, 5)]
        [TestCase(33, 0.5, 5)]
        [TestCase(34, 0.5, 6)]
        [TestCase(35, 0.5, 6)]
        [TestCase(36, 0.5, 6)]
        [TestCase(37, 0.5, 6)]
        [TestCase(38, 0.5, 7)]
        [TestCase(39, 0.5, 7)]
        [TestCase(40, 0.5, 7)]
        [TestCase(41, 0.5, 7)]
        [TestCase(42, 0.5, 8)]
        [TestCase(43, 0.5, 8)]
        [TestCase(44, 0.5, 8)]
        [TestCase(45, 0.5, 8)]
        [TestCase(46, 0.5, 9)]
        [TestCase(47, 0.5, 9)]
        [TestCase(48, 0.5, 9)]
        [TestCase(49, 0.5, 9)]
        [TestCase(50, 0.5, 10)]
        [TestCase(1, 1, -5)]
        [TestCase(2, 1, -4)]
        [TestCase(3, 1, -4)]
        [TestCase(4, 1, -3)]
        [TestCase(5, 1, -3)]
        [TestCase(6, 1, -2)]
        [TestCase(7, 1, -2)]
        [TestCase(8, 1, -1)]
        [TestCase(9, 1, -1)]
        [TestCase(10, 1, 0)]
        [TestCase(11, 1, 0)]
        [TestCase(12, 1, 1)]
        [TestCase(13, 1, 1)]
        [TestCase(14, 1, 2)]
        [TestCase(15, 1, 2)]
        [TestCase(16, 1, 3)]
        [TestCase(17, 1, 3)]
        [TestCase(18, 1, 4)]
        [TestCase(19, 1, 4)]
        [TestCase(20, 1, 5)]
        [TestCase(21, 1, 5)]
        [TestCase(22, 1, 6)]
        [TestCase(23, 1, 6)]
        [TestCase(24, 1, 7)]
        [TestCase(25, 1, 7)]
        [TestCase(26, 1, 8)]
        [TestCase(27, 1, 8)]
        [TestCase(28, 1, 9)]
        [TestCase(29, 1, 9)]
        [TestCase(30, 1, 10)]
        [TestCase(31, 1, 10)]
        [TestCase(32, 1, 11)]
        [TestCase(33, 1, 11)]
        [TestCase(34, 1, 12)]
        [TestCase(35, 1, 12)]
        [TestCase(36, 1, 13)]
        [TestCase(37, 1, 13)]
        [TestCase(38, 1, 14)]
        [TestCase(39, 1, 14)]
        [TestCase(40, 1, 15)]
        [TestCase(41, 1, 15)]
        [TestCase(42, 1, 16)]
        [TestCase(43, 1, 16)]
        [TestCase(44, 1, 17)]
        [TestCase(45, 1, 17)]
        [TestCase(46, 1, 18)]
        [TestCase(47, 1, 18)]
        [TestCase(48, 1, 19)]
        [TestCase(49, 1, 19)]
        [TestCase(50, 1, 20)]
        [TestCase(1, 1.5, -5)]
        [TestCase(2, 1.5, -4)]
        [TestCase(3, 1.5, -4)]
        [TestCase(4, 1.5, -3)]
        [TestCase(5, 1.5, -3)]
        [TestCase(6, 1.5, -2)]
        [TestCase(7, 1.5, -2)]
        [TestCase(8, 1.5, -1)]
        [TestCase(9, 1.5, -1)]
        [TestCase(10, 1.5, 0)]
        [TestCase(11, 1.5, 0)]
        [TestCase(12, 1.5, 1)]
        [TestCase(13, 1.5, 1)]
        [TestCase(14, 1.5, 3)]
        [TestCase(15, 1.5, 3)]
        [TestCase(16, 1.5, 4)]
        [TestCase(17, 1.5, 4)]
        [TestCase(18, 1.5, 6)]
        [TestCase(19, 1.5, 6)]
        [TestCase(20, 1.5, 7)]
        [TestCase(21, 1.5, 7)]
        [TestCase(22, 1.5, 9)]
        [TestCase(23, 1.5, 9)]
        [TestCase(24, 1.5, 10)]
        [TestCase(25, 1.5, 10)]
        [TestCase(26, 1.5, 12)]
        [TestCase(27, 1.5, 12)]
        [TestCase(28, 1.5, 13)]
        [TestCase(29, 1.5, 13)]
        [TestCase(30, 1.5, 15)]
        [TestCase(31, 1.5, 15)]
        [TestCase(32, 1.5, 16)]
        [TestCase(33, 1.5, 16)]
        [TestCase(34, 1.5, 18)]
        [TestCase(35, 1.5, 18)]
        [TestCase(36, 1.5, 19)]
        [TestCase(37, 1.5, 19)]
        [TestCase(38, 1.5, 21)]
        [TestCase(39, 1.5, 21)]
        [TestCase(40, 1.5, 22)]
        [TestCase(41, 1.5, 22)]
        [TestCase(42, 1.5, 24)]
        [TestCase(43, 1.5, 24)]
        [TestCase(44, 1.5, 25)]
        [TestCase(45, 1.5, 25)]
        [TestCase(46, 1.5, 27)]
        [TestCase(47, 1.5, 27)]
        [TestCase(48, 1.5, 28)]
        [TestCase(49, 1.5, 28)]
        [TestCase(50, 1.5, 30)]
        public void From_ReturnsAttack_WithDamageMultiplier(int strengthValue, double multiplier, int bonus)
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = "my roll", Type = "my damage type" },
                new() { Roll = "my other roll", Type = "my other damage type" },
            ];
            selection.DamageEffect = "effect";
            selection.FrequencyQuantity = 1;
            selection.IsPrimary = true;
            selection.IsMelee = true;
            selection.IsNatural = true;
            selection.DamageBonusMultiplier = multiplier;

            abilities[AbilityConstants.Strength].BaseScore = strengthValue;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            var bonusString = bonus > 0 ? $"+{bonus}" : bonus < 0 ? bonus.ToString() : string.Empty;
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo($"my roll{bonusString} my damage type + my other roll my other damage type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(bonus));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void From_ReturnsAttack_Primary_Ranged_Breath()
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = "my roll", Type = "my type" },
            ];
            selection.DamageEffect = "effect";
            selection.FrequencyQuantity = 1;
            selection.IsPrimary = true;
            selection.IsMelee = false;
            selection.IsNatural = true;
            selection.DamageBonusMultiplier = 0;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll my type plus effect"));
            Assert.That(attack.DamageBonus, Is.Zero);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void From_ReturnsAttack_Primary_Ranged_Thrown()
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = "my roll", Type = "my type" },
            ];
            selection.DamageEffect = "effect";
            selection.FrequencyQuantity = 1;
            selection.IsPrimary = true;
            selection.IsMelee = false;
            selection.IsNatural = true;
            selection.DamageBonusMultiplier = 1.5;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll+67650 my type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(67650));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase("my nat melee roll", "my nat melee type", true, true, "nat melee effect")]
        [TestCase("my melee roll", "my melee type", true, false, "melee effect")]
        [TestCase("my nat range roll", "my nat range type", false, true, "nat range effect")]
        [TestCase("my range roll", "my range type", false, false, "range effect")]
        public void From_ReturnsAttack_Primary(string roll, string type, bool melee, bool natural, string effect)
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = roll, Type = type },
            ];
            selection.DamageEffect = effect;
            selection.FrequencyQuantity = 1;
            selection.IsPrimary = true;
            selection.IsMelee = melee;
            selection.IsNatural = natural;
            selection.DamageBonusMultiplier = 1.5;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("nat melee attack"));
            Assert.That(attack.IsMelee, Is.EqualTo(melee));
            Assert.That(attack.IsNatural, Is.EqualTo(natural));
            Assert.That(attack.DamageBonus, Is.EqualTo(67650));
            Assert.That(attack.DamageEffect, Is.EqualTo(effect));
            Assert.That(attack.DamageSummary, Is.EqualTo($"{roll}+67650 {type} plus {effect}"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Primary_Multiple(bool isNatural)
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = "my roll", Type = "my type" },
            ];
            selection.DamageEffect = "effect";
            selection.FrequencyQuantity = 2;
            selection.IsPrimary = true;
            selection.IsMelee = true;
            selection.IsNatural = isNatural;
            selection.DamageBonusMultiplier = 1;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll+45100 my type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(45100));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_ReturnsAttack_Secondary(bool isNatural)
        {
            var selection = GetTestSelection();
            selection.Name = "secondary attack";
            selection.Damages =
            [
                new() { Roll = "my secondary roll", Type = "my secondary type" },
            ];
            selection.DamageEffect = "secondary effect";
            selection.FrequencyQuantity = 1;
            selection.IsPrimary = false;
            selection.IsMelee = true;
            selection.IsNatural = isNatural;
            selection.DamageBonusMultiplier = 0.5;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("secondary attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my secondary roll+22550 my secondary type plus secondary effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo("secondary effect"));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void From_ReturnsAttack_WithAbilityEffect(string ability)
        {
            var selection = GetTestSelection();
            selection.Damages =
            [
                new() { Roll = "my secondary roll", Type = "my secondary type" },
                new() { Roll = "1d4", Type = ability },
            ];
            selection.DamageEffect = $"{ability} drain";
            selection.FrequencyQuantity = 1;
            selection.IsMelee = true;
            selection.DamageBonusMultiplier = 0.5;

            abilities[AbilityConstants.Strength].BaseScore = 1336;

            var attack = Attack.From(selection, abilities, 9266, 90210, 42);
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo($"my roll+22550 my type + 1d4 {ability} plus {ability} drain"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo($"{ability} drain"));
        }
    }
}