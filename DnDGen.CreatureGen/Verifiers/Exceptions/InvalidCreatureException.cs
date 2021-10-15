using DnDGen.CreatureGen.Creatures;
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

        public InvalidCreatureException(bool asCharacter, string creature = null, string challengeRating = null, string template = null, string type = null)
        {
            this.creature = creature;
            this.challengeRating = challengeRating;
            this.asCharacter = asCharacter;
            this.template = template;
            this.type = type;
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.AppendLine("Invalid creature:");
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

                return message.ToString();
            }
        }
    }
}