using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class SaveBonusesTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.SaveBonuses;

        [Test]
        public void SaveBonusesNames()
        {
            var creatures = CreatureConstants.GetAll();
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(types).Union(subtypes).Union(templates);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SaveBonusesTestData), nameof(SaveBonusesTestData.Creatures))]
        [TestCaseSource(typeof(SaveBonusesTestData), nameof(SaveBonusesTestData.Types))]
        [TestCaseSource(typeof(SaveBonusesTestData), nameof(SaveBonusesTestData.Subtypes))]
        [TestCaseSource(typeof(SaveBonusesTestData), nameof(SaveBonusesTestData.Templates))]
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
