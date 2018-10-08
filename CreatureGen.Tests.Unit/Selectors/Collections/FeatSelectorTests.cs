using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Helpers;
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
        private Dictionary<string, List<string>> specialQualitiesData;
        private CreatureType creatureType;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockTypesAndAmountsSelector = new Mock<ITypeAndAmountSelector>();
            featsSelector = new FeatsSelector(mockCollectionsSelector.Object, mockTypesAndAmountsSelector.Object);

            featsData = new Dictionary<string, IEnumerable<string>>();
            creatureType = new CreatureType();
            creatureType.Name = "creature type";
            specialQualitiesData = new Dictionary<string, List<string>>();
            specialQualitiesData["creature"] = new List<string>();
            specialQualitiesData[creatureType.Name] = new List<string>();

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredSizes, It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, It.IsAny<string>()))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Collection.FeatData)).Returns(featsData);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SpecialQualityData, It.IsAny<string>())).Returns((string t, string c) => specialQualitiesData[c]);
        }

        [Test]
        public void GetSpecialQuality()
        {
            AddSpecialQualityData("creature", "special quality");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetNoSpecialQualities()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SpecialQualityData, "creature")).Returns(Enumerable.Empty<string>());

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualities()
        {
            AddSpecialQualityData("creature", "special quality 1");
            AddSpecialQualityData("creature", "special quality 2");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(2));

            var first = specialQualities.First();
            var last = specialQualities.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality 1"));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.Zero);
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MinimumAbilities, Is.Empty);
            Assert.That(first.Power, Is.Zero);
            Assert.That(first.RandomFociQuantity, Is.Empty);
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.RequiresEquipment, Is.False);

            Assert.That(last.Feat, Is.EqualTo("special quality 2"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.Zero);
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MinimumAbilities, Is.Empty);
            Assert.That(last.Power, Is.Zero);
            Assert.That(last.RandomFociQuantity, Is.Empty);
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.RequiresEquipment, Is.False);
        }

        private void AddSpecialQualityData(string source, string featName, string focus = "", int frequencyQuantity = 0, string frequencyTimePeriod = "", int power = 0, string randomFociQuantity = "", bool requiresEquipment = false)
        {
            var data = SpecialQualityHelper.BuildData(featName, randomFociQuantity, focus, frequencyQuantity, frequencyTimePeriod, power, requiresEquipment);
            var entry = SpecialQualityHelper.BuildData(data);

            if (!specialQualitiesData.ContainsKey(source))
                specialQualitiesData[source] = new List<string>();

            specialQualitiesData[source].Add(entry);
        }

        [Test]
        public void GetSpecialQualityWithFocusType()
        {
            AddSpecialQualityData("creature", "special quality", focus: "focus type");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.EqualTo("focus type"));
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithFrequency()
        {
            AddSpecialQualityData("creature", "special quality", frequencyQuantity: 9266, frequencyTimePeriod: "fortnight");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(specialQuality.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithMinimumAbility()
        {
            AddSpecialQualityData("creature", "special quality");

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality"))
                .Returns(abilityRequirements);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Not.Empty);
            Assert.That(specialQuality.MinimumAbilities.Count, Is.EqualTo(1));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("ability"));
            Assert.That(specialQuality.MinimumAbilities["ability"], Is.EqualTo(9266));
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityFromTypeWithMinimumAbility()
        {
            AddSpecialQualityData(creatureType.Name, "special quality");

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "creature typespecial quality"))
                .Returns(abilityRequirements);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Not.Empty);
            Assert.That(specialQuality.MinimumAbilities.Count, Is.EqualTo(1));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("ability"));
            Assert.That(specialQuality.MinimumAbilities["ability"], Is.EqualTo(9266));
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithMinimumAbilities()
        {
            AddSpecialQualityData("creature", "special quality");

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other ability", Amount = 90210 }
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality"))
                .Returns(abilityRequirements);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Not.Empty);
            Assert.That(specialQuality.MinimumAbilities.Count, Is.EqualTo(2));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("ability"));
            Assert.That(specialQuality.MinimumAbilities.Keys, Contains.Item("other ability"));
            Assert.That(specialQuality.MinimumAbilities["ability"], Is.EqualTo(9266));
            Assert.That(specialQuality.MinimumAbilities["other ability"], Is.EqualTo(90210));
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithPower()
        {
            AddSpecialQualityData("creature", "special quality", power: 9266);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.EqualTo(9266));
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithRandomFociQuantity()
        {
            AddSpecialQualityData("creature", "special quality", randomFociQuantity: "random foci quantity");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.EqualTo("random foci quantity"));
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeat()
        {
            AddSpecialQualityData("creature", "special quality");

            var requiredFeats = new[]
            {
                "required feat"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(1));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

            var requiredFeat = specialQuality.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatFromCreatureType()
        {
            AddSpecialQualityData(creatureType.Name, "special quality");

            var requiredFeats = new[]
            {
                "required feat"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creature typespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(1));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

            var requiredFeat = specialQuality.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeatWithFocus()
        {
            AddSpecialQualityData("creature", "special quality");

            var requiredFeats = new[]
            {
                "required feat/required focus"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(1));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

            var requiredFeat = specialQuality.RequiredFeats.Single();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));
        }

        [Test]
        public void GetSpecialQualityWithRequiredFeats()
        {
            AddSpecialQualityData("creature", "special quality");

            var requiredFeats = new[]
            {
                "required feat",
                "other required feat",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

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
            AddSpecialQualityData("creature", "special quality");

            var requiredFeats = new[]
            {
                "required feat/required focus",
                "other required feat/other required focus",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

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
            AddSpecialQualityData("creature", "special quality");

            var requiredFeats = new[]
            {
                "required feat/required focus",
                "other required feat",
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "creaturespecial quality")).Returns(requiredFeats);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Not.Empty);
            Assert.That(specialQuality.RequiredFeats.Count(), Is.EqualTo(2));
            Assert.That(specialQuality.RequiresEquipment, Is.False);

            var requiredFeat = specialQuality.RequiredFeats.First();
            Assert.That(requiredFeat.Feat, Is.EqualTo("required feat"));
            Assert.That(requiredFeat.Foci.Single(), Is.EqualTo("required focus"));

            var otherRequiredFeat = specialQuality.RequiredFeats.Last();
            Assert.That(otherRequiredFeat.Feat, Is.EqualTo("other required feat"));
            Assert.That(otherRequiredFeat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithRequiresEquipment()
        {
            AddSpecialQualityData("creature", "special quality", requiresEquipment: true);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.True);
        }

        [Test]
        public void GetSpecialQualityWithNotRequiresEquipment()
        {
            AddSpecialQualityData("creature", "special quality", requiresEquipment: false);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualitiesFromCreatureType()
        {
            AddSpecialQualityData(creatureType.Name, "special quality");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualitiesFromCreatureSubtype()
        {
            creatureType.SubTypes = new[] { "subtype" };

            AddSpecialQualityData("subtype", "special quality");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetSpecialQualitiesFromCreatureSubtypes()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            AddSpecialQualityData("subtype", "special quality 1");
            AddSpecialQualityData("other subtype", "special quality 2");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(2));

            var first = specialQualities.First();
            var last = specialQualities.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality 1"));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.Zero);
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MinimumAbilities, Is.Empty);
            Assert.That(first.Power, Is.Zero);
            Assert.That(first.RandomFociQuantity, Is.Empty);
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.RequiresEquipment, Is.False);

            Assert.That(last.Feat, Is.EqualTo("special quality 2"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.Zero);
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MinimumAbilities, Is.Empty);
            Assert.That(last.Power, Is.Zero);
            Assert.That(last.RandomFociQuantity, Is.Empty);
            Assert.That(last.RequiredFeats, Is.Empty);
            Assert.That(last.RequiresEquipment, Is.False);
        }

        [Test]
        public void GetAllSpecialQualities()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            AddSpecialQualityData("creature", "special quality 1");
            AddSpecialQualityData(creatureType.Name, "special quality 2");
            AddSpecialQualityData("subtype", "special quality 3");
            AddSpecialQualityData("other subtype", "special quality 4");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType).ToArray();
            Assert.That(specialQualities.Length, Is.EqualTo(4));

            Assert.That(specialQualities[0].Feat, Is.EqualTo("special quality 1"));
            Assert.That(specialQualities[0].FocusType, Is.Empty);
            Assert.That(specialQualities[0].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[0].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[0].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[0].Power, Is.Zero);
            Assert.That(specialQualities[0].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[0].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[0].RequiresEquipment, Is.False);

            Assert.That(specialQualities[1].Feat, Is.EqualTo("special quality 2"));
            Assert.That(specialQualities[1].FocusType, Is.Empty);
            Assert.That(specialQualities[1].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[1].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[1].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[1].Power, Is.Zero);
            Assert.That(specialQualities[1].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[1].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[1].RequiresEquipment, Is.False);

            Assert.That(specialQualities[2].Feat, Is.EqualTo("special quality 3"));
            Assert.That(specialQualities[2].FocusType, Is.Empty);
            Assert.That(specialQualities[2].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[2].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[2].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[2].Power, Is.Zero);
            Assert.That(specialQualities[2].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[2].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[2].RequiresEquipment, Is.False);

            Assert.That(specialQualities[3].Feat, Is.EqualTo("special quality 4"));
            Assert.That(specialQualities[3].FocusType, Is.Empty);
            Assert.That(specialQualities[3].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[3].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[3].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[3].Power, Is.Zero);
            Assert.That(specialQualities[3].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[3].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[3].RequiresEquipment, Is.False);
        }

        [Test]
        public void RemoveDuplicateSpecialQualities()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            AddSpecialQualityData("creature", "special quality");
            AddSpecialQualityData(creatureType.Name, "special quality");
            AddSpecialQualityData("subtype", "special quality");
            AddSpecialQualityData("other subtype", "special quality");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType).ToArray();
            Assert.That(specialQualities.Length, Is.EqualTo(1));

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Feat, Is.EqualTo("special quality"));
            Assert.That(specialQuality.FocusType, Is.Empty);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQuality.MinimumAbilities, Is.Empty);
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.RandomFociQuantity, Is.Empty);
            Assert.That(specialQuality.RequiredFeats, Is.Empty);
            Assert.That(specialQuality.RequiresEquipment, Is.False);
        }

        [Test]
        public void DoNotRemoveNonDuplicateSpecialQualities()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            AddSpecialQualityData("creature", "special quality", focus: "focus");
            AddSpecialQualityData(creatureType.Name, "special quality", frequencyQuantity: 9266, frequencyTimePeriod: "fortnight");
            AddSpecialQualityData("subtype", "special quality", power: 90210);
            AddSpecialQualityData("other subtype", "special quality", requiresEquipment: true);

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType).ToArray();
            Assert.That(specialQualities.Length, Is.EqualTo(4));

            Assert.That(specialQualities[0].Feat, Is.EqualTo("special quality"));
            Assert.That(specialQualities[0].FocusType, Is.EqualTo("focus"));
            Assert.That(specialQualities[0].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[0].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[0].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[0].Power, Is.Zero);
            Assert.That(specialQualities[0].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[0].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[0].RequiresEquipment, Is.False);

            Assert.That(specialQualities[1].Feat, Is.EqualTo("special quality"));
            Assert.That(specialQualities[1].FocusType, Is.Empty);
            Assert.That(specialQualities[1].Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(specialQualities[1].Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(specialQualities[1].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[1].Power, Is.Zero);
            Assert.That(specialQualities[1].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[1].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[1].RequiresEquipment, Is.False);

            Assert.That(specialQualities[2].Feat, Is.EqualTo("special quality"));
            Assert.That(specialQualities[2].FocusType, Is.Empty);
            Assert.That(specialQualities[2].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[2].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[2].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[2].Power, Is.EqualTo(90210));
            Assert.That(specialQualities[2].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[2].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[2].RequiresEquipment, Is.False);

            Assert.That(specialQualities[3].Feat, Is.EqualTo("special quality"));
            Assert.That(specialQualities[3].FocusType, Is.Empty);
            Assert.That(specialQualities[3].Frequency.Quantity, Is.Zero);
            Assert.That(specialQualities[3].Frequency.TimePeriod, Is.Empty);
            Assert.That(specialQualities[3].MinimumAbilities, Is.Empty);
            Assert.That(specialQualities[3].Power, Is.Zero);
            Assert.That(specialQualities[3].RandomFociQuantity, Is.Empty);
            Assert.That(specialQualities[3].RequiredFeats, Is.Empty);
            Assert.That(specialQualities[3].RequiresEquipment, Is.True);
        }

        //INFO: Type or Subtype special qualities might have size requirements
        //Example is Swarm subtype having defenses dependent on size
        [Test]
        public void GetNoSizeRequirementForSpecialQuality()
        {
            AddSpecialQualityData("creature", "special quality");

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();
            Assert.That(specialQuality.RequiredSizes, Is.Empty);
        }

        //INFO: Type or Subtype special qualities might have size requirements
        //Example is Swarm subtype having defenses dependent on size
        [Test]
        public void GetSizeRequirementForSpecialQuality()
        {
            AddSpecialQualityData("creature", "special quality");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredSizes, "creaturespecial quality"))
                .Returns(new[] { "size" });

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();
            Assert.That(specialQuality.RequiredSizes, Contains.Item("size"));
            Assert.That(specialQuality.RequiredSizes.Count, Is.EqualTo(1));
        }

        //INFO: Type or Subtype special qualities might have size requirements
        //Example is Swarm subtype having defenses dependent on size
        [Test]
        public void GetSizeRequirementsForSpecialQuality()
        {
            AddSpecialQualityData("creature", "special quality");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredSizes, "creaturespecial quality"))
                .Returns(new[] { "size", "other size" });

            var specialQualities = featsSelector.SelectSpecialQualities("creature", creatureType);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();
            Assert.That(specialQuality.RequiredSizes, Contains.Item("size"));
            Assert.That(specialQuality.RequiredSizes, Contains.Item("other size"));
            Assert.That(specialQuality.RequiredSizes.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetFeat()
        {
            featsData["feat"] = BuildFeatData();

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            featsData["feat 1"] = BuildFeatData();
            featsData["feat 2"] = BuildFeatData();

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(2));

            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Feat, Is.EqualTo("feat 1"));
            Assert.That(first.CanBeTakenMultipleTimes, Is.False);
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.Zero);
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MinimumCasterLevel, Is.Zero);
            Assert.That(first.Power, Is.Zero);
            Assert.That(first.RequiredAbilities, Is.Empty);
            Assert.That(first.RequiredBaseAttack, Is.Zero);
            Assert.That(first.RequiredFeats, Is.Empty);
            Assert.That(first.RequiredSkills, Is.Empty);

            Assert.That(last.Feat, Is.EqualTo("feat 2"));
            Assert.That(last.CanBeTakenMultipleTimes, Is.False);
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.Frequency.Quantity, Is.Zero);
            Assert.That(last.Frequency.TimePeriod, Is.Empty);
            Assert.That(last.MinimumCasterLevel, Is.Zero);
            Assert.That(last.Power, Is.Zero);
            Assert.That(last.RequiredAbilities, Is.Empty);
            Assert.That(last.RequiredBaseAttack, Is.Zero);
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
            bool requiresSpecialAttack = false,
            bool requiresSpellLikeAbility = false,
            bool requiresNaturalArmor = false,
            int requiredNaturalWeaponQuantity = 0,
            int requiredHandQuantity = 0)
        {
            var data = DataIndexConstants.FeatData.InitializeData();

            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = baseAttack.ToString();
            data[DataIndexConstants.FeatData.FocusTypeIndex] = focus;
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.FeatData.PowerIndex] = power.ToString();
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = minimumCasterLevel.ToString();
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = requiresSpecialAttack.ToString();
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbility] = requiresSpellLikeAbility.ToString();
            data[DataIndexConstants.FeatData.RequiresNaturalArmor] = requiresNaturalArmor.ToString();
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = requiredNaturalWeaponQuantity.ToString();
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = requiredHandQuantity.ToString();

            return data;
        }

        [Test]
        public void GetFeatThatCanBeTakenMultipleTimes()
        {
            featsData["feat"] = BuildFeatData();

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "wrong feat", "feat" });

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.True);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
            Assert.That(feat.RequiredFeats, Is.Empty);
            Assert.That(feat.RequiredSkills, Is.Empty);
        }

        [Test]
        public void GetFeatWithFocusType()
        {
            featsData["feat"] = BuildFeatData("focus type");

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.EqualTo("focus type"));
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.EqualTo(9266));
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.EqualTo(9266));
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat"))
                .Returns(abilityRequirements);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Not.Empty);
            Assert.That(feat.RequiredAbilities.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("ability"));
            Assert.That(feat.RequiredAbilities["ability"], Is.EqualTo(9266));
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat"))
                .Returns(abilityRequirements);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Not.Empty);
            Assert.That(feat.RequiredAbilities.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("ability"));
            Assert.That(feat.RequiredAbilities.Keys, Contains.Item("other ability"));
            Assert.That(feat.RequiredAbilities["ability"], Is.EqualTo(9266));
            Assert.That(feat.RequiredAbilities["other ability"], Is.EqualTo(90210));
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.Zero);
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.Zero);
            Assert.That(featSelection.Power, Is.Zero);
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.Zero);
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.Zero);
            Assert.That(featSelection.Power, Is.Zero);
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.Zero);
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.Zero);
            Assert.That(featSelection.Power, Is.Zero);
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.Zero);
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.Zero);
            Assert.That(featSelection.Power, Is.Zero);
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.CanBeTakenMultipleTimes, Is.False);
            Assert.That(featSelection.FocusType, Is.Empty);
            Assert.That(featSelection.Frequency.Quantity, Is.Zero);
            Assert.That(featSelection.Frequency.TimePeriod, Is.Empty);
            Assert.That(featSelection.MinimumCasterLevel, Is.Zero);
            Assert.That(featSelection.Power, Is.Zero);
            Assert.That(featSelection.RequiredAbilities, Is.Empty);
            Assert.That(featSelection.RequiredBaseAttack, Is.Zero);
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
            featsData["feat"] = BuildFeatData();

            var requiredFeats = new[]
            {
                GroupConstants.WeaponProficiency
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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
            featsData["feat"] = BuildFeatData();

            var requiredFeats = new[]
            {
                $"{GroupConstants.WeaponProficiency}/{WeaponConstants.HandCrossbow},{WeaponConstants.HeavyCrossbow},{WeaponConstants.LightCrossbow}"
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredFeats, "feat")).Returns(requiredFeats);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.CanBeTakenMultipleTimes, Is.False);
            Assert.That(feat.FocusType, Is.Empty);
            Assert.That(feat.Frequency.Quantity, Is.Zero);
            Assert.That(feat.Frequency.TimePeriod, Is.Empty);
            Assert.That(feat.MinimumCasterLevel, Is.Zero);
            Assert.That(feat.Power, Is.Zero);
            Assert.That(feat.RequiredAbilities, Is.Empty);
            Assert.That(feat.RequiredBaseAttack, Is.Zero);
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(speedRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(speedRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
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

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, "feat")).Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var additionalFeats = featsSelector.SelectFeats();
            Assert.That(additionalFeats.Count, Is.EqualTo(1));

            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));
            Assert.That(featSelection.RequiredSpeeds, Is.Empty);
        }

        [Test]
        public void GetFeatRequiringSpellLikeAbility()
        {
            featsData["feat"] = BuildFeatData(requiresSpellLikeAbility: true);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiresSpellLikeAbility, Is.True);
        }

        [Test]
        public void GetFeatNotRequiringSpellLikeAbility()
        {
            featsData["feat"] = BuildFeatData(requiresSpellLikeAbility: false);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiresSpellLikeAbility, Is.False);
        }

        [Test]
        public void GetFeatRequiringNaturalArmor()
        {
            featsData["feat"] = BuildFeatData(requiresNaturalArmor: true);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiresNaturalArmor, Is.True);
        }

        [Test]
        public void GetFeatNotRequiringNaturalArmor()
        {
            featsData["feat"] = BuildFeatData(requiresNaturalArmor: false);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiresNaturalArmor, Is.False);
        }

        [Test]
        public void GetFeatRequiringNaturalWeaponQuantity()
        {
            featsData["feat"] = BuildFeatData(requiredNaturalWeaponQuantity: 9266);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredNaturalWeapons, Is.EqualTo(9266));
        }

        [Test]
        public void GetFeatNotRequiringNaturalWeaponQuantity()
        {
            featsData["feat"] = BuildFeatData(requiredNaturalWeaponQuantity: 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredNaturalWeapons, Is.Zero);
        }

        [Test]
        public void GetFeatRequiringHands()
        {
            featsData["feat"] = BuildFeatData(requiredHandQuantity: 9266);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredHands, Is.EqualTo(9266));
        }

        [Test]
        public void GetFeatNotRequiringHands()
        {
            featsData["feat"] = BuildFeatData(requiredHandQuantity: 0);

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredHands, Is.Zero);
        }

        [Test]
        public void GetFeatRequiringSize()
        {
            featsData["feat"] = BuildFeatData();

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredSizes, "feat"))
                .Returns(new[] { "size" });

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredSizes.Count, Is.EqualTo(1));
            Assert.That(feat.RequiredSizes, Contains.Item("size"));
        }

        [Test]
        public void GetFeatRequiringSizes()
        {
            featsData["feat"] = BuildFeatData();

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.RequiredSizes, "feat"))
                .Returns(new[] { "size", "other size" });

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredSizes.Count, Is.EqualTo(2));
            Assert.That(feat.RequiredSizes, Contains.Item("size"));
            Assert.That(feat.RequiredSizes, Contains.Item("other size"));
        }

        [Test]
        public void GetFeatNotRequiringSizes()
        {
            featsData["feat"] = BuildFeatData();

            var feats = featsSelector.SelectFeats();
            Assert.That(feats.Count(), Is.EqualTo(1));

            var feat = feats.Single();

            Assert.That(feat.Feat, Is.EqualTo("feat"));
            Assert.That(feat.RequiredSizes, Is.Empty);
        }
    }
}