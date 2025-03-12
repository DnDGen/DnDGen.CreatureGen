using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using System;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    [Obsolete("Use Infrastructure DataHelper instead")]
    public class AttackHelper : DataHelper
    {
        public AttackHelper()
            : base(AttackDataSelection.Divider)
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
    }
}
