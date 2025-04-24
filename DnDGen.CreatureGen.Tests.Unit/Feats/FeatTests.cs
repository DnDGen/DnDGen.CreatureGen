using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Feats
{
    [TestFixture]
    public class FeatTests
    {
        private Feat feat;

        [SetUp]
        public void Setup()
        {
            feat = new Feat();
        }

        [Test]
        public void FeatInitialized()
        {
            Assert.That(feat.Name, Is.Not.Null);
            Assert.That(feat.Foci, Is.Empty);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void GetSummary_ReturnsSummary()
        {
            feat.Name = "my feat";

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat"));
        }

        [Test]
        public void GetSummary_ReturnsSummary_WithFocus()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus"];

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat (my focus)"));
        }

        [Test]
        public void GetSummary_ReturnsSummary_WithFoci()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat (my focus/my other focus)"));
        }

        [Test]
        public void GetSummary_ReturnsSummary_WithSortedFoci()
        {
            feat.Name = "my feat";
            feat.Foci = ["focus abc", "focus zyx", "focus YZ", "focus BCD"];

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat (focus abc/focus BCD/focus YZ/focus zyx)"));
        }

        [Test]
        public void GetSummary_ReturnsSummary_WithPower()
        {
            feat.Name = "my feat";
            feat.Power = 9266;

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat, Power 9266"));
        }

        [Test]
        public void GetSummary_ReturnsSummary_WithAll()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var summary = feat.GetSummary();
            Assert.That(summary, Is.EqualTo("my feat (my focus/my other focus), Power 9266"));
        }

        [Test]
        public void ToString_ReturnsSummary()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            Assert.That(feat.ToString(), Is.EqualTo(feat.GetSummary()));
        }

        [Test]
        public void GetHashCode_ReturnsSummaryHasCode()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            Assert.That(feat.GetHashCode(), Is.EqualTo(feat.GetSummary().GetHashCode()));
        }

        [Test]
        public void Equals_ReturnsFalse_WhenObjectIsNull()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            Assert.That(feat.Equals(null), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenObjectIsNotFeat()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var notFeat = new object();
            Assert.That(feat.Equals(notFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenSummaryNotEqual_DifferentName()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = "my other feat", Foci = feat.Foci, Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenSummaryNotEqual_DifferentFocus()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = ["my focus", "wrong focus"], Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenSummaryNotEqual_MissingFocus()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = ["my focus"], Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenSummaryNotEqual_ExtraFocus()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = ["my focus", "my other focus", "wrong focus"], Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenSummaryEqual_FociInDifferentOrder()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = ["my other focus", "my focus"], Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenSummaryEqual_NoFoci()
        {
            feat.Name = "my feat";
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenSummaryNotEqual_DifferentPower()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = feat.Foci, Power = 90210 };
            Assert.That(feat.Equals(otherFeat), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenSummaryEqual_JustName()
        {
            feat.Name = "my feat";

            var otherFeat = new Feat { Name = feat.Name };
            Assert.That(feat.Equals(otherFeat), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenSummaryEqual()
        {
            feat.Name = "my feat";
            feat.Foci = ["my focus", "my other focus"];
            feat.Power = 9266;

            var otherFeat = new Feat { Name = feat.Name, Foci = feat.Foci, Power = feat.Power };
            Assert.That(feat.Equals(otherFeat), Is.True);
        }

        [Test]
        public void From_FeatDataSelection_ReturnsFeat()
        {
            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                CanBeTakenMultipleTimes = true,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
            };

            var feat = Feat.From(selection);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.True);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void From_FeatDataSelection_ReturnsFeat_MultipleTimes(bool multiple)
        {
            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                CanBeTakenMultipleTimes = multiple,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
            };

            var feat = Feat.From(selection);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.EqualTo(multiple));
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_FeatDataSelection_ReturnsFeat_NoPower()
        {
            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                CanBeTakenMultipleTimes = true,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
            };

            var feat = Feat.From(selection);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.True);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_FeatDataSelection_ReturnsFeat_NoFrequency()
        {
            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                CanBeTakenMultipleTimes = true,
            };

            var feat = Feat.From(selection);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.True);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_FeatDataSelection_ReturnsFeat_JustName()
        {
            var selection = new FeatDataSelection
            {
                Feat = "my feat",
            };

            var feat = Feat.From(selection);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_SpecialQualityDataSelection_ReturnsFeat()
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
            };
            var abilities = new Dictionary<string, Ability>
            {
                ["my ability"] = new Ability("my ability"),
                ["my other ability"] = new Ability("my other ability")
            };

            var feat = Feat.From(selection, abilities);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_SpecialQualityDataSelection_ReturnsFeat_WithSave()
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
                SaveAbility = "my other ability",
                SaveBaseValue = 42,
                Save = "my save",
            };
            var abilities = new Dictionary<string, Ability>
            {
                ["my ability"] = new Ability("my ability"),
                ["my other ability"] = new Ability("my other ability")
            };

            var feat = Feat.From(selection, abilities);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Not.Null);
            Assert.That(feat.Save.Save, Is.EqualTo("my save"));
            Assert.That(feat.Save.BaseAbility, Is.EqualTo(abilities["my other ability"]));
            Assert.That(feat.Save.BaseValue, Is.EqualTo(42));
        }

        [Test]
        public void From_SpecialQualityDataSelection_ReturnsFeat_NoPower()
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = "my feat",
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
            };
            var abilities = new Dictionary<string, Ability>
            {
                ["my ability"] = new Ability("my ability"),
                ["my other ability"] = new Ability("my other ability")
            };

            var feat = Feat.From(selection, abilities);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(90210));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("my time period"));
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_SpecialQualityDataSelection_ReturnsFeat_NoFrequency()
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = "my feat",
                Power = 9266,
            };
            var abilities = new Dictionary<string, Ability>
            {
                ["my ability"] = new Ability("my ability"),
                ["my other ability"] = new Ability("my other ability")
            };

            var feat = Feat.From(selection, abilities);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void From_SpecialQualityDataSelection_ReturnsFeat_JustName()
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = "my feat",
            };
            var abilities = new Dictionary<string, Ability>
            {
                ["my ability"] = new Ability("my ability"),
                ["my other ability"] = new Ability("my other ability")
            };

            var feat = Feat.From(selection, abilities);
            Assert.That(feat.Name, Is.EqualTo("my feat"));
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.Frequency, Is.Not.Null);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.Save, Is.Null);
        }

        [Test]
        public void FociMatch_ReturnTrue()
        {
            feat.Frequency.TimePeriod = string.Empty;
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = ["my focus"];

            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FocusType = "my focus type",
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.True);
        }

        [Test]
        public void FociMatch_ReturnFalse_WhenFeatHasTimePeriod()
        {
            feat.Frequency.TimePeriod = "my time period";
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = ["my focus"];

            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FocusType = "my focus type",
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.False);
        }

        [Test]
        public void FociMatch_ReturnFalse_WhenNamesDiffer()
        {
            feat.Frequency.TimePeriod = string.Empty;
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = ["my focus"];

            var selection = new FeatDataSelection
            {
                Feat = "my other feat",
                Power = 9266,
                FocusType = "my focus type",
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.False);
        }

        [Test]
        public void FociMatch_ReturnFalse_WhenPowerDiffers()
        {
            feat.Frequency.TimePeriod = string.Empty;
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = ["my focus"];

            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 90210,
                FocusType = "my focus type",
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.False);
        }

        [Test]
        public void FociMatch_ReturnFalse_WhenNoFoci()
        {
            feat.Frequency.TimePeriod = string.Empty;
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = [];

            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FocusType = "my focus type",
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.False);
        }

        [Test]
        public void FociMatch_ReturnFalse_WhenNoFocusType()
        {
            feat.Frequency.TimePeriod = string.Empty;
            feat.Name = "my feat";
            feat.Power = 9266;
            feat.Foci = ["my focus"];

            var selection = new FeatDataSelection
            {
                Feat = "my feat",
                Power = 9266,
                FocusType = string.Empty,
            };

            var fociMatch = feat.FociMatch(selection);
            Assert.That(fociMatch, Is.False);
        }
    }
}