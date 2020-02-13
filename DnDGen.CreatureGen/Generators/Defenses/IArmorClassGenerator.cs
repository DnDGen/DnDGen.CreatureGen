using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Defenses
{
    internal interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Dictionary<string, Ability> abilities, string size, string creatureName, CreatureType creatureType, IEnumerable<Feat> feats, int naturalArmor);
    }
}