using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Defenses
{
    internal interface ISavesGenerator
    {
        Dictionary<string, Save> GenerateWith(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
    }
}