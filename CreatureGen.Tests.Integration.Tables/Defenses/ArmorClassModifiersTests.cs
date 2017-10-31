using CreatureGen.Tables;
using CreatureGen.Feats;
using NUnit.Framework;
using TreasureGen.Items.Magical;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ArmorClassModifiersTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.ArmorClassModifiers; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Deflection,
                GroupConstants.NaturalArmor,
                GroupConstants.DodgeBonus,
                GroupConstants.ArmorBonus
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Deflection,
            RingConstants.Protection)]
        [TestCase(GroupConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            FeatConstants.ArmorBonus,
            WondrousItemConstants.AmuletOfNaturalArmor)]
        [TestCase(GroupConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            FeatConstants.Dodge)]
        [TestCase(GroupConstants.ArmorBonus,
            FeatConstants.InertialArmor,
            WondrousItemConstants.BracersOfArmor)]
        public void ArmorClassModifier(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
