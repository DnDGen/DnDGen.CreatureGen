using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
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
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly ICreaturePrototypeFactory prototypeFactory;

        public NoneApplicator(
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            ICollectionTypeAndAmountSelector typeAndAmountSelector,
            ICreaturePrototypeFactory prototypeFactory)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.prototypeFactory = prototypeFactory;
        }

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = AreFiltersCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                false,
                creature.HitPoints.HitDiceQuantity,
                filters);
            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([CreatureConstants.Templates.None])]);
            }

            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = AreFiltersCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                false,
                creature.HitPoints.HitDiceQuantity,
                filters);
            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([CreatureConstants.Templates.None])]);
            }

            return await Task.FromResult(creature);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            bool adjustCharacterChallengeRating,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Type) && !types.Contains(filters.Type))
            {
                return (false, $"Type filter '{filters.Type}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var creatureType = types.First();

                if (adjustCharacterChallengeRating && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
                {
                    creatureChallengeRating = ChallengeRatingConstants.CR0;
                }

                if (creatureChallengeRating != filters.ChallengeRating)
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
            if (string.IsNullOrEmpty(filters?.ChallengeRating)
                && string.IsNullOrEmpty(filters?.Type)
                && string.IsNullOrEmpty(filters?.Alignment))
                return sourceCreatures;

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allHitDice = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);

            return sourceCreatures
                .Where(c => AreFiltersCompatible(
                    allTypes[c],
                    allAlignments[c],
                    allData[c].Single().ChallengeRating,
                    asCharacter,
                    allHitDice[c].Single().AmountAsDouble,
                    filters).Compatible);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, filters);
            if (!compatibleCreatures.Any())
                return [];

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(CreaturePrototype prototype, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
            {
                prototype.Alignments = [.. prototype.Alignments.Where(adjustmentSelector => adjustmentSelector.Full == presetAlignment)];
            }

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatiblePrototypes = sourceCreatures
                .Where(p => AreFiltersCompatible(
                    p.Type.AllTypes,
                    p.Alignments.Select(a => a.Full),
                    p.ChallengeRating,
                    false,
                    p.HitDiceQuantity,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }
    }
}
