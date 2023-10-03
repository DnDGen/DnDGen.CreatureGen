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

        public int LowerDiff => ComputeDiff(LowerWeight, Lower);
        public int UpperDiff => ComputeDiff(UpperWeight, Upper);

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
            Quantity = ComputeQuantityFloor(adjust, Die);
        }

        public void SetBase(int adjust)
        {
            Base = ComputeBase(adjust, Quantity);
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

        private int ComputeQuantityFloor(int adjust, int die)
        {
            if (die == 0)
            {
                return adjust;
            }

            if (die == 1)
            {
                return 1 + adjust;
            }

            return Math.Max(1, (int)Math.Floor((Upper - Lower) / (double)(UpperMultiplier * die - LowerMultiplier))) + adjust;
        }

        private int ComputeBase(int adjust, int quantity) => Lower - LowerMultiplier * quantity + adjust;
        private int ComputeLowerWeight(int baseWeight, int quantity) => baseWeight + LowerMultiplier * quantity;
        private int ComputeUpperWeight(int baseWeight, int quantity, int die) => baseWeight + UpperMultiplier * quantity * die;
        private int ComputeDiff(int weight, int target) => Math.Abs(weight - target);

        public bool CouldBeBetter(int die, int quantityAdjust)
        {
            if (!CouldBeValid(die, quantityAdjust))
                return false;

            var lowerQuantity = ComputeQuantityFloor(quantityAdjust, die);
            var lowerBase = ComputeBase((int)SigmaMin * -1, lowerQuantity);
            var lowerHigh = ComputeUpperWeight(lowerBase, lowerQuantity, die);

            var upperQuantity = ComputeQuantityFloor(quantityAdjust, die);
            var upperBase = ComputeBase((int)SigmaMin, upperQuantity);
            var upperHigh = ComputeUpperWeight(upperBase, upperQuantity, die);

            if (lowerHigh <= Upper && upperHigh >= Upper)
                return true;

            return ComputeDiff(lowerHigh, Upper) <= UpperDiff
                || ComputeDiff(upperHigh, Upper) <= UpperDiff;
        }

        public bool CouldBeValid(int die, int quantityAdjust = -666)
        {
            //If lowest base has upper still too high, no
            var lowerQuantity = ComputeQuantityFloor(quantityAdjust == -666 ? 0 : quantityAdjust, die);
            var lowerBase = ComputeBase((int)SigmaMin * -1, lowerQuantity);
            var lowerHigh = ComputeUpperWeight(lowerBase, lowerQuantity, die);

            if (lowerHigh > Upper + SigmaMax)
                return false;

            //If highest base has upper still too low, no
            var upperQuantity = ComputeQuantityFloor(quantityAdjust == -666 ? 1 : quantityAdjust, die);
            var upperBase = ComputeBase((int)SigmaMin, upperQuantity);
            var upperHigh = ComputeUpperWeight(upperBase, upperQuantity, die);

            if (upperHigh < Upper - SigmaMax)
                return false;

            return true;
        }
    }
}
