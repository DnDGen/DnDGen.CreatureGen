using CreatureGen.Domain.Selectors.Collections;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class SelectorsModuleTests : IoCTests
    {
        [Test]
        public void LanguageSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageCollectionsSelector>();
        }

        [Test]
        public void LevelAdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAdjustmentsSelector>();
        }

        [Test]
        public void StatAdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAbilityAdjustmentsSelector>();
        }

        [Test]
        public void SkillSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ISkillSelector>();
        }

        [Test]
        public void FeatsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IFeatsSelector>();
        }

        [Test]
        public void FeatsSelectorsIsDecorated()
        {
            AssertIsInstanceOf<IFeatsSelector, FeatsSelectorEventGenDecorator>();
        }

        [Test]
        public void LeadershipSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILeadershipSelector>();
        }
    }
}