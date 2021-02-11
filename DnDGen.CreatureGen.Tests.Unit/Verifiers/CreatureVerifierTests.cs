using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Generators;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests
    {
        private ICreatureVerifier verifier;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object, mockCreatureDataSelector.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_CompatibleIfTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(compatible);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void CanBeCharacter_FalseIfNullLevelAdjustment()
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = null;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void CanBeCharacter_FalseIfNullLevelAdjustment(int levelAdjustment)
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = levelAdjustment;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.True);
        }
    }
}