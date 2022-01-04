using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, RandomFilters filters = null);

        Creature ApplyTo(Creature creature, bool asCharacter, RandomFilters filters = null);
        Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, RandomFilters filters = null);
    }
}
