using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Generators.Defenses
{
    internal interface IHitPointsGenerator
    {
        HitPoints GenerateFor(string creatureName, Ability constitution);
        HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats);
    }
}