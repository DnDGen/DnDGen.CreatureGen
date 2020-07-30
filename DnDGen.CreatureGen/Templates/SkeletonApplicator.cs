using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class SkeletonApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly Dice dice;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly IEnumerable<string> creatureTypes;

        public SkeletonApplicator(
            ICollectionSelector collectionSelector,
            IAdjustmentsSelector adjustmentSelector,
            Dice dice,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISavesGenerator savesGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.dice = dice;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.savesGenerator = savesGenerator;

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
                CreatureConstants.Types.Vermin,
            };
        }

        public Creature ApplyTo(Creature creature)
        {
            //Type
            UpdateCreatureType(creature);

            //Abilities
            UpdateCreatureAbilities(creature);

            //Speed
            UpdateCreatureSpeeds(creature);

            //Challenge Rating
            UpdateCreatureChallengeRating(creature);

            //Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            //Skills
            UpdateCreatureSkills(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //Alignment
            UpdateCreatureAlignment(creature);

            //INFO: Depends on abilities
            //Hit Points
            UpdateCreatureHitPoints(creature);

            //INFO: Depends on type, hit points, abilities, skills, alignment
            //Special Qualities
            UpdateCreatureSpecialQualitiesAndFeats(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Attacks
            UpdateCreatureAttacks(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Saves
            UpdateCreatureSaves(creature);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.Name = CreatureConstants.Types.Undead;
            var invalidSubtypes = new[]
            {
                CreatureConstants.Types.Subtypes.Angel,
                CreatureConstants.Types.Subtypes.Archon,
                CreatureConstants.Types.Subtypes.Chaotic,
                CreatureConstants.Types.Subtypes.Dwarf,
                CreatureConstants.Types.Subtypes.Elf,
                CreatureConstants.Types.Subtypes.Evil,
                CreatureConstants.Types.Subtypes.Gnoll,
                CreatureConstants.Types.Subtypes.Gnome,
                CreatureConstants.Types.Subtypes.Goblinoid,
                CreatureConstants.Types.Subtypes.Good,
                CreatureConstants.Types.Subtypes.Halfling,
                CreatureConstants.Types.Subtypes.Human,
                CreatureConstants.Types.Subtypes.Lawful,
                CreatureConstants.Types.Subtypes.Orc,
                CreatureConstants.Types.Subtypes.Reptilian,
                CreatureConstants.Types.Subtypes.Shapechanger,
            };

            creature.Type.SubTypes = creature.Type.SubTypes.Except(invalidSubtypes);
        }

        private void UpdateCreatureHitPoints(Creature creature)
        {
            creature.HitPoints.HitDie = 12;
            creature.HitPoints.Roll(dice);
            creature.HitPoints.RollDefault(dice);
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment = 2;
            creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment = int.MinValue;
            creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment = int.MinValue;
            creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 10 - creature.Abilities[AbilityConstants.Wisdom].FullScore;
            creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 1 - creature.Abilities[AbilityConstants.Charisma].FullScore;
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            if (creature.Speeds.ContainsKey(SpeedConstants.Fly))
            {
                if (creature.Speeds[SpeedConstants.Fly].Description.ToLower().Contains("wings"))
                {
                    creature.Speeds.Remove(SpeedConstants.Fly);
                }
            }
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            if (creature.HitPoints.HitDiceQuantity <= 0.5)
            {
                creature.ChallengeRating = ChallengeRatingConstants.OneSixth;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 1)
            {
                creature.ChallengeRating = ChallengeRatingConstants.OneThird;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 3)
            {
                creature.ChallengeRating = ChallengeRatingConstants.One;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 5)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Two;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 7)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Three;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 9)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Four;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 11)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Five;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 14)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Six;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 17)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Seven;
            }
            else if (creature.HitPoints.HitDiceQuantity <= 20)
            {
                creature.ChallengeRating = ChallengeRatingConstants.Eight;
            }
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            creature.LevelAdjustment = null;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            creature.Skills = Enumerable.Empty<Skill>();
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creature.Type, creature.HitPoints);

            var skeletonAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Skeleton,
                SizeConstants.Medium,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            skeletonAttacks = attacksGenerator.ApplyAttackBonuses(skeletonAttacks, allFeats, creature.Abilities);
            var newClaw = skeletonAttacks.First(a => a.Name == "Claw");

            if (creature.Attacks.Any(a => a.Name == "Claw"))
            {
                var oldClaw = creature.Attacks.First(a => a.Name == "Claw");

                var oldMax = dice.Roll(oldClaw.DamageRoll).AsPotentialMaximum();
                var newMax = dice.Roll(newClaw.DamageRoll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldClaw.DamageRoll = newClaw.DamageRoll;
                }

                skeletonAttacks = skeletonAttacks.Except(new[] { newClaw });
            }
            else
            {
                newClaw.Frequency.Quantity = creature.NumberOfHands;

                if (creature.NumberOfHands == 0)
                {
                    skeletonAttacks = skeletonAttacks.Except(new[] { newClaw });
                }
            }

            creature.Attacks = creature.Attacks
                .Union(skeletonAttacks)
                .Where(a => !a.IsSpecial);
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            creature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);
            var naturalArmorBonus = 0;

            switch (creature.Size)
            {
                case SizeConstants.Colossal: naturalArmorBonus = 10; break;
                case SizeConstants.Gargantuan: naturalArmorBonus = 6; break;
                case SizeConstants.Huge: naturalArmorBonus = 3; break;
                case SizeConstants.Large:
                case SizeConstants.Medium: naturalArmorBonus = 2; break;
                case SizeConstants.Small: naturalArmorBonus = 1; break;
                default: break;
            }

            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, naturalArmorBonus);
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Lawfulness = AlignmentConstants.Neutral;
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureSaves(Creature creature)
        {
            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            creature.Saves = savesGenerator.GenerateWith(
                CreatureConstants.Templates.Skeleton,
                creature.Type,
                creature.HitPoints,
                allFeats,
                creature.Abilities);
        }

        private void UpdateCreatureSpecialQualitiesAndFeats(Creature creature)
        {
            var featNamesToKeep = new List<string>();
            featNamesToKeep.Add(FeatConstants.SpecialQualities.AttackBonus);

            var weaponProficiencies = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            featNamesToKeep.AddRange(weaponProficiencies);

            var armorProficiencies = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency);
            featNamesToKeep.AddRange(armorProficiencies);

            var skeletonQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Skeleton,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities
                .Where(sq => featNamesToKeep.Contains(sq.Name))
                .Union(skeletonQualities);

            creature.Feats = creature.Feats.Where(f => featNamesToKeep.Contains(f.Name));
        }

        public Task<Creature> ApplyToAsync(Creature creature)
        {
            throw new NotImplementedException();
        }

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (!creatureTypes.Contains(types.First()))
                return false;

            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            var hasSkeleton = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton);
            if (!hasSkeleton.Contains(creature))
                return false;

            var hitDice = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            if (hitDice > 20)
                return false;

            return true;
        }
    }
}
