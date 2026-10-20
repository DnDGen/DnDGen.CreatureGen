using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        Ability MinimumAbility { get; }

        Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null);
        Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null);
        CreaturePrototype ApplyTo(CreaturePrototype creature, Filters filters = null);

        bool IsCompatible(CreaturePrototype creature, Filters filters = null);
    }
}
