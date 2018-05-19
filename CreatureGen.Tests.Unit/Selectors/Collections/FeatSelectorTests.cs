using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

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
            Assert.That(requiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatWithFocus()
        {
            AddSpecialQualityData("special quality", string.Empty, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty);

            var requiredFeats = new[]
            {
                "required feat/required focus"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

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
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

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
            Assert.That(requiredFeat.Foci, Is.Empty);

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci, Is.Empty);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

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
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci.Single(), Is.EqualTo("other required focus"));
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

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
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci, Is.Empty);
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
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetNoFeats()
        {
            var feats = featsSelector.SelectFeats();
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetFeats()
        {
            featsData["feat 1"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);
            featsData["feat 2"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(2));

            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Feat, Is.EqualTo("feat 1"));
            Assert.That(first.CanBeTakenMultipleTimes, Is.False);
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(first.Power, Is.EqualTo(0));
            Assert.That(first.RequiredAbilities, Is.Empty);
            Assert.That(first.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.RequiredSkills, Is.Empty);

            Assert.That(last.Feat, Is.EqualTo("feat 2"));
            Assert.That(last.CanBeTakenMultipleTimes, Is.False);
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(last.Power, Is.EqualTo(0));
            Assert.That(last.RequiredAbilities, Is.Empty);
            Assert.That(last.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.RequiredSkills, Is.Empty);
        }

        private IEnumerable<string> BuildFeatData(
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int baseAttack = 0,
            int power = 0,
            int minimumCasterLevel = 0,
            bool requiresSpecialAttack = false)
        {
            var data = DataIndexConstants.FeatData.InitializeData();

            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = baseAttack.ToString();
            data[DataIndexConstants.FeatData.FocusTypeIndex] = focus;
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.FeatData.PowerIndex] = power.ToString();
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = minimumCasterLevel.ToString();
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = requiresSpecialAttack.ToString();

            return data;
        }

        [Test]
        public void GetFeatThatCanBeTakenMultipleTimes()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "wrong feat", "feat" });

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.True);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithFocusType()
        {
            featsData["feat"] = BuildFeatData("focus type", 0, string.Empty, 0, 0, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.EqualTo("focus type"));
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithFrequency()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 9266, "fortnight", 0, 0, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithMinimumCasterLevel()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 9266);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(9266));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithPower()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 9266, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredAbility()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "feat"))
                .Returns(abilityRequirements);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Not.Empty);
            Assert.That(feat.RequiredAbilities.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("ability"));
            Assert.That(feat.RequiredAbilities["ability"], Is.EqualTo(9266));
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredAbilities()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other ability", Amount = 90210 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "feat"))
                .Returns(abilityRequirements);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Not.Empty);
            Assert.That(feat.RequiredAbilities.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("ability"));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("other ability"));
            Assert.That(feat.RequiredAbilities["ability"], Is.EqualTo(9266));
            Assert.That(feat.RequiredAbilities["other ability"], Is.EqualTo(90210));
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredBaseAttack()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 9266, 0, 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredFeat()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredFeatWithFocus()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat/required focus"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));
        }

        [Test]
        public void GetFeatWithRequiredFeats()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat",
                "other required feat",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci, Is.Empty);

            var otherRequiredFeat = feat.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredFeatsWithFocus()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat/required focus",
                "other required feat/other required focus",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));

            var otherRequiredFeat = feat.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci.Single(), Is.EqualTo("other required focus"));
        }

        [Test]
        public void GetFeatWithRequiredFeatsWithFoci()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat/required focus,other required focus",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci.First, Is.EqualTo("required focus"));
            Assert.That(requiredFeat.Foci.Last, Is.EqualTo("other required focus"));
            Assert.That(requiredFeat.Foci.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetFeatWithRequiredFeatsWithAndWithoutFoci()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                "required feat",
                "other required feat/other required focus",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci, Is.Empty);

            var otherRequiredFeat = feat.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci.Single(), Is.EqualTo("other required focus"));
        }

        [Test]
        public void GetFeatWithRequiredSkill()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill", Amount = 9266 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(requiredSkill.Focus, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredSkillWithFocus()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill/focus", Amount = 9266 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other skill", Amount = 90210 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills.Count(), Is.EqualTo(2));

            var requiredSkill = featSelection.RequiredSkills.First();
            Assert.That(requiredSkill.Skill, Is.EqualTo("skill"));
            Assert.That(requiredSkill.Ranks, Is.EqualTo(9266));
            Assert.That(requiredSkill.Focus, Is.Empty);

            var otherRequiredSkill = featSelection.RequiredSkills.Last();
            Assert.That(otherRequiredSkill.Skill, Is.EqualTo("other skill"));
            Assert.That(otherRequiredSkill.Ranks, Is.EqualTo(90210));
            Assert.That(otherRequiredSkill.Focus, Is.Empty);
        }

        [Test]
        public void GetFeatWithRequiredSkillsWithFoci()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill/focus", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other skill/other focus", Amount = 90210 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills.Count(), Is.EqualTo(2));

            var requiredSkill = featSelection.RequiredSkills.First();
            Assert.That(requiredSkill.Skill, Is.EqualTo("skill"));
            Assert.That(requiredSkill.Ranks, Is.EqualTo(9266));
            Assert.That(requiredSkill.Focus, Is.EqualTo("focus"));

            var otherRequiredSkill = featSelection.RequiredSkills.Last();
            Assert.That(otherRequiredSkill.Skill, Is.EqualTo("other skill"));
            Assert.That(otherRequiredSkill.Ranks, Is.EqualTo(90210));
            Assert.That(otherRequiredSkill.Focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void GetFeatWithRequiredSkillsWithAndWithoutFoci()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other skill/other focus", Amount = 90210 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills.Count(), Is.EqualTo(2));

            var requiredSkill = featSelection.RequiredSkills.First();
            Assert.That(requiredSkill.Skill, Is.EqualTo("skill"));
            Assert.That(requiredSkill.Ranks, Is.EqualTo(9266));
            Assert.That(requiredSkill.Focus, Is.Empty);

            var otherRequiredSkill = featSelection.RequiredSkills.Last();
            Assert.That(otherRequiredSkill.Skill, Is.EqualTo("other skill"));
            Assert.That(otherRequiredSkill.Ranks, Is.EqualTo(90210));
            Assert.That(otherRequiredSkill.Focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void GetFeatWithWeaponProficiencyRequirement()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                GroupConstants.WeaponProficiency
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo(GroupConstants.WeaponProficiency));
            Assert.That(requiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetFeatWithCrossbowProficiencyRequirement()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0, 0);

            var requiredFeats = new[]
            {
                $"{GroupConstants.WeaponProficiency}/{WeaponConstants.HandCrossbow},{WeaponConstants.HeavyCrossbow},{WeaponConstants.LightCrossbow}"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Not.Empty);
            Assert.That(feat.RequiredFeats.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSkills, Is.Empty);

            var requiredFeat = feat.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo(GroupConstants.WeaponProficiency));
            Assert.That(requiredFeat.Foci, Is.Not.Empty);
            Assert.That(requiredFeat.Foci.Count, Is.EqualTo(3));
            Assert.That(requiredFeat.Foci, Contains.Item(WeaponConstants.HandCrossbow));
            Assert.That(requiredFeat.Foci, Contains.Item(WeaponConstants.HeavyCrossbow));
            Assert.That(requiredFeat.Foci, Contains.Item(WeaponConstants.LightCrossbow));
        }

        [Test]
        public void GetFeatRequiringSpecialAttack()
        {
            featsData["feat"] = BuildFeatData(requiresSpecialAttack: true);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
            Assert.That(feat.RequiresSpecialAttack, Is.True);
        }

        [Test]
        public void GetFeatNotRequiringSpecialAttack()
        {
            featsData["feat"] = BuildFeatData(requiresSpecialAttack: false);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(feat.Power, Is.EqualTo(0));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
            Assert.That(feat.RequiresSpecialAttack, Is.False);
        }

        [Test]
        public void GetFeatRequiringSpeed()
        {
            featsData["feat"] = BuildFeatData();

            var speedRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "speed", Amount = 9266 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(speedRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills, Is.Empty);
            Assert.That(featSelection.RequiredSpeeds, Is.Not.Empty);
            Assert.That(featSelection.RequiredSpeeds.Count(), Is.EqualTo(1));

            Assert.That(featSelection.RequiredSpeeds.Keys, Contains.Item("speed"));
            Assert.That(featSelection.RequiredSpeeds["speed"], Is.EqualTo(9266));
        }

        [Test]
        public void GetFeatRequiringSpeeds()
        {
            featsData["feat"] = BuildFeatData();

            var speedRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "speed", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other speed", Amount = 90210 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(speedRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills, Is.Empty);
            Assert.That(featSelection.RequiredSpeeds, Is.Not.Empty);
            Assert.That(featSelection.RequiredSpeeds.Count(), Is.EqualTo(2));

            Assert.That(featSelection.RequiredSpeeds.Keys, Contains.Item("speed"));
            Assert.That(featSelection.RequiredSpeeds["speed"], Is.EqualTo(9266));

            Assert.That(featSelection.RequiredSpeeds.Keys, Contains.Item("other speed"));
            Assert.That(featSelection.RequiredSpeeds["other speed"], Is.EqualTo(90210));
        }

        [Test]
        public void GetFeatNotRequiringSpeeds()
        {
            featsData["feat"] = BuildFeatData();

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

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
            Assert.That(featSelection.RequiredSkills, Is.Empty);
            Assert.That(featSelection.RequiredSpeeds, Is.Empty);
        }

        [Test]
        public void GetFeatRequiringSpellLikeAbility()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatNotRequiringSpellLikeAbility()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatRequiringNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatNotRequiringNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatRequiringNaturalWeaponQuantity()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatNotRequiringNaturalWeaponQuantity()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatRequiringHands()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetFeatNotRequiringHands()
        {
            Assert.Fail("not yet written");
        }
    }
}