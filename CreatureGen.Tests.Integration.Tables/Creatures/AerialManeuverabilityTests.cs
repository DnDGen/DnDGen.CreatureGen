using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AerialManeuverabilityTests : CollectionTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        internal ITypeAndAmountSelector TypesAndAmountsSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.AerialManeuverability;
            }
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            AssertCollectionNames(creatures);
        }

        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Gargoyle, "Average Maneuverability")]
        [TestCase(CreatureConstants.Harpy, "Average Maneuverability")]
        [TestCase(CreatureConstants.Janni, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.OgreMage, "Good Maneuverability")]
        [TestCase(CreatureConstants.Pixie, "Good Maneuverability")]
        public void AerialManeuverability(string creature, string maneuverability)
        {
            DistinctCollection(creature, maneuverability);
        }

        [Test]
        public void AllCreaturesWithAerialSpeedHaveManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var aerialCreatures = aerialSpeeds.Select(kvp => kvp.Key);

            foreach (var creature in aerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Not.Empty, creature);
                Assert.That(maneuverability.Count, Is.EqualTo(1), creature);
            }
        }

        [Test]
        public void AllCreaturesWithoutAerialSpeedHaveNoManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var nonAerialSpeeds = speeds.Except(aerialSpeeds);
            var nonAerialCreatures = nonAerialSpeeds.Select(kvp => kvp.Key);

            foreach (var creature in nonAerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Empty, creature);
            }
        }
    }
}
