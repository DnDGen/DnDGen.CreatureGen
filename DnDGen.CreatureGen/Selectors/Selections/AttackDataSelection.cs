using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using DnDGen.Infrastructure.Models;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AttackDataSelection : DataSelection<AttackDataSelection>
    {
        public DamageDataSelection Damage { get; set; }
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

        public override int SectionCount => 14;

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
                Damage = DataHelper.Parse<DamageDataSelection>(splitData[DataIndexConstants.AttackData.DamageIndex]),
            };

            return selection;
        }

        public static string[] Map(AttackDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.NameIndex] = selection.Name;
            data[DataIndexConstants.AttackData.DamageEffectIndex] = selection.DamageEffect;
            data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = selection.DamageBonusMultiplier.ToString();
            data[DataIndexConstants.AttackData.IsMeleeIndex] = selection.IsMelee.ToString();
            data[DataIndexConstants.AttackData.IsNaturalIndex] = selection.IsNatural.ToString();
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = selection.IsPrimary.ToString();
            data[DataIndexConstants.AttackData.IsSpecialIndex] = selection.IsSpecial.ToString();
            data[DataIndexConstants.AttackData.FrequencyQuantityIndex] = selection.FrequencyQuantity.ToString();
            data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = selection.FrequencyTimePeriod;
            data[DataIndexConstants.AttackData.SaveIndex] = selection.Save ?? string.Empty;
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = selection.SaveAbility ?? string.Empty;
            data[DataIndexConstants.AttackData.AttackTypeIndex] = selection.AttackType;
            data[DataIndexConstants.AttackData.SaveDcBonusIndex] = selection.SaveDcBonus.ToString();
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = selection.RequiredGender ?? string.Empty;

            if (selection.Damage != null)
                data[DataIndexConstants.AttackData.DamageIndex] = DataHelper.Parse(selection.Damage);
            else
                data[DataIndexConstants.AttackData.DamageIndex] = string.Empty;

            return data;
        }

        public AttackDataSelection()
        {
            DamageEffect = string.Empty;
            Name = string.Empty;
        }

        public bool RequirementsMet(string gender) => string.IsNullOrEmpty(RequiredGender) || RequiredGender == gender;

        private static string BuildKeyFromSections(params string[] keySections) => string.Join(string.Empty, keySections);

        public string BuildDamageKey(string creature, string size)
        {
            var data = MapFrom(this);
            return BuildKeyFromSections(creature,
                size,
                data[DataIndexConstants.AttackData.NameIndex],
                data[DataIndexConstants.AttackData.IsPrimaryIndex],
                data[DataIndexConstants.AttackData.DamageEffectIndex]);
        }
    }
}
