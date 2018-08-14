using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class SpecialQualityHelperTests
    {
        [Test]
        public void BuildDataIntoArray()
        {
            var data = SpecialQualityHelper.BuildData("feat name", "focus", 9266, "time period", 600, 1337, false);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("focus"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(9266.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(600.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(1337.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data.Length, Is.EqualTo(7));
        }

        [Test]
        public void BuildDataIntoArrayAndRequiringEquipment()
        {
            var data = SpecialQualityHelper.BuildData("feat name", "focus", 9266, "time period", 600, 1337, true);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("focus"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(9266.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(600.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(1337.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(true.ToString()));
            Assert.That(data.Length, Is.EqualTo(7));
        }

        [Test]
        public void BuildDataIntoArrayWithDefaults()
        {
            var data = SpecialQualityHelper.BuildData("feat name");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data.Length, Is.EqualTo(7));
        }

        [Test]
        public void BuildDataIntoString()
        {
            var data = new[] { "this", "is", "my", "data" };
            var dataString = SpecialQualityHelper.BuildData(data);
            Assert.That(dataString, Is.EqualTo("this#is#my#data"));
        }

        [Test]
        public void BuildDataIntoStringWithDefaults()
        {
            var data = new[] { "this", "is", "my", "data", "with", "0", "defaults", string.Empty, string.Empty };
            var dataString = SpecialQualityHelper.BuildData(data);
            Assert.That(dataString, Is.EqualTo("this#is#my#data#with#0#defaults##"));
        }

        [Test]
        public void ParseData()
        {
            var data = SpecialQualityHelper.ParseData("this#is#my#data");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data" }));
        }

        [Test]
        public void ParseDataWithDefaults()
        {
            var data = SpecialQualityHelper.ParseData("this#is#my#data#with#0#defaults##");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data", "with", "0", "defaults", string.Empty, string.Empty }));
        }

        [Test]
        public void BuildRealData()
        {
            var data = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day);
            var entry = SpecialQualityHelper.BuildData(data);
            Assert.That(entry, Is.EqualTo("Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False"));
        }

        [Test]
        public void BuildRealDataRequiringEquipment()
        {
            var data = SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true);
            var entry = SpecialQualityHelper.BuildData(data);
            Assert.That(entry, Is.EqualTo("Simple Weapon Proficiency#0#All#0##0#True"));
        }

        [Test]
        public void BuildRealDataWithDefaults()
        {
            var data = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision);
            var entry = SpecialQualityHelper.BuildData(data);
            Assert.That(entry, Is.EqualTo("All-Around Vision#0##0##0#False"));
        }

        [Test]
        public void ParseRealData()
        {
            var data = SpecialQualityHelper.ParseData("Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.SpecialQualities.SpellLikeAbility));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo(SpellConstants.ClairaudienceClairvoyance));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(3.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Day));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
        }

        [Test]
        public void ParseRealDataRequiringEquipment()
        {
            var data = SpecialQualityHelper.ParseData("Simple Weapon Proficiency#0#All#0##0#True");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.WeaponProficiency_Simple));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo(GroupConstants.All));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(true.ToString()));
        }

        [Test]
        public void ParseRealDataWithDefaults()
        {
            var data = SpecialQualityHelper.ParseData("All-Around Vision#0##0##0#False");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.SpecialQualities.AllAroundVision));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
        }
    }
}
