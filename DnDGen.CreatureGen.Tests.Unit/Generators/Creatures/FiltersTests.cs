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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void GetDescription_ReturnsDescription_With2ChallengeRatings()
        {
            filters.ChallengeRatings = ["my challenge rating", "my other CR"];

            var description = filters.GetDescription();

            var expected = new StringBuilder();
            expected.AppendLine("Types: []");
            expected.AppendLine("CRs: [my challenge rating, my other CR]");
            expected.AppendLine("Alignments: []");

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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

            Assert.That(description, Is.EqualTo(expected.ToString()));
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
            expected.AppendLine("Alignments: [my alignment, other alignment, alignment 3]");

            Assert.That(description, Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void ToString_IsDescription()
        {
            filters.Types = ["my type", "my other type"];
            filters.ChallengeRatings = ["my challenge rating", "my CR", "another CR", "additional CR"];
            filters.Alignments = ["my alignment", "other alignment", "alignment 3"];

            var expected = new StringBuilder();
            expected.AppendLine("Types: [my type, my other type]");
            expected.AppendLine("CRs: [my challenge rating, my CR, another CR, additional CR]");
            expected.AppendLine("Alignments: [my alignment, other alignment, alignment 3]");

            Assert.That(filters.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenNoFilters_Null()
        {
            filters.Alignments = null;
            filters.ChallengeRatings = null;
            filters.Types = null;

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenNoFilters_Empty()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = [];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAlignmentMatches()
        {
            filters.Alignments = ["my alignment"];
            filters.ChallengeRatings = [];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyAlignmentMatches()
        {
            filters.Alignments = ["wrong alignment", "nope alignment", "other alignment"];
            filters.ChallengeRatings = [];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoAlignmentMatches()
        {
            filters.Alignments = ["wrong alignment", "nope alignment"];
            filters.ChallengeRatings = [];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo("Alignment filter [wrong alignment, nope alignment] is not compatible with [my alignment, other alignment]"));
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenChallengeRatingMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = ["my cr"];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyChallengeRatingMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = ["wrong cr", "nope cr", "other cr"];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoChallengeRatingMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = ["wrong cr", "nope cr"];
            filters.Types = [];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo($"CR filter [wrong cr, nope cr] is not compatible with [my cr, other cr]"));
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenTypeMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = [];
            filters.Types = ["my type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAnyTypeMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = [];
            filters.Types = ["wrong type", "nope type", "other type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenNoTypeMatches()
        {
            filters.Alignments = [];
            filters.ChallengeRatings = [];
            filters.Types = ["wrong type", "nope type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo($"Type filter [wrong type, nope type] is not compatible with [my type, other type]"));
        }

        [Test]
        public void AreCompatible_ReturnsTrue_WhenAllFiltersMatches()
        {
            filters.Alignments = ["some alignment", "other alignment"];
            filters.ChallengeRatings = ["my cr", "wrong cr"];
            filters.Types = ["this type", "that type", "my type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.True);
            Assert.That(Reason, Is.Null);
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustAlignmentHasNoMatches()
        {
            filters.Alignments = ["some alignment", "wrong alignment"];
            filters.ChallengeRatings = ["my cr", "wrong cr"];
            filters.Types = ["this type", "that type", "my type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo("Alignment filter [some alignment, wrong alignment] is not compatible with [my alignment, other alignment]"));
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustChallengeRatingHasNoMatches()
        {
            filters.Alignments = ["some alignment", "other alignment"];
            filters.ChallengeRatings = ["nope cr", "wrong cr"];
            filters.Types = ["this type", "that type", "my type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo("CR filter [nope cr, wrong cr] is not compatible with [my cr, other cr]"));
        }

        [Test]
        public void AreCompatible_ReturnsFalse_WhenJustTypeHasNoMatches()
        {
            filters.Alignments = ["some alignment", "other alignment"];
            filters.ChallengeRatings = ["my cr", "wrong cr"];
            filters.Types = ["this type", "that type", "wrong type"];

            var (Compatible, Reason) = filters.AreCompatible(["my alignment", "other alignment"], ["my cr", "other cr"], ["my type", "other type"]);
            Assert.That(Compatible, Is.False);
            Assert.That(Reason, Is.EqualTo("Type filter [this type, that type, wrong type] is not compatible with [my type, other type]"));
        }
    }
}
