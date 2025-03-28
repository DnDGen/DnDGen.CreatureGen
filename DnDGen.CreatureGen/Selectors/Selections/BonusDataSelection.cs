using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.RollGen;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class BonusDataSelection : DataSelection<BonusDataSelection>
    {
        public string Target { get; set; }
        public string BonusRoll { get; set; }
        public int Bonus { get; private set; }
        public string Condition { get; set; }

        public override Func<string[], BonusDataSelection> MapTo => Map;
        public override Func<BonusDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 3;

        public BonusDataSelection()
        {
            BonusRoll = string.Empty;
            Target = string.Empty;
            Condition = string.Empty;
        }

        public static BonusDataSelection Map(string[] splitData)
        {
            return new BonusDataSelection
            {
                BonusRoll = splitData[DataIndexConstants.BonusData.BonusRollIndex],
                Target = splitData[DataIndexConstants.BonusData.TargetIndex],
                Condition = splitData[DataIndexConstants.BonusData.ConditionIndex],
            };
        }

        public static string[] Map(BonusDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusRollIndex] = selection.BonusRoll;
            data[DataIndexConstants.BonusData.TargetIndex] = selection.Target;
            data[DataIndexConstants.BonusData.ConditionIndex] = selection.Condition;

            return data;
        }

        public void SetAdditionalProperties(Dice dice)
        {
            Bonus = dice.Roll(BonusRoll).AsSum();
        }
    }
}
