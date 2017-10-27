using CreatureGen.Alignments;
using CreatureGen.Randomizers.Alignments;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NeutralAlignmentRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void StressNeutralAlignment()
        {
            stressor.Stress(AssertAlignment);
        }

        protected void AssertAlignment()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));
            Assert.That(AlignmentConstants.Neutral, Is.EqualTo(alignment.Goodness)
                .Or.EqualTo(alignment.Lawfulness));
        }

        [Test]
        public void NeutralGoodnessHappens()
        {
            var alignment = stressor.GenerateOrFail(AlignmentRandomizer.Randomize,
                a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness != AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.Not.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void NeutralLawfulnessHappens()
        {
            var alignment = stressor.GenerateOrFail(AlignmentRandomizer.Randomize,
                a => a.Lawfulness == AlignmentConstants.Neutral && a.Goodness != AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.Not.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void TrueNeutralHappens()
        {
            var alignment = stressor.GenerateOrFail(AlignmentRandomizer.Randomize,
                a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }
    }
}