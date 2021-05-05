using System;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
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
                var formattedTemplate = $"'{template}'";
                if (string.IsNullOrEmpty(template))
                {
                    formattedTemplate += " (None)";
                }

                return $"'{creature}' and {formattedTemplate} are not compatible";
            }
        }
    }
}