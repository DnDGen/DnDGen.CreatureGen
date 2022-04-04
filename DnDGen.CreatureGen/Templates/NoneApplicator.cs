using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
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

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                new[] { creature.Alignment.Full },
                creature.ChallengeRating,
                filters);
            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    CreatureConstants.Templates.None,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment);
            }

            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                new[] { creature.Alignment.Full },
                creature.ChallengeRating,
                filters);
            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    CreatureConstants.Templates.None,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment);
            }

            return creature;
        }

        private bool IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            double creatureHitDiceQuantity,
            string creatureChallengeRating,
            bool asCharacter,
            Filters filters)
        {
            var creatureType = types.First();

            if (asCharacter && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                creatureChallengeRating = ChallengeRatingConstants.CR0;
            }

            var compatibility = IsCompatible(types, alignments, creatureChallengeRating, filters);
            return compatibility.Compatible;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Type) && !types.Contains(filters.Type))
            {
                return (false, $"Type filter '{filters.Type}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating) && creatureChallengeRating != filters.ChallengeRating)
            {
                return (false, $"CR filter {filters.ChallengeRating} does not match creature CR {creatureChallengeRating}");
            }

            if (!string.IsNullOrEmpty(filters?.Alignment) && !alignments.Contains(filters.Alignment))
            {
                return (false, $"Alignment filter '{filters.Alignment}' is not valid");
            }

            return (true, null);
        }

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var filteredBaseCreatures = sourceCreatures;
            var allData = creatureDataSelector.SelectAll();
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups);

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => PotentialChallengeRatingMatches(allData[c].ChallengeRating, filters.ChallengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(filters?.Type))
            {
                filteredBaseCreatures = filteredBaseCreatures.Where(c => allTypes[c].Contains(filters.Type));
            }

            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var alignmentCreatures = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureGroups, filters.Alignment);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(alignmentCreatures);
            }

            var templateCreatures = filteredBaseCreatures
                .Where(c => IsCompatible(
                    allTypes[c],
                    allAlignments[c + GroupConstants.Exploded],
                    allHitDice[c],
                    allData[c].ChallengeRating,
                    asCharacter,
                    filters));

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

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
