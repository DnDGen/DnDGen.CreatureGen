using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
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

namespace DnDGen.CreatureGen.Templates.HalfDragons
{
    internal abstract class HalfDragonApplicator : TemplateApplicator
    {
        protected abstract string DragonSpecies { get; }

        private readonly ICollectionSelector collectionSelector;
        private readonly IEnumerable<string> creatureTypes;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly Dice dice;
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ICreatureDataSelector creatureDataSelector;

        public HalfDragonApplicator(
            ICollectionSelector collectionSelector,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISkillsGenerator skillsGenerator,
            IAlignmentGenerator alignmentGenerator,
            Dice dice,
            IMagicGenerator magicGenerator,
            ICreatureDataSelector creatureDataSelector)
        {
            this.collectionSelector = collectionSelector;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.skillsGenerator = skillsGenerator;
            this.alignmentGenerator = alignmentGenerator;
            this.dice = dice;
            this.magicGenerator = magicGenerator;
            this.creatureDataSelector = creatureDataSelector;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
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
            // Template
            UpdateCreatureTemplate(creature);

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

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //INFO: This depends on abilities
            //Hit Points
            UpdateCreatureHitPoints(creature);

            //INFO: This depends on alignment, abilities
            // Magic
            UpdateCreatureMagic(creature);

            //INFO: This depends on abilities
            // Languages
            UpdateCreatureLanguages(creature);

            //INFO: This depends on hit points
            //Skills
            UpdateCreatureSkills(creature);

            //INFO: This depends on skills
            // Special Qualities
            UpdateCreatureSpecialQualities(creature);

            //INFO: This depends on special qualities
            // Attacks
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
            return new[] { CreatureConstants.Types.Dragon }
                .Union(subtypes)
                .Union(new[] { CreatureConstants.Types.Subtypes.Augmented, creatureType });
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            var sizes = SizeConstants.GetOrdered();
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);
            var sizeIndex = Array.IndexOf(sizes, creature.Size);

            if (sizeIndex >= largeIndex
                && creature.Speeds.ContainsKey(SpeedConstants.Land)
                && !creature.Speeds.ContainsKey(SpeedConstants.Fly))
            {
                var dragonSpeeds = speedsGenerator.Generate(DragonSpecies);

                var dragonFlySpeedValue = Math.Min(120, creature.Speeds[SpeedConstants.Land].Value * 2);
                creature.Speeds[SpeedConstants.Fly] = dragonSpeeds[SpeedConstants.Fly];
                creature.Speeds[SpeedConstants.Fly].Value = dragonFlySpeedValue;
            }
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
                DragonSpecies + LanguageConstants.Groups.Automatic);

            languages.Add(automaticLanguage);

            var bonusLanguages = collectionSelector.SelectFrom(
                TableNameConstants.Collection.LanguageGroups,
                DragonSpecies + LanguageConstants.Groups.Bonus);
            var quantity = Math.Min(1, creature.Abilities[AbilityConstants.Intelligence].Modifier);
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

        private void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                naturalArmorBonus.Value += 4;
            }

            if (!creature.ArmorClass.NaturalArmorBonuses.Any())
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 4);
            }
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Strength].HasScore)
                creature.Abilities[AbilityConstants.Strength].TemplateAdjustment = 8;

            if (creature.Abilities[AbilityConstants.Constitution].HasScore)
                creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Charisma].HasScore)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 2;
        }

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = DragonSpecies;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment = alignmentGenerator.Generate(DragonSpecies);
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private string UpdateCreatureChallengeRating(string challengeRating)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = Array.IndexOf(challengeRatings, challengeRating);
            var threeIndex = Array.IndexOf(challengeRatings, ChallengeRatingConstants.CR3);

            if (index > -1 && index + 2 < threeIndex)
            {
                return ChallengeRatingConstants.CR3;
            }

            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 3;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            foreach (var skill in creature.Skills)
            {
                skill.Ranks = 0;
            }

            creature.Skills = skillsGenerator.ApplySkillPointsAsRanks(creature.Skills, creature.HitPoints, creature.Type, creature.Abilities, true);
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var attacks = attacksGenerator.GenerateAttacks(
                DragonSpecies,
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            attacks = attacksGenerator.ApplyAttackBonuses(attacks, allFeats, creature.Abilities);

            if (creature.Attacks.Any(a => a.Name == "Claw"))
            {
                var oldClaw = creature.Attacks.First(a => a.Name == "Claw");
                var newClaw = attacks.First(a => a.Name == "Claw");

                //Claw attacks only ever do a single damage roll
                var oldMax = dice.Roll(oldClaw.Damages[0].Roll).AsPotentialMaximum();
                var newMax = dice.Roll(newClaw.Damages[0].Roll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldClaw.Damages.Clear();
                    oldClaw.Damages.Add(newClaw.Damages[0]);
                }

                attacks = attacks.Except(new[] { newClaw });
            }

            if (creature.Attacks.Any(a => a.Name == "Bite"))
            {
                var oldBite = creature.Attacks.First(a => a.Name == "Bite");
                var newBite = attacks.First(a => a.Name == "Bite");

                //Bite attacks only ever do a single damage roll
                var oldMax = dice.Roll(oldBite.Damages[0].Roll).AsPotentialMaximum();
                var newMax = dice.Roll(newBite.Damages[0].Roll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldBite.Damages.Clear();
                    oldBite.Damages.Add(newBite.Damages[0]);
                }

                attacks = attacks.Except(new[] { newBite });
            }

            creature.Attacks = creature.Attacks.Union(attacks);
        }

        private void UpdateCreatureMagic(Creature creature)
        {
            creature.Magic = magicGenerator.GenerateWith(creature.Name, creature.Alignment, creature.Abilities, creature.Equipment);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var specialQualities = featsGenerator.GenerateSpecialQualities(
                DragonSpecies,
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

        private void UpdateCreatureHitPoints(Creature creature)
        {
            foreach (var hitDice in creature.HitPoints.HitDice)
            {
                hitDice.HitDie = Math.Min(hitDice.HitDie + 2, 12);
            }

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

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

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on abilities
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature));
            tasks.Add(hitPointTask);

            //INFO: This depends on alignment, abilities
            // Magic
            var magicTask = Task.Run(() => UpdateCreatureMagic(creature));
            tasks.Add(magicTask);

            //INFO: This depends on abilities
            // Languages
            var languageTask = Task.Run(() => UpdateCreatureLanguages(creature));
            tasks.Add(languageTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on hit points
            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on skills
            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on special qualities
            // Attacks
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
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            if (!creatureTypes.Contains(types.First()))
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
            var data = creatureDataSelector.SelectFor(creature);
            var adjustedChallengeRating = UpdateCreatureChallengeRating(data.ChallengeRating);

            return adjustedChallengeRating;
        }
    }
}
