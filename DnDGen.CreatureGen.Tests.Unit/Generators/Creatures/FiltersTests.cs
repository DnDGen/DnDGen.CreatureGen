using DnDGen.CreatureGen.Generators.Creatures;
using NUnit.Framework;

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
            Assert.That(filters.Alignment, Is.Null);
            Assert.That(filters.ChallengeRating, Is.Null);
            Assert.That(filters.Type, Is.Null);
        }
    }
}
