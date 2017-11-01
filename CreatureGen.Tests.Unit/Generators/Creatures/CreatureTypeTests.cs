using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class CreatureTypeTests
    {
        private CreatureType creatureType;
        private const int RecursiveCount = 42;

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
            Assert.Fail();
        }

        [Test]
        public void CreatureTypeIsViaSubType()
        {
            Assert.Fail();
        }

        [Test]
        public void CreatureTypeIsNot()
        {
            Assert.Fail();
        }
    }
}
