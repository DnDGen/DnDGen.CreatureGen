using DnDGen.CreatureGen.Creatures;
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

        [Test]
        public void CleanTemplates_ReturnsNonEmptyTemplates()
        {
            filters.Templates.Add("template 1");
            filters.Templates.Add(string.Empty);
            filters.Templates.Add("template 2");
            filters.Templates.Add(null);
            filters.Templates.Add(CreatureConstants.Templates.None);
            filters.Templates.Add("template 3");

            Assert.That(filters.CleanTemplates, Is.EqualTo(new[] { "template 1", "template 2", CreatureConstants.Templates.None, "template 3" }));
        }
    }
}
