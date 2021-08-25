using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class LichApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly Dice dice;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        private const int PhylacterySpellLevel = 11;

        public LichApplicator(
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            Dice dice,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.dice = dice;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public Creature ApplyTo(Creature creature)
        {
            // Template
            UpdateCreatureTemplate(creature);

            //Type
            UpdateCreatureType(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Alignment
            UpdateCreatureAlignment(creature);

            // Languages
            UpdateCreatureLanguages(creature);

            //Challenge Rating
            UpdateCreatureChallengeRating(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            //Hit Points
            UpdateCreatureHitPoints(creature);

            //Skills
            UpdateCreatureSkills(creature);

            //Special Qualities
            UpdateCreatureSpecialQualities(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //Attacks
            UpdateCreatureAttacks(creature);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);

            creature.Type.Name = adjustedTypes.First();
            creature.Type.SubTypes = adjustedTypes.Skip(1);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Undead }
                .Union(subtypes)
                .Union(new[] { CreatureConstants.Types.Subtypes.Augmented, creatureType });
        }

        private void UpdateCreatureHitPoints(Creature creature)
        {
            foreach (var hitDice in creature.HitPoints.HitDice)
            {
                hitDice.HitDie = 12;
            }

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);
        }

        private void UpdateCreatureLanguages(Creature creature)
        {
            if (!creature.Languages.Any())
            {
                return;
            }

            var automaticLanguage = collectionSelector.SelectRandomFrom(
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic);

            creature.Languages = creature.Languages.Union(new[] { automaticLanguage });
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 4;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;

            if (creature.Abilities[AbilityConstants.Wisdom].HasScore)
                creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Charisma].HasScore)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 2;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            var lichSkills = new[]
            {
                SkillConstants.Hide,
                SkillConstants.Listen,
                SkillConstants.MoveSilently,
                SkillConstants.Search,
                SkillConstants.SenseMotive,
                SkillConstants.Spot
            };

            foreach (var skill in creature.Skills)
            {
                if (lichSkills.Contains(skill.Name))
                {
                    skill.AddBonus(8);
                }
            }

            var concentration = creature.Skills.FirstOrDefault(s => s.Name == SkillConstants.Concentration);
            if (concentration != null)
            {
                concentration.BaseAbility = creature.Abilities[AbilityConstants.Charisma];
            }
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private string UpdateCreatureChallengeRating(string challengeRating)
        {
            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
        }

        public (string Lower, string Upper) GetChallengeRatingRange(string challengeRating) => (
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2),
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2));
        //public (string Lower, string Upper) GetChallengeRatingRange() => (null, null);
        public (double? Lower, double? Upper) GetHitDiceRange(string challengeRating) => (null, null);

        private void UpdateCreatureAttacks(Creature creature)
        {
            var lichAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Lich,
                creature.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            lichAttacks = attacksGenerator.ApplyAttackBonuses(lichAttacks, allFeats, creature.Abilities);

            creature.Attacks = creature.Attacks.Union(lichAttacks);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var lichQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Lich,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities.Union(lichQualities);
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 5);
        }

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = CreatureConstants.Templates.Lich;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

            //Type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
            tasks.Add(alignmentTask);

            // Languages
            var languageTask = Task.Run(() => UpdateCreatureLanguages(creature));
            tasks.Add(languageTask);

            //Challenge Rating
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature));
            tasks.Add(challengeRatingTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature));
            tasks.Add(hitPointTask);

            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            //Special Qualities
            var specialQualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature));
            tasks.Add(specialQualityTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //Attacks
            await Task.Run(() => UpdateCreatureAttacks(creature));

            return creature;
        }

        public bool IsCompatible(string creature, string type = null, string challengeRating = null)
        {
            if (!IsCompatible(creature))
                return false;

            if (!string.IsNullOrEmpty(type))
            {
                var types = GetPotentialTypes(creature);
                if (!types.Contains(type))
                    return false;
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var cr = GetPotentialChallengeRating(creature);
                if (cr != challengeRating)
                    return false;
            }

            return true;
        }

        private bool IsCompatible(string creature)
        {
            var creatureTypes = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (creatureTypes.First() != CreatureConstants.Types.Humanoid)
            {
                return false;
            }

            var creatureData = creatureDataSelector.SelectFor(creature);
            if (creatureData.LevelAdjustment.HasValue)
            {
                return true;
            }

            if (creatureData.CasterLevel >= PhylacterySpellLevel)
            {
                return true;
            }

            var spellcasters = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Casters, creature);
            if (spellcasters.Any(s => s.Amount >= PhylacterySpellLevel))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<string> GetPotentialTypes(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            var creatureType = types.First();
            var subtypes = types.Skip(1);

            var adjustedTypes = UpdateCreatureType(creatureType, subtypes);

            return adjustedTypes;
        }

        public string GetPotentialChallengeRating(string creature)
        {
            var data = creatureDataSelector.SelectFor(creature);
            var adjustedChallengeRating = UpdateCreatureChallengeRating(data.ChallengeRating);

            return adjustedChallengeRating;
        }
    }
}
