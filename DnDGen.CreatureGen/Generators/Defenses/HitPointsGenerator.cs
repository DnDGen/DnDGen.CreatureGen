using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Defenses
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

        public HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution, string size, int additionalHitDice = 0, bool asCharacter = false)
        {
            var hitPoints = new HitPoints();

            var quantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creatureName);
            var die = adjustmentSelector.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, creatureType.Name);
            quantity += additionalHitDice;

            if (asCharacter && creatureType.Name == CreatureConstants.Types.Humanoid)
            {
                quantity--;
            }

            hitPoints.Constitution = constitution;
            hitPoints.Bonus = GetBonus(creatureType, size);

            if (quantity > 0)
            {
                hitPoints.HitDice.Add(new HitDice { Quantity = quantity, HitDie = die });
            }

            hitPoints.RollDefaultTotal(dice);
            hitPoints.RollTotal(dice);

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

            hitPoints.RollDefaultTotal(dice);
            hitPoints.RollTotal(dice);

            return hitPoints;
        }
    }
}