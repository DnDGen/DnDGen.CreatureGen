using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Languages
{
    internal class LanguageGenerator : ILanguageGenerator
    {
        private readonly ICollectionSelector collectionsSelector;

        public LanguageGenerator(ICollectionSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<string> GenerateWith(string creature, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            var languages = new List<string>();

            var automaticLanguages = collectionsSelector.SelectFrom(TableNameConstants.Collection.LanguageGroups, creature + LanguageConstants.Groups.Automatic);
            languages.AddRange(automaticLanguages);

            var bonusLanguages = collectionsSelector.SelectFrom(TableNameConstants.Collection.LanguageGroups, creature + LanguageConstants.Groups.Bonus);
            var remainingBonusLanguages = bonusLanguages.Except(languages).ToList();
            var numberOfBonusLanguages = abilities[AbilityConstants.Intelligence].Modifier;

            if (IsInterpreter(skills))
                numberOfBonusLanguages = Math.Max(1, abilities[AbilityConstants.Intelligence].Modifier + 1);

            if (numberOfBonusLanguages >= remainingBonusLanguages.Count)
            {
                languages.AddRange(remainingBonusLanguages);
                return languages;
            }

            while (numberOfBonusLanguages-- > 0 && remainingBonusLanguages.Any())
            {
                var language = collectionsSelector.SelectRandomFrom(remainingBonusLanguages);
                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }

        private bool IsInterpreter(IEnumerable<Skill> skills)
        {
            return skills.Any(s => s.Name == SkillConstants.Profession && s.Focus == SkillConstants.Foci.Profession.Interpreter);
        }
    }
}