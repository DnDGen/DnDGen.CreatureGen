using CreatureGen.Abilities;
using CreatureGen.Creatures;
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
        private readonly IBonusSelector bonusSelector;

        public SavesGenerator(ICollectionSelector collectionsSelector, IBonusSelector bonusSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.bonusSelector = bonusSelector;
        }

        public Dictionary<string, Save> GenerateWith(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var saves = new Dictionary<string, Save>();

            saves[SaveConstants.Fortitude] = GetFortitudeSave(creatureName, creatureType, hitPoints, feats, abilities);
            saves[SaveConstants.Reflex] = GetReflexSave(creatureName, creatureType, hitPoints, feats, abilities);
            saves[SaveConstants.Will] = GetWillSave(creatureName, creatureType, hitPoints, feats, abilities);

            return saves;
        }

        private Save GetFortitudeSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();

            save.BaseAbility = abilities[AbilityConstants.Constitution];
            save.BaseValue = GetSaveBaseValue(creatureType, hitPoints, SaveConstants.Fortitude);

            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Fortitude);
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Fortitude);

            return save;
        }

        private Save GetReflexSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();

            save.BaseAbility = abilities[AbilityConstants.Dexterity];
            save.BaseValue = GetSaveBaseValue(creatureType, hitPoints, SaveConstants.Reflex);

            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Reflex);
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Reflex);

            return save;
        }

        private Save GetWillSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();
            save.BaseValue = GetSaveBaseValue(creatureType, hitPoints, SaveConstants.Will);

            if (feats.Any(f => f.Name == FeatConstants.SpecialQualities.Madness))
                save.BaseAbility = abilities[AbilityConstants.Charisma];
            else
                save.BaseAbility = abilities[AbilityConstants.Wisdom];

            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Will);
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Will);

            return save;
        }

        private int GetSaveBaseValue(CreatureType creatureType, HitPoints hitPoints, string saveName)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return 0;

            var strongSaves = collectionsSelector.SelectFrom(TableNameConstants.Collection.CreatureGroups, saveName);

            if (strongSaves.Contains(creatureType.Name))
                return hitPoints.RoundedHitDiceQuantity / 2 + 2;

            return hitPoints.RoundedHitDiceQuantity / 3;
        }

        private Save GetRacialSavingThrowBonuses(Save save, string creatureName, CreatureType creatureType, string saveName)
        {
            var creatureBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.SaveBonuses, creatureName);
            var creatureTypeBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.SaveBonuses, creatureType.Name);

            var bonuses = creatureBonuses.Union(creatureTypeBonuses);

            foreach (var subtype in creatureType.SubTypes)
            {
                var subtypeBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.SaveBonuses, subtype);
                bonuses = bonuses.Union(subtypeBonuses);
            }

            foreach (var bonus in bonuses)
            {
                if (bonus.Source == FeatConstants.Foci.All || bonus.Source == saveName)
                    save.AddBonus(bonus.Bonus, bonus.Condition);
            }

            return save;
        }

        private Save GetFeatSavingThrowBonuses(Save save, IEnumerable<Feat> feats, string saveName)
        {
            var saveFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, saveName);
            var saveFeats = feats.Where(f => saveFeatNames.Contains(f.Name));

            foreach (var feat in saveFeats)
            {
                save.AddBonus(feat.Power);
            }

            return save;
        }
    }
}