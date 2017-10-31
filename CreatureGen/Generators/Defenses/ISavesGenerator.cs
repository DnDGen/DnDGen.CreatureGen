using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Generators.Defenses
{
    internal interface ISavesGenerator
    {
        Saves GenerateWith(string creatureName, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
    }
}