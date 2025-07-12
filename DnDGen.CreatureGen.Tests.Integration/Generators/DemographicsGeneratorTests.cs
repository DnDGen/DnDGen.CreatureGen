using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Generators
{
    [TestFixture]
    internal class DemographicsGeneratorTests : IntegrationTests
    {
        private IDemographicsGenerator demographicsGenerator;
        private Dictionary<string, (int min, int max)> rollRanges;
        private Dictionary<string, (int min, int max)> weightRanges;

        [SetUp]
        public void Setup()
        {
            demographicsGenerator = GetNewInstanceOf<IDemographicsGenerator>();

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

        public static IEnumerable SizeAdvancements
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var o = 0; o < sizes.Length; o++)
                {
                    for (var a = o; a < sizes.Length; a++)
                    {
                        yield return new TestCaseData(sizes[o], sizes[a]);
                    }
                }
            }
        }

        private void AssertDemographicsScaleCorrectly(string originalSize, string advancedSize, double originalValue, double originalWeight)
        {
            var demographics = new Demographics
            {
                Height = new("inches") { Value = originalValue },
                Length = new("inches") { Value = originalValue },
                Wingspan = new("inches") { Value = originalValue },
                Weight = new("pounds") { Value = originalWeight },
            };

            var scaledDemographics = demographicsGenerator.AdjustDemographicsBySize(demographics, originalSize, advancedSize);
            Assert.That(scaledDemographics.Height.Value, Is.InRange(rollRanges[advancedSize].min, rollRanges[advancedSize].max));
            Assert.That(scaledDemographics.Length.Value, Is.InRange(rollRanges[advancedSize].min, rollRanges[advancedSize].max));
            Assert.That(scaledDemographics.Wingspan.Value, Is.InRange(rollRanges[advancedSize].min, rollRanges[advancedSize].max));
            Assert.That(scaledDemographics.Weight.Value, Is.InRange(weightRanges[advancedSize].min, weightRanges[advancedSize].max));
        }

        [TestCaseSource(nameof(SizeAdvancements))]
        public void DemographicsScaleCorrectly_Average(string originalSize, string advancedSize)
            => AssertDemographicsScaleCorrectly(originalSize, advancedSize,
                rollRanges[originalSize].min / 2d + rollRanges[originalSize].max / 2d,
                weightRanges[originalSize].min / 2d + weightRanges[originalSize].max / 2d);

        [Test]
        public void DEBUG_GenerateBetaDemographics()
        {
            var demographics = demographicsGenerator.Generate(CreatureConstants.Shrieker);
            Assert.That(demographics, Is.Not.Null);
        }
    }
}
