using DnDGen.CreatureGen.Abilities;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Defenses
{
    public class Save
    {
        public Ability BaseAbility { get; set; }
        public int BaseValue { get; set; }
        public IEnumerable<Bonus> Bonuses { get; private set; }

        public bool IsConditional => Bonuses.Any(b => b.IsConditional);
        public bool HasSave => BaseAbility != null && BaseAbility.HasScore;

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

        public Save()
        {
            Bonuses = Enumerable.Empty<Bonus>();
        }

        public void AddBonus(int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            Bonuses = Bonuses.Union(new[] { bonus });
        }
    }
}
