using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class HalfFiendApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IEnumerable<string> creatureTypes;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly Dice dice;

        public HalfFiendApplicator(
            ICollectionSelector collectionSelector,
            ITypeAndAmountSelector typeAndAmountSelector,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISkillsGenerator skillsGenerator,
            Dice dice)
        {
            this.collectionSelector = collectionSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.skillsGenerator = skillsGenerator;
            this.dice = dice;

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
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };
        }

        public Creature ApplyTo(Creature creature)
        {
            // Creature type
            UpdateCreatureType(creature);

            // Challenge ratings
            UpdateCreatureChallengeRating(creature);

            //Speed
            UpdateCreatureSpeeds(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Alignment
            UpdateCreatureAlignment(creature);

            // Languages
            UpdateCreatureLanguages(creature);

            // Special Qualities
            UpdateCreatureSpecialQualities(creature);

            //Skills
            UpdateCreatureSkills(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            // Attacks
            UpdateCreatureAttacks(creature);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.Name = CreatureConstants.Types.Outsider;
            creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
            {
                CreatureConstants.Types.Subtypes.Native
            });
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            var speeds = speedsGenerator.Generate(CreatureConstants.Templates.HalfFiend);

            if (!creature.Speeds.ContainsKey(SpeedConstants.Fly)
                || creature.Speeds[SpeedConstants.Land].Value > creature.Speeds[SpeedConstants.Fly].Value)
            {
                creature.Speeds[SpeedConstants.Fly] = speeds[SpeedConstants.Fly];
                creature.Speeds[SpeedConstants.Fly].Value = creature.Speeds[SpeedConstants.Land].Value;
            }
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                naturalArmorBonus.Value++;
            }

            if (!creature.ArmorClass.NaturalArmorBonuses.Any())
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 1);
            }
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Strength].HasScore)
                creature.Abilities[AbilityConstants.Strength].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Dexterity].HasScore)
                creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Constitution].HasScore)
                creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Charisma].HasScore)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 2;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = challengeRatings.ToList().IndexOf(creature.ChallengeRating);

            if (creature.HitPoints.HitDiceQuantity >= 11)
            {
                creature.ChallengeRating = challengeRatings[index + 3];
            }
            else if (creature.HitPoints.HitDiceQuantity >= 5)
            {
                creature.ChallengeRating = challengeRatings[index + 2];
            }
            else
            {
                creature.ChallengeRating = challengeRatings[index + 1];
            }
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 4;
        }

        private void UpdateCreatureLanguages(Creature creature)
        {
            if (!creature.Languages.Any())
            {
                return;
            }

            var languages = new List<string>(creature.Languages);
            var automaticLanguage = collectionSelector.SelectRandomFrom(
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic);

            languages.Add(automaticLanguage);

            var bonusLanguages = collectionSelector.SelectFrom(
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus);
            var quantity = Math.Min(2, creature.Abilities[AbilityConstants.Intelligence].Modifier);
            var availableBonusLanguages = bonusLanguages.Except(languages);

            if (availableBonusLanguages.Count() <= quantity && quantity > 0)
            {
                languages.AddRange(availableBonusLanguages);
            }

            while (quantity-- > 0 && availableBonusLanguages.Any())
            {
                var bonusLanguage = collectionSelector.SelectRandomFrom(availableBonusLanguages);
                languages.Add(bonusLanguage);
            }

            creature.Languages = languages.Distinct();
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            foreach (var skill in creature.Skills)
            {
                skill.Ranks = 0;
            }

            creature.Skills = skillsGenerator.ApplySkillPointsAsRanks(creature.Skills, creature.HitPoints, creature.Type, creature.Abilities);
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var attacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.HalfFiend,
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            attacks = attacksGenerator.ApplyAttackBonuses(attacks, allFeats, creature.Abilities);

            var smiteEvil = attacks.First(a => a.Name == "Smite Good");
            smiteEvil.DamageRoll = Math.Min(creature.HitPoints.RoundedHitDiceQuantity, 20).ToString();

            if (creature.Attacks.Any(a => a.Name == "Claw"))
            {
                var oldClaw = creature.Attacks.First(a => a.Name == "Claw");
                var newClaw = attacks.First(a => a.Name == "Claw");

                var oldMax = dice.Roll(oldClaw.DamageRoll).AsPotentialMaximum();
                var newMax = dice.Roll(newClaw.DamageRoll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldClaw.DamageRoll = newClaw.DamageRoll;
                }

                attacks = attacks.Except(new[] { newClaw });
            }

            if (creature.Attacks.Any(a => a.Name == "Bite"))
            {
                var oldBite = creature.Attacks.First(a => a.Name == "Bite");
                var newBite = attacks.First(a => a.Name == "Bite");

                var oldMax = dice.Roll(oldBite.DamageRoll).AsPotentialMaximum();
                var newMax = dice.Roll(newBite.DamageRoll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldBite.DamageRoll = newBite.DamageRoll;
                }

                attacks = attacks.Except(new[] { newBite });
            }

            creature.Attacks = creature.Attacks.Union(attacks);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var specialQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.HalfFiend,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            foreach (var sq in specialQualities)
            {
                var matching = creature.SpecialQualities.FirstOrDefault(f =>
                    f.Name == sq.Name
                    && !f.Foci.Except(sq.Foci).Any()
                    && !sq.Foci.Except(f.Foci).Any());

                if (matching == null)
                {
                    creature.SpecialQualities = creature.SpecialQualities.Union(new[] { sq });
                }
                else if (matching.Power < sq.Power)
                {
                    matching.Power = sq.Power;
                }
            }
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var tasks = new List<Task>();

            // Creature type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Challenge ratings
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature));
            tasks.Add(challengeRatingTask);

            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
            tasks.Add(alignmentTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            // Languages
            var languageTask = Task.Run(() => UpdateCreatureLanguages(creature));
            tasks.Add(languageTask);

            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature));
            tasks.Add(qualityTask);

            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            // Attacks
            await Task.Run(() => UpdateCreatureAttacks(creature));

            return creature;
        }

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            if (!creatureTypes.Contains(types.First()))
                return false;

            var alignments = collectionSelector.SelectFrom(TableNameConstants.Collection.AlignmentGroups, creature);
            if (!alignments.Any(a => !a.Contains(AlignmentConstants.Good)))
                return false;

            var abilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, creature);
            var intelligenceAdjustment = abilityAdjustments.FirstOrDefault(a => a.Type == AbilityConstants.Intelligence);
            if (intelligenceAdjustment == null)
                return false;

            return intelligenceAdjustment.Amount + Ability.DefaultScore >= 4;
        }
    }
}
