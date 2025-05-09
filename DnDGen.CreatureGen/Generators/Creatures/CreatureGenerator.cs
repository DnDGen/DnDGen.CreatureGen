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
    internal class CreatureGenerator : ICreatureGenerator
    {
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly ICreatureVerifier creatureVerifier;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAbilitiesGenerator abilitiesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly IArmorClassGenerator armorClassGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly JustInTimeFactory justInTimeFactory;
        private readonly IAdvancementSelector advancementSelector;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IEquipmentGenerator equipmentGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ILanguageGenerator languageGenerator;
        private readonly IDemographicsGenerator demographicsGenerator;

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
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
            IDemographicsGenerator demographicsGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.creatureVerifier = creatureVerifier;
            this.collectionsSelector = collectionsSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.armorClassGenerator = armorClassGenerator;
            this.savesGenerator = savesGenerator;
            this.justInTimeFactory = justInTimeFactory;
            this.advancementSelector = advancementSelector;
            this.attacksGenerator = attacksGenerator;
            this.speedsGenerator = speedsGenerator;
            this.equipmentGenerator = equipmentGenerator;
            this.magicGenerator = magicGenerator;
            this.languageGenerator = languageGenerator;
            this.demographicsGenerator = demographicsGenerator;
        }

        public Creature Generate(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates)
            => Generate(asCharacter, creatureName, abilityRandomizer, new Filters { Templates = [.. templates] });

        public (string Creature, string[] Templates) GenerateRandomName(bool asCharacter, Filters filters = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, null, filters);
            if (!compatible)
            {
                throw new InvalidCreatureException(null, asCharacter, null, filters);
            }

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            var validCreatures = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group);

            if (filters?.CleanTemplates?.Any() != true)
            {
                var randomValidCreature = GetRandomValidCreature(validCreatures, asCharacter, filters);
                return (randomValidCreature.CreatureName, new[] { randomValidCreature.Template });
            }

            validCreatures = GetCreaturesOfTemplates(validCreatures, asCharacter, filters);
            if (!validCreatures.Any())
            {
                throw new InvalidCreatureException($"No valid creatures ({group}) of template {string.Join(", ", filters.CleanTemplates)}", asCharacter, null, filters);
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);
            return (randomCreature, filters.CleanTemplates?.ToArray());
        }

        private IEnumerable<string> GetCreaturesOfTemplates(IEnumerable<string> creatureGroup, bool asCharacter, Filters filters)
        {
            if (filters?.CleanTemplates.Any() != true)
                return [];

            var template = filters.CleanTemplates[0] ?? string.Empty;
            var applicator = justInTimeFactory.Build<TemplateApplicator>(template);
            IEnumerable<CreaturePrototype> prototypes;

            //INFO: We only want to apply filters to the last template in the series
            if (filters.CleanTemplates.Count == 1)
            {
                prototypes = applicator.GetCompatiblePrototypes(creatureGroup, asCharacter, filters);
            }
            else
            {
                prototypes = applicator.GetCompatiblePrototypes(creatureGroup, asCharacter);
            }

            for (var i = 1; i < filters.CleanTemplates.Count; i++)
            {
                template = filters.CleanTemplates[i] ?? string.Empty;
                applicator = justInTimeFactory.Build<TemplateApplicator>(template);

                //INFO: We only want to apply filters to the last template in the series
                if (i == filters.CleanTemplates.Count - 1)
                {
                    prototypes = applicator.GetCompatiblePrototypes(prototypes, asCharacter, filters);
                }
                else
                {
                    prototypes = applicator.GetCompatiblePrototypes(prototypes, asCharacter);
                }
            }

            return prototypes.Select(p => p.Name);
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, bool asCharacter, Filters filters)
        {
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            var creatures = templateApplicator.GetCompatibleCreatures(creatureGroup, asCharacter, filters);

            return creatures;
        }

        public Creature GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, filters);

            var nonNullTemplates = randomCreature.Templates.Where(t => t != CreatureConstants.Templates.None);
            if (filters?.CleanTemplates.Any() != true && nonNullTemplates.Any())
            {
                filters ??= new Filters();
                filters.Templates.AddRange(randomCreature.Templates);
            }

            var creature = Generate(asCharacter, randomCreature.Creature, abilityRandomizer, filters);
            return creature;
        }

        private IEnumerable<string> GetValidCreatures(IEnumerable<string> creatureGroup, bool asCharacter, Filters filters)
        {
            var validCreatures = new List<string>();

            var compatibleCreatures = GetCreaturesOfTemplate(CreatureConstants.Templates.None, creatureGroup, asCharacter, filters);
            validCreatures.AddRange(compatibleCreatures);

            var templates = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);

            //This will weight things in favor of non-templated creatures
            //INFO: Using this instead of the creature verifier, so that we can ensure compatiblity with the specified creature group
            foreach (var template in templates)
            {
                compatibleCreatures = GetCreaturesOfTemplate(template, creatureGroup, asCharacter, filters);
                if (compatibleCreatures.Any())
                    validCreatures.Add(template);
            }

            return validCreatures;
        }

        private (string CreatureName, string Template) GetRandomValidCreature(IEnumerable<string> creatureGroup, bool asCharacter, Filters filters)
        {
            var validCreatures = GetValidCreatures(creatureGroup, asCharacter, filters);
            if (!validCreatures.Any())
            {
                throw new ArgumentException($"No valid creatures in creature group (as character: {asCharacter}; type: {filters?.Type}; CR: {filters?.ChallengeRating})");
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);

            var templates = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);
            if (!templates.Contains(randomCreature))
                return (randomCreature, CreatureConstants.Templates.None);

            var template = randomCreature;

            var creaturesOfTemplate = GetCreaturesOfTemplate(template, creatureGroup, asCharacter, filters);
            if (!creaturesOfTemplate.Any())
            {
                throw new ArgumentException($"No valid creatures in creature group of template {template} (as character: {asCharacter}; type: {filters?.Type}; CR: {filters?.ChallengeRating})");
            }

            randomCreature = collectionsSelector.SelectRandomFrom(creaturesOfTemplate);

            return (randomCreature, template);
        }

        private Creature Generate(
            bool asCharacter,
            string creatureName,
            AbilityRandomizer abilityRandomizer,
            Filters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters);

            var creature = GenerateBaseCreature(creatureName, asCharacter, abilityRandomizer, filters);

            if (filters?.CleanTemplates?.Any() == true)
            {
                foreach (var template in filters.CleanTemplates.Take(filters.CleanTemplates.Count - 1))
                {
                    var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
                    creature = templateApplicator.ApplyTo(creature, asCharacter, null);
                }

                var lastTemplate = filters.CleanTemplates.Last();
                var lastTemplateApplicator = justInTimeFactory.Build<TemplateApplicator>(lastTemplate);
                creature = lastTemplateApplicator.ApplyTo(creature, asCharacter, filters);
            }

            return creature;
        }

        private Creature GenerateBaseCreature(string creatureName, bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            var templates = filters?.CleanTemplates ?? [];

            var creature = new Creature
            {
                Name = creatureName
            };

            var creatureData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, creatureName);
            creature.Size = creatureData.Size;
            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;
            creature.CanUseEquipment = creatureData.CanUseEquipment;
            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.LevelAdjustment = creatureData.LevelAdjustment;
            creature.CasterLevel = creatureData.CasterLevel;
            creature.NumberOfHands = creatureData.NumberOfHands;

            creature.Type = GetCreatureType(creatureName);
            creature.Demographics = demographicsGenerator.Generate(creatureName);

            abilityRandomizer ??= new AbilityRandomizer();
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName, abilityRandomizer, creature.Demographics);

            if (advancementSelector.IsAdvanced(creatureName, templates, filters?.ChallengeRating))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName, templates);

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
                    creatureName,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    advancement.AdditionalHitDice,
                    asCharacter);

                creature.Demographics = AdjustDemographics(creature.Demographics, creatureData.Size, advancement.Size);
            }
            else
            {
                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    creatureName,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    asCharacter: asCharacter);
            }

            if (creature.HitPoints.HitDiceQuantity == 0)
            {
                creature.ChallengeRating = ChallengeRatingConstants.CR0;
            }

            creature.Alignment = alignmentGenerator.Generate(creatureName, templates, filters?.Alignment);
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

            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creature.Type, creature.HitPoints);

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

        private int ComputeInitiativeBonus(IEnumerable<Feat> feats)
        {
            var initiativeBonus = 0;

            var improvedInitiative = feats.FirstOrDefault(f => f.Name == FeatConstants.Initiative_Improved);
            if (improvedInitiative != null)
                initiativeBonus += improvedInitiative.Power;

            return initiativeBonus;
        }

        private CreatureType GetCreatureType(CreatureDataSelection data) => new(data.Types);

        public async Task<Creature> GenerateAsync(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer, params string[] templates)
            => await GenerateAsync(asCharacter, creatureName, abilityRandomizer, new Filters { Templates = [.. templates] });

        public async Task<Creature> GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer, Filters filters = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, filters);

            var nonNullTemplates = randomCreature.Templates.Where(t => t != CreatureConstants.Templates.None);
            if (filters?.CleanTemplates.Any() != true && nonNullTemplates.Any())
            {
                filters ??= new Filters();
                filters.Templates.AddRange(randomCreature.Templates);
            }

            return await GenerateAsync(asCharacter, randomCreature.Creature, abilityRandomizer, filters);
        }

        private async Task<Creature> GenerateAsync(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer, Filters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters);

            var creature = GenerateBaseCreature(creatureName, asCharacter, abilityRandomizer, filters);

            if (filters?.CleanTemplates?.Any() == true)
            {
                foreach (var template in filters.CleanTemplates.Take(filters.CleanTemplates.Count - 1))
                {
                    var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
                    creature = await templateApplicator.ApplyToAsync(creature, asCharacter, null);
                }

                var lastTemplate = filters.CleanTemplates.Last();
                var lastTemplateApplicator = justInTimeFactory.Build<TemplateApplicator>(lastTemplate);
                creature = await lastTemplateApplicator.ApplyToAsync(creature, asCharacter, filters);
            }

            return creature;
        }
    }
}