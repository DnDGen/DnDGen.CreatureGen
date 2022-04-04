using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AdvancementSelectorTests
    {
        private IAdvancementSelector advancementSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private List<TypeAndAmountSelection> typesAndAmounts;
        private CreatureType creatureType;
        private TypeAndAmountSelection creatureTypeDivisor;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            advancementSelector = new AdvancementSelector(
                mockTypeAndAmountSelector.Object,
                mockPercentileSelector.Object,
                mockCollectionSelector.Object,
                mockAdjustmentsSelector.Object);

            typesAndAmounts = new List<TypeAndAmountSelection>();
            creatureType = new CreatureType();
            creatureType.Name = "creature type";

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, "creature")).Returns(typesAndAmounts);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(1);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(int.MaxValue);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "other template")).Returns(int.MaxValue);

            creatureTypeDivisor = new TypeAndAmountSelection();
            creatureTypeDivisor.Type = "creature type";
            creatureTypeDivisor.Amount = 999999999;

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.TypeAndAmount.Advancements, "creature type")).Returns(creatureTypeDivisor);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.Last());
        }

        [Test]
        public void SelectAdvancement()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [Test]
        public void SelectValidAdvancement_NoTemplate()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(550);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(610);

            var advancement = advancementSelector.SelectRandomFor("creature", null, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [Test]
        public void SelectValidAdvancement_Template()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(550);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(610);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [Test]
        public void SelectValidAdvancement_MultipleTemplates()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(550);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "other template")).Returns(610);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        private void SetUpAdvancement(
            string size,
            int additionalHitDice,
            double space = 0,
            double reach = 0)
        {
            var data = new string[3];
            data[DataIndexConstants.AdvancementSelectionData.Reach] = reach.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Size] = size;
            data[DataIndexConstants.AdvancementSelectionData.Space] = space.ToString();

            var typeAndAmountSelection = new TypeAndAmountSelection();
            typeAndAmountSelection.Amount = additionalHitDice;
            typeAndAmountSelection.Type = string.Join(",", data);

            typesAndAmounts.Add(typeAndAmountSelection);
        }

        [Test]
        public void SelectAdvancedSpace()
        {
            SetUpAdvancement(SizeConstants.Large, 42, space: 92.66);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.Space, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectAdvancedReach()
        {
            SetUpAdvancement(SizeConstants.Large, 42, reach: 92.66);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.Reach, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectRandomFromMultipleAdvancements()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 9266);
            SetUpAdvancement("wrong advanced size", 90210);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.ElementAt(1));

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(9266));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Huge));
        }

        [Test]
        public void SelectRandomFromMultipleValidAdvancements()
        {
            SetUpAdvancement(SizeConstants.Medium, 42);
            SetUpAdvancement(SizeConstants.Large, 96);
            SetUpAdvancement(SizeConstants.Huge, 9266);
            SetUpAdvancement("wrong advanced size", 90210);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.ElementAt(1));

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(550);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(700);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(96));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [TestCase(0.1, 19)]
        [TestCase(0.5, 19)]
        [TestCase(1, 19)]
        [TestCase(2, 18)]
        [TestCase(10, 10)]
        [TestCase(19, 1)]
        [TestCase(20, 0)]
        public void SelectMinimumFromNoValidAdvancements(double creatureHitDice, int additionalHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 42);
            SetUpAdvancement(SizeConstants.Large, 96);
            SetUpAdvancement(SizeConstants.Huge, 9266);
            SetUpAdvancement("wrong advanced size", 90210);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(creatureHitDice);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(20);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(additionalHitDice));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Medium));
        }

        [TestCase(0.1, 19)]
        [TestCase(0.5, 19)]
        [TestCase(1, 19)]
        [TestCase(2, 18)]
        [TestCase(10, 10)]
        [TestCase(19, 1)]
        [TestCase(20, 0)]
        public void SelectMinimumFromNoValidAdvancements_MultipleTemplates(double creatureHitDice, int additionalHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 42);
            SetUpAdvancement(SizeConstants.Large, 96);
            SetUpAdvancement(SizeConstants.Huge, 9266);
            SetUpAdvancement("wrong advanced size", 90210);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(creatureHitDice);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "template")).Returns(200);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, "other template")).Returns(20);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(additionalHitDice));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Medium));
        }

        [Test]
        public void SelectNoAdvancements()
        {
            Assert.That(() => advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1), Throws.Exception);
        }

        [Test]
        public void IsAdvanced_IsRandomlyAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", null);
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void IsAdvanced_IsRandomlyNotAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(false);

            var isAdvanced = advancementSelector.IsAdvanced("creature", null);
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsAdvanced_IsNotAdvanced_IfNoAdvancements()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", null);
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsAdvanced_IsNotAdvanced_WhenChallengeRatingIsFiltered()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", "my challenge rating");
            Assert.That(isAdvanced, Is.False);
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.Strength))]
        public void SelectAdvancedStrength(string originalSize, string advancedSize, int strengthAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, originalSize, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(strengthAdjustment));
        }

        private class AdvancementsTestData
        {
            public static IEnumerable Strength
            {
                get
                {
                    var sizes = SizeConstants.GetOrdered();

                    for (var i = 0; i < sizes.Length; i++)
                    {
                        for (var j = i; j < sizes.Length; j++)
                        {
                            var strengthAdjustment = GetStrengthAdjustment(sizes[i], sizes[j]);
                            yield return new TestCaseData(sizes[i], sizes[j], strengthAdjustment);
                        }
                    }
                }
            }

            private static int GetStrengthAdjustment(string originalSize, string advancedSize)
            {
                var strengthAdjustment = 0;
                var currentSize = originalSize;

                while (currentSize != advancedSize)
                {
                    switch (currentSize)
                    {
                        case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                        case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; strengthAdjustment += 2; break;
                        case SizeConstants.Tiny: currentSize = SizeConstants.Small; strengthAdjustment += 4; break;
                        case SizeConstants.Small: currentSize = SizeConstants.Medium; strengthAdjustment += 4; break;
                        case SizeConstants.Medium: currentSize = SizeConstants.Large; strengthAdjustment += 8; break;
                        case SizeConstants.Large: currentSize = SizeConstants.Huge; strengthAdjustment += 8; break;
                        case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; strengthAdjustment += 8; break;
                        case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; strengthAdjustment += 8; break;
                        case SizeConstants.Colossal:
                        default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                    }
                }

                return strengthAdjustment;
            }

            public static IEnumerable Dexterity
            {
                get
                {
                    var sizes = SizeConstants.GetOrdered();

                    for (var i = 0; i < sizes.Length; i++)
                    {
                        for (var j = i; j < sizes.Length; j++)
                        {
                            var dexterityAdjustment = GetDexterityAdjustment(sizes[i], sizes[j]);
                            yield return new TestCaseData(sizes[i], sizes[j], dexterityAdjustment);
                        }
                    }
                }
            }

            private static int GetDexterityAdjustment(string originalSize, string advancedSize)
            {
                var dexterityAdjustment = 0;
                var currentSize = originalSize;

                while (currentSize != advancedSize)
                {
                    switch (currentSize)
                    {
                        case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; dexterityAdjustment -= 2; break;
                        case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; dexterityAdjustment -= 2; break;
                        case SizeConstants.Tiny: currentSize = SizeConstants.Small; dexterityAdjustment -= 2; break;
                        case SizeConstants.Small: currentSize = SizeConstants.Medium; dexterityAdjustment -= 2; break;
                        case SizeConstants.Medium: currentSize = SizeConstants.Large; dexterityAdjustment -= 2; break;
                        case SizeConstants.Large: currentSize = SizeConstants.Huge; dexterityAdjustment -= 2; break;
                        case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; break;
                        case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; break;
                        case SizeConstants.Colossal:
                        default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                    }
                }

                return dexterityAdjustment;
            }

            public static IEnumerable Constitution
            {
                get
                {
                    var sizes = SizeConstants.GetOrdered();

                    for (var i = 0; i < sizes.Length; i++)
                    {
                        for (var j = i; j < sizes.Length; j++)
                        {
                            var constitutionAdjustment = GetConstitutionAdjustment(sizes[i], sizes[j]);
                            yield return new TestCaseData(sizes[i], sizes[j], constitutionAdjustment);
                        }
                    }
                }
            }

            private static int GetConstitutionAdjustment(string originalSize, string advancedSize)
            {
                var constitutionAdjustment = 0;
                var currentSize = originalSize;

                while (currentSize != advancedSize)
                {
                    switch (currentSize)
                    {
                        case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                        case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                        case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                        case SizeConstants.Small: currentSize = SizeConstants.Medium; constitutionAdjustment += 2; break;
                        case SizeConstants.Medium: currentSize = SizeConstants.Large; constitutionAdjustment += 4; break;
                        case SizeConstants.Large: currentSize = SizeConstants.Huge; constitutionAdjustment += 4; break;
                        case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; constitutionAdjustment += 4; break;
                        case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; constitutionAdjustment += 4; break;
                        case SizeConstants.Colossal:
                        default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                    }
                }

                return constitutionAdjustment;
            }

            public static IEnumerable NaturalArmor
            {
                get
                {
                    var sizes = SizeConstants.GetOrdered();

                    for (var i = 0; i < sizes.Length; i++)
                    {
                        for (var j = i; j < sizes.Length; j++)
                        {
                            var naturalArmorAdjustment = GetNaturalArmorAdjustment(sizes[i], sizes[j]);
                            yield return new TestCaseData(sizes[i], sizes[j], naturalArmorAdjustment);
                        }
                    }
                }
            }

            private static int GetNaturalArmorAdjustment(string originalSize, string advancedSize)
            {
                var naturalArmorAdjustment = 0;
                var currentSize = originalSize;

                while (currentSize != advancedSize)
                {
                    switch (currentSize)
                    {
                        case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                        case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                        case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                        case SizeConstants.Small: currentSize = SizeConstants.Medium; break;
                        case SizeConstants.Medium: currentSize = SizeConstants.Large; naturalArmorAdjustment += 2; break;
                        case SizeConstants.Large: currentSize = SizeConstants.Huge; naturalArmorAdjustment += 3; break;
                        case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; naturalArmorAdjustment += 4; break;
                        case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; naturalArmorAdjustment += 5; break;
                        case SizeConstants.Colossal:
                        default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                    }
                }

                return naturalArmorAdjustment;
            }
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.Dexterity))]
        public void SelectAdvancedDexterity(string originalSize, string advancedSize, int dexterityAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, originalSize, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.DexterityAdjustment, Is.EqualTo(dexterityAdjustment));
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.Constitution))]
        public void SelectAdvancedConstitution(string originalSize, string advancedSize, int constitutionAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, originalSize, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(constitutionAdjustment));
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.NaturalArmor))]
        public void SelectAdvancedNaturalArmor(string originalSize, string advancedSize, int naturalArmorAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, originalSize, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(naturalArmorAdjustment));
        }

        [TestCaseSource(typeof(AdvancedChallengeRatingTestData), nameof(AdvancedChallengeRatingTestData.HitDice))]
        public void SelectAdvancedChallengeRatingByHitDice(string challengeRating, int hitDice, int divisor, string advancedChallengeRating)
        {
            SetUpAdvancement(SizeConstants.Medium, hitDice);

            var typeAndAmount = new TypeAndAmountSelection();
            typeAndAmount.Type = "creature type";
            typeAndAmount.Amount = divisor;

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.TypeAndAmount.Advancements, "creature type")).Returns(typeAndAmount);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, challengeRating);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(hitDice));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Medium));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(advancedChallengeRating));
        }

        private class AdvancedChallengeRatingTestData
        {
            public static IEnumerable HitDice
            {
                get
                {
                    var challengeRatings = ChallengeRatingConstants.GetOrdered();
                    var additionalHitDices = Enumerable.Range(1, 50);
                    var divisors = Enumerable.Range(1, 4);

                    foreach (var challengeRating in challengeRatings)
                    {
                        foreach (var divisor in divisors)
                        {
                            foreach (var additionalHitDice in additionalHitDices)
                            {
                                var advancementAmount = additionalHitDice / divisor;
                                var advancedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, advancementAmount);

                                yield return new TestCaseData(challengeRating, additionalHitDice, divisor, advancedChallengeRating);
                            }
                        }
                    }
                }
            }

            public static IEnumerable Size
            {
                get
                {
                    var challengeRatings = ChallengeRatingConstants.GetOrdered();
                    var sizes = SizeConstants.GetOrdered();

                    foreach (var challengeRating in challengeRatings)
                    {
                        for (var i = 0; i < sizes.Length; i++)
                        {
                            var originalSize = sizes[i];

                            for (var j = i; j < sizes.Length; j++)
                            {
                                var advancedSize = sizes[j];
                                var advancedChallengeRating = GetAdjustedChallengeRating(originalSize, advancedSize, challengeRating);

                                yield return new TestCaseData(originalSize, advancedSize, challengeRating, advancedChallengeRating);
                            }
                        }
                    }
                }
            }

            private static string GetAdjustedChallengeRating(string originalSize, string advancedSize, string originalChallengeRating)
            {
                var sizes = SizeConstants.GetOrdered();
                var originalSizeIndex = Array.IndexOf(sizes, originalSize);
                var advancedIndex = Array.IndexOf(sizes, advancedSize);
                var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);

                if (advancedIndex < largeIndex || originalSize == advancedSize)
                {
                    return originalChallengeRating;
                }

                var increase = advancedIndex - Math.Max(largeIndex - 1, originalSizeIndex);
                var adjustedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, increase);

                return adjustedChallengeRating;
            }
        }

        [TestCaseSource(typeof(AdvancedChallengeRatingTestData), nameof(AdvancedChallengeRatingTestData.Size))]
        public void SelectAdvancedChallengeRatingBySize(string originalSize, string advancedSize, string originalChallengeRating, string advancedChallengeRating)
        {
            SetUpAdvancement(advancedSize, 1);

            var typeAndAmount = new TypeAndAmountSelection();
            typeAndAmount.Type = "creature type";
            typeAndAmount.Amount = 2;

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.TypeAndAmount.Advancements, "creature type")).Returns(typeAndAmount);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, originalSize, originalChallengeRating);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(1));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(advancedChallengeRating));
        }

        [Test]
        public void SelectAdvancedCasterLevel()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(0));
        }

        [Test]
        public void SelectAdvancedBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, new[] { "template", "other template" }, creatureType, SizeConstants.Medium, ChallengeRatingConstants.CR1);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }
    }
}
