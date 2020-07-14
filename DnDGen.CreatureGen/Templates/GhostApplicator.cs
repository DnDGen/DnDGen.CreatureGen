using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class GhostApplicator : TemplateApplicator
    {
        private readonly Dice dice;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ICollectionSelector collectionSelector;
        private readonly IFeatsGenerator featsGenerator;
        private readonly IItemsGenerator itemsGenerator;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IEnumerable<string> creatureTypes;

        public GhostApplicator(
            Dice dice,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            ICollectionSelector collectionSelector,
            IFeatsGenerator featsGenerator,
            IItemsGenerator itemsGenerator,
            ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.dice = dice;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.collectionSelector = collectionSelector;
            this.featsGenerator = featsGenerator;
            this.itemsGenerator = itemsGenerator;
            this.typeAndAmountSelector = typeAndAmountSelector;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
            };
        }

        public Creature ApplyTo(Creature creature)
        {
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

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.Name = CreatureConstants.Types.Undead;

            if (!creature.Type.SubTypes.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
            {
                creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
                {
                    CreatureConstants.Types.Subtypes.Incorporeal
                });
            }
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Constitution].BaseScore = 0;
            creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 4;
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

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered().ToList();
            var index = challengeRatings.IndexOf(creature.ChallengeRating);
            creature.ChallengeRating = challengeRatings[index + 2];
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                creature.LevelAdjustment += 5;
            }
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            var ghostSkills = new[] { SkillConstants.Hide, SkillConstants.Listen, SkillConstants.Search, SkillConstants.Spot };
            foreach (var skill in creature.Skills)
            {
                if (ghostSkills.Contains(skill.Name))
                {
                    skill.AddBonus(8);
                }
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
            creature.HitPoints.HitDie = 12;
            creature.HitPoints.Roll(dice);
            creature.HitPoints.RollDefault(dice);
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var ghostAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Ghost,
                creature.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

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

        private void UpdateCreatureArmorClass(Creature creature)
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

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var tasks = new List<Task>();

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

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (!creatureTypes.Contains(types.First()))
                return false;

            var abilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, creature);
            var charismaAdjustment = abilityAdjustments.FirstOrDefault(a => a.Type == AbilityConstants.Charisma);
            if (charismaAdjustment == null)
                return false;

            return charismaAdjustment.Amount + Ability.DefaultScore >= 6;
        }
    }
}
