using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Generators;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly JustInTimeFactory factory;
        private readonly ICreatureDataSelector creatureDataSelector;

        public CreatureVerifier(JustInTimeFactory factory, ICreatureDataSelector creatureDataSelector)
        {
            this.factory = factory;
            this.creatureDataSelector = creatureDataSelector;
        }

        public bool CanBeCharacter(string creatureName)
        {
            var creatureData = creatureDataSelector.SelectFor(creatureName);
            return creatureData.LevelAdjustment.HasValue;
        }

        public bool VerifyCompatibility(string creatureName, string templateName)
        {
            var applicator = factory.Build<TemplateApplicator>(templateName);

            return applicator.IsCompatible(creatureName);
        }
    }
}