using CreatureGen.Creatures;
using CreatureGen.Tables;
using CreatureGen.Verifiers;
using DnDGen.Core.Selectors.Collections;
using System.Linq;

namespace CreatureGen.Generators.Verifiers
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

            var templateCreatures = collectionsSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, templateName);
            return templateCreatures.Contains(creatureName);
        }
    }
}