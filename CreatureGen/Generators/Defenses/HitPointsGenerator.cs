using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Defenses
{
    internal class HitPointsGenerator : IHitPointsGenerator
    {
        private readonly Dice dice;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;

        public HitPointsGenerator(Dice dice, ITypeAndAmountSelector typeAndAmountSelector, IAdjustmentsSelector adjustmentSelector)
        {
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.adjustmentSelector = adjustmentSelector;
        }

        public HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution)
        {
            var hitPoints = new HitPoints();

            hitPoints.HitDiceQuantity = adjustmentSelector.SelectFrom(TableNameConstants.Set.Collection.HitDice, creatureName);
            hitPoints.HitDie = adjustmentSelector.SelectFrom(TableNameConstants.Set.Collection.HitDice, creatureType.Name);
            hitPoints.Constitution = constitution;

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }

        public HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats)
        {
            hitPoints.Bonus = feats.Where(f => f.Name == FeatConstants.Toughness).Sum(f => f.Power);

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }
    }
}