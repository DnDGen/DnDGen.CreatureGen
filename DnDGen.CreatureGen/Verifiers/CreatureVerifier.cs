using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Generators;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly JustInTimeFactory factory;

        public CreatureVerifier(JustInTimeFactory factory)
        {
            this.factory = factory;
        }

        public bool VerifyCompatibility(string creatureName, string templateName)
        {
            var applicator = factory.Build<TemplateApplicator>(templateName);

            return applicator.IsCompatible(creatureName);
        }
    }
}