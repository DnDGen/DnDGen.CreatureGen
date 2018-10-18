using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class SaveBonusesTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.SaveBonuses;

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var types = CreatureConstants.Types.All();
            var subtypes = CreatureConstants.Types.Subtypes.All();

            var names = creatures.Union(types).Union(subtypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SaveBonusesTestData), "Creatures")]
        [TestCaseSource(typeof(SaveBonusesTestData), "Types")]
        [TestCaseSource(typeof(SaveBonusesTestData), "Subtypes")]
        public void SaveBonuses(string source, Dictionary<string, int> saveAndBonus)
        {
            if (!saveAndBonus.Any())
                Assert.Fail("Test case did not specify saves bonuses or NONE");

            if (saveAndBonus.ContainsKey(SaveBonusesTestData.None))
                saveAndBonus.Clear();

            AssertTypesAndAmounts(source, saveAndBonus);
        }
    }
}
