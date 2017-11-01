using CreatureGen.Generators.Alignments;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorTests
    {
        private IAlignmentGenerator alignmentGenerator;

        [SetUp]
        public void Setup()
        {
            alignmentGenerator = new AlignmentGenerator();
        }

        [Test]
        public void GenerateAlignment()
        {
            var alignment = alignmentGenerator.Generate("creature name");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }
    }
}