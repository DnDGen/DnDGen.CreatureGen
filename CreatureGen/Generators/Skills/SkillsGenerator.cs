using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Skills
{
    internal class SkillsGenerator : ISkillsGenerator
    {
        private readonly ISkillSelector skillSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public SkillsGenerator(ISkillSelector skillSelector, ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, IPercentileSelector percentileSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.skillSelector = skillSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.percentileSelector = percentileSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<Skill> GenerateFor(HitPoints hitPoints, string creatureName, CreatureType creatureType, Dictionary<string, Ability> abilities, bool canUseEquipment, string size)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return Enumerable.Empty<Skill>();

            var untrainedSkillNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.Untrained);
            var creatureSkillNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, creatureName);

            if (!canUseEquipment)
            {
                var unnaturalSkills = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.Unnatural);
                untrainedSkillNames = untrainedSkillNames.Except(unnaturalSkills);
            }

            var allSkillNames = untrainedSkillNames.Union(creatureSkillNames);
            var skillSelections = GetSkillSelections(allSkillNames, creatureSkillNames);
            var skills = InitializeSkills(abilities, skillSelections, hitPoints, creatureSkillNames);

            skills = ApplySkillPointsAsRanks(skills, hitPoints, creatureType, abilities);
            skills = ApplyBonuses(creatureName, skills, size);
            skills = ApplySkillSynergy(creatureName, skills, size);

            return skills;
        }

        private IEnumerable<Skill> ApplyBonuses(string creature, IEnumerable<Skill> skills, string size)
        {
            var bonuses = skillSelector.SelectBonusesFor(creature);

            foreach (var bonus in bonuses)
            {
                var matchingSkills = skills.Where(s => s.IsEqualTo(bonus.Source));

                foreach (var skill in matchingSkills)
                {
                    skill.AddBonus(bonus.Bonus, bonus.Condition);
                }
            }

            skills = ApplyHideSkillSizeModifier(skills, size);

            return skills;
        }

        private IEnumerable<Skill> ApplySkillSynergy(string creature, IEnumerable<Skill> skills, string size)
        {
            var synergyOpportunities = skills.Where(s => s.EffectiveRanks >= 5);

            foreach (var sourceSkill in synergyOpportunities)
            {
                var bonuses = skillSelector.SelectBonusesFor(sourceSkill.Key);

                foreach (var bonus in bonuses)
                {
                    var matchingSkills = skills.Where(s => s.IsEqualTo(bonus.Source));

                    foreach (var skill in matchingSkills)
                    {
                        skill.AddBonus(bonus.Bonus, bonus.Condition);
                    }
                }
            }

            return skills;
        }

        private IEnumerable<Skill> ApplyHideSkillSizeModifier(IEnumerable<Skill> skills, string size)
        {
            var hideSkill = skills.FirstOrDefault(s => s.Name == SkillConstants.Hide);
            if (hideSkill != null)
            {
                var bonus = GetHideSkillSizeModifier(size);
                hideSkill.AddBonus(bonus);
            }

            return skills;
        }

        private int GetHideSkillSizeModifier(string size)
        {
            var sizes = SizeConstants.GetOrdered();
            var mediumIndex = Array.IndexOf(sizes, SizeConstants.Medium);
            var sizeIndex = Array.IndexOf(sizes, size);

            return (mediumIndex - sizeIndex) * 4;
        }

        private IEnumerable<Skill> MaxOutSkills(IEnumerable<Skill> skills)
        {
            foreach (var skill in skills)
            {
                skill.Ranks = skill.RankCap;
            }

            return skills;
        }

        private IEnumerable<Skill> ApplySkillPointsAsRanks(IEnumerable<Skill> skills, HitPoints hitPoints, CreatureType creatureType, Dictionary<string, Ability> abilities)
        {
            var points = GetTotalSkillPoints(creatureType, hitPoints.RoundedHitDiceQuantity, abilities[AbilityConstants.Intelligence]);
            var totalRanksAvailable = skills.Count() * (hitPoints.HitDiceQuantity + 3);

            if (points >= totalRanksAvailable)
            {
                return MaxOutSkills(skills);
            }

            var skillsWithAvailableRanks = skills.Where(s => !s.RanksMaxedOut);
            var creatureSkills = skillsWithAvailableRanks.Where(s => s.ClassSkill);
            var untrainedSkills = skillsWithAvailableRanks.Where(s => !s.ClassSkill);

            while (points-- > 0)
            {
                var skill = collectionsSelector.SelectRandomFrom(creatureSkills, untrainedSkills);
                skill.Ranks++;
            }

            return skills;
        }

        private IEnumerable<SkillSelection> GetSkillSelections(IEnumerable<string> skillNames, IEnumerable<string> creatureSkills)
        {
            var selections = new List<SkillSelection>();

            foreach (var skillName in skillNames)
            {
                var selection = skillSelector.SelectFor(skillName);
                selection.ClassSkill = creatureSkills.Contains(skillName) || creatureSkills.Contains(selection.SkillName);

                var explodedSelection = ExplodeSelectedSkill(selection);

                selections.AddRange(explodedSelection);
            }

            return selections;
        }

        private IEnumerable<SkillSelection> ExplodeSelectedSkill(SkillSelection skillSelection)
        {
            if (skillSelection.RandomFociQuantity == 0)
                return new[] { skillSelection };

            var skillFoci = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName).ToList();

            if (skillSelection.RandomFociQuantity >= skillFoci.Count)
            {
                return skillFoci.Select(f => new SkillSelection { BaseAbilityName = skillSelection.BaseAbilityName, SkillName = skillSelection.SkillName, Focus = f, ClassSkill = skillSelection.ClassSkill });
            }

            var selections = new List<SkillSelection>();

            while (skillSelection.RandomFociQuantity > selections.Count)
            {
                var focus = collectionsSelector.SelectRandomFrom(skillFoci);
                var selection = new SkillSelection();

                selection.BaseAbilityName = skillSelection.BaseAbilityName;
                selection.SkillName = skillSelection.SkillName;
                selection.Focus = focus;
                selection.ClassSkill = skillSelection.ClassSkill;

                selections.Add(selection);
                skillFoci.Remove(focus);
            }

            return selections;
        }

        private IEnumerable<Skill> InitializeSkills(Dictionary<string, Ability> abilities, IEnumerable<SkillSelection> skillSelections, HitPoints hitPoints, IEnumerable<string> creatureSkills)
        {
            var skills = new List<Skill>();
            var skillsWithArmorCheckPenalties = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty);

            foreach (var skillSelection in skillSelections)
            {
                if (!abilities[skillSelection.BaseAbilityName].HasScore)
                    continue;

                var skill = new Skill(skillSelection.SkillName, abilities[skillSelection.BaseAbilityName], hitPoints.RoundedHitDiceQuantity + 3, skillSelection.Focus);

                skill.HasArmorCheckPenalty = skillsWithArmorCheckPenalties.Contains(skill.Name);
                //INFO: all creature skills are class skills
                skill.ClassSkill = skillSelection.ClassSkill;

                skills.Add(skill);
            }

            return skills;
        }

        private int GetTotalSkillPoints(CreatureType creatureType, int hitDieQuantity, Ability intelligence)
        {
            if (hitDieQuantity == 0 || !intelligence.HasScore)
                return 0;

            var points = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.SkillPoints, creatureType.Name);
            var perHitDie = Math.Max(1, points + intelligence.Modifier);
            var multiplier = hitDieQuantity + 3;
            var total = perHitDie * multiplier;

            return total;
        }

        public IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            if (feats.Any(f => f.Name == FeatConstants.SpecialQualities.SwapSkillBaseAbility))
            {
                var swap = feats.First(f => f.Name == FeatConstants.SpecialQualities.SwapSkillBaseAbility);
                foreach (var focus in swap.Foci)
                {
                    var sections = focus.Split(':');
                    var skillName = sections[0];
                    var abilityName = sections[1];

                    var skill = skills.First(s => s.Name == skillName);
                    skill.BaseAbility = abilities[abilityName];
                }
            }

            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.All);

            foreach (var feat in feats)
            {
                if (feat.Foci.Any())
                {
                    var matchingFoci = feat.Foci.Where(f => allSkills.Any(s => f.StartsWith(s)));

                    foreach (var focus in matchingFoci)
                    {
                        var matchingSkills = skills.Where(s => s.IsEqualTo(focus) || focus.StartsWith(s.Name));

                        foreach (var skill in matchingSkills)
                        {
                            if (skill.IsEqualTo(focus))
                            {
                                skill.AddBonus(feat.Power);
                            }
                            else
                            {
                                var condition = focus.Replace(skill.Name + ": ", string.Empty);
                                skill.AddBonus(feat.Power, condition);
                            }
                        }
                    }
                }
                else
                {
                    var skillsToReceiveBonus = collectionsSelector.SelectFrom(TableNameConstants.Collection.SkillGroups, feat.Name);

                    foreach (var skillName in skillsToReceiveBonus)
                    {
                        var skill = skills.FirstOrDefault(s => s.IsEqualTo(skillName));

                        if (skill != null)
                            skill.AddBonus(feat.Power);
                    }
                }
            }

            return skills;
        }
    }
}