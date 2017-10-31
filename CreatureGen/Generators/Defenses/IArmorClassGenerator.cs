using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Generators.Defenses
{
    internal interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Ability dexterity, string size, string creatureName, IEnumerable<Feat> feats);
    }
}