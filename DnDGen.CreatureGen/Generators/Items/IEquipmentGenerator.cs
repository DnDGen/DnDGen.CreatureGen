using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Items
{
    internal interface IEquipmentGenerator
    {
        Equipment Generate(string creatureName, bool canUseEquipment, IEnumerable<Feat> feats, int level, IEnumerable<Attack> attacks);
    }
}
