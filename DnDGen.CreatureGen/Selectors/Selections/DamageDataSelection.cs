using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.TreasureGen.Items;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class DamageDataSelection : DataSelection<DamageDataSelection>
    {
        public string Roll { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }

        public override Func<string[], DamageDataSelection> MapTo => Map;
        public override Func<DamageDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 3;

        public DamageDataSelection()
        {
            Roll = string.Empty;
            Type = string.Empty;
            Condition = string.Empty;
        }

        public static DamageDataSelection Map(string[] splitData)
        {
            return new DamageDataSelection
            {
                Roll = splitData[DataIndexConstants.AttackData.DamageData.RollIndex],
                Type = splitData[DataIndexConstants.AttackData.DamageData.TypeIndex],
                Condition = splitData[DataIndexConstants.AttackData.DamageData.ConditionIndex],
            };
        }

        public static string[] Map(DamageDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.DamageData.RollIndex] = selection.Roll;
            data[DataIndexConstants.AttackData.DamageData.TypeIndex] = selection.Type;
            data[DataIndexConstants.AttackData.DamageData.ConditionIndex] = selection.Condition;

            return data;
        }

        internal Damage To()
        {
            return new Damage
            {
                Roll = Roll,
                Type = Type,
                Condition = Condition
            };
        }
    }
}
