using Ninject;
using NUnit.Framework;
using System.Diagnostics;

namespace DnDGen.CreatureGen.Tests.Integration.IoC
{
    [TestFixture]
    public abstract class IoCTests : IntegrationTests
    {
        [Inject]
        public Stopwatch Stopwatch { get; set; }

        private const int TimeLimitInMilliseconds = 300;

        [TearDown]
        public void IoCTeardown()
        {
            Stopwatch.Reset();
        }

        private T InjectAndAssertDuration<T>()
        {
            Stopwatch.Restart();

            var instance = GetNewInstanceOf<T>();
            Assert.That(Stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(TimeLimitInMilliseconds));

            return instance;
        }

        private T InjectAndAssertDuration<T>(string name)
        {
            Stopwatch.Restart();

            var instance = GetNewInstanceOf<T>(name);
            Assert.That(Stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(TimeLimitInMilliseconds));

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