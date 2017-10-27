using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class AnimalTests
    {
        private Animal animal;

        [SetUp]
        public void Setup()
        {
            animal = new Animal();
        }

        [Test]
        public void AnimalInitialized()
        {
            Assert.That(animal.Race, Is.Not.Null);
            Assert.That(animal.Abilities, Is.Empty);
            Assert.That(animal.Feats, Is.Empty);
            Assert.That(animal.Languages, Is.Empty);
            Assert.That(animal.Skills, Is.Empty);
            Assert.That(animal.Combat, Is.Not.Null);
            Assert.That(animal.Tricks, Is.EqualTo(0));
        }
    }
}