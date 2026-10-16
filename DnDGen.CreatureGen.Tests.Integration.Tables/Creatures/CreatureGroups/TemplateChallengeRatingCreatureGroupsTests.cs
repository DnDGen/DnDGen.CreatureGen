using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Templates;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class TemplateChallengeRatingCreatureGroupsTests : CreatureGroupsTestBase
    {
        [Test]
        public void CreatureAllGroupNames() => AssertAllCreatureGroupNames();

        [Test]
        public void TemplateChallengeRatingCreatureGroupNames()
        {
            AssertSubsetCreatureGroupNames(TemplateChallengeRatingPairs.Select(p => p[0] + bool.FalseString + p[1]));
        }

        [TestCaseSource(typeof(CreatureGroupsTestBase), nameof(TemplatesWithChallengeRatingFilter))]
        public void CreatureGroup_Template_ResultsInChallengeRating(string template, string cr)
        {
            var sourcePrototypes = GetTemplatePrototypes(template, false);
            var applicator = GetNewInstanceOf<TemplateApplicator>(template);
            var filters = new Filters { ChallengeRatings = [cr] };

            var templateCreatures = sourcePrototypes
                .Where(p => applicator.IsCompatible(p, filters))
                .Select(p => p.Name);

            var groupName = template + bool.FalseString + cr;
            AssertDistinctCollection(groupName, [.. templateCreatures]);
        }
    }
}
