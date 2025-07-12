using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    internal class SpaceReachHelper
    {
        private readonly Dictionary<string, double> defaultSpace;
        private readonly Dictionary<string, double> defaultTallReach;
        private readonly Dictionary<string, double> defaultLongReach;
        private readonly MeasurementHelper measurementHelper;
        private readonly string[] tallOverride;

        public SpaceReachHelper(MeasurementHelper measurementHelper)
        {
            this.measurementHelper = measurementHelper;

            defaultSpace = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0.5,
                [SizeConstants.Diminutive] = 1,
                [SizeConstants.Tiny] = 2.5,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 10,
                [SizeConstants.Huge] = 15,
                [SizeConstants.Gargantuan] = 20,
                [SizeConstants.Colossal] = 30,
            };
            defaultTallReach = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0,
                [SizeConstants.Diminutive] = 0,
                [SizeConstants.Tiny] = 0,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 10,
                [SizeConstants.Huge] = 15,
                [SizeConstants.Gargantuan] = 20,
                [SizeConstants.Colossal] = 30,
            };
            defaultLongReach = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0,
                [SizeConstants.Diminutive] = 0,
                [SizeConstants.Tiny] = 0,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 5,
                [SizeConstants.Huge] = 10,
                [SizeConstants.Gargantuan] = 15,
                [SizeConstants.Colossal] = 20,
            };
            tallOverride =
            [
                //INFO: Even though Elder Black Puddings are longer than tall, because they are amorphous and can shift their form,
                //they are considered tall creatures for space and reach
                CreatureConstants.BlackPudding_Elder,
                //INFO: Even though Hell Hounds are longer than tall, because they can stand on their hind legs, they are considered tall creatures for space and reach
                CreatureConstants.HellHound,
                CreatureConstants.HellHound_NessianWarhound,
                //INFO: Even though Lillends are longer than tall, because they have serpentine bodies with humanoid torso,
                //they are considered tall creatures for space and reach
                CreatureConstants.Lillend,
                //INFO: Even though Mariliths are longer than tall, because they have serpentine bodies with humanoid torso,
                //they are considered tall creatures for space and reach
                CreatureConstants.Marilith,
                //INFO: Even though Mimics are longer than tall, because they are amorphous and can shift their form,
                //they are considered tall creatures for space and reach
                CreatureConstants.Mimic,
                //INFO: Even though Salamanders are longer than tall, because they have serpentine bodies with humanoid torso,
                //they are considered tall creatures for space and reach
                CreatureConstants.Salamander_Flamebrother,
                CreatureConstants.Salamander_Average,
                CreatureConstants.Salamander_Noble,
                //INFO: Even though Umber hulks are longer than tall, because they can stand on their hind legs, they are considered tall creatures for space and reach
                CreatureConstants.UmberHulk,
                CreatureConstants.UmberHulk_TrulyHorrid,
                //INFO: Even though Xorns are the same height and length, because they have extra arms and appendages,
                //they are considered tall creatures for space and reach
                CreatureConstants.Xorn_Minor,
                CreatureConstants.Xorn_Average,
                CreatureConstants.Xorn_Elder,
                //INFO: Even though Yuan-ti Abominations are longer than tall, because they have serpentine bodies with humanoid torso,
                //they are considered tall creatures for space and reach
                CreatureConstants.YuanTi_Abomination,
                //INFO: While Zelekhuts are based on Centaurs, and Centaurs are long, Zelekhuts count as tall due to their long arms
                CreatureConstants.Zelekhut,
            ];
        }

        public double GetDefaultReach(string creature, string size)
        {
            if (IsTall(creature))
                return defaultTallReach[size];

            return defaultLongReach[size];
        }

        public double GetDefaultSpace(string size) => defaultSpace[size];

        public double GetAdvancedReach(string creature, string originalSize, double originalReach, string advancedSize)
        {
            if (advancedSize == originalSize)
                return originalReach;

            if (IsTall(creature))
                return ComputeIncrease(originalReach, defaultTallReach[originalSize], defaultTallReach[advancedSize]);

            return ComputeIncrease(originalReach, defaultLongReach[originalSize], defaultLongReach[advancedSize]);
        }

        private bool IsTall(string creature)
        {
            return measurementHelper.IsTall(creature) || tallOverride.Contains(creature);
        }

        public double GetAdvancedSpace(string originalSize, double originalSpace, string advancedSize)
        {
            if (originalSize == advancedSize)
                return originalSpace;

            return ComputeIncrease(originalSpace, defaultSpace[originalSize], defaultSpace[advancedSize]);
        }

        private double ComputeIncrease(double original, double originalDefault, double advancedDefault)
        {
            if (original == originalDefault)
                return advancedDefault;

            var divisor = originalDefault > 0 ? originalDefault : 1;
            var multiplier = original / divisor;
            return advancedDefault * multiplier;
        }
    }
}
