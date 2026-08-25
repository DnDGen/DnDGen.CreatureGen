using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public class Filters
    {
        /// <summary>
        /// A creature that matches any of these types, after templates are applied, will satisfy the filters
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// A creature that matches any of these challenge ratings, after templates are applied, will satisfy the filters
        /// </summary>
        public List<string> ChallengeRatings { get; set; }

        /// <summary>
        /// A creature that matches any of these alignments, after templates are applied, will satisfy the filters
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
            description.AppendLine($"Types: {GetMessage(Types)}");
            description.AppendLine($"CRs: {GetMessage(ChallengeRatings)}");
            description.AppendLine($"Alignments: {GetMessage(Alignments)}");

            return description.ToString();
        }

        public override string ToString() => GetDescription();

        private static string GetMessage(IEnumerable<string> collection)
        {
            if (collection is null)
                return "<Null>";

            if (!collection.Any())
                return "[]";

            var joined = string.Join(", ", collection);
            return $"[{joined}]";
        }

        public (bool Compatible, string Reason) AreCompatible(IEnumerable<string> alignments, IEnumerable<string> challengeRatings, IEnumerable<string> types)
        {
            if (Alignments?.Count > 0)
            {
                if (!Alignments.Intersect(alignments).Any())
                    return (false, $"Alignment filter is not compatible with {GetMessage(alignments)}. Filters: {GetDescription()}");
            }

            if (ChallengeRatings?.Count > 0)
            {
                if (!ChallengeRatings.Intersect(challengeRatings).Any())
                    return (false, $"CR filter is not compatible with {GetMessage(challengeRatings)}. Filters: {GetDescription()}");
            }

            if (Types?.Count > 0)
            {
                if (!Types.Intersect(types).Any())
                    return (false, $"Type filter is not compatible with {GetMessage(types)}. Filters: {GetDescription()}");
            }

            return (true, null);
        }
    }
}
