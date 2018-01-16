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

        [TestCase(CreatureConstants.Aasimar)]
        [TestCase(CreatureConstants.Aboleth)]
        [TestCase(CreatureConstants.Aboleth_Mage)]
        [TestCase(CreatureConstants.Achaierai)]
        [TestCase(CreatureConstants.Allip)]
        [TestCase(CreatureConstants.Androsphinx)]
        [TestCase(CreatureConstants.Angel_AstralDeva, "Good Maneuverability")]
        [TestCase(CreatureConstants.Angel_Planetar, "Good Maneuverability")]
        [TestCase(CreatureConstants.Angel_Solar, "Good Maneuverability")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Large)]
        [TestCase(CreatureConstants.AnimatedObject_Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Small)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny)]
        [TestCase(CreatureConstants.Ankheg)]
        [TestCase(CreatureConstants.Annis)]
        [TestCase(CreatureConstants.Ant_Giant_Queen)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier)]
        [TestCase(CreatureConstants.Ant_Giant_Worker)]
        [TestCase(CreatureConstants.Ape)]
        [TestCase(CreatureConstants.Ape_Dire)]
        [TestCase(CreatureConstants.Aranea)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Arrowhawk_Elder, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.AssassinVine)]
        [TestCase(CreatureConstants.Avoral, "Good Maneuverability")]
        [TestCase(CreatureConstants.Babau)]
        [TestCase(CreatureConstants.Baboon)]
        [TestCase(CreatureConstants.Balor, "Good Maneuverability")]
        [TestCase(CreatureConstants.Basilisk)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater)]
        [TestCase(CreatureConstants.Bebilith)]
        [TestCase(CreatureConstants.Dretch)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Greater, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Huge, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Large, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Medium, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Small, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Gargoyle, "Average Maneuverability")]
        [TestCase(CreatureConstants.Glabrezu)]
        [TestCase(CreatureConstants.Harpy, "Average Maneuverability")]
        [TestCase(CreatureConstants.Hezrou)]
        [TestCase(CreatureConstants.Janni, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Marilith)]
        [TestCase(CreatureConstants.Mephit_Air, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Dust, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Earth, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Fire, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Ice, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Magma, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Ooze, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Salt, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Steam, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Water, "Average Maneuverability")]
        [TestCase(CreatureConstants.Nalfeshnee, "Poor Maneuverability")]
        [TestCase(CreatureConstants.OgreMage, "Good Maneuverability")]
        [TestCase(CreatureConstants.Pixie, "Good Maneuverability")]
        [TestCase(CreatureConstants.Quasit, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Retriever)]
        [TestCase(CreatureConstants.Salamander_Average)]
        [TestCase(CreatureConstants.Salamander_Flamebrother)]
        [TestCase(CreatureConstants.Salamander_Noble)]
        [TestCase(CreatureConstants.Succubus, "Average Maneuverability")]
        [TestCase(CreatureConstants.Tojanida_Adult)]
        [TestCase(CreatureConstants.Tojanida_Elder)]
        [TestCase(CreatureConstants.Tojanida_Juvenile)]
        [TestCase(CreatureConstants.Vrock, "Average Maneuverability")]
        [TestCase(CreatureConstants.Xorn_Average)]
        [TestCase(CreatureConstants.Xorn_Elder)]
        [TestCase(CreatureConstants.Xorn_Minor)]
        public void AerialManeuverability(string creature, params string[] maneuverability)
        {
            Assert.That(maneuverability.Length, Is.Zero.Or.EqualTo(1), creature);
            DistinctCollection(creature, maneuverability);
        }

        [Test]
        public void AllCreaturesWithAerialSpeedHaveManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var aerialCreatures = aerialSpeeds.Select(kvp => kvp.Key);

            AssertCollection(aerialCreatures.Intersect(table.Keys), aerialCreatures);

            foreach (var creature in aerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Not.Empty, creature);
                Assert.That(maneuverability.Count, Is.EqualTo(1), creature);
                Assert.That(maneuverability.Single(), Is.Not.Empty, creature);
                Assert.That(maneuverability.Single(), Does.EndWith(" Maneuverability"), creature);
            }
        }

        [Test]
        public void AllCreaturesWithoutAerialSpeedHaveNoManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var nonAerialSpeeds = speeds.Except(aerialSpeeds);
            var nonAerialCreatures = nonAerialSpeeds.Select(kvp => kvp.Key);

            AssertCollection(nonAerialCreatures.Intersect(table.Keys), nonAerialCreatures);

            foreach (var creature in nonAerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Empty, creature);
            }
        }
    }
}
