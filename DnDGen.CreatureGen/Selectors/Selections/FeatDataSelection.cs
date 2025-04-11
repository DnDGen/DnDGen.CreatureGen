using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using DnDGen.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class FeatDataSelection : DataSelection<FeatDataSelection>
    {
        public string Feat { get; set; }
        public int FrequencyQuantity { get; set; }
        public string FrequencyTimePeriod { get; set; }
        public int Power { get; set; }
        public IEnumerable<RequiredFeatDataSelection> RequiredFeats { get; set; }
        public int RequiredBaseAttack { get; set; }
        public Dictionary<string, int> RequiredAbilities { get; set; }
        public Dictionary<string, int> RequiredSpeeds { get; set; }
        public IEnumerable<RequiredSkillDataSelection> RequiredSkills { get; set; }
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

        public override int SectionCount => 24;
        public static char Delimiter => '|';

        public static FeatDataSelection Map(string[] splitData)
        {
            var selection = new FeatDataSelection
            {
                Feat = splitData[DataIndexConstants.FeatData.NameIndex],
                RequiredBaseAttack = Convert.ToInt32(splitData[DataIndexConstants.FeatData.BaseAttackRequirementIndex]),
                FocusType = splitData[DataIndexConstants.FeatData.FocusTypeIndex],
                FrequencyQuantity = Convert.ToInt32(splitData[DataIndexConstants.FeatData.FrequencyQuantityIndex]),
                FrequencyTimePeriod = splitData[DataIndexConstants.FeatData.FrequencyTimePeriodIndex],
                Power = Convert.ToInt32(splitData[DataIndexConstants.FeatData.PowerIndex]),
                MinimumCasterLevel = Convert.ToInt32(splitData[DataIndexConstants.FeatData.MinimumCasterLevelIndex]),
                RequiredHands = Convert.ToInt32(splitData[DataIndexConstants.FeatData.RequiredHandQuantityIndex]),
                RequiredNaturalWeapons = Convert.ToInt32(splitData[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex]),
                RequiresNaturalArmor = Convert.ToBoolean(splitData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex]),
                RequiresSpecialAttack = Convert.ToBoolean(splitData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex]),
                RequiresSpellLikeAbility = Convert.ToBoolean(splitData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex]),
                RequiresEquipment = Convert.ToBoolean(splitData[DataIndexConstants.FeatData.RequiresEquipmentIndex]),
                RequiredFeats = GetRequiredFeats(splitData[DataIndexConstants.FeatData.RequiredFeatsIndex]),
                RequiredSkills = GetRequiredSkills(splitData[DataIndexConstants.FeatData.RequiredSkillsIndex]),
                RequiredAbilities = GetRequiredAbilities(splitData),
                RequiredSpeeds = GetRequiredSpeeds(splitData),
                RequiredSizes = GetRequiredSizes(splitData[DataIndexConstants.FeatData.RequiredSizesIndex]),
                CanBeTakenMultipleTimes = Convert.ToBoolean(splitData[DataIndexConstants.FeatData.TakenMultipleTimesIndex]),
            };

            return selection;
        }

        private static IEnumerable<string> GetRequiredSizes(string requiredSizesData)
        {
            if (string.IsNullOrEmpty(requiredSizesData))
                return [];

            return requiredSizesData.Split(Delimiter);
        }

        private static Dictionary<string, int> GetRequiredSpeeds(string[] splitData) => new()
        {
            [SpeedConstants.Fly] = Convert.ToInt32(splitData[DataIndexConstants.FeatData.RequiredFlySpeedIndex]),
        };

        private static Dictionary<string, int> GetRequiredAbilities(string[] splitData) => new()
        {
            [AbilityConstants.Strength] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredStrengthIndex]),
            [AbilityConstants.Constitution] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredConstitutionIndex]),
            [AbilityConstants.Dexterity] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredDexterityIndex]),
            [AbilityConstants.Intelligence] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredIntelligenceIndex]),
            [AbilityConstants.Wisdom] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredWisdomIndex]),
            [AbilityConstants.Charisma] = GetRequiredAbility(splitData[DataIndexConstants.FeatData.RequiredCharismaIndex]),
        };

        private static int GetRequiredAbility(string requiredAbilities)
        {
            if (string.IsNullOrEmpty(requiredAbilities))
                return 0;

            return Convert.ToInt32(requiredAbilities);
        }

        private static IEnumerable<RequiredSkillDataSelection> GetRequiredSkills(string requiredSkillsData)
        {
            if (string.IsNullOrEmpty(requiredSkillsData))
                return [];

            return requiredSkillsData.Split(Delimiter).Select(DataHelper.Parse<RequiredSkillDataSelection>);
        }

        private static IEnumerable<RequiredFeatDataSelection> GetRequiredFeats(string requiredFeatsData)
        {
            if (string.IsNullOrEmpty(requiredFeatsData))
                return [];

            return requiredFeatsData.Split(Delimiter).Select(DataHelper.Parse<RequiredFeatDataSelection>);
        }

        public static string[] Map(FeatDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.FeatData.NameIndex] = selection.Feat;
            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = selection.RequiredBaseAttack.ToString();
            data[DataIndexConstants.FeatData.FocusTypeIndex] = selection.FocusType;
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = selection.FrequencyQuantity.ToString();
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = selection.FrequencyTimePeriod;
            data[DataIndexConstants.FeatData.PowerIndex] = selection.Power.ToString();
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = selection.MinimumCasterLevel.ToString();
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = selection.RequiredHands.ToString();
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = selection.RequiredNaturalWeapons.ToString();
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = selection.RequiresNaturalArmor.ToString();
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = selection.RequiresSpecialAttack.ToString();
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = selection.RequiresSpellLikeAbility.ToString();
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = selection.RequiresEquipment.ToString();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = GetRequiredFeats(selection.RequiredFeats);
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = GetRequiredSkills(selection.RequiredSkills);
            data[DataIndexConstants.FeatData.RequiredStrengthIndex] = GetRequiredAbility(AbilityConstants.Strength, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredConstitutionIndex] = GetRequiredAbility(AbilityConstants.Constitution, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredDexterityIndex] = GetRequiredAbility(AbilityConstants.Dexterity, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredIntelligenceIndex] = GetRequiredAbility(AbilityConstants.Intelligence, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredWisdomIndex] = GetRequiredAbility(AbilityConstants.Wisdom, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredCharismaIndex] = GetRequiredAbility(AbilityConstants.Charisma, selection.RequiredAbilities);
            data[DataIndexConstants.FeatData.RequiredFlySpeedIndex] = GetRequiredSpeed(SpeedConstants.Fly, selection.RequiredSpeeds);
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = GetRequiredSizes(selection.RequiredSizes);
            data[DataIndexConstants.FeatData.TakenMultipleTimesIndex] = selection.CanBeTakenMultipleTimes.ToString();

            return data;
        }

        private static string GetRequiredSizes(IEnumerable<string> requiredSizesData) => string.Join(Delimiter, requiredSizesData);

        private static string GetRequiredSpeed(string speed, Dictionary<string, int> requiredSpeeds)
        {
            if (!requiredSpeeds.ContainsKey(speed))
                return 0.ToString();

            return requiredSpeeds[speed].ToString();
        }

        private static string GetRequiredAbility(string ability, Dictionary<string, int> requiredAbilities)
        {
            if (!requiredAbilities.ContainsKey(ability))
                return 0.ToString();

            return requiredAbilities[ability].ToString();
        }

        private static string GetRequiredSkills(IEnumerable<RequiredSkillDataSelection> requiredSkillsData)
        {
            var parsedData = requiredSkillsData.Select(DataHelper.Parse);
            return string.Join(Delimiter, parsedData);
        }

        private static string GetRequiredFeats(IEnumerable<RequiredFeatDataSelection> requiredFeatsData)
        {
            var parsedData = requiredFeatsData.Select(DataHelper.Parse);
            return string.Join(Delimiter, parsedData);
        }

        public FeatDataSelection()
        {
            Feat = string.Empty;
            RequiredFeats = [];
            RequiredAbilities = [];
            RequiredSpeeds = [];
            RequiredSkills = [];
            FocusType = string.Empty;
            FrequencyTimePeriod = string.Empty;
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

            //foreach (var requiredSpeed in RequiredSpeeds.Where(rs => rs.Value > 0))
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
            return feats.Any(FeatSelected);
        }

        private bool FeatSelected(Feat feat)
        {
            return feat.Name == Feat && FocusSelected(feat);
        }

        private bool FocusSelected(Feat feat)
        {
            return string.IsNullOrEmpty(FocusType) || feat.Foci.Contains(GroupConstants.All);
        }

        internal class RequiredFeatDataSelection : DataSelection<RequiredFeatDataSelection>
        {
            public string Feat { get; set; }
            public IEnumerable<string> Foci { get; set; }

            public override Func<string[], RequiredFeatDataSelection> MapTo => Map;
            public override Func<RequiredFeatDataSelection, string[]> MapFrom => Map;

            public override int SectionCount => 2;
            public override char Separator => '#';
            public static char Delimiter => ',';

            public static RequiredFeatDataSelection Map(string[] splitData)
            {
                var selection = new RequiredFeatDataSelection
                {
                    Feat = splitData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex],
                    Foci = splitData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex]
                        .Split(Delimiter)
                        .Where(f => !string.IsNullOrEmpty(f)),

                };

                return selection;
            }

            public static string[] Map(RequiredFeatDataSelection selection)
            {
                var data = new string[selection.SectionCount];
                data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = selection.Feat;
                data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(Delimiter, selection.Foci);

                return data;
            }

            public RequiredFeatDataSelection()
            {
                Feat = string.Empty;
                Foci = [];
            }

            public bool RequirementMet(IEnumerable<Feat> otherFeats)
            {
                var requiredFeats = otherFeats.Where(f => f.Name == Feat);

                if (!requiredFeats.Any())
                    return false;

                if (!Foci.Any())
                    return true;

                var requiredFoci = requiredFeats.SelectMany(f => f.Foci);
                return Foci.Intersect(requiredFoci).Any();
            }
        }

        internal class RequiredSkillDataSelection : DataSelection<RequiredSkillDataSelection>
        {
            public string Skill { get; set; }
            public string Focus { get; set; }
            public int Ranks { get; set; }

            public override Func<string[], RequiredSkillDataSelection> MapTo => Map;
            public override Func<RequiredSkillDataSelection, string[]> MapFrom => Map;

            public override int SectionCount => 3;
            public override char Separator => '#';

            public static RequiredSkillDataSelection Map(string[] splitData)
            {
                var selection = new RequiredSkillDataSelection
                {
                    Skill = splitData[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex],
                    Focus = splitData[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex],
                    Ranks = Convert.ToInt32(splitData[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex]),
                };

                return selection;
            }

            public static string[] Map(RequiredSkillDataSelection selection)
            {
                var data = new string[selection.SectionCount];
                data[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex] = selection.Skill;
                data[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex] = selection.Focus;
                data[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex] = selection.Ranks.ToString();

                return data;
            }

            public RequiredSkillDataSelection()
            {
                Skill = string.Empty;
                Focus = string.Empty;
            }

            public bool RequirementMet(IEnumerable<Skill> otherSkills)
            {
                var thisSkill = SkillConstants.Build(Skill, Focus);
                var requiredSkills = otherSkills.Where(s => s.IsEqualTo(thisSkill));

                if (!requiredSkills.Any())
                    return false;

                if (!string.IsNullOrEmpty(Focus) && !requiredSkills.Any(s => s.Focus == Focus))
                    return false;

                var anyHaveSufficientRanks = requiredSkills.Any(s => s.EffectiveRanks >= Ranks);

                return anyHaveSufficientRanks;
            }
        }
    }
}
