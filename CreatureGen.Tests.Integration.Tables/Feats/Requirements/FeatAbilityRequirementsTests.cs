using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatAbilityRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var feats = FeatConstants.All();
        }
    }
}
