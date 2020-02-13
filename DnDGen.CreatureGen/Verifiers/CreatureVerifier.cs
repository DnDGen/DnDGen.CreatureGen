using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly ICollectionSelector collectionsSelector;

        public CreatureVerifier(ICollectionSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public bool VerifyCompatibility(string creatureName, string templateName)
        {
            if (templateName == CreatureConstants.Templates.None)
                return true;

            var templateCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, templateName);
            return templateCreatures.Contains(creatureName);
        }
    }
}