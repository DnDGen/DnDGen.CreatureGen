using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal abstract class LycanthropeApplicator : TemplateApplicator
    {
        protected abstract string LycanthropeSpecies { get; }
        protected abstract string AnimalSpecies { get; }

        private readonly ICollectionSelector collectionSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly Dice dice;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IFeatsGenerator featsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IEnumerable<string> creatureTypes;

        public LycanthropeApplicator(
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            Dice dice,
            ITypeAndAmountSelector typeAndAmountSelector,
            IFeatsGenerator featsGenerator,
            IAttacksGenerator attacksGenerator,
            ISavesGenerator savesGenerator,
            ISkillsGenerator skillsGenerator,
            ISpeedsGenerator speedsGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.featsGenerator = featsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.savesGenerator = savesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.speedsGenerator = speedsGenerator;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
            };
        }

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (!creatureTypes.Contains(types.First()))
                return false;

            var creatureData = creatureDataSelector.SelectFor(creature);
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);
            var sizes = SizeConstants.GetOrdered();
            var creatureIndex = Array.IndexOf(sizes, creatureData.Size);
            var animalIndex = Array.IndexOf(sizes, animalData.Size);

            if (Math.Abs(creatureIndex - animalIndex) > 1)
                return false;

            return true;
        }

        public Creature ApplyTo(Creature creature)
        {
            var animalCreatureType = new CreatureType { Name = CreatureConstants.Types.Animal };
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);

            // Creature type
            UpdateCreatureType(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature, animalData);

            //Speed
            UpdateCreatureSpeeds(creature);

            //INFO: This depends on abilities
            //Hit Points
            var animalHitPoints = UpdateCreatureHitPoints(creature, animalCreatureType, animalData);

            //INFO: This depends on hit points
            //Skills
            var animalSkills = UpdateCreatureSkills(creature, animalCreatureType, animalHitPoints, animalData);

            //INFO: This depends on hit points
            // Challenge ratings
            UpdateCreatureChallengeRating(creature, animalHitPoints);

            //INFO: This depends on skills
            // Special Qualities
            var animalSpecialQualities = UpdateCreatureSpecialQualities(creature, animalCreatureType, animalHitPoints, animalData, animalSkills);

            //INFO: This depends on special qualities
            // Attacks
            var animalAttacks = UpdateCreatureAttacks(creature, animalCreatureType, animalHitPoints, animalData);

            //INFO: This depends on special qualities, attacks, skills, abilities, hit points, 
            // Feats
            var animalFeats = UpdateCreatureFeats(
                creature,
                animalHitPoints,
                animalData,
                animalSkills,
                animalAttacks.AnimalAttacks,
                animalAttacks.AnimalBaseAttack,
                animalSpecialQualities);

            //INFO: This depends on feats, hit points
            //Hit Points
            UpdateCreatureHitPointsWithFeats(creature, animalFeats);

            //INFO: This depends on feats, hit points
            //Saves
            UpdateCreatureSaves(creature, animalCreatureType, animalHitPoints, animalSpecialQualities, animalFeats);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
            {
                CreatureConstants.Types.Subtypes.Shapechanger,
            });
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Wisdom].HasScore)
                creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 2;

            var animalAbilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, AnimalSpecies);
            var physicalAbilities = new[] { AbilityConstants.Strength, AbilityConstants.Constitution, AbilityConstants.Dexterity };

            foreach (var adjustment in animalAbilityAdjustments.Where(a => physicalAbilities.Contains(a.Type)))
            {
                creature.Abilities[adjustment.Type].Bonuses.Add(new Bonus
                {
                    Value = adjustment.Amount,
                    Condition = "In Animal or Hybrid form",
                });
            }
        }

        private HitPoints UpdateCreatureHitPoints(Creature creature, CreatureType animalCreatureType, CreatureDataSelection animalData)
        {
            var animalHitPoints = hitPointsGenerator.GenerateFor(AnimalSpecies, animalCreatureType, creature.Abilities[AbilityConstants.Constitution], animalData.Size);
            creature.HitPoints.HitDice.Add(animalHitPoints.HitDice[0]);

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);

            return animalHitPoints;
        }

        private void UpdateCreatureChallengeRating(Creature creature, HitPoints animalHitPoints)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = Array.IndexOf(challengeRatings, creature.ChallengeRating);
            var increase = 2;

            if (animalHitPoints.HitDice[0].Quantity > 20)
            {
                increase = 6;
            }
            else if (animalHitPoints.HitDice[0].Quantity > 10)
            {
                increase = 5;
            }
            else if (animalHitPoints.HitDice[0].Quantity > 5)
            {
                increase = 4;
            }
            else if (animalHitPoints.HitDice[0].Quantity > 2)
            {
                increase = 3;
            }

            creature.ChallengeRating = challengeRatings[index + increase];
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                if (LycanthropeSpecies.Contains("Afflicted"))
                    creature.LevelAdjustment += 2;
                else if (LycanthropeSpecies.Contains("Natural"))
                    creature.LevelAdjustment += 3;
            }
        }

        private IEnumerable<Skill> UpdateCreatureSkills(Creature creature, CreatureType animalCreatureType, HitPoints animalHitPoints, CreatureDataSelection animalData)
        {
            var animalSkills = skillsGenerator.GenerateFor(
                animalHitPoints,
                AnimalSpecies,
                animalCreatureType,
                creature.Abilities,
                creature.CanUseEquipment,
                animalData.Size,
                false);

            if (LycanthropeSpecies.Contains("Afflicted"))
            {
                var controlShape = new Skill(
                    SkillConstants.Special.ControlShape,
                    creature.Abilities[AbilityConstants.Wisdom],
                    creature.HitPoints.RoundedHitDiceQuantity + 3);
                controlShape.ClassSkill = true;

                animalSkills = animalSkills.Union(new[] { controlShape });

                foreach (var animalSkill in animalSkills)
                {
                    animalSkill.Ranks = 0;
                    animalSkill.RankCap = creature.HitPoints.RoundedHitDiceQuantity + 3;
                }

                animalSkills = skillsGenerator.ApplySkillPointsAsRanks(
                    animalSkills,
                    animalHitPoints,
                    animalCreatureType,
                    creature.Abilities,
                    false);
            }

            foreach (var creatureSkill in creature.Skills)
            {
                creatureSkill.RankCap = creature.HitPoints.RoundedHitDiceQuantity + 3;
            }

            foreach (var animalSkill in animalSkills)
            {
                animalSkill.RankCap = creature.HitPoints.RoundedHitDiceQuantity + 3;

                var creatureSkill = creature.Skills.FirstOrDefault(s => s.Key == animalSkill.Key);
                if (creatureSkill != null)
                {
                    creatureSkill.Ranks += animalSkill.Ranks;
                    creatureSkill.ClassSkill |= animalSkill.ClassSkill;

                    foreach (var bonus in animalSkill.Bonuses)
                    {
                        creatureSkill.AddBonus(bonus.Value, bonus.Condition);
                    }
                }
                else
                {
                    creature.Skills = creature.Skills.Union(new[] { animalSkill });
                }
            }

            return animalSkills;
        }

        private IEnumerable<Feat> UpdateCreatureSpecialQualities(
            Creature creature,
            CreatureType animalCreatureType,
            HitPoints animalHitPoints,
            CreatureDataSelection animalData,
            IEnumerable<Skill> animalSkills)
        {
            var animalSpecialQualities = featsGenerator.GenerateSpecialQualities(
                AnimalSpecies,
                animalCreatureType,
                animalHitPoints,
                creature.Abilities,
                animalSkills,
                animalData.CanUseEquipment,
                animalData.Size,
                creature.Alignment);

            foreach (var sq in animalSpecialQualities)
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

            var lycanthropeSpecialQualities = featsGenerator.GenerateSpecialQualities(
                LycanthropeSpecies,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            foreach (var sq in lycanthropeSpecialQualities)
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

            return animalSpecialQualities;
        }

        private void UpdateCreatureSaves(
            Creature creature,
            CreatureType animalCreatureType,
            HitPoints animalHitPoints,
            IEnumerable<Feat> animalSpecialQualities,
            IEnumerable<Feat> animalFeats)
        {
            var allFeats = animalFeats.Union(animalSpecialQualities);
            var animalSaves = savesGenerator.GenerateWith(AnimalSpecies, animalCreatureType, animalHitPoints, allFeats, creature.Abilities);

            foreach (var kvp in animalSaves)
            {
                creature.Saves[kvp.Key].BaseValue += kvp.Value.BaseValue;

                foreach (var bonus in kvp.Value.Bonuses)
                {
                    creature.Saves[kvp.Key].AddBonus(bonus.Value, bonus.Condition);
                }
            }
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            var animalSpeeds = speedsGenerator.Generate(AnimalSpecies);

            foreach (var kvp in animalSpeeds)
            {
                if (creature.Speeds.ContainsKey(kvp.Key))
                {
                    var bonus = Convert.ToInt32(kvp.Value.Value - creature.Speeds[kvp.Key].Value);
                    creature.Speeds[kvp.Key].AddBonus(bonus, "In Animal Form");
                }
                else
                {
                    creature.Speeds.Add(kvp.Key, kvp.Value);

                    if (!string.IsNullOrEmpty(kvp.Value.Description))
                    {
                        kvp.Value.Description += ", ";
                    }

                    kvp.Value.Description += "In Animal Form";
                }
            }
        }

        private IEnumerable<Feat> UpdateCreatureFeats(
            Creature creature,
            HitPoints animalHitPoints,
            CreatureDataSelection animalData,
            IEnumerable<Skill> animalSkills,
            IEnumerable<Attack> animalAttacks,
            int animalBaseAttack,
            IEnumerable<Feat> animalSpecialQualities)
        {
            var animalFeats = featsGenerator.GenerateFeats(
                animalHitPoints,
                animalBaseAttack,
                creature.Abilities,
                animalSkills,
                animalAttacks,
                animalSpecialQualities,
                animalData.CasterLevel,
                creature.Speeds,
                animalData.NaturalArmor,
                animalData.NumberOfHands,
                animalData.Size,
                animalData.CanUseEquipment);

            foreach (var feat in animalFeats)
            {
                var matching = creature.Feats.FirstOrDefault(f =>
                    f.Name == feat.Name
                    && !f.Foci.Except(feat.Foci).Any()
                    && !feat.Foci.Except(f.Foci).Any());

                if (matching == null || feat.CanBeTakenMultipleTimes)
                {
                    creature.Feats = creature.Feats.Union(new[] { feat });
                }
                else if (matching.Power < feat.Power)
                {
                    matching.Power = feat.Power;
                }
            }

            return animalFeats;
        }

        private void UpdateCreatureHitPointsWithFeats(Creature creature, IEnumerable<Feat> animalFeats)
        {
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, animalFeats);
        }

        private (IEnumerable<Attack> AnimalAttacks, int AnimalBaseAttack) UpdateCreatureAttacks(Creature creature, CreatureType animalCreatureType, HitPoints animalHitPoints, CreatureDataSelection animalData)
        {
            var baseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(animalCreatureType, animalHitPoints);
            creature.BaseAttackBonus += baseAttackBonus;

            foreach (var attack in creature.Attacks)
            {
                if (attack.IsSpecial)
                {
                    attack.Name += $" (in {creature.Type.Name} form)";
                }
                else
                {
                    attack.Name += $" (in {creature.Type.Name} or Hybrid form)";
                }
            }

            var animalAttacks = attacksGenerator.GenerateAttacks(
                AnimalSpecies,
                animalData.Size,
                animalData.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            animalAttacks = attacksGenerator.ApplyAttackBonuses(animalAttacks, allFeats, creature.Abilities);

            foreach (var animalAttack in animalAttacks)
            {
                animalAttack.Name += " (in Animal form)";
            }

            var biggerSize = GetBiggerSize(creature.Size, animalData.Size);
            var lycanthropeAttacks = attacksGenerator.GenerateAttacks(
                LycanthropeSpecies,
                SizeConstants.Medium,
                biggerSize,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            lycanthropeAttacks = attacksGenerator.ApplyAttackBonuses(lycanthropeAttacks, allFeats, creature.Abilities);

            foreach (var lycanthropeAttack in lycanthropeAttacks)
            {
                var searchName = lycanthropeAttack.Name.Replace(" (in Hybrid form)", string.Empty);
                var animalAttack = animalAttacks.FirstOrDefault(a => a.Name.StartsWith(searchName));
                if (animalAttack == null)
                    continue;

                if (!string.IsNullOrEmpty(lycanthropeAttack.DamageEffect))
                {
                    animalAttack.DamageEffect = lycanthropeAttack.DamageEffect;
                }
            }

            creature.Attacks = creature.Attacks.Union(animalAttacks).Union(lycanthropeAttacks);

            return (animalAttacks, baseAttackBonus);
        }

        private string GetBiggerSize(string size1, string size2)
        {
            var ordered = SizeConstants.GetOrdered();
            var index1 = Array.IndexOf(ordered, size1);
            var index2 = Array.IndexOf(ordered, size2);

            if (index1 >= index2)
                return size1;

            return size2;
        }

        private void UpdateCreatureArmorClass(Creature creature, CreatureDataSelection animalData)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                naturalArmorBonus.Value += 2;
                naturalArmorBonus.Condition = "In base or hybrid form";
            }

            if (!creature.ArmorClass.NaturalArmorBonuses.Any())
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 2, "In base or hybrid form");
            }

            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, animalData.NaturalArmor + 2, "In animal or hybrid form");
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var animalCreatureType = new CreatureType { Name = CreatureConstants.Types.Animal };
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);
            var tasks = new List<Task>();

            // Creature type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            // Abilities
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature, animalData));
            tasks.Add(armorClassTask);

            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on abilities
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature, animalCreatureType, animalData));
            tasks.Add(hitPointTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalHitPoints = hitPointTask.Result;

            //INFO: This depends on hit points
            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature, animalCreatureType, animalHitPoints, animalData));
            tasks.Add(skillTask);

            //INFO: This depends on hit points
            //Challenge Rating
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature, animalHitPoints));
            tasks.Add(challengeRatingTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalSkills = skillTask.Result;

            //INFO: This depends on skills
            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature, animalCreatureType, animalHitPoints, animalData, animalSkills));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalSpecialQualities = qualityTask.Result;

            //INFO: This depends on special qualities
            // Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature, animalCreatureType, animalHitPoints, animalData));
            tasks.Add(attackTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalAttacks = attackTask.Result;

            //INFO: This depends on special qualities, attacks, skills, abilities, hit points, 
            // Feats
            var featTask = Task.Run(() => UpdateCreatureFeats(
                creature,
                animalHitPoints,
                animalData,
                animalSkills,
                animalAttacks.AnimalAttacks,
                animalAttacks.AnimalBaseAttack,
                animalSpecialQualities));
            tasks.Add(featTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalFeats = featTask.Result;

            //INFO: This depends on feats, hit points
            //Hit Points
            var hitPointWithFeatsTask = Task.Run(() => UpdateCreatureHitPointsWithFeats(creature, animalFeats));
            tasks.Add(hitPointWithFeatsTask);

            //INFO: This depends on feats, hit points
            //Saves
            var saveTask = Task.Run(() => UpdateCreatureSaves(creature, animalCreatureType, animalHitPoints, animalSpecialQualities, animalFeats));
            tasks.Add(saveTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            return creature;
        }
    }
}
