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
        private IFeatsSelector featsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<ITypeAndAmountSelector> mockTypesAndAmountsSelector;
        private Dictionary<string, IEnumerable<string>> featsData;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockTypesAndAmountsSelector = new Mock<ITypeAndAmountSelector>();
            featsSelector = new FeatsSelector(mockCollectionsSelector.Object, mockTypesAndAmountsSelector.Object);

            featsData = new Dictionary<string, IEnumerable<string>>();

            mockCollectionsSelector.Setup(s => s.SelectFrom(It.IsAny<string>(), It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, It.IsAny<string>()))
                .Returns(new List<TypeAndAmountSelection>());
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.FeatData)).Returns(featsData);
        }

        [Test]
        public void GetSpecialQualities()
        {
            var specialQualities = new[]
            {
                BuildSpecialQualityData("special quality 1", string.Empty, 0, string.Empty, 600, 9266, 0, string.Empty, "ginormous"),
                BuildSpecialQualityData("special quality 2", "focusness", 42, "fortnight", 0, 0, 90210, "12d34", string.Empty),
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialQualityData, "creature")).Returns(specialQualities);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality 1")).Returns(new[] { "feat 1", "feat 2/focus" });

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 14 },
                new TypeAndAmountSelection { Type = "other ability", Amount = 14 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality 2")).Returns(abilityRequirements);

            var racialFeats = featsSelector.SelectSpecialQualities("creature");
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality 1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(first.Power, Is.EqualTo(0));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MaximumHitDieRequirement, Is.EqualTo(600));
            Assert.That(first.MinimumAbilities, Is.Empty);
            Assert.That(first.RandomFociQuantity, Is.Empty);

            var firstRequirement = first.RequiredFeats.First();
            var lastRequirement = first.RequiredFeats.Last();
            Assert.That(firstRequirement.Feat, Is.EqualTo("feat 1"));
            Assert.That(firstRequirement.Focus, Is.Empty);
            Assert.That(lastRequirement.Feat, Is.EqualTo("feat 2"));
            Assert.That(lastRequirement.Focus, Is.EqualTo("focus"));
            Assert.That(first.RequiredFeats.Count(), Is.EqualTo(2));

            Assert.That(last.Feat, Is.EqualTo("special quality 2"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Power, Is.EqualTo(90210));
            Assert.That(last.FocusType, Is.EqualTo("focusness"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.MinimumAbilities["ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities["other ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities.Count, Is.EqualTo(2));
            Assert.That(last.RandomFociQuantity, Is.EqualTo("12d34"));
            Assert.That(last.RequiredFeats, Is.Empty);
        }

        private string BuildSpecialQualityData(string featName, string focus, int frequencyQuantity, string frequencyTimePeriod, int maxHitDice, int minHitDice, int power, string randomFociQuantity, string size)
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

            return string.Join("/", data);
        }

        [Test]
        public void GetDifferentSpecialQualitiesWithSameName()
        {
            var specialQualities = new[]
            {
                BuildSpecialQualityData("special quality 1", string.Empty, 0, string.Empty, 600, 9266, 0, string.Empty, "ginormous"),
                BuildSpecialQualityData("special quality 1", "focusness", 42, "fortnight", 0, 0, 90210, "12d34", string.Empty),
            };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialQualityData, "creature")).Returns(specialQualities);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, "creaturespecial quality 1")).Returns(new[] { "feat 1", "feat 2/focus" });

            var abilityRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "ability", Amount = 14 },
                new TypeAndAmountSelection { Type = "other ability", Amount = 14 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "creaturespecial quality 1")).Returns(abilityRequirements);

            var specialQualitySelection = featsSelector.SelectSpecialQualities("creature");
            Assert.That(specialQualitySelection.Count(), Is.EqualTo(2));

            var first = specialQualitySelection.First();
            var last = specialQualitySelection.Last();

            Assert.That(first.Feat, Is.EqualTo("special quality 1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(first.Power, Is.EqualTo(0));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.MaximumHitDieRequirement, Is.EqualTo(600));
            Assert.That(last.MinimumAbilities["ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities["other ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities.Count, Is.EqualTo(2));
            Assert.That(first.RandomFociQuantity, Is.Empty);

            var firstRequirement = first.RequiredFeats.First();
            var lastRequirement = first.RequiredFeats.Last();
            Assert.That(firstRequirement.Feat, Is.EqualTo("feat 1"));
            Assert.That(firstRequirement.Focus, Is.Empty);
            Assert.That(lastRequirement.Feat, Is.EqualTo("feat 2"));
            Assert.That(lastRequirement.Focus, Is.EqualTo("focus"));
            Assert.That(first.RequiredFeats.Count(), Is.EqualTo(2));

            Assert.That(last.Feat, Is.EqualTo("special quality 1"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Power, Is.EqualTo(90210));
            Assert.That(last.FocusType, Is.EqualTo("focusness"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.MinimumAbilities["ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities["other ability"], Is.EqualTo(14));
            Assert.That(last.MinimumAbilities.Count, Is.EqualTo(2));
            Assert.That(last.RandomFociQuantity, Is.EqualTo("12d34"));

            firstRequirement = last.RequiredFeats.First();
            lastRequirement = last.RequiredFeats.Last();
            Assert.That(firstRequirement.Feat, Is.EqualTo("feat 1"));
            Assert.That(firstRequirement.Focus, Is.Empty);
            Assert.That(lastRequirement.Feat, Is.EqualTo("feat 2"));
            Assert.That(lastRequirement.Focus, Is.EqualTo("focus"));
            Assert.That(first.RequiredFeats.Count(), Is.EqualTo(2));
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
        public void GetFeatsWithRequiredSkillWithFocus()
        {
            featsData["feat"] = BuildFeatData(string.Empty, 0, string.Empty, 0, 0);

            var featRequirements = new Dictionary<string, IEnumerable<string>>();
            featRequirements["feat"] = Enumerable.Empty<string>();
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats)).Returns(featRequirements);

            var skillRankRequirements = new[]
            {
                new TypeAndAmountSelection { Type = "skill 1", Amount = 0 },
                new TypeAndAmountSelection { Type = "skill 2/focus", Amount = 4 },
            };

            mockTypesAndAmountsSelector.Setup(s => s.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "feat")).Returns(skillRankRequirements);

            var additionalFeats = featsSelector.SelectFeats();
            var featSelection = additionalFeats.Single();
            Assert.That(featSelection.Feat, Is.EqualTo("feat"));

            var requiredSkills = featSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("skill 1"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(0));
            Assert.That(requiredSkills[0].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("skill 2"));
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(4));
            Assert.That(requiredSkills[1].Focus, Is.EqualTo("focus"));
            Assert.That(requiredSkills.Length, Is.EqualTo(2));
        }
    }
}