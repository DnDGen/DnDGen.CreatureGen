using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Feats;
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
    internal class VampireApplicator : TemplateApplicator
    {
        private readonly Dice dice;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ICollectionSelector collectionSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly IEnumerable<string> creatureTypes;

        public VampireApplicator(
            Dice dice,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            IAdjustmentsSelector adjustmentSelector)
        {
            this.dice = dice;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.adjustmentSelector = adjustmentSelector;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            };
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

            //INFO: Depends on special qualities + feats
            //Initiative Bonus
            UpdateCreatureInitiativeBonus(creature);

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

        private void UpdateCreatureInitiativeBonus(Creature creature)
        {
            var allFeats = creature.SpecialQualities.Union(creature.Feats);
            var improvedInitiative = allFeats.FirstOrDefault(f => f.Name == FeatConstants.Initiative_Improved);
            if (improvedInitiative != null)
            {
                creature.InitiativeBonus = improvedInitiative.Power;
            }
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

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 8;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;

            if (creature.Abilities[AbilityConstants.Strength].HasScore)
                creature.Abilities[AbilityConstants.Strength].TemplateAdjustment = 6;

            if (creature.Abilities[AbilityConstants.Dexterity].HasScore)
                creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Wisdom].HasScore)
                creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Charisma].HasScore)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 4;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            var vampireSkills = new[]
            {
                SkillConstants.Bluff,
                SkillConstants.Hide,
                SkillConstants.Listen,
                SkillConstants.MoveSilently,
                SkillConstants.Search,
                SkillConstants.SenseMotive,
                SkillConstants.Spot
            };

            foreach (var skill in creature.Skills)
            {
                if (vampireSkills.Contains(skill.Name))
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

        private void UpdateCreatureAttacks(Creature creature)
        {
            var vampireAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Vampire,
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            vampireAttacks = attacksGenerator.ApplyAttackBonuses(vampireAttacks, allFeats, creature.Abilities);

            if (creature.Attacks.Any(a => a.Name == "Slam"))
            {
                var oldSlam = creature.Attacks.First(a => a.Name == "Slam");
                var newSlam = vampireAttacks.First(a => a.Name == "Slam");

                var oldMax = dice.Roll(oldSlam.Damages[0].Roll).AsPotentialMaximum();
                var newMax = dice.Roll(newSlam.Damages[0].Roll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldSlam.Damages.Clear();
                    oldSlam.Damages.Add(newSlam.Damages[0]);
                }

                vampireAttacks = vampireAttacks.Except(new[] { newSlam });
            }

            creature.Attacks = creature.Attacks.Union(vampireAttacks);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var vampireQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Vampire,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities.Union(vampireQualities);
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var bonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                bonus.Value += 6;
            }

            if (!creature.ArmorClass.NaturalArmorBonuses.Any())
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 6);
            }
        }

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = CreatureConstants.Templates.Vampire;
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

            //INFO: Depends on special qualities + feats
            //Initiative Bonus
            var initiativeTask = Task.Run(() => UpdateCreatureInitiativeBonus(creature));
            tasks.Add(initiativeTask);

            //Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

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
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (!creatureTypes.Contains(types.First()))
            {
                return false;
            }

            var creatureData = creatureDataSelector.SelectFor(creature);
            if (creatureData.LevelAdjustment.HasValue)
            {
                return true;
            }

            var hitDice = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            if (hitDice >= 5)
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
