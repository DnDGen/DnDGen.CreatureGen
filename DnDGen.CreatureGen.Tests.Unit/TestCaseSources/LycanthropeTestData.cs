using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.TestCaseSources
{
    public class LycanthropeTestData
    {
        public static IEnumerable IncompatibleFilters
        {
            get
            {
                yield return new TestCaseData(false, "subtype 1", ChallengeRatingConstants.CR3, "wrong alignment", "Alignment filter 'wrong alignment' is not valid");
                yield return new TestCaseData(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match updated creature CR 3 (from CR 1)");
                yield return new TestCaseData(false, "wrong subtype", ChallengeRatingConstants.CR3, "original alignment", "Type filter 'wrong subtype' is not valid");
                //INFO: This test case isn't valid, since As Character doesn't affect already-generated creature compatibility
                //yield return new TestCaseData(true, "subtype 1", ChallengeRatingConstants.CR3, "original alignment");
            }
        }

        public static IEnumerable BUG_HitPointTotals
        {
            get
            {
                yield return new TestCaseData(1, 4, 1, 3, 76, 10);
                yield return new TestCaseData(1, 4, 2, 3, 76, 10);
                yield return new TestCaseData(1, 4, 3, 3, 76, 10);
                yield return new TestCaseData(1, 5, 1, 3.5, 4, 10);
                yield return new TestCaseData(1, 5, 2, 3.5, 4, 10);
                yield return new TestCaseData(1, 5, 3, 3.5, 4, 10);
                yield return new TestCaseData(1, 9, 1, 5.5, 44, 4);
                yield return new TestCaseData(1, 9, 2, 5.5, 44, 4);
                yield return new TestCaseData(1, 9, 5, 5.5, 44, 4);
                yield return new TestCaseData(1, 10, 6, 6, 7, 9);
                yield return new TestCaseData(2, 6, 8, 8, 71, 10);
                yield return new TestCaseData(8, 11, 52, 52, 8, 11);
            }
        }

        public static IEnumerable SizeComparisons
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var i = 1; i < sizes.Length; i++)
                {
                    yield return new TestCaseData(sizes[i - 1], sizes[i]);
                }
            }
        }

        public static IEnumerable Sizes => SizeConstants.GetOrdered().Select(s => new TestCaseData(s));

        //INFO: Reducing the number of test cases, only getting 1 size too big or too small
        public static IEnumerable SizeCompatible
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var c = 0; c < sizes.Length; c++)
                {
                    var startCompare = Math.Max(0, c - 2);
                    var endCompare = Math.Min(sizes.Length - 1, c + 2);

                    for (var a = startCompare; a <= endCompare; a++)
                    {
                        var compatible = Math.Abs(c - a) <= 1;

                        yield return new TestCaseData(sizes[c], sizes[a], compatible);
                    }
                }
            }
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        public static IEnumerable ChallengeRatings
        {
            get
            {
                //INFO: Don't need to test every CR, since it is the basic Increase functionality, which is tested separately
                //So, we only need to test the amount it is increased, not every CR permutation
                var challengeRating = ChallengeRatingConstants.CR1;
                var hitDiceQuantities = new[]
                {
                    0.5, 1, 2, 3, 4, 5, 6, 9, 10, 11, 19, 20, 21
                };

                foreach (var hitDiceQuantity in hitDiceQuantities)
                {
                    var increase = 0;

                    if (hitDiceQuantity <= 2)
                        increase = 2;
                    else if (hitDiceQuantity <= 5)
                        increase = 3;
                    else if (hitDiceQuantity <= 10)
                        increase = 4;
                    else if (hitDiceQuantity <= 20)
                        increase = 5;
                    else if (hitDiceQuantity > 20)
                        increase = 6;

                    var newCr = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, increase);
                    yield return new TestCaseData(challengeRating, hitDiceQuantity, newCr);
                }
            }
        }

        //Afflicted, +2
        //Natural, +3
        public static IEnumerable LevelAdjustments
        {
            get
            {
                var levelAdjustments = new int?[]
                {
                    null,
                    0,
                    1,
                    2,
                    10,
                };

                foreach (var levelAdjustment in levelAdjustments)
                {
                    if (levelAdjustment == null)
                    {
                        yield return new TestCaseData(null, null, true);
                        yield return new TestCaseData(null, null, false);
                    }
                    else
                    {
                        yield return new TestCaseData(levelAdjustment, levelAdjustment + 3, true);
                        yield return new TestCaseData(levelAdjustment, levelAdjustment + 2, false);
                    }
                }
            }
        }

        public static IEnumerable CreatureTypeCompatible
        {
            get
            {
                var compatibilities = new[]
                {
                    (CreatureConstants.Types.Aberration, false),
                    (CreatureConstants.Types.Animal, false),
                    (CreatureConstants.Types.Construct, false),
                    (CreatureConstants.Types.Dragon, false),
                    (CreatureConstants.Types.Elemental, false),
                    (CreatureConstants.Types.Fey, false),
                    (CreatureConstants.Types.Giant, true),
                    (CreatureConstants.Types.Humanoid, true),
                    (CreatureConstants.Types.MagicalBeast, false),
                    (CreatureConstants.Types.MonstrousHumanoid, false),
                    (CreatureConstants.Types.Ooze, false),
                    (CreatureConstants.Types.Outsider, false),
                    (CreatureConstants.Types.Plant, false),
                    (CreatureConstants.Types.Undead, false),
                    (CreatureConstants.Types.Vermin, false),
                };

                foreach (var compatibility in compatibilities)
                {
                    yield return new TestCaseData(compatibility.Item1, compatibility.Item2);
                }
            }
        }
    }
}
