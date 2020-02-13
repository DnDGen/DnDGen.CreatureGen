using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests
    {
        private ICreatureVerifier verifier;
        private Mock<ICollectionSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            verifier = new CreatureVerifier(mockCollectionsSelector.Object);
        }

        [Test]
        public void CompatibleIfNoTemplate()
        {
            var compatible = verifier.VerifyCompatibility("creature", CreatureConstants.Templates.None);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void CompatibleIfNoTemplateContainsCreature()
        {
            var creatures = new[] { "other creature", "creature" };
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "template")).Returns(creatures);

            var compatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void NotCompatibleIfNoTemplateDoesNotContainCreature()
        {
            var creatures = new[] { "wrong creature", "other creature" };
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "template")).Returns(creatures);

            var compatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(compatible, Is.False);
        }
    }
}