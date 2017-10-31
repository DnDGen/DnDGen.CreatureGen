using CreatureGen.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class NoneFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Templates.None);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Empty<string>();
            AssertCollectionNames(names);
        }
    }
}
