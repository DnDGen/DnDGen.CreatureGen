using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public class DamageHelper : DataHelper
    {
        public DamageHelper()
            : base(AttackSelection.DamageDivider)
        { }

        public string[] BuildData(string roll, string type, string condition = "")
        {
            var data = DataIndexConstants.AttackData.DamageData.InitializeData();

            data[DataIndexConstants.AttackData.DamageData.RollIndex] = roll;
            data[DataIndexConstants.AttackData.DamageData.TypeIndex] = type;
            data[DataIndexConstants.AttackData.DamageData.ConditionIndex] = condition;

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.AttackData.DamageData.RollIndex],
                data[DataIndexConstants.AttackData.DamageData.TypeIndex],
                data[DataIndexConstants.AttackData.DamageData.ConditionIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            var init = DataIndexConstants.AttackData.DamageData.InitializeData();
            return data.Length == init.Length;
        }

        public string BuildEntries(params string[] data)
        {
            var entries = new List<string>();
            var init = DataIndexConstants.AttackData.DamageData.InitializeData();

            for (var i = 0; i < data.Length; i += init.Length)
            {
                var subData = data.Skip(i).Take(init.Length).ToArray();
                if (subData.Length < init.Length)
                {
                    var empty = new string[init.Length - subData.Length];
                    subData = subData.Concat(empty).ToArray();
                }

                var entry = BuildEntry(subData);
                entries.Add(entry);
            }

            return string.Join(AttackSelection.DamageSplitDivider.ToString(), entries);
        }

        public string[][] ParseEntries(string entry)
        {
            if (string.IsNullOrEmpty(entry))
            {
                return new string[0][];
            }

            var entries = entry.Split(AttackSelection.DamageSplitDivider);
            var data = new string[entries.Length][];

            for (var i = 0; i < entries.Length; i++)
            {
                data[i] = ParseEntry(entries[i]);
            }

            return data;
        }

        public bool ValidateEntries(string entry)
        {
            if (string.IsNullOrEmpty(entry))
            {
                return true;
            }

            var entries = entry.Split(AttackSelection.DamageSplitDivider);
            var valid = true;

            foreach (var subEntry in entries)
            {
                valid &= ValidateEntry(subEntry);
            }

            return valid;
        }
    }
}
