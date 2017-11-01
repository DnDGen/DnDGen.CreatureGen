using CreatureGen.Feats;
using CreatureGen.Generators.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorEventDecoratorTests
    {
        private IFeatFocusGenerator decorator;
        private Mock<IFeatFocusGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private List<Skill> skills;
        private List<RequiredFeatSelection> requiredFeats;
        private List<Feat> otherFeats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatFocusGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatFocusGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            skills = new List<Skill>();
            requiredFeats = new List<RequiredFeatSelection>();
            otherFeats = new List<Feat>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;
        }

        [Test]
        public void ReturnInnerFocusAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns(string.Empty);

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocus()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGeneration()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGeneration()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass)).Returns(string.Empty);

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocusAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills)).Returns(string.Empty);

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocusEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills)).Returns(string.Empty);

            var focus = decorator.GenerateFrom("feat", "focus type", skills);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }
    }
}
