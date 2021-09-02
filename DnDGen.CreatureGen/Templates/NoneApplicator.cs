using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class NoneApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;

        public NoneApplicator(ICollectionSelector collectionSelector, ICreatureDataSelector creatureDataSelector, IAdjustmentsSelector adjustmentSelector)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.adjustmentSelector = adjustmentSelector;
        }

        public Creature ApplyTo(Creature creature)
        {
            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            return creature;
        }

        public string GetPotentialChallengeRating(string creature, bool asCharacter)
        {
            var quantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            var creatureType = types.First();

            if (asCharacter && quantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                return ChallengeRatingConstants.CR0;
            }

            var data = creatureDataSelector.SelectFor(creature);
            return data.ChallengeRating;
        }

        private IEnumerable<string> GetPotentialTypes(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            return types;
        }

        private bool IsCompatible(string creature, bool asCharacter, string type = null, string challengeRating = null)
        {
            if (!IsCompatible(creature))
                return false;

            if (!string.IsNullOrEmpty(type))
            {
                var types = GetPotentialTypes(creature);
                if (!types.Contains(type))
                    return false;
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var cr = GetPotentialChallengeRating(creature, asCharacter);
                if (cr != challengeRating)
                    return false;
            }

            return true;
        }

        private bool IsCompatible(string creature)
        {
            return true;
        }

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, string type = null, string challengeRating = null)
        {
            var filteredBaseCreatures = sourceCreatures;

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var allData = creatureDataSelector.SelectAll();
                var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
                var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);

                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => CreatureInRange(allData[c].ChallengeRating, challengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(type))
            {
                var ofType = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(ofType);
            }

            var templateCreatures = filteredBaseCreatures.Where(c => IsCompatible(c, asCharacter, type, challengeRating));

            return templateCreatures;
        }

        private bool CreatureInRange(
            string creatureChallengeRating,
            string filterChallengeRating,
            bool asCharacter,
            double creatureHitDiceQuantity,
            IEnumerable<string> creatureTypes)
        {
            var creatureType = creatureTypes.First();

            if (asCharacter && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                creatureChallengeRating = ChallengeRatingConstants.CR0;
            }

            return creatureChallengeRating == filterChallengeRating;
        }
    }
}
