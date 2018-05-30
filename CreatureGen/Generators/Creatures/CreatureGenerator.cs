using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using CreatureGen.Templates;
using CreatureGen.Verifiers;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Creatures
{
    internal class CreatureGenerator : ICreatureGenerator
    {
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ICreatureVerifier creatureVerifier;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAbilitiesGenerator abilitiesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly IArmorClassGenerator armorClassGenerator;
        private readonly IAttackSelector attackSelector;
        private readonly ISavesGenerator savesGenerator;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly Dice dice;
        private readonly JustInTimeFactory justInTimeFactory;
        private readonly IAdvancementSelector advancementSelector;

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            ICreatureVerifier creatureVerifier,
            ICollectionSelector collectionsSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            IArmorClassGenerator armorClassGenerator,
            IAttackSelector attackSelector,
            ISavesGenerator savesGenerator,
            ITypeAndAmountSelector typeAndAmountSelector,
            Dice dice,
            JustInTimeFactory justInTimeFactory,
            IAdvancementSelector advancementSelector)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
            this.creatureVerifier = creatureVerifier;
            this.collectionsSelector = collectionsSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.armorClassGenerator = armorClassGenerator;
            this.attackSelector = attackSelector;
            this.savesGenerator = savesGenerator;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.dice = dice;
            this.justInTimeFactory = justInTimeFactory;
            this.advancementSelector = advancementSelector;
        }

        public Creature Generate(string creatureName, string template)
        {
            var compatible = creatureVerifier.VerifyCompatibility(creatureName, template);
            if (!compatible)
                throw new IncompatibleCreatureAndTemplateException(creatureName, template);

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

            creature.HitPoints = hitPointsGenerator.GenerateFor(creatureName, creature.Type, creature.Abilities[AbilityConstants.Constitution]);

            var naturalArmorAdvancementAmount = 0;

            if (advancementSelector.IsAdvanced(creatureName))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName);
                creature.HitPoints.HitDiceQuantity += advancement.AdditionalHitDice;

                if (IsBarghest(creature))
                {
                    creature.Abilities[AbilityConstants.Strength].BaseScore += advancement.AdditionalHitDice;
                    creature.Abilities[AbilityConstants.Constitution].BaseScore += advancement.AdditionalHitDice;
                    naturalArmorAdvancementAmount = advancement.AdditionalHitDice;
                }

                creature.HitPoints.RollDefault(dice);
                creature.HitPoints.Roll(dice);

                creature.Size = advancement.Size;
                creature.Space.Value = advancement.Space;
                creature.Reach.Value = advancement.Reach;
            }

            if (IsBarghest(creature))
            {
                creature.CasterLevel = creature.HitPoints.HitDiceQuantity;
            }

            creature.Skills = skillsGenerator.GenerateFor(creature.HitPoints, creatureName, creature.Type, creature.Abilities);
            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(creatureName, creature.HitPoints, creature.Size, creature.Abilities, creature.Skills);
            creature.Attacks = attackSelector.Select(creatureName);
            creature.BaseAttackBonus = ComputeBaseAttackBonus(creature.Type, creature.HitPoints);

            var naturalArmor = creatureData.NaturalArmor + naturalArmorAdvancementAmount;
            creature.Feats = featsGenerator.GenerateFeats(
                creature.HitPoints,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.Skills,
                creature.Attacks,
                creature.SpecialQualities,
                creature.CasterLevel,
                creature.Speeds,
                naturalArmor,
                creature.NumberOfHands,
                creature.Size);

            creature.Skills = skillsGenerator.ApplyBonusesFromFeats(creature.Skills, creature.Feats);
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.Feats);

            creature.GrappleBonus = ComputeGrappleBonus(creature.Size, creature.BaseAttackBonus, creature.Abilities[AbilityConstants.Strength]);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            creature.Attacks = ComputeAttackBonuses(creature.Attacks, creature.Abilities, creature.BaseAttackBonus, allFeats);

            creature.InitiativeBonus = ComputeInitiative(creature.Abilities, creature.Feats);

            var speeds = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.Speeds, creatureName);

            foreach (var speedKvp in speeds)
            {
                var measurement = new Measurement("feet per round");
                measurement.Value = speedKvp.Amount;

                if (speedKvp.Type == SpeedConstants.Fly)
                    measurement.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, creatureName).Single();

                creature.Speeds[speedKvp.Type] = measurement;
            }

            creature.ArmorClass = armorClassGenerator.GenerateWith(creature.Abilities[AbilityConstants.Dexterity], creature.Size, creatureName, allFeats);
            creature.ArmorClass.NaturalArmorBonus += naturalArmor;

            creature.Saves = savesGenerator.GenerateWith(creature.Type, creature.HitPoints, allFeats, creature.Abilities);

            creature.Alignment = alignmentGenerator.Generate(creatureName);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature);

            return creature;
        }

        private bool IsBarghest(Creature creature)
        {
            return creature.Name == CreatureConstants.Barghest || creature.Name == CreatureConstants.Barghest_Greater;
        }

        private IEnumerable<Attack> ComputeAttackBonuses(IEnumerable<Attack> attacks, Dictionary<string, Ability> abilities, int baseAttackBonus, IEnumerable<Feat> feats)
        {
            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                var ability = GetAbilityForAttack(abilities, attack);
                attack.TotalAttackBonus = ability.Modifier + baseAttackBonus;

                if (!attack.IsPrimary && attack.IsNatural && feats.Any(f => f.Name == FeatConstants.Monster.Multiattack))
                    attack.TotalAttackBonus -= 2;
                else if (!attack.IsPrimary)
                    attack.TotalAttackBonus -= 5;
            }

            return attacks;
        }

        private Ability GetAbilityForAttack(Dictionary<string, Ability> abilities, Attack attack)
        {
            if (!attack.IsMelee || !abilities[AbilityConstants.Strength].HasScore)
                return abilities[AbilityConstants.Dexterity];

            return abilities[AbilityConstants.Strength];
        }

        private int? ComputeGrappleBonus(string size, int baseAttackBonus, Ability strength)
        {
            if (!strength.HasScore)
                return null;

            var sizeModifier = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, size);
            return baseAttackBonus + strength.Modifier + sizeModifier;
        }

        private int ComputeBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return 0;

            var baseAttackQuality = collectionsSelector.FindCollectionOf(TableNameConstants.Set.Collection.CreatureGroups, creatureType.Name, GroupConstants.GoodBaseAttack, GroupConstants.AverageBaseAttack, GroupConstants.PoorBaseAttack);

            switch (baseAttackQuality)
            {
                case GroupConstants.GoodBaseAttack: return GetGoodBaseAttackBonus(hitPoints.HitDiceQuantity);
                case GroupConstants.AverageBaseAttack: return GetAverageBaseAttackBonus(hitPoints.HitDiceQuantity);
                case GroupConstants.PoorBaseAttack: return GetPoorBaseAttackBonus(hitPoints.HitDiceQuantity);
                default: throw new ArgumentException($"{creatureType.Name} has no base attack");
            }
        }

        private int GetGoodBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity;
        }

        private int GetAverageBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity * 3 / 4;
        }

        private int GetPoorBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity / 2;
        }

        private int ComputeInitiative(Dictionary<string, Ability> abilities, IEnumerable<Feat> feats)
        {
            var initiativeBonus = abilities[AbilityConstants.Dexterity].Modifier;

            if (!abilities[AbilityConstants.Dexterity].HasScore)
                initiativeBonus = abilities[AbilityConstants.Intelligence].Modifier;

            var improvedInitiative = feats.FirstOrDefault(f => f.Name == FeatConstants.Initiative_Improved);
            if (improvedInitiative != null)
                initiativeBonus += improvedInitiative.Power;

            return initiativeBonus;
        }

        private CreatureType GetCreatureType(string creatureName)
        {
            var creatureType = new CreatureType();
            var types = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.CreatureTypes, creatureName);

            creatureType.Name = types.First();
            creatureType.SubTypes = types.Skip(1);

            return creatureType;
        }
    }
}