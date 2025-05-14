using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
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
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IAttackSelector> mockAttackSelector;
        private HitPoints hitPoints;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockAttackSelector = new Mock<IAttackSelector>();

            attacksGenerator = new AttacksGenerator(mockTypeAndAmountSelector.Object, mockAttackSelector.Object);

            hitPoints = new HitPoints();

            abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.SizeModifiers, It.IsAny<string>()))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 0 });
        }

        [TestCase(0, BaseAttackQuality.Good, 0)]
        [TestCase(1, BaseAttackQuality.Good, 1)]
        [TestCase(2, BaseAttackQuality.Good, 2)]
        [TestCase(3, BaseAttackQuality.Good, 3)]
        [TestCase(4, BaseAttackQuality.Good, 4)]
        [TestCase(5, BaseAttackQuality.Good, 5)]
        [TestCase(6, BaseAttackQuality.Good, 6)]
        [TestCase(7, BaseAttackQuality.Good, 7)]
        [TestCase(8, BaseAttackQuality.Good, 8)]
        [TestCase(9, BaseAttackQuality.Good, 9)]
        [TestCase(10, BaseAttackQuality.Good, 10)]
        [TestCase(11, BaseAttackQuality.Good, 11)]
        [TestCase(12, BaseAttackQuality.Good, 12)]
        [TestCase(13, BaseAttackQuality.Good, 13)]
        [TestCase(14, BaseAttackQuality.Good, 14)]
        [TestCase(15, BaseAttackQuality.Good, 15)]
        [TestCase(16, BaseAttackQuality.Good, 16)]
        [TestCase(17, BaseAttackQuality.Good, 17)]
        [TestCase(18, BaseAttackQuality.Good, 18)]
        [TestCase(19, BaseAttackQuality.Good, 19)]
        [TestCase(20, BaseAttackQuality.Good, 20)]
        [TestCase(9266, BaseAttackQuality.Good, 9266)]
        [TestCase(0, BaseAttackQuality.Average, 0)]
        [TestCase(1, BaseAttackQuality.Average, 0)]
        [TestCase(2, BaseAttackQuality.Average, 1)]
        [TestCase(3, BaseAttackQuality.Average, 2)]
        [TestCase(4, BaseAttackQuality.Average, 3)]
        [TestCase(5, BaseAttackQuality.Average, 3)]
        [TestCase(6, BaseAttackQuality.Average, 4)]
        [TestCase(7, BaseAttackQuality.Average, 5)]
        [TestCase(8, BaseAttackQuality.Average, 6)]
        [TestCase(9, BaseAttackQuality.Average, 6)]
        [TestCase(10, BaseAttackQuality.Average, 7)]
        [TestCase(11, BaseAttackQuality.Average, 8)]
        [TestCase(12, BaseAttackQuality.Average, 9)]
        [TestCase(13, BaseAttackQuality.Average, 9)]
        [TestCase(14, BaseAttackQuality.Average, 10)]
        [TestCase(15, BaseAttackQuality.Average, 11)]
        [TestCase(16, BaseAttackQuality.Average, 12)]
        [TestCase(17, BaseAttackQuality.Average, 12)]
        [TestCase(18, BaseAttackQuality.Average, 13)]
        [TestCase(19, BaseAttackQuality.Average, 14)]
        [TestCase(20, BaseAttackQuality.Average, 15)]
        [TestCase(9266, BaseAttackQuality.Average, 6949)]
        [TestCase(0, BaseAttackQuality.Poor, 0)]
        [TestCase(1, BaseAttackQuality.Poor, 0)]
        [TestCase(2, BaseAttackQuality.Poor, 1)]
        [TestCase(3, BaseAttackQuality.Poor, 1)]
        [TestCase(4, BaseAttackQuality.Poor, 2)]
        [TestCase(5, BaseAttackQuality.Poor, 2)]
        [TestCase(6, BaseAttackQuality.Poor, 3)]
        [TestCase(7, BaseAttackQuality.Poor, 3)]
        [TestCase(8, BaseAttackQuality.Poor, 4)]
        [TestCase(9, BaseAttackQuality.Poor, 4)]
        [TestCase(10, BaseAttackQuality.Poor, 5)]
        [TestCase(11, BaseAttackQuality.Poor, 5)]
        [TestCase(12, BaseAttackQuality.Poor, 6)]
        [TestCase(13, BaseAttackQuality.Poor, 6)]
        [TestCase(14, BaseAttackQuality.Poor, 7)]
        [TestCase(15, BaseAttackQuality.Poor, 7)]
        [TestCase(16, BaseAttackQuality.Poor, 8)]
        [TestCase(17, BaseAttackQuality.Poor, 8)]
        [TestCase(18, BaseAttackQuality.Poor, 9)]
        [TestCase(19, BaseAttackQuality.Poor, 9)]
        [TestCase(20, BaseAttackQuality.Poor, 10)]
        [TestCase(9266, BaseAttackQuality.Poor, 4633)]
        internal void GenerateBaseAttackBonus(int hitDiceQuantity, BaseAttackQuality bonusQuality, int bonus)
        {
            Assert.Fail("this should fail, if nunit can see internal methods");
            hitPoints.HitDice.Add(new HitDice { Quantity = hitDiceQuantity });

            var baseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(bonusQuality, hitPoints);
            Assert.That(baseAttackBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void GenerateGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.GrappleBonuses, "size"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 42 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.GrappleBonuses, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 600 });

            var grappleBonus = attacksGenerator.GenerateGrappleBonus("creature", "size", 9266, abilities[AbilityConstants.Strength]);
            Assert.That(grappleBonus, Is.EqualTo(9266 + abilities[AbilityConstants.Strength].Modifier + 42 + 600));
        }

        [Test]
        public void GenerateNegativeGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.GrappleBonuses, "size"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = -1 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.GrappleBonuses, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 0 });

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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
                    DamageEffect = "damage effect",
                    AttackType = "attack type",
                    FrequencyQuantity = 42,
                    FrequencyTimePeriod = "time period",
                    IsMelee = isMelee,
                    IsNatural = isNatural,
                    IsPrimary = isPrimary,
                    IsSpecial = isSpecial
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 90210, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "good attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
                    DamageEffect = "damage effect",
                    AttackType = "attack type",
                    FrequencyQuantity = 42,
                    FrequencyTimePeriod = "time period",
                },
                new()
                {
                    Name = "gendered attack",
                    Damages =
                    [
                        new() { Roll = "my gendered roll", Type = "my gendered damage type", Condition = "my gendered condition" },
                        new() { Roll = "my other gendered roll", Type = "my other gendered damage type", Condition = "my other gendered condition" },
                    ],
                    DamageEffect = "gendered damage effect",
                    AttackType = "gendered attack type",
                    FrequencyQuantity = 600,
                    FrequencyTimePeriod = "gendered time period",
                    RequiredGender = "required gender"
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 90210, "gender");
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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "good attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
                    DamageEffect = "damage effect",
                    AttackType = "attack type",
                    FrequencyQuantity = 42,
                    FrequencyTimePeriod = "time period",
                },
                new()
                {
                    Name = "gendered attack",
                    Damages =
                    [
                        new() { Roll = "my gendered roll", Type = "my gendered damage type", Condition = "my gendered condition" },
                        new() { Roll = "my other gendered roll", Type = "my other gendered damage type", Condition = "my other gendered condition" },
                    ],
                    DamageEffect = "gendered damage effect",
                    AttackType = "gendered attack type",
                    FrequencyQuantity = 600,
                    FrequencyTimePeriod = "gendered time period",
                    RequiredGender = "required gender"
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 90210, "required gender");
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

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void GenerateAttack_WithSave(string saveAbility)
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
                    DamageEffect = "damage effect",
                    AttackType = "attack type",
                    FrequencyQuantity = 42,
                    FrequencyTimePeriod = "time period",
                    IsNatural = true,
                    Save = "save",
                    SaveAbility = saveAbility,
                    SaveDcBonus = 1337
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

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
            Assert.That(attack.IsNatural, Is.True);
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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
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
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
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
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

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

        [Test]
        public void GenerateAttack_WithSave()
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type", Condition = "my condition" },
                        new() { Roll = "my other roll", Type = "my other damage type", Condition = "my other condition" },
                    ],
                    DamageEffect = "damage effect",
                    AttackType = "attack type",
                    FrequencyQuantity = 42,
                    FrequencyTimePeriod = "time period",
                    Save = "save",
                    SaveAbility = abilities.Keys.First(),
                    SaveDcBonus = 600,
                    IsNatural = true,
                }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 1337, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

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
            Assert.That(attack.Save.BaseValue, Is.EqualTo(10 + 600 + 1337 / 2));
            Assert.That(attack.Save.BaseAbility, Is.EqualTo(abilities.Values.First()));
            Assert.That(attack.Save.Save, Is.EqualTo("save"));
        }

        [Test]
        public void GenerateAttacks()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack", Damages = [new() { Roll = "my roll", Type = "my type" }] },
                new() { Name = "other attack", Damages = [new() { Roll = "my other roll", Type = "my other type" }] }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Not.Empty);
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(2));

            var attack = generatedAttacks.First();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll my type"));

            attack = generatedAttacks.Last();
            Assert.That(attack.Name, Is.EqualTo("other attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my other roll my other type"));
        }

        [Test]
        public void GenerateNoAttacks()
        {
            var attacks = new List<AttackDataSelection>();

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks, Is.Empty);
        }

        [Test]
        public void GenerateAttackBaseAttackBonus()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1" }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbility()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1", IsMelee = true }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]));
        }

        [Test]
        public void GenerateRangedAttackBaseAbility()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1", IsMelee = false }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateMeleeAttackBaseAbilityWithNoStrength()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1", IsMelee = true }
            };

            abilities[AbilityConstants.Strength].BaseScore = 0;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
        }

        [Test]
        public void GenerateAttackSizeModifier()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1" }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.SizeModifiers, "size"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 90210 });

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.SizeModifier, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateSpecialAttack()
        {
            var attacks = new List<AttackDataSelection>
            {
                new() { Name = "attack 1", IsMelee = true, IsSpecial = true }
            };

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.SizeModifiers, "size"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 90210 });

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            var generatedAttack = generatedAttacks.Single();

            Assert.That(generatedAttack.BaseAttackBonus, Is.EqualTo(9266));
            Assert.That(generatedAttack.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Strength]), generatedAttack.BaseAbility?.Name);
            Assert.That(generatedAttack.SizeModifier, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateAttack_WithDamageMultiplier()
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my damage type" },
                        new() { Roll = "my other roll", Type = "my other damage type" },
                    ],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = true
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 1337;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            //1327 / 2 = 663, 663 * 1.5 = 994
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo($"my roll+994 my damage type + my other roll my other damage type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(994));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Breath()
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages = [new() { Roll = "my roll", Type = "my type" }],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 0,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = false,
                    IsNatural = true
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll my type plus effect"));
            Assert.That(attack.DamageBonus, Is.Zero);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_Ranged_Thrown()
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages = [new() { Roll = "my roll", Type = "my type" }],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = false,
                    IsNatural = true
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll+67650 my type plus effect"));
            Assert.That(attack.DamageBonus, Is.EqualTo(67650));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Primary_AllSole()
        {
            var attackSelections = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "nat melee attack",
                    Damages = [new() { Roll = "my nat melee roll", Type = "my nat melee type" }],
                    DamageEffect = "nat melee effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = true
                },
                new()
                {
                    Name = "nat range attack",
                    Damages = [new() { Roll = "my nat range roll", Type = "my nat range type" }],
                    DamageEffect = "nat range effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = false,
                    IsNatural = true
                },
                new()
                {
                    Name = "melee attack",
                    Damages = [new() { Roll = "my melee roll", Type = "my melee type" }],
                    DamageEffect = "melee effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = false
                },
                new()
                {
                    Name = "range attack",
                    Damages = [new() { Roll = "my range roll", Type = "my range type" }],
                    DamageEffect = "range effect",
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = false,
                    IsNatural = false
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(4));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("nat melee attack"));
            Assert.That(attacks[0].IsMelee, Is.True);
            Assert.That(attacks[0].IsNatural, Is.True);
            Assert.That(attacks[0].DamageSummary, Is.EqualTo("my nat melee roll+67650 my nat melee type plus nat melee effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("nat melee effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("nat range attack"));
            Assert.That(attacks[1].IsMelee, Is.False);
            Assert.That(attacks[1].IsNatural, Is.True);
            Assert.That(attacks[1].DamageSummary, Is.EqualTo("my nat range roll+67650 my nat range type plus nat range effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("nat range effect"));
            Assert.That(attacks[2].Name, Is.EqualTo("melee attack"));
            Assert.That(attacks[2].IsMelee, Is.True);
            Assert.That(attacks[2].IsNatural, Is.False);
            Assert.That(attacks[2].DamageSummary, Is.EqualTo("my melee roll+67650 my melee type plus melee effect"));
            Assert.That(attacks[2].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[2].DamageEffect, Is.EqualTo("melee effect"));
            Assert.That(attacks[3].Name, Is.EqualTo("range attack"));
            Assert.That(attacks[3].IsMelee, Is.False);
            Assert.That(attacks[3].IsNatural, Is.False);
            Assert.That(attacks[3].DamageSummary, Is.EqualTo("my range roll+67650 my range type plus range effect"));
            Assert.That(attacks[3].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[3].DamageEffect, Is.EqualTo("range effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_Multiple(bool isNatural)
        {
            var attackSelections = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages = [new() { Roll = "my roll", Type = "my type" }],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 1,
                    FrequencyQuantity = 2,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = isNatural
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(1));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageSummary, Is.EqualTo("my roll+45100 my type plus effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("effect"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateAttack_Primary_WithSecondary(bool isNatural)
        {
            var attackSelections = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "primary attack",
                    Damages = [new() { Roll = "my primary roll", Type = "my primary type" }],
                    DamageEffect = "primary effect",
                    DamageBonusMultiplier = 1,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = isNatural
                },
                new()
                {
                    Name = "secondary attack",
                    Damages = [new() { Roll = "my secondary roll", Type = "my secondary type" }],
                    DamageEffect = "secondary effect",
                    DamageBonusMultiplier = 0.5,
                    FrequencyQuantity = 1,
                    IsPrimary = false,
                    IsMelee = true,
                    IsNatural = isNatural
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("primary attack"));
            Assert.That(attacks[0].DamageSummary, Is.EqualTo("my primary roll+45100 my primary type plus primary effect"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("primary effect"));
            Assert.That(attacks[1].Name, Is.EqualTo("secondary attack"));
            Assert.That(attacks[1].DamageSummary, Is.EqualTo("my secondary roll+22550 my secondary type plus secondary effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(22550));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("secondary effect"));
        }

        [Test]
        public void GenerateAttack_Primary_SoleAndMultiple()
        {
            var attackSelections = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages = [new() { Roll = "my roll", Type = "my type" }],
                    DamageBonusMultiplier = 1.5,
                    FrequencyQuantity = 1,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = false
                },
                new()
                {
                    Name = "nat attack",
                    Damages = [new() { Roll = "my nat roll", Type = "my nat type" }],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 1,
                    FrequencyQuantity = 2,
                    IsPrimary = true,
                    IsMelee = true,
                    IsNatural = true
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attackSelections);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attackSelections.Count()).And.EqualTo(2));

            var attacks = generatedAttacks.ToArray();
            Assert.That(attacks[0].Name, Is.EqualTo("attack"));
            Assert.That(attacks[0].DamageSummary, Is.EqualTo("my roll+67650 my type"));
            Assert.That(attacks[0].DamageBonus, Is.EqualTo(67650));
            Assert.That(attacks[0].DamageEffect, Is.Empty);
            Assert.That(attacks[1].Name, Is.EqualTo("nat attack"));
            Assert.That(attacks[1].DamageSummary, Is.EqualTo("my nat roll+45100 my nat type plus effect"));
            Assert.That(attacks[1].DamageBonus, Is.EqualTo(45100));
            Assert.That(attacks[1].DamageEffect, Is.EqualTo("effect"));
        }

        [Test]
        public void GenerateAttack_Secondary()
        {
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages = [new() { Roll = "my roll", Type = "my type" }],
                    DamageEffect = "effect",
                    DamageBonusMultiplier = 0.5,
                    FrequencyQuantity = 1,
                    IsPrimary = false,
                    IsMelee = true,
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo("my roll+22550 my type plus effect"));
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
            var attacks = new List<AttackDataSelection>
            {
                new()
                {
                    Name = "attack",
                    Damages =
                    [
                        new() { Roll = "my roll", Type = "my type" },
                        new() { Roll = "1d4", Type = ability },
                    ],
                    DamageEffect = $"{ability} drain",
                    DamageBonusMultiplier = 0.5,
                    FrequencyQuantity = 1,
                    IsMelee = true,
                }
            };

            abilities[AbilityConstants.Strength].BaseScore = 90210;

            mockAttackSelector.Setup(s => s.Select("creature", "size")).Returns(attacks);

            var generatedAttacks = attacksGenerator.GenerateAttacks("creature", "size", 9266, abilities, 600, "gender");
            Assert.That(generatedAttacks.Count, Is.EqualTo(attacks.Count()).And.EqualTo(1));

            var attack = generatedAttacks.Single();
            Assert.That(attack.Name, Is.EqualTo("attack"));
            Assert.That(attack.DamageSummary, Is.EqualTo($"my roll+22550 my type + 1d4 {ability} plus {ability} drain"));
            Assert.That(attack.DamageBonus, Is.EqualTo(22550));
            Assert.That(attack.DamageEffect, Is.EqualTo($"{ability} drain"));
        }

        [Test]
        public void ApplyPrimaryAttackBonuses()
        {
            var attacks = new List<Attack>
            {
                new() { Name = "attack 1", IsPrimary = true }
            };

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
