using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using System;
using System.Text;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class InvalidCreatureException : Exception
    {
        private readonly string creature;
        private readonly string challengeRating;
        private readonly bool asCharacter;
        private readonly string template;
        private readonly string type;
        private readonly string alignment;
        private readonly string reason;

        public InvalidCreatureException(string reason, bool asCharacter, string creature = null, RandomFilters filters = null)
            : this(reason, asCharacter, creature, filters?.Template, filters?.Type, filters?.ChallengeRating, filters?.Alignment)
        {

        }

        public InvalidCreatureException(
            string reason, 
            bool asCharacter, 
            string creature = null, 
            string template = null,
            string type = null, 
            string challengeRating = null, 
            string alignment = null)
        {
            this.reason = reason;
            this.asCharacter = asCharacter;
            this.creature = creature;
            this.template = template;
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

                if (template == CreatureConstants.Templates.None)
                    message.AppendLine($"\tTemplate: None");
                else if (template != null)
                    message.AppendLine($"\tTemplate: {template}");

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