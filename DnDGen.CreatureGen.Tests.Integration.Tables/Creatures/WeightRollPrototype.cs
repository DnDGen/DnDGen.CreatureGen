using DnDGen.RollGen;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    public class WeightRollPrototype
    {
        public readonly int LowerMultiplier;
        public readonly int UpperMultiplier;
        public readonly int Lower;
        public readonly int Upper;
        public readonly double SigmaMin;
        public readonly double SigmaMax;

        public int Quantity { get; set; }
        public int Die { get; set; }
        public int Base { get; set; }

        public string Roll => Die == 0 ? $"0+{RollHelper.GetRollWithFewestDice(Lower, Upper)}"
            : Die == 1 ? $"1+{Base}"
            : $"{Quantity}d{Die}+{Base}";

        public int LowerWeight => ComputeLowerWeight(Die == 0 ? Lower : Base, Quantity);
        public int UpperWeight => ComputeUpperWeight(Die == 0 ? Upper : Base, Quantity, Die);

        public int LowerDiff => Math.Abs(LowerWeight - Lower);
        public int UpperDiff => Math.Abs(UpperWeight - Upper);

        public bool IsValid => UpperDiff <= SigmaMax && LowerDiff <= SigmaMin && LowerWeight > 0;

        public WeightRollPrototype(int lower, int upper, int lowerMultiplier, int upperMultiplier)
        {
            Lower = lower;
            Upper = upper;
            LowerMultiplier = lowerMultiplier;
            UpperMultiplier = upperMultiplier;

            SigmaMin = new[] { 1d, (Upper + Lower) * 0.025, LowerMultiplier / 2d }.Max();
            SigmaMax = new[] { 1d, (Upper + Lower) * 0.025, UpperMultiplier / 2d }.Max();
        }

        public void SetQuantityFloor(int adjust)
        {
            Quantity = ComputeQuantityFloor(adjust);
        }

        public void SetBase(int adjust)
        {
            Base = ComputeBase(adjust);
        }

        public bool IsPerfectMatch() => LowerDiff == 0 && UpperDiff == 0;

        public override string ToString() => Roll;

        public WeightRollPrototype GetBest(int die, int quantityAdjust, int baseAdjust)
        {
            if (IsPerfectMatch())
                return this;

            var candidate = new WeightRollPrototype(Lower, Upper, LowerMultiplier, UpperMultiplier);
            candidate.Die = die;

            candidate.SetQuantityFloor(quantityAdjust);
            candidate.SetBase(baseAdjust);

            if (candidate.IsPerfectMatch())
                return candidate;

            if (candidate.UpperDiff < UpperDiff && (candidate.IsValid || candidate.LowerDiff < LowerDiff))
                return candidate;

            if (candidate.UpperDiff == UpperDiff && candidate.LowerDiff < LowerDiff)
                return candidate;

            return this;
        }

        public int ComputeQuantityFloor(int adjust)
        {
            if (Die == 0)
            {
                return adjust;
            }

            if (Die == 1)
            {
                return 1 + adjust;
            }

            return Math.Max(1, (int)Math.Floor((Upper - Lower) / (double)(UpperMultiplier * Die - LowerMultiplier))) + adjust;
        }
        private int ComputeBase(int adjust) => Lower - LowerMultiplier * Quantity + adjust;
        private int ComputeLowerWeight(int baseWeight, int quantity) => baseWeight + LowerMultiplier * quantity;
        private int ComputeUpperWeight(int baseWeight, int quantity, int die) => baseWeight + UpperMultiplier * quantity * die;

        public bool CouldBeValid(int die)
        {
            //If lowest base has upper still too high, no
            var lowerBase = ComputeBase((int)SigmaMin * -1);
            var lowerQuantity = ComputeQuantityFloor(0);
            var lowerHigh = ComputeUpperWeight(lowerBase, lowerQuantity, die);

            if (lowerHigh > Upper + SigmaMax)
                return false;

            //If highest base has upper still too low, no
            var upperBase = ComputeBase((int)SigmaMin);
            var upperQuantity = ComputeQuantityFloor(1);
            var upperHigh = ComputeUpperWeight(upperBase, upperQuantity, die);

            if (upperHigh < Upper - SigmaMax)
                return false;

            return true;
        }

        public bool CouldBeValid(int die, int quantityAdjust)
        {
            //If lowest base has upper still too high, no
            var lowerBase = ComputeBase((int)SigmaMin * -1);
            var lowerQuantity = ComputeQuantityFloor(quantityAdjust);
            var lowerHigh = ComputeUpperWeight(lowerBase, lowerQuantity, die);

            if (lowerHigh > Upper + SigmaMax)
                return false;

            //If highest base has upper still too low, no
            var upperBase = ComputeBase((int)SigmaMin);
            var upperQuantity = ComputeQuantityFloor(quantityAdjust);
            var upperHigh = ComputeUpperWeight(upperBase, upperQuantity, die);

            if (upperHigh < Upper - SigmaMax)
                return false;

            return true;
        }
    }
}
