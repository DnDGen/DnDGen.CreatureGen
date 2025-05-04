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
using DnDGen.CreatureGen.Selectors.Selections;
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
    internal class HalfCelestialApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IEnumerable<string> creatureTypes;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly ICreaturePrototypeFactory prototypeFactory;
        private readonly IDemographicsGenerator demographicsGenerator;

        public HalfCelestialApplicator(
            ICollectionSelector collectionSelector,
            ICollectionTypeAndAmountSelector typeAndAmountSelector,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISkillsGenerator skillsGenerator,
            IMagicGenerator magicGenerator,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            ICreaturePrototypeFactory prototypeFactory,
            IDemographicsGenerator demographicsGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.skillsGenerator = skillsGenerator;
            this.magicGenerator = magicGenerator;
            this.creatureDataSelector = creatureDataSelector;
            this.prototypeFactory = prototypeFactory;
            this.demographicsGenerator = demographicsGenerator;

            creatureTypes =
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
        }

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[AbilityConstants.Intelligence],
                creature.ChallengeRating,
                creature.HitPoints.RoundedHitDiceQuantity,
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
            UpdateCreatureAlignment(creature, filters?.Alignment);

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

        private void UpdateCreatureType(Creature creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private void UpdateCreatureType(CreaturePrototype creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Outsider }
                .Union(subtypes)
                .Union([CreatureConstants.Types.Subtypes.Native, CreatureConstants.Types.Subtypes.Augmented, creatureType]);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.Update(creature.Demographics, creature.Name, CreatureConstants.Templates.HalfCelestial, true);
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

        private void UpdateCreatureArmorClass(Creature creature)
        {
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                naturalArmorBonus.Value++;
            }

            if (!creature.ArmorClass.NaturalArmorBonuses.Any())
            {
                creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 1);
            }
        }

        private void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);

        private void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
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

        private void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private void UpdateCreatureAlignment(Creature creature, string presetAlignment)
        {
            creature.Alignment = UpdateCreatureAlignment(creature.Alignment, presetAlignment);
        }

        private void UpdateCreatureAlignment(CreaturePrototype creature, string presetAlignment)
        {
            creature.Alignments = [.. creature.Alignments
                .Where(a => a.Goodness != AlignmentConstants.Evil)
                .Select(a => UpdateCreatureAlignment(a, presetAlignment))
                .Distinct()];
        }

        private Alignment UpdateCreatureAlignment(Alignment alignment, string presetAlignment)
        {
            if (presetAlignment != null)
            {
                return new Alignment(presetAlignment);
            }

            return UpdateCreatureAlignment(alignment.Full);
        }

        private Alignment UpdateCreatureAlignment(string alignment)
        {
            var newAlignment = new Alignment(alignment)
            {
                Goodness = AlignmentConstants.Good
            };

            return newAlignment;
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, creature.HitPoints.RoundedHitDiceQuantity);
        }

        private void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, creature.GetRoundedHitDiceQuantity());
        }

        private string UpdateCreatureChallengeRating(string challengeRating, double hitDiceQuantity)
        {
            return UpdateCreatureChallengeRating(challengeRating, HitDice.GetRoundedQuantity(hitDiceQuantity));
        }

        private string UpdateCreatureChallengeRating(string challengeRating, int hitDiceQuantity)
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

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 4;
        }

        private void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
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

        private void UpdateCreatureSavingThrows(Creature creature)
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

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(CreatureConstants.Templates.HalfCelestial);
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                [creature.Alignment.Full],
                creature.Abilities[AbilityConstants.Intelligence],
                creature.ChallengeRating,
                creature.HitPoints.RoundedHitDiceQuantity,
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
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature, filters?.Alignment));
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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var presetAlignment = new Alignment(filters.Alignment);
                if (presetAlignment.Goodness != AlignmentConstants.Good)
                {
                    return [];
                }
            }

            var templateCreatures = collectionSelector.SelectFrom(
                Config.Name,
                TableNameConstants.Collection.CreatureGroups,
                CreatureConstants.Templates.HalfCelestial + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);
            if (!filteredBaseCreatures.Any())
                return [];

            if (string.IsNullOrEmpty(filters?.ChallengeRating)
                && string.IsNullOrEmpty(filters?.Type)
                && string.IsNullOrEmpty(filters?.Alignment))
                return filteredBaseCreatures;

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allHitDice = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);

            filteredBaseCreatures = filteredBaseCreatures
                .Where(c => AreFiltersCompatible(
                    allTypes[c],
                    allAlignments[c],
                    allData[c].Single().ChallengeRating,
                    asCharacter,
                    allHitDice[c].Single().AmountAsDouble,
                    filters).Compatible);

            return filteredBaseCreatures;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            Ability intelligence,
            string creatureChallengeRating,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            var compatibility = IsCompatible(types, alignments, intelligence);
            if (!compatibility.Compatible)
                return (false, compatibility.Reason);

            //INFO: This method is used when the creature has already been generated, either as Creature or Prototype
            //The character challenge rating has already been accounted for
            return AreFiltersCompatible(types, alignments, creatureChallengeRating, false, creatureHitDiceQuantity, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            string creatureChallengeRating,
            bool adjustCharacterChallengeRating,
            double creatureHitDiceQuantity,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment))
            {
                var presetAlignment = new Alignment(filters.Alignment);
                if (presetAlignment.Goodness != AlignmentConstants.Good)
                {
                    return (false, $"Alignment filter '{filters.Alignment}' is not valid");
                }

                var newAlignments = alignments
                    .Where(a => !a.Contains(AlignmentConstants.Evil))
                    .Select(UpdateCreatureAlignment);
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
                var creatureType = types.First();

                if (adjustCharacterChallengeRating && creatureHitDiceQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
                {
                    creatureChallengeRating = ChallengeRatingConstants.CR0;
                }

                var cr = UpdateCreatureChallengeRating(creatureChallengeRating, creatureHitDiceQuantity);
                if (cr != filters.ChallengeRating)
                    return (false, $"CR filter {filters.ChallengeRating} does not match updated creature CR {cr} (from CR {creatureChallengeRating})");
            }

            return (true, null);
        }

        private (bool Compatible, string Reason) IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments, Ability intelligence)
        {
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return (false, "Creature is Incorporeal");

            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            if (!alignments.Any(a => !a.Contains(AlignmentConstants.Evil)))
                return (false, "Creature has no non-evil alignments");

            if (intelligence.FullScore < 4)
                return (false, $"Creature has insufficient Intelligence ({intelligence.FullScore}, needs 4)");

            return (true, null);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, filters);
            if (!compatibleCreatures.Any())
                return [];

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);
            var updatedPrototypes = prototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(CreaturePrototype prototype, string presetAlignment)
        {
            UpdateCreatureAbilities(prototype);
            UpdateCreatureAlignment(prototype, presetAlignment);
            UpdateCreatureChallengeRating(prototype);
            UpdateCreatureLevelAdjustment(prototype);
            UpdateCreatureType(prototype);

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatiblePrototypes = sourceCreatures
                .Where(p => IsCompatible(
                    p.Type.AllTypes,
                    p.Alignments.Select(a => a.Full),
                    p.Abilities[AbilityConstants.Intelligence],
                    p.ChallengeRating,
                    p.HitDiceQuantity,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(p => ApplyToPrototype(p, filters?.Alignment));

            return updatedPrototypes;
        }
    }
}
