using System;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class IncompatibleCreatureAsCharacterException : Exception
    {
        private readonly string creature;

        public IncompatibleCreatureAsCharacterException(string creature)
        {
            this.creature = creature;
        }

        public override string Message
        {
            get
            {
                return $"{creature} cannot be generated as a character";
            }
        }
    }
}