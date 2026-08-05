using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using System;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class InvalidCreatureException(
        string reason,
        bool asCharacter,
        string creature = null,
        string type = null,
        string challengeRating = null,
        string alignment = null,
        string abilityRoll = null,
        params string[] templates) : Exception
    {
        public InvalidCreatureException(string reason, bool asCharacter, string creature = null, Filters filters = null, AbilityRandomizer abilityRandomizer = null)
            : this(reason, asCharacter, creature, filters?.Type, filters?.ChallengeRating, filters?.Alignment, abilityRandomizer?.Roll, filters?.CleanTemplates?.ToArray() ?? [])
        {

        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.AppendLine("Invalid creature:");

                if (reason != null)
                    message.AppendLine($"\tReason: {reason}");

                message.AppendLine($"\tAs Character: {asCharacter}");

                if (creature != null)
                    message.AppendLine($"\tCreature: {creature}");

                var nonEmptyTemplates = templates.Where(t => !string.IsNullOrEmpty(t));
                var joinedTemplates = string.Join(", ", nonEmptyTemplates);

                if (nonEmptyTemplates.Any())
                    message.AppendLine($"\tTemplate: {joinedTemplates}");

                if (type != null)
                    message.AppendLine($"\tType: {type}");

                if (challengeRating != null)
                    message.AppendLine($"\tCR: {challengeRating}");

                if (alignment != null)
                    message.AppendLine($"\tAlignment: {alignment}");

                if (abilityRoll != null)
                    message.AppendLine($"\tAbility Roll: {abilityRoll}");

                return message.ToString();
            }
        }
    }
}