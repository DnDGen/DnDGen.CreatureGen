using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class CreatureDataSelection : DataSelection<CreatureDataSelection>
    {
        public string Size { get; set; }
        public string ChallengeRating { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }
        public int NumberOfHands { get; set; }
        public int NaturalArmor { get; set; }
        public bool CanUseEquipment { get; set; }

        public CreatureDataSelection()
        {
            Size = string.Empty;
            ChallengeRating = string.Empty;
        }

        public override Func<string[], CreatureDataSelection> MapTo => Map;
        public override Func<CreatureDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 9;

        public static CreatureDataSelection Map(string[] splitData)
        {
            return new CreatureDataSelection
            {
                Size = splitData[DataIndexConstants.CreatureData.Size],
                ChallengeRating = splitData[DataIndexConstants.CreatureData.ChallengeRating],
                Space = splitData[DataIndexConstants.CreatureData.Space],
                Reach = splitData[DataIndexConstants.CreatureData.Reach],
                LevelAdjustment = splitData[DataIndexConstants.CreatureData.LevelAdjustment],
                CasterLevel = splitData[DataIndexConstants.CreatureData.CasterLevel],
                NumberOfHands = splitData[DataIndexConstants.CreatureData.NumberOfHands],
                NaturalArmor = splitData[DataIndexConstants.CreatureData.NaturalArmor],
                CanUseEquipment = splitData[DataIndexConstants.CreatureData.CanUseEquipment],
            };
        }

        public static string[] Map(CreatureDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusIndex] = selection.Bonus.ToString();
            data[DataIndexConstants.BonusData.TargetIndex] = selection.Target;
            data[DataIndexConstants.BonusData.ConditionIndex] = selection.Condition;

            return data;
        }
    }
}
