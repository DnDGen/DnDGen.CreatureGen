using CreatureGen.Abilities;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Defenses
{
    public class Save
    {
        public Ability BaseAbility { get; set; }
        public int BaseValue { get; set; }
        public IEnumerable<Bonus> Bonuses { get; private set; }

        public bool CircumstantialBonus => Bonuses.Any(b => b.IsConditional);
        public bool HasSave => BaseAbility != null;

        public int Bonus
        {
            get
            {
                return Bonuses
                    .Where(b => !b.IsConditional)
                    .Select(b => b.Value)
                    .Sum();
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

        public void AddBonus(int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            Bonuses = Bonuses.Union(new[] { bonus });
        }
    }
}
