using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class AttackHelperTests
    {
        private AttackHelper helper;
        private DamageHelper damageHelper;

        [SetUp]
        public void Setup()
        {
            helper = new AttackHelper();
            damageHelper = new DamageHelper();
        }

        [Test]
        public void BuildDataIntoArray_WithEmptyDamageData()
        {
            var data = helper.BuildData(
                "attack name",
                string.Empty,
                "damage effect",
                92.66,
                "attack type",
                90210,
                "time period",
                true,
                true,
                true,
                true);
            Assert.That(data[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("attack name"));
            Assert.That(data[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("damage effect"));
            Assert.That(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(92.66.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("attack type"));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(90210.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(14));
        }

        [TestCase("1d6")]
        [TestCase("1d6 acid")]
        [TestCase("1d6 bludgeoning")]
        [TestCase("damage roll")]
        [TestCase("1d3 + 1d4 fire")]
        public void BuildDataIntoArray_WithIncorrectDamageData(string damageData)
        {
            Assert.That(
                () => helper.BuildData(
                    "attack name",
                    damageData,
                    "damage effect",
                    92.66,
                    "attack type",
                    90210,
                    "time period",
                    true,
                    true,
                    true,
                    true),
                Throws.ArgumentException.With.Message.EqualTo($"Data Damage Entry '{damageData}' is not valid"));
        }

        [Test]
        public void BuildDataIntoArray_WithCorrectDamageData()
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);
            var data = helper.BuildData(
                "attack name",
                damageEntry,
                "damage effect",
                92.66,
                "attack type",
                90210,
                "time period",
                true,
                true,
                true,
                true);
            Assert.That(data[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("attack name"));
            Assert.That(data[DataIndexConstants.AttackData.DamageDataIndex], Is.EqualTo(damageEntry));
            Assert.That(data[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("damage effect"));
            Assert.That(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(92.66.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("attack type"));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(90210.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(14));
        }

        [Test]
        public void BuildDataIntoArray_WithMultipleCorrectDamageData()
        {
            var damageData1 = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry1 = damageHelper.BuildEntry(damageData1);

            var damageData2 = damageHelper.BuildData("my other roll", "my other damage type");
            var damageEntry2 = damageHelper.BuildEntry(damageData2);

            var damageDataEntry = string.Join(AttackSelection.DamageSplitDivider, damageEntry1, damageEntry2);

            var data = helper.BuildData(
                "attack name",
                damageDataEntry,
                "damage effect",
                92.66,
                "attack type",
                90210,
                "time period",
                true,
                true,
                true,
                true);
            Assert.That(data[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("attack name"));
            Assert.That(data[DataIndexConstants.AttackData.DamageDataIndex], Is.EqualTo(damageDataEntry));
            Assert.That(data[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("damage effect"));
            Assert.That(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(92.66.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("attack type"));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(90210.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(14));
        }

        [TestCase(true, true, true, true)]
        [TestCase(true, true, true, false)]
        [TestCase(true, true, false, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, true)]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, true, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, true, false, true)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, true)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, false, true)]
        [TestCase(false, false, false, false)]
        public void BuildDataIntoArray_WithoutSave(bool melee, bool natural, bool primary, bool special)
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = helper.BuildData(
                "attack name",
                damageEntry,
                "damage effect",
                92.66,
                "attack type",
                90210,
                "time period",
                melee,
                natural,
                primary,
                special);
            Assert.That(data[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("attack name"));
            Assert.That(data[DataIndexConstants.AttackData.DamageDataIndex], Is.EqualTo(damageEntry));
            Assert.That(data[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("damage effect"));
            Assert.That(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(92.66.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("attack type"));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(90210.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(melee.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(natural.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(primary.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(special.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(14));
        }

        [TestCase(true, true, true, true)]
        [TestCase(true, true, true, false)]
        [TestCase(true, true, false, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, true)]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, true, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, true, false, true)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, true)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, false, true)]
        [TestCase(false, false, false, false)]
        public void BuildDataIntoArray_WithSave(bool melee, bool natural, bool primary, bool special)
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = helper.BuildData(
                "attack name",
                damageEntry,
                "damage effect",
                92.66,
                "attack type",
                90210,
                "time period",
                melee,
                natural,
                primary,
                special,
                "my save",
                "save ability",
                1336);
            Assert.That(data[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("attack name"));
            Assert.That(data[DataIndexConstants.AttackData.DamageDataIndex], Is.EqualTo(damageEntry));
            Assert.That(data[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("damage effect"));
            Assert.That(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(92.66.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("attack type"));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(90210.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(melee.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(natural.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(primary.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(special.ToString()));
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.EqualTo("save ability"));
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(data[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(1336.ToString()));
            Assert.That(data.Length, Is.EqualTo(14));
        }

        [Test]
        public void BuildDataIntoString()
        {
            var data = new[] { "this", "is", "my", "data" };
            var dataString = helper.BuildEntry(data);
            Assert.That(dataString, Is.EqualTo("this@is@my@data"));
        }

        [Test]
        public void ParseData()
        {
            var data = helper.ParseEntry("this@is@my@data");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data" }));
        }

        [TestCase(true, "", "")]
        [TestCase(true, "", "my effect")]
        [TestCase(true, "my roll", "")]
        [TestCase(true, "my roll", "my effect")]
        [TestCase(false, "", "")]
        [TestCase(false, "", "my effect")]
        [TestCase(false, "my roll", "")]
        [TestCase(false, "my roll", "my effect")]
        public void BuildKey_FromData(bool primary, string roll, string effect)
        {
            var damageData = damageHelper.BuildData(roll, "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = helper.ParseEntry($"My Attack@{damageEntry}@True@True@{primary}@False@1@Round (6 seconds)@@@melee@{effect}@1.5@0");
            var key = helper.BuildKey("creature", data);
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{damageEntry}{effect}"));
        }

        [TestCase(true, "", "")]
        [TestCase(true, "", "my effect")]
        [TestCase(true, "my roll", "")]
        [TestCase(true, "my roll", "my effect")]
        [TestCase(false, "", "")]
        [TestCase(false, "", "my effect")]
        [TestCase(false, "my roll", "")]
        [TestCase(false, "my roll", "my effect")]
        public void BuildKey_FromString_WithDamageEffect(bool primary, string roll, string effect)
        {
            var damageData = damageHelper.BuildData(roll, "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var key = helper.BuildKey("creature", $"My Attack@{damageEntry}@True@True@{primary}@False@1@Round (6 seconds)@@@melee@{effect}@1.5@0");
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{damageEntry}{effect}"));
        }

        [TestCase(true, "", "")]
        [TestCase(true, "", "my effect")]
        [TestCase(true, "my roll", "")]
        [TestCase(true, "my roll", "my effect")]
        [TestCase(false, "", "")]
        [TestCase(false, "", "my effect")]
        [TestCase(false, "my roll", "")]
        [TestCase(false, "my roll", "my effect")]
        public void BuildKeyFromSections(bool primary, string roll, string effect)
        {
            var damageData = damageHelper.BuildData(roll, "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var key = helper.BuildKeyFromSections("creature", "My Attack", primary.ToString(), damageEntry, effect);
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{damageEntry}{effect}"));
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(7, false)]
        [TestCase(8, false)]
        [TestCase(9, false)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        [TestCase(12, false)]
        [TestCase(13, false)]
        [TestCase(14, true)]
        [TestCase(15, false)]
        [TestCase(16, false)]
        [TestCase(17, false)]
        [TestCase(18, false)]
        [TestCase(19, false)]
        [TestCase(20, false)]
        public void ValidateEntry_IsValid(int length, bool isValid)
        {
            var data = new string[length];
            var entry = helper.BuildEntry(data);
            var valid = helper.ValidateEntry(entry);
            Assert.That(valid, Is.EqualTo(isValid));
        }
    }
}
