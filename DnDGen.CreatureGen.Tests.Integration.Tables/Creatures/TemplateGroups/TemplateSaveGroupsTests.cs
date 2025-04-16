using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.TemplateGroups
{
    [TestFixture]
    public class TemplateSaveGroupsTests : TemplateGroupsTableTests
    {
        [Test]
        public void TemplateGroupNames()
        {
            AssertTemplateGroupNamesAreComplete();
        }

        [TestCase(SaveConstants.Fortitude,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(SaveConstants.Reflex,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider)]
        [TestCase(SaveConstants.Will,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Undead)]
        public void TemplateSaveGroup(string save, params string[] group)
        {
            AssertTemplateGroup(save, group);
        }

        [TestCase(SaveConstants.Will, CreatureConstants.Templates.Skeleton)]
        [TestCase(SaveConstants.Will, CreatureConstants.Templates.Zombie)]
        public void BUG_SaveGroupContainsTemplate(string save, string template)
        {
            var explodedGroup = ExplodeCollection(tableName, save + GroupConstants.TREE);
            Assert.That(explodedGroup, Contains.Item(template));
            Assert.That(table[save], Contains.Item(template));
        }
    }
}
