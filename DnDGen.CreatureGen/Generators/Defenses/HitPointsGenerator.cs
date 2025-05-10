using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Defenses
{
    internal class HitPointsGenerator : IHitPointsGenerator
    {
        private readonly Dice dice;

        public HitPointsGenerator(Dice dice)
        {
            this.dice = dice;
        }

        public HitPoints GenerateFor(double quantity, int die, CreatureType creatureType, Ability constitution, string size, int additionalHitDice = 0)
        {
            var hitPoints = new HitPoints();
            quantity += additionalHitDice;

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

            return size switch
            {
                SizeConstants.Colossal => 80,
                SizeConstants.Gargantuan => 60,
                SizeConstants.Huge => 40,
                SizeConstants.Large => 30,
                SizeConstants.Medium => 20,
                SizeConstants.Small => 10,
                _ => 0,
            };
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