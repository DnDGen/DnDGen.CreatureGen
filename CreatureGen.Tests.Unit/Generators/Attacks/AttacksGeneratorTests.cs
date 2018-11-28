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
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.AttackBonuses, "size")).Returns(90210);

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
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.AttackBonuses, "size")).Returns(90210);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.Zero);
            Assert.That(generatedAttack.BaseAbility, Is.Null);
            Assert.That(generatedAttack.SizeModifierForAttackBonus, Is.Zero);
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
