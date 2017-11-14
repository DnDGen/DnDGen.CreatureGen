using CreatureGen.Creatures;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CrossCreatureGroupsTests : CreatureGroupsTableTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientID = Guid.NewGuid();
            ClientIdManager.SetClientID(clientID);
        }

        [Test]
        public void EntriesAreComplete()
        {
            AssertCreatureGroupEntriesAreComplete();
        }

        [Test]
        public void AllCreaturesHaveType()
        {
            var allCreatures = CreatureConstants.All();
            var allTypes = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Construct,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Elemental,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Outsider,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Undead,
                CreatureConstants.Types.Vermin,
            };

            foreach (var creature in allCreatures)
            {
                var type = CollectionSelector.FindCollectionOf(tableName, creature, allTypes);
                Assert.That(type, Is.Not.Null, creature);
                Assert.That(type, Is.Not.Empty, creature);
                Assert.That(new[] { type }, Is.SubsetOf(allTypes), creature);
            }
        }

        [Test]
        public void NoCircularSubgroups()
        {
            var table = CollectionMapper.Map(tableName);

            foreach (var kvp in table)
            {
                AssertGroupDoesNotContain(kvp.Key, kvp.Key);
            }
        }

        private void AssertGroupDoesNotContain(string name, string forbiddenEntry)
        {
            var table = CollectionMapper.Map(tableName);
            var group = table[name];

            if (name != forbiddenEntry)
                Assert.That(group, Does.Not.Contain(forbiddenEntry));

            var subgroupNames = group.Intersect(table.Keys);

            foreach (var subgroupName in subgroupNames)
            {
                //INFO: This is so groups can contain themselves (such as Ape containing Ape and Dire ape)
                if (subgroupName == name)
                    continue;

                AssertGroupDoesNotContain(subgroupName, forbiddenEntry);
                AssertGroupDoesNotContain(subgroupName, subgroupName);
            }
        }
    }
}
