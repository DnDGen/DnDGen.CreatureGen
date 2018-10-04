using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.TestCaseSources
{
    public class NumericTestData
    {
        public static IEnumerable<int> TestValues = new[]
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

        public static IEnumerable<int> PositiveValues => TestValues.Where(v => v > 0);

        public static IEnumerable AllValues
        {
            get
            {
                foreach (var value in TestValues)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable AllPositiveValues
        {
            get
            {
                foreach (var value in PositiveValues)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable AllNonPositiveValues
        {
            get
            {
                foreach (var value in PositiveValues)
                {
                    yield return new TestCaseData(value);
                }
            }
        }

        public static IEnumerable ValueLessThanPositiveRequirement
        {
            get
            {
                foreach (var requirement in PositiveValues)
                {
                    var values = TestValues.Where(v => v < requirement);

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
                    var values = TestValues.Where(v => v >= requirement);

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
                foreach (var requirement in PositiveValues)
                {
                    foreach (var value1 in PositiveValues)
                    {
                        foreach (var value2 in TestValues)
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
                foreach (var requirement in PositiveValues)
                {
                    foreach (var value1 in PositiveValues)
                    {
                        foreach (var value2 in TestValues)
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
                        foreach (var value2 in TestValues)
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
                    foreach (var value in TestValues)
                    {
                        yield return new TestCaseData(requirement, value);
                    }
                }
            }
        }
    }
}
