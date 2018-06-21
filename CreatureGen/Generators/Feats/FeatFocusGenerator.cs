using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public FeatFocusGenerator(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeats, int casterLevel, Dictionary<string, Ability> abilities)
        {
            if (string.IsNullOrEmpty(focusType))
                return string.Empty;

            var isPreset = FocusTypeIsPreset(focusType);
            if (isPreset)
                return focusType;

            var foci = GetFoci(feat, focusType, otherFeats, requiredFeats, casterLevel, abilities);
            var usedFeats = otherFeats.Where(f => f.Name == feat);
            var usedFoci = usedFeats.SelectMany(f => f.Foci);

            if (usedFoci.Contains(FeatConstants.Foci.All))
                return FeatConstants.Foci.NoValidFociAvailable;

            foci = foci.Except(usedFoci);

            return SelectRandomAndIncludeSkills(foci, skills);
        }

        private string SelectRandomAndIncludeSkills(IEnumerable<string> foci, IEnumerable<Skill> skills)
        {
            var skillFoci = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatFoci, GroupConstants.Skills);
            var applicableSkillFoci = skillFoci.Intersect(foci);

            if (applicableSkillFoci.Any())
            {
                var potentialSkillFoci = skills.Select(s => s.Focus.Any() ? $"{s.Name}/{s.Focus}" : s.Name);
                foci = applicableSkillFoci.Intersect(potentialSkillFoci);
            }

            if (!foci.Any())
                return FeatConstants.Foci.NoValidFociAvailable;

            return collectionsSelector.SelectRandomFrom(foci);
        }

        private bool FocusTypeIsPreset(string focusType)
        {
            var isCollection = collectionsSelector.IsCollection(TableNameConstants.Collection.FeatFoci, focusType);
            return focusType != FeatConstants.Foci.All && !isCollection;
        }

        private IEnumerable<string> GetExplodedFoci(string feat, string focusType, IEnumerable<Feat> otherFeats)
        {
            if (focusType != FeatConstants.Foci.All)
                return collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatFoci, focusType);

            var featFoci = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatFoci, feat);

            if (feat != FeatConstants.WeaponProficiency_Martial && feat != FeatConstants.WeaponProficiency_Exotic)
                return featFoci;

            var weaponFamiliarityFeats = otherFeats.Where(f => f.Name == FeatConstants.SpecialQualities.WeaponFamiliarity);
            var familiarityFoci = weaponFamiliarityFeats.SelectMany(f => f.Foci);

            if (feat == FeatConstants.WeaponProficiency_Martial)
                return featFoci.Union(familiarityFoci);

            return featFoci.Except(familiarityFoci);
        }

        private IEnumerable<string> GetFoci(string feat, string focusType, IEnumerable<Feat> otherFeats, IEnumerable<RequiredFeatSelection> requiredFeats, int casterLevel, Dictionary<string, Ability> abilities)
        {
            var proficiencyRequired = requiredFeats.Any(f => f.Feat == GroupConstants.WeaponProficiency);
            var applicableFoci = GetExplodedFoci(feat, focusType, otherFeats);

            applicableFoci = FilterByAbilityRequirements(applicableFoci, feat, abilities);
            var requiredFeatNames = requiredFeats.Select(f => f.Feat);
            var requiredFeatsWithFoci = otherFeats.Where(f => RequirementHasFocus(requiredFeatNames, f));

            if (!proficiencyRequired && !requiredFeatsWithFoci.Any())
                return applicableFoci;

            if (proficiencyRequired)
            {
                var weaponProficiencyRequirement = requiredFeats.First(f => f.Feat == GroupConstants.WeaponProficiency);

                //INFO: Add in automatic proficiencies as well
                var automaticProficiency = new Feat();
                automaticProficiency.Name = GroupConstants.WeaponProficiency;
                automaticProficiency.Foci = GetProficiencies(focusType, otherFeats, weaponProficiencyRequirement);

                requiredFeatsWithFoci = requiredFeatsWithFoci.Union(new[] { automaticProficiency });
            }

            var requiredFociByFeat = requiredFeatsWithFoci.ToDictionary(f => f.Name, f => f.Foci);

            foreach (var kvp in requiredFociByFeat)
            {
                var featName = kvp.Key;
                var foci = kvp.Value;

                if (foci.Contains(FeatConstants.Foci.All))
                    foci = GetExplodedFoci(featName, FeatConstants.Foci.All, otherFeats);

                applicableFoci = applicableFoci.Intersect(foci);
            }

            if (casterLevel <= 0)
                applicableFoci = applicableFoci.Except(new[] { FeatConstants.Foci.Weapons.Ray });

            return applicableFoci;
        }

        //INFO: Automatic Proficiencies include things such as Unarmed Strike, Grapple, and Ray
        private IEnumerable<string> GetProficiencies(string focusType, IEnumerable<Feat> otherFeats, RequiredFeatSelection weaponProficiencyRequirement)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            var proficiencyFeats = otherFeats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var proficiencyFoci = new List<string>();

            foreach (var proficiencyFeat in proficiencyFeats)
            {
                foreach (var focus in proficiencyFeat.Foci)
                {
                    if (focus != FeatConstants.Foci.All)
                    {
                        proficiencyFoci.Add(focus);
                    }
                    else
                    {
                        var explodedFoci = GetExplodedFoci(proficiencyFeat.Name, FeatConstants.Foci.All, otherFeats);
                        proficiencyFoci.AddRange(explodedFoci);
                    }
                }
            }

            var foci = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatFoci, focusType);
            var weaponFoci = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatFoci, FeatConstants.Foci.Weapon);
            var automaticFoci = foci.Except(weaponFoci);

            proficiencyFoci.AddRange(automaticFoci);

            if (weaponProficiencyRequirement.Foci.Any())
            {
                return proficiencyFoci.Intersect(weaponProficiencyRequirement.Foci);
            }

            return proficiencyFoci;
        }

        private IEnumerable<string> FilterByAbilityRequirements(IEnumerable<string> foci, string featName, Dictionary<string, Ability> abilities)
        {
            var applicableFoci = new List<string>(foci);

            foreach (var focus in foci)
            {
                var combo = $"{featName}/{focus}";
                var abilityRequirements = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, combo);

                if (!abilityRequirements.Any())
                    continue;

                if (!abilityRequirements.Any(r => abilities[r.Type].FullScore >= r.Amount))
                    applicableFoci.Remove(focus);
            }

            return applicableFoci;
        }

        private bool RequirementHasFocus(IEnumerable<string> requiredFeatNames, Feat feat)
        {
            return requiredFeatNames.Contains(feat.Name) && feat.Foci.Any();
        }

        public string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            if (string.IsNullOrEmpty(focusType))
                return string.Empty;

            var isPreset = FocusTypeIsPreset(focusType);
            if (isPreset)
                return focusType;

            var foci = GetFoci(feat, focusType, Enumerable.Empty<Feat>(), Enumerable.Empty<RequiredFeatSelection>(), 0, abilities);

            return SelectRandomAndIncludeSkills(foci, skills);
        }

        public string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeats, int casterLevel, Dictionary<string, Ability> abilities)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills, requiredFeats, otherFeats, casterLevel, abilities);
        }

        public string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills, abilities);
        }
    }
}