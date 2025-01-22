using DnDGen.CreatureGen.Attacks;
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

        public Feat Clone()
        {
            var clone = new Feat
            {
                CanBeTakenMultipleTimes = CanBeTakenMultipleTimes,
                Foci = Foci.ToArray()
            };
            clone.Frequency.Quantity = Frequency.Quantity;
            clone.Frequency.TimePeriod = Frequency.TimePeriod;
            clone.Name = Name;
            clone.Power = Power;

            return clone;
        }

        public string GetSummary()
        {
            var summary = Name;

            if (Foci.Any())
                summary += $" ({string.Join("/", Foci)})";

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
    }
}