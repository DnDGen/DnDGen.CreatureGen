using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class FeatDataSelection : DataSelection<FeatDataSelection>
    {
        public string Feat { get; set; }
        public Frequency Frequency { get; set; }
        public int Power { get; set; }
        public IEnumerable<RequiredFeatSelection> RequiredFeats { get; set; }
        public int RequiredBaseAttack { get; set; }
        public Dictionary<string, int> RequiredAbilities { get; set; }
        public Dictionary<string, int> RequiredSpeeds { get; set; }
        public IEnumerable<RequiredSkillSelection> RequiredSkills { get; set; }
        public string FocusType { get; set; }
        public bool CanBeTakenMultipleTimes { get; set; }
        public int MinimumCasterLevel { get; set; }
        public bool RequiresNaturalArmor { get; set; }
        public bool RequiresSpecialAttack { get; set; }
        public bool RequiresSpellLikeAbility { get; set; }
        public bool RequiresEquipment { get; set; }
        public int RequiredNaturalWeapons { get; set; }
        public int RequiredHands { get; set; }
        public IEnumerable<string> RequiredSizes { get; set; }

        public override Func<string[], FeatDataSelection> MapTo => Map;
        public override Func<FeatDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 10;

        public static FeatDataSelection Map(string[] splitData)
        {
            var selection = new FeatDataSelection
            {
                AdditionalHitDiceRoll = splitData[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll],
                Size = splitData[DataIndexConstants.AdvancementSelectionData.Size],
                Space = Convert.ToDouble(splitData[DataIndexConstants.AdvancementSelectionData.Space]),
                Reach = Convert.ToDouble(splitData[DataIndexConstants.AdvancementSelectionData.Reach]),
                StrengthAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment]),
                ConstitutionAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment]),
                DexterityAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment]),
                NaturalArmorAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment]),
                ChallengeRatingDivisor = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor]),
                AdjustedChallengeRating = splitData[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating],
            };

            return selection;
        }

        public static string[] Map(FeatDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AdvancementSelectionData.Size] = selection.Size;
            data[DataIndexConstants.AdvancementSelectionData.Space] = selection.Space.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Reach] = selection.Reach.ToString();
            data[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll] = selection.AdditionalHitDiceRoll;
            data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = selection.StrengthAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = selection.ConstitutionAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = selection.DexterityAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = selection.NaturalArmorAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor] = selection.ChallengeRatingDivisor.ToString();
            data[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating] = selection.AdjustedChallengeRating;

            return data;
        }

        public FeatDataSelection()
        {
            Feat = string.Empty;
            RequiredFeats = [];
            RequiredAbilities = [];
            RequiredSpeeds = [];
            RequiredSkills = [];
            FocusType = string.Empty;
            Frequency = new Frequency();
            RequiredSizes = [];
        }

        public bool ImmutableRequirementsMet(
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size,
            bool canUseEquipment)
        {
            if (baseAttackBonus < RequiredBaseAttack)
                return false;

            if (casterLevel < MinimumCasterLevel)
                return false;

            if (hands < RequiredHands)
                return false;

            if (RequiresNaturalArmor && naturalArmor <= 0)
                return false;

            if (RequiresEquipment && !canUseEquipment)
                return false;

            if (RequiresSpecialAttack && !attacks.Any(a => a.IsSpecial))
                return false;

            if (attacks.Count(a => a.IsNatural) < RequiredNaturalWeapons)
                return false;

            if (RequiredSkills.Any() && !RequiredSkills.All(s => s.RequirementMet(skills)))
                return false;

            if (RequiredSizes.Any() && !RequiredSizes.Contains(size))
                return false;

            foreach (var requiredAbility in RequiredAbilities)
                if (abilities[requiredAbility.Key].FullScore < requiredAbility.Value)
                    return false;

            foreach (var requiredSpeed in RequiredSpeeds)
                if (!speeds.ContainsKey(requiredSpeed.Key) || speeds[requiredSpeed.Key].Value < requiredSpeed.Value)
                    return false;

            return true;
        }

        public bool MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            if (FeatSelected(feats) && !CanBeTakenMultipleTimes)
                return false;

            if (RequiresSpellLikeAbility && !feats.Any(f => f.Name == FeatConstants.SpecialQualities.SpellLikeAbility))
                return false;

            if (!RequiredFeats.Any())
                return true;

            return RequiredFeats.All(f => f.RequirementMet(feats));
        }

        private bool FeatSelected(IEnumerable<Feat> feats)
        {
            return feats.Any(f => FeatSelected(f));
        }

        private bool FeatSelected(Feat feat)
        {
            return feat.Name == Feat && FocusSelected(feat);
        }

        private bool FocusSelected(Feat feat)
        {
            return string.IsNullOrEmpty(FocusType) || feat.Foci.Contains(GroupConstants.All);
        }
    }
}
