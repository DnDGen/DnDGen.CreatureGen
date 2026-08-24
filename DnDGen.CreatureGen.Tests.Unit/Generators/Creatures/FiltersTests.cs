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
            Assert.That(filters.Alignments, Is.Not.Null.And.Empty);
            Assert.That(filters.ChallengeRatings, Is.Not.Null.And.Empty);
            Assert.That(filters.Types, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetDescription_ReturnsDescription()
        {
            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_WithNullTypes()
        {
            filters.Types = null;

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: <Null>");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With0Types()
        {
            filters.Types = [];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With1Types()
        {
            filters.Types = ["my type"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: [my type]");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With2Types()
        {
            filters.Types = ["my type", "my other type"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: [my type, my other type]");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_WithNullChallengeRatings()
        {
            filters.ChallengeRatings = null;

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: <Null>");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With0ChallengeRatings()
        {
            filters.ChallengeRatings = [];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With1ChallengeRatings()
        {
            filters.ChallengeRatings = ["my challenge rating"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: [my challenge rating]");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With2ChallengeRatings()
        {
            filters.ChallengeRatings = ["my challenge rating", "my other CR"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CR: [my challenge rating, my other CR]");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_WithNullAlignments()
        {
            filters.Alignments = null;

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: <Null>");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With0Alignments()
        {
            filters.Alignments = [];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With1Alignments()
        {
            filters.Alignments = ["my alignment"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: [my alignment]");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With2Alignments()
        {
            filters.Alignments = ["my alignment", "my other alignment"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: []");
            expected.AppendLine("Alignments: [my alignment, my other alignment]");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void GetDescription_ReturnsDescription_WithAllProperties()
        {
            filters.Types = ["my type", "my other type"];
            filters.ChallengeRatings = ["my challenge rating", "my CR", "another CR", "additional CR"];
            filters.Alignments = ["my alignment", "other alignment", "alignment 3"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: [my type, my other type]");
            expected.AppendLine("CRs: [my challenge rating, my CR, another CR, additional CR]");
            expected.AppendLine("Alignments: [my alignment, other alignment, alignment 23]");

            Assert.That(description, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_IsDescription()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenNoFilters()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAlignmentMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyAlignmentMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoAlignmentMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenChallengeRatingMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyChallengeRatingMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoChallengeRatingMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenTypeMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyTypeMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoTypeMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAllFiltersMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustAlignmentHasNoMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustChallengeRatingHasNoMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustTypeHasNoMatches()
        {
            Assert.Fail("not yet written");
        }
    }
}
