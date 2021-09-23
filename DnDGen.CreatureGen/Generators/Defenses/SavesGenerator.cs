using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Defenses
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

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Generating Fortitude save for {creatureName}");
            saves[SaveConstants.Fortitude] = GetFortitudeSave(creatureName, creatureType, hitPoints, feats, abilities);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Generating Reflex save for {creatureName}");
            saves[SaveConstants.Reflex] = GetReflexSave(creatureName, creatureType, hitPoints, feats, abilities);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Generating Will save for {creatureName}");
            saves[SaveConstants.Will] = GetWillSave(creatureName, creatureType, hitPoints, feats, abilities);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Generated saves for {creatureName}");
            return saves;
        }

        private Save GetFortitudeSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();

            save.BaseAbility = abilities[AbilityConstants.Constitution];

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Fortitude base save value");
            save.BaseValue = GetSaveBaseValue(creatureName, hitPoints, SaveConstants.Fortitude);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Fortitude racial saving throw bonuses");
            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Fortitude);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Fortitude feat saving throw bonuses");
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Fortitude, abilities);

            return save;
        }

        private Save GetReflexSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();

            save.BaseAbility = abilities[AbilityConstants.Dexterity];

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Reflex base save value");
            save.BaseValue = GetSaveBaseValue(creatureName, hitPoints, SaveConstants.Reflex);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Reflex racial saving throw bonuses");
            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Reflex);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Reflex feat saving throw bonuses");
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Reflex, abilities);

            return save;
        }

        private Save GetWillSave(string creatureName, CreatureType creatureType, HitPoints hitPoints, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var save = new Save();

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Will base save value");
            save.BaseValue = GetSaveBaseValue(creatureName, hitPoints, SaveConstants.Will);

            if (feats.Any(f => f.Name == FeatConstants.SpecialQualities.Madness))
                save.BaseAbility = abilities[AbilityConstants.Charisma];
            else
                save.BaseAbility = abilities[AbilityConstants.Wisdom];

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Will racial saving throw bonuses");
            save = GetRacialSavingThrowBonuses(save, creatureName, creatureType, SaveConstants.Will);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Getting Will feat saving throw bonuses");
            save = GetFeatSavingThrowBonuses(save, feats, SaveConstants.Will, abilities);

            return save;
        }

        private int GetSaveBaseValue(string creatureName, HitPoints hitPoints, string saveName)
        {
            if (hitPoints.RoundedHitDiceQuantity == 0)
                return 0;

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Exploding group of strong save {saveName}");
            var strongSaves = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, saveName);

            Console.WriteLine($"[{DateTime.Now:O}] SavesGenerator: Computing base save value for save {saveName} for creature {creatureName}");
            if (strongSaves.Contains(creatureName))
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
                if (bonus.Target == GroupConstants.All || bonus.Target == saveName)
                    save.AddBonus(bonus.Bonus, bonus.Condition);
            }

            return save;
        }

        private Save GetFeatSavingThrowBonuses(Save save, IEnumerable<Feat> feats, string saveName, Dictionary<string, Ability> abilities)
        {
            var saveFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, saveName);
            var saveFeats = feats.Where(f => saveFeatNames.Contains(f.Name));

            foreach (var feat in saveFeats)
            {
                save.AddBonus(feat.Power);
            }

            if (feats.Any(f => f.Name == FeatConstants.SpecialQualities.UnearthlyGrace))
            {
                save.AddBonus(abilities[AbilityConstants.Charisma].Modifier);
            }

            return save;
        }
    }
}