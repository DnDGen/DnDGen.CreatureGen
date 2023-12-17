using NUnit.Framework;
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
        public void NoTestClassHasMoreThan2000Tests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var classes = assembly.GetTypes();
            var classesOverLimit = new List<string>();

            foreach (var testClass in classes)
            {
                var methods = testClass.GetMethods();
                var activeTests = methods.Where(m => IsActiveTest(m));

                if (!activeTests.Any())
                    continue;

                var testsCount = activeTests.Sum(m => m.GetCustomAttributes<TestAttribute>(true).Count());
                var testCasesCount = activeTests.Sum(m => m.GetCustomAttributes<TestCaseAttribute>().Count(tc => TestCaseIsActive(tc)));
                var testsTotal = testsCount + testCasesCount;

                if (testsTotal > TestLimit)
                    classesOverLimit.Add($"{testClass.FullName}: {testsTotal}");
            }

            Assert.That(classesOverLimit, Is.Empty);
        }

        private static bool IsActiveTest(MethodInfo method)
        {
            if (method.GetCustomAttributes<IgnoreAttribute>(true).Any())
                return false;

            if (method.GetCustomAttributes<TestAttribute>(true).Any())
                return true;

            return method.GetCustomAttributes<TestCaseAttribute>(true).Any(tc => TestCaseIsActive(tc));
        }

        private static bool TestCaseIsActive(TestCaseAttribute testCase)
        {
            return string.IsNullOrEmpty(testCase.Ignore) && string.IsNullOrEmpty(testCase.IgnoreReason);
        }
    }
}
