using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Selectors.Selections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Skills
{
    internal class SkillsGenerator : ISkillsGenerator
    {
        private readonly ISkillSelector skillSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IPercentileSelector percentileSelector;

        public SkillsGenerator(ISkillSelector skillSelector, ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, IPercentileSelector percentileSelector)
        {
            this.skillSelector = skillSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.percentileSelector = percentileSelector;
        }

        public IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities)
        {
            var classSkillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, characterClass.Name).ToList(); //INFO: Calling ToList so we can add specialist skills later
            var crossClassSkillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Untrained);

            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var specialistSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, specialistField);
                classSkillNames.AddRange(specialistSkills);
            }

            var classSkillSelections = GetSkillSelections(classSkillNames);
            var crossClassSkillSelections = GetSkillSelections(crossClassSkillNames);

            if (characterClass.Name == CharacterClassConstants.Expert)
                classSkillSelections = GetRandomClassSkillSelections();

            classSkillSelections = AddProfessionSkills(classSkillSelections);

            var skills = InitializeSkills(abilities, classSkillSelections, crossClassSkillSelections, characterClass);

            skills = AddRanks(characterClass, race, abilities, classSkillSelections, crossClassSkillSelections, skills);

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace))
                skills = AddMonsterSkillRanks(race, abilities, skills);

            skills = ApplySkillSynergies(skills);

            return skills;
        }

        private IEnumerable<SkillSelection> AddProfessionSkills(IEnumerable<SkillSelection> classSkillSelections)
        {
            var professionSkill = classSkillSelections.FirstOrDefault(s => s.SkillName == SkillConstants.Profession);

            if (professionSkill == null)
                return classSkillSelections;

            var profession = professionSkill.Focus;
            var professionSkillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, profession);
            var professionSkillSelections = GetSkillSelections(professionSkillNames);

            return classSkillSelections.Union(professionSkillSelections);
        }

        private IEnumerable<SkillSelection> GetRandomClassSkillSelections()
        {
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.All);
            var randomSkillSelections = new List<SkillSelection>();

            while (randomSkillSelections.Count < 10)
            {
                var skill = collectionsSelector.SelectRandomFrom(allSkills);
                var skillSelection = skillSelector.SelectFor(skill);
                var explodedSelections = ExplodeSelectedSkill(skillSelection);
                var newSelections = explodedSelections.Where(ss => !randomSkillSelections.Any(s => s.IsEqualTo(ss)));

                randomSkillSelections.AddRange(newSelections);
            }

            return randomSkillSelections;
        }

        private IEnumerable<Skill> AddMonsterSkillRanks(Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            var monsterHitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice, race.BaseRace);

            if (monsterHitDice == 0)
                return skills;

            var skillsList = skills.ToList();
            var monsterSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace);
            var monsterSkillSelections = GetSkillSelections(monsterSkills);

            foreach (var monsterSkillSelection in monsterSkillSelections)
            {
                if (skillsList.Any(s => monsterSkillSelection.IsEqualTo(s)) == false)
                {
                    if (abilities.ContainsKey(monsterSkillSelection.BaseStatName) == false)
                        continue;

                    var newSkill = new Skill(monsterSkillSelection.SkillName, abilities[monsterSkillSelection.BaseStatName], 3, monsterSkillSelection.Focus);
                    skillsList.Add(newSkill);
                }

                var monsterSkill = skillsList.First(s => monsterSkillSelection.IsEqualTo(s));

                monsterSkill.RankCap += monsterHitDice;
                monsterSkill.ClassSkill = true;
            }

            var points = GetTotalSkillPoints(race.BaseRace, monsterHitDice, abilities[AbilityConstants.Intelligence], race);
            var validMonsterSkills = FilterOutInvalidSkills(monsterSkillSelections, skillsList);

            while (points-- > 0 && validMonsterSkills.Any())
            {
                var skillSelection = collectionsSelector.SelectRandomFrom(validMonsterSkills);
                var skill = skillsList.First(s => skillSelection.IsEqualTo(s));
                skill.Ranks++;

                validMonsterSkills = FilterOutInvalidSkills(monsterSkillSelections, skillsList);
            }

            return skillsList;
        }

        private IEnumerable<SkillSelection> GetSkillSelections(IEnumerable<string> skillNames)
        {
            var selections = skillNames.Select(s => skillSelector.SelectFor(s));
            var explodedSelections = selections.SelectMany(s => ExplodeSelectedSkill(s));

            //INFO: Calling immediate execution, since exploding includes potentially random results, and after this method is complete, we want consistent results.
            return explodedSelections.ToList();
        }

        private IEnumerable<SkillSelection> ExplodeSelectedSkill(SkillSelection skillSelection)
        {
            if (skillSelection.RandomFociQuantity == 0)
                return new[] { skillSelection };

            var skillFoci = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName).ToList();

            if (skillSelection.RandomFociQuantity >= skillFoci.Count)
            {
                return skillFoci.Select(f => new SkillSelection { BaseStatName = skillSelection.BaseStatName, SkillName = skillSelection.SkillName, Focus = f });
            }

            var selections = new List<SkillSelection>();

            while (skillSelection.RandomFociQuantity > selections.Count)
            {
                var focus = collectionsSelector.SelectRandomFrom(skillFoci);
                var selection = new SkillSelection();

                selection.BaseStatName = skillSelection.BaseStatName;
                selection.SkillName = skillSelection.SkillName;
                selection.Focus = focus;

                selections.Add(selection);
                skillFoci.Remove(focus);
            }

            return selections;
        }

        private IEnumerable<Skill> InitializeSkills(Dictionary<string, Ability> stats, IEnumerable<SkillSelection> classSkillSelections, IEnumerable<SkillSelection> crossClassSkillSelections, CharacterClass characterClass)
        {
            var skills = new List<Skill>();
            var allSkillSelections = classSkillSelections.Union(crossClassSkillSelections);

            foreach (var skillSelection in allSkillSelections)
            {
                if (stats.ContainsKey(skillSelection.BaseStatName) == false)
                    continue;

                var skill = skills.FirstOrDefault(s => skillSelection.IsEqualTo(s));

                if (skill == null)
                {
                    skill = new Skill(skillSelection.SkillName, stats[skillSelection.BaseStatName], characterClass.Level + 3, skillSelection.Focus);

                    var skillsWithArmorCheckPenalties = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty);
                    skill.HasArmorCheckPenalty = skillsWithArmorCheckPenalties.Contains(skill.Name);
                    skills.Add(skill);
                }

                skill.ClassSkill = classSkillSelections.Any(s => s.IsEqualTo(skill));
            }

            return skills;
        }

        private IEnumerable<Skill> AddRanks(CharacterClass characterClass, Race race, Dictionary<string, Ability> stats, IEnumerable<SkillSelection> classSkillSelections, IEnumerable<SkillSelection> crossClassSkillSelections, IEnumerable<Skill> skills)
        {
            var points = GetTotalSkillPoints(characterClass.Name, characterClass.Level, stats[AbilityConstants.Intelligence], race);
            var allSkillSelections = classSkillSelections.Union(crossClassSkillSelections);
            var validSkills = FilterOutInvalidSkills(allSkillSelections, skills);

            while (points > 0 && validSkills.Any())
            {
                var skillCollection = GetRandomSkillCollection(skills, classSkillSelections, crossClassSkillSelections);
                var skillSelection = collectionsSelector.SelectRandomFrom(skillCollection);
                var skill = skills.First(s => skillSelection.IsEqualTo(s));

                skill.Ranks++;
                points--;

                validSkills = FilterOutInvalidSkills(allSkillSelections, skills);
            }

            return skills;
        }

        private int GetTotalSkillPoints(string source, int levels, Ability intelligence, Race race)
        {
            var points = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, source);
            var perLevel = points + intelligence.Bonus;
            var multiplier = levels + 3;

            if (race.BaseRace == SizeConstants.BaseRaces.Human)
                perLevel++;

            return Math.Max(perLevel * multiplier, levels);
        }

        private IEnumerable<SkillSelection> GetRandomSkillCollection(IEnumerable<Skill> skills, IEnumerable<SkillSelection> classSkillSelections, IEnumerable<SkillSelection> crossClassSkillSelections)
        {
            var validClassSkills = FilterOutInvalidSkills(classSkillSelections, skills);
            var validCrossClassSkills = FilterOutInvalidSkills(crossClassSkillSelections, skills);

            if (!validClassSkills.Any())
                return validCrossClassSkills;

            if (!validCrossClassSkills.Any())
                return validClassSkills;

            var shouldAddPointToCrossClassSkill = percentileSelector.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill);
            if (shouldAddPointToCrossClassSkill)
                return validCrossClassSkills;

            return validClassSkills;
        }

        private IEnumerable<SkillSelection> FilterOutInvalidSkills(IEnumerable<SkillSelection> skillSelections, IEnumerable<Skill> skills)
        {
            return skillSelections.Where(ss => skills.Any(s => ss.IsEqualTo(s) && s.RanksMaxedOut == false));
        }

        private IEnumerable<Skill> ApplySkillSynergies(IEnumerable<Skill> skills)
        {
            var skillsWarrantingSynergy = skills.Where(s => s.QualifiesForSkillSynergy);

            foreach (var skill in skillsWarrantingSynergy)
            {
                var name = skill.Focus.Any() ? $"{skill.Name}/{skill.Focus}" : skill.Name;
                var synergySkillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, name);

                foreach (var synergySkillName in synergySkillNames)
                {
                    var synergySkill = skills.FirstOrDefault(s => s.IsEqualTo(synergySkillName));

                    if (synergySkill != null)
                        synergySkill.Bonus += 2;
                }
            }

            return skills;
        }
    }
}