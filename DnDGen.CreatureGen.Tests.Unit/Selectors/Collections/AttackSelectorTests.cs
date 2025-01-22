using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AttackSelectorTests
    {
        private IAttackSelector attackSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private AttackHelper attackHelper;
        private DamageHelper damageHelper;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            attackSelector = new AttackSelector(mockCollectionSelector.Object);
            attackHelper = new AttackHelper();
            damageHelper = new DamageHelper();
        }

        [Test]
        public void SelectNoAttacks()
        {
            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Empty);
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
        public void SelectAttack(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial)
        {
            var damageData = damageHelper.BuildEntries("my roll", "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, "effect", 4.2, 9266, "time period", "attack type", isNatural, isMelee, isPrimary, isSpecial, "save", "save ability", 90210)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
            Assert.That(attack.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.Name, Is.EqualTo("name"));
            Assert.That(attack.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(attack.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Save, Is.EqualTo("save"));
            Assert.That(attack.SaveAbility, Is.EqualTo("save ability"));
            Assert.That(attack.SaveDcBonus, Is.EqualTo(90210));
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
        public void SelectAttackWithoutSave(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial)
        {
            var damageData = damageHelper.BuildEntries("my roll", "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, "effect", 4.2, 9266, "time period", "attack type", isNatural, isMelee, isPrimary, isSpecial)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
            Assert.That(attack.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.IsSpecial, Is.EqualTo(isSpecial));
            Assert.That(attack.Name, Is.EqualTo("name"));
            Assert.That(attack.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(attack.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Save, Is.Empty);
            Assert.That(attack.SaveAbility, Is.Empty);
            Assert.That(attack.SaveDcBonus, Is.Zero);
        }

        private string GetData(
            string name,
            string damageData,
            string damageEffect,
            double damageBonusMultiplier,
            int frequencyQuantity,
            string frequencyTimePeriod,
            string attackType,
            bool isNatural = false,
            bool isMelee = false,
            bool isPrimary = false,
            bool isSpecial = false,
            string save = null,
            string saveAbility = null,
            int saveDcBonus = 0)
        {
            var data = attackHelper.BuildData(
                name,
                damageData,
                damageEffect,
                damageBonusMultiplier,
                attackType,
                frequencyQuantity,
                frequencyTimePeriod,
                isMelee,
                isNatural,
                isPrimary,
                isSpecial,
                save,
                saveAbility,
                saveDcBonus);
            return attackHelper.BuildEntry(data);
        }

        [Test]
        public void SelectAttackWithoutDamage()
        {
            var attackData = new[]
            {
                GetData("name", string.Empty, "effect", 4.2, 9266, "time period", "attack type")
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Is.Empty);
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
            Assert.That(attack.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(attack.IsMelee, Is.False);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.False);
            Assert.That(attack.Name, Is.EqualTo("name"));
            Assert.That(attack.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(attack.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Save, Is.Empty);
            Assert.That(attack.SaveAbility, Is.Empty);
            Assert.That(attack.SaveDcBonus, Is.Zero);
        }

        [Test]
        public void SelectAttackWithMultipleDamages()
        {
            var damageData = damageHelper.BuildEntries("my roll", "my damage type", "my condition", "my other roll", "my other damage type", "my other condition");

            var attackData = new[]
            {
                GetData("name", damageData, "effect", 4.2, 9266, "time period", "attack type")
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(2));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(attack.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attack.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attack.Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attack.DamageEffect, Is.EqualTo("effect"));
            Assert.That(attack.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(attack.IsMelee, Is.False);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.False);
            Assert.That(attack.Name, Is.EqualTo("name"));
            Assert.That(attack.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(attack.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(attack.AttackType, Is.EqualTo("attack type"));
            Assert.That(attack.Save, Is.Empty);
            Assert.That(attack.SaveAbility, Is.Empty);
            Assert.That(attack.SaveDcBonus, Is.Zero);
        }

        [Test]
        public void SelectAttacks()
        {
            var damageData1 = damageHelper.BuildEntries("my roll", "my damage type");
            var damageData2 = damageHelper.BuildEntries("another roll", "another damage type", string.Empty, "my other roll", "my other damage type", "my other condition");

            var attackData = new[]
            {
                GetData("name", damageData1, "my effect", 902.10, 9266, "time period", "attack type", true, false, true, false),
                GetData("other name", damageData2, string.Empty, 4.2, 600, "other time period", "other attack type", false, true, false, true),
                GetData("third name", string.Empty, string.Empty, 0, 1336, "third time period", "third attack type", true, true, true, false, "my save", "my save ability", 96),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size").ToArray();
            Assert.That(attacks, Is.Not.Empty.And.Length.EqualTo(3));

            Assert.That(attacks[0].Damages, Has.Count.EqualTo(1));
            Assert.That(attacks[0].Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(attacks[0].Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attacks[0].Damages[0].Condition, Is.Empty);
            Assert.That(attacks[0].Name, Is.EqualTo("name"));
            Assert.That(attacks[0].DamageEffect, Is.EqualTo("my effect"));
            Assert.That(attacks[0].DamageBonusMultiplier, Is.EqualTo(902.10));
            Assert.That(attacks[0].FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(attacks[0].FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(attacks[0].AttackType, Is.EqualTo("attack type"));
            Assert.That(attacks[0].IsMelee, Is.False);
            Assert.That(attacks[0].IsNatural, Is.True);
            Assert.That(attacks[0].IsPrimary, Is.True);
            Assert.That(attacks[0].IsSpecial, Is.False);
            Assert.That(attacks[0].Save, Is.Empty);
            Assert.That(attacks[0].SaveAbility, Is.Empty);
            Assert.That(attacks[0].SaveDcBonus, Is.Zero);

            Assert.That(attacks[1].Damages, Has.Count.EqualTo(2));
            Assert.That(attacks[1].Damages[0].Roll, Is.EqualTo("another roll"));
            Assert.That(attacks[1].Damages[0].Type, Is.EqualTo("another damage type"));
            Assert.That(attacks[1].Damages[0].Condition, Is.Empty);
            Assert.That(attacks[1].Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(attacks[1].Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(attacks[1].Damages[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(attacks[1].Name, Is.EqualTo("other name"));
            Assert.That(attacks[1].DamageEffect, Is.Empty);
            Assert.That(attacks[1].DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(attacks[1].FrequencyQuantity, Is.EqualTo(600));
            Assert.That(attacks[1].FrequencyTimePeriod, Is.EqualTo("other time period"));
            Assert.That(attacks[1].AttackType, Is.EqualTo("other attack type"));
            Assert.That(attacks[1].IsMelee, Is.True);
            Assert.That(attacks[1].IsNatural, Is.False);
            Assert.That(attacks[1].IsPrimary, Is.False);
            Assert.That(attacks[1].IsSpecial, Is.True);
            Assert.That(attacks[1].Save, Is.Empty);
            Assert.That(attacks[1].SaveAbility, Is.Empty);
            Assert.That(attacks[1].SaveDcBonus, Is.Zero);

            Assert.That(attacks[2].Damages, Is.Empty);
            Assert.That(attacks[2].Name, Is.EqualTo("third name"));
            Assert.That(attacks[2].DamageEffect, Is.Empty);
            Assert.That(attacks[2].DamageBonusMultiplier, Is.Zero);
            Assert.That(attacks[2].FrequencyQuantity, Is.EqualTo(1336));
            Assert.That(attacks[2].FrequencyTimePeriod, Is.EqualTo("third time period"));
            Assert.That(attacks[2].AttackType, Is.EqualTo("third attack type"));
            Assert.That(attacks[2].IsMelee, Is.True);
            Assert.That(attacks[2].IsNatural, Is.True);
            Assert.That(attacks[2].IsPrimary, Is.True);
            Assert.That(attacks[2].IsSpecial, Is.False);
            Assert.That(attacks[2].Save, Is.EqualTo("my save"));
            Assert.That(attacks[2].SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(attacks[2].SaveDcBonus, Is.EqualTo(96));
        }

        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d2", "1d2")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d3", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d4", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d6", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d8", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d10", "1d10")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d8", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d2", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d3", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d4", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d6", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d8", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d10", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d2", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d3", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d4", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d2", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d3", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d4", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d2", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d3", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d2", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d2", "1d2")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d3", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d4", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d6", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d8", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d10", "1d10")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d8", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d2", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d3", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d4", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d6", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d8", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d10", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d2", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d3", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d4", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d2", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d3", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d4", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d2", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d3", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d2", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d2", "1d2")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d3", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d4", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d6", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d8", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d10", "1d10")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d8", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d2", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d3", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d4", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d6", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d8", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d10", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d2", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d3", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d4", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d2", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d3", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d4", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d2", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d3", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d2", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d2", "1d2")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d3", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d4", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d6", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d8", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d10", "1d10")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d8", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d2", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d3", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d4", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d6", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d8", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d10", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d2", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d3", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d4", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d2", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d3", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d4", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d2", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d3", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d2", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d2", "1d2")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d3", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d4", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d6", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d8", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d10", "1d10")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d8", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d2", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d3", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d4", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d6", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d8", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d10", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d2", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d3", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d4", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d2", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d3", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d4", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d2", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d3", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d2", "1d2")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d3", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d4", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d6", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d8", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d10", "1d10")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d8", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d2", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d3", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d4", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d6", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d8", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d10", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d2", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d3", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d4", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d2", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d3", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d4", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d2", "1d2")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d3", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d4", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d6", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d8", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d10", "1d10")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d8", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d2", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d3", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d4", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d6", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d8", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d10", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d2", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d3", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d4", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d2", "1d2")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d3", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d4", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d6", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d8", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d10", "1d10")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d6", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d8", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d2", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d3", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d4", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d6", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d8", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d10", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d2", "1d2")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d3", "1d3")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d4", "1d4")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d6", "1d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d8", "1d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d10", "1d10")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d6", "2d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d8", "2d8")]
        public void AdjustDamageForAdvancedSizeForNaturalAttack(string originalSize, string advancedSize, string originalDamage, string advancedDamage)
        {
            var damageData = damageHelper.BuildEntries(originalDamage, "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, string.Empty, 0, 9266, "time period", "attack type", isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", originalSize, advancedSize);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo(advancedDamage));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [TestCaseSource(nameof(VerboseDamages))]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithVerboseRollDamage(string originalDamageData, string adjustedDamageData)
        {
            var attackData = new[]
            {
                GetData("name", originalDamageData, string.Empty, 0, 9266, "time period", "attack type", isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            var adjustedDamages = damageHelper.ParseEntries(adjustedDamageData);

            Assert.That(attack.Damages, Has.Count.EqualTo(adjustedDamages.Length));

            for (var i = 0; i < adjustedDamages.Length; i++)
            {
                Assert.That(attack.Damages[i].Roll, Is.EqualTo(adjustedDamages[i][DataIndexConstants.AttackData.DamageData.RollIndex]));
                Assert.That(attack.Damages[i].Type, Is.EqualTo(adjustedDamages[i][DataIndexConstants.AttackData.DamageData.TypeIndex]));
                Assert.That(attack.Damages[i].Condition, Is.EqualTo(adjustedDamages[i][DataIndexConstants.AttackData.DamageData.ConditionIndex]));
            }

            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        private static IEnumerable VerboseDamages
        {
            get
            {
                var damageHelper = new DamageHelper();

                var originalDamagesDatas = new[]
                {
                    damageHelper.BuildEntries("1d6", "piercing"),
                    damageHelper.BuildEntries("1d6", "piercing", "sometimes"),
                    damageHelper.BuildEntries("1d6", "bludgeoning", string.Empty, "1d4", "acid", "often"),
                    damageHelper.BuildEntries("1d6", "bludgeoning", "sometimes", "1d4", "acid", string.Empty),
                    damageHelper.BuildEntries("1d6", "bludgeoning", "sometimes", "1d4", "acid", "often"),
                    damageHelper.BuildEntries("1d2", "bludgeoning", string.Empty, "1d10", "acid", "often"),
                    damageHelper.BuildEntries("1d2", "bludgeoning", "sometimes", "1d10", "acid", string.Empty),
                    damageHelper.BuildEntries("1d2", "bludgeoning", "sometimes", "1d10", "acid", "often"),
                };

                var adjustedDamagesDatas = new[]
                {
                    damageHelper.BuildEntries("3d6", "piercing"),
                    damageHelper.BuildEntries("3d6", "piercing", "sometimes"),
                    damageHelper.BuildEntries("3d6", "bludgeoning", string.Empty, "3d6", "acid", "often"),
                    damageHelper.BuildEntries("3d6", "bludgeoning", "sometimes", "3d6", "acid", string.Empty),
                    damageHelper.BuildEntries("3d6", "bludgeoning", "sometimes", "3d6", "acid", "often"),
                    damageHelper.BuildEntries("3d6", "bludgeoning", string.Empty, "3d8", "acid", "often"),
                    damageHelper.BuildEntries("3d6", "bludgeoning", "sometimes", "3d8", "acid", string.Empty),
                    damageHelper.BuildEntries("3d6", "bludgeoning", "sometimes", "3d8", "acid", "often"),
                };

                for (var i = 0; i < originalDamagesDatas.Length; i++)
                {
                    yield return new TestCaseData(originalDamagesDatas[i], adjustedDamagesDatas[i]);
                }
            }
        }

        [TestCase("4d6")]
        [TestCase("4d4")]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithNonAdjustableRollDamage(string originalDamage)
        {
            var damageData = damageHelper.BuildEntries(originalDamage, "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, string.Empty, 0, 9266, "time period", "attack type", isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo(originalDamage));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [Test]
        public void DoNotAdjustDamageForAdvancedSizeForUnnaturalAttack()
        {
            var damageData = damageHelper.BuildEntries("1d2", "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, string.Empty, 0, 9266, "time period", "attack type", isNatural: false)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("1d2"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [Test]
        public void DoNotAdjustEffectRolls()
        {
            var damageData = damageHelper.BuildEntries("1d2", "my damage type");

            var attackData = new[]
            {
                GetData("name", damageData, "1d2", 1, 9266, "time period", "attack type", isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damages, Has.Count.EqualTo(1));
            Assert.That(attack.Damages[0].Roll, Is.EqualTo("3d6"));
            Assert.That(attack.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(attack.Damages[0].Condition, Is.Empty);
            Assert.That(attack.DamageEffect, Is.EqualTo("1d2"));
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }
    }
}
