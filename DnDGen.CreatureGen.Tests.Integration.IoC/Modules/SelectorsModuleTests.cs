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
        public void FeatsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IFeatsSelector>();
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

        [Test]
        public void CollectionData_BonusDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<BonusDataSelection>>();
        }

        [Test]
        public void CollectionData_CreatureDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<CreatureDataSelection>>();
        }

        [Test]
        public void CollectionData_FeatDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<FeatDataSelection>>();
        }

        [Test]
        public void CollectionData_SpecialQualityDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<SpecialQualityDataSelection>>();
        }

        [Test]
        public void CollectionData_SkillDataSelectorIsNotConstructedAsSingleton()
        {
            AssertNotSingleton<ICollectionDataSelector<SkillDataSelection>>();
        }
    }
}