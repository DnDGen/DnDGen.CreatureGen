using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;

namespace CreatureGen.Selectors.Helpers
{
    public static class AttackHelper
    {
        public static string[] BuildData(
            string name,
            string damageRoll,
            string damageEffect,
            double damageBonusMultiplier,
            string attackType,
            int frequencyQuantity,
            string frequencyTimePeriod,
            bool isMelee,
            bool isNatural,
            bool isPrimary,
            bool isSpecial,
            string save = null,
            string saveAbility = null,
            int baseSave = 0)
        {
            var data = DataIndexConstants.AttackData.InitializeData();

            data[DataIndexConstants.AttackData.NameIndex] = name;
            data[DataIndexConstants.AttackData.DamageRollIndex] = damageRoll;
            data[DataIndexConstants.AttackData.DamageEffectIndex] = damageEffect;
            data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = damageBonusMultiplier.ToString();
            data[DataIndexConstants.AttackData.IsMeleeIndex] = isMelee.ToString();
            data[DataIndexConstants.AttackData.IsNaturalIndex] = isNatural.ToString();
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = isPrimary.ToString();
            data[DataIndexConstants.AttackData.IsSpecialIndex] = isSpecial.ToString();
            data[DataIndexConstants.AttackData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.AttackData.SaveIndex] = save;
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = saveAbility;
            data[DataIndexConstants.AttackData.BaseSaveIndex] = baseSave.ToString();
            data[DataIndexConstants.AttackData.AttackTypeIndex] = attackType;

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
