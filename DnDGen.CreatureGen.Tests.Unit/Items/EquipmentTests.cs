using DnDGen.CreatureGen.Items;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Items
{
    [TestFixture]
    public class EquipmentTests
    {
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            equipment = new Equipment();
        }

        [Test]
        public void EquipmentInitialized()
        {
            Assert.That(equipment.Weapons, Is.Empty);
            Assert.That(equipment.Armor, Is.Null);
            Assert.That(equipment.Shield, Is.Null);
            Assert.That(equipment.Items, Is.Empty);
        }
    }
}
