using CreatureGen.Abilities;
using CreatureGen.Attacks;
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
        public IEnumerable<RequiredSkillSelection> RequiredSkills { get; set; }
        public string FocusType { get; set; }
        public bool CanBeTakenMultipleTimes { get; set; }
        public int MinimumCasterLevel { get; set; }

        public FeatSelection()
        {
            Feat = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeatSelection>();
            RequiredAbilities = new Dictionary<string, int>();
            RequiredSkills = Enumerable.Empty<RequiredSkillSelection>();
            FocusType = string.Empty;
            Frequency = new Frequency();
        }

        public bool ImmutableRequirementsMet(int baseAttackBonus, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, IEnumerable<Attack> attack, int casterLevel)
        {
            foreach (var requiredAbility in RequiredAbilities)
                if (abilities[requiredAbility.Key].FullScore < requiredAbility.Value)
                    return false;

            if (baseAttackBonus < RequiredBaseAttack || casterLevel < MinimumCasterLevel)
                return false;

            return !RequiredSkills.Any() || RequiredSkills.Any(s => s.RequirementMet(skills));
        }

        public bool MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            if (FeatSelected(feats) && !CanBeTakenMultipleTimes)
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
