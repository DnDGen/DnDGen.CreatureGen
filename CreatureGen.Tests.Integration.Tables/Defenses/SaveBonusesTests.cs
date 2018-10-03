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

            var foci = CollectionMapper.Map(TableNameConstants.Collection.FeatFoci);
            var skills = foci[GroupConstants.Skills];

            var names = creatures.Union(types).Union(subtypes).Union(skills);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SaveBonusesTestData), "Creatures")]
        [TestCaseSource(typeof(SaveBonusesTestData), "Types")]
        [TestCaseSource(typeof(SaveBonusesTestData), "Subtypes")]
        [TestCaseSource(typeof(SaveBonusesTestData), "SkillSynergies")]
        public void SkillBonuses(string source, Dictionary<string, int> skillAndBonus)
        {
            if (!skillAndBonus.Any())
                Assert.Fail("Test case did not specify special qualities or NONE");

            if (skillAndBonus.ContainsKey(SaveBonusesTestData.None))
                skillAndBonus.Clear();

            AssertTypesAndAmounts(source, skillAndBonus);
        }
    }
}
