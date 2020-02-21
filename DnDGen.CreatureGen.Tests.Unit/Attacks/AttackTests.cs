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
        public void AttackBonuses_Secondary_Manufactured(int baseAttackBonus, params int[] bonuses)
        {
            attack.BaseAttackBonus = baseAttackBonus;
            attack.IsPrimary = false;
            attack.IsNatural = false;
            attack.AttackBonuses.Add(-5);

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