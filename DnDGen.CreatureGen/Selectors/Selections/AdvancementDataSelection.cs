using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AdvancementDataSelection : DataSelection<AdvancementDataSelection>
    {
        public int AdditionalHitDice { get; set; }
        public string Size { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int StrengthAdjustment { get; set; }
        public int DexterityAdjustment { get; set; }
        public int ConstitutionAdjustment { get; set; }
        public int NaturalArmorAdjustment { get; set; }
        public string AdjustedChallengeRating { get; set; }
        public int CasterLevelAdjustment { get; set; }

        public override Func<string[], AdvancementDataSelection> MapTo => Map;
        public override Func<AdvancementDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 3;

        public static AdvancementDataSelection Map(string[] splitData)
        {
            var selection = new AdvancementDataSelection
            {
                Size = splitData[DataIndexConstants.AdvancementSelectionData.Size],
                Space = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.Space]),
                Reach = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.Reach]),
            };

            return selection;
        }

        public static string[] Map(AdvancementDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AdvancementSelectionData.Size] = selection.Size;
            data[DataIndexConstants.AdvancementSelectionData.Space] = selection.Space.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Reach] = selection.Reach.ToString();

            return data;
        }

        public AdvancementDataSelection()
        {
            Size = string.Empty;
            AdjustedChallengeRating = string.Empty;
        }
    }
}
