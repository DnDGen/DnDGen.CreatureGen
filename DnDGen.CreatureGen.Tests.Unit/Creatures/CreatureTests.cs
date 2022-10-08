using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class CreatureTests
    {
        private Creature creature;

        [SetUp]
        public void Setup()
        {
            creature = new Creature();
        }

        [Test]
        public void CreatureInitialized()
        {
            Assert.That(creature.Abilities, Is.Empty);
            Assert.That(creature.Alignment, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.Attacks, Is.Empty);
            Assert.That(creature.BaseAttackBonus, Is.Zero);
            Assert.That(creature.CanUseEquipment, Is.False);
            Assert.That(creature.ChallengeRating, Is.Empty);
            Assert.That(creature.Feats, Is.Empty);
            Assert.That(creature.FullMeleeAttack, Is.Empty);
            Assert.That(creature.FullRangedAttack, Is.Empty);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.GrappleBonus, Is.Null);
            Assert.That(creature.HitPoints, Is.Not.Null);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.Zero);
            Assert.That(creature.LevelAdjustment, Is.Null);
            Assert.That(creature.MeleeAttack, Is.Null);
            Assert.That(creature.Name, Is.Empty);
            Assert.That(creature.NumberOfHands, Is.Zero);
            Assert.That(creature.RangedAttack, Is.Null);
            Assert.That(creature.Reach, Is.Not.Null);
            Assert.That(creature.Reach.Unit, Is.EqualTo("feet"));
            Assert.That(creature.Saves, Is.Not.Null);
            Assert.That(creature.Size, Is.Empty);
            Assert.That(creature.Skills, Is.Empty);
            Assert.That(creature.Space, Is.Not.Null);
            Assert.That(creature.Space.Unit, Is.EqualTo("feet"));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.SpecialQualities, Is.Empty);
            Assert.That(creature.Speeds, Is.Empty);
            Assert.That(creature.Summary, Is.Empty);
            Assert.That(creature.Templates, Is.Empty);
            Assert.That(creature.Type, Is.Not.Null);
            Assert.That(creature.Magic, Is.Not.Null);
            Assert.That(creature.Languages, Is.Empty);
            Assert.That(creature.Demographics, Is.Not.Null);
        }

        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(6, -2)]
        [TestCase(8, -1)]
        [TestCase(10, 0)]
        [TestCase(12, 1)]
        [TestCase(14, 2)]
        [TestCase(16, 3)]
        [TestCase(18, 4)]
        [TestCase(42, 16)]
        public void TotalInitiativeBonus_DexterityModifier(int dexterity, int initiative)
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = dexterity };
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(initiative));
        }

        [Test]
        public void TotalInitiativeBonus_NoDexterity_UseIntelligence()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 0 };
            creature.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence) { BaseScore = 9266 };

            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(4628));
        }

        [Test]
        public void TotalInitiativeBonus_NoDexterity_NoIntelligence()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 0 };
            creature.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence) { BaseScore = 0 };

            Assert.That(creature.TotalInitiativeBonus, Is.Zero);
        }

        [Test]
        public void TotalInitiativeBonus_Bonus()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            creature.InitiativeBonus = 90210;

            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(90210));
        }

        [Test]
        public void TotalInitiativeBonus_DexterityModifierAndBonus()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 9266 };
            creature.InitiativeBonus = 90210;

            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(94838));
        }

        [Test]
        public void TotalInitiativeBonus_NoDexterityModifierAndBonus_Intelligence()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 0 };
            creature.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence) { BaseScore = 9266 };
            creature.InitiativeBonus = 90210;

            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(94838));
        }

        [Test]
        public void TotalInitiativeBonus_NoDexterityModifierAndBonus_NoIntelligence()
        {
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity) { BaseScore = 0 };
            creature.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence) { BaseScore = 0 };
            creature.InitiativeBonus = 90210;

            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(90210));
        }

        [Test]
        public void MeleeAttackIsFirstPrimaryMeleeAttack()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "other primary melee attack" },
            };

            var attack = creature.MeleeAttack;
            Assert.That(attack.Name, Is.EqualTo("primary melee attack"));
        }

        [Test]
        public void MeleeAttackIsNotFirstPrimaryMeleeAttack()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
            };

            var attack = creature.MeleeAttack;
            Assert.That(attack, Is.Null);
        }

        [Test]
        public void RangedAttackIsFirstPrimaryRangedAttack()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "other primary ranged attack" },
            };

            var attack = creature.RangedAttack;
            Assert.That(attack.Name, Is.EqualTo("primary ranged attack"));
        }

        [Test]
        public void RangedAttackIsNotFirstPrimaryRangedAttack()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
            };

            var attack = creature.RangedAttack;
            Assert.That(attack, Is.Null);
        }

        [Test]
        public void FullMeleeAttackIsAllMeleeAttacks()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "other primary melee attack" },
            };

            var attacks = creature.FullMeleeAttack.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary melee attack"));
            Assert.That(attacks[1].Name, Is.EqualTo("other primary melee attack"));
            Assert.That(attacks[2].Name, Is.EqualTo("secondary melee attack"));
            Assert.That(attacks.Length, Is.EqualTo(3));
        }

        [Test]
        public void FullMeleeAttackIsEmpty()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
            };

            var attacks = creature.FullMeleeAttack;
            Assert.That(attacks, Is.Empty);
        }

        [Test]
        public void FullRangedAttackIsAllRangedAttacks()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "other primary ranged attack" },
            };

            var attacks = creature.FullRangedAttack.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary ranged attack"));
            Assert.That(attacks[1].Name, Is.EqualTo("other primary ranged attack"));
            Assert.That(attacks[2].Name, Is.EqualTo("secondary ranged attack"));
            Assert.That(attacks.Length, Is.EqualTo(3));
        }

        [Test]
        public void FullRangedAttackIsEmpty()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
            };

            var attacks = creature.FullRangedAttack;
            Assert.That(attacks, Is.Empty);
        }

        [Test]
        public void SpecialAttacksAreSpecial()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = true, Name = "secondary ranged special attack" },
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = true, Name = "secondary melee special attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = true, Name = "primary ranged special attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = true, Name = "primary melee special attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
            };

            var attacks = creature.SpecialAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary melee special attack"));
            Assert.That(attacks[1].Name, Is.EqualTo("primary ranged special attack"));
            Assert.That(attacks[2].Name, Is.EqualTo("secondary melee special attack"));
            Assert.That(attacks[3].Name, Is.EqualTo("secondary ranged special attack"));
            Assert.That(attacks.Length, Is.EqualTo(4));
        }

        [Test]
        public void SpecialAttacksIsEmpty()
        {
            creature.Attacks = new[]
            {
                new Attack { IsPrimary = false, IsMelee = false, IsSpecial = false, Name = "secondary ranged attack" },
                new Attack { IsPrimary = false, IsMelee = true, IsSpecial = false, Name = "secondary melee attack" },
                new Attack { IsPrimary = true, IsMelee = false, IsSpecial = false, Name = "primary ranged attack" },
                new Attack { IsPrimary = true, IsMelee = true, IsSpecial = false, Name = "primary melee attack" },
            };

            var attacks = creature.SpecialAttacks;
            Assert.That(attacks, Is.Empty);
        }

        [Test]
        public void CreatureSummary()
        {
            creature.Name = Guid.NewGuid().ToString();

            Assert.That(creature.Summary, Is.EqualTo(creature.Name));
        }

        [Test]
        public void CreatureSummary_Advanced()
        {
            creature.Name = Guid.NewGuid().ToString();
            creature.IsAdvanced = true;

            Assert.That(creature.Summary, Is.EqualTo(creature.Name + " [Advanced]"));
        }

        [Test]
        public void CreatureSummaryWithTemplate()
        {
            creature.Name = Guid.NewGuid().ToString();
            creature.Templates.Add("my template");

            Assert.That(creature.Summary, Is.EqualTo($"my template {creature.Name}"));
        }

        [Test]
        public void CreatureSummaryWithTemplate_Advanced()
        {
            creature.Name = Guid.NewGuid().ToString();
            creature.Templates.Add("my template");
            creature.IsAdvanced = true;

            Assert.That(creature.Summary, Is.EqualTo($"my template {creature.Name} [Advanced]"));
        }

        [Test]
        public void CreatureSummaryWithMultipleTemplates()
        {
            creature.Name = Guid.NewGuid().ToString();
            creature.Templates.Add("my template");
            creature.Templates.Add("my other template");

            Assert.That(creature.Summary, Is.EqualTo($"my template my other template {creature.Name}"));
        }

        [Test]
        public void CreatureSummaryWithMultipleTemplates_Advanced()
        {
            creature.Name = Guid.NewGuid().ToString();
            creature.Templates.Add("my template");
            creature.Templates.Add("my other template");
            creature.IsAdvanced = true;

            Assert.That(creature.Summary, Is.EqualTo($"my template my other template {creature.Name} [Advanced]"));
        }
    }
}