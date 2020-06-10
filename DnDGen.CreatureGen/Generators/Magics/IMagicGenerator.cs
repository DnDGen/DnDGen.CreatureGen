using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Magics;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Magics
{
    internal interface IMagicGenerator
    {
        Magic GenerateWith(string creatureName, Alignment alignment, Dictionary<string, Ability> abilities, Equipment equipment);
    }
}
