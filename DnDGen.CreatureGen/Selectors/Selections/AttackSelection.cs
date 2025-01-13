using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AttackSelection
    {
        public const char Divider = '@';
        public const char DamageDivider = '#';
        public const char DamageSplitDivider = ',';

        public List<Damage> Damages { get; set; }
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
        public string RequiredGender { get; set; }

        public AttackSelection()
        {
            Damages = [];
            DamageEffect = string.Empty;
            Name = string.Empty;
        }

        public bool RequirementsMet(string gender) => string.IsNullOrEmpty(RequiredGender) || RequiredGender == gender;

        public static AttackSelection From(string rawData)
        {
            var helper = new AttackHelper();
            var data = helper.ParseEntry(rawData);

            var selection = new AttackSelection
            {
                IsMelee = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsMeleeIndex]),
                IsNatural = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsNaturalIndex]),
                IsPrimary = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsPrimaryIndex]),
                IsSpecial = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsSpecialIndex]),
                Name = data[DataIndexConstants.AttackData.NameIndex],
                DamageEffect = data[DataIndexConstants.AttackData.DamageEffectIndex],
                DamageBonusMultiplier = Convert.ToDouble(data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex]),
                FrequencyQuantity = Convert.ToInt32(data[DataIndexConstants.AttackData.FrequencyQuantityIndex]),
                FrequencyTimePeriod = data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex],
                Save = data[DataIndexConstants.AttackData.SaveIndex],
                SaveAbility = data[DataIndexConstants.AttackData.SaveAbilityIndex],
                AttackType = data[DataIndexConstants.AttackData.AttackTypeIndex],
                SaveDcBonus = Convert.ToInt32(data[DataIndexConstants.AttackData.SaveDcBonusIndex]),
                RequiredGender = data[DataIndexConstants.AttackData.RequiredGenderIndex],
            };

            var damageHelper = new DamageHelper();
            var damageEntries = damageHelper.ParseEntries(data[DataIndexConstants.AttackData.DamageDataIndex]);

            foreach (var damageData in damageEntries)
            {
                var damage = new Damage
                {
                    Roll = damageData[DataIndexConstants.AttackData.DamageData.RollIndex],
                    Type = damageData[DataIndexConstants.AttackData.DamageData.TypeIndex],
                    Condition = damageData[DataIndexConstants.AttackData.DamageData.ConditionIndex],
                };

                selection.Damages.Add(damage);
            }

            return selection;
        }
    }
}
