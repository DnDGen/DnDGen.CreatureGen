using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System;
using System.Collections;

namespace DnDGen.CreatureGen.Tests.Unit.TestCaseSources
{
    public class CreatureTestData
    {
        public static IEnumerable SizeIncreases
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var o = 0; o < sizes.Length; o++)
                {
                    var heightMultiplier = GetHeightMultiplier(o);
                    //INFO: index 0 is Fine, which has special rules for scaling weight
                    var weightMultiplier = GetWeightMultiplier(o - 1);
                    yield return new TestCaseData(sizes[0], sizes[o], heightMultiplier, weightMultiplier);
                    yield return new TestCaseData(sizes[o], sizes[o], 1, 1);
                }

                yield return new TestCaseData(SizeConstants.Small, SizeConstants.Large, GetHeightMultiplier(2), GetWeightMultiplier(2));
                yield return new TestCaseData(SizeConstants.Small, SizeConstants.Medium, GetHeightMultiplier(1), GetWeightMultiplier(1));
                yield return new TestCaseData(SizeConstants.Medium, SizeConstants.Large, GetHeightMultiplier(1), GetWeightMultiplier(1));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Huge, GetHeightMultiplier(1), GetWeightMultiplier(1));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Gargantuan, GetHeightMultiplier(2), GetWeightMultiplier(2));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Colossal, GetHeightMultiplier(3), GetWeightMultiplier(3));
            }
        }

        private static int GetHeightMultiplier(int increase) => GetMultiplier(2, increase);
        private static int GetWeightMultiplier(int increase) => GetMultiplier(8, increase);
        private static int GetMultiplier(int root, int exponent) => (int)Math.Pow(root, Math.Max(exponent, 0));
    }
}
