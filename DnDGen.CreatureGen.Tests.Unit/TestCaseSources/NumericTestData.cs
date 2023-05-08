using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.TestCaseSources
{
    public class NumericTestData
    {
        public static IEnumerable<int> BaseTestNumbers => new[]
        {
            0, 1, 2, 10, 100
        };

        public static IEnumerable<int> BaseAbilityTestNumbers => new[]
        {
            3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
        };

        public static IEnumerable<int> AllTestValues => NegativeValues.Union(NonNegativeValues);
        public static IEnumerable<int> AllBaseTestValues => BaseTestNumbers.Union(NegativeBaseValues);
        public static IEnumerable<int> NegativeValues => NonPositiveValues.Where(v => v < 0);
        public static IEnumerable<int> NegativeBaseValues => BaseTestNumbers.Select(n => n * -1);
        public static IEnumerable<int> NonNegativeValues => BaseTestNumbers;
        public static IEnumerable<int> PositiveValues => NonNegativeValues.Where(v => v > 0);
        public static IEnumerable<int> NonPositiveValues => NonNegativeValues.Select(n => n * -1);

        public static IEnumerable AllValues => AllTestValues.Select(v => new TestCaseData(v));
        public static IEnumerable AllPositiveValues => PositiveValues.Select(v => new TestCaseData(v));
        public static IEnumerable AllNonPositiveValues => NonPositiveValues.Select(v => new TestCaseData(v));

        public static IEnumerable ValueLessThanPositiveRequirement
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    var values = AllTestValues.Where(v => v < requirement);

                    foreach (var value in values)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }

        public static IEnumerable ValueGreaterThanOrEqualToPositiveRequirement
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    var values = AllTestValues.Where(v => v >= requirement);

                    foreach (var value in values)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }

        public static IEnumerable SumOfValuesLessThanPositiveRequirementWithMinimumOne
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    foreach (var value1 in PositiveValues)
                    {
                        foreach (var value2 in AllTestValues)
                        {
                            var sum = Math.Max(value1 + value2, 1);

                            if (sum < requirement)
                                yield return new TestCaseData(requirement, value1, value2);
                        }
                    }
                }
            }
        }

        public static IEnumerable SumOfValuesGreaterThanOrEqualToPositiveRequirement
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    foreach (var value1 in PositiveValues)
                    {
                        foreach (var value2 in AllTestValues)
                        {
                            if (value1 + value2 >= requirement)
                                yield return new TestCaseData(requirement, value1, value2);
                        }
                    }
                }
            }
        }

        public static IEnumerable AllValuesAndAllPositiveRequirements
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    foreach (var value in AllTestValues)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }
    }
}
