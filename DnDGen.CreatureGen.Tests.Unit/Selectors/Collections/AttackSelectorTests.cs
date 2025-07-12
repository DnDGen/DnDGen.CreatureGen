using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AttackSelectorTests
    {
        private IAttackSelector attackSelector;
        private Mock<ICollectionDataSelector<AttackDataSelection>> mockAttackDataSelector;
        private Mock<ICollectionDataSelector<DamageDataSelection>> mockDamageDataSelector;

        [SetUp]
        public void Setup()
        {
            mockAttackDataSelector = new Mock<ICollectionDataSelector<AttackDataSelection>>();
            mockDamageDataSelector = new Mock<ICollectionDataSelector<DamageDataSelection>>();
            attackSelector = new AttackSelector(mockAttackDataSelector.Object, mockDamageDataSelector.Object);
        }

        [Test]
        public void SelectNoAttacks()
        {
            var attacks = attackSelector.Select("creature", "size");
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
            var attackData = new[]
            {
                GetData("name", "effect", 4.2, 9266, "time period", "attack type", isNatural, isMelee, isPrimary, isSpecial, "save", "save ability", 90210)
            };

            mockAttackDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var damageData = GetDamageData("my roll", "my damage type");
            var key = attackData[0].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key)).Returns(damageData);

            var attacks = attackSelector.Select("creature", "size");
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
            var attackData = new[]
            {
                GetData("name", "effect", 4.2, 9266, "time period", "attack type", isNatural, isMelee, isPrimary, isSpecial)
            };

            mockAttackDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var damageData = GetDamageData("my roll", "my damage type");
            var key = attackData[0].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key)).Returns(damageData);

            var attacks = attackSelector.Select("creature", "size");
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

        private List<DamageDataSelection> GetDamageData(string roll, string type, string condition = "", string roll2 = "", string type2 = "", string condition2 = "")
        {
            var selections = new List<DamageDataSelection>
            {
                new DamageDataSelection
                {
                    Roll = roll,
                    Type = type,
                    Condition = condition
                }
            };

            if (!string.IsNullOrEmpty(roll2))
            {
                selections.Add(new DamageDataSelection
                {
                    Roll = roll2,
                    Type = type2,
                    Condition = condition2
                });
            }

            return selections;
        }

        private AttackDataSelection GetData(
            string name,
            string damageEffect,
            double damageBonusMultiplier,
            int frequencyQuantity,
            string frequencyTimePeriod,
            string attackType,
            bool isNatural = false,
            bool isMelee = false,
            bool isPrimary = false,
            bool isSpecial = false,
            string save = "",
            string saveAbility = "",
            int saveDcBonus = 0)
        {
            return new AttackDataSelection
            {
                Name = name,
                DamageEffect = damageEffect,
                DamageBonusMultiplier = damageBonusMultiplier,
                FrequencyQuantity = frequencyQuantity,
                FrequencyTimePeriod = frequencyTimePeriod,
                AttackType = attackType,
                IsMelee = isMelee,
                IsNatural = isNatural,
                IsPrimary = isPrimary,
                IsSpecial = isSpecial,
                Save = save,
                SaveAbility = saveAbility,
                SaveDcBonus = saveDcBonus,
            };
        }

        [Test]
        public void SelectAttackWithoutDamage()
        {
            var attackData = new[]
            {
                GetData("name", "effect", 4.2, 9266, "time period", "attack type")
            };

            mockAttackDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var key = attackData[0].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key)).Returns([]);

            var attacks = attackSelector.Select("creature", "size");
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
            var attackData = new[]
            {
                GetData("name", "effect", 4.2, 9266, "time period", "attack type")
            };

            mockAttackDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var damageData = GetDamageData("my roll", "my damage type", "my condition", "my other roll", "my other damage type", "my other condition");
            var key = attackData[0].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key)).Returns(damageData);

            var attacks = attackSelector.Select("creature", "size");
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
            var attackData = new[]
            {
                GetData("name", "my effect", 902.10, 9266, "time period", "attack type", true, false, true, false),
                GetData("other name", string.Empty, 4.2, 600, "other time period", "other attack type", false, true, false, true),
                GetData("third name", string.Empty, 0, 1336, "third time period", "third attack type", true, true, true, false, "my save", "my save ability", 96),
            };

            mockAttackDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var damageData1 = GetDamageData("my roll", "my damage type");
            var key1 = attackData[0].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key1)).Returns(damageData1);

            var damageData2 = GetDamageData("another roll", "another damage type", string.Empty, "my other roll", "my other damage type", "my other condition");
            var key2 = attackData[1].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key2)).Returns(damageData2);

            var key3 = attackData[2].BuildDamageKey("creature", "size");
            mockDamageDataSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key3)).Returns([]);

            var attacks = attackSelector.Select("creature", "size").ToArray();
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
    }
}
