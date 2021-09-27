using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly JustInTimeFactory factory;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly ICollectionSelector collectionsSelector;

        public CreatureVerifier(
            JustInTimeFactory factory,
            ICreatureDataSelector creatureDataSelector,
            ICollectionSelector collectionsSelector)
        {
            this.factory = factory;
            this.creatureDataSelector = creatureDataSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public bool CanBeCharacter(string creatureName)
        {
            var creatureData = creatureDataSelector.SelectFor(creatureName);
            return creatureData.LevelAdjustment.HasValue;
        }

        public bool VerifyCompatibility(bool asCharacter, string creature = null, string template = null, string type = null, string challengeRating = null)
        {
            IEnumerable<string> baseCreatures = new[] { creature };
            if (string.IsNullOrEmpty(creature))
            {
                baseCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            if (!string.IsNullOrEmpty(template))
            {
                var applicator = factory.Build<TemplateApplicator>(template);

                var filteredBaseCreatures = applicator.GetCompatibleCreatures(baseCreatures, asCharacter, type, challengeRating);
                return filteredBaseCreatures.Any();
            }

            //INFO: We can cheat and use the None template applicator
            var noneApplicator = factory.Build<TemplateApplicator>(CreatureConstants.Templates.None);

            var nonTemplateCreatures = noneApplicator.GetCompatibleCreatures(baseCreatures, asCharacter, type, challengeRating);

            if (nonTemplateCreatures.Any())
                return true;

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            foreach (var otherTemplate in templates)
            {
                var templateApplicator = factory.Build<TemplateApplicator>(otherTemplate);

                var filteredBaseCreatures = templateApplicator.GetCompatibleCreatures(baseCreatures, asCharacter, type, challengeRating);
                if (filteredBaseCreatures.Any())
                    return true;
            }

            return false;
        }
    }
}