using CreatureGen.Creatures;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.Tables.TestData;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class HitDiceTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.HitDice; }
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var types = CreatureConstants.Types.All();

            var names = creatures.Union(types);

            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 1)]
        public void HitDiceQuantity(string creature, int quantity)
        {
            AssertAdjustment(creature, quantity);
        }

        [TestCase(CreatureConstants.Types.Aberration, 8)]
        [TestCase(CreatureConstants.Types.Animal, 8)]
        [TestCase(CreatureConstants.Types.Construct, 10)]
        [TestCase(CreatureConstants.Types.Dragon, 12)]
        [TestCase(CreatureConstants.Types.Elemental, 8)]
        [TestCase(CreatureConstants.Types.Fey, 6)]
        [TestCase(CreatureConstants.Types.Giant, 8)]
        [TestCase(CreatureConstants.Types.Humanoid, 8)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 10)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 8)]
        [TestCase(CreatureConstants.Types.Ooze, 10)]
        [TestCase(CreatureConstants.Types.Outsider, 8)]
        [TestCase(CreatureConstants.Types.Plant, 8)]
        [TestCase(CreatureConstants.Types.Undead, 12)]
        [TestCase(CreatureConstants.Types.Vermin, 8)]
        public void HitDie(string creatureType, int quantity)
        {
            AssertAdjustment(creatureType, quantity);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void AllCreaturesHavePositiveHitDiceQuantity(string creature)
        {
            var hitDiceQuantity = GetAdjustment(creature);
            Assert.That(hitDiceQuantity, Is.Positive);
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void AllCreatureTypesHaveValidHitDie(string creatureType)
        {
            var validDie = new[] { 2, 3, 4, 6, 8, 10, 12, 20, 100 };

            var hitDie = GetAdjustment(creatureType);
            Assert.That(hitDie, Is.Positive);
            Assert.That(validDie, Contains.Item(hitDie));
        }
    }
}
