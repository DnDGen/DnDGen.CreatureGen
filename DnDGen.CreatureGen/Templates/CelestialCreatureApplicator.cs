using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Feats;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Templates
{
    internal class CelestialCreatureApplicator : TemplateApplicator
    {
        private readonly IAttacksGenerator attackGenerator;
        private readonly IFeatsGenerator featGenerator;

        public CelestialCreatureApplicator(IAttacksGenerator attackGenerator, IFeatsGenerator featGenerator)
        {
            this.attackGenerator = attackGenerator;
            this.featGenerator = featGenerator;
        }

        public Creature ApplyTo(Creature creature)
        {
            // Creature type
            if (creature.Type.Name == CreatureConstants.Types.Animal
                || creature.Type.Name == CreatureConstants.Types.Vermin)
            {
                creature.Type.Name = CreatureConstants.Types.MagicalBeast;
            }

            if (!creature.Type.SubTypes.Contains(CreatureConstants.Types.Subtypes.Extraplanar))
            {
                creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
                {
                    CreatureConstants.Types.Subtypes.Extraplanar
                });
            }

            // Challenge ratings
            var challengeRatings = ChallengeRatingConstants.GetOrdered();

            if (creature.HitPoints.HitDiceQuantity >= 8)
            {
                var index = challengeRatings.ToList().IndexOf(creature.ChallengeRating);
                creature.ChallengeRating = challengeRatings[index + 2];
            }
            else if (creature.HitPoints.HitDiceQuantity >= 4)
            {
                var index = challengeRatings.ToList().IndexOf(creature.ChallengeRating);
                creature.ChallengeRating = challengeRatings[index + 1];
            }

            // Abilities
            while (creature.Abilities[AbilityConstants.Intelligence].FullScore < 3)
                creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment++;

            // Level Adjustment
            if (creature.LevelAdjustment.HasValue)
                creature.LevelAdjustment += 2;

            // Alignment
            creature.Alignment.Goodness = AlignmentConstants.Good;

            // Attacks
            var attacks = attackGenerator.GenerateAttacks(
                CreatureConstants.Templates.CelestialCreature,
                creature.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var smiteEvil = attacks.First(a => a.Name == "Smite Evil");
            smiteEvil.DamageRoll = Math.Min(creature.HitPoints.RoundedHitDiceQuantity, 20).ToString();

            creature.Attacks = creature.Attacks.Union(attacks);

            // Special Qualities
            var specialQualities = featGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.CelestialCreature,
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

            return creature;
        }
    }
}
