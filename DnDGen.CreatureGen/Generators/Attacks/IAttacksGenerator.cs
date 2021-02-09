using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Attacks
{
    internal interface IAttacksGenerator
    {
        int GenerateBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints);
        int? GenerateGrappleBonus(string creature, string size, int baseAttackBonus, Ability strength);
        IEnumerable<Attack> GenerateAttacks(string creatureName, string originalSize, string size, int baseAttackBonus, Dictionary<string, Ability> abilities, int hitDiceQuantity);
        IEnumerable<Attack> ApplyAttackBonuses(IEnumerable<Attack> attacks, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
    }
}
