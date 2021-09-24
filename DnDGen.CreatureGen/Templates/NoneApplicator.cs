using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
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

        private bool IsCompatible(
            IEnumerable<string> types,
            double creatureHitDiceQuantity,
            CreatureDataSelection creatureData,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            if (!string.IsNullOrEmpty(type) && !types.Contains(type))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                if (!CreatureInRange(creatureData.ChallengeRating, challengeRating, asCharacter, creatureHitDiceQuantity, types))
                    return false;
            }

            return true;
        }

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, string type = null, string challengeRating = null)
        {
            var filteredBaseCreatures = sourceCreatures;
            var allData = creatureDataSelector.SelectAll();
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);

            if (!string.IsNullOrEmpty(challengeRating))
            {
                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => CreatureInRange(allData[c].ChallengeRating, challengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(type))
            {
                filteredBaseCreatures = filteredBaseCreatures.Where(c => allTypes[c].Contains(type));
            }

            var templateCreatures = filteredBaseCreatures.Where(c => IsCompatible(allTypes[c], allHitDice[c], allData[c], asCharacter, type, challengeRating));

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
