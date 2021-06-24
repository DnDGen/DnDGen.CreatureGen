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

        public CreatureVerifier(JustInTimeFactory factory, ICreatureDataSelector creatureDataSelector, ICollectionSelector collectionsSelector)
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
            IEnumerable<string> creatures = new[] { creature };
            if (string.IsNullOrEmpty(creature))
            {
                creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                creatures = creatures.Intersect(characters);
            }

            IEnumerable<string> templates = null;
            if (!string.IsNullOrEmpty(template))
            {
                var applicator = factory.Build<TemplateApplicator>(template);
                creatures = creatures.Where(applicator.IsCompatible);

                templates = new[] { template };
            }

            if (!string.IsNullOrEmpty(type))
            {
                creatures = GetCreaturesOfType(type, creatures, templates);
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                creatures = GetCreaturesOfChallengeRating(challengeRating, creatures, templates);
            }

            return creatures.Any();
        }

        private IEnumerable<string> GetCreaturesOfType(string creatureType, IEnumerable<string> creatureGroup, IEnumerable<string> templates = null)
        {
            var creatures = new List<string>();

            if (templates == null)
            {
                templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

                //Add in non-template creatures
                var ofType = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, creatureType);
                creatures.AddRange(ofType.Intersect(creatureGroup));
            }

            foreach (var template in templates)
            {
                var creaturesOfTypeAndTemplate = GetCreaturesOfTemplate(template, creatureGroup, creatureType: creatureType);
                creatures.AddRange(creaturesOfTypeAndTemplate);
            }

            return creatures;
        }

        private IEnumerable<string> GetCreaturesOfChallengeRating(string challengeRating, IEnumerable<string> creatureGroup, IEnumerable<string> templates = null)
        {
            var creatures = new List<string>();

            if (templates == null)
            {
                templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

                //Add in non-template creatures
                var ofChallengeRating = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, challengeRating);
                creatures.AddRange(ofChallengeRating.Intersect(creatureGroup));
            }

            foreach (var template in templates)
            {
                var creaturesOfChallengeRatingAndTemplate = GetCreaturesOfTemplate(template, creatureGroup, challengeRating: challengeRating);
                creatures.AddRange(creaturesOfChallengeRatingAndTemplate);
            }

            return creatures;
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, string creatureType = null, string challengeRating = null)
        {
            var templateApplicator = factory.Build<TemplateApplicator>(template);
            var creatures = creatureGroup.Where(templateApplicator.IsCompatible);

            if (creatureType != null)
            {
                creatures = creatures.Where(c => templateApplicator.GetPotentialTypes(c).Contains(creatureType));
            }

            if (challengeRating != null)
            {
                creatures = creatures.Where(c => templateApplicator.GetPotentialChallengeRating(c) == challengeRating);
            }

            return creatures;
        }
    }
}