using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Creatures
{
    public class Creature
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public string Size { get; set; }
        public CreatureType Type { get; set; }
        public HitPoints HitPoints { get; set; }
        public int InitiativeBonus { get; set; }
        public Dictionary<string, Measurement> Speeds { get; private set; }
        public ArmorClass ArmorClass { get; set; }
        public int BaseAttackBonus { get; set; }
        public int? GrappleBonus { get; set; }
        public Measurement Space { get; set; }
        public Measurement Reach { get; set; }
        public IEnumerable<Feat> SpecialQualities { get; set; }
        public Saves Saves { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public string ChallengeRating { get; set; }
        public Alignment Alignment { get; set; }
        public int? LevelAdjustment { get; set; }

        public IEnumerable<Attack> Attacks { get; set; }

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
                return $"{Template} {Name}".Trim();
            }
        }

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
            Saves = new Saves();
            Size = string.Empty;
            Skills = Enumerable.Empty<Skill>();
            Space = new Measurement("feet");
            SpecialQualities = Enumerable.Empty<Feat>();
            Template = string.Empty;
            Type = new CreatureType();
            Speeds = new Dictionary<string, Measurement>();
        }
    }
}