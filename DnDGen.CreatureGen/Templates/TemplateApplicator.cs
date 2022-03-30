using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null);
        IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null);
        IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null);

        Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null);
        Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null);
    }
}
