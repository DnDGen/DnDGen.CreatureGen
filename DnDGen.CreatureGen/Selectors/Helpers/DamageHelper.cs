using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public class DamageHelper : DataHelper
    {
        public DamageHelper()
            : base(AttackSelection.DamageDivider)
        { }

        public string[] BuildData(string roll, string type)
        {
            var data = DataIndexConstants.AttackData.DamageData.InitializeData();

            data[DataIndexConstants.AttackData.DamageData.RollIndex] = roll;
            data[DataIndexConstants.AttackData.DamageData.TypeIndex] = type;

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.AttackData.DamageData.RollIndex],
                data[DataIndexConstants.AttackData.DamageData.TypeIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            var init = DataIndexConstants.AttackData.DamageData.InitializeData();
            return data.Length == init.Length;
        }
    }
}
