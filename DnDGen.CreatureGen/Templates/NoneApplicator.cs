using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class NoneApplicator : TemplateApplicator
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

        public CreaturePrototype ApplyTo(CreaturePrototype creature, bool asCharacter, Filters filters = null)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                creature.Alignments = [.. creature.Alignments.Where(adjustmentSelector => adjustmentSelector.Full == filters.Alignment)];
            }

            return creature;
        }

        public bool IsCompatible(CreaturePrototype creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, _) = AreFiltersCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.ChallengeRating,
                filters);

            return Compatible;
        }
    }
}
