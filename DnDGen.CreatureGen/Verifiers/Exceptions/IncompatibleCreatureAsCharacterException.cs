using System;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class IncompatibleCreatureAsCharacterException : Exception
    {
        private readonly string creature;
        private readonly string challengeRating;

        public IncompatibleCreatureAsCharacterException(string creature, string challengeRating = null)
        {
            this.creature = creature;
            this.challengeRating = challengeRating;
        }

        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(challengeRating))
                {
                    return $"{creature} cannot be generated as a character";
                }

                return $"{creature} (CR {challengeRating}) cannot be generated as a character";
            }
        }
    }
}