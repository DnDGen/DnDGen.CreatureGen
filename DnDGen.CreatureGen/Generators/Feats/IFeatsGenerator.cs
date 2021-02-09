using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Feats
{
    internal interface IFeatsGenerator
    {
        IEnumerable<Feat> GenerateSpecialQualities(
            string creatureName,
            CreatureType creatureType,
            HitPoints hitPoints,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            bool canUseEquipment,
            string size,
            Alignment alignment);
        IEnumerable<Feat> GenerateFeats(
            HitPoints hitPoints,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            IEnumerable<Feat> specialQualities,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size,
            bool canUseEquipment);
    }
}