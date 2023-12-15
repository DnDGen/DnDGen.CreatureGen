using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Generic;
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

        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        public void AlignmentGroupContainsCorrectCreatures(string alignment)
        {
            var allCreatures = CreatureConstants.GetAll();
            var entries = new List<string>();
            var allAlignmentGroups = collectionSelector.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups);

            foreach (var creature in allCreatures)
            {
                if (allAlignmentGroups[creature + GroupConstants.Exploded].Contains(alignment))
                    entries.Add(creature);
            }

            Assert.That(entries, Is.Not.Empty);
            AssertDistinctCollection(alignment, entries.ToArray());
        }
    }
}
