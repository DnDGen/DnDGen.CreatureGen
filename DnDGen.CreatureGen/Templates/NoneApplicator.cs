using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class NoneApplicator(ICollectionSelector collectionSelector, ICreaturePrototypeFactory prototypeFactory) : TemplateApplicator
    {
        public Ability MinimumAbility => null;

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = AreFiltersCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    null,
                    [.. creature.Templates.Union([CreatureConstants.Templates.None])]);
            }

            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = AreFiltersCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    null,
                    [.. creature.Templates.Union([CreatureConstants.Templates.None])]);
            }

            return await Task.FromResult(creature);
        }

        private static (bool Compatible, string Reason) AreFiltersCompatible(
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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null)
        {
            var templateCreatures = collectionSelector.SelectFrom(
                Config.Name,
                TableNameConstants.Collection.CreatureGroups,
                CreatureConstants.Templates.None + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);

            if (!string.IsNullOrEmpty(filters?.Type))
            {
                var groupName = CreatureConstants.Templates.None + filters.Type;
                var typeCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(typeCreatures);
            }

            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var groupName = CreatureConstants.Templates.None + filters.Alignment;
                var alignmentCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(alignmentCreatures);
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var groupName = CreatureConstants.Templates.None + asCharacter + filters.ChallengeRating;
                var crCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(crCreatures);
            }

            return filteredBaseCreatures;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(
            IEnumerable<string> sourceCreatures,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, abilityRandomizer, filters);
            if (!compatibleCreatures.Any())
                return [];

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }

        private static CreaturePrototype ApplyToPrototype(CreaturePrototype prototype, string presetAlignment)
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
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }
    }
}
