using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System;
using System.Collections.Generic;
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

        public HitPoints GenerateFor(string creatureName, Ability constitution)
        {
            var hitPoints = new HitPoints();

            var hitDice = typeAndAmountSelector.SelectOne(TableNameConstants.Set.Collection.HitDice, creatureName);
            hitPoints.HitDiceQuantity = Convert.ToInt32(hitDice.Type);
            hitPoints.HitDie = hitDice.Amount;
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