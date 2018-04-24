using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Feats
{
    internal class FeatFocusGenerator : IFeatFocusGenerator
    {
        private readonly ICollectionSelector collectionsSelector;

        public FeatFocusGenerator(ICollectionSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeats)
        {
            if (string.IsNullOrEmpty(focusType))
                return string.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != FeatConstants.Foci.All && !allSourceFeatFoci.ContainsKey(focusType))
                return focusType;

            var requiredFeatNames = requiredFeats.Select(f => f.Feat);
            var foci = GetFoci(feat, focusType, allSourceFeatFoci, otherFeats, requiredFeatNames);
            var usedFeats = otherFeats.Where(f => f.Name == feat);
            var usedFoci = usedFeats.SelectMany(f => f.Foci);

            if (usedFoci.Contains(FeatConstants.Foci.All))
                return FeatConstants.Foci.NoValidFociAvailable;

            foci = foci.Except(usedFoci);
            var skillFoci = allSourceFeatFoci[GroupConstants.Skills].Intersect(foci);

            if (skillFoci.Any())
            {
                var potentialSkillFoci = skills.Select(s => s.Focus.Any() ? $"{s.Name}/{s.Focus}" : s.Name);
                foci = skillFoci.Intersect(potentialSkillFoci);
            }

            if (!foci.Any())
                return FeatConstants.Foci.NoValidFociAvailable;

            return collectionsSelector.SelectRandomFrom(foci);
        }

        private IEnumerable<string> GetExplodedFoci(Dictionary<string, IEnumerable<string>> allSourceFeatFoci, string feat, string focusType, IEnumerable<Feat> otherFeats)
        {
            if (focusType != FeatConstants.Foci.All)
                return allSourceFeatFoci[focusType];

            if (feat != FeatConstants.WeaponProficiency_Martial && feat != FeatConstants.WeaponProficiency_Exotic)
                return allSourceFeatFoci[feat];

            var weaponFamiliarityFeats = otherFeats.Where(f => f.Name == FeatConstants.SpecialQualities.WeaponFamiliarity);
            var familiarityFoci = weaponFamiliarityFeats.SelectMany(f => f.Foci);

            if (feat == FeatConstants.WeaponProficiency_Martial)
                return allSourceFeatFoci[feat].Union(familiarityFoci);

            return allSourceFeatFoci[feat].Except(familiarityFoci);
        }

        private IEnumerable<string> GetFoci(string feat, string focusType, Dictionary<string, IEnumerable<string>> allSourceFeatFoci, IEnumerable<Feat> otherFeats, IEnumerable<string> requiredFeatNames)
        {
            var proficiencyRequired = requiredFeatNames.Contains(GroupConstants.WeaponProficiency);
            var sourceFeatFoci = GetExplodedFoci(allSourceFeatFoci, feat, focusType, otherFeats);

            if (!proficiencyRequired && !otherFeats.Any(f => RequirementHasFocus(requiredFeatNames, f)))
                return sourceFeatFoci;

            var requiredFeatWithFoci = otherFeats.Where(f => RequirementHasFocus(requiredFeatNames, f));

            if (proficiencyRequired)
            {
                var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.WeaponProficiency);
                var proficiencyFeats = otherFeats.Where(f => proficiencyFeatNames.Contains(f.Name));

                requiredFeatWithFoci = requiredFeatWithFoci.Union(proficiencyFeats);
            }

            var requiredFeats = requiredFeatWithFoci.Where(f => !f.Foci.Contains(FeatConstants.Foci.All));
            var requiredFoci = requiredFeats.SelectMany(f => f.Foci);

            var featsWithAllFocus = requiredFeatWithFoci.Where(f => f.Foci.Contains(FeatConstants.Foci.All));

            foreach (var featWithAllFocus in featsWithAllFocus)
            {
                var explodedFoci = GetExplodedFoci(allSourceFeatFoci, featWithAllFocus.Name, FeatConstants.Foci.All, otherFeats);
                requiredFoci = requiredFoci.Union(explodedFoci);
            }

            var applicableFoci = requiredFoci.Intersect(sourceFeatFoci);

            if (focusType.StartsWith(FeatConstants.Foci.Weapon) && requiredFeatWithFoci.Any() == false)
            {
                var automaticFoci = allSourceFeatFoci[focusType].Except(allSourceFeatFoci[FeatConstants.Foci.Weapon]);
                applicableFoci = applicableFoci.Union(automaticFoci);
            }

            return applicableFoci;
        }

        private bool RequirementHasFocus(IEnumerable<string> requiredFeatNames, Feat feat)
        {
            return requiredFeatNames.Contains(feat.Name) && feat.Foci.Any();
        }

        public string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills)
        {
            if (string.IsNullOrEmpty(focusType))
                return string.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != FeatConstants.Foci.All && allSourceFeatFoci.Keys.Contains(focusType) == false)
                return focusType;

            var foci = GetFoci(feat, focusType, allSourceFeatFoci, Enumerable.Empty<Feat>(), Enumerable.Empty<string>());
            var skillFoci = allSourceFeatFoci[GroupConstants.Skills].Intersect(foci);

            if (skillFoci.Any())
            {
                var potentialSkillFoci = skills.Select(s => s.Focus.Any() ? $"{s.Name}/{s.Focus}" : s.Name);
                foci = skillFoci.Intersect(potentialSkillFoci);
            }

            if (foci.Any() == false)
                return FeatConstants.Foci.All;

            return collectionsSelector.SelectRandomFrom(foci);
        }

        public string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeats)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills, requiredFeats, otherFeats);
        }

        public string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills);
        }
    }
}