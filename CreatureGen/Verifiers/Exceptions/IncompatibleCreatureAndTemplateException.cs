using System;

namespace CreatureGen.Verifiers.Exceptions
{
    public class IncompatibleCreatureAndTemplateException : Exception
    {
        private readonly string creature;
        private readonly string template;

        public IncompatibleCreatureAndTemplateException(string creature, string template)
        {
            this.creature = creature;
            this.template = template;
        }

        public override string Message
        {
            get
            {
                return $"{creature} and {template} are not compatible";
            }
        }
    }
}