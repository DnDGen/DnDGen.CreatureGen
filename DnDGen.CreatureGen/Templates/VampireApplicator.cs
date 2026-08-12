using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Skills;
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
    internal class VampireApplicator(
        Dice dice,
        IAttacksGenerator attacksGenerator,
        IFeatsGenerator featsGenerator,
        ICollectionSelector collectionSelector,
        ICreaturePrototypeFactory prototypeFactory,
        IDemographicsGenerator demographicsGenerator) : TemplateApplicator
    {
        public Ability MinimumAbility => null;

        private readonly IEnumerable<string> creatureTypes =
            [
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            ];
        private const int MinimumVampireHitDice = 5;

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                asCharacter,
                creature.LevelAdjustment,
                creature.HitPoints.HitDiceQuantity,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    null,
                    [.. creature.Templates.Union([CreatureConstants.Templates.Vampire])]);
            }

            // Template
            UpdateCreatureTemplate(creature);

            //Type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Alignment
            UpdateCreatureAlignment(creature, filters?.Alignment);

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

        private static void UpdateCreatureType(Creature creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private static void UpdateCreatureType(CreaturePrototype creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private static IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Undead }
                .Union(subtypes)
                .Union([CreatureConstants.Types.Subtypes.Augmented, creatureType]);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, CreatureConstants.Templates.Vampire);
        }

        private static void UpdateCreatureInitiativeBonus(Creature creature)
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

        private static void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 8;
        }

        private static void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 8;
        }

        private void UpdateCreatureAlignment(Creature creature, string presetAlignment)
        {
            creature.Alignment = UpdateCreatureAlignment(creature.Alignment, presetAlignment);
        }

        private void UpdateCreatureAlignment(CreaturePrototype creature, string presetAlignment)
        {
            creature.Alignments = [.. creature.Alignments
                .Select(a => UpdateCreatureAlignment(a, presetAlignment))
                .Distinct()];
        }

        private Alignment UpdateCreatureAlignment(Alignment alignment, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
            {
                return new Alignment(presetAlignment);
            }

            return UpdateCreatureAlignment(alignment.Full);
        }

        private Alignment UpdateCreatureAlignment(string alignment)
        {
            var newAlignment = new Alignment(alignment)
            {
                Goodness = AlignmentConstants.Evil
            };

            return newAlignment;
        }

        private static void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);
        private static void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private static void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
        {
            abilities[AbilityConstants.Constitution].TemplateScore = 0;

            if (abilities[AbilityConstants.Strength].HasScore)
                abilities[AbilityConstants.Strength].TemplateAdjustment += 6;

            if (abilities[AbilityConstants.Dexterity].HasScore)
                abilities[AbilityConstants.Dexterity].TemplateAdjustment += 4;

            if (abilities[AbilityConstants.Wisdom].HasScore)
                abilities[AbilityConstants.Wisdom].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Intelligence].HasScore)
                abilities[AbilityConstants.Intelligence].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Charisma].HasScore)
                abilities[AbilityConstants.Charisma].TemplateAdjustment += 4;
        }

        private static void UpdateCreatureSkills(Creature creature)
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

        private static void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private static void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private static string UpdateCreatureChallengeRating(string challengeRating) => ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);

        private void UpdateCreatureAttacks(Creature creature)
        {
            var vampireAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Vampire,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

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

                vampireAttacks = vampireAttacks.Except([newSlam]);
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

        private static void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var bonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                bonus.Value += 6;
            }

            if (creature.ArmorClass.NaturalArmorBonuses.Count == 0)
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 6);
            }
        }

        private static void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(CreatureConstants.Templates.Vampire);
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                asCharacter,
                creature.LevelAdjustment,
                creature.HitPoints.HitDiceQuantity,
                filters);

            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    null,
                    [.. creature.Templates.Union([CreatureConstants.Templates.Vampire])]);
            }

            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

            //Type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Demographics
            var demographicsTask = Task.Run(() => UpdateCreatureDemographics(creature));
            tasks.Add(demographicsTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature, filters?.Alignment));
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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null)
        {
            var templateCreatures = collectionSelector.SelectFrom(
                Config.Name,
                TableNameConstants.Collection.CreatureGroups,
                CreatureConstants.Templates.Vampire + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);

            if (!string.IsNullOrEmpty(filters?.Type))
            {
                var groupName = CreatureConstants.Templates.Vampire + filters.Type;
                var typeCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(typeCreatures);
            }

            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var groupName = CreatureConstants.Templates.Vampire + filters.Alignment;
                var alignmentCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(alignmentCreatures);
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var groupName = CreatureConstants.Templates.Vampire + asCharacter + filters.ChallengeRating;
                var crCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(crCreatures);
            }

            return filteredBaseCreatures;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            bool asCharacter,
            int? levelAdjustment,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            var (Compatible, Reason) = IsCompatible(types, levelAdjustment, asCharacter, creatureHitDiceQuantity);
            if (!Compatible)
                return (false, Reason);

            return AreFiltersCompatible(types, alignments, creatureChallengeRating, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var presetAlignment = new Alignment(filters.Alignment);
                if (presetAlignment.Goodness != AlignmentConstants.Evil)
                    return (false, $"Alignment filter '{filters.Alignment}' is not valid");

                var newAlignments = alignments.Select(UpdateCreatureAlignment);
                if (!newAlignments.Any(a => a.Full == filters.Alignment))
                    return (false, $"Alignment filter '{filters.Alignment}' is not valid for creature alignments");
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

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, int? levelAdjustment, bool asCharacter, double creatureHitDiceQuantity)
        {
            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            if (levelAdjustment.HasValue && asCharacter)
                return (true, null);

            if (creatureHitDiceQuantity < MinimumVampireHitDice)
                return (false, $"Creature has insufficient Hit Dice ({creatureHitDiceQuantity} < {MinimumVampireHitDice})");

            return (true, null);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(
            IEnumerable<string> sourceCreatures,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, abilityRandomizer, filters);
            if (!compatibleCreatures.Any())
                return [];

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(CreaturePrototype prototype, string presetAlignment)
        {
            UpdateCreatureAbilities(prototype);
            UpdateCreatureChallengeRating(prototype);
            UpdateCreatureLevelAdjustment(prototype);
            UpdateCreatureType(prototype);
            UpdateCreatureAlignment(prototype, presetAlignment);

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatiblePrototypes = sourceCreatures
                .Where(p => IsCompatible(
                    p.Type.AllTypes,
                    p.Alignments.Select(a => a.Full),
                    p.ChallengeRating,
                    asCharacter,
                    p.LevelAdjustment,
                    p.HitDiceQuantity,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }
    }
}
