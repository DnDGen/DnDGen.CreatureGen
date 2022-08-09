using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal interface ICreaturePrototypeFactory
    {
        IEnumerable<CreaturePrototype> Build(IEnumerable<string> creatureNames, bool asCharacter);
    }
}
