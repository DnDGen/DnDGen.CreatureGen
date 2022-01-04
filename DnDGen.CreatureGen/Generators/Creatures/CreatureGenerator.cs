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
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Generators;
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
        private readonly ICreatureDataSelector creatureDataSelector;
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

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
            ICreatureVerifier creatureVerifier,
            ICollectionSelector collectionsSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            IArmorClassGenerator armorClassGenerator,
            ISavesGenerator savesGenerator,
            JustInTimeFactory justInTimeFactory,
            IAdvancementSelector advancementSelector,
            IAttacksGenerator attacksGenerator,
            ISpeedsGenerator speedsGenerator,
            IEquipmentGenerator equipmentGenerator,
            IMagicGenerator magicGenerator,
            ILanguageGenerator languageGenerator)
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
        }

        public Creature Generate(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer = null) 
            => Generate(creatureName, template, asCharacter, abilityRandomizer, null);

        public (string Creature, string Template) GenerateRandomName(bool asCharacter, RandomFilters filters = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, null, filters);
            if (!compatible)
            {
                throw new InvalidCreatureException(null, asCharacter, null, filters);
            }

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            var validCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, group);

            if (filters?.Template == null)
            {
                return GetRandomValidCreature(validCreatures, asCharacter, filters);
            }

            validCreatures = GetCreaturesOfTemplate(filters.Template, validCreatures, asCharacter, filters);
            if (!validCreatures.Any())
            {
                throw new InvalidCreatureException($"No valid creatures ({group}) of template {filters.Template}", asCharacter, null, filters);
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);
            return (randomCreature, filters.Template);
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, bool asCharacter, RandomFilters filters)
        {
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            var creatures = templateApplicator.GetCompatibleCreatures(creatureGroup, asCharacter, filters);

            return creatures;
        }

        public Creature GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer, RandomFilters filters = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, filters);
            var creature = Generate(randomCreature.Creature, randomCreature.Template, asCharacter, abilityRandomizer, filters);

            return creature;
        }

        private IEnumerable<string> GetValidCreatures(IEnumerable<string> creatureGroup, bool asCharacter, RandomFilters filters)
        {
            var validCreatures = new List<string>();

            var compatibleCreatures = GetCreaturesOfTemplate(CreatureConstants.Templates.None, creatureGroup, asCharacter, filters);
            validCreatures.AddRange(compatibleCreatures);

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

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

        private (string CreatureName, string Template) GetRandomValidCreature(IEnumerable<string> creatureGroup, bool asCharacter, RandomFilters filters)
        {
            var validCreatures = GetValidCreatures(creatureGroup, asCharacter, filters);
            if (!validCreatures.Any())
            {
                throw new ArgumentException($"No valid creatures in creature group (as character: {asCharacter}; type: {filters?.Type}; CR: {filters?.ChallengeRating})");
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);
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
            string creatureName,
            string template,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer,
            RandomFilters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters);

            var creature = GeneratePrototype(creatureName, template, asCharacter, abilityRandomizer, filters);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature, asCharacter, filters);

            return creature;
        }

        private Creature GeneratePrototype(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer, RandomFilters filters)
        {
            var creature = new Creature();
            creature.Name = creatureName;

            var creatureData = creatureDataSelector.SelectFor(creatureName);
            creature.Size = creatureData.Size;
            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;
            creature.CanUseEquipment = creatureData.CanUseEquipment;
            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.LevelAdjustment = creatureData.LevelAdjustment;
            creature.CasterLevel = creatureData.CasterLevel;
            creature.NumberOfHands = creatureData.NumberOfHands;

            creature.Type = GetCreatureType(creatureName);
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName, abilityRandomizer);

            if (advancementSelector.IsAdvanced(creatureName, filters?.ChallengeRating))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName, template, creature.Type, creature.Size, creature.ChallengeRating);

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

            creature.Alignment = alignmentGenerator.Generate(creatureName, template, filters?.Alignment);
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
                creatureData.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

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

        private CreatureType GetCreatureType(string creatureName)
        {
            var creatureType = new CreatureType();
            var types = collectionsSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName);

            creatureType.Name = types.First();
            creatureType.SubTypes = types.Skip(1);

            return creatureType;
        }

        public async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer)
            => await GenerateAsync(creatureName, template, asCharacter, abilityRandomizer, null);

        public async Task<Creature> GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer, RandomFilters filters = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, filters);
            return await GenerateAsync(randomCreature.Creature, randomCreature.Template, asCharacter, abilityRandomizer, filters);
        }

        private async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer, RandomFilters filters)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, filters);
            if (!compatible)
                throw new InvalidCreatureException(null, asCharacter, creatureName, filters);

            var creature = GeneratePrototype(creatureName, template, asCharacter, abilityRandomizer, filters);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = await templateApplicator.ApplyToAsync(creature, asCharacter, filters);

            return creature;
        }
    }
}