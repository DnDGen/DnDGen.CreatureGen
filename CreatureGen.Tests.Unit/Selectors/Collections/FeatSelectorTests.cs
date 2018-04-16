using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class FeatSelectorTests
    {
        private const string creature = "creature";

        private IFeatsSelector featsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<ITypeAndAmountSelector> mockTypesAndAmountsSelector;
        private Dictionary<string, IEnumerable<string>> featsData;
        private List<string> specialQualitiesData;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockTypesAndAmountsSelector = new Mock<ITypeAndAmountSelector>();
            featsSelector = new FeatsSelector(mockCollectionsSelector.Object, mockTypesAndAmountsSelector.Object);

            featsData = new Dictionary<string, IEnumerable<string>>();
            specialQualitiesData = new List<string>();

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, It.IsAny<string>()))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.FeatData)).Returns(featsData);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialQualityData, "creature")).Returns(specialQualitiesData);
        }

        [Test]
        public void GetSpecialQuality()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetNoSpecialQualities()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialQualityData, "creature")).Returns(Enumerable.Empty<string>());

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualities()
        {
            AddSpecialQualityData("special quality 1", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);
            AddSpecialQualityData("special quality 2", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(2));

            var first = specialQualities.First();
            var last = specialQualities.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality 1"));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(first.MinimumAbilities, Is.Empty);
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(first.Power, Is.EqualTo(0));
            Assert.That(first.RandomFociQuantity, Is.Empty);
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.SizeRequirement, Is.Empty);

            Assert.That(last.Feat, Is.EqualTo("special quality 2"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.MinimumAbilities, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Power, Is.EqualTo(0));
            Assert.That(last.RandomFociQuantity, Is.Empty);
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.SizeRequirement, Is.Empty);
        }

        private void AddSpecialQualityData(string featName, string focus, int frequencyQuantity, string frequencyTimePeriod, int maxHitDice, int minHitDice, int power, string randomFociQuantity, string size)
        {
            var data = new List<string>(11);
            while (data.Count < data.Capacity)
                data.Add(string.Empty);

            data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = featName;
            data[DataIndexConstants.SpecialQualityData.FocusIndex] = focus;
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.SpecialQualityData.MaximumHitDiceRequirementIndex] = maxHitDice.ToString();
            data[DataIndexConstants.SpecialQualityData.MinimumHitDiceRequirementIndex] = minHitDice.ToString();
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = power.ToString();
            data[DataIndexConstants.SpecialQualityData.RandomFociQuantity] = randomFociQuantity;
            data[DataIndexConstants.SpecialQualityData.SizeRequirementIndex] = size;

            var totalData = string.Join("/", data);

            specialQualitiesData.Add(totalData);
        }

        [Test]
        public void GetSpecialQualityWithFocusType()
        {
            AddSpecialQualityData("special quality", "focus type", 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.EqualTo("focus type"));
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithFrequency()
        {
            AddSpecialQualityData("special quality", string.Empty, 9266, "fortnight", 0, 0, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithMaximumHitDieRequirement()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 9266, 0, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithMinimumAbility()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality"))
                .Returns(abilityRequirements);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Not.Empty);
            Assert.That(specialQuality.MinimumAbilities.Count, Is.EqualTo(1));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("ability"));
            Assert.That(specialQuality.MinimumAbilities["ability"], Is.EqualTo(9266));
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithMinimumAbilities()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other ability", Amount = 90210 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality"))
                .Returns(abilityRequirements);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Not.Empty);
            Assert.That(specialQuality.MinimumAbilities.Count, Is.EqualTo(2));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("ability"));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("other ability"));
            Assert.That(specialQuality.MinimumAbilities["ability"], Is.EqualTo(9266));
            Assert.That(specialQuality.MinimumAbilities["other ability"], Is.EqualTo(90210));
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithMinimumHitDieRequirement()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 9266, 0, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithPower()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 9266, string.Empty, string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(9266));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRandomFociQuantity()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, "random foci quantity", string.Empty);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.EqualTo("random foci quantity"));
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeat()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "special quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(1));
            Assert.That(specialQuality.SizeRequirement, Is.Empty);

            var requiredFeat = specialQuality.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Focus, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatWithFocus()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat/required focus"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "special quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(1));
            Assert.That(specialQuality.SizeRequirement, Is.Empty);

            var requiredFeat = specialQuality.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Focus, Is.EqualTo("required focus"));
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeats()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat",
                "other required feat",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "special quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.SizeRequirement, Is.Empty);

            var requiredFeat = specialQuality.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Focus, Is.Empty);

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Focus, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatsWithFoci()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat/required focus",
                "other required feat/other required focus",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "special quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.SizeRequirement, Is.Empty);

            var requiredFeat = specialQuality.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Focus, Is.EqualTo("required focus"));

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Focus, Is.EqualTo("other required focus"));
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatWithAndWithoutFoci()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat/required focus",
                "other required feat",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "special quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.SizeRequirement, Is.Empty);

            var requiredFeat = specialQuality.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Focus, Is.EqualTo("required focus"));

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Focus, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithSizeRequirement()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, "ginormous");

            var specialQualities = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(specialQuality.Power, Is.EqualTo(0));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.SizeRequirement, Is.EqualTo("ginormous"));
        }

        //INFO: An example of this would be a special quality with a different power depending on the hit dice of the creature
        [Test]
        public void GetDifferentSpecialQualitiesWithSameName()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 600, 0, 1, string.Empty, string.Empty);
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 601, 2, string.Empty, string.Empty);

            var specialQualitySelections = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualitySelections.Count(), Is.EqualTo(2));

            var first = specialQualitySelections.First();
            var last = specialQualitySelections.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality"));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MaximumHitDieRequirement, Is.EqualTo(600));
            Assert.That(first.MinimumAbilities, Is.Empty);
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(first.Power, Is.EqualTo(1));
            Assert.That(first.RandomFociQuantity, Is.Empty);
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.SizeRequirement, Is.Empty);

            Assert.That(last.Feat, Is.EqualTo("special quality"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.MinimumAbilities, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(601));
            Assert.That(last.Power, Is.EqualTo(2));
            Assert.That(last.RandomFociQuantity, Is.Empty);
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.SizeRequirement, Is.Empty);
        }

        [Test]
        public void GetFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void GetNoFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeats()
        {
            featsData["feat 1"] = BuildFeatData(string.Empty, 0, string.Empty, 9266, 42);
            featsData["feat 2"] = BuildFeatData("focus", 9266, "occasionally", 0, 0);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat 1")).Returns(new[] { "feat 1", "feat 2/focus" });

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability 1", Amount = 13 },
                new TypeAndAmountSelection { Type = "ability 2", Amount = 16 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "feat 2")).Returns(abilityRequirements);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill 1", Amount = 0 },
                new TypeAndAmountSelection { Type = "skill 2", Amount = 4 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat 2")).Returns(skillRankRequirements);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat 2", "feat 3" });

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(2));

            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Feat, Is.EqualTo("feat 1"));
            Assert.That(first.Power, Is.EqualTo(42));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(first.CanBeTakenMultipleTimes, Is.False);

            var firstRequirement = first.RequiredFeats.First();
            var lastRequirement = first.RequiredFeats.Last();
            Assert.That(firstRequirement.Feat, Is.EqualTo("feat 1"));
            Assert.That(firstRequirement.Focus, Is.Empty);
            Assert.That(lastRequirement.Feat, Is.EqualTo("feat 2"));
            Assert.That(lastRequirement.Focus, Is.EqualTo("focus"));
            Assert.That(first.RequiredFeats.Count(), Is.EqualTo(2));

            Assert.That(first.RequiredSkills, Is.Empty);
            Assert.That(first.RequiredAbilities, Is.Empty);
            Assert.That(first.FocusType, Is.Empty);

            Assert.That(last.Feat, Is.EqualTo("feat 2"));
            Assert.That(last.Power, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("occasionally"));
            Assert.That(last.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.CanBeTakenMultipleTimes, Is.True);

            var requiredSkills = last.RequiredSkills.ToArray();
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("skill 1"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(0));
            Assert.That(requiredSkills[0].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("skill 2"));
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(4));
            Assert.That(requiredSkills[1].Focus, Is.Empty);
            Assert.That(requiredSkills.Length, Is.EqualTo(2));

            Assert.That(last.RequiredSkills.Count, Is.EqualTo(2));
            Assert.That(last.RequiredAbilities["ability 1"], Is.EqualTo(13));
            Assert.That(last.RequiredAbilities["ability 2"], Is.EqualTo(16));
            Assert.That(last.RequiredAbilities.Count, Is.EqualTo(2));
            Assert.That(last.FocusType, Is.EqualTo("focus"));
        }

        private IEnumerable<string> BuildFeatData(string focus, int frequencyQuantity, string frequencyTimePeriod, int baseAttack, int power)
        {
            var data = new List<string>(5);
            while (data.Count < data.Capacity)
                data.Add(string.Empty);

            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = baseAttack.ToString();
            data[DataIndexConstants.FeatData.FocusTypeIndex] = focus;
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.FeatData.PowerIndex] = power.ToString();

            return data;
        }

        [Test]
        public void GetFeatThatCanBeTakenMultipleTimes()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithFocusType()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithFrequency()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithMinimumCasterLevel()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithPower()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredAbility()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredAbilities()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredBaseAttack()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredFeatWithFocus()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredFeatsWithFoci()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithrequiredFeatsWithAndWithoutFoci()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredSkill()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredSkillWithFocus()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill/focus", Amount = 9266 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(featSelection.Power, Is.EqualTo(0));
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(featSelection.RequiredFeats, Is.Empty);
            Assert.That(featSelection.RequiredSkills, Is.Not.Empty);
            Assert.That(featSelection.RequiredSkills.Count(), Is.EqualTo(1));

            var requiredSkill = featSelection.RequiredSkills.Single();
            Assert.That(requiredSkill.Skill, Is.EqualTo("skill"));
            Assert.That(requiredSkill.Ranks, Is.EqualTo(9266));
            Assert.That(requiredSkill.Focus, Is.EqualTo("focus"));
        }

        [Test]
        public void GetFeatWithRequiredSkills()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredSkillsWithFoci()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithRequiredSkillsWithAndWithoutFoci()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatWithWeaponProficiencyRequirement()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatWithCrossbowProficiencyRequirement()
        {
            Assert.Fail("not yet written");
        }
    }
}