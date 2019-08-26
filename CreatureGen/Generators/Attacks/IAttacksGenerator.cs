using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Generators.Attacks
{
    internal interface IAttacksGenerator
    {
        int GenerateBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints);
        int? GenerateGrappleBonus(string size, int baseAttackBonus, Ability strength);
        IEnumerable<Attack> GenerateAttacks(string creatureName, string originalSize, string size, int baseAttackBonus, Dictionary<string, Ability> abilities, int hitDiceQuantity);
        IEnumerable<Attack> ApplyAttackBonuses(IEnumerable<Attack> attacks, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
    }
}
