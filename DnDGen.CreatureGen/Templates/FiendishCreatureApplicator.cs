using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class FiendishCreatureApplicator : TemplateApplicator
    {
        private readonly IAttacksGenerator attackGenerator;
        private readonly IFeatsGenerator featGenerator;
        private readonly ICollectionSelector collectionSelector;
        private readonly IEnumerable<string> creatureTypes;
        private readonly IMagicGenerator magicGenerator;

        public FiendishCreatureApplicator(IAttacksGenerator attackGenerator, IFeatsGenerator featGenerator, ICollectionSelector collectionSelector, IMagicGenerator magicGenerator)
        {
            this.attackGenerator = attackGenerator;
            this.featGenerator = featGenerator;
            this.collectionSelector = collectionSelector;
            this.magicGenerator = magicGenerator;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
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
            UpdateCreatureTemplate(creature);

            // Creature type
            UpdateCreatureType(creature);

            // Challenge ratings
            UpdateCreatureChallengeRating(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            // Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            // Alignment
            UpdateCreatureAlignment(creature);

            // Languages
            UpdateCreatureLanguages(creature);

            //INFO: Depends on abilities
            // Attacks
            UpdateCreatureAttacks(creature);

            //INFO: Depends on abilities, alignment
            // Special Qualities
            UpdateCreatureSpecialQualities(creature);

            //INFO: Depends on abilities, alignment
            // Magic
            UpdateCreatureMagic(creature);

            return creature;
        }

        private void UpdateCreatureMagic(Creature creature)
        {
            creature.Magic = magicGenerator.GenerateWith(creature.Name, creature.Alignment, creature.Abilities, creature.Equipment);
        }

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
            {
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                creature.Type.Name,
            });

            if (creature.Type.Name == CreatureConstants.Types.Animal
                || creature.Type.Name == CreatureConstants.Types.Vermin)
            {
                creature.Type.Name = CreatureConstants.Types.MagicalBeast;
            }
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Intelligence].FullScore < 3)
                creature.Abilities[AbilityConstants.Intelligence].TemplateScore = 3;
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureLanguages(Creature creature)
        {
            if (!creature.Languages.Any())
            {
                return;
            }

            var language = collectionSelector.SelectRandomFrom(
                TableNameConstants.Collection.LanguageGroups,
                CreatureConstants.Templates.FiendishCreature + LanguageConstants.Groups.Automatic);

            creature.Languages = creature.Languages.Union(new[] { language });
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = challengeRatings.ToList().IndexOf(creature.ChallengeRating);

            if (creature.HitPoints.RoundedHitDiceQuantity >= 8)
            {
                creature.ChallengeRating = challengeRatings[index + 2];
            }
            else if (creature.HitPoints.RoundedHitDiceQuantity >= 4)
            {
                creature.ChallengeRating = challengeRatings[index + 1];
            }
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 2;
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            var attacks = attackGenerator.GenerateAttacks(
                CreatureConstants.Templates.FiendishCreature,
                creature.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var smiteEvil = attacks.First(a => a.Name == "Smite Good");
            smiteEvil.DamageRoll = Math.Min(creature.HitPoints.RoundedHitDiceQuantity, 20).ToString();

            creature.Attacks = creature.Attacks.Union(attacks);
        }

        private void UpdateCreatureSpecialQualities(Creature creature)
        {
            var specialQualities = featGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.FiendishCreature,
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

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Template = CreatureConstants.Templates.FiendishCreature;
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

            // Abilities
            var abilitiesTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilitiesTask);

            // Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            // Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
            tasks.Add(alignmentTask);

            // Languages
            var languageTask = Task.Run(() => UpdateCreatureLanguages(creature));
            tasks.Add(languageTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on abilities
            // Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            //INFO: Depends on abilities, alignment
            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature));
            tasks.Add(qualityTask);

            //INFO: Depends on abilities, alignment
            // Magic
            var magicTask = Task.Run(() => UpdateCreatureMagic(creature));
            tasks.Add(magicTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            return creature;
        }

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return false;

            if (!creatureTypes.Contains(types.First()))
                return false;

            var alignments = collectionSelector.SelectFrom(TableNameConstants.Collection.AlignmentGroups, creature);

            return alignments.Any(a => !a.Contains(AlignmentConstants.Good));
        }
    }
}
