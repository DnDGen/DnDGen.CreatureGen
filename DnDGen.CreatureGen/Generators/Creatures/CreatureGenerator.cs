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

        public Creature Generate(string creatureName, string template) => Generate(creatureName, template, false);
        public Creature GenerateAsCharacter(string creatureName, string template) => Generate(creatureName, template, true);

        public string GenerateRandomNameOfTemplate(string template, string challengeRating = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(false, template: template, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(false, template: template, challengeRating: challengeRating);

            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            var templateCreatures = GetCreaturesOfTemplate(template, creatures, false, challengeRating: challengeRating);

            var randomCreature = collectionsSelector.SelectRandomFrom(templateCreatures);
            return randomCreature;
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, bool asCharacter, string creatureType = null, string challengeRating = null)
        {
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            var creatures = templateApplicator.GetCompatibleCreatures(creatureGroup, asCharacter, creatureType, challengeRating);

            return creatures;
        }

        public Creature GenerateRandomOfTemplate(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplate(template, challengeRating);
            return Generate(randomCreature, template);
        }

        public string GenerateRandomNameOfTemplateAsCharacter(string template, string challengeRating = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(true, template: template, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(true, template: template, challengeRating: challengeRating);

            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var templateCreatures = GetCreaturesOfTemplate(template, creatures, true, challengeRating: challengeRating);

            if (!templateCreatures.Any())
            {
                var cr = string.IsNullOrEmpty(challengeRating) ? string.Empty : $" (CR {challengeRating})";
                throw new Exception($"No characters exist for template {template}{cr}");
            }

            var randomCreature = collectionsSelector.SelectRandomFrom(templateCreatures);
            return randomCreature;
        }

        public Creature GenerateRandomOfTemplateAsCharacter(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplateAsCharacter(template, challengeRating);
            return Generate(randomCreature, template, true);
        }

        public (string CreatureName, string Template) GenerateRandomNameOfType(string creatureType, string challengeRating = null)
        {
            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating random creature name of type '{creatureType}' (CR {challengeRating ?? "None"})");

            var compatible = creatureVerifier.VerifyCompatibility(false, type: creatureType, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(false, type: creatureType, challengeRating: challengeRating);

            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            var randomCreature = GetRandomValidCreature(creatures, false, creatureType, challengeRating);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Creature name: {randomCreature}");
            return randomCreature;
        }

        private IEnumerable<string> GetValidCreatures(
            IEnumerable<string> creatureGroup,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            var noneApplicator = justInTimeFactory.Build<TemplateApplicator>(CreatureConstants.Templates.None);
            var creaturesOfType = noneApplicator.GetCompatibleCreatures(creatureGroup, asCharacter, type, challengeRating);

            foreach (var creature in creaturesOfType)
                yield return creature;

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            //This will weight things in favor of non-templated creatures
            foreach (var template in templates)
            {
                if (creatureVerifier.VerifyCompatibility(asCharacter, null, template, type, challengeRating))
                    yield return template;
            }
        }

        private (string CreatureName, string Template) GetRandomValidCreature(
            IEnumerable<string> creatureGroup,
            bool asCharacter,
            string type = null,
            string challengeRating = null)
        {
            var validCreatures = GetValidCreatures(creatureGroup, asCharacter, type, challengeRating);
            var randomCreature = collectionsSelector.SelectRandomFrom(validCreatures);

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);
            if (!templates.Contains(randomCreature))
                return (randomCreature, CreatureConstants.Templates.None);

            var template = randomCreature;
            var creaturesOfTemplate = GetCreaturesOfTemplate(template, creatureGroup, asCharacter, type, challengeRating);
            randomCreature = collectionsSelector.SelectRandomFrom(creaturesOfTemplate);

            return (randomCreature, template);
        }

        public (string CreatureName, string Template) GenerateRandomNameOfChallengeRating(string challengeRating)
        {
            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Exploding all creatures");
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Getting random valid creature");
            var randomCreature = GetRandomValidCreature(creatures, false, challengeRating: challengeRating);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Creature name: {randomCreature}");
            return randomCreature;
        }

        public Creature GenerateRandomOfType(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfType(creatureType, challengeRating);
            return Generate(randomCreature.CreatureName, randomCreature.Template);
        }

        public Creature GenerateRandomOfChallengeRating(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRating(challengeRating);
            return Generate(randomCreature.CreatureName, randomCreature.Template);
        }

        public (string CreatureName, string Template) GenerateRandomNameOfTypeAsCharacter(string creatureType, string challengeRating = null)
        {
            var compatible = creatureVerifier.VerifyCompatibility(true, type: creatureType, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(true, type: creatureType, challengeRating: challengeRating);

            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var randomCreature = GetRandomValidCreature(creatures, true, creatureType, challengeRating);
            return randomCreature;
        }

        public (string CreatureName, string Template) GenerateRandomNameOfChallengeRatingAsCharacter(string challengeRating)
        {
            var compatible = creatureVerifier.VerifyCompatibility(true, challengeRating: challengeRating);
            if (!compatible)
                throw new InvalidCreatureException(true, challengeRating: challengeRating);

            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var randomCreature = GetRandomValidCreature(creatures, true, challengeRating: challengeRating);
            return randomCreature;
        }

        public Creature GenerateRandomOfTypeAsCharacter(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTypeAsCharacter(creatureType, challengeRating);
            return GenerateAsCharacter(randomCreature.CreatureName, randomCreature.Template);
        }

        public Creature GenerateRandomOfChallengeRatingAsCharacter(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRatingAsCharacter(challengeRating);
            return GenerateAsCharacter(randomCreature.CreatureName, randomCreature.Template);
        }

        private Creature Generate(string creatureName, string template, bool asCharacter)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creature: creatureName, template: template);
            if (!compatible)
                throw new InvalidCreatureException(asCharacter, creature: creatureName, template: template);

            var creature = GeneratePrototype(creatureName, asCharacter);


            Console.WriteLine($"[{DateTime.Now:O}] Applying template '{template}' to '{creatureName}'");
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generation complete: {creature.Summary}");
            return creature;
        }

        private Creature GeneratePrototype(string creatureName, bool asCharacter)
        {
            var creature = new Creature();
            creature.Name = creatureName;

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Selecting creature data for '{creatureName}'");
            var creatureData = creatureDataSelector.SelectFor(creatureName);
            creature.Size = creatureData.Size;
            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;
            creature.CanUseEquipment = creatureData.CanUseEquipment;
            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.LevelAdjustment = creatureData.LevelAdjustment;
            creature.CasterLevel = creatureData.CasterLevel;
            creature.NumberOfHands = creatureData.NumberOfHands;

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Getting creature type for '{creatureName}'");
            creature.Type = GetCreatureType(creatureName);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating abilities for '{creatureName}'");
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName);

            if (advancementSelector.IsAdvanced(creatureName))
            {
                Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Setting advancement for '{creatureName}'");
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

                Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating hit points for '{creatureName}'");
                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    creatureName,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    advancement.AdditionalHitDice, asCharacter);
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating hit points for '{creatureName}'");
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

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating alignment for '{creatureName}'");
            creature.Alignment = alignmentGenerator.Generate(creatureName);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating skills for '{creatureName}'");
            creature.Skills = skillsGenerator.GenerateFor(creature.HitPoints, creatureName, creature.Type, creature.Abilities, creature.CanUseEquipment, creature.Size);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating languages for '{creatureName}'");
            creature.Languages = languageGenerator.GenerateWith(creatureName, creature.Abilities, creature.Skills);


            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating special qualities for '{creatureName}'");
            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(
                creatureName,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);


            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating base attack bonus for '{creatureName}'");
            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creature.Type, creature.HitPoints);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating attacks for '{creatureName}'");
            creature.Attacks = attacksGenerator.GenerateAttacks(
                creatureName,
                creatureData.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);


            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating feats for '{creatureName}'");
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


            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Applying skill bonuses from feats for '{creatureName}'");
            creature.Skills = skillsGenerator.ApplyBonusesFromFeats(creature.Skills, creature.Feats, creature.Abilities);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Regenerating hit points for '{creatureName}'");
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.Feats);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating grapple bonus for '{creatureName}'");
            creature.GrappleBonus = attacksGenerator.GenerateGrappleBonus(
                creatureName,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities[AbilityConstants.Strength]);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Applying attack bonuses for '{creatureName}'");
            creature.Attacks = attacksGenerator.ApplyAttackBonuses(creature.Attacks, allFeats, creature.Abilities);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Adding additional attacks for '{creatureName}'");
            creature.Attacks = equipmentGenerator.AddAttacks(allFeats, creature.Attacks, creature.NumberOfHands);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating equipment for '{creatureName}'");
            creature.Equipment = equipmentGenerator.Generate(
                creature.Name,
                creature.CanUseEquipment,
                allFeats,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Attacks,
                creature.Abilities,
                creature.Size);


            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Setting max ability bonuses for '{creatureName}'");
            creature.Abilities = abilitiesGenerator.SetMaxBonuses(creature.Abilities, creature.Equipment);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Setting armor check penalties for '{creatureName}'");
            creature.Skills = skillsGenerator.SetArmorCheckPenalties(creature.Name, creature.Skills, creature.Equipment);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Setting initiave bonus for '{creatureName}'");
            creature.InitiativeBonus = ComputeInitiativeBonus(creature.Feats);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating speeds for '{creatureName}'");
            creature.Speeds = speedsGenerator.Generate(creature.Name);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating armor class for '{creatureName}'");
            creature.ArmorClass = armorClassGenerator.GenerateWith(
                creature.Abilities,
                creature.Size,
                creatureName,
                creature.Type,
                allFeats,
                creatureData.NaturalArmor,
                creature.Equipment);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating saving throws for '{creatureName}'");
            creature.Saves = savesGenerator.GenerateWith(creature.Name, creature.Type, creature.HitPoints, allFeats, creature.Abilities);

            Console.WriteLine($"[{DateTime.Now:O}] CreatureGenerator: Generating magic for '{creatureName}'");
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

        public async Task<Creature> GenerateAsync(string creatureName, string template) => await GenerateAsync(creatureName, template, false);

        public async Task<Creature> GenerateAsCharacterAsync(string creatureName, string template) => await GenerateAsync(creatureName, template, true);

        public async Task<Creature> GenerateRandomOfTemplateAsync(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplate(template, challengeRating);
            return await GenerateAsync(randomCreature, template);
        }

        public async Task<Creature> GenerateRandomOfTemplateAsCharacterAsync(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplateAsCharacter(template, challengeRating);
            return await GenerateAsync(randomCreature, template, true);
        }

        public async Task<Creature> GenerateRandomOfTypeAsync(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfType(creatureType, challengeRating);
            return await GenerateAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfTypeAsCharacterAsync(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTypeAsCharacter(creatureType, challengeRating);
            return await GenerateAsCharacterAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfChallengeRatingAsync(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRating(challengeRating);
            return await GenerateAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfChallengeRatingAsCharacterAsync(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRatingAsCharacter(challengeRating);
            return await GenerateAsCharacterAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        private async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter)
        {
            var compatible = creatureVerifier.VerifyCompatibility(asCharacter, creature: creatureName, template: template);
            if (!compatible)
                throw new InvalidCreatureException(asCharacter, creature: creatureName, template: template);

            var creature = GeneratePrototype(creatureName, asCharacter);

            Console.WriteLine($"[{DateTime.Now:O}] Applying template '{template}' to '{creatureName}'");
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = await templateApplicator.ApplyToAsync(creature);

            Console.WriteLine($"[{DateTime.Now:O}] Generation complete: {creature.Summary}");
            return creature;
        }
    }
}