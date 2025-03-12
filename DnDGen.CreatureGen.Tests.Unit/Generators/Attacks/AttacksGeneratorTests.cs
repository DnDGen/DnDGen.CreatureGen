﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Attacks
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
            hitPoints.HitDice.Add(new HitDice { Quantity = hitDiceQuantity });
            mockCollectionSelector.Setup(s => s.FindCollectionOf(Config.Name, TableNameConstants.Collection.CreatureGroups, creatureType.Name,
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
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "creature")).Returns(600);

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("creature", "size", 9266, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.EqualTo(9266 + abilities[AbilityConstants.Strength].Modifier + 42 + 600));
        }

        [Test]
        public void GenerateNegativeGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1;

            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "size")).Returns(-1);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, "creature")).Returns(0);

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("creature", "size", 0, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.EqualTo(0 + abilities[AbilityConstants.Strength].Modifier - 1));
            Assert.That(grappleBonus, Is.EqualTo(-6));
        }

        [Test]
        public void NoGrappleBonusIfNoStrengthScore()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("creature", "size", 9266, abilities[AbilityConstants.Strength]);
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
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
                IsMelee = isMelee,
                IsNatural = isNatural,
                IsPrimary = isPrimary,
                IsSpecial = isSpecial
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 90210, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Null);
        }

        [Test]
        public void GenerateAttack_IgnoreAttacksThatDoNotMeetRequirements()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "good attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
            });
            attacks.Add(new AttackDataSelection()
            {
                Name = "gendered attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my gendered roll", Type = "my gendered damage type", Condition = "my gendered condition" },
                    new Damage { Roll = "my other gendered roll", Type = "my other gendered damage type", Condition = "my other gendered condition" },
                },
                DamageEffect = "gendered damage effect",
                AttackType = "gendered attack type",
                FrequencyQuantity = 600,
                FrequencyTimePeriod = "gendered time period",
                RequiredGender = "required gender"
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 90210, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("good attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Null);
        }

        [Test]
        public void GenerateAttack_AddAttacksThatMeetRequirements()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "good attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
            });
            attacks.Add(new AttackDataSelection()
            {
                Name = "gendered attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my gendered roll", Type = "my gendered damage type", Condition = "my gendered condition" },
                    new Damage { Roll = "my other gendered roll", Type = "my other gendered damage type", Condition = "my other gendered condition" },
                },
                DamageEffect = "gendered damage effect",
                AttackType = "gendered attack type",
                FrequencyQuantity = 600,
                FrequencyTimePeriod = "gendered time period",
                RequiredGender = "required gender"
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 90210, "required gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(2));

            var attack = generatedAttacks.First();
            Assert.That(attack.Name, Is.EqualTo("good attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(attack.Save, Is.Null);

            attack = generatedAttacks.Last();
            Assert.That(attack.Name, Is.EqualTo("gendered attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my gendered roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my gendered damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my gendered condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other gendered roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other gendered damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other gendered condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("gendered damage effect"));
            Assert.That(attack.AttackType, Is.EqualTo("gendered attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("gendered time period"));
            Assert.That(attack.Save, Is.Null);
        }

        [TestCase(false, false, false, AbilityConstants.Charisma)]
        [TestCase(false, false, false, AbilityConstants.Constitution)]
        [TestCase(false, false, false, AbilityConstants.Dexterity)]
        [TestCase(false, false, false, AbilityConstants.Intelligence)]
        [TestCase(false, false, false, AbilityConstants.Strength)]
        [TestCase(false, false, false, AbilityConstants.Wisdom)]
        [TestCase(false, false, true, AbilityConstants.Charisma)]
        [TestCase(false, false, true, AbilityConstants.Constitution)]
        [TestCase(false, false, true, AbilityConstants.Dexterity)]
        [TestCase(false, false, true, AbilityConstants.Intelligence)]
        [TestCase(false, false, true, AbilityConstants.Strength)]
        [TestCase(false, false, true, AbilityConstants.Wisdom)]
        [TestCase(false, true, false, AbilityConstants.Charisma)]
        [TestCase(false, true, false, AbilityConstants.Constitution)]
        [TestCase(false, true, false, AbilityConstants.Dexterity)]
        [TestCase(false, true, false, AbilityConstants.Intelligence)]
        [TestCase(false, true, false, AbilityConstants.Strength)]
        [TestCase(false, true, false, AbilityConstants.Wisdom)]
        [TestCase(false, true, true, AbilityConstants.Charisma)]
        [TestCase(false, true, true, AbilityConstants.Constitution)]
        [TestCase(false, true, true, AbilityConstants.Dexterity)]
        [TestCase(false, true, true, AbilityConstants.Intelligence)]
        [TestCase(false, true, true, AbilityConstants.Strength)]
        [TestCase(false, true, true, AbilityConstants.Wisdom)]
        [TestCase(true, false, false, AbilityConstants.Charisma)]
        [TestCase(true, false, false, AbilityConstants.Constitution)]
        [TestCase(true, false, false, AbilityConstants.Dexterity)]
        [TestCase(true, false, false, AbilityConstants.Intelligence)]
        [TestCase(true, false, false, AbilityConstants.Strength)]
        [TestCase(true, false, false, AbilityConstants.Wisdom)]
        [TestCase(true, false, true, AbilityConstants.Charisma)]
        [TestCase(true, false, true, AbilityConstants.Constitution)]
        [TestCase(true, false, true, AbilityConstants.Dexterity)]
        [TestCase(true, false, true, AbilityConstants.Intelligence)]
        [TestCase(true, false, true, AbilityConstants.Strength)]
        [TestCase(true, false, true, AbilityConstants.Wisdom)]
        [TestCase(true, true, false, AbilityConstants.Charisma)]
        [TestCase(true, true, false, AbilityConstants.Constitution)]
        [TestCase(true, true, false, AbilityConstants.Dexterity)]
        [TestCase(true, true, false, AbilityConstants.Intelligence)]
        [TestCase(true, true, false, AbilityConstants.Strength)]
        [TestCase(true, true, false, AbilityConstants.Wisdom)]
        [TestCase(true, true, true, AbilityConstants.Charisma)]
        [TestCase(true, true, true, AbilityConstants.Constitution)]
        [TestCase(true, true, true, AbilityConstants.Dexterity)]
        [TestCase(true, true, true, AbilityConstants.Intelligence)]
        [TestCase(true, true, true, AbilityConstants.Strength)]
        [TestCase(true, true, true, AbilityConstants.Wisdom)]
        public void GenerateAttack_WithSave(bool isMelee, bool isPrimary, bool isSpecial, string saveAbility)
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
                IsMelee = isMelee,
                IsNatural = true,
                IsPrimary = isPrimary,
                IsSpecial = isSpecial,
                Save = "save",
                SaveAbility = saveAbility,
                SaveDcBonus = 1337
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 300 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities[saveAbility]));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void GenerateAttack_WithSave_UnnaturalAndWithoutAbility()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
                IsMelee = true,
                IsNatural = false,
                IsPrimary = false,
                IsSpecial = true,
                Save = "save",
                SaveDcBonus = 1337
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.True);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.True);
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void BUG_GenerateAttack_WithSave_NaturalAndWithoutAbility()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
                IsMelee = true,
                IsNatural = true,
                IsPrimary = false,
                IsSpecial = true,
                Save = "save",
                SaveDcBonus = 1337
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.IsMelee, Is.True);
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.True);
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 1337));
            Assert.That(attack.Save.BaseAbility, Is.Null);
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [TestCase(1, 0, 10)]
        [TestCase(1, 1, 11)]
        [TestCase(1, 2, 12)]
        [TestCase(1, 96, 106)]
        [TestCase(2, 0, 11)]
        [TestCase(2, 1, 12)]
        [TestCase(2, 2, 13)]
        [TestCase(2, 96, 107)]
        [TestCase(3, 0, 11)]
        [TestCase(3, 1, 12)]
        [TestCase(3, 2, 13)]
        [TestCase(3, 96, 107)]
        [TestCase(4, 0, 12)]
        [TestCase(4, 1, 13)]
        [TestCase(4, 2, 14)]
        [TestCase(4, 96, 108)]
        [TestCase(5, 0, 12)]
        [TestCase(5, 1, 13)]
        [TestCase(5, 2, 14)]
        [TestCase(5, 96, 108)]
        [TestCase(6, 0, 13)]
        [TestCase(6, 1, 14)]
        [TestCase(6, 2, 15)]
        [TestCase(6, 96, 109)]
        [TestCase(7, 0, 13)]
        [TestCase(7, 1, 14)]
        [TestCase(7, 2, 15)]
        [TestCase(7, 96, 109)]
        [TestCase(8, 0, 14)]
        [TestCase(8, 1, 15)]
        [TestCase(8, 2, 16)]
        [TestCase(8, 96, 110)]
        [TestCase(9, 0, 14)]
        [TestCase(9, 1, 15)]
        [TestCase(9, 2, 16)]
        [TestCase(9, 96, 110)]
        [TestCase(10, 0, 15)]
        [TestCase(10, 1, 16)]
        [TestCase(10, 2, 17)]
        [TestCase(10, 96, 111)]
        [TestCase(11, 0, 15)]
        [TestCase(11, 1, 16)]
        [TestCase(11, 2, 17)]
        [TestCase(11, 96, 111)]
        [TestCase(12, 0, 16)]
        [TestCase(12, 1, 17)]
        [TestCase(12, 2, 18)]
        [TestCase(12, 96, 112)]
        [TestCase(13, 0, 16)]
        [TestCase(13, 1, 17)]
        [TestCase(13, 2, 18)]
        [TestCase(13, 96, 112)]
        [TestCase(14, 0, 17)]
        [TestCase(14, 1, 18)]
        [TestCase(14, 2, 19)]
        [TestCase(14, 96, 113)]
        [TestCase(15, 0, 17)]
        [TestCase(15, 1, 18)]
        [TestCase(15, 2, 19)]
        [TestCase(15, 96, 113)]
        [TestCase(16, 0, 18)]
        [TestCase(16, 1, 19)]
        [TestCase(16, 2, 20)]
        [TestCase(16, 96, 114)]
        [TestCase(17, 0, 18)]
        [TestCase(17, 1, 19)]
        [TestCase(17, 2, 20)]
        [TestCase(17, 96, 114)]
        [TestCase(18, 0, 19)]
        [TestCase(18, 1, 20)]
        [TestCase(18, 2, 21)]
        [TestCase(18, 96, 115)]
        [TestCase(19, 0, 19)]
        [TestCase(19, 1, 20)]
        [TestCase(19, 2, 21)]
        [TestCase(19, 96, 115)]
        [TestCase(20, 0, 20)]
        [TestCase(20, 1, 21)]
        [TestCase(20, 2, 22)]
        [TestCase(20, 96, 116)]
        [TestCase(21, 0, 20)]
        [TestCase(21, 1, 21)]
        [TestCase(21, 2, 22)]
        [TestCase(21, 96, 116)]
        [TestCase(22, 0, 21)]
        [TestCase(22, 1, 22)]
        [TestCase(22, 2, 23)]
        [TestCase(22, 96, 117)]
        [TestCase(23, 0, 21)]
        [TestCase(23, 1, 22)]
        [TestCase(23, 2, 23)]
        [TestCase(23, 96, 117)]
        [TestCase(24, 0, 22)]
        [TestCase(24, 1, 23)]
        [TestCase(24, 2, 24)]
        [TestCase(24, 96, 118)]
        [TestCase(25, 0, 22)]
        [TestCase(25, 1, 23)]
        [TestCase(25, 2, 24)]
        [TestCase(25, 96, 118)]
        [TestCase(26, 0, 23)]
        [TestCase(26, 1, 24)]
        [TestCase(26, 2, 25)]
        [TestCase(26, 96, 119)]
        [TestCase(27, 0, 23)]
        [TestCase(27, 1, 24)]
        [TestCase(27, 2, 25)]
        [TestCase(27, 96, 119)]
        [TestCase(28, 0, 24)]
        [TestCase(28, 1, 25)]
        [TestCase(28, 2, 26)]
        [TestCase(28, 96, 120)]
        [TestCase(29, 0, 24)]
        [TestCase(29, 1, 25)]
        [TestCase(29, 2, 26)]
        [TestCase(29, 96, 120)]
        [TestCase(30, 0, 25)]
        [TestCase(30, 1, 26)]
        [TestCase(30, 2, 27)]
        [TestCase(30, 96, 121)]
        [TestCase(31, 0, 25)]
        [TestCase(31, 1, 26)]
        [TestCase(31, 2, 27)]
        [TestCase(31, 96, 121)]
        [TestCase(32, 0, 26)]
        [TestCase(32, 1, 27)]
        [TestCase(32, 2, 28)]
        [TestCase(32, 96, 122)]
        [TestCase(33, 0, 26)]
        [TestCase(33, 1, 27)]
        [TestCase(33, 2, 28)]
        [TestCase(33, 96, 122)]
        [TestCase(34, 0, 27)]
        [TestCase(34, 1, 28)]
        [TestCase(34, 2, 29)]
        [TestCase(34, 96, 123)]
        [TestCase(35, 0, 27)]
        [TestCase(35, 1, 28)]
        [TestCase(35, 2, 29)]
        [TestCase(35, 96, 123)]
        [TestCase(36, 0, 28)]
        [TestCase(36, 1, 29)]
        [TestCase(36, 2, 30)]
        [TestCase(36, 96, 124)]
        [TestCase(37, 0, 28)]
        [TestCase(37, 1, 29)]
        [TestCase(37, 2, 30)]
        [TestCase(37, 96, 124)]
        [TestCase(38, 0, 29)]
        [TestCase(38, 1, 30)]
        [TestCase(38, 2, 31)]
        [TestCase(38, 96, 125)]
        [TestCase(39, 0, 29)]
        [TestCase(39, 1, 30)]
        [TestCase(39, 2, 31)]
        [TestCase(39, 96, 125)]
        [TestCase(40, 0, 30)]
        [TestCase(40, 1, 31)]
        [TestCase(40, 2, 32)]
        [TestCase(40, 96, 126)]
        [TestCase(41, 0, 30)]
        [TestCase(41, 1, 31)]
        [TestCase(41, 2, 32)]
        [TestCase(41, 96, 126)]
        [TestCase(42, 0, 31)]
        [TestCase(42, 1, 32)]
        [TestCase(42, 2, 33)]
        [TestCase(42, 96, 127)]
        [TestCase(43, 0, 31)]
        [TestCase(43, 1, 32)]
        [TestCase(43, 2, 33)]
        [TestCase(43, 96, 127)]
        [TestCase(44, 0, 32)]
        [TestCase(44, 1, 33)]
        [TestCase(44, 2, 34)]
        [TestCase(44, 96, 128)]
        [TestCase(45, 0, 32)]
        [TestCase(45, 1, 33)]
        [TestCase(45, 2, 34)]
        [TestCase(45, 96, 128)]
        [TestCase(46, 0, 33)]
        [TestCase(46, 1, 34)]
        [TestCase(46, 2, 35)]
        [TestCase(46, 96, 129)]
        [TestCase(47, 0, 33)]
        [TestCase(47, 1, 34)]
        [TestCase(47, 2, 35)]
        [TestCase(47, 96, 129)]
        [TestCase(48, 0, 34)]
        [TestCase(48, 1, 35)]
        [TestCase(48, 2, 36)]
        [TestCase(48, 96, 130)]
        [TestCase(49, 0, 34)]
        [TestCase(49, 1, 35)]
        [TestCase(49, 2, 36)]
        [TestCase(49, 96, 130)]
        [TestCase(50, 0, 35)]
        [TestCase(50, 1, 36)]
        [TestCase(50, 2, 37)]
        [TestCase(50, 96, 131)]
        public void GenerateAttack_WithSave(int hitDice, int bonus, int saveBase)
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                    new Damage { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                },
                DamageEffect = "damage effect",
                AttackType = "attack type",
                FrequencyQuantity = 42,
                FrequencyTimePeriod = "time period",
                Save = "save",
                SaveAbility = abilities.Keys.First(),
                SaveDcBonus = bonus,
                IsNatural = true,
            });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, hitDice, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("damage effect"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Frequency, Is.Not.Null);
            Assert.That(attack.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(attack.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(attack.Save, Is.Not.Null);
            Assert.That(attack.Save.BaseValue, Is.EqualTo(saveBase));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities.Values.First()));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void GenerateAttacks()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection() { Name = "attack", Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } } });
            attacks.Add(new AttackDataSelection() { Name = "other attack", Damages = new List<Damage> { new Damage { Roll = "my other roll", Type = "my other type" } } });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()));
            Assert.That(generatedAttacks.Count, Is.EqualTo(2));

            var attack = generatedAttacks.First();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo("my roll my type"));

            attack = generatedAttacks.Last();
            Assert.That(attack.Name, Is.EqualTo("other attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo("my other roll my other type"));
        }

        [Test]
        public void GenerateNoAttacks()
        {
            var attacks = new List<AttackDataSelection>();

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Empty);
        }

        [Test]
        public void GenerateAttackBaseAttackBonus()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbility()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1", IsMelee = true });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateRangedAttackBaseAbility()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1", IsMelee = false });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbilityWithNoStrength()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1", IsMelee = true });

            abilities[AbilityConstants.Strength].BaseScore = 0;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateAttackSizeModifier()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1" });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(90210);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SizeModifier, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateSpecialAttack()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection { Name = "attack 1", IsMelee = true, IsSpecial = true });

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(90210);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.EqualTo(9266));
            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]), generatedAttack.BaseAbility?.Name);
            Assert.That(generatedAttack.SizeModifier, Is.EqualTo(90210));
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
        public void GenerateAttack_WithDamageMultiplier(int strengthValue, double multiplier, int bonus)
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my damage type" },
                    new Damage { Roll = "my other roll", Type = "my other damage type" },
                },
                DamageEffect = "effect",
                DamageBonusMultiplier = multiplier,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = true
            });

            abilities[AbilityConstants.Strength].BaseScore = strengthValue;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            var bonusString = bonus > 0 ? $"+{bonus}" : bonus < 0 ? bonus.ToString() : string.Empty;
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo($"my roll{bonusString} my damage type + my other roll my other damage type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(bonus));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Breath()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } },
                DamageEffect = "effect",
                DamageBonusMultiplier = 0,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = false,
                IsNatural = true
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo("my roll my type plus effect"));
            Assert.That(attack.DamageBonus, Is.Zero);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Thrown()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } },
                DamageEffect = "effect",
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = false,
                IsNatural = true
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo("my roll+67650 my type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(67650));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_AllSole()
        {
            var attackSelections = new List<AttackDataSelection>();
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "nat melee attack",
                Damages = new List<Damage> { new Damage { Roll = "my nat melee roll", Type = "my nat melee type" } },
                DamageEffect = "nat melee effect",
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = true
            });
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "nat range attack",
                Damages = new List<Damage> { new Damage { Roll = "my nat range roll", Type = "my nat range type" } },
                DamageEffect = "nat range effect",
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = false,
                IsNatural = true
            });
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "melee attack",
                Damages = new List<Damage> { new Damage { Roll = "my melee roll", Type = "my melee type" } },
                DamageEffect = "melee effect",
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = false
            });
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "range attack",
                Damages = new List<Damage> { new Damage { Roll = "my range roll", Type = "my range type" } },
                DamageEffect = "range effect",
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = false,
                IsNatural = false
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(4));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("nat melee attack"));
            Assert.That(attacks[0].IsMelee, Is.True);
            Assert.That(attacks[0].IsNatural, Is.True);
            Assert.That(attacks[0].DamageDescription, Is.EqualTo("my nat melee roll+67650 my nat melee type plus nat melee effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("nat melee effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("nat range attack"));
            Assert.That(attacks[1].IsMelee, Is.False);
            Assert.That(attacks[1].IsNatural, Is.True);
            Assert.That(attacks[1].DamageDescription, Is.EqualTo("my nat range roll+67650 my nat range type plus nat range effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("nat range effect"));
            Assert.That(attacks[2].Name, Is.EqualTo("melee attack"));
            Assert.That(attacks[2].IsMelee, Is.True);
            Assert.That(attacks[2].IsNatural, Is.False);
            Assert.That(attacks[2].DamageDescription, Is.EqualTo("my melee roll+67650 my melee type plus melee effect"));
            Assert.That(attacks[2].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[2].DamageEffect, Is.EqualTo("melee effect"));
            Assert.That(attacks[3].Name, Is.EqualTo("range attack"));
            Assert.That(attacks[3].IsMelee, Is.False);
            Assert.That(attacks[3].IsNatural, Is.False);
            Assert.That(attacks[3].DamageDescription, Is.EqualTo("my range roll+67650 my range type plus range effect"));
            Assert.That(attacks[3].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[3].DamageEffect, Is.EqualTo("range effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_Multiple(bool isNatural)
        {
            var attackSelections = new List<AttackDataSelection>();
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } },
                DamageEffect = "effect",
                DamageBonusMultiplier = 1,
                FrequencyQuantity = 2,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = isNatural
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(1));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageDescription, Is.EqualTo("my roll+45100 my type plus effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_WithSecondary(bool isNatural)
        {
            var attackSelections = new List<AttackDataSelection>();
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "primary attack",
                Damages = new List<Damage> { new Damage { Roll = "my primary roll", Type = "my primary type" } },
                DamageEffect = "primary effect",
                DamageBonusMultiplier = 1,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = isNatural
            });
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "secondary attack",
                Damages = new List<Damage> { new Damage { Roll = "my secondary roll", Type = "my secondary type" } },
                DamageEffect = "secondary effect",
                DamageBonusMultiplier = 0.5,
                FrequencyQuantity = 1,
                IsPrimary = false,
                IsMelee = true,
                IsNatural = isNatural
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary attack"));
            Assert.That(attacks[0].DamageDescription, Is.EqualTo("my primary roll+45100 my primary type plus primary effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("primary effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("secondary attack"));
            Assert.That(attacks[1].DamageDescription, Is.EqualTo("my secondary roll+22550 my secondary type plus secondary effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(22550));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("secondary effect"));
        }

        [Test]
        public void GenerateAttack_Primary_SoleAndMultiple()
        {
            var attackSelections = new List<AttackDataSelection>();
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } },
                DamageBonusMultiplier = 1.5,
                FrequencyQuantity = 1,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = false
            });
            attackSelections.Add(new AttackDataSelection()
            {
                Name = "nat attack",
                Damages = new List<Damage> { new Damage { Roll = "my nat roll", Type = "my nat type" } },
                DamageEffect = "effect",
                DamageBonusMultiplier = 1,
                FrequencyQuantity = 2,
                IsPrimary = true,
                IsMelee = true,
                IsNatural = true
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageDescription, Is.EqualTo("my roll+67650 my type"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.Empty);
            Assert.That(attacks[1].Name, Is.EqualTo("nat attack"));
            Assert.That(attacks[1].DamageDescription, Is.EqualTo("my nat roll+45100 my nat type plus effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Secondary()
        {
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage> { new Damage { Roll = "my roll", Type = "my type" } },
                DamageEffect = "effect",
                DamageBonusMultiplier = 0.5,
                FrequencyQuantity = 1,
                IsPrimary = false,
                IsMelee = true,
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo("my roll+22550 my type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
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
            var attacks = new List<AttackDataSelection>();
            attacks.Add(new AttackDataSelection()
            {
                Name = "attack",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "my roll", Type = "my type" },
                    new Damage { Roll = "1d4", Type = ability },
                },
                DamageEffect = $"{ability} drain",
                DamageBonusMultiplier = 0.5,
                FrequencyQuantity = 1,
                IsMelee = true,
            });

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "original size", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "original size", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageDescription, Is.EqualTo($"my roll+22550 my type + 1d4 {ability} plus {ability} drain"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo($"{ability} drain"));
        }

        [Test]
        public void ApplyPrimaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
            Assert.That(generatedAttack.TotalAttackBonus, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
            Assert.That(generatedAttack.TotalAttackBonus, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
            Assert.That(generatedAttack.TotalAttackBonus, Is.Zero);
        }

        [Test]
        public void ApplyPrimaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = true, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
            Assert.That(generatedAttack.TotalAttackBonus, Is.Zero);
        }

        [Test]
        public void ApplySecondaryAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Has.Count.EqualTo(1).And.Contains(-5));
            Assert.That(generatedAttack.TotalAttackBonus, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = false });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Has.Count.EqualTo(1).And.Contains(-5));
            Assert.That(generatedAttack.TotalAttackBonus, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = "feat" } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Has.Count.EqualTo(1).And.Contains(-5));
            Assert.That(generatedAttack.TotalAttackBonus, Is.EqualTo(-5));
        }

        [Test]
        public void ApplySecondaryNaturalAttackBonusesWithMultiattack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsPrimary = false, IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.Monster.Multiattack } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Has.Count.EqualTo(2)
                .And.Contains(-5)
                .And.Contains(3));
            Assert.That(generatedAttack.TotalAttackBonus, Is.EqualTo(-2));
        }

        [Test]
        public void ApplySpecialAttackBonuses()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack
            {
                Name = "attack 1",
                Damages = new List<Damage> { new Damage { Roll = "roll 1", Type = "type 1" } },
                IsPrimary = false,
                IsMelee = false,
                IsNatural = false,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 2",
                Damages = new List<Damage> { new Damage { Roll = "roll 2", Type = "type 2" } },
                IsPrimary = false,
                IsMelee = false,
                IsNatural = true,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 3",
                Damages = new List<Damage> { new Damage { Roll = "roll 3", Type = "type 3" } },
                IsPrimary = true,
                IsMelee = false,
                IsNatural = false,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 4",
                Damages = new List<Damage> { new Damage { Roll = "roll 4", Type = "type 4" } },
                IsPrimary = true,
                IsMelee = false,
                IsNatural = true,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 5",
                Damages = new List<Damage> { new Damage { Roll = "roll 5", Type = "type 5" } },
                IsPrimary = false,
                IsMelee = true,
                IsNatural = false,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 6",
                Damages = new List<Damage> { new Damage { Roll = "roll 6", Type = "type 6" } },
                IsPrimary = false,
                IsMelee = true,
                IsNatural = true,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 7",
                Damages = new List<Damage> { new Damage { Roll = "roll 7", Type = "type 7" } },
                IsPrimary = true,
                IsMelee = true,
                IsNatural = false,
                IsSpecial = true,
            });
            attacks.Add(new Attack
            {
                Name = "attack 8",
                Damages = new List<Damage> { new Damage { Roll = "roll 8", Type = "type 8" } },
                IsPrimary = true,
                IsMelee = true,
                IsNatural = true,
                IsSpecial = true,
            });

            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = FeatConstants.Monster.Multiattack }
            };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);

            foreach (var attack in generatedAttacks)
            {
                Assert.That(attack.AttackBonuses, Is.Empty);
                Assert.That(attack.TotalAttackBonus, Is.Zero);
            }
        }

        [Test]
        public void ApplyDexterityForMeleeInsteadOfStrengthForNaturalAttacksIfWeaponFinesse_IfNatural()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength], IsNatural = true });

            var feats = new[] { new Feat { Name = FeatConstants.WeaponFinesse } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void DoNotApplyDexterityForMeleeInsteadOfStrengthForNaturalAttacksIfWeaponFinesse_Unnatural()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "attack 1", IsMelee = true, BaseAbility = abilities[AbilityConstants.Strength], IsNatural = false });

            var feats = new[] { new Feat { Name = FeatConstants.WeaponFinesse } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
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

        [Test]
        public void ApplyRockThrowingBonusToRockAttack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "Rock", IsMelee = false, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Dexterity] });

            var feats = new[] { new Feat { Name = FeatConstants.SpecialQualities.RockThrowing, Power = 42 } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Has.Count.EqualTo(1).And.Contains(42));
        }

        [Test]
        public void DoNotApplyRockThrowingBonusToRockAttack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "Rock", IsMelee = false, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Dexterity] });

            var feats = new[] { new Feat { Name = "not " + FeatConstants.SpecialQualities.RockThrowing, Power = 42 } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
        }

        [Test]
        public void DoNotApplyRockThrowingBonusToNonRockAttack()
        {
            var attacks = new List<Attack>();
            attacks.Add(new Attack { Name = "not Rock", IsMelee = false, IsPrimary = true, BaseAbility = abilities[AbilityConstants.Dexterity] });

            var feats = new[] { new Feat { Name = FeatConstants.SpecialQualities.RockThrowing, Power = 42 } };

            var generatedAttacks = attacksGenerator.ApplyAttackBonuses(attacks, feats, abilities);
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.AttackBonuses, Is.Empty);
        }
    }
}
