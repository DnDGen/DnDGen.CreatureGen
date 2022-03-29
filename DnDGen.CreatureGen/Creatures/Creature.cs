using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Creatures
{
    public class Creature
    {
        public string Name { get; set; }
        public List<string> Templates { get; set; }
        public string Size { get; set; }
        public CreatureType Type { get; set; }
        public HitPoints HitPoints { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public bool IsAdvanced { get; set; }

        public int InitiativeBonus { get; set; }
        public int TotalInitiativeBonus
        {
            get
            {
                if (!Abilities.Any())
                    return 0;

                var totalInitiativeBonus = Abilities[AbilityConstants.Dexterity].Modifier;

                if (!Abilities[AbilityConstants.Dexterity].HasScore)
                    totalInitiativeBonus = Abilities[AbilityConstants.Intelligence].Modifier;

                totalInitiativeBonus += InitiativeBonus;

                return totalInitiativeBonus;
            }
        }

        public Dictionary<string, Measurement> Speeds { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public int BaseAttackBonus { get; set; }
        public int? GrappleBonus { get; set; }

        public Measurement Space { get; set; }
        public Measurement Reach { get; set; }
        public IEnumerable<Feat> SpecialQualities { get; set; }
        public Dictionary<string, Save> Saves { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public string ChallengeRating { get; set; }
        public Alignment Alignment { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }
        public int NumberOfHands { get; set; }
        public bool CanUseEquipment { get; set; }
        public IEnumerable<Attack> Attacks { get; set; }
        public Magic Magic { get; set; }

        public IEnumerable<Attack> FullMeleeAttack
        {
            get
            {
                //INFO: Ordering by descending because false is 0 and true is 1
                return Attacks.Where(a => a.IsMelee && !a.IsSpecial).OrderByDescending(a => a.IsPrimary);
            }
        }

        public IEnumerable<Attack> FullRangedAttack
        {
            get
            {
                //INFO: Ordering by descending because false is 0 and true is 1
                return Attacks.Where(a => !a.IsMelee && !a.IsSpecial).OrderByDescending(a => a.IsPrimary);
            }
        }

        public IEnumerable<Attack> SpecialAttacks
        {
            get
            {
                //INFO: Ordering by descending because false is 0 and true is 1
                return Attacks.Where(a => a.IsSpecial).OrderByDescending(a => a.IsPrimary).ThenByDescending(a => a.IsMelee);
            }
        }

        public Attack MeleeAttack
        {
            get
            {
                return FullMeleeAttack.FirstOrDefault(a => a.IsPrimary);
            }
        }

        public Attack RangedAttack
        {
            get
            {
                return FullRangedAttack.FirstOrDefault(a => a.IsPrimary);
            }
        }

        public string Summary
        {
            get
            {
                var summary = string.Join(" ", Templates);
                summary += $" {Name}";

                if (IsAdvanced)
                    summary += " [Advanced]";

                return summary.Trim();
            }
        }

        public Equipment Equipment { get; set; }

        public Creature()
        {
            Abilities = new Dictionary<string, Ability>();
            Alignment = new Alignment();
            ArmorClass = new ArmorClass();
            Attacks = Enumerable.Empty<Attack>();
            ChallengeRating = string.Empty;
            Feats = Enumerable.Empty<Feat>();
            HitPoints = new HitPoints();
            Name = string.Empty;
            Reach = new Measurement("feet");
            Saves = new Dictionary<string, Save>();
            Size = string.Empty;
            Skills = Enumerable.Empty<Skill>();
            Space = new Measurement("feet");
            SpecialQualities = Enumerable.Empty<Feat>();
            Templates = new List<string>();
            Type = new CreatureType();
            Speeds = new Dictionary<string, Measurement>();
            Equipment = new Equipment();
            Magic = new Magic();
            Languages = new List<string>();
        }
    }
}