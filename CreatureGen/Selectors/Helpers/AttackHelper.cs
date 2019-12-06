using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;

namespace CreatureGen.Selectors.Helpers
{
    public class AttackHelper : DataHelper
    {
        public AttackHelper()
            : base(AttackSelection.Divider)
        { }

        public string[] BuildData(
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
            int saveDcBonus = 0)
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
            data[DataIndexConstants.AttackData.AttackTypeIndex] = attackType;
            data[DataIndexConstants.AttackData.SaveDcBonusIndex] = saveDcBonus.ToString();

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature, data[DataIndexConstants.AttackData.NameIndex], data[DataIndexConstants.AttackData.IsPrimaryIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            return data.Length > DataIndexConstants.AttackData.NameIndex
                && data.Length > DataIndexConstants.AttackData.IsPrimaryIndex;
        }
    }
}
