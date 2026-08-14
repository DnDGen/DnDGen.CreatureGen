using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Verifiers
{
    public interface ICreatureVerifier
    {
        /// <summary>
        /// Returns true if any valid creature exists that satisfies the template and filters.
        /// If no templates are specified as a filter, all templates will be checked to see if any templated creature is valid.
        /// If you explicitly want only non-templated creatures, include the template "None" in your template filters.
        /// </summary>
        /// <param name="asCharacter"></param>
        /// <param name="creature"></param>
        /// <param name="abilityRandomizer"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        bool VerifyCompatibility(bool asCharacter, string creature = null, AbilityRandomizer abilityRandomizer = null, Filters filters = null);

        IEnumerable<string> GetCompatibleCreaturesForTemplate(
            IEnumerable<string> sourceCreatures,
            string template,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null);
        IEnumerable<CreaturePrototype> GetChainedTemplates(
            IEnumerable<string> sourceCreatures,
            List<string> templates,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null);
    }
}