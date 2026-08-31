using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public class Filters
    {
        /// <summary>
        /// A creature that matches any of these types, after templates are applied, will satisfy the filters.
        /// Null and empty values are ignored.
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// A creature that matches any of these challenge ratings, after templates are applied, will satisfy the filters.
        /// Null and empty values are ignored.
        /// </summary>
        public List<string> ChallengeRatings { get; set; }

        /// <summary>
        /// A creature that matches any of these alignments, after templates are applied, will satisfy the filters.
        /// Null and empty values are ignored.
        /// </summary>
        public List<string> Alignments { get; set; }

        public Filters()
        {
            Types = [];
            ChallengeRatings = [];
            Alignments = [];
        }

        public string GetDescription()
        {
            var description = new StringBuilder();
            description.AppendLine($"Alignments: {GetMessage(Alignments)}");
            description.AppendLine($"CRs: {GetMessage(ChallengeRatings)}");
            description.AppendLine($"Types: {GetMessage(Types)}");

            return description.ToString();
        }

        public override string ToString() => GetDescription();

        private static string GetMessage(IEnumerable<string> collection)
        {
            if (collection is null)
                return "<Null>";

            var clean = GetClean(collection);
            if (!clean.Any())
                return "[]";

            var joined = string.Join(", ", clean);
            return $"[{joined}]";
        }

        private static IEnumerable<string> GetClean(IEnumerable<string> collection) => collection.Where(IsNotEmpty);
        private static bool IsNotEmpty(string v) => !string.IsNullOrEmpty(v);
        private static bool ShouldApplyFilter(IEnumerable<string> collection) => GetClean(collection ?? []).Any();

        public (bool Compatible, string Reason) AreCompatible(IEnumerable<string> alignments, IEnumerable<string> challengeRatings, IEnumerable<string> types)
        {
            if (ShouldApplyFilter(Alignments))
            {
                if (!Alignments.Where(IsNotEmpty).Intersect(alignments).Any())
                    return (false, $"Alignment filter {GetMessage(Alignments)} is not compatible with {GetMessage(alignments)}");
            }

            if (ShouldApplyFilter(ChallengeRatings))
            {
                if (!ChallengeRatings.Where(IsNotEmpty).Intersect(challengeRatings).Any())
                    return (false, $"CR filter {GetMessage(ChallengeRatings)} is not compatible with {GetMessage(challengeRatings)}");
            }

            if (ShouldApplyFilter(Types))
            {
                if (!Types.Where(IsNotEmpty).Intersect(types).Any())
                    return (false, $"Type filter {GetMessage(Types)} is not compatible with {GetMessage(types)}");
            }

            return (true, null);
        }
    }
}
