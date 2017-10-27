using CreatureGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells
{
    [TestFixture]
    public class ArcaneSpellFailuresTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.ArcaneSpellFailures;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                ArmorConstants.AbsorbingShield,
                ArmorConstants.ArmorOfArrowAttraction,
                ArmorConstants.ArmorOfRage,
                ArmorConstants.BandedMail,
                ArmorConstants.BandedMailOfLuck,
                ArmorConstants.Breastplate,
                ArmorConstants.BreastplateOfCommand,
                ArmorConstants.Buckler,
                ArmorConstants.CastersShield,
                ArmorConstants.CelestialArmor,
                ArmorConstants.Chainmail,
                ArmorConstants.ChainShirt,
                ArmorConstants.DemonArmor,
                ArmorConstants.DwarvenPlate,
                ArmorConstants.ElvenChain,
                ArmorConstants.FullPlate,
                ArmorConstants.FullPlateOfSpeed,
                ArmorConstants.HalfPlate,
                ArmorConstants.HeavySteelShield,
                ArmorConstants.HeavyWoodenShield,
                ArmorConstants.HideArmor,
                ArmorConstants.LeatherArmor,
                ArmorConstants.LightSteelShield,
                ArmorConstants.LightWoodenShield,
                ArmorConstants.LionsShield,
                ArmorConstants.PaddedArmor,
                ArmorConstants.PlateArmorOfTheDeep,
                ArmorConstants.RhinoHide,
                ArmorConstants.ScaleMail,
                ArmorConstants.SpinedShield,
                ArmorConstants.SplintMail,
                ArmorConstants.StuddedLeatherArmor,
                ArmorConstants.TowerShield,
                ArmorConstants.WingedShield
            };

            AssertCollectionNames(names);
        }

        [TestCase(ArmorConstants.AbsorbingShield, 15)]
        [TestCase(ArmorConstants.ArmorOfArrowAttraction, 35)]
        [TestCase(ArmorConstants.ArmorOfRage, 35)]
        [TestCase(ArmorConstants.BandedMail, 35)]
        [TestCase(ArmorConstants.BandedMailOfLuck, 35)]
        [TestCase(ArmorConstants.Breastplate, 25)]
        [TestCase(ArmorConstants.BreastplateOfCommand, 25)]
        [TestCase(ArmorConstants.Buckler, 5)]
        [TestCase(ArmorConstants.CastersShield, 5)]
        [TestCase(ArmorConstants.CelestialArmor, 15)]
        [TestCase(ArmorConstants.Chainmail, 30)]
        [TestCase(ArmorConstants.ChainShirt, 20)]
        [TestCase(ArmorConstants.DemonArmor, 35)]
        [TestCase(ArmorConstants.DwarvenPlate, 35)]
        [TestCase(ArmorConstants.ElvenChain, 30)]
        [TestCase(ArmorConstants.FullPlate, 35)]
        [TestCase(ArmorConstants.FullPlateOfSpeed, 35)]
        [TestCase(ArmorConstants.HalfPlate, 40)]
        [TestCase(ArmorConstants.HeavySteelShield, 15)]
        [TestCase(ArmorConstants.HeavyWoodenShield, 15)]
        [TestCase(ArmorConstants.HideArmor, 20)]
        [TestCase(ArmorConstants.LeatherArmor, 10)]
        [TestCase(ArmorConstants.LightSteelShield, 5)]
        [TestCase(ArmorConstants.LightWoodenShield, 5)]
        [TestCase(ArmorConstants.LionsShield, 15)]
        [TestCase(ArmorConstants.PaddedArmor, 5)]
        [TestCase(ArmorConstants.PlateArmorOfTheDeep, 35)]
        [TestCase(ArmorConstants.RhinoHide, 20)]
        [TestCase(ArmorConstants.ScaleMail, 25)]
        [TestCase(ArmorConstants.SpinedShield, 15)]
        [TestCase(ArmorConstants.SplintMail, 40)]
        [TestCase(ArmorConstants.StuddedLeatherArmor, 15)]
        [TestCase(ArmorConstants.TowerShield, 50)]
        [TestCase(ArmorConstants.WingedShield, 15)]
        public void ArcaneSpellFailurePercentage(string name, int percentage)
        {
            base.Adjustment(name, percentage);
        }
    }
}
