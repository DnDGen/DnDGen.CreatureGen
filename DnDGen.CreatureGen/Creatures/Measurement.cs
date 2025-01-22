using System.Collections.Generic;

namespace DnDGen.CreatureGen.Creatures
{
    public class Measurement
    {
        public double Value { get; set; }
        public string Description { get; set; }
        public List<Bonus> Bonuses { get; private set; }

        public readonly string Unit;

        public Measurement(string unit)
        {
            Description = string.Empty;
            Unit = unit;
            Bonuses = [];
        }

        public void AddBonus(int value, string condition)
        {
            Bonuses.Add(new Bonus { Value = value, Condition = condition });
        }
    }
}
