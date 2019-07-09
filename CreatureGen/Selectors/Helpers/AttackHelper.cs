using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;

namespace CreatureGen.Selectors.Helpers
{
    public static class AttackHelper
    {
        public static string[] BuildData(string name, string damage, bool isMelee, bool isNatural, bool isPrimary, bool isSpecial)
        {
            var data = DataIndexConstants.AttackData.InitializeData();

            data[DataIndexConstants.AttackData.NameIndex] = name;
            data[DataIndexConstants.AttackData.DamageIndex] = damage;
            data[DataIndexConstants.AttackData.IsMeleeIndex] = isMelee.ToString();
            data[DataIndexConstants.AttackData.IsNaturalIndex] = isNatural.ToString();
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = isPrimary.ToString();
            data[DataIndexConstants.AttackData.IsSpecialIndex] = isSpecial.ToString();

            return data;
        }

        public static string BuildData(string[] data)
        {
            return string.Join(AttackSelection.Divider.ToString(), data);
        }

        public static string[] ParseData(string input)
        {
            return input.Split(AttackSelection.Divider);
        }
    }
}
