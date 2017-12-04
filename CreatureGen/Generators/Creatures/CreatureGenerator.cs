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
using DnDGen.Core.Selectors.Percentiles;
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
        private readonly IPercentileSelector percentileSelector;
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

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            ICreatureVerifier creatureVerifier,
            IPercentileSelector percentileSelector,
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
            JustInTimeFactory justInTimeFactory)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
            this.creatureVerifier = creatureVerifier;
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.armorClassGenerator = armorClassGenerator;
            this.attackSelector = attackSelector;
            this.savesGenerator = savesGenerator;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.dice = dice;
            this.justInTimeFactory = justInTimeFactory;
        }

        public Creature Generate(string creatureName, string template)
        {
            var compatible = creatureVerifier.VerifyCompatibility(creatureName, template);
            if (!compatible)
                throw new IncompatibleCreatureAndTemplateException();

            var creature = new Creature();
            creature.Name = creatureName;

            var creatureData = creatureDataSelector.SelectFor(creatureName);
            creature.Size = creatureData.Size;
            creature.Type = GetCreatureType(creatureName);
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName);

            creature.HitPoints = hitPointsGenerator.GenerateFor(creatureName, creature.Abilities[AbilityConstants.Constitution]);

            var advancements = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.Advancements, creatureName);
            if (percentileSelector.SelectFrom(.1) && advancements.Any())
            {
                var advancement = collectionsSelector.SelectRandomFrom(advancements);

                creature.HitPoints.HitDiceQuantity = advancement.Amount;

                creature.HitPoints.RollDefault(dice);
                creature.HitPoints.Roll(dice);

                creature.Size = advancement.Type;
            }

            creature.Skills = skillsGenerator.GenerateFor(creature.HitPoints, creatureName, creature.Abilities);
            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(creatureName, creature.HitPoints, creature.Size, creature.Abilities, creature.Skills);
            creature.Attacks = attackSelector.Select(creatureName);
            creature.BaseAttackBonus = ComputeBaseAttackBonus(creatureName, creature.HitPoints);
            creature.Feats = featsGenerator.GenerateFeats(creature.HitPoints, creature.BaseAttackBonus, creature.Abilities, creature.Skills, creature.Attacks, creature.SpecialQualities);

            creature.Skills = skillsGenerator.ApplyBonusesFromFeats(creature.Skills, creature.Feats);
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.Feats);

            creature.GrappleBonus = ComputeGrappleBonus(creature.Size, creature.BaseAttackBonus, creature.Abilities[AbilityConstants.Strength]);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            ComputeAttackBonuses(creature.Attacks, creature.Abilities[AbilityConstants.Strength], creature.Abilities[AbilityConstants.Dexterity], creature.BaseAttackBonus, allFeats);

            creature.InitiativeBonus = ComputeInitiative(creature.Abilities[AbilityConstants.Dexterity], creature.Feats);
            creature.LandSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, creatureName);
            creature.AerialSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, creatureName);
            creature.AerialSpeed.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, creatureName).Single();
            creature.SwimSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SwimSpeeds, creatureName);

            creature.ArmorClass = armorClassGenerator.GenerateWith(creature.Abilities[AbilityConstants.Dexterity], creature.Size, creatureName, allFeats);

            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;

            creature.Saves = savesGenerator.GenerateWith(creatureName, creature.HitPoints, allFeats, creature.Abilities);

            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.Alignment = alignmentGenerator.Generate(creatureName);
            creature.LevelAdjustment = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, creatureName);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature);

            return creature;
        }

        private void ComputeAttackBonuses(IEnumerable<Attack> attacks, Ability strength, Ability dexterity, int baseAttackBonus, IEnumerable<Feat> feats)
        {
            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                var ability = attack.IsMelee ? strength : dexterity;
                attack.TotalAttackBonus = ability.Bonus + baseAttackBonus;

                if (!attack.IsPrimary && attack.IsNatural && feats.Any(f => f.Name == FeatConstants.MultiAttack))
                    attack.TotalAttackBonus -= 2;
                else if (!attack.IsPrimary)
                    attack.TotalAttackBonus -= 5;
            }
        }

        private int ComputeGrappleBonus(string size, int baseAttackBonus, Ability strength)
        {
            var sizeModifier = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, size);
            return baseAttackBonus + strength.Bonus + sizeModifier;
        }

        private int ComputeBaseAttackBonus(string creatureName, HitPoints hitPoints)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return 0;

            var baseAttackQuality = collectionsSelector.FindCollectionOf(TableNameConstants.Set.Collection.CreatureGroups, creatureName, GroupConstants.GoodBaseAttack, GroupConstants.AverageBaseAttack, GroupConstants.PoorBaseAttack);

            switch (baseAttackQuality)
            {
                case GroupConstants.GoodBaseAttack: return GetGoodBaseAttackBonus(hitPoints.HitDiceQuantity);
                case GroupConstants.AverageBaseAttack: return GetAverageBaseAttackBonus(hitPoints.HitDiceQuantity);
                case GroupConstants.PoorBaseAttack: return GetPoorBaseAttackBonus(hitPoints.HitDiceQuantity);
                default: throw new ArgumentException($"{creatureName} has no base attack");
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

        private int ComputeInitiative(Ability ability, IEnumerable<Feat> feats)
        {
            var initiativeBonus = ability.Bonus;

            var improvedInitiative = feats.FirstOrDefault(f => f.Name == FeatConstants.ImprovedInitiative);
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