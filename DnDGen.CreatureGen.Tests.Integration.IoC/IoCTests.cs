using NUnit.Framework;
using System.Diagnostics;

namespace DnDGen.CreatureGen.Tests.Integration.IoC
{
    [TestFixture]
    public abstract class IoCTests : IntegrationTests
    {
        private Stopwatch stopwatch;

        private const int TimeLimitInMilliseconds = 300;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
        }

        private T InjectAndAssertDuration<T>()
        {
            stopwatch.Restart();

            var instance = GetNewInstanceOf<T>();
            Assert.That(stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(TimeLimitInMilliseconds));

            return instance;
        }

        private T InjectAndAssertDuration<T>(string name)
        {
            stopwatch.Restart();

            var instance = GetNewInstanceOf<T>(name);
            Assert.That(stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(TimeLimitInMilliseconds));

            return instance;
        }

        protected void AssertNotSingleton<T>()
        {
            var first = InjectAndAssertDuration<T>();
            var second = InjectAndAssertDuration<T>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNotSingleton<T>(string name)
        {
            var first = InjectAndAssertDuration<T>(name);
            var second = InjectAndAssertDuration<T>(name);
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNamedIsInstanceOf<I, T>(string name)
            where T : I
        {
            var item = InjectAndAssertDuration<I>(name);
            Assert.That(item, Is.InstanceOf<T>());
        }
    }
}