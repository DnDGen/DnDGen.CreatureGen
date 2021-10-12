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

        public Creature Generate(string creatureName, string template, bool asCharacter) => Generate(creatureName, template, asCharacter, true);

        public (string Creature, string Template) GenerateRandomName(bool asCharacter, string template = null, string type = null, string challengeRating = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, template: template, type: type, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(asCharacter, template: template, type: type, challengeRating: challengeRating);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            var validCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, group);

            if (string.IsNullOrEmpty(template))
            {
                return GetRandomValidCreature(validCreatures, asCharacter, type, challengeRating);
            }

            validCreatures = GetCreaturesOfTemplate(template, validCreatures, asCharacter, type, challengeRating);
            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);
            return (randomCreature, template);
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, bool asCharacter, string creatureType = null, string challengeRating = null)
        {
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            var creatures = templateApplicator.GetCompatibleCreatures(creatureGroup, asCharacter, creatureType, challengeRating);

            return creatures;
        }

        public Creature GenerateRandom(bool asCharacter, string template = null, string type = null, string challengeRating = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, template, type, challengeRating);
            var allowAdvanced = string.IsNullOrEmpty(challengeRating);

            return Generate(randomCreature.Creature, randomCreature.Template, asCharacter, allowAdvanced);
        }

        private IEnumerable<string> GetValidCreatures(
            IEnumerable<string> creatureGroup,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            var validCreatures = new List<string>();

            var compatibleCreatures = GetCreaturesOfTemplate(CreatureConstants.Templates.None, creatureGroup, asCharacter, type, challengeRating);
            validCreatures.AddRange(compatibleCreatures);

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            //This will weight things in favor of non-templated creatures
            //INFO: Using this instead of the creature verifier, so that we can ensure compatiblity with the specified creature group
            foreach (var template in templates)
            {
                compatibleCreatures = GetCreaturesOfTemplate(template, creatureGroup, asCharacter, type, challengeRating);
                if (compatibleCreatures.Any())
                    validCreatures.Add(template);
            }

            return validCreatures;
        }

        private (string CreatureName, string Template) GetRandomValidCreature(
            IEnumerable<string> creatureGroup,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            var validCreatures = GetValidCreatures(creatureGroup, asCharacter, type, challengeRating);
            if (!validCreatures.Any())
            {
                throw new ArgumentException($"No valid creatures in creature group (as character: {asCharacter}; type: {type}; CR: {challengeRating})");
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);
            if (!templates.Contains(randomCreature))
                return (randomCreature, CreatureConstants.Templates.None);

            var template = randomCreature;

            var creaturesOfTemplate = GetCreaturesOfTemplate(template, creatureGroup, asCharacter, type, challengeRating);
            if (!creaturesOfTemplate.Any())
            {
                throw new ArgumentException($"No valid creatures in creature group of template {template} (as character: {asCharacter}; type: {type}; CR: {challengeRating})");
            }

            randomCreature = collectionsSelector.SelectRandomFrom(creaturesOfTemplate);

            return (randomCreature, template);
        }

        private Creature Generate(string creatureName, string template, bool asCharacter, bool allowAdvancement)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creature: creatureName, template: template);
            if (!compatible)
                throw new InvalidCreatureException(asCharacter, creature: creatureName, template: template);

            var creature = GeneratePrototype(creatureName, asCharacter, allowAdvancement);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature);

            return creature;
        }

        private Creature GeneratePrototype(string creatureName, bool asCharacter, bool allowAdvancement)
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
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName);

            if (allowAdvancement && advancementSelector.IsAdvanced(creatureName))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName, creature.Type, creature.Size, creature.ChallengeRating);

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
                    advancement.AdditionalHitDice, asCharacter);
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

            creature.Alignment = alignmentGenerator.Generate(creatureName);
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

        public async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter) => await GenerateAsync(creatureName, template, asCharacter, true);

        public async Task<Creature> GenerateRandomAsync(bool asCharacter, string template = null, string type = null, string challengeRating = null)
        {
            var randomCreature = GenerateRandomName(asCharacter, template, type, challengeRating);
            return await GenerateAsync(randomCreature.Creature, randomCreature.Template, asCharacter);
        }

        private async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter, bool allowAdvancement)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creature: creatureName, template: template);
            if (!compatible)
                throw new InvalidCreatureException(asCharacter, creature: creatureName, template: template);

            var creature = GeneratePrototype(creatureName, asCharacter, allowAdvancement);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = await templateApplicator.ApplyToAsync(creature);

            return creature;
        }
    }
}