using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AttackDataSelection : DataSelection<AttackDataSelection>
    {
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
        public override Func<string[], AttackDataSelection> MapTo => Map;
        public override Func<AttackDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 10;

        public static AttackDataSelection Map(string[] splitData)
        {
            var selection = new AttackDataSelection
            {
                IsMelee = Convert.ToBoolean(splitData[DataIndexConstants.AttackData.IsMeleeIndex]),
                IsNatural = Convert.ToBoolean(splitData[DataIndexConstants.AttackData.IsNaturalIndex]),
                IsPrimary = Convert.ToBoolean(splitData[DataIndexConstants.AttackData.IsPrimaryIndex]),
                IsSpecial = Convert.ToBoolean(splitData[DataIndexConstants.AttackData.IsSpecialIndex]),
                Name = splitData[DataIndexConstants.AttackData.NameIndex],
                DamageEffect = splitData[DataIndexConstants.AttackData.DamageEffectIndex],
                DamageBonusMultiplier = Convert.ToDouble(splitData[DataIndexConstants.AttackData.DamageBonusMultiplierIndex]),
                FrequencyQuantity = Convert.ToInt32(splitData[DataIndexConstants.AttackData.FrequencyQuantityIndex]),
                FrequencyTimePeriod = splitData[DataIndexConstants.AttackData.FrequencyTimePeriodIndex],
                Save = splitData[DataIndexConstants.AttackData.SaveIndex],
                SaveAbility = splitData[DataIndexConstants.AttackData.SaveAbilityIndex],
                AttackType = splitData[DataIndexConstants.AttackData.AttackTypeIndex],
                SaveDcBonus = Convert.ToInt32(splitData[DataIndexConstants.AttackData.SaveDcBonusIndex]),
                RequiredGender = splitData[DataIndexConstants.AttackData.RequiredGenderIndex],
            };

            return selection;
        }

        public static string[] Map(AttackDataSelection selection)
        {
            var data = new string[selection.SectionCount];
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

        public AttackDataSelection()
        {
            Damages = [];
            DamageEffect = string.Empty;
            Name = string.Empty;
        }

        public bool RequirementsMet(string gender) => string.IsNullOrEmpty(RequiredGender) || RequiredGender == gender;

        public static AttackDataSelection From(string rawData)
        {
            var helper = new AttackHelper();
            var data = helper.ParseEntry(rawData);

            var selection = new AttackDataSelection
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

        public string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.AttackData.NameIndex],
                data[DataIndexConstants.AttackData.IsPrimaryIndex],
                data[DataIndexConstants.AttackData.DamageDataIndex],
                data[DataIndexConstants.AttackData.DamageEffectIndex]);
        }

        public string BuildKeyFromSections(string creature, params string[] keySections)
        {
            var key = creature;
            foreach (var section in keySections)
                key += section;

            return key;
        }
    }
}
