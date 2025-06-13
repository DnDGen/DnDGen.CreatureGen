using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Generators
{
    [TestFixture]
    internal class DemographicsGeneratorTests : IntegrationTests
    {
        private CreatureAsserter creatureAsserter;
        private IDemographicsGenerator demographicsGenerator;
        private ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private ICollectionDataSelector<AdvancementDataSelection> advancementSelector;
        private ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private ICollectionSelector collectionSelector;
        private Dictionary<string, (int min, int max)> rollRanges;
        private Dictionary<string, (int min, int max)> weightRanges;
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = GetNewInstanceOf<CreatureAsserter>();
            demographicsGenerator = GetNewInstanceOf<IDemographicsGenerator>();
            typeAndAmountSelector = GetNewInstanceOf<ICollectionTypeAndAmountSelector>();
            advancementSelector = GetNewInstanceOf<ICollectionDataSelector<AdvancementDataSelection>>();
            creatureDataSelector = GetNewInstanceOf<ICollectionDataSelector<CreatureDataSelection>>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();

            rollRanges = new Dictionary<string, (int min, int max)>
            {
                [SizeConstants.Fine] = (1, 6),
                [SizeConstants.Diminutive] = (6, 12),
                [SizeConstants.Tiny] = (1 * 12, 2 * 12),
                [SizeConstants.Small] = (2 * 12, 4 * 12),
                [SizeConstants.Medium] = (4 * 12, 8 * 12),
                [SizeConstants.Large] = (8 * 12, 16 * 12),
                [SizeConstants.Huge] = (16 * 12, 32 * 12),
                [SizeConstants.Gargantuan] = (32 * 12, 64 * 12),
                [SizeConstants.Colossal] = (64 * 12, int.MaxValue),
            };
            weightRanges = new Dictionary<string, (int min, int max)>
            {
                [SizeConstants.Fine] = (0, 1),
                [SizeConstants.Diminutive] = (0, 1),
                [SizeConstants.Tiny] = (1, 8),
                [SizeConstants.Small] = (8, 60),
                [SizeConstants.Medium] = (60, 500),
                [SizeConstants.Large] = (500, 2 * 2000),
                [SizeConstants.Huge] = (2 * 2000, 16 * 2000),
                [SizeConstants.Gargantuan] = (16 * 2000, 125 * 2000),
                [SizeConstants.Colossal] = (125 * 2000, int.MaxValue),
            };
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureMeasurementsScaleCorrectlyWithAdvancedSizes(string creature)
        {
            var advancements = advancementSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, creature);
            if (!advancements.Any())
                Assert.Pass($"Creature {creature} has no advancements");

            var heights = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Heights, creature);
            var lengths = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Lengths, creature);
            var weights = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Weights, creature);
            var genders = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Genders, creature);
            var data = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, creature);

            foreach (var gender in genders)
            {
                var isTall = IsTall(heights, lengths, creature, gender);
                var heightRoll = GetRoll(heights, creature, gender);
                var lengthRoll = GetRoll(lengths, creature, gender);
                var weightRoll = GetMultipliedRoll(weights, isTall ? heights : lengths, creature, gender);

                foreach (var advancement in advancements)
                {
                    var demographics = new Demographics
                    {
                        Height = new("inches") { Value = dice.Roll(heightRoll).AsPotentialAverage() },
                        Length = new("inches") { Value = dice.Roll(lengthRoll).AsPotentialAverage() },
                        Weight = new("pounds") { Value = dice.Roll(weightRoll).AsPotentialAverage() },
                    };

                    var scaledDemographics = demographicsGenerator.AdjustDemographicsBySize(demographics, data.Size, advancement.Size);
                    if (isTall)
                        Assert.That(scaledDemographics.Height.Value, Is.InRange(rollRanges[advancement.Size].min, rollRanges[advancement.Size].max));
                    else
                        Assert.That(scaledDemographics.Length.Value, Is.InRange(rollRanges[advancement.Size].min, rollRanges[advancement.Size].max));

                    if (scaledDemographics.Weight.Value > 0)
                        Assert.That(scaledDemographics.Weight.Value, Is.InRange(weightRanges[advancement.Size].min, weightRanges[advancement.Size].max));
                }
            }
        }

        private bool IsTall(
            IEnumerable<TypeAndAmountDataSelection> heightSelections,
            IEnumerable<TypeAndAmountDataSelection> lengthSelections,
            string creature,
            string gender)
        {
            var heightRoll = GetRoll(heightSelections, creature, gender);
            var lengthRoll = GetRoll(lengthSelections, creature, gender);
            var averageHeight = dice.Roll(heightRoll).AsPotentialAverage();
            var averageLength = dice.Roll(lengthRoll).AsPotentialAverage();
            return averageHeight >= averageLength;
        }

        private string GetRoll(IEnumerable<TypeAndAmountDataSelection> selections, string creature, string gender)
        {
            var baseSelection = selections.First(h => h.Type == gender);
            var modifierSelection = selections.First(h => h.Type == creature);
            return $"{baseSelection.Roll}+{modifierSelection.Roll}";
        }

        private string GetMultipliedRoll(
            IEnumerable<TypeAndAmountDataSelection> weightSelections,
            IEnumerable<TypeAndAmountDataSelection> multiplierSelections,
            string creature, string gender)
        {
            var roll = GetRoll(weightSelections, creature, gender);
            var multiplierSelection = multiplierSelections.First(h => h.Type == creature);

            return $"{roll}*{multiplierSelection.Roll}";
        }

        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Orc_Half)]
        public void DEBUG_GenerateBetaDemographics(string creature)
        {
            var demographics = demographicsGenerator.Generate(creature);
            Assert.That(demographics, Is.Not.Null);
        }
    }
}
