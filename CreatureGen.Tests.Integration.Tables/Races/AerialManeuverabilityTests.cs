using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class AerialManeuverabilityTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.AerialManeuverability; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.BaseRaces.Gargoyle, "Average Maneuverability")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "Average Maneuverability")]
        [TestCase(SizeConstants.BaseRaces.Janni, "Perfect Maneuverability")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "Good Maneuverability")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "Good Maneuverability")]
        [TestCase(SizeConstants.Metaraces.Ghost, "Perfect Maneuverability")]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, "Good Maneuverability")]
        [TestCase(SizeConstants.Metaraces.HalfDragon, "Average Maneuverability")]
        [TestCase(SizeConstants.Metaraces.HalfFiend, "Average Maneuverability")]
        public void AerialManeuverability(string name, string maneuverability)
        {
            DistinctCollection(name, maneuverability);
        }

        [Test]
        public void AerialSpeedOf0HasEmptyManeuverability()
        {
            var aerialSpeeds = CollectionsMapper.Map(TableNameConstants.Set.Adjustments.AerialSpeeds);
            var aerialManeuverabilities = CollectionsMapper.Map(TableNameConstants.Set.Collection.AerialManeuverability);

            var racesWithNoAerialSpeed = aerialSpeeds.Where(s => s.Value.Single() == "0").Select(s => s.Key);

            Assert.That(racesWithNoAerialSpeed, Is.SubsetOf(aerialManeuverabilities.Keys));

            foreach (var race in racesWithNoAerialSpeed)
            {
                Assert.That(aerialManeuverabilities.Keys, Contains.Item(race));
                Assert.That(aerialManeuverabilities[race].Single(), Is.Empty, race);
            }
        }

        [Test]
        public void PositiveAerialSpeedHasManeuverability()
        {
            var aerialSpeeds = CollectionsMapper.Map(TableNameConstants.Set.Adjustments.AerialSpeeds);
            var aerialManeuverabilities = CollectionsMapper.Map(TableNameConstants.Set.Collection.AerialManeuverability);

            var racesWithAerialSpeed = aerialSpeeds.Where(s => s.Value.Single() != "0").Select(s => s.Key);

            Assert.That(racesWithAerialSpeed, Is.SubsetOf(aerialManeuverabilities.Keys));

            foreach (var race in racesWithAerialSpeed)
            {
                Assert.That(aerialManeuverabilities.Keys, Contains.Item(race));
                Assert.That(aerialManeuverabilities[race].Single(), Is.Not.Empty, race);
            }
        }
    }
}