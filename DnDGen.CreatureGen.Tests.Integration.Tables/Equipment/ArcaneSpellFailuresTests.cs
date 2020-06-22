using DnDGen.CreatureGen.Tables;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Equipment
{
    [TestFixture]
    public class ArcaneSpellFailuresTests : AdjustmentsTests
    {
        protected override string tableName => TableNameConstants.Adjustments.ArcaneSpellFailures;

        [Test]
        public void ArcaneSpellFailuresContainsAllItemNames()
        {
            var armorNames = ArmorConstants.GetAllArmorsAndShields(true);
            AssertCollectionNames(armorNames);
        }

        [TestCase(ArmorConstants.AbsorbingShield, )]
        public void ArcaneSpellFailureChance(string armorName, int failureChance)
        {
            AssertAdjustment(armorName, failureChance);
        }
    }
}
