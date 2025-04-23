using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class BonusDataSelection : DataSelection<BonusDataSelection>
    {
        public string Target { get; set; }
        public int Bonus { get; set; }
        public string Condition { get; set; }

        public override Func<string[], BonusDataSelection> MapTo => Map;
        public override Func<BonusDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 3;

        public BonusDataSelection()
        {
            Target = string.Empty;
            Condition = string.Empty;
        }

        public static BonusDataSelection Map(string[] splitData)
        {
            return new BonusDataSelection
            {
                Bonus = Convert.ToInt32(splitData[DataIndexConstants.BonusData.BonusIndex]),
                Target = splitData[DataIndexConstants.BonusData.TargetIndex],
                Condition = splitData[DataIndexConstants.BonusData.ConditionIndex] ?? string.Empty,
            };
        }

        public static string[] Map(BonusDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusIndex] = selection.Bonus.ToString();
            data[DataIndexConstants.BonusData.TargetIndex] = selection.Target;
            data[DataIndexConstants.BonusData.ConditionIndex] = selection.Condition ?? string.Empty;

            return data;
        }
    }
}
