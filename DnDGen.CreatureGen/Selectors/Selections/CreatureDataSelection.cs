using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class CreatureDataSelection : DataSelection<CreatureDataSelection>
    {
        public string Size { get; set; }
        public string ChallengeRating { private get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }
        public int NumberOfHands { get; set; }
        public int NaturalArmor { get; set; }
        public bool CanUseEquipment { get; set; }
        public BaseAttackQuality BaseAttackQuality { get; set; }
        public IEnumerable<string> Types { get; set; }
        public double HitDiceQuantity { private get; set; }
        public int HitDie { get; set; }
        public bool HasSkeleton { get; set; }

        public CreatureDataSelection()
        {
            Size = string.Empty;
            ChallengeRating = string.Empty;
            Types = [];
        }

        public override Func<string[], CreatureDataSelection> MapTo => Map;
        public override Func<CreatureDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 14;
        public static char Delimiter => '|';

        public static CreatureDataSelection Map(string[] splitData)
        {
            var selection = new CreatureDataSelection
            {
                Size = splitData[DataIndexConstants.CreatureData.Size],
                ChallengeRating = splitData[DataIndexConstants.CreatureData.ChallengeRating],
                Space = Convert.ToDouble(splitData[DataIndexConstants.CreatureData.Space]),
                Reach = Convert.ToDouble(splitData[DataIndexConstants.CreatureData.Reach]),
                CasterLevel = Convert.ToInt32(splitData[DataIndexConstants.CreatureData.CasterLevel]),
                NumberOfHands = Convert.ToInt32(splitData[DataIndexConstants.CreatureData.NumberOfHands]),
                NaturalArmor = Convert.ToInt32(splitData[DataIndexConstants.CreatureData.NaturalArmor]),
                CanUseEquipment = Convert.ToBoolean(splitData[DataIndexConstants.CreatureData.CanUseEquipment]),
                BaseAttackQuality = (BaseAttackQuality)Convert.ToInt32(splitData[DataIndexConstants.CreatureData.BaseAttackQuality]),
                Types = splitData[DataIndexConstants.CreatureData.Types].Split(Delimiter),
                HitDiceQuantity = Convert.ToDouble(splitData[DataIndexConstants.CreatureData.HitDiceQuantity]),
                HitDie = Convert.ToInt32(splitData[DataIndexConstants.CreatureData.HitDie]),
                HasSkeleton = Convert.ToBoolean(splitData[DataIndexConstants.CreatureData.HasSkeleton]),
            };

            if (string.IsNullOrEmpty(splitData[DataIndexConstants.CreatureData.LevelAdjustment]))
            {
                selection.LevelAdjustment = null;
            }
            else
            {
                selection.LevelAdjustment = Convert.ToInt32(splitData[DataIndexConstants.CreatureData.LevelAdjustment]);
            }

            return selection;
        }

        public static string[] Map(CreatureDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.Size] = selection.Size;
            data[DataIndexConstants.CreatureData.ChallengeRating] = selection.ChallengeRating;
            data[DataIndexConstants.CreatureData.Space] = selection.Space.ToString();
            data[DataIndexConstants.CreatureData.Reach] = selection.Reach.ToString();
            data[DataIndexConstants.CreatureData.CasterLevel] = selection.CasterLevel.ToString();
            data[DataIndexConstants.CreatureData.NumberOfHands] = selection.NumberOfHands.ToString();
            data[DataIndexConstants.CreatureData.NaturalArmor] = selection.NaturalArmor.ToString();
            data[DataIndexConstants.CreatureData.CanUseEquipment] = selection.CanUseEquipment.ToString();
            data[DataIndexConstants.CreatureData.LevelAdjustment] = selection.LevelAdjustment?.ToString() ?? string.Empty;
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = ((int)selection.BaseAttackQuality).ToString();
            data[DataIndexConstants.CreatureData.Types] = string.Join(Delimiter, selection.Types);
            data[DataIndexConstants.CreatureData.HitDiceQuantity] = selection.HitDiceQuantity.ToString();
            data[DataIndexConstants.CreatureData.HitDie] = selection.HitDie.ToString();
            data[DataIndexConstants.CreatureData.HasSkeleton] = selection.HasSkeleton.ToString();

            return data;
        }

        public double GetEffectiveHitDiceQuantity(bool asCharacter)
        {
            if (asCharacter && HitDiceQuantity <= 1 && Types.First() == CreatureConstants.Types.Humanoid)
            {
                return 0;
            }

            return HitDiceQuantity;
        }

        public string GetEffectiveChallengeRating(bool asCharacter)
        {
            var hitDiceQuantity = GetEffectiveHitDiceQuantity(asCharacter);
            if (hitDiceQuantity == 0)
            {
                return ChallengeRatingConstants.CR0;
            }

            return ChallengeRating;
        }
    }
}
