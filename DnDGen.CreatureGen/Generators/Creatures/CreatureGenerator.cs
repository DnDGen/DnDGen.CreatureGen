using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Generators.Languages;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class CreatureGenerator(IAlignmentGenerator alignmentGenerator,
        ICreatureVerifier creatureVerifier,
        ICollectionSelector collectionsSelector,
        IAbilitiesGenerator abilitiesGenerator,
        ISkillsGenerator skillsGenerator,
        IFeatsGenerator featsGenerator,
        ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
        IHitPointsGenerator hitPointsGenerator,
        IArmorClassGenerator armorClassGenerator,
        ISavesGenerator savesGenerator,
        JustInTimeFactory justInTimeFactory,
        IAdvancementSelector advancementSelector,
        IAttacksGenerator attacksGenerator,
        ISpeedsGenerator speedsGenerator,
        IEquipmentGenerator equipmentGenerator,
        IMagicGenerator magicGenerator,
        ILanguageGenerator languageGenerator,
        IDemographicsGenerator demographicsGenerator) : ICreatureGenerator
    {
        public Creature Generate(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates)
            => Generate(asCharacter, creatureName, templates, abilityRandomizer, null);

        public (string Creature, string[] Templates) GenerateRandomName(bool asCharacter, Filters filters = null, params string[] templates)
            => GenerateRandomName(asCharacter, templates, null, filters);

        private (string Creature, string[] Templates) GenerateRandomName(bool asCharacter, string[] templates, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            if (!compatible)
            {
                throw new InvalidCreatureException(null, asCharacter, null, filters, abilityRandomizer ?? new());
            }

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            var validCreatures = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group);

            if (templates.Length == 0)
            {
                var (CreatureName, Template) = GetRandomValidCreature(validCreatures, asCharacter, abilityRandomizer, filters);
                return (CreatureName, new[] { Template });
            }

            var prototypes = creatureVerifier.GetChainedTemplates(validCreatures, templates, asCharacter, abilityRandomizer, filters);
            if (!prototypes.Any())
            {
                throw new InvalidCreatureException($"No valid creatures ({group}) of template {string.Join(", ", templates)}", asCharacter, null, filters);
            }

            var randomPrototype = collectionsSelector.SelectRandomFrom(prototypes);
            return (randomPrototype.Name, [.. randomPrototype.Templates]);
        }

        public Creature GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters = null, params string[] templates)
        {
            var randomCreature = GenerateRandomName(asCharacter, templates, abilityRandomizer, filters);
            var creature = Generate(asCharacter, randomCreature.Creature, randomCreature.Templates, abilityRandomizer, filters);

            return creature;
        }

        private IEnumerable<string> GetValidCreatures(IEnumerable<string> creatureGroup, bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            var compatibleCreatures = creatureVerifier.GetCompatibleCreaturesForTemplate(creatureGroup, null, asCharacter, abilityRandomizer, filters);

            foreach (var creature in compatibleCreatures)
                yield return creature;

            var templates = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);

            //INFO: By only adding 1 entry for each compatible template (instead of 1 per template-base pairing),
            //odds are weighted in favor of non-templated creatures
            foreach (var template in templates)
            {
                compatibleCreatures = creatureVerifier.GetCompatibleCreaturesForTemplate(creatureGroup, template, asCharacter, abilityRandomizer, filters);
                if (compatibleCreatures.Any())
                    yield return template;
            }
        }

        private (string CreatureName, string Template) GetRandomValidCreature(
            IEnumerable<string> creatureGroup,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer,
            Filters filters)
        {
            var validCreatures = GetValidCreatures(creatureGroup, asCharacter, abilityRandomizer, filters);
            if (!validCreatures.Any())
            {
                var filtersDescription = filters.GetDescription(asCharacter);
                throw new ArgumentException($"No valid creatures in creature group (filters: {filtersDescription})");
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);

            var templates = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);
            if (!templates.Contains(randomCreature))
                return (randomCreature, CreatureConstants.Templates.None);

            var template = randomCreature;

            var creaturesOfTemplate = creatureVerifier.GetCompatibleCreaturesForTemplate(creatureGroup, template, asCharacter, abilityRandomizer, filters);
            if (!creaturesOfTemplate.Any())
            {
                var filtersDescription = filters.GetDescription(asCharacter);
                throw new ArgumentException($"No valid creatures in creature group of template {template} (filters: {filtersDescription})");
            }

            randomCreature = collectionsSelector.SelectRandomFrom(creaturesOfTemplate);

            return (randomCreature, template);
        }

        private Creature Generate(
            bool asCharacter,
            string creatureName,
            string[] templates,
            AbilityRandomizer abilityRandomizer,
            Filters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, abilityRandomizer, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters, abilityRandomizer ?? new());

            var creature = GenerateBaseCreature(creatureName, templates, asCharacter, abilityRandomizer, filters);

            if (templates.Length > 0)
            {
                for (var i = 0; i < templates.Length - 1; i++)
                {
                    var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(templates[i]);
                    creature = templateApplicator.ApplyTo(creature, asCharacter, null);
                }

                var lastTemplate = templates[^1];
                var lastTemplateApplicator = justInTimeFactory.Build<TemplateApplicator>(lastTemplate);
                creature = lastTemplateApplicator.ApplyTo(creature, asCharacter, filters);
            }

            return creature;
        }

        private Creature GenerateBaseCreature(string creatureName, string[] templates, bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            templates ??= [];

            var creature = new Creature
            {
                Name = creatureName
            };

            var creatureData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, creatureName);
            creature.Size = creatureData.Size;
            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;
            creature.CanUseEquipment = creatureData.CanUseEquipment;
            creature.ChallengeRating = creatureData.GetEffectiveChallengeRating(asCharacter);
            creature.LevelAdjustment = creatureData.LevelAdjustment;
            creature.CasterLevel = creatureData.CasterLevel;
            creature.NumberOfHands = creatureData.NumberOfHands;
            creature.HasSkeleton = creatureData.HasSkeleton;

            creature.Type = GetCreatureType(creatureData);
            creature.Demographics = demographicsGenerator.Generate(creatureName);
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName, [.. templates], asCharacter, abilityRandomizer, creature.Demographics);

            var hitDiceQuantity = creatureData.GetEffectiveHitDiceQuantity(asCharacter);

            if (advancementSelector.IsAdvanced(creatureName, templates, hitDiceQuantity, filters))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName, templates, hitDiceQuantity, filters);

                creature.IsAdvanced = true;
                creature.Size = advancement.Size;
                creature.Space.Value = advancement.Space;
                creature.Reach.Value = advancement.Reach;
                creature.CasterLevel += advancement.CasterLevelAdjustment;
                creature.ChallengeRating = advancement.AdjustedChallengeRating;
                creatureData.NaturalArmor += advancement.NaturalArmorAdjustment;

                creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment += advancement.StrengthAdjustment;
                creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment += advancement.DexterityAdjustment;
                creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment += advancement.ConstitutionAdjustment;

                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    hitDiceQuantity,
                    creatureData.HitDie,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    advancement.AdditionalHitDice);

                creature.Demographics = demographicsGenerator.AdjustDemographicsBySize(creature.Demographics, creatureData.Size, advancement.Size);
            }
            else
            {
                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    hitDiceQuantity,
                    creatureData.HitDie,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size);
            }

            if (creature.HitPoints.HitDiceQuantity == 0)
            {
                creature.ChallengeRating = ChallengeRatingConstants.CR0;
            }

            creature.Alignment = alignmentGenerator.Generate(creatureName, templates, filters);
            creature.Skills = skillsGenerator.GenerateFor(creature.HitPoints, creatureName, creature.Type, creature.Abilities, creature.CanUseEquipment, creature.Size);
            creature.Languages = languageGenerator.GenerateWith(creatureName, creature.Abilities, creature.Skills);

            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(
                creatureName,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creatureData.BaseAttackQuality, creature.HitPoints);

            creature.Attacks = attacksGenerator.GenerateAttacks(
                creatureName,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

            creature.Feats = featsGenerator.GenerateFeats(
                creature.HitPoints,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.Skills,
                creature.Attacks,
                creature.SpecialQualities,
                creature.CasterLevel,
                creature.Speeds,
                creatureData.NaturalArmor,
                creature.NumberOfHands,
                creature.Size,
                creature.CanUseEquipment);

            creature.Skills = skillsGenerator.ApplyBonusesFromFeats(creature.Skills, creature.Feats, creature.Abilities);
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.Feats);

            creature.GrappleBonus = attacksGenerator.GenerateGrappleBonus(
                creatureName,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities[AbilityConstants.Strength]);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            creature.Attacks = attacksGenerator.ApplyAttackBonuses(creature.Attacks, allFeats, creature.Abilities);
            creature.Attacks = equipmentGenerator.AddAttacks(allFeats, creature.Attacks, creature.NumberOfHands);

            creature.Equipment = equipmentGenerator.Generate(
                creature.Name,
                creature.CanUseEquipment,
                allFeats,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Attacks,
                creature.Abilities,
                creature.Size);

            creature.Abilities = abilitiesGenerator.SetMaxBonuses(creature.Abilities, creature.Equipment);
            creature.Skills = skillsGenerator.SetArmorCheckPenalties(creature.Name, creature.Skills, creature.Equipment);
            creature.InitiativeBonus = ComputeInitiativeBonus(creature.Feats);
            creature.Speeds = speedsGenerator.Generate(creature.Name);

            creature.ArmorClass = armorClassGenerator.GenerateWith(
                creature.Abilities,
                creature.Size,
                creatureName,
                creature.Type,
                allFeats,
                creatureData.NaturalArmor,
                creature.Equipment);

            creature.Saves = savesGenerator.GenerateWith(creature.Name, creature.Type, creature.HitPoints, allFeats, creature.Abilities);
            creature.Magic = magicGenerator.GenerateWith(creature.Name, creature.Alignment, creature.Abilities, creature.Equipment);

            return creature;
        }

        private static int ComputeInitiativeBonus(IEnumerable<Feat> feats)
        {
            var initiativeBonus = 0;

            var improvedInitiative = feats.FirstOrDefault(f => f.Name == FeatConstants.Initiative_Improved);
            if (improvedInitiative != null)
                initiativeBonus += improvedInitiative.Power;

            return initiativeBonus;
        }

        private static CreatureType GetCreatureType(CreatureDataSelection data) => new(data.Types);

        public async Task<Creature> GenerateAsync(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer, params string[] templates)
            => await GenerateAsync(asCharacter, creatureName, templates, abilityRandomizer, null);

        public async Task<Creature> GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters = null, params string[] templates)
        {
            var randomCreature = GenerateRandomName(asCharacter, templates, abilityRandomizer, filters);
            return await GenerateAsync(asCharacter, randomCreature.Creature, randomCreature.Templates, abilityRandomizer, filters);
        }

        private async Task<Creature> GenerateAsync(bool asCharacter, string creatureName, string[] templates, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, abilityRandomizer, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters, abilityRandomizer);

            var creature = GenerateBaseCreature(creatureName, templates, asCharacter, abilityRandomizer, filters);

            if (templates.Length > 0)
            {
                for (var i = 0; i < templates.Length - 1; i++)
                {
                    var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(templates[i]);
                    creature = await templateApplicator.ApplyToAsync(creature, asCharacter, null);
                }

                var lastTemplate = templates[^1];
                var lastTemplateApplicator = justInTimeFactory.Build<TemplateApplicator>(lastTemplate);
                creature = await lastTemplateApplicator.ApplyToAsync(creature, asCharacter, filters);
            }

            return creature;
        }
    }
}