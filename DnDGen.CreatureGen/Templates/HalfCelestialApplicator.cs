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
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly ICreatureDataSelector creatureDataSelector;

        public HalfCelestialApplicator(
            ICollectionSelector collectionSelector,
            ITypeAndAmountSelector typeAndAmountSelector,
            ISpeedsGenerator speedsGenerator,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISkillsGenerator skillsGenerator,
            IMagicGenerator magicGenerator,
            IAdjustmentsSelector adjustmentSelector,
            ICreatureDataSelector creatureDataSelector)
        {
            this.collectionSelector = collectionSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.skillsGenerator = skillsGenerator;
            this.magicGenerator = magicGenerator;
            this.adjustmentSelector = adjustmentSelector;
            this.creatureDataSelector = creatureDataSelector;

            creatureTypes = new[]
            {
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
            };
        }

        public Creature ApplyTo(Creature creature)
        {
            // Template
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature template");
            UpdateCreatureTemplate(creature);

            // Creature type
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature type");
            UpdateCreatureType(creature);

            // Challenge ratings
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature CR");
            UpdateCreatureChallengeRating(creature);

            //Speed
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature speeds");
            UpdateCreatureSpeeds(creature);

            // Abilities
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature abilities");
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature level adjustment");
            UpdateCreatureLevelAdjustment(creature);

            // Saving Throws
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature saving throws");
            UpdateCreatureSavingThrows(creature);

            // Alignment
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature alignment");
            UpdateCreatureAlignment(creature);

            //Armor Class
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature armor class");
            UpdateCreatureArmorClass(creature);

            //INFO: Depends on abilities
            // Languages
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature languages");
            UpdateCreatureLanguages(creature);

            //INFO: Depends on abilities
            // Attacks
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature attacks");
            UpdateCreatureAttacks(creature);

            //INFO: Depends on creature type, abilities
            //Skills
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature skills");
            UpdateCreatureSkills(creature);

            //INFO: This depends on alignment, abilities
            // Magic
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature magic");
            UpdateCreatureMagic(creature);

            //INFO: Depends on creature type, abilities, skills, alignment
            // Special Qualities
            Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Updating creature special qualities");
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

            creature.Type.Name = adjustedTypes.First();
            creature.Type.SubTypes = adjustedTypes.Skip(1);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Outsider }
                .Union(subtypes)
                .Union(new[] { CreatureConstants.Types.Subtypes.Native, CreatureConstants.Types.Subtypes.Augmented, creatureType });
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

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Strength].HasScore)
                creature.Abilities[AbilityConstants.Strength].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Dexterity].HasScore)
                creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Constitution].HasScore)
                creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = 2;

            if (creature.Abilities[AbilityConstants.Wisdom].HasScore)
                creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 4;

            if (creature.Abilities[AbilityConstants.Charisma].HasScore)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 4;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Goodness = AlignmentConstants.Good;
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.ChallengeRating, creature.HitPoints.RoundedHitDiceQuantity);
        }

        private IEnumerable<string> GetChallengeRatings(string challengeRating) => new[]
        {
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 1),
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2),
            ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 3),
        };

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
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

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
                    creature.SpecialQualities = creature.SpecialQualities.Union(new[] { sq });
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
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.HalfCelestial + LanguageConstants.Groups.Automatic);

            languages.Add(automaticLanguage);

            var bonusLanguages = collectionSelector.SelectFrom(
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
            creature.Template = CreatureConstants.Templates.HalfCelestial;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

            // Creature type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

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
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
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

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, string type = null, string challengeRating = null)
        {
            var filteredBaseCreatures = sourceCreatures;

            var allData = creatureDataSelector.SelectAll();
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups);

            //INFO: Select all of ability adjustments is slow, might be better to let this still be individual/per creature
            //Console.WriteLine($"[{DateTime.Now:O}] HalfCelestialApplicator: Getting all ability adjustments");
            //var allAbilityAdjustments = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.AbilityAdjustments);

            if (!string.IsNullOrEmpty(challengeRating))
            {
                filteredBaseCreatures = filteredBaseCreatures
                    .Where(c => CreatureInRange(allData[c].ChallengeRating, challengeRating, asCharacter, allHitDice[c], allTypes[c]));
            }

            if (!string.IsNullOrEmpty(type))
            {
                //INFO: Unless this type is added by a template, it must already exist on the base creature
                //So first, we check to see if the template could return this type for a human
                //If not, then we can filter the base creatures down to ones that already have this type
                var templateTypes = GetPotentialTypes(allTypes[CreatureConstants.Human]).Except(allTypes[CreatureConstants.Human]);
                if (!templateTypes.Contains(type))
                {
                    filteredBaseCreatures = filteredBaseCreatures.Where(c => allTypes[c].Contains(type));
                }
            }

            var templateCreatures = filteredBaseCreatures
                //.Where(c => IsCompatible(allTypes[c], allAlignments[c], allAbilityAdjustments[c], allData[c], allHitDice[c], asCharacter, type, challengeRating));
                .Where(c => IsCompatible(allTypes[c], allAlignments[c], c, allData[c], allHitDice[c], asCharacter, type, challengeRating));

            return templateCreatures;
        }

        private IEnumerable<string> GetPotentialTypes(IEnumerable<string> types)
        {
            var creatureType = types.First();
            var subtypes = types.Skip(1);

            var adjustedTypes = UpdateCreatureType(creatureType, subtypes);

            return adjustedTypes;
        }

        private bool IsCompatible(
            IEnumerable<string> types,
            IEnumerable<string> alignments,
            //IEnumerable<TypeAndAmountSelection> abilityAdjustments,
            string creature,
            CreatureDataSelection creatureData,
            double creatureHitDiceQuantity,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                var updatedTypes = GetPotentialTypes(types);
                if (!updatedTypes.Contains(type))
                    return false;
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var cr = GetPotentialChallengeRating(asCharacter, types, creatureHitDiceQuantity, creatureData);
                if (cr != challengeRating)
                    return false;
            }

            //if (!IsCompatible(types, alignments, abilityAdjustments))
            if (!IsCompatible(types, alignments, creature))
                return false;

            return true;
        }

        //private bool IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments, IEnumerable<TypeAndAmountSelection> abilityAdjustments)
        private bool IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments, string creature)
        {
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            if (!creatureTypes.Contains(types.First()))
                return false;

            if (!alignments.Any(a => !a.Contains(AlignmentConstants.Evil)))
                return false;

            var abilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, creature);
            var intelligenceAdjustment = abilityAdjustments.FirstOrDefault(a => a.Type == AbilityConstants.Intelligence);
            if (intelligenceAdjustment == null)
                return false;

            return intelligenceAdjustment.Amount + Ability.DefaultScore >= 4;
        }

        private string GetPotentialChallengeRating(
            bool asCharacter,
            IEnumerable<string> types,
            double creatureQuantity,
            CreatureDataSelection creatureData)
        {
            var creatureType = types.First();
            var hitDice = new HitDice { Quantity = creatureQuantity };

            if (asCharacter && creatureQuantity <= 1 && creatureType == CreatureConstants.Types.Humanoid)
            {
                return UpdateCreatureChallengeRating(ChallengeRatingConstants.CR0, hitDice.RoundedQuantity);
            }

            var adjustedChallengeRating = UpdateCreatureChallengeRating(creatureData.ChallengeRating, hitDice.RoundedQuantity);

            return adjustedChallengeRating;
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
