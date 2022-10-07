using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public class Filters
    {
        public List<string> Templates { get; set; }
        public string Type { get; set; }
        public string ChallengeRating { get; set; }
        public string Alignment { get; set; }

        public List<string> CleanTemplates => Templates.Where(t => !string.IsNullOrEmpty(t)).ToList();

        public Filters()
        {
            Templates = new List<string>();
        }

        public string GetDescription(bool asCharacter)
        {
            var description = new StringBuilder();
            var joinedTemplates = string.Join(", ", CleanTemplates);
            var messageTemplate = CleanTemplates.Count > 0 ? (!string.IsNullOrEmpty(joinedTemplates) ? joinedTemplates : "(None)") : "Null";

            description.AppendLine($"As Character: {asCharacter}");
            description.AppendLine($"Template: {messageTemplate}");
            description.AppendLine($"Type: {Type ?? "Null"}");
            description.AppendLine($"CR: {ChallengeRating ?? "Null"}");
            description.AppendLine($"Alignment: {Alignment ?? "Null"}");

            return description.ToString();
        }
    }
}
