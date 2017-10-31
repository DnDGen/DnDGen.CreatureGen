using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System;
using System.Linq;

namespace CreatureGen.Generators.Defenses
{
    internal class HitPointsGenerator : IHitPointsGenerator
    {
        private readonly Dice dice;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public HitPointsGenerator(Dice dice, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public HitPoints GenerateFor(Creature creature)
        {
            var hitPoints = new HitPoints();

            var hitDice = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.HitDice, creature.Name).Single();
            hitPoints.HitDiceQuantity = Convert.ToInt32(hitDice.Type);
            hitPoints.HitDie = hitDice.Amount;
            hitPoints.Constitution = creature.Abilities[AbilityConstants.Constitution];

            var templateHitDice = typeAndAmountSelector.SelectOne(TableNameConstants.Set.Collection.HitDice, creature.Template);
            if (templateHitDice.Amount > 0)
                hitPoints.HitDie = templateHitDice.Amount;

            hitPoints.Total = RollHitPoints(hitPoints.HitDiceQuantity, hitPoints.HitDie, hitPoints.Constitution.Bonus);

            var averageTotal = dice.Roll(hitPoints.Roll).AsPotentialAverage();
            hitPoints.DefaultTotal = Convert.ToInt32(averageTotal);

            return hitPoints;
        }

        private int RollHitPoints(int quantity, int die, int constitutionBonus)
        {
            var rolls = dice.Roll(quantity).d(die).AsIndividualRolls();
            rolls = rolls.Select(r => Math.Max(r + constitutionBonus, 1));

            return rolls.Sum();
        }
    }
}