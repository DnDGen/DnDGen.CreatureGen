using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public class AttackHelper : DataHelper
    {
        public AttackHelper()
            : base(AttackSelection.Divider)
        { }

        public string[] BuildData(
            string name,
            string damageData,
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
            int saveDcBonus = 0,
            string requiredGender = null)
        {
            var data = DataIndexConstants.AttackData.InitializeData();

            data[DataIndexConstants.AttackData.NameIndex] = name;
            data[DataIndexConstants.AttackData.DamageDataIndex] = damageData;
            data[DataIndexConstants.AttackData.DamageEffectIndex] = damageEffect;
            data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = damageBonusMultiplier.ToString();
            data[DataIndexConstants.AttackData.IsMeleeIndex] = isMelee.ToString();
            data[DataIndexConstants.AttackData.IsNaturalIndex] = isNatural.ToString();
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = isPrimary.ToString();
            data[DataIndexConstants.AttackData.IsSpecialIndex] = isSpecial.ToString();
            data[DataIndexConstants.AttackData.FrequencyQuantityIndex] = frequencyQuantity.ToString();
            data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.AttackData.SaveIndex] = save ?? string.Empty;
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = saveAbility ?? string.Empty;
            data[DataIndexConstants.AttackData.AttackTypeIndex] = attackType;
            data[DataIndexConstants.AttackData.SaveDcBonusIndex] = saveDcBonus.ToString();
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = requiredGender ?? string.Empty;

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.AttackData.NameIndex],
                data[DataIndexConstants.AttackData.IsPrimaryIndex],
                data[DataIndexConstants.AttackData.DamageDataIndex],
                data[DataIndexConstants.AttackData.DamageEffectIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            var init = DataIndexConstants.AttackData.InitializeData();
            return data.Length == init.Length;
        }
    }
}
