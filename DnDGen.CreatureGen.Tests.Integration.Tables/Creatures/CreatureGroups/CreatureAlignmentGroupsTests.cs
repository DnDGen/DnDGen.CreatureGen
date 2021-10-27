using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureAlignmentGroupsTests : CreatureGroupsTableTests
    {
        private ICollectionSelector collectionSelector;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AlignmentGroupsContainCreature(string creature)
        {
            var creatureAlignments = collectionSelector.Explode(TableNameConstants.Collection.AlignmentGroups, creature);
            var allAlignments = collectionSelector.Explode(TableNameConstants.Collection.AlignmentGroups, GroupConstants.All);
            var invalidAlignments = allAlignments.Except(creatureAlignments);

            foreach (var alignment in creatureAlignments)
            {
                Assert.That(table, Contains.Key(alignment));
                Assert.That(table[alignment], Contains.Item(creature), alignment);
            }

            foreach (var alignment in invalidAlignments)
            {
                Assert.That(table, Contains.Key(alignment));
                Assert.That(table[alignment], Does.Not.Contain(creature), alignment);
            }
        }
    }
}
