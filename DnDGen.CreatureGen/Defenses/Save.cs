using DnDGen.CreatureGen.Abilities;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Defenses
{
    public class Save
    {
        public Ability BaseAbility { get; set; }
        public int BaseValue { get; set; }
        public List<Bonus> Bonuses { get; set; }

        public bool IsConditional => Bonuses.Any(b => b.IsConditional);
        public bool HasSave => BaseAbility != null && BaseAbility.HasScore;

        public int Bonus
        {
            get
            {
                return Bonuses
                    .Where(b => !b.IsConditional)
                    .Sum(b => b.Value);
            }
        }

        public int TotalBonus
        {
            get
            {
                var total = BaseValue + Bonus;

                if (HasSave)
                    total += BaseAbility.Modifier;

                return total;
            }
        }

        public Save()
        {
            Bonuses = [];
        }

        public void AddBonus(int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            Bonuses.Add(bonus);
        }
    }
}
