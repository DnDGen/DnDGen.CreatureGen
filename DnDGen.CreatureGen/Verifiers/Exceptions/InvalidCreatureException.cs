using DnDGen.CreatureGen.Generators.Creatures;
using System;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class InvalidCreatureException : Exception
    {
        private readonly string creature;
        private readonly string challengeRating;
        private readonly bool asCharacter;
        private readonly string[] templates;
        private readonly string type;
        private readonly string alignment;
        private readonly string reason;

        public InvalidCreatureException(string reason, bool asCharacter, string creature = null, Filters filters = null)
            : this(reason, asCharacter, creature, filters?.Type, filters?.ChallengeRating, filters?.Alignment, filters?.CleanTemplates?.ToArray() ?? new string[0])
        {

        }

        public InvalidCreatureException(
            string reason,
            bool asCharacter,
            string creature = null,
            string type = null,
            string challengeRating = null,
            string alignment = null,
            params string[] templates)
        {
            this.reason = reason;
            this.asCharacter = asCharacter;
            this.creature = creature;
            this.templates = templates;
            this.type = type;
            this.challengeRating = challengeRating;
            this.alignment = alignment;
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.AppendLine("Invalid creature:");

                if (reason != null)
                    message.AppendLine($"\tReason: {reason}");

                message.AppendLine($"\tAs Character: {asCharacter}");

                if (creature != null)
                    message.AppendLine($"\tCreature: {creature}");

                var nonEmptyTemplates = templates.Where(t => !string.IsNullOrEmpty(t));
                var joinedTemplates = string.Join(", ", nonEmptyTemplates);

                if (nonEmptyTemplates.Any())
                    message.AppendLine($"\tTemplate: {joinedTemplates}");

                if (type != null)
                    message.AppendLine($"\tType: {type}");

                if (challengeRating != null)
                    message.AppendLine($"\tCR: {challengeRating}");

                if (alignment != null)
                    message.AppendLine($"\tAlignment: {alignment}");

                return message.ToString();
            }
        }
    }
}