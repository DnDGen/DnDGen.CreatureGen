using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class CreatureTypeTests
    {
        private CreatureType creatureType;

        [SetUp]
        public void Setup()
        {
            creatureType = new CreatureType();
        }

        [Test]
        public void CreatureTypeInitialized()
        {
            Assert.That(creatureType.Name, Is.Empty);
            Assert.That(creatureType.SubType, Is.Null);
        }

        [Test]
        public void CreateSubtype()
        {
            creatureType.SubType = new CreatureType();
            Assert.That(creatureType.SubType.Name, Is.Empty);
            Assert.That(creatureType.SubType.SubType, Is.Null);
        }

        [Test]
        public void CreatureTypeIsViaType()
        {
            creatureType.Name = "creature type";

            var isType = creatureType.Is("creature type");
            Assert.That(isType, Is.True);
        }

        [Test]
        public void CreatureTypeIsViaSubType()
        {
            creatureType.Name = "creature type";
            creatureType.SubType = new CreatureType();
            creatureType.SubType.Name = "subtype";

            var isType = creatureType.Is("subtype");
            Assert.That(isType, Is.True);
        }

        [Test]
        public void CreatureTypeIsNot()
        {
            creatureType.Name = "creature type";
            creatureType.SubType = new CreatureType();
            creatureType.SubType.Name = "subtype";

            var isType = creatureType.Is("wrong subtype");
            Assert.That(isType, Is.False);
        }
    }
}
