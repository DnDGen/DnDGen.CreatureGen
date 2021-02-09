using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.IoC
{
    [TestFixture]
    public abstract class IoCTests : IntegrationTests
    {
        protected void AssertNotSingleton<T>()
        {
            var first = GetNewInstanceOf<T>();
            var second = GetNewInstanceOf<T>();
            Assert.That(first, Is.Not.EqualTo(second));
        }
        protected void AssertSingleton<T>()
        {
            var first = GetNewInstanceOf<T>();
            var second = GetNewInstanceOf<T>();
            Assert.That(first, Is.EqualTo(second));
        }

        protected void AssertNotSingleton<T>(string name)
        {
            var first = GetNewInstanceOf<T>(name);
            var second = GetNewInstanceOf<T>(name);
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNamedIsInstanceOf<I, T>(string name)
            where T : I
        {
            var item = GetNewInstanceOf<I>(name);
            Assert.That(item, Is.InstanceOf<T>());
        }
    }
}