using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AdvancementSelectionTests
    {
        private AdvancementSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new AdvancementSelection();
        }

        [Test]
        public void AdvancementSelectionIsInitialized()
        {
            Assert.That(selection.AdditionalHitDice, Is.Zero);
            Assert.That(selection.AdjustedChallengeRating, Is.Empty);
            Assert.That(selection.CasterLevelAdjustment, Is.Zero);
            Assert.That(selection.ConstitutionAdjustment, Is.Zero);
            Assert.That(selection.DexterityAdjustment, Is.Zero);
            Assert.That(selection.NaturalArmorAdjustment, Is.Zero);
            Assert.That(selection.Reach, Is.Zero);
            Assert.That(selection.Size, Is.Empty);
            Assert.That(selection.Space, Is.Zero);
            Assert.That(selection.StrengthAdjustment, Is.Zero);
        }
    }
}
