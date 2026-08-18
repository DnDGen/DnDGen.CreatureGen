using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    internal class LycanthropeApplicatorIsCompatibleTests : LycanthropeApplicatorTestsBase
    {
        [Test]
        public void IsCompatible_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void IsCompatible_ReturnsCompatibility_BasedOnCreatureType(string creatureType, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(SizeCompatible))]
        public void IsCompatible_ReturnsCompatibility_BasedOnSize(string creatureSize, string animalSize, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAlignment_ReturnsTrue_IfPrototypeContainsAlignmentFilter()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAlignment_ReturnsFalse_IfPrototypeDoesNotContainAlignmentFilter()
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void IsCompatible_WithChallengeRating_ReturnsTrue_BasedOnUpdatedChallengeRating(
            string originalChallengeRating,
            double animalHitDiceQuantity,
            string updatedChallengeRating)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_BasedOnUpdatedChallengeRating_NonCharacter(
            string original,
            double animalHitDiceQuantity,
            string challengeRating,
            bool expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered_HumanoidCharacter))]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_BasedOnUpdatedChallengeRating_HumanoidCharacter(
            string original,
            double animalHitDiceQuantity,
            double creatureHitDiceQuantity,
            string challengeRating,
            bool expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_BasedOnUpdatedChallengeRating_NonHumanoidCharacter(
            string original,
            double animalHitDiceQuantity,
            string challengeRating,
            bool expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Animal, false)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, true)]
        [TestCase("wrong type", false)]
        public void IsCompatible_WithType_ReturnsCompatibility_BasedOnUpdatedTypes(string type, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecausePrototype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            Assert.Fail("not yet written");
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        private static IEnumerable ChallengeRatingAdjustments_Filtered_HumanoidCharacter
        {
            get
            {
                var challengeRatings = new Dictionary<string, IEnumerable<double>>
                {
                    [ChallengeRatingConstants.CR0] = [0d], //Humanoid Character
                    [ChallengeRatingConstants.CR1_4th] = [1d], //Kobold
                    [ChallengeRatingConstants.CR1_3rd] = [1d], //Goblin
                    [ChallengeRatingConstants.CR1_2nd] = [1d], //Dwarf, Elf, Gnome, Halfling, Hobgoblin, Merfolk, Orc, Human
                    [ChallengeRatingConstants.CR1] = [1d, 2d], //Duergar, Drow, Gnoll, Svirfneblin, Lizardfolk, Troglodyte
                    [ChallengeRatingConstants.CR3] = [4d], //Ogre
                    [ChallengeRatingConstants.CR5] = [6d], //Troll
                    [ChallengeRatingConstants.CR6] = [10d], //Ettin
                    [ChallengeRatingConstants.CR7] = [12d], //Hill Giant
                    [ChallengeRatingConstants.CR8] = [14d, 5d], //Stone Giant, Ogre Mage
                    [ChallengeRatingConstants.CR9] = [14d], //Frost Giant, Stone Giant Elder
                    [ChallengeRatingConstants.CR10] = [15d], //Fire Giant
                    [ChallengeRatingConstants.CR11] = [17d], //Cloud Giant
                    [ChallengeRatingConstants.CR13] = [19d] //Storm Giant
                };

                var animalHitDiceQuantities = new[]
                {
                    6, //Brown Bear, Dire Wolf, Tiger
                    3, //Boar
                    7, //Dire boar
                    1, //Dire rat
                    2, //Wolf
                };

                foreach (var animalHitDiceQuantity in animalHitDiceQuantities)
                {
                    var increase = 0;

                    if (animalHitDiceQuantity <= 2)
                        increase = 2;
                    else if (animalHitDiceQuantity <= 5)
                        increase = 3;
                    else if (animalHitDiceQuantity <= 10)
                        increase = 4;
                    else if (animalHitDiceQuantity <= 20)
                        increase = 5;
                    else if (animalHitDiceQuantity > 20)
                        increase = 6;

                    foreach (var cr in challengeRatings)
                    {
                        var creatureCr = cr.Key;

                        foreach (var creatureHitDiceQuantity in cr.Value)
                        {
                            if (creatureHitDiceQuantity <= 1)
                                creatureCr = ChallengeRatingConstants.CR0;

                            var low1Cr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase - 1);
                            var newCr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase);
                            var high1Cr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase + 1);

                            if (newCr != creatureCr)
                            {
                                yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, creatureCr, false);
                            }

                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, low1Cr, false);
                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, newCr, true);
                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, high1Cr, false);
                        }
                    }
                }
            }
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        private static IEnumerable ChallengeRatingAdjustments_Filtered
        {
            get
            {
                //INFO: Doing specific numbers, instead of full range because the number of test cases explodes:
                //1. Per challenge rating
                //2. Per hit die quantity
                var challengeRatings = new[]
                {
                    ChallengeRatingConstants.CR0, //Character
                    ChallengeRatingConstants.CR1_4th, //Kobold
                    ChallengeRatingConstants.CR1_3rd, //Goblin
                    ChallengeRatingConstants.CR1_2nd, //Dwarf, Elf, Gnome, Half-Elf, Halforc, Halfling, Hobgoblin, Human, Merfolk, Orc
                    ChallengeRatingConstants.CR1, //Duergar, Drow, Gnoll, Svirfneblin, Lizardfolk, Troglodyte
                    ChallengeRatingConstants.CR3, //Ogre
                    ChallengeRatingConstants.CR5, //Troll
                    ChallengeRatingConstants.CR6, //Ettin
                    ChallengeRatingConstants.CR7, //Hill Giant
                    ChallengeRatingConstants.CR8, //Stone Giant, Ogre Mage
                    ChallengeRatingConstants.CR9, //Frost Giant, Stone Giant Elder
                    ChallengeRatingConstants.CR10, //Fire Giant
                    ChallengeRatingConstants.CR11, //Cloud Giant
                    ChallengeRatingConstants.CR13, //Storm Giant
                };

                var hitDiceQuantities = new[]
                {
                    0.5, 1, 2, 3, 4, 5, 6, 9, 10, 11, 19, 20, 21
                };

                foreach (var animalHitDiceQuantity in hitDiceQuantities)
                {
                    var increase = 0;

                    if (animalHitDiceQuantity <= 2)
                        increase = 2;
                    else if (animalHitDiceQuantity <= 5)
                        increase = 3;
                    else if (animalHitDiceQuantity <= 10)
                        increase = 4;
                    else if (animalHitDiceQuantity <= 20)
                        increase = 5;
                    else if (animalHitDiceQuantity > 20)
                        increase = 6;

                    foreach (var cr in challengeRatings)
                    {
                        var low1Cr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase - 1);
                        var newCr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase);
                        var high1Cr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase + 1);

                        if (newCr != cr)
                        {
                            yield return new TestCaseData(cr, animalHitDiceQuantity, cr, false);
                        }

                        yield return new TestCaseData(cr, animalHitDiceQuantity, low1Cr, false);
                        yield return new TestCaseData(cr, animalHitDiceQuantity, newCr, true);
                        yield return new TestCaseData(cr, animalHitDiceQuantity, high1Cr, false);
                    }
                }
            }
        }
    }
}
