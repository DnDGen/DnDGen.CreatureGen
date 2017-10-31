using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Defenses
{
    internal class SavesGenerator : ISavesGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public SavesGenerator(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public Saves GenerateWith(string creatureName, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var saves = new Saves();

            saves.Constitution = abilities[AbilityConstants.Constitution];
            saves.Dexterity = abilities[AbilityConstants.Dexterity];
            saves.Wisdom = abilities[AbilityConstants.Wisdom];

            saves.FeatFortitudeBonus = GetFeatSavingThrowBonus(feats, SaveConstants.Fortitude);
            saves.FeatReflexBonus = GetFeatSavingThrowBonus(feats, SaveConstants.Reflex);
            saves.FeatWillBonus = GetFeatSavingThrowBonus(feats, SaveConstants.Will);

            saves.RacialFortitudeBonus = GetRacialSavingThrowBonus(creatureName, SaveConstants.Fortitude);
            saves.RacialReflexBonus = GetRacialSavingThrowBonus(creatureName, SaveConstants.Reflex);
            saves.RacialWillBonus = GetRacialSavingThrowBonus(creatureName, SaveConstants.Will);

            return saves;
        }

        private int GetRacialSavingThrowBonus(string creatureName, string saveName)
        {
            var saveBonuses = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.SaveBonuses, creatureName);
            var bonus = saveBonuses.First(b => b.Type == saveName);

            return bonus.Amount;
        }

        private int GetBonus(Feat feat, string savingThrow)
        {
            if (feat.Foci.Contains(FeatConstants.Foci.All) || feat.Foci.Contains(savingThrow))
                return feat.Power;

            return 0;
        }

        private int GetFeatSavingThrowBonus(IEnumerable<Feat> feats, string saveName)
        {
            var saveFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, saveName);
            var saveFeats = feats.Where(f => saveFeatNames.Contains(f.Name));

            var bonus = saveFeats.Sum(f => f.Power);

            var anySavingThrowFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SavingThrows);
            var anySavingThrowFeats = feats.Where(f => anySavingThrowFeatNames.Contains(f.Name));

            foreach (var feat in anySavingThrowFeats)
            {
                bonus += GetBonus(feat, saveName);
            }

            return saveFeats.Sum(f => f.Power);
        }
    }
}