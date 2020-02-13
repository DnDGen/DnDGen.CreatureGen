using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class SpecialQualitySelection
    {
        public const char Divider = '#';

        public string Feat { get; set; }
        public Frequency Frequency { get; set; }
        public string FocusType { get; set; }
        public int Power { get; set; }
        public Dictionary<string, int> MinimumAbilities { get; set; }
        public string RandomFociQuantity { get; set; }
        public IEnumerable<RequiredFeatSelection> RequiredFeats { get; set; }
        public IEnumerable<string> RequiredSizes { get; set; }
        public IEnumerable<string> RequiredAlignments { get; set; }
        public bool RequiresEquipment { get; set; }
        public string SaveAbility { get; set; }
        public string Save { get; set; }
        public int SaveBaseValue { get; set; }

        public SpecialQualitySelection()
        {
            Feat = string.Empty;
            Frequency = new Frequency();
            FocusType = string.Empty;
            MinimumAbilities = new Dictionary<string, int>();
            RandomFociQuantity = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeatSelection>();
            RequiredSizes = Enumerable.Empty<string>();
            RequiredAlignments = Enumerable.Empty<string>();
            SaveAbility = string.Empty;
            Save = string.Empty;
        }

        public bool RequirementsMet(Dictionary<string, Ability> abilities, IEnumerable<Feat> feats, bool canUseEquipment, string size, Alignment alignment)
        {
            if (!MinimumAbilityMet(abilities))
                return false;

            foreach (var requirement in RequiredFeats)
            {
                var requirementFeats = feats.Where(f => f.Name == requirement.Feat);

                if (!requirementFeats.Any())
                    return false;

                if (requirement.Foci.Any() && !requirementFeats.Any(f => f.Foci.Intersect(requirement.Foci).Any()))
                    return false;
            }

            if (RequiredSizes.Any() && !RequiredSizes.Contains(size))
                return false;

            if (RequiredAlignments.Any() && !RequiredAlignments.Contains(alignment.Full))
                return false;

            return !RequiresEquipment || canUseEquipment;
        }

        private bool MinimumAbilityMet(Dictionary<string, Ability> abilities)
        {
            if (!MinimumAbilities.Any())
                return true;

            foreach (var minimumAbility in MinimumAbilities)
                if (abilities[minimumAbility.Key].FullScore >= minimumAbility.Value)
                    return true;

            return false;
        }
    }
}