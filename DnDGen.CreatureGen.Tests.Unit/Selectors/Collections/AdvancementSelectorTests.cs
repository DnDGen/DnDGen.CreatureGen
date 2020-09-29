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
        private List<TypeAndAmountSelection> typesAndAmounts;
        private CreatureType creatureType;
        private TypeAndAmountSelection creatureTypeDivisor;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            advancementSelector = new AdvancementSelector(mockTypeAndAmountSelector.Object, mockPercentileSelector.Object, mockCollectionSelector.Object);

            typesAndAmounts = new List<TypeAndAmountSelection>();
            creatureType = new CreatureType();
            creatureType.Name = "creature type";

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, "creature")).Returns(typesAndAmounts);

            creatureTypeDivisor = new TypeAndAmountSelection();
            creatureTypeDivisor.Type = "creature type";
            creatureTypeDivisor.Amount = 999999999;

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.TypeAndAmount.Advancements, "creature type")).Returns(creatureTypeDivisor);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
        }

        [Test]
        public void SelectRandomAdvancement()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
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

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.Space, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectAdvancedReach()
        {
            SetUpAdvancement(SizeConstants.Large, 42, reach: 92.66);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
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

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(9266));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Huge));
        }

        [Test]
        public void SelectNoAdvancements()
        {
            Assert.That(() => advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One), Throws.Exception);
        }

        [Test]
        public void IsRandomlyAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void IsRandomlyNotAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(false);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsNotAdvancedIfNoAdvancements()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.False);
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.Strength))]
        public void SelectAdvancedStrength(string originalSize, string advancedSize, int strengthAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, originalSize, ChallengeRatingConstants.One);
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

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, originalSize, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.DexterityAdjustment, Is.EqualTo(dexterityAdjustment));
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.Constitution))]
        public void SelectAdvancedConstitution(string originalSize, string advancedSize, int constitutionAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, originalSize, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(constitutionAdjustment));
        }

        [TestCaseSource(typeof(AdvancementsTestData), nameof(AdvancementsTestData.NaturalArmor))]
        public void SelectAdvancedNaturalArmor(string originalSize, string advancedSize, int naturalArmorAdjustment)
        {
            SetUpAdvancement(advancedSize, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, originalSize, ChallengeRatingConstants.One);
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

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, challengeRating);
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
                    //INFO: Skipping the first, as it is Zero
                    var challengeRatings = ChallengeRatingConstants.GetOrdered().Skip(1).ToArray();
                    var additionalHitDices = Enumerable.Range(1, 50);
                    var divisors = Enumerable.Range(1, 4);

                    foreach (var challengeRating in challengeRatings)
                    {
                        foreach (var divisor in divisors)
                        {
                            foreach (var additionalHitDice in additionalHitDices)
                            {
                                var advancementAmount = additionalHitDice / divisor;
                                var advancedChallengeRating = GetNextChallengeRating(challengeRating, advancementAmount);

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
                    //INFO: Skipping the first, as it is Zero
                    var challengeRatings = ChallengeRatingConstants.GetOrdered().Skip(1).ToArray();
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
                var adjustedChallengeRating = originalChallengeRating;
                var currentSize = originalSize;

                while (currentSize != advancedSize)
                {
                    switch (currentSize)
                    {
                        case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                        case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                        case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                        case SizeConstants.Small: currentSize = SizeConstants.Medium; break;
                        case SizeConstants.Medium: currentSize = SizeConstants.Large; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                        case SizeConstants.Large: currentSize = SizeConstants.Huge; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                        case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                        case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                        case SizeConstants.Colossal:
                        default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                    }
                }

                return adjustedChallengeRating;
            }

            private static string GetNextChallengeRating(string challengeRating, int amount = 1)
            {
                var orderedChallengeRatings = ChallengeRatingConstants.GetOrdered();
                var index = Array.IndexOf(orderedChallengeRatings, challengeRating);

                if (index < 0)
                    return IncrementChallengeRating(challengeRating, amount);

                if (index + amount < orderedChallengeRatings.Length)
                    return orderedChallengeRatings[index + amount];

                var lastChallengeRating = orderedChallengeRatings.Last();
                var additionalIncrement = index + amount - orderedChallengeRatings.Length + 1;

                return IncrementChallengeRating(lastChallengeRating, additionalIncrement);
            }

            private static string IncrementChallengeRating(string challengeRating, int amount)
            {
                var numericCR = Convert.ToInt32(challengeRating);
                return Convert.ToString(numericCR + amount);
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

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, originalSize, originalChallengeRating);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(1));
            Assert.That(advancement.Size, Is.EqualTo(advancedSize));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(advancedChallengeRating));
        }

        [Test]
        public void SelectAdvancedCasterLevel()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(0));
        }

        [Test]
        public void SelectAdvancedBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType, SizeConstants.Medium, ChallengeRatingConstants.One);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }
    }
}
