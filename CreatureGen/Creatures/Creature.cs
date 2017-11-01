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
        public Measurement LandSpeed { get; set; }
        public Measurement AerialSpeed { get; set; }
        public Measurement SwimSpeed { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public int BaseAttackBonus { get; set; }
        public int GrappleBonus { get; set; }
        public IEnumerable<Attack> FullMeleeAttack { get; set; }
        public IEnumerable<Attack> FullRangedAttack { get; set; }
        public Measurement Space { get; set; }
        public Measurement Reach { get; set; }
        public IEnumerable<Attack> SpecialAttacks { get; set; }
        public IEnumerable<Feat> SpecialQualities { get; set; }
        public Saves Saves { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public string ChallengeRating { get; set; }
        public Alignment Alignment { get; set; }
        public int LevelAdjustment { get; set; }

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

        public IEnumerable<Attack> Attacks
        {
            get
            {
                return FullMeleeAttack.Union(FullRangedAttack).Union(SpecialAttacks);
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
            AerialSpeed = new Measurement("feet per round");
            Alignment = new Alignment();
            ArmorClass = new ArmorClass();
            ChallengeRating = string.Empty;
            Feats = Enumerable.Empty<Feat>();
            FullMeleeAttack = Enumerable.Empty<Attack>();
            FullRangedAttack = Enumerable.Empty<Attack>();
            LandSpeed = new Measurement("feet per round");
            Reach = new Measurement("feet");
            Saves = new Saves();
            Skills = Enumerable.Empty<Skill>();
            Space = new Measurement("feet");
            SpecialAttacks = Enumerable.Empty<Attack>();
            SpecialQualities = Enumerable.Empty<Feat>();
            SwimSpeed = new Measurement("feet per round");
            Type = new CreatureType();
        }
    }
}