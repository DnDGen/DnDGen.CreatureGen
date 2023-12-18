using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Unit
{
    [TestFixture]
    public class TestClassesTests
    {
        private const int TestLimit = 2048;

        [Test]
        public void TestClassesDoNotHaveTooManyTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var classes = assembly.GetTypes();
            var classesOverLimit = new List<string>();

            foreach (var testClass in classes)
            {
                var methods = testClass.GetMethods();
                var activeTests = methods.Where(m => IsActiveTest(m, testClass));

                if (!activeTests.Any())
                    continue;

                var testsCount = activeTests.Sum(m => m.GetCustomAttributes<TestAttribute>(true).Count());
                var testCasesCount = activeTests.Sum(m => m.GetCustomAttributes<TestCaseAttribute>().Count(TestCaseIsActive));
                var testCaseSourcesCount = activeTests.Sum(m => m.GetCustomAttributes<TestCaseSourceAttribute>().Sum(tcs => GetTestCaseSourceCount(tcs, testClass)));
                var testsTotal = testsCount + testCasesCount + testCaseSourcesCount;

                if (testsTotal > TestLimit)
                    classesOverLimit.Add($"{testClass.FullName}: {testsTotal}");
            }

            Assert.That(classesOverLimit, Is.Empty);
        }

        private static bool IsActiveTest(MethodInfo method, Type testClass)
        {
            if (method.GetCustomAttributes<IgnoreAttribute>(true).Any())
                return false;

            return method.GetCustomAttributes<TestAttribute>(true).Any()
                || method.GetCustomAttributes<TestCaseAttribute>(true).Any(tc => TestCaseIsActive(tc))
                || method.GetCustomAttributes<TestCaseSourceAttribute>(true).Any(tcs => TestCaseSourceIsActive(tcs, testClass));
        }

        private static bool TestCaseIsActive(TestCaseAttribute testCase)
        {
            return string.IsNullOrEmpty(testCase.Ignore) && string.IsNullOrEmpty(testCase.IgnoreReason);
        }

        private static bool TestCaseSourceIsActive(TestCaseSourceAttribute testCaseSource, Type testClass)
        {
            var testCases = GetFromSource(testCaseSource, testClass);

            foreach (var testCase in testCases)
                return true;

            return false;
        }

        private static int GetTestCaseSourceCount(TestCaseSourceAttribute testCaseSource, Type testClass)
        {
            var testCases = GetFromSource(testCaseSource, testClass);
            var count = 0;

            foreach (var testCase in testCases)
                count++;

            return count;
        }

        private static IEnumerable GetFromSource(TestCaseSourceAttribute testCaseSource, Type testClass)
        {
            var sourceClass = testClass;
            if (testCaseSource.SourceType != null)
            {
                sourceClass = testCaseSource.SourceType;
            }

            var method = sourceClass.GetMethod(testCaseSource.SourceName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (method == null)
            {
                var property = sourceClass.GetProperty(testCaseSource.SourceName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                method = property.GetGetMethod();
            }

            var instance = Activator.CreateInstance(testClass);
            var testCases = (IEnumerable)method.Invoke(instance, new object[0]);

            return testCases;
        }
    }
}
