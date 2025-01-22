using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    internal class AgeConstantsTests
    {
        [TestCase(AgeConstants.Categories.Adulthood, "Adulthood")]
        [TestCase(AgeConstants.Categories.MiddleAge, "Middle Age")]
        [TestCase(AgeConstants.Categories.Old, "Old")]
        [TestCase(AgeConstants.Categories.Venerable, "Venerable")]
        [TestCase(AgeConstants.Categories.Maximum, "Maximum")]
        [TestCase(AgeConstants.Categories.Swarm, "Swarm")]
        [TestCase(AgeConstants.Categories.Undead, "Undead")]
        [TestCase(AgeConstants.Categories.Construct, "Construct")]
        [TestCase(AgeConstants.Categories.Multiplier, "Multiplier")]
        [TestCase(AgeConstants.Categories.Arrowhawk.Juvenile, "Juvenile (Arrowhawk)")]
        [TestCase(AgeConstants.Categories.Arrowhawk.Adult, "Adult (Arrowhawk)")]
        [TestCase(AgeConstants.Categories.Arrowhawk.Elder, "Elder (Arrowhawk)")]
        [TestCase(AgeConstants.Categories.Dragon.Wyrmling, "Wyrmling (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.VeryYoung, "Very Young (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Young, "Young (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Juvenile, "Juvenile (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.YoungAdult, "Young Adult (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Adult, "Adult (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.MatureAdult, "Mature Adult (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Old, "Old (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.VeryOld, "Very Old (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Ancient, "Ancient (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.Wyrm, "Wyrm (Dragon)")]
        [TestCase(AgeConstants.Categories.Dragon.GreatWyrm, "Great Wyrm (Dragon)")]
        [TestCase(AgeConstants.Categories.Salamander.Flamebrother, "Flamebrother (Salamander)")]
        [TestCase(AgeConstants.Categories.Salamander.Average, "Average (Salamander)")]
        [TestCase(AgeConstants.Categories.Salamander.Noble, "Noble (Salamander)")]
        [TestCase(AgeConstants.Categories.Tojanida.Juvenile, "Juvenile (Tojanida)")]
        [TestCase(AgeConstants.Categories.Tojanida.Adult, "Adult (Tojanida)")]
        [TestCase(AgeConstants.Categories.Tojanida.Elder, "Elder (Tojanida)")]
        [TestCase(AgeConstants.Categories.Xorn.Minor, "Minor (Xorn)")]
        [TestCase(AgeConstants.Categories.Xorn.Average, "Average (Xorn)")]
        [TestCase(AgeConstants.Categories.Xorn.Elder, "Elder (Xorn)")]
        [TestCase(AgeConstants.Categories.BlackPudding.Elder, "Elder (Black Pudding)")]
        public void AgeConstant_Text(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(AgeConstants.Ageless, -1)]
        public void AgeConstant_Numeric(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
