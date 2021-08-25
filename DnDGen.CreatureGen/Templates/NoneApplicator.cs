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

        public NoneApplicator(ICollectionSelector collectionSelector, ICreatureDataSelector creatureDataSelector)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
        }

        public Creature ApplyTo(Creature creature)
        {
            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            return creature;
        }

        public string GetPotentialChallengeRating(string creature)
        {
            var data = creatureDataSelector.SelectFor(creature);
            return data.ChallengeRating;
        }

        public (string Lower, string Upper) GetChallengeRatingRange(string challengeRating) => (challengeRating, challengeRating);
        //public (string Lower, string Upper) GetChallengeRatingRange() => (null, null);
        public (double? Lower, double? Upper) GetHitDiceRange(string challengeRating) => (null, null);

        public IEnumerable<string> GetPotentialTypes(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            return types;
        }

        public bool IsCompatible(string creature, string type = null, string challengeRating = null)
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
                var cr = GetPotentialChallengeRating(creature);
                if (cr != challengeRating)
                    return false;
            }

            return true;
        }

        private bool IsCompatible(string creature)
        {
            return true;
        }
    }
}
