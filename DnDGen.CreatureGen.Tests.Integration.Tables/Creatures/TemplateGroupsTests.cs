using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class TemplateGroupsTests : CollectionTests
    {
        protected override string TableName => TableNameConstants.Collection.TemplateGroups;

        [Test]
        public void TemplateGroupNames()
        {
            var entries = new[]
            {
                GroupConstants.All,
                GroupConstants.HasMinimumAbility,
            };

            AssertCollectionNames(entries);
        }

        [Test]
        public void AllGroup()
        {
            var allTemplates = CreatureConstants.Templates.GetAll().Except([CreatureConstants.Templates.None]);
            AssertDistinctCollection(GroupConstants.All, [.. allTemplates]);
        }

        [Test]
        public void HasMinimumAbilityGroup()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var minimumAbilityTemplates = new List<string>();

            foreach (var template in templates)
            {
                var applicator = GetNewInstanceOf<TemplateApplicator>(template);
                if (applicator.MinimumAbility is not null)
                    minimumAbilityTemplates.Add(template);
            }

            AssertDistinctCollection(GroupConstants.HasMinimumAbility, [.. minimumAbilityTemplates]);
        }
    }
}
