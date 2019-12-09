using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class AttackHelperTests
    {
        private AttackHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new AttackHelper();
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
            var data = helper.BuildData(
                "attack name",
                "damage roll",
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
            Assert.That(data[DataIndexConstants.AttackData.DamageRollIndex], Is.EqualTo("damage roll"));
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

            var data = helper.BuildData(
                "attack name",
                "damage roll",
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
            Assert.That(data[DataIndexConstants.AttackData.DamageRollIndex], Is.EqualTo("damage roll"));
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
            var data = helper.ParseEntry($"My Attack@{roll}@True@True@{primary}@False@1@Round (6 seconds)@@@melee@{effect}@1.5@0");
            var key = helper.BuildKey("creature", data);
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{roll}{effect}"));
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
            var key = helper.BuildKey("creature", $"My Attack@{roll}@True@True@{primary}@False@1@Round (6 seconds)@@@melee@{effect}@1.5@0");
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{roll}{effect}"));
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
            var key = helper.BuildKeyFromSections("creature", "My Attack", primary.ToString(), roll, effect);
            Assert.That(key, Is.EqualTo($"creatureMy Attack{primary}{roll}{effect}"));
        }
    }
}
