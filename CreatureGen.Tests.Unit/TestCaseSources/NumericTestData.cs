using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.TestCaseSources
{
    public class NumericTestData
    {
        private static IEnumerable<int> testValues = new[]
        {
            -90210,
            -9266,
            -8245,
            -1337,
            -1336,
            -783,
            -600,
            -96,
            -42,
            -2,
            -1,
            0,
            1,
            2,
            42,
            96,
            600,
            783,
            1336,
            1337,
            8245,
            9266,
            90210
        };

        public static IEnumerable AllValues
        {
            get
            {
                foreach (var value in testValues)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable AllPositiveValues
        {
            get
            {
                var positive = testValues.Where(v => v > 0);

                foreach (var value in positive)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable AllNonPositiveValues
        {
            get
            {
                var positive = testValues.Where(v => v <= 0);

                foreach (var value in positive)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable ValueLessThanPositiveRequirement
        {
            get
            {
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in positive)
                {
                    var values = testValues.Where(v => v < requirement);

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
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in testValues)
                {
                    var values = testValues.Where(v => v >= requirement);

                    foreach (var value in values)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }

        public static IEnumerable SumOfValuesLessThanPositiveRequirement
        {
            get
            {
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in positive)
                {
                    foreach (var value1 in positive)
                    {
                        foreach (var value2 in testValues)
                        {
                            if (value1 + value2 < requirement)
                                yield return new TestCaseData(requirement, value1, value2);
                        }
                    }
                }
            }
        }

        public static IEnumerable SumOfValuesLessThanPositiveRequirementWithMinimumOne
        {
            get
            {
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in positive)
                {
                    foreach (var value1 in positive)
                    {
                        foreach (var value2 in testValues)
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
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in positive)
                {
                    foreach (var value1 in positive)
                    {
                        foreach (var value2 in testValues)
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
                var positive = testValues.Where(v => v > 0);

                foreach (var requirement in positive)
                {
                    foreach (var value in testValues)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }
    }
}
