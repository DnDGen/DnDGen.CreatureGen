using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Defenses
{
    internal interface IHitPointsGenerator
    {
        HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution, string size, int additionalHitDice = 0, bool asCharacter = false);
        HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats);
    }
}