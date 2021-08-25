﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Magics;
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
    internal class SkeletonApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly Dice dice;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly IEnumerable<string> creatureTypes;
        private readonly IEnumerable<string> invalidSubtypes;

        public SkeletonApplicator(
            ICollectionSelector collectionSelector,
            IAdjustmentsSelector adjustmentSelector,
            Dice dice,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISavesGenerator savesGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.dice = dice;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.savesGenerator = savesGenerator;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Elemental,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Vermin,
            };

            invalidSubtypes = new[]
            {
                CreatureConstants.Types.Subtypes.Angel,
                CreatureConstants.Types.Subtypes.Archon,
                CreatureConstants.Types.Subtypes.Chaotic,
                CreatureConstants.Types.Subtypes.Dwarf,
                CreatureConstants.Types.Subtypes.Elf,
                CreatureConstants.Types.Subtypes.Evil,
                CreatureConstants.Types.Subtypes.Gnoll,
                CreatureConstants.Types.Subtypes.Gnome,
                CreatureConstants.Types.Subtypes.Goblinoid,
                CreatureConstants.Types.Subtypes.Good,
                CreatureConstants.Types.Subtypes.Halfling,
                CreatureConstants.Types.Subtypes.Human,
                CreatureConstants.Types.Subtypes.Lawful,
                CreatureConstants.Types.Subtypes.Orc,
                CreatureConstants.Types.Subtypes.Reptilian,
                CreatureConstants.Types.Subtypes.Shapechanger,
            };
        }

        public Creature ApplyTo(Creature creature)
        {
            // Template
            UpdateCreatureTemplate(creature);

            //Type
            UpdateCreatureType(creature);

            //Abilities
            UpdateCreatureAbilities(creature);

            //Speed
            UpdateCreatureSpeeds(creature);

            //Challenge Rating
            UpdateCreatureChallengeRating(creature);

            //Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            //Skills
            UpdateCreatureSkills(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //Alignment
            UpdateCreatureAlignment(creature);

            //Magic
            UpdateCreatureMagic(creature);

            //INFO: Depends on abilities
            //Hit Points
            UpdateCreatureHitPoints(creature);

            //INFO: Depends on type, hit points, abilities, skills, alignment
            //Special Qualities
            UpdateCreatureSpecialQualitiesAndFeats(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Attacks
            UpdateCreatureAttacks(creature);

            //INFO: Depends on special qualities + feats
            //Initiative Bonus
            UpdateCreatureInitiativeBonus(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Saves
            UpdateCreatureSaves(creature);

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
                .Except(invalidSubtypes);
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

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment = 2;
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;
            creature.Abilities[AbilityConstants.Intelligence].TemplateScore = 0;
            creature.Abilities[AbilityConstants.Wisdom].TemplateScore = 10;
            creature.Abilities[AbilityConstants.Charisma].TemplateScore = 1;
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            if (creature.Speeds.ContainsKey(SpeedConstants.Fly))
            {
                if (creature.Speeds[SpeedConstants.Fly].Description.ToLower().Contains("wings"))
                {
                    creature.Speeds.Remove(SpeedConstants.Fly);
                }
            }
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.HitPoints.HitDiceQuantity, creature.Name);
        }

        private string UpdateCreatureChallengeRating(double hitDiceQuantity, string creature)
        {
            if (hitDiceQuantity <= 0.5)
            {
                return ChallengeRatingConstants.CR1_6th;
            }
            else if (hitDiceQuantity <= 1)
            {
                return ChallengeRatingConstants.CR1_3rd;
            }
            else if (hitDiceQuantity <= 3)
            {
                return ChallengeRatingConstants.CR1;
            }
            else if (hitDiceQuantity <= 5)
            {
                return ChallengeRatingConstants.CR2;
            }
            else if (hitDiceQuantity <= 7)
            {
                return ChallengeRatingConstants.CR3;
            }
            else if (hitDiceQuantity <= 9)
            {
                return ChallengeRatingConstants.CR4;
            }
            else if (hitDiceQuantity <= 11)
            {
                return ChallengeRatingConstants.CR5;
            }
            else if (hitDiceQuantity <= 14)
            {
                return ChallengeRatingConstants.CR6;
            }
            else if (hitDiceQuantity <= 17)
            {
                return ChallengeRatingConstants.CR7;
            }
            else if (hitDiceQuantity <= 20)
            {
                return ChallengeRatingConstants.CR8;
            }

            throw new ArgumentException($"Skeleton hit dice cannot be greater than 20, but was {hitDiceQuantity} for creature {creature}");
        }

        public IEnumerable<string> GetChallengeRatings() => new[]
        {
            ChallengeRatingConstants.CR1_6th,
            ChallengeRatingConstants.CR1_3rd,
            ChallengeRatingConstants.CR1,
            ChallengeRatingConstants.CR2,
            ChallengeRatingConstants.CR3,
            ChallengeRatingConstants.CR4,
            ChallengeRatingConstants.CR5,
            ChallengeRatingConstants.CR6,
            ChallengeRatingConstants.CR7,
            ChallengeRatingConstants.CR8,
        };
        public IEnumerable<string> GetChallengeRatings(string challengeRating) => GetChallengeRatings();

        public (double? Lower, double? Upper) GetHitDiceRange(string challengeRating)
        {
            switch (challengeRating)
            {
                case ChallengeRatingConstants.CR1_6th: return (0, 0.5);
                case ChallengeRatingConstants.CR1_3rd: return (0.5, 1);
                case ChallengeRatingConstants.CR1: return (1, 3);
                case ChallengeRatingConstants.CR2: return (3, 5);
                case ChallengeRatingConstants.CR3: return (5, 7);
                case ChallengeRatingConstants.CR4: return (7, 9);
                case ChallengeRatingConstants.CR5: return (9, 11);
                case ChallengeRatingConstants.CR6: return (11, 14);
                case ChallengeRatingConstants.CR7: return (14, 17);
                case ChallengeRatingConstants.CR8: return (17, 20);
                default: throw new ArgumentException($"Skeleton challenge rating cannot be less than CR 1/6 or greater than CR 8, but was {challengeRating}");
            }
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            creature.LevelAdjustment = null;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            creature.Skills = Enumerable.Empty<Skill>();
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

        private void UpdateCreatureAttacks(Creature creature)
        {
            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creature.Type, creature.HitPoints);

            var skeletonAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Skeleton,
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            skeletonAttacks = attacksGenerator.ApplyAttackBonuses(skeletonAttacks, creature.SpecialQualities, creature.Abilities);
            var newClaw = skeletonAttacks.First(a => a.Name == "Claw");

            if (creature.Attacks.Any(a => a.Name == "Claw"))
            {
                var oldClaw = creature.Attacks.First(a => a.Name == "Claw");

                var oldMax = dice.Roll(oldClaw.Damages[0].Roll).AsPotentialMaximum();
                var newMax = dice.Roll(newClaw.Damages[0].Roll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldClaw.Damages.Clear();
                    oldClaw.Damages.Add(newClaw.Damages[0]);
                }

                skeletonAttacks = skeletonAttacks.Except(new[] { newClaw });
            }
            else
            {
                newClaw.Frequency.Quantity = creature.NumberOfHands;

                if (creature.NumberOfHands == 0)
                {
                    skeletonAttacks = skeletonAttacks.Except(new[] { newClaw });
                }
            }

            creature.Attacks = creature.Attacks
                .Union(skeletonAttacks)
                .Where(a => !a.IsSpecial);
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            creature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);
            var naturalArmorBonus = 0;

            switch (creature.Size)
            {
                case SizeConstants.Colossal: naturalArmorBonus = 10; break;
                case SizeConstants.Gargantuan: naturalArmorBonus = 6; break;
                case SizeConstants.Huge: naturalArmorBonus = 3; break;
                case SizeConstants.Large:
                case SizeConstants.Medium: naturalArmorBonus = 2; break;
                case SizeConstants.Small: naturalArmorBonus = 1; break;
                default: break;
            }

            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, naturalArmorBonus);
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Lawfulness = AlignmentConstants.Neutral;
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureMagic(Creature creature)
        {
            creature.Magic = new Magic();
        }

        private void UpdateCreatureSaves(Creature creature)
        {
            creature.Saves = savesGenerator.GenerateWith(
                CreatureConstants.Templates.Skeleton,
                creature.Type,
                creature.HitPoints,
                creature.SpecialQualities,
                creature.Abilities);
        }

        private void UpdateCreatureSpecialQualitiesAndFeats(Creature creature)
        {
            var featNamesToKeep = new List<string>();
            featNamesToKeep.Add(FeatConstants.SpecialQualities.AttackBonus);

            var weaponProficiencies = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            featNamesToKeep.AddRange(weaponProficiencies);

            var armorProficiencies = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency);
            featNamesToKeep.AddRange(armorProficiencies);

            var skeletonQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Skeleton,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities
                .Where(sq => featNamesToKeep.Contains(sq.Name))
                .Union(skeletonQualities);

            creature.Feats = creature.Feats.Where(f => featNamesToKeep.Contains(f.Name));
        }

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = CreatureConstants.Templates.Skeleton;
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

            //Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

            //Challenge Rating
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature));
            tasks.Add(challengeRatingTask);

            //Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            //Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
            tasks.Add(alignmentTask);

            //Magic
            var magicTask = Task.Run(() => UpdateCreatureMagic(creature));
            tasks.Add(magicTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on abilities
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature));
            tasks.Add(hitPointTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on type, hit points, abilities, skills, alignment
            //Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualitiesAndFeats(creature));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            //INFO: Depends on special qualities + feats
            //Initiative Bonus
            var initiativeTask = Task.Run(() => UpdateCreatureInitiativeBonus(creature));
            tasks.Add(initiativeTask);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Saves
            var saveTask = Task.Run(() => UpdateCreatureSaves(creature));
            tasks.Add(saveTask);

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
                if (ChallengeRatingConstants.IsGreaterThan(challengeRating, ChallengeRatingConstants.CR8)
                    || ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR1_6th, challengeRating))
                {
                    return false;
                }

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
                return false;

            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            var hasSkeleton = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton);
            if (!hasSkeleton.Contains(creature))
                return false;

            var hitDice = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            if (hitDice > 20)
                return false;

            return true;
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
            var quantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            var adjustedChallengeRating = UpdateCreatureChallengeRating(quantity, creature);

            return adjustedChallengeRating;
        }
    }
}
