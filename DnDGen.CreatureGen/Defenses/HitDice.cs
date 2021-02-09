using System;

namespace DnDGen.CreatureGen.Defenses
{
    public class HitDice
    {
        public int HitDie { get; set; }
        public double Quantity { get; set; }

        public int RoundedQuantity
        {
            get
            {
                if (Quantity == 0)
                    return 0;

                if (Quantity < 1)
                    return 1;

                var rawRoundedHitDiceQuantity = Math.Floor(Quantity);
                var roundedHitDiceQuantity = Convert.ToInt32(rawRoundedHitDiceQuantity);

                return roundedHitDiceQuantity;
            }
        }

        public string DefaultRoll
        {
            get
            {
                if (HitDie == 0)
                {
                    return 0.ToString();
                }

                var roll = $"{RoundedQuantity}d{HitDie}";

                if (Divisor > 1)
                {
                    roll += $"/{Divisor}";
                }

                return roll;
            }
        }

        public int Divisor
        {
            get
            {
                if (Quantity == 0)
                    return 1;

                return Math.Max(Convert.ToInt32(1 / Quantity), 1);
            }
        }
    }
}
