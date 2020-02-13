using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class SkillBonusesTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.SkillBonuses;

        [Test]
        public void SkillBonusesNames()
        {
            var creatures = CreatureConstants.All();
            var types = CreatureConstants.Types.All();
            var subtypes = CreatureConstants.Types.Subtypes.All();

            var foci = CollectionMapper.Map(TableNameConstants.Collection.FeatFoci);
            var skills = foci[GroupConstants.Skills];

            var nonFociSkills = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };

            var names = creatures.Union(types).Union(subtypes).Union(skills).Union(nonFociSkills);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SkillBonusesTestData), "Creatures")]
        [TestCaseSource(typeof(SkillBonusesTestData), "Types")]
        [TestCaseSource(typeof(SkillBonusesTestData), "Subtypes")]
        public void SkillBonuses(string source, Dictionary<string, int> skillAndBonus)
        {
            if (!skillAndBonus.Any())
                Assert.Fail("Test case did not specify skill bonuses or NONE");

            if (skillAndBonus.ContainsKey(SkillBonusesTestData.None))
                skillAndBonus.Clear();

            AssertTypesAndAmounts(source, skillAndBonus);
        }

        [TestCaseSource(typeof(SkillBonusesTestData), "SkillSynergies")]
        public void SkillSynergies(string source, Dictionary<string, int> skillAndBonus)
        {
            if (!skillAndBonus.Any())
                Assert.Fail("Test case did not specify skill bonuses or NONE");

            if (skillAndBonus.ContainsKey(SkillBonusesTestData.None))
                skillAndBonus.Clear();

            AssertTypesAndAmounts(source, skillAndBonus);
        }
    }
}
