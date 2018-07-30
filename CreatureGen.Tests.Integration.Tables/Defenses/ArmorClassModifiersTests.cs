using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ArmorClassModifiersTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Collection.ArmorClassModifiers; }
        }

        [Test]
        public void CollectionNames()
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

        [TestCase(GroupConstants.Deflection)]
        [TestCase(GroupConstants.NaturalArmor)]
        [TestCase(GroupConstants.DodgeBonus,
            FeatConstants.SpecialQualities.DodgeBonus,
            FeatConstants.Dodge)]
        [TestCase(GroupConstants.ArmorBonus,
            FeatConstants.SpecialQualities.InertialArmor)]
        public void ArmorClassModifier(string name, params string[] collection)
        {
            base.AssertDistinctCollection(name, collection);
        }
    }
}
