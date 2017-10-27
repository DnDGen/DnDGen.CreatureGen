using CreatureGen.Randomizers.Alignments;
using Ninject;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public ISetAlignmentRandomizer SetAlignmentRandomizer { get; set; }

        [Test]
        public void StressSetAlignment()
        {
            stressor.Stress(AssertAlignment);
        }

        protected void AssertAlignment()
        {
            SetAlignmentRandomizer.SetAlignment.Goodness = Guid.NewGuid().ToString();
            SetAlignmentRandomizer.SetAlignment.Lawfulness = Guid.NewGuid().ToString();

            var alignment = SetAlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(SetAlignmentRandomizer.SetAlignment));
            Assert.That(alignment.Full, Is.EqualTo(SetAlignmentRandomizer.SetAlignment.Full));
        }
    }
}