﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class HalfDragonApplicator : TemplateApplicator
    {
        public string DragonSpecies { get; set; }

        private readonly ICollectionSelector collectionSelector;
        private readonly IEnumerable<string> creatureTypes;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly Dice dice;
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly ICreaturePrototypeFactory prototypeFactory;
        private readonly IDemographicsGenerator demographicsGenerator;

        public HalfDragonApplicator(
            ICollectionSelector collectionSelector,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISkillsGenerator skillsGenerator,
            IAlignmentGenerator alignmentGenerator,
            Dice dice,
            IMagicGenerator magicGenerator,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            ICreaturePrototypeFactory prototypeFactory,
            IDemographicsGenerator demographicsGenerator)
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
            this.prototypeFactory = prototypeFactory;
            this.demographicsGenerator = demographicsGenerator;

            creatureTypes =
            [
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
            ];
        }

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var dragonAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, DragonSpecies);
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                dragonAlignments,
                creature.ChallengeRating,
                filters);

            if (!compatibility.Compatible)
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([DragonSpecies])]);

            // Template
            UpdateCreatureTemplate(creature);

            // Creature type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

            // Challenge ratings
            UpdateCreatureChallengeRating(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Alignment
            UpdateCreatureAlignment(creature, filters?.Alignment);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //INFO: This depends on demographics
            //Speed
            UpdateCreatureSpeeds(creature);

            //INFO: This depends on abilities
            //Hit Points
            UpdateCreatureHitPoints(creature);

            //INFO: This depends on alignment, abilities
            // Magic
            UpdateCreatureMagic(creature);

            //INFO: This depends on abilities
            // Languages
            UpdateCreatureLanguages(creature);

            //INFO: This depends on hit points, abilities
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
            creature.Type = new CreatureType(adjustedTypes);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            var addWings = IsAtLeastLarge(creature.Size) && creature.Speeds.ContainsKey(SpeedConstants.Land);

            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, DragonSpecies, addWings);

            if (addWings && !creature.Demographics.Other.Contains("wing", StringComparison.OrdinalIgnoreCase))
            {
                var separator = DemographicsGenerator.GetAppearanceSeparator(creature.Demographics.Other);
                creature.Demographics.Other += $"{separator}Has dragon wings.";
            }
        }

        private void UpdateCreatureType(CreaturePrototype creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private bool IsAtLeastLarge(string size)
        {
            var sizes = SizeConstants.GetOrdered();
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);
            var sizeIndex = Array.IndexOf(sizes, size);
            return sizeIndex >= largeIndex;
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Dragon }
                .Union(subtypes)
                .Union([CreatureConstants.Types.Subtypes.Augmented, creatureType]);
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            if (IsAtLeastLarge(creature.Size) && creature.Speeds.ContainsKey(SpeedConstants.Land))
            {
                if (!creature.Speeds.ContainsKey(SpeedConstants.Fly))
                {
                    var dragonSpeeds = speedsGenerator.Generate(DragonSpecies);

                    var dragonFlySpeedValue = Math.Min(120, creature.Speeds[SpeedConstants.Land].Value * 2);
                    creature.Speeds[SpeedConstants.Fly] = dragonSpeeds[SpeedConstants.Fly];
                    creature.Speeds[SpeedConstants.Fly].Value = dragonFlySpeedValue;
                }
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
                Config.Name,
                TableNameConstants.Collection.LanguageGroups,
                DragonSpecies + LanguageConstants.Groups.Automatic);

            languages.Add(automaticLanguage);

            var bonusLanguages = collectionSelector.SelectFrom(
                Config.Name,
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

        private void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);

        private void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
        {
            if (abilities[AbilityConstants.Strength].HasScore)
                abilities[AbilityConstants.Strength].TemplateAdjustment += 8;

            if (abilities[AbilityConstants.Constitution].HasScore)
                abilities[AbilityConstants.Constitution].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Intelligence].HasScore)
                abilities[AbilityConstants.Intelligence].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Charisma].HasScore)
                abilities[AbilityConstants.Charisma].TemplateAdjustment += 2;
        }

        private void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(DragonSpecies);
        }

        private void UpdateCreatureAlignment(Creature creature, string presetAlignment)
        {
            creature.Alignment = alignmentGenerator.Generate(DragonSpecies, null, presetAlignment);
        }

        private void UpdateCreatureAlignment(CreaturePrototype creature, string presetAlignment, IEnumerable<string> dragonAlignments)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
            {
                creature.Alignments = [new(presetAlignment)];
            }
            else
            {
                creature.Alignments = [.. dragonAlignments.Distinct().Select(a => new Alignment(a))];
            }
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private string UpdateCreatureChallengeRating(string challengeRating)
        {
            var increased = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);

            if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increased))
            {
                return ChallengeRatingConstants.CR3;
            }

            return increased;
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 3;
        }

        private void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
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
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

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

                attacks = attacks.Except([newClaw]);
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

                attacks = attacks.Except([newBite]);
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

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var dragonAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, DragonSpecies);
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                dragonAlignments,
                creature.ChallengeRating,
                filters);

            if (!compatibility.Compatible)
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([DragonSpecies])]);

            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

            // Creature type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Demographics
            var demographicsTask = Task.Run(() => UpdateCreatureDemographics(creature));
            tasks.Add(demographicsTask);

            // Challenge ratings
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature));
            tasks.Add(challengeRatingTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature, filters?.Alignment));

            tasks.Add(alignmentTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on demographics
            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var dragonAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, DragonSpecies);

            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                //INFO: For Half-Dragons, alignments are purely based on Dragon Species, not Base Creature
                if (!dragonAlignments.Contains(filters.Alignment))
                    return [];
            }

            var templateCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, DragonSpecies + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);
            if (!filteredBaseCreatures.Any())
                return [];

            if (string.IsNullOrEmpty(filters?.ChallengeRating)
                && string.IsNullOrEmpty(filters?.Type)
                && string.IsNullOrEmpty(filters?.Alignment))
                return filteredBaseCreatures;

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);

            filteredBaseCreatures = filteredBaseCreatures
                .Where(c => AreFiltersCompatible(
                    allData[c].Single().Types,
                    dragonAlignments,
                    allData[c].Single().GetEffectiveChallengeRating(asCharacter),
                    filters).Compatible);

            return filteredBaseCreatures;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> dragonAlignments,
            string creatureChallengeRating,
            Filters filters)
        {
            var compatibility = IsCompatible(types);
            if (!compatibility.Compatible)
                return (false, compatibility.Reason);

            return AreFiltersCompatible(types, dragonAlignments, creatureChallengeRating, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> dragonAlignments,
            string creatureChallengeRating,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                //INFO: For Half-Dragons, alignments are purely based on Dragon Species, not Base Creature
                if (!dragonAlignments.Contains(filters.Alignment))
                    return (false, $"Alignment filter '{filters.Alignment}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.Type))
            {
                var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
                if (!updatedTypes.Contains(filters.Type))
                    return (false, $"Type filter '{filters.Type}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var cr = UpdateCreatureChallengeRating(creatureChallengeRating);
                if (cr != filters.ChallengeRating)
                    return (false, $"CR filter {filters.ChallengeRating} does not match updated creature CR {cr} (from CR {creatureChallengeRating})");
            }

            return (true, null);
        }

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types)
        {
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return (false, "Creature is incorporeal");

            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            return (true, null);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, filters);
            if (!compatibleCreatures.Any())
                return [];

            var dragonAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, DragonSpecies);
            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment, dragonAlignments));

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(CreaturePrototype prototype, string presetAlignment, IEnumerable<string> dragonAlignments)
        {
            UpdateCreatureAbilities(prototype);
            UpdateCreatureAlignment(prototype, presetAlignment, dragonAlignments);
            UpdateCreatureChallengeRating(prototype);
            UpdateCreatureLevelAdjustment(prototype);
            UpdateCreatureType(prototype);

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var dragonAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, DragonSpecies);
            var compatiblePrototypes = sourceCreatures
                .Where(p => IsCompatible(
                    p.Type.AllTypes,
                    dragonAlignments,
                    p.ChallengeRating,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment, dragonAlignments));

            return updatedPrototypes;
        }
    }
}
