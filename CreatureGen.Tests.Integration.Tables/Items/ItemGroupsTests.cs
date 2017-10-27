using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Items
{
    [TestFixture]
    public class ItemGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.ItemGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.TowerShieldProficiency,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.HeavyArmorProficiency,
            ArmorConstants.SplintMail,
            ArmorConstants.BandedMail,
            ArmorConstants.HalfPlate,
            ArmorConstants.FullPlate)]
        [TestCase(FeatConstants.LightArmorProficiency,
            ArmorConstants.PaddedArmor,
            ArmorConstants.LeatherArmor,
            ArmorConstants.StuddedLeatherArmor,
            ArmorConstants.ChainShirt,
            ArmorConstants.ElvenChain,
            ArmorConstants.CelestialArmor)]
        [TestCase(FeatConstants.MediumArmorProficiency,
            ArmorConstants.HideArmor,
            ArmorConstants.ScaleMail,
            ArmorConstants.Chainmail,
            ArmorConstants.Breastplate,
            ArmorConstants.FullPlateOfSpeed)]
        [TestCase(FeatConstants.ShieldProficiency,
            ArmorConstants.Buckler,
            ArmorConstants.HeavySteelShield,
            ArmorConstants.HeavyWoodenShield,
            ArmorConstants.LightSteelShield,
            ArmorConstants.LightWoodenShield)]
        [TestCase(FeatConstants.TowerShieldProficiency, ArmorConstants.TowerShield)]
        public void ItemGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
