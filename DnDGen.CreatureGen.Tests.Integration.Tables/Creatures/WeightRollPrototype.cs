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

            var quantity = ComputeQuantityFloor(quantityAdjust, die);

            var lowerBase = ComputeBase((int)SigmaMin * -1, quantity);
            var lowerHigh = ComputeUpperWeight(lowerBase, quantity, die);

            var upperBase = ComputeBase((int)SigmaMin, quantity);
            var upperHigh = ComputeUpperWeight(upperBase, quantity, die);

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

        public int[] GetPotentialBaseAdjustments(int die, int quantityAdjust)
        {
            if (die == 1)
            {
                return Enumerable.Range((int)SigmaMin * -1, (int)SigmaMin * 2 + 1).ToArray();
            }

            var quantity = ComputeQuantityFloor(quantityAdjust, die);

            var lowerBase = ComputeBase((int)SigmaMin * -1, quantity);
            var lowerHigh = ComputeUpperWeight(lowerBase, quantity, die);

            //If the lowest High is still above the Upper, then the lowest high is the closest we will get
            if (lowerHigh > Upper)
                return new[] { (int)Math.Floor(SigmaMin) * -1, (int)Math.Ceiling(SigmaMin) * -1 };

            var upperBase = ComputeBase((int)SigmaMin, quantity);
            var upperHigh = ComputeUpperWeight(upperBase, quantity, die);

            //If the highest High is still below the Upper, then the highest high is the closest we will get
            if (upperHigh < Upper)
                return new[] { (int)Math.Floor(SigmaMin), (int)Math.Ceiling(SigmaMin) };

            //This means that, potentially, somewhere in the middle we might actually have a Diff = 0
            var midBase = ComputeBase(0, quantity);
            var midUpper = ComputeUpperWeight(midBase, quantity, die);
            var midAdjust = Upper - midUpper;

            if (Math.Abs(midAdjust) > SigmaMin)
                return new int[0];

            return new[] { midAdjust };
        }

        public static WeightRollPrototype GetBest(int lower, int upper, int lowerMultiplier, int upperMultiplier)
        {
            var dice = new[] { 100, 20, 12, 10, 8, 6, 4, 3, 2 };

            var bestPrototype = new WeightRollPrototype(lower, upper, lowerMultiplier, upperMultiplier);
            bestPrototype.Die = dice[0];
            bestPrototype.SetQuantityFloor(0);
            bestPrototype.SetBase(0);

            if (bestPrototype.IsPerfectMatch())
                return bestPrototype;

            foreach (var die in dice)
            {
                if (!bestPrototype.CouldBeValid(die))
                    continue;

                for (var quantityAdjust = 0; quantityAdjust <= 1; quantityAdjust++)
                {
                    if (!bestPrototype.CouldBeBetter(die, quantityAdjust))
                        continue;

                    var baseAdjustments = bestPrototype.GetPotentialBaseAdjustments(die, quantityAdjust);
                    foreach (var baseAdjust in baseAdjustments)
                    {
                        bestPrototype = bestPrototype.GetBest(die, quantityAdjust, baseAdjust);

                        if (bestPrototype.IsPerfectMatch())
                            return bestPrototype;
                    }
                }
            }

            if (bestPrototype.IsValid)
                return bestPrototype;

            if (bestPrototype.CouldBeBetter(1, 0))
            {
                var baseAdjustments = bestPrototype.GetPotentialBaseAdjustments(1, 0);
                foreach (var baseAdjust in baseAdjustments)
                {
                    bestPrototype = bestPrototype.GetBest(1, 0, baseAdjust);

                    if (bestPrototype.IsPerfectMatch())
                        return bestPrototype;
                }
            }

            if (bestPrototype.IsValid)
                return bestPrototype;

            return bestPrototype.GetBest(0, 0, 0);
        }
    }
}
