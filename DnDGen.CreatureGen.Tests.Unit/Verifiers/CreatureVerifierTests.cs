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

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CompatibleIfNoTemplateContainsCreature(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(compatible);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }
    }
}