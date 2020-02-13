using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AttackSelection
    {
        public const char Divider = '@';

        public string DamageRoll { get; set; }
        public string DamageEffect { get; set; }
        public double DamageBonusMultiplier { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsMelee { get; set; }
        public bool IsNatural { get; set; }
        public bool IsSpecial { get; set; }
        public int FrequencyQuantity { get; set; }
        public string FrequencyTimePeriod { get; set; }
        public string Save { get; set; }
        public string SaveAbility { get; set; }
        public string AttackType { get; set; }
        public int SaveDcBonus { get; set; }

        public AttackSelection()
        {
            DamageRoll = string.Empty;
            DamageEffect = string.Empty;
            Name = string.Empty;
        }

        public static AttackSelection From(string rawData)
        {
            var helper = new AttackHelper();
            var data = helper.ParseEntry(rawData);

            var selection = new AttackSelection();
            selection.IsMelee = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsMeleeIndex]);
            selection.IsNatural = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsNaturalIndex]);
            selection.IsPrimary = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsPrimaryIndex]);
            selection.IsSpecial = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsSpecialIndex]);
            selection.Name = data[DataIndexConstants.AttackData.NameIndex];
            selection.DamageRoll = data[DataIndexConstants.AttackData.DamageRollIndex];
            selection.DamageEffect = data[DataIndexConstants.AttackData.DamageEffectIndex];
            selection.DamageBonusMultiplier = Convert.ToDouble(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex]);
            selection.FrequencyQuantity = Convert.ToInt32(data[DataIndexConstants.AttackData.FrequencyQuantityIndex]);
            selection.FrequencyTimePeriod = data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex];
            selection.Save = data[DataIndexConstants.AttackData.SaveIndex];
            selection.SaveAbility = data[DataIndexConstants.AttackData.SaveAbilityIndex];
            selection.AttackType = data[DataIndexConstants.AttackData.AttackTypeIndex];
            selection.SaveDcBonus = Convert.ToInt32(data[DataIndexConstants.AttackData.SaveDcBonusIndex]);

            return selection;
        }
    }
}
