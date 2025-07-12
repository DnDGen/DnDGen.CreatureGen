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
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class LycanthropeApplicator : TemplateApplicator
    {
        public string LycanthropeSpecies { get; set; }
        public string AnimalSpecies { get; set; }
        public bool IsNatural { get; set; }

        private readonly ICollectionSelector collectionSelector;
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly Dice dice;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly IFeatsGenerator featsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IEnumerable<string> creatureTypes;
        private readonly ICreaturePrototypeFactory prototypeFactory;
        private readonly IDemographicsGenerator demographicsGenerator;

        public LycanthropeApplicator(
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            Dice dice,
            ICollectionTypeAndAmountSelector typeAndAmountSelector,
            IFeatsGenerator featsGenerator,
            IAttacksGenerator attacksGenerator,
            ISavesGenerator savesGenerator,
            ISkillsGenerator skillsGenerator,
            ISpeedsGenerator speedsGenerator,
            ICreaturePrototypeFactory prototypeFactory,
            IDemographicsGenerator demographicsGenerator)
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
            this.prototypeFactory = prototypeFactory;
            this.demographicsGenerator = demographicsGenerator;

            creatureTypes =
            [
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
            ];
        }

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var animalData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, AnimalSpecies);
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Size,
                creature.ChallengeRating,
                animalData.Size,
                animalData.GetEffectiveHitDiceQuantity(asCharacter),
                filters);

            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([LycanthropeSpecies])]);
            }

            // Template
            UpdateCreatureTemplate(creature);

            // Creature type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

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
            var animalCreatureType = new CreatureType(animalData.Types);
            var animalHitPoints = UpdateCreatureHitPoints(creature, animalCreatureType, animalData, asCharacter);

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
            var animalAttacks = UpdateCreatureAttacks(creature, animalHitPoints, animalData);

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
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, LycanthropeSpecies);
        }

        private void UpdateCreatureType(CreaturePrototype creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { creatureType }
                .Union(subtypes)
                .Union([CreatureConstants.Types.Subtypes.Shapechanger]);
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            var animalAbilityAdjustments = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, AnimalSpecies);
            UpdateCreatureAbilities(creature.Abilities, animalAbilityAdjustments);
        }

        private void UpdateCreatureAbilities(CreaturePrototype creature, IEnumerable<TypeAndAmountDataSelection> animalAbilityAdjustments)
        {
            UpdateCreatureAbilities(creature.Abilities, animalAbilityAdjustments);
        }

        private void UpdateCreatureAbilities(Dictionary<string, Ability> abilities, IEnumerable<TypeAndAmountDataSelection> animalAbilityAdjustments)
        {
            if (abilities[AbilityConstants.Wisdom].HasScore)
                abilities[AbilityConstants.Wisdom].TemplateAdjustment += 2;

            var physicalAbilities = new[] { AbilityConstants.Strength, AbilityConstants.Constitution, AbilityConstants.Dexterity };

            foreach (var adjustment in animalAbilityAdjustments.Where(a => physicalAbilities.Contains(a.Type)))
            {
                abilities[adjustment.Type].Bonuses.Add(new Bonus
                {
                    Value = adjustment.Amount,
                    Condition = "In Animal or Hybrid form",
                });
            }
        }

        private HitPoints UpdateCreatureHitPoints(Creature creature, CreatureType animalCreatureType, CreatureDataSelection animalData, bool asCharacter)
        {
            var animalHitDiceQuantity = animalData.GetEffectiveHitDiceQuantity(asCharacter);
            var animalHitPoints = hitPointsGenerator.GenerateFor(
                animalHitDiceQuantity,
                animalData.HitDie,
                animalCreatureType,
                creature.Abilities[AbilityConstants.Constitution],
                animalData.Size);
            creature.HitPoints.HitDice.Add(animalHitPoints.HitDice[0]);

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);

            return animalHitPoints;
        }

        private void UpdateCreatureChallengeRating(Creature creature, HitPoints animalHitPoints)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, animalHitPoints.HitDice[0].Quantity);
        }

        private void UpdateCreatureChallengeRating(CreaturePrototype creature, double animalHitDiceQuantity)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, animalHitDiceQuantity);
        }

        private string UpdateCreatureChallengeRating(string challengeRating, double animalHitDiceQuantity)
        {
            var increase = 2;

            if (animalHitDiceQuantity > 20)
            {
                increase = 6;
            }
            else if (animalHitDiceQuantity > 10)
            {
                increase = 5;
            }
            else if (animalHitDiceQuantity > 5)
            {
                increase = 4;
            }
            else if (animalHitDiceQuantity > 2)
            {
                increase = 3;
            }

            return ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, increase);
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                if (IsNatural)
                    creature.LevelAdjustment += 3;
                else
                    creature.LevelAdjustment += 2;
            }
        }

        private void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
        {
            if (creature.LevelAdjustment.HasValue)
            {
                if (IsNatural)
                    creature.LevelAdjustment += 3;
                else
                    creature.LevelAdjustment += 2;
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

            var newCap = creature.HitPoints.RoundedHitDiceQuantity + 3;

            if (!IsNatural)
            {
                var controlShape = new Skill(
                    SkillConstants.Special.ControlShape,
                    creature.Abilities[AbilityConstants.Wisdom],
                    animalHitPoints.RoundedHitDiceQuantity);
                controlShape.ClassSkill = true;

                animalSkills = animalSkills.Union([controlShape]);

                foreach (var animalSkill in animalSkills)
                {
                    animalSkill.Ranks = 0;
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
                creatureSkill.RankCap = newCap;
            }

            foreach (var animalSkill in animalSkills)
            {
                animalSkill.RankCap = newCap;

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
                    creature.Skills = creature.Skills.Union([animalSkill]);
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
                    creature.SpecialQualities = creature.SpecialQualities.Union([sq]);
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
                    creature.SpecialQualities = creature.SpecialQualities.Union([sq]);
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
                    creature.Feats = creature.Feats.Union([feat]);
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

        private (IEnumerable<Attack> AnimalAttacks, int AnimalBaseAttack) UpdateCreatureAttacks(
            Creature creature,
            HitPoints animalHitPoints,
            CreatureDataSelection animalData)
        {
            var baseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(animalData.BaseAttackQuality, animalHitPoints);
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

            creature.GrappleBonus = attacksGenerator.GenerateGrappleBonus(
                creature.Name,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities[AbilityConstants.Strength]);

            var animalAttacks = attacksGenerator.GenerateAttacks(
                AnimalSpecies,
                animalData.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            animalAttacks = attacksGenerator.ApplyAttackBonuses(animalAttacks, allFeats, creature.Abilities);

            foreach (var animalAttack in animalAttacks)
            {
                animalAttack.Name += " (in Animal form)";
            }

            var biggerSize = GetBiggerSize(creature.Size, animalData.Size);
            var lycanthropeAttacks = attacksGenerator.GenerateAttacks(
                LycanthropeSpecies,
                biggerSize,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

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

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(LycanthropeSpecies);
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var animalData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, AnimalSpecies);
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Size,
                creature.ChallengeRating,
                animalData.Size,
                animalData.GetEffectiveHitDiceQuantity(asCharacter),
                filters);

            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([LycanthropeSpecies])]);
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
            var animalCreatureType = new CreatureType(animalData.Types);
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature, animalCreatureType, animalData, asCharacter));
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
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature, animalHitPoints, animalData));
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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var templateCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, LycanthropeSpecies + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);
            if (!filteredBaseCreatures.Any())
                return [];

            if (string.IsNullOrEmpty(filters?.ChallengeRating)
                && string.IsNullOrEmpty(filters?.Type)
                && string.IsNullOrEmpty(filters?.Alignment))
                return filteredBaseCreatures;

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);

            filteredBaseCreatures = filteredBaseCreatures
                .Where(c => AreFiltersCompatible(
                    allData[c].Single().Types,
                    allAlignments[c],
                    allData[c].Single().GetEffectiveChallengeRating(asCharacter),
                    allData[AnimalSpecies].Single().GetEffectiveHitDiceQuantity(asCharacter),
                    filters).Compatible);

            return filteredBaseCreatures;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureSize,
            string creatureChallengeRating,
            string animalSize,
            double animalHitDiceQuantity,
            Filters filters)
        {
            var compatibility = IsCompatible(types, creatureSize, animalSize);
            if (!compatibility.Compatible)
                return (false, compatibility.Reason);

            return AreFiltersCompatible(types, alignments, creatureChallengeRating, animalHitDiceQuantity, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            double animalHitDiceQuantity,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Type))
            {
                var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
                if (!updatedTypes.Contains(filters.Type))
                    return (false, $"Type filter '{filters.Type}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var cr = UpdateCreatureChallengeRating(creatureChallengeRating, animalHitDiceQuantity);
                if (cr != filters.ChallengeRating)
                    return (false, $"CR filter {filters.ChallengeRating} does not match updated creature CR {cr} (from CR {creatureChallengeRating})");
            }

            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                if (!alignments.Contains(filters.Alignment))
                    return (false, $"Alignment filter '{filters.Alignment}' is not valid");
            }

            return (true, null);
        }

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, string creatureSize, string animalSize)
        {
            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            var sizes = SizeConstants.GetOrdered();
            var creatureIndex = Array.IndexOf(sizes, creatureSize);
            var animalIndex = Array.IndexOf(sizes, animalSize);

            if (Math.Abs(creatureIndex - animalIndex) > 1)
                return (false, $"Sizes {creatureSize} (creature) and {animalSize} (animal) are too far apart");

            return (true, null);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, filters);
            if (!compatibleCreatures.Any())
                return [];

            var animalAbilityAdjustments = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, AnimalSpecies);
            var animalData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, AnimalSpecies);

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var animalHitDiceQuantity = animalData.GetEffectiveHitDiceQuantity(asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment, animalAbilityAdjustments, animalHitDiceQuantity));

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(
            CreaturePrototype prototype,
            string presetAlignment,
            IEnumerable<TypeAndAmountDataSelection> animalAbilityAdjustments,
            double animalHitDiceQuantity)
        {
            UpdateCreatureAbilities(prototype, animalAbilityAdjustments);
            UpdateCreatureChallengeRating(prototype, animalHitDiceQuantity);
            UpdateCreatureLevelAdjustment(prototype);
            UpdateCreatureType(prototype);

            if (!string.IsNullOrEmpty(presetAlignment))
            {
                prototype.Alignments = [.. prototype.Alignments.Where(adjustmentSelector => adjustmentSelector.Full == presetAlignment)];
            }

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var animalData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, AnimalSpecies);
            var animalAbilityAdjustments = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, AnimalSpecies);
            var animalHitDiceQuantity = animalData.GetEffectiveHitDiceQuantity(asCharacter);

            var compatiblePrototypes = sourceCreatures
                .Where(p => IsCompatible(
                    p.Type.AllTypes,
                    p.Alignments.Select(a => a.Full),
                    p.Size,
                    p.ChallengeRating,
                    animalData.Size,
                    animalHitDiceQuantity,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment, animalAbilityAdjustments, animalHitDiceQuantity));

            return updatedPrototypes;
        }
    }
}
