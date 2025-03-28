using DnDGen.CreatureGen.Selectors;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.Infrastructure.Selectors.Collections;
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

        [Test]
        public void ItemSelectorIsInjected()
        {
            AssertNotSingleton<IItemSelector>();
        }

        [Test]
        public void CollectionData_AdvancementDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<AdvancementDataSelection>>();
        }

        [Test]
        public void CollectionData_AttackDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<AttackDataSelection>>();
        }

        [Test]
        public void CollectionData_DamageDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<DamageDataSelection>>();
        }
    }
}