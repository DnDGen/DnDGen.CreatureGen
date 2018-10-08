using CreatureGen.Creatures;
using CreatureGen.Defenses;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureSaveGroupsTests : CreatureGroupsTableTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void NamesAreComplete()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(SaveConstants.Fortitude,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(SaveConstants.Reflex,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider)]
        [TestCase(SaveConstants.Will,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Undead)]
        public void CreatureTypeSaveGroup(string save, params string[] group)
        {
            var creatureTypes = CreatureConstants.Types.All();
            var subset = table[save].Intersect(creatureTypes);

            AssertCollection(subset, group);
        }

        [TestCase(SaveConstants.Fortitude,
            CreatureConstants.YuanTi_Abomination,
            CreatureConstants.YuanTi_Pureblood)]
        [TestCase(SaveConstants.Reflex,
            CreatureConstants.YuanTi_Abomination,
            CreatureConstants.YuanTi_Pureblood)]
        [TestCase(SaveConstants.Will,
            CreatureConstants.Ape_Dire,
            CreatureConstants.Badger_Dire,
            CreatureConstants.Bat_Dire,
            CreatureConstants.Bear_Dire,
            CreatureConstants.Boar_Dire,
            CreatureConstants.Lion_Dire,
            CreatureConstants.Rat_Dire,
            CreatureConstants.Shark_Dire,
            CreatureConstants.Tiger_Dire,
            CreatureConstants.Weasel_Dire,
            CreatureConstants.Wolf_Dire,
            CreatureConstants.Wolverine_Dire)]
        public void CreatureSaveGroup(string save, params string[] group)
        {
            Assert.Fail("TODO: Check out all elementals");
            Assert.Fail("TODO: Since elementals have strengths by element type, see if subtype would fit better here");
            Assert.Fail("TODO: Check out all humanoid");

            var creatures = CreatureConstants.All();
            var subset = table[save].Intersect(creatures);

            AssertCollection(subset, group);
        }

        [TestCase(SaveConstants.Fortitude)]
        [TestCase(SaveConstants.Reflex)]
        [TestCase(SaveConstants.Will)]
        public void AllGroupMembersOfSaveGroupAreCreatureOrCreatureType(string save)
        {
            var creatureTypes = CreatureConstants.Types.All();
            var creatures = CreatureConstants.All();
            var expectedGroupMembers = creatures.Union(creatureTypes);

            Assert.That(expectedGroupMembers, Is.SupersetOf(table[save]));

        }

        [TestCase(SaveConstants.Fortitude)]
        [TestCase(SaveConstants.Reflex)]
        [TestCase(SaveConstants.Will)]
        public void NoOverlapBetweenCreatureAndCreatureType(string save)
        {
            var creatureTypes = CreatureConstants.Types.All();
            var saveCreatureTypes = table[save].Intersect(creatureTypes);

            foreach (var saveCreatureType in saveCreatureTypes)
            {
                var creaturesOfType = CollectionSelector.Explode(tableName, saveCreatureType);
                var overlap = table[save].Intersect(creaturesOfType);

                Assert.That(overlap, Is.Empty, saveCreatureType);
            }
        }
    }
}
