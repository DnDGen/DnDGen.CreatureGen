using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Selections
{
    internal class FeatSelection
    {
        public string Feat { get; set; }
        public Frequency Frequency { get; set; }
        public int Power { get; set; }
        public IEnumerable<RequiredFeatSelection> RequiredFeats { get; set; }
        public int RequiredBaseAttack { get; set; }
        public Dictionary<string, int> RequiredAbilities { get; set; }
        public Dictionary<string, int> RequiredSpeeds { get; set; }
        public IEnumerable<RequiredSkillSelection> RequiredSkills { get; set; }
        public string FocusType { get; set; }
        public bool CanBeTakenMultipleTimes { get; set; }
        public int MinimumCasterLevel { get; set; }
        public bool RequiresNaturalArmor { get; set; }
        public bool RequiresSpecialAttack { get; set; }
        public bool RequiresSpellLikeAbility { get; set; }
        public int RequiredNaturalWeapons { get; set; }
        public int RequiredHands { get; set; }
        public IEnumerable<string> RequiredSizes { get; set; }

        public FeatSelection()
        {
            Feat = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeatSelection>();
            RequiredAbilities = new Dictionary<string, int>();
            RequiredSpeeds = new Dictionary<string, int>();
            RequiredSkills = Enumerable.Empty<RequiredSkillSelection>();
            FocusType = string.Empty;
            Frequency = new Frequency();
            RequiredSizes = Enumerable.Empty<string>();
        }

        public bool ImmutableRequirementsMet(
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size)
        {
            foreach (var requiredAbility in RequiredAbilities)
                if (abilities[requiredAbility.Key].FullScore < requiredAbility.Value)
                    return false;

            if (baseAttackBonus < RequiredBaseAttack)
                return false;

            if (casterLevel < MinimumCasterLevel)
                return false;

            if (hands < RequiredHands)
                return false;

            if (RequiredSkills.Any() && !RequiredSkills.All(s => s.RequirementMet(skills)))
                return false;

            if (RequiresSpecialAttack && !attacks.Any(a => a.IsSpecial))
                return false;

            if (attacks.Count(a => a.IsNatural) < RequiredNaturalWeapons)
                return false;

            if (RequiresNaturalArmor && naturalArmor <= 0)
                return false;

            if (RequiredSizes.Any() && !RequiredSizes.Contains(size))
                return false;

            foreach (var requiredSpeed in RequiredSpeeds)
                if (!speeds.ContainsKey(requiredSpeed.Key) || speeds[requiredSpeed.Key].Value < requiredSpeed.Value)
                    return false;

            return true;
        }

        public bool MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            if (FeatSelected(feats) && !CanBeTakenMultipleTimes)
                return false;

            if (RequiresSpellLikeAbility && !feats.Any(f => f.Name == FeatConstants.SpecialQualities.SpellLikeAbility))
                return false;

            if (!RequiredFeats.Any())
                return true;

            return RequiredFeats.All(f => f.RequirementMet(feats));
        }

        private bool FeatSelected(IEnumerable<Feat> feats)
        {
            return feats.Any(f => FeatSelected(f));
        }

        private bool FeatSelected(Feat feat)
        {
            return feat.Name == Feat && FocusSelected(feat);
        }

        private bool FocusSelected(Feat feat)
        {
            return string.IsNullOrEmpty(FocusType) || feat.Foci.Contains(FeatConstants.Foci.All);
        }
    }
}
