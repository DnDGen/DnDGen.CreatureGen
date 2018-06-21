using CreatureGen.Creatures;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Linq;

namespace CreatureGen.Verifiers
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