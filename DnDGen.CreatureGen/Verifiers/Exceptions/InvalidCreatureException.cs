using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using System;
using System.Text;

namespace DnDGen.CreatureGen.Verifiers.Exceptions
{
    public class InvalidCreatureException(
        string reason,
        bool asCharacter,
        string creature = null,
        Filters filters = null,
        AbilityRandomizer abilityRandomizer = null,
        params string[] templates) : Exception
    {
        public InvalidCreatureException(
            string reason,
            bool asCharacter,
            string creature,
            Filters filters,
            string abilityRoll,
            params string[] templates) :
            this(reason, asCharacter, creature, filters, new AbilityRandomizer(abilityRoll), templates)
        {

        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.AppendLine("Invalid creature:");

                if (reason is not null)
                    message.AppendLine($"\tReason: {reason}");

                message.AppendLine($"\tAs Character: {asCharacter}");

                if (creature is not null)
                    message.AppendLine($"\tCreature: {creature}");

                if (templates.Length > 0)
                    message.AppendLine($"\tTemplates: {string.Join(", ", templates)}");

                if (filters is not null)
                {
                    var description = filters.GetDescription();
                    message.AppendLine($"Filters: {description}");
                }

                if (abilityRandomizer != null)
                    message.AppendLine($"\tAbility Roll: {abilityRandomizer.Roll}");

                return message.ToString();
            }
        }
    }
}