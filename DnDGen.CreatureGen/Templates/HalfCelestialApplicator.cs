using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class HalfCelestialApplicator(
        ICollectionSelector collectionSelector,
        ISpeedsGenerator speedsGenerator,
        IAttacksGenerator attacksGenerator,
        IFeatsGenerator featsGenerator,
        ISkillsGenerator skillsGenerator,
        IMagicGenerator magicGenerator,
        IDemographicsGenerator demographicsGenerator) : TemplateApplicator
    {
        private readonly IEnumerable<string> creatureTypes =
            [
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
            ];

        public Ability MinimumAbility => new(AbilityConstants.Intelligence) { BaseScore = 4 };

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                creature.HitPoints.RoundedHitDiceQuantity,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Union([CreatureConstants.Templates.HalfCelestial])]);
            }

            // Template
            UpdateCreatureTemplate(creature);

            // Creature type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

            // Challenge ratings
            UpdateCreatureChallengeRating(creature);

            //Speed
            UpdateCreatureSpeeds(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Saving Throws
            UpdateCreatureSavingThrows(creature);

            // Alignment
            UpdateCreatureAlignment(creature, filters);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //INFO: Depends on abilities
            // Languages
            UpdateCreatureLanguages(creature);

            //INFO: Depends on abilities
            // Attacks
            UpdateCreatureAttacks(creature);

            //INFO: Depends on creature type, abilities
            //Skills
            UpdateCreatureSkills(creature);

            //INFO: This depends on alignment, abilities
            // Magic
            UpdateCreatureMagic(creature);

            //INFO: Depends on creature type, abilities, skills, alignment
            // Special Qualities
            UpdateCreatureSpecialQualities(creature);

            return creature;
        }

        private void UpdateCreatureMagic(Creature creature)
        {
            creature.Magic = magicGenerator.GenerateWith(creature.Name, creature.Alignment, creature.Abilities, creature.Equipment);
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
            return new[] { CreatureConstants.Types.Outsider }
                .Union(subtypes)
                .Union([CreatureConstants.Types.Subtypes.Native, CreatureConstants.Types.Subtypes.Augmented, creatureType]);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, CreatureConstants.Templates.HalfCelestial, true);
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            var celestialSpeeds = speedsGenerator.Generate(CreatureConstants.Templates.HalfCelestial);

            if (creature.Speeds.ContainsKey(SpeedConstants.Land) && !creature.Speeds.ContainsKey(SpeedConstants.Fly))
            {
                creature.Speeds[SpeedConstants.Fly] = celestialSpeeds[SpeedConstants.Fly];
                creature.Speeds[SpeedConstants.Fly].Value = creature.Speeds[SpeedConstants.Land].Value * 2;
            }
        }

        private static void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                naturalArmorBonus.Value++;
            }

            if (creature.ArmorClass.NaturalArmorBonuses.Count == 0)
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 1);
            }
        }

        private static void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);

        private static void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
        {
            if (abilities[AbilityConstants.Strength].HasScore)
                abilities[AbilityConstants.Strength].TemplateAdjustment += 4;

            if (abilities[AbilityConstants.Dexterity].HasScore)
                abilities[AbilityConstants.Dexterity].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Constitution].HasScore)
                abilities[AbilityConstants.Constitution].TemplateAdjustment += 4;

            if (abilities[AbilityConstants.Intelligence].HasScore)
                abilities[AbilityConstants.Intelligence].TemplateAdjustment += 2;

            if (abilities[AbilityConstants.Wisdom].HasScore)
                abilities[AbilityConstants.Wisdom].TemplateAdjustment += 4;

            if (abilities[AbilityConstants.Charisma].HasScore)
                abilities[AbilityConstants.Charisma].TemplateAdjustment += 4;
        }

        private static void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private Alignment UpdateCreatureAlignment(Alignment alignment) => UpdateCreatureAlignment(alignment.Full);
        private Alignment UpdateCreatureAlignment(string alignment) => new(alignment) { Goodness = AlignmentConstants.Good };

        private void UpdateCreatureAlignment(CreaturePrototype creature, Filters filters)
        {
            var updatedAlignments = creature.Alignments
                .Where(a => a.Goodness != AlignmentConstants.Evil)
                .Select(UpdateCreatureAlignment);

            if (filters?.Alignments?.Count > 0)
            {
                var validFilters = filters.Alignments.Where(a => a.Contains(AlignmentConstants.Good));
                updatedAlignments = updatedAlignments.Where(a => validFilters.Contains(a.Full));
            }

            creature.Alignments = [.. updatedAlignments];
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
                    templates: [.. creature.Templates.Concat([CreatureConstants.Templates.CelestialCreature])]);
            }
        }

        private static void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, creature.HitPoints.RoundedHitDiceQuantity);
        }

        private static void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, creature.GetRoundedHitDiceQuantity());
        }

        private static string UpdateCreatureChallengeRating(string challengeRating, double hitDiceQuantity)
        {
            return UpdateCreatureChallengeRating(challengeRating, HitDice.GetRoundedQuantity(hitDiceQuantity));
        }

        private static string UpdateCreatureChallengeRating(string challengeRating, int hitDiceQuantity)
        {
            if (hitDiceQuantity >= 11)
            {
                return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 3);
            }
            else if (hitDiceQuantity >= 6)
            {
                return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
            }

            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 1);
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

        private void UpdateCreatureSkills(Creature creature)
        {
            foreach (var skill in creature.Skills)
            {
                skill.Ranks = 0;
            }

            creature.Skills = skillsGenerator.ApplySkillPointsAsRanks(creature.Skills, creature.HitPoints, creature.Type, creature.Abilities, true);
        }

        private static void UpdateCreatureSavingThrows(Creature creature)
        {
            creature.Saves[SaveConstants.Fortitude].AddBonus(4, "against poison");
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var attacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.HalfCelestial,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

            var smiteEvil = attacks.First(a => a.Name == "Smite Evil");
            smiteEvil.Damages.Add(new Damage
            {
                Roll = Math.Min(creature.HitPoints.RoundedHitDiceQuantity, 20).ToString()
            });

            creature.Attacks = creature.Attacks.Union(attacks);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var specialQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.HalfCelestial,
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
                    creature.SpecialQualities = creature.SpecialQualities.Union([sq]);
                }
                else if (matching.Power < sq.Power)
                {
                    matching.Power = sq.Power;
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
                CreatureConstants.Templates.HalfCelestial + LanguageConstants.Groups.Automatic);

            languages.Add(automaticLanguage);

            var bonusLanguages = collectionSelector.SelectFrom(
                Config.Name,
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.HalfCelestial + LanguageConstants.Groups.Bonus);
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

        private static void UpdateCreatureTemplate(Creature creature) => creature.Templates.Add(CreatureConstants.Templates.HalfCelestial);
        private static void UpdateCreatureTemplate(CreaturePrototype creature) => creature.Templates.Add(CreatureConstants.Templates.HalfCelestial);

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                creature.HitPoints.RoundedHitDiceQuantity,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Union([CreatureConstants.Templates.HalfCelestial])]);
            }

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

            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Saving Throws
            var saveTask = Task.Run(() => UpdateCreatureSavingThrows(creature));
            tasks.Add(saveTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature, filters));
            tasks.Add(alignmentTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on abilities
            // Languages
            var languageTask = Task.Run(() => UpdateCreatureLanguages(creature));
            tasks.Add(languageTask);

            //INFO: Depends on abilities
            // Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            //INFO: Depends on creature type, abilities
            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            //INFO: This depends on alignment, abilities
            // Magic
            var magicTask = Task.Run(() => UpdateCreatureMagic(creature));
            tasks.Add(magicTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on creature type, abilities, skills, alignment
            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            return creature;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            Ability intelligence,
            string creatureChallengeRating,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            var (Compatible, Reason) = IsCompatible(types, alignments, intelligence);
            if (!Compatible)
                return (false, Reason);

            return AreFiltersCompatible(types, alignments, creatureChallengeRating, creatureHitDiceQuantity, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            if (filters is null)
                return (true, null);

            var updatedAlignments = alignments
                    .Where(a => !a.Contains(AlignmentConstants.Evil))
                    .Select(UpdateCreatureAlignment)
                    .Select(a => a.Full);
            var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
            var cr = UpdateCreatureChallengeRating(creatureChallengeRating, creatureHitDiceQuantity);

            return filters.AreCompatible(updatedAlignments, [cr], updatedTypes);
        }

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments, Ability intelligence)
        {
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return (false, "Creature is Incorporeal");

            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            if (!alignments.Any(a => !a.Contains(AlignmentConstants.Evil)))
                return (false, "Creature has no non-evil alignments");

            if (intelligence.FullScore < MinimumAbility.FullScore)
                return (false, $"Creature has insufficient Intelligence ({intelligence.FullScore}, needs {MinimumAbility.Name})");

            return (true, null);
        }

        public CreaturePrototype ApplyTo(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.Abilities[AbilityConstants.Intelligence],
                creature.ChallengeRating,
                creature.GetRoundedHitDiceQuantity(),
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    creature.AsCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Concat([CreatureConstants.Templates.HalfCelestial])]);
            }

            UpdateCreatureAbilities(creature);
            UpdateCreatureAlignment(creature, filters);
            UpdateCreatureChallengeRating(creature);
            UpdateCreatureLevelAdjustment(creature);
            UpdateCreatureType(creature);
            UpdateCreatureTemplate(creature);

            return creature;
        }

        public bool IsCompatible(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                creature.HitDiceQuantity,
                filters);

            return Compatible;
        }
    }
}
