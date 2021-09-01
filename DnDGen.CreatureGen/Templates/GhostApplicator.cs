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
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;

        public GhostApplicator(
            Dice dice,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            ICollectionSelector collectionSelector,
            IFeatsGenerator featsGenerator,
            IItemsGenerator itemsGenerator,
            ITypeAndAmountSelector typeAndAmountSelector,
            ICreatureDataSelector creatureDataSelector,
            IAdjustmentsSelector adjustmentSelector)
        {
            this.dice = dice;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.collectionSelector = collectionSelector;
            this.featsGenerator = featsGenerator;
            this.itemsGenerator = itemsGenerator;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.adjustmentSelector = adjustmentSelector;

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
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);

            creature.Type.Name = adjustedTypes.First();
            creature.Type.SubTypes = adjustedTypes.Skip(1);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Undead }
                .Union(subtypes)
                .Union(new[] { CreatureConstants.Types.Subtypes.Incorporeal, CreatureConstants.Types.Subtypes.Augmented, creatureType });
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Constitution].TemplateScore = 0;
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
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating);
        }

        private string UpdateCreatureChallengeRating(string challengeRating)
        {
            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
        }

        public IEnumerable<string> GetChallengeRatings() => null;
        public IEnumerable<string> GetChallengeRatings(string challengeRating) => new[]
        {
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2),
        };
        public (double? Lower, double? Upper) GetHitDiceRange(string challengeRating) => (null, null);

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

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = CreatureConstants.Templates.Ghost;
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

        public bool IsCompatible(string creature, bool asCharacter, string type = null, string challengeRating = null)
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
                var cr = GetPotentialChallengeRating(creature, asCharacter);
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

            var abilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, creature);
            var charismaAdjustment = abilityAdjustments.FirstOrDefault(a => a.Type == AbilityConstants.Charisma);
            if (charismaAdjustment == null)
                return false;

            return charismaAdjustment.Amount + Ability.DefaultScore >= 6;
        }

        public IEnumerable<string> GetPotentialTypes(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            var creatureType = types.First();
            var subtypes = types.Skip(1);

            var adjustedTypes = UpdateCreatureType(creatureType, subtypes);

            return adjustedTypes;
        }

        public string GetPotentialChallengeRating(string creature, bool asCharacter)
        {
            var quantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            var creatureType = types.First();

            if (asCharacter && quantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                return UpdateCreatureChallengeRating(ChallengeRatingConstants.CR0);
            }

            var data = creatureDataSelector.SelectFor(creature);
            var adjustedChallengeRating = UpdateCreatureChallengeRating(data.ChallengeRating);

            return adjustedChallengeRating;
        }

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, string type = null, string challengeRating = null)
        {
            var filteredBaseCreatures = sourceCreatures;

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var allData = creatureDataSelector.SelectAll();
                var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
                var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);

                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => CreatureInRange(allData[c].ChallengeRating, challengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(type))
            {
                //INFO: Unless this type is added by a template, it must already exist on the base creature
                //So first, we check to see if the template could return this type for a human
                //If not, then we can filter the base creatures down to ones that already have this type
                var humanTypes = GetPotentialTypes(CreatureConstants.Human);
                var templateTypes = humanTypes
                    .Except(new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human });

                if (!templateTypes.Contains(type))
                {
                    var ofType = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);
                    filteredBaseCreatures = filteredBaseCreatures.Intersect(ofType);
                }
            }

            var templateCreatures = filteredBaseCreatures.Where(c => IsCompatible(c, asCharacter, type, challengeRating));

            return templateCreatures;
        }

        private bool CreatureInRange(
            string creatureChallengeRating,
            string filterChallengeRating,
            bool asCharacter,
            double creatureHitDiceQuantity,
            IEnumerable<string> creatureTypes)
        {
            var creatureType = creatureTypes.First();

            if (asCharacter && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                creatureChallengeRating = ChallengeRatingConstants.CR0;
            }

            var templateChallengeRatings = GetChallengeRatings(creatureChallengeRating);
            return templateChallengeRatings.Contains(filterChallengeRating);
        }
    }
}
