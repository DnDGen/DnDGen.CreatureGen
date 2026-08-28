using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal interface ICreaturePrototypeFactory
    {
        IEnumerable<CreaturePrototype> Build(IEnumerable<string> creatureNames, bool asCharacter, AbilityRandomizer abilityRandomizer = null);
        CreaturePrototype Build(string creatureName, bool asCharacter, AbilityRandomizer abilityRandomizer = null);
        CreaturePrototype Clone(CreaturePrototype source);
    }
}
