using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class SkillDataSelection : DataSelection<SkillDataSelection>
    {
        public string BaseAbilityName { get; set; }
        public int RandomFociQuantity { get; set; }
        public string SkillName { get; set; }
        public string Focus { get; set; }
        public bool ClassSkill { get; set; }

        public override Func<string[], SkillDataSelection> MapTo => Map;
        public override Func<SkillDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 4;

        public static SkillDataSelection Map(string[] splitData)
        {
            var selection = new SkillDataSelection
            {
                BaseAbilityName = splitData[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex],
                SkillName = splitData[DataIndexConstants.SkillSelectionData.SkillNameIndex],
                RandomFociQuantity = Convert.ToInt32(splitData[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex]),
                Focus = splitData[DataIndexConstants.SkillSelectionData.FocusIndex]
            };

            return selection;
        }

        public static string[] Map(SkillDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex] = selection.BaseAbilityName;
            data[DataIndexConstants.SkillSelectionData.SkillNameIndex] = selection.SkillName;
            data[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex] = selection.RandomFociQuantity.ToString();
            data[DataIndexConstants.SkillSelectionData.FocusIndex] = selection.Focus;

            return data;
        }

        public SkillDataSelection()
        {
            BaseAbilityName = string.Empty;
            SkillName = string.Empty;
            Focus = string.Empty;
        }

        public bool IsEqualTo(Skill skill)
        {
            if (RandomFociQuantity > 0)
                throw new InvalidOperationException("Cannot test equality of a skill selection while random foci quantity is positive");

            return SkillName == skill.Name && Focus == skill.Focus;
        }

        public bool IsEqualTo(SkillDataSelection selection)
        {
            if (RandomFociQuantity > 0 || selection.RandomFociQuantity > 0)
                throw new InvalidOperationException("Cannot test equality of a skill selection while random foci quantity is positive");

            return SkillName == selection.SkillName && Focus == selection.Focus;
        }
    }
}