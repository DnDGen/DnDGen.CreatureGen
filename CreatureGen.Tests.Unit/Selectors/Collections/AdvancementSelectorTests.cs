using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Collections
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
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
        }

        private void SetUpAdvancement(
            string size,
            string challengeRating,
            int additionalHitDice,
            double space = 0,
            double reach = 0,
            int strengthAdjustment = 0,
            int dexterityAdjustment = 0,
            int constitutionAdjustment = 0,
            int naturalArmorAdjustment = 0,
            int casterLevel = 0)
        {
            var data = new string[9];
            data[DataIndexConstants.AdvancementSelectionData.CasterLevel] = casterLevel.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRating] = challengeRating;
            data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = constitutionAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = dexterityAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = naturalArmorAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Reach] = reach.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Size] = size;
            data[DataIndexConstants.AdvancementSelectionData.Space] = space.ToString();
            data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = strengthAdjustment.ToString();

            var typeAndAmountSelection = new TypeAndAmountSelection();
            typeAndAmountSelection.Amount = additionalHitDice;
            typeAndAmountSelection.Type = string.Join(",", data);

            typesAndAmounts.Add(typeAndAmountSelection);
        }

        [Test]
        public void SelectAdvancedSpace()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, space: 92.66);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.Space, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectAdvancedReach()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, reach: 92.66);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.Reach, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectRandomFromMultipleAdvancements()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42);
            SetUpAdvancement("other advanced size", ChallengeRatingConstants.Two, 9266);
            SetUpAdvancement("wrong advanced size", "wrong advanced challenge rating", 90210);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.ElementAt(1));

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(9266));
            Assert.That(advancement.Size, Is.EqualTo("other advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.Two));
        }

        [Test]
        public void SelectNoAdvancements()
        {
            Assert.That(() => advancementSelector.SelectRandomFor("creature", creatureType), Throws.Exception);
        }

        [Test]
        public void IsRandomlyAdvanced()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void IsRandomlyNotAdvanced()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42);

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

        [Test]
        public void SelectAdvancedStrength()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, strengthAdjustment: 9266);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectAdvancedDexterity()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, dexterityAdjustment: 9266);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.DexterityAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectAdvancedConstitution()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, constitutionAdjustment: 9266);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectAdvancedNaturalArmor()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, naturalArmorAdjustment: 9266);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectAdvancedChallengeRatingBySize()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
        }

        [TestCaseSource(typeof(AdvancedChallengeRatingTestData), "All")]
        public void SelectAdvancedChallengeRatingByHitDice(string challengeRating, int hitDice, int divisor, string advancedChallengeRating)
        {
            SetUpAdvancement("advanced size", challengeRating, hitDice);

            var typeAndAmount = new TypeAndAmountSelection();
            typeAndAmount.Type = "creature type";
            typeAndAmount.Amount = divisor;

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.TypeAndAmount.Advancements, "creature type")).Returns(typeAndAmount);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(hitDice));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(advancedChallengeRating));
        }

        private class AdvancedChallengeRatingTestData
        {
            private static IEnumerable<int> divisors = new[]
            {
                1, 2, 3, 4
            };

            public static IEnumerable All
            {
                get
                {
                    //INFO: Skipping the first, as it is Zero
                    var challengeRatings = ChallengeRatingConstants.GetOrdered().Skip(1).ToArray();
                    var additionalHitDices = Enumerable.Range(1, 50);

                    foreach (var challengeRating in challengeRatings)
                    {
                        foreach (var divisor in divisors)
                        {
                            foreach (var additionalHitDice in additionalHitDices)
                            {
                                var advancedChallengeRating = string.Empty;
                                var numericChallengeRating = 0;
                                var advancementAmount = additionalHitDice / divisor;
                                var index = Array.IndexOf(challengeRatings, challengeRating);

                                if (int.TryParse(challengeRating, out numericChallengeRating))
                                {
                                    advancedChallengeRating = Convert.ToString(numericChallengeRating + advancementAmount);
                                }
                                else if (index + advancementAmount < challengeRatings.Length)
                                {
                                    advancedChallengeRating = challengeRatings[index + advancementAmount];
                                }
                                else
                                {
                                    var lastCR = challengeRatings.Last();
                                    var lastNumericCR = Convert.ToInt32(lastCR);
                                    var numericAdvancedChallengeRating = lastNumericCR + index + advancementAmount - challengeRatings.Length + 1;

                                    advancedChallengeRating = Convert.ToString(numericAdvancedChallengeRating);
                                }

                                yield return new TestCaseData(challengeRating, additionalHitDice, divisor, advancedChallengeRating);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void SelectAdvancedCasterLevel()
        {
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, casterLevel: 9266);

            var advancement = advancementSelector.SelectRandomFor("creature", creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectAdvancedBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, strengthAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, constitutionAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, naturalArmorAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, casterLevel: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestStrengthAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, strengthAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestConstitutionAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, constitutionAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestNaturalArmorAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, naturalArmorAdjustment: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdvancedGreaterBarghestCasterLevelAsAdditionalHitDice()
        {
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.Advancements, CreatureConstants.Barghest_Greater)).Returns(typesAndAmounts);
            SetUpAdvancement("advanced size", ChallengeRatingConstants.One, 42, casterLevel: 666);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, creatureType);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }
    }
}
