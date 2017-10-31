using CreatureGen.Creatures;
using CreatureGen.Generators.Verifiers;
using CreatureGen.Tables;
using CreatureGen.Verifiers;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Verifiers
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
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Set.Collection.CreatureGroups, "template")).Returns(creatures);

            var compatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void NotCompatibleIfNoTemplateDoesNotContainCreature()
        {
            var creatures = new[] { "wrong creature", "other creature" };
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Set.Collection.CreatureGroups, "template")).Returns(creatures);

            var compatible = verifier.VerifyCompatibility("creature", "template");
            Assert.That(compatible, Is.False);
        }
    }
}