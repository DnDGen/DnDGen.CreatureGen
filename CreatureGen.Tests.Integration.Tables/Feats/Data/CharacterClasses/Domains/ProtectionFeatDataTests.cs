using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class ProtectionFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Protection); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.SpellLikeAbility };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility,
            FeatConstants.SpellLikeAbility,
            "Grant someone you touch a resistance bonus equal to your cleric level on his or her next saving throw.",
            1,
            "",
            FeatConstants.Frequencies.Day,
            0,
            0,
            0,
            "", true)]
        public override void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.ClassFeatData(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
