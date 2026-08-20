using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using NUnit.Framework;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class FiltersTests
    {
        private Filters filters;

        [SetUp]
        public void Setup()
        {
            filters = new Filters();
        }

        [Test]
        public void FiltersInitialized()
        {
            Assert.That(filters.Templates, Is.Not.Null.And.Empty);
            Assert.That(filters.Alignments, Is.Not.Null.And.Empty);
            Assert.That(filters.ChallengeRatings, Is.Not.Null.And.Empty);
            Assert.That(filters.Types, Is.Not.Null.And.Empty);
        }

        [Test]
        public void CleanTemplates_ReturnsNonEmptyTemplates()
        {
            filters.Templates.Add("template 1");
            filters.Templates.Add(string.Empty);
            filters.Templates.Add("template 2");
            filters.Templates.Add(null);
            filters.Templates.Add(CreatureConstants.Templates.None);
            filters.Templates.Add("template 3");

            Assert.That(filters.CleanTemplates, Is.EqualTo(["template 1", "template 2", CreatureConstants.Templates.None, "template 3"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription(bool asCharacter)
        {
            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithNullTemplates(bool asCharacter)
        {
            filters.Templates = null;

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: <Null>");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With0Templates(bool asCharacter)
        {
            filters.Templates = [];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With1Templates(bool asCharacter)
        {
            filters.Templates = ["my template"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: [my template]");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With2Templates(bool asCharacter)
        {
            filters.Templates = ["my template", "my other template"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: [my template, my other template]");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithCleanTemplates(bool asCharacter)
        {
            filters.Templates = ["my template", null, CreatureConstants.Templates.None, string.Empty, "my other template"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine($"Templates: [my template, {CreatureConstants.Templates.None}, my other template]");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithNullTypes(bool asCharacter)
        {
            filters.Types = null;

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: <Null>");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With0Types(bool asCharacter)
        {
            filters.Types = [];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With1Types(bool asCharacter)
        {
            filters.Types = ["my type"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: [my type]");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With2Types(bool asCharacter)
        {
            filters.Types = ["my type", "my other type"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: [my type, my other type]");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithNullChallengeRatings(bool asCharacter)
        {
            filters.ChallengeRatings = null;

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: <Null>");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With0ChallengeRatings(bool asCharacter)
        {
            filters.ChallengeRatings = [];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With1ChallengeRatings(bool asCharacter)
        {
            filters.ChallengeRatings = ["my challenge rating"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: [my challenge rating]");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With2ChallengeRatings(bool asCharacter)
        {
            filters.ChallengeRatings = ["my challenge rating", "my other CR"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CR: [my challenge rating, my other CR]");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithNullAlignments(bool asCharacter)
        {
            filters.Alignments = null;

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: <Null>");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With0Alignments(bool asCharacter)
        {
            filters.Alignments = [];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With1Alignments(bool asCharacter)
        {
            filters.Alignments = ["my alignment"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: [my alignment]");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_With2Alignments(bool asCharacter)
        {
            filters.Alignments = ["my alignment", "my other alignment"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: []");
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: [my alignment, my other alignment]");

            Assert.That(description, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetDescription_ReturnsDescription_WithAllProperties(bool asCharacter)
        {
            filters.Templates = ["my template"];
            filters.Types = ["my type", "my other type"];
            filters.ChallengeRatings = ["my challenge rating", "my CR", "another CR", "additional CR"];
            filters.Alignments = ["my alignment", "other alignment", "alignment 3"];

            var description = filters.GetDescription(asCharacter);

            var expected = new StringBuilder();
            expected.AppendLine($"As Character: {asCharacter}");
            expected.AppendLine("Templates: [my template]");
            expected.AppendLine("Types: [my type, my other type]");
            expected.AppendLine("CRs: [my challenge rating, my CR, another CR, additional CR]");
            expected.AppendLine("Alignments: [my alignment, other alignment, alignment 23]");

            Assert.That(description, Is.EqualTo(expected));
        }
    }
}
