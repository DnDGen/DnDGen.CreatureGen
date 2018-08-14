using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Generators.Defenses
{
    internal interface IHitPointsGenerator
    {
        HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution, string size, int additionalHitDice = 0);
        HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats);
    }
}