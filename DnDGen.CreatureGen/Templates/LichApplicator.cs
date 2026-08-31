using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Languages;
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
    internal class LichApplicator(
        ICollectionSelector collectionSelector,
        Dice dice,
        IAttacksGenerator attacksGenerator,
        IFeatsGenerator featsGenerator,
        IDemographicsGenerator demographicsGenerator) : TemplateApplicator
    {
        private const int PhylacterySpellCasterLevel = 11;

        public Ability MinimumAbility => null;

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                asCharacter,
                creature.LevelAdjustment,
                [creature.CasterLevel, creature.Magic.CasterLevel],
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters,
                    templates: [.. creature.Templates.Union([CreatureConstants.Templates.Lich])]);
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
            UpdateCreatureAlignment(creature, filters);

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
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, CreatureConstants.Templates.Lich);
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
                Config.Name,
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic);

            creature.Languages = creature.Languages.Union([automaticLanguage]);
        }

        private static void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 4;
        }

        private static void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 4;
        }

        private void UpdateCreatureAlignment(Creature creature, Filters filters)
        {
            creature.Alignment = UpdateCreatureAlignment(creature.Alignment);

            if (filters.Alignments.Count > 0 && !filters.Alignments.Contains(creature.Alignment.Full))
            {
                throw new InvalidCreatureException(
                    $"Alignment {creature.Alignment} is not valid for filters",
                    false,
                    creature.Name,
                    filters,
                    templates: [.. creature.Templates.Concat([CreatureConstants.Templates.Lich])]);
            }
        }

        private void UpdateCreatureAlignment(CreaturePrototype creature, Filters filters)
        {
            var updatedAlignments = creature.Alignments.Select(UpdateCreatureAlignment);

            if (filters?.Alignments?.Count > 0)
            {
                var validFilters = filters.Alignments.Where(a => a.Contains(AlignmentConstants.Evil));
                //INFO: Using Where instead of Intersect to maintain alignment weighting
                updatedAlignments = updatedAlignments.Where(a => validFilters.Contains(a.Full));
            }

            creature.Alignments = [.. updatedAlignments];
        }

        private Alignment UpdateCreatureAlignment(Alignment alignment) => UpdateCreatureAlignment(alignment.Full);
        private Alignment UpdateCreatureAlignment(string alignment) => new(alignment) { Goodness = AlignmentConstants.Evil };

        private static void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);
        private static void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private static void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
        {
            abilities[AbilityConstants.Constitution].TemplateScore = 0;

            if (abilities[AbilityConstants.Wisdom].HasScore)
                abilities[AbilityConstants.Wisdom].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Intelligence].HasScore)
                abilities[AbilityConstants.Intelligence].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Charisma].HasScore)
                abilities[AbilityConstants.Charisma].TemplateAdjustment += 2;
        }

        private static void UpdateCreatureSkills(Creature creature)
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

        private static void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private static void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private static string UpdateCreatureChallengeRating(string challengeRating)
        {
            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var lichAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Lich,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

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

        private static void UpdateCreatureArmorClass(Creature creature)
        {
            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 5);
        }

        private static void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(CreatureConstants.Templates.Lich);
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.ChallengeRating,
                asCharacter,
                creature.LevelAdjustment,
                [creature.CasterLevel, creature.Magic.CasterLevel],
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    filters,
                    templates: [.. creature.Templates.Union([CreatureConstants.Templates.Lich])]);
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
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature, filters));
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

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            bool asCharacter,
            int? levelAdjustment,
            IEnumerable<int> casterLevels,
            Filters filters)
        {
            var (Compatible, Reason) = IsCompatible(types, levelAdjustment, casterLevels, asCharacter);
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
            if (filters is null)
                return (true, null);

            var updatedAlignments = alignments
                    .Select(UpdateCreatureAlignment)
                    .Select(a => a.Full);
            var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
            var cr = UpdateCreatureChallengeRating(creatureChallengeRating);

            return filters.AreCompatible(updatedAlignments, [cr], updatedTypes);
        }

        private static (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, int? levelAdjustment, IEnumerable<int> casterLevels, bool asCharacter)
        {
            if (types.First() != CreatureConstants.Types.Humanoid)
            {
                return (false, $"Type '{types.First()}' is not valid");
            }

            if (levelAdjustment.HasValue && asCharacter)
            {
                return (true, null);
            }

            if (casterLevels.Any() && casterLevels.Max() >= PhylacterySpellCasterLevel)
            {
                return (true, null);
            }

            return (false, "Creature is unable to cast spells");
        }

        public CreaturePrototype ApplyTo(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.ChallengeRating,
                creature.AsCharacter,
                creature.LevelAdjustment,
                [creature.CasterLevel],
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    creature.AsCharacter,
                    creature.Name,
                    filters,
                    templates: [.. creature.Templates.Concat([CreatureConstants.Templates.Lich])]);
            }

            UpdateCreatureAbilities(creature);
            UpdateCreatureChallengeRating(creature);
            UpdateCreatureLevelAdjustment(creature);
            UpdateCreatureType(creature);
            UpdateCreatureAlignment(creature, filters);

            return creature;
        }

        public bool IsCompatible(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, _) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.ChallengeRating,
                creature.AsCharacter,
                creature.LevelAdjustment,
                [creature.CasterLevel],
                filters);

            return Compatible;
        }
    }
}
