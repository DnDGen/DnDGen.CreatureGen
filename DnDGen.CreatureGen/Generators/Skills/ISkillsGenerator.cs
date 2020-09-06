using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateFor(
            HitPoints hitPoints,
            string creatureName,
            CreatureType creatureType,
            Dictionary<string, Ability> abilities,
            bool canUseEquipment,
            string size,
            bool includeFirstHitDieBonus = true);
        IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
        IEnumerable<Skill> SetArmorCheckPenalties(string creature, IEnumerable<Skill> skills, Equipment equipment);
        IEnumerable<Skill> ApplySkillPointsAsRanks(IEnumerable<Skill> skills, HitPoints hitPoints, CreatureType creatureType, Dictionary<string, Ability> abilities);
    }
}