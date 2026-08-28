using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class GhostApplicator(
        Dice dice,
        ISpeedsGenerator speedsGenerator,
        IAttacksGenerator attacksGenerator,
        ICollectionSelector collectionSelector,
        IFeatsGenerator featsGenerator,
        IItemsGenerator itemsGenerator,
        IDemographicsGenerator demographicsGenerator) : TemplateApplicator
    {
        private readonly IEnumerable<string> creatureTypes =
            [
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
            ];

        public Ability MinimumAbility => new(AbilityConstants.Charisma) { BaseScore = 6 };

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Concat([CreatureConstants.Templates.Ghost])]);
            }

            // Template
            UpdateCreatureTemplate(creature);

            //Type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

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

            //Hit Points
            UpdateCreatureHitPoints(creature);

            //Attacks
            UpdateCreatureAttacks(creature);

            //Special Qualities
            UpdateCreatureSpecialQualities(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //Equipment
            UpdateCreatureEquipment(creature);

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
                .Union([CreatureConstants.Types.Subtypes.Incorporeal, CreatureConstants.Types.Subtypes.Augmented, creatureType]);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, CreatureConstants.Templates.Ghost);

            //As Ghosts are Incorporeal, they have no weight.
            //Will leave the weight description as-is, to inform what the ghost might have looked like in life.
            creature.Demographics.Weight.Value = 0;
        }

        private static void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;
            creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment += 4;
        }

        private static void UpdateCreatureAbilities(CreaturePrototype creature)
        {
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;
            creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment += 4;
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            var ghostSpeeds = speedsGenerator.Generate(CreatureConstants.Templates.Ghost);

            foreach (var kvp in ghostSpeeds)
            {
                if (!creature.Speeds.ContainsKey(kvp.Key))
                {
                    creature.Speeds[kvp.Key] = kvp.Value;
                }

                if (creature.Speeds[kvp.Key].Value < kvp.Value.Value)
                {
                    creature.Speeds[kvp.Key].Value = kvp.Value.Value;
                }

                creature.Speeds[kvp.Key].Description = kvp.Value.Description;
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

        private static void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                creature.LevelAdjustment += 5;
            }
        }

        private static void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                creature.LevelAdjustment += 5;
            }
        }

        private static void UpdateCreatureSkills(Creature creature)
        {
            var ghostSkills = new[] { SkillConstants.Hide, SkillConstants.Listen, SkillConstants.Search, SkillConstants.Spot };
            foreach (var skill in creature.Skills)
            {
                if (ghostSkills.Contains(skill.Name))
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

        private void UpdateCreatureEquipment(Creature creature)
        {
            if (creature.CanUseEquipment)
            {
                if (creature.Equipment.Armor != null
                    || creature.Equipment.Items.Any()
                    || creature.Equipment.Shield != null
                    || creature.Equipment.Weapons.Any())
                {
                    var quantity = dice.Roll(2).d4().AsSum();
                    var ghostItems = new List<Item>();

                    while (ghostItems.Count < quantity)
                    {
                        var items = itemsGenerator.GenerateRandomAtLevel(creature.HitPoints.RoundedHitDiceQuantity);
                        items = items.Take(quantity - ghostItems.Count);

                        ghostItems.AddRange(items);
                    }

                    creature.Equipment.Items = creature.Equipment.Items.Union(ghostItems);
                }
            }
        }

        private async Task UpdateCreatureEquipmentAsync(Creature creature)
        {
            if (creature.CanUseEquipment)
            {
                if (creature.Equipment.Armor != null
                    || creature.Equipment.Items.Any()
                    || creature.Equipment.Shield != null
                    || creature.Equipment.Weapons.Any())
                {
                    var quantity = dice.Roll(2).d4().AsSum();
                    var ghostItems = new List<Item>();

                    while (ghostItems.Count < quantity)
                    {
                        var items = await itemsGenerator.GenerateRandomAtLevelAsync(creature.HitPoints.RoundedHitDiceQuantity);
                        items = items.Take(quantity - ghostItems.Count);

                        ghostItems.AddRange(items);
                    }

                    creature.Equipment.Items = creature.Equipment.Items.Union(ghostItems);
                }
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

        private void UpdateCreatureAttacks(Creature creature)
        {
            var ghostAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Ghost,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

            var manifestation = ghostAttacks.First(a => a.Name == "Manifestation");
            var newAttacks = new List<Attack> { manifestation };
            var amount = dice.Roll().d3().AsSum();
            var availableAttacks = ghostAttacks.Except(newAttacks);

            while (newAttacks.Count < amount + 1)
            {
                var attack = collectionSelector.SelectRandomFrom(availableAttacks);
                newAttacks.Add(attack);
            }

            creature.Attacks = creature.Attacks.Union(newAttacks);
        }

        private static void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                if (!naturalArmorBonus.IsConditional)
                {
                    naturalArmorBonus.Condition = "Only applies for ethereal creatures";
                }
                else
                {
                    naturalArmorBonus.Condition += " AND Only applies for ethereal creatures";
                }
            }

            var deflectionBonus = Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier);
            creature.ArmorClass.AddBonus(ArmorClassConstants.Deflection, deflectionBonus);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var ghostQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Ghost,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities.Union(ghostQualities);
        }

        private static void UpdateCreatureTemplate(Creature creature) => creature.Templates.Add(CreatureConstants.Templates.Ghost);
        private static void UpdateCreatureTemplate(CreaturePrototype creature) => creature.Templates.Add(CreatureConstants.Templates.Ghost);

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    asCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Concat([CreatureConstants.Templates.Ghost])]);
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

            //Equipment
            var equipmentTask = UpdateCreatureEquipmentAsync(creature);
            tasks.Add(equipmentTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: These depend on abilities from earlier
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature));
            tasks.Add(hitPointTask);

            //Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on hit points and abilities from earlier
            //Special Qualities
            await Task.Run(() => UpdateCreatureSpecialQualities(creature));

            return creature;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            Ability charisma,
            string creatureChallengeRating,
            Filters filters)
        {
            var (Compatible, Reason) = IsCompatible(types, charisma);
            if (!Compatible)
                return (false, Reason);

            return AreFiltersCompatible(types, alignments, creatureChallengeRating, filters);
        }

        private static (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            Filters filters)
        {
            if (filters is null)
                return (true, null);

            var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
            var cr = UpdateCreatureChallengeRating(creatureChallengeRating);

            return filters.AreCompatible(alignments, updatedTypes, [cr]);
        }

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, Ability charisma)
        {
            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            if (charisma == null)
                return (false, "Creature has no Charisma");

            if (charisma.FullScore < MinimumAbility.FullScore)
                return (false, $"Creature has insufficient Charisma ({charisma.FullScore}, needs {MinimumAbility.FullScore})");

            return (true, null);
        }

        public CreaturePrototype ApplyTo(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.Abilities[AbilityConstants.Charisma],
                creature.ChallengeRating,
                filters);
            if (!Compatible)
            {
                throw new InvalidCreatureException(
                    Reason,
                    creature.AsCharacter,
                    creature.Name,
                    creature.Abilities[MinimumAbility.Name].FullScore.ToString(),
                    filters,
                    [.. creature.Templates.Concat([CreatureConstants.Templates.Ghost])]);
            }

            UpdateCreatureAbilities(creature);
            UpdateCreatureChallengeRating(creature);
            UpdateCreatureLevelAdjustment(creature);
            UpdateCreatureType(creature);
            UpdateCreatureTemplate(creature);

            if (filters?.Alignments?.Count > 0)
            {
                creature.Alignments = [.. creature.Alignments.Where(a => filters.Alignments.Contains(a.Full))];
            }

            return creature;
        }

        public bool IsCompatible(CreaturePrototype creature, Filters filters = null)
        {
            var (Compatible, Reason) = IsCompatible(
                creature.Type.AllTypes,
                creature.Alignments.Select(a => a.Full),
                creature.Abilities[MinimumAbility.Name],
                creature.ChallengeRating,
                filters);

            return Compatible;
        }
    }
}
