using DnDGen.CreatureGen.Selectors.Collections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class SelectorsModuleTests : IoCTests
    {
        [Test]
        public void AdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAdjustmentsSelector>();
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
        public void TypeAndAmountSelectorIsInjected()
        {
            AssertNotSingleton<ITypeAndAmountSelector>();
        }

        [Test]
        public void CreatureDataSelectorIsInjected()
        {
            AssertNotSingleton<ICreatureDataSelector>();
        }

        [Test]
        public void AttackSelectorIsInjected()
        {
            AssertNotSingleton<IAttackSelector>();
        }

        [Test]
        public void AdvancementSelectorIsInjected()
        {
            AssertNotSingleton<IAdvancementSelector>();
        }

        [Test]
        public void BonusSelectorIsInjected()
        {
            AssertNotSingleton<IBonusSelector>();
        }
    }
}