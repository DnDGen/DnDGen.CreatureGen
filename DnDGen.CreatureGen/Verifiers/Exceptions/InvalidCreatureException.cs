using System;

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
                var message = $"Invalid creature:\n\tAs Character: {asCharacter}";

                if (!string.IsNullOrEmpty(creature))
                {
                    message += $"\n\tCreature: {creature}";
                }

                if (!string.IsNullOrEmpty(template))
                {
                    message += $"\n\tTemplate: {template}";
                }

                if (!string.IsNullOrEmpty(type))
                {
                    message += $"\n\tType: {type}";
                }

                if (!string.IsNullOrEmpty(challengeRating))
                {
                    message += $"\n\tCR: {challengeRating}";
                }

                return message;
            }
        }
    }
}