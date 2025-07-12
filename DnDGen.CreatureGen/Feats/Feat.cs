using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Feats
{
    public class Feat
    {
        public string Name { get; set; }
        public IEnumerable<string> Foci { get; set; }
        public int Power { get; set; }
        public Frequency Frequency { get; set; }
        public bool CanBeTakenMultipleTimes { get; set; }
        public SaveDieCheck Save { get; set; }

        public Feat()
        {
            Name = string.Empty;
            Foci = [];
            Frequency = new Frequency();
        }

        public string GetSummary()
        {
            var summary = Name;

            if (Foci.Any())
                summary += $" ({string.Join("/", Foci.OrderBy(f => f.ToLower()))})";

            if (Power > 0)
                summary += $", Power {Power}";

            return summary;
        }

        public override string ToString() => GetSummary();

        public override bool Equals(object obj)
        {
            if (obj is not Feat || obj is null)
                return false;

            return obj.ToString() == GetSummary();
        }

        public override int GetHashCode()
        {
            return GetSummary().GetHashCode();
        }

        internal static Feat From(FeatDataSelection selection)
        {
            var feat = new Feat
            {
                Name = selection.Feat,
                Power = selection.Power,
                CanBeTakenMultipleTimes = selection.CanBeTakenMultipleTimes
            };
            feat.Frequency.Quantity = selection.FrequencyQuantity;
            feat.Frequency.TimePeriod = selection.FrequencyTimePeriod;

            return feat;
        }

        internal static Feat From(SpecialQualityDataSelection selection, Dictionary<string, Ability> abilities)
        {
            var specialQuality = new Feat
            {
                Name = selection.Feat,
                Power = selection.Power
            };
            specialQuality.Frequency.Quantity = selection.FrequencyQuantity;
            specialQuality.Frequency.TimePeriod = selection.FrequencyTimePeriod;

            if (!string.IsNullOrEmpty(selection.SaveAbility))
            {
                specialQuality.Save = new SaveDieCheck
                {
                    BaseAbility = abilities[selection.SaveAbility],
                    Save = selection.Save,
                    BaseValue = selection.SaveBaseValue
                };
            }

            return specialQuality;
        }

        internal bool FociMatch(FeatDataSelection featSelection)
        {
            return Frequency.TimePeriod == string.Empty
                && Name == featSelection.Feat
                && Power == featSelection.Power
                && Foci.Any()
                && !string.IsNullOrEmpty(featSelection.FocusType);
        }
    }
}