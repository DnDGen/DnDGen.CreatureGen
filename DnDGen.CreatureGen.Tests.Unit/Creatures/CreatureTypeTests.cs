using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
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
            Assert.That(creatureType.SubTypes, Is.Empty);
        }

        [Test]
        public void CreateSubtype()
        {
            creatureType.SubTypes = new[] { "subtype" };
            Assert.That(creatureType.SubTypes, Contains.Item("subtype"));
            Assert.That(creatureType.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void CreateMultipleSubtypes()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };
            Assert.That(creatureType.SubTypes, Contains.Item("subtype"));
            Assert.That(creatureType.SubTypes, Contains.Item("other subtype"));
            Assert.That(creatureType.SubTypes.Count, Is.EqualTo(2));
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
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            var isType = creatureType.Is("subtype");
            Assert.That(isType, Is.True);
        }

        [Test]
        public void CreatureTypeIsNot()
        {
            creatureType.Name = "creature type";
            creatureType.SubTypes = new[] { "subtype", "other subtype" };

            var isType = creatureType.Is("wrong subtype");
            Assert.That(isType, Is.False);
        }

        [Test]
        public void CreatureTypeIsNotWithNoSubtypes()
        {
            creatureType.Name = "creature type";

            var isType = creatureType.Is("subtype");
            Assert.That(isType, Is.False);
        }
    }
}
