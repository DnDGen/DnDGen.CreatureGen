using CreatureGen.Abilities;
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
using CreatureGen.Verifiers;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
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
            ISavesGenerator savesGenerator)
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

            //INFO: Creature Type must be computed before Hit Points
            creature.Type = GetCreatureType(creatureName);

            //INFO: Abilities must be computed before Hit Points
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName);

            //INFO: Hit Points must be done before Skills
            creature.HitPoints = hitPointsGenerator.GenerateFor(creature);

            //INFO: Skills must be computed before Feats or Special Qualities
            creature.Skills = skillsGenerator.GenerateFor(creature);

            //INFO: Special Qualities must be computed before Feats
            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(creatureName, creature.HitPoints, creature.Size, creature.Abilities, creature.Skills);

            //INFO: Attacks must be done before Feats
            var attacks = attackSelector.Select(creatureName);
            creature.MeleeAttack = attacks.First(a => a.IsPrimary && a.IsMelee && !a.IsSpecial);
            creature.RangedAttack = attacks.First(a => a.IsPrimary && !a.IsMelee && !a.IsSpecial);
            creature.FullMeleeAttack = attacks.Where(a => a.IsMelee && !a.IsSpecial);
            creature.FullRangedAttack = attacks.Where(a => !a.IsMelee && !a.IsSpecial);
            creature.SpecialAttacks = attacks.Where(a => a.IsSpecial);

            //INFO: Feats must be computed before Initiative Bonus and Attack Bonuses
            creature.Feats = featsGenerator.GenerateFeats(creature);

            //InFO: Base Attack bonus must be computed before Attack Bonuses
            creature.BaseAttackBonus = ComputeBaseAttackBonus(creatureName, creature.HitPoints);
            creature.GrappleBonus = ComputeGrappleBonus(creature);

            ComputeAttackBonuses(creature);

            creature.InitiativeBonus = ComputeInitiative(creature.Abilities[AbilityConstants.Dexterity], creature.Feats);

            creature.LandSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, creatureName);
            creature.AerialSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, creatureName);
            creature.AerialSpeed.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, creatureName).Single();
            creature.SwimSpeed.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SwimSpeeds, creatureName);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            creature.ArmorClass = armorClassGenerator.GenerateWith(creature.Abilities[AbilityConstants.Dexterity], creature.Size, creatureName, allFeats);

            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;

            creature.Saves = savesGenerator.GenerateWith(creatureName, allFeats, creature.Abilities);

            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.Alignment = alignmentGenerator.Generate(creatureName);
            creature.LevelAdjustment = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, creatureName);

            //TODO: Advancement

            //TODO: Apply template

            return creature;
        }

        private void ComputeAttackBonuses(Creature creature)
        {
            foreach (var attack in creature.Attacks)
            {
                if (attack.IsSpecial)
                    continue;

                var ability = attack.IsMelee ? creature.Abilities[AbilityConstants.Strength] : creature.Abilities[AbilityConstants.Dexterity];
                attack.TotalAttackBonus = ability.Bonus + creature.BaseAttackBonus;

                if (!attack.IsPrimary)
                    attack.TotalAttackBonus -= 5;

                if (!attack.IsPrimary && attack.IsNatural && creature.Feats.Any(f => f.Name == FeatConstants.MultiAttack))
                    attack.TotalAttackBonus += 3;
            }
        }

        private int ComputeGrappleBonus(Creature creature)
        {
            var sizeModifier = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, creature.Size);
            return creature.BaseAttackBonus + creature.Abilities[AbilityConstants.Strength].Bonus + sizeModifier;
        }

        private int ComputeBaseAttackBonus(string creatureName, HitPoints hitPoints)
        {
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

            var pointer = creatureType;
            foreach (var type in types)
            {
                if (pointer == null)
                    pointer = new CreatureType();

                pointer.Name = type;
                pointer = creatureType.SubType;
            }

            return creatureType;
        }
    }
}