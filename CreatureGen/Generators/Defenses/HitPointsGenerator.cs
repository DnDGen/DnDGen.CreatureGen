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
        private readonly IAdjustmentsSelector adjustmentSelector;

        public HitPointsGenerator(Dice dice, IAdjustmentsSelector adjustmentSelector)
        {
            this.dice = dice;
            this.adjustmentSelector = adjustmentSelector;
        }

        public HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution, string size, int additionalHitDice = 0)
        {
            var hitPoints = new HitPoints();

            hitPoints.HitDiceQuantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creatureName);
            hitPoints.HitDie = adjustmentSelector.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, creatureType.Name);
            hitPoints.Constitution = constitution;
            hitPoints.Bonus = GetBonus(creatureType, size);
            hitPoints.HitDiceQuantity += additionalHitDice;

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }

        private int GetBonus(CreatureType creatureType, string size)
        {
            if (creatureType.Name != CreatureConstants.Types.Construct)
                return 0;

            switch (size)
            {
                case SizeConstants.Colossal: return 80;
                case SizeConstants.Gargantuan: return 60;
                case SizeConstants.Huge: return 40;
                case SizeConstants.Large: return 30;
                case SizeConstants.Medium: return 20;
                case SizeConstants.Small: return 10;
                case SizeConstants.Tiny:
                case SizeConstants.Diminutive:
                case SizeConstants.Fine:
                default: return 0;
            }
        }

        public HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats)
        {
            hitPoints.Bonus += feats.Where(f => f.Name == FeatConstants.Toughness).Sum(f => f.Power);

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }
    }
}