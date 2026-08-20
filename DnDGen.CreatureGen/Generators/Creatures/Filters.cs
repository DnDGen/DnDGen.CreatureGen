using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public class Filters
    {
        /// <summary>
        /// Templates are all applied, and in order.
        /// </summary>
        //public List<string> Templates { get; set; }

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

        //public string[] CleanTemplates => [.. Templates.Where(t => !string.IsNullOrEmpty(t))];

        public Filters()
        {
            //Templates = [];
            Types = [];
            ChallengeRatings = [];
            Alignments = [];
        }

        public string GetDescription(bool asCharacter)
        {
            var description = new StringBuilder();
            description.AppendLine($"As Character: {asCharacter}");
            //description.AppendLine($"Templates: {GetMessage(CleanTemplates)}");
            description.AppendLine($"Types: {GetMessage(Types)}");
            description.AppendLine($"CR: {GetMessage(ChallengeRatings)}");
            description.AppendLine($"Alignments: {GetMessage(Alignments)}");

            return description.ToString();
        }

        private static string GetMessage(IEnumerable<string> collection)
        {
            if (collection is null)
                return "<Null>";

            if (!collection.Any())
                return "[]";

            var joined = string.Join(", ", collection);
            return $"[{joined}]";
        }
    }
}
