using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Schools
{
    [TestFixture]
    public class NecromancyFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Schools.Necromancy); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.SkillBonus };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus,
            FeatConstants.SkillBonus,
            SkillConstants.Spellcraft + " (Learn Necromancy spells)",
            0,
            "",
            "",
            1,
            0,
            2,
            "", true)]
        public override void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.ClassFeatData(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
