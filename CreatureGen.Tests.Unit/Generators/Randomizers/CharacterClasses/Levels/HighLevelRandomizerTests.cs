using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.Levels;
using Moq;
using NUnit.Framework;
using RollGen;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests
    {
        [Test]
        public void Add10ToRoll()
        {
            var mockDice = new Mock<Dice>();
            mockDice.Setup(d => d.Roll(1).d(5).AsSum()).Returns(9266);
            var randomizer = new HighLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9276));
        }
    }
}