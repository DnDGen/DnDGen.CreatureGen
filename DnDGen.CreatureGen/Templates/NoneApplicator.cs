using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
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

        public Creature ApplyTo(Creature creature, bool asCharacter, string type = null, string challengeRating = null, string alignment = null)
        {
            if (!IsCompatible(
                creature.Type.AllTypes,
                new[] { creature.Alignment.Full },
                creature.ChallengeRating,
                type,
                challengeRating,
                alignment))
            {
                throw new InvalidCreatureException(asCharacter, creature.Name, CreatureConstants.Templates.None, type, challengeRating, alignment);
            }

            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, string type = null, string challengeRating = null, string alignment = null)
        {
            if (!IsCompatible(
                creature.Type.AllTypes,
                new[] { creature.Alignment.Full },
                creature.ChallengeRating,
                type,
                challengeRating,
                alignment))
            {
                throw new InvalidCreatureException(asCharacter, creature.Name, CreatureConstants.Templates.None, type, challengeRating, alignment);
            }

            return creature;
        }

        private bool IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            double creatureHitDiceQuantity,
            string creatureChallengeRating,
            bool asCharacter,
            string type = null,
            string challengeRating = null,
            string alignment = null)
        {
            var creatureType = types.First();

            if (asCharacter && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                creatureChallengeRating = ChallengeRatingConstants.CR0;
            }

            return IsCompatible(types, alignments, creatureChallengeRating, type, challengeRating, alignment);
        }

        private bool IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            string type = null,
            string challengeRating = null,
            string alignment = null)
        {
            if (!string.IsNullOrEmpty(type) && !types.Contains(type))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                if (creatureChallengeRating != challengeRating)
                    return false;
            }

            if (!string.IsNullOrEmpty(alignment))
            {
                if (!alignments.Contains(alignment))
                    return false;
            }

            return true;
        }

        public IEnumerable<string> GetCompatibleCreatures(
            IEnumerable<string> sourceCreatures,
            bool asCharacter,
            string type = null,
            string challengeRating = null,
            string alignment = null)
        {
            var filteredBaseCreatures = sourceCreatures;
            var allData = creatureDataSelector.SelectAll();
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups);

            if (!string.IsNullOrEmpty(challengeRating))
            {
                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => PotentialChallengeRatingMatches(allData[c].ChallengeRating, challengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(type))
            {
                filteredBaseCreatures = filteredBaseCreatures.Where(c => allTypes[c].Contains(type));
            }

            if (!string.IsNullOrEmpty(alignment))
            {
                filteredBaseCreatures = filteredBaseCreatures.Where(c => allAlignments[c + GroupConstants.Exploded].Contains(alignment));
            }

            var templateCreatures = filteredBaseCreatures
                .Where(c => IsCompatible(
                    allTypes[c],
                    allAlignments[c + GroupConstants.Exploded],
                    allHitDice[c],
                    allData[c].ChallengeRating,
                    asCharacter,
                    type,
                    challengeRating,
                    alignment));

            return templateCreatures;
        }

        private bool PotentialChallengeRatingMatches(
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
