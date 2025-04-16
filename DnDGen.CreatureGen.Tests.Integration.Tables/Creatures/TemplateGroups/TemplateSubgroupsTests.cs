using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.TemplateGroups
{
    [TestFixture]
    public class TemplateSubgroupsTests : TemplateGroupsTableTests
    {
        [Test]
        public void TemplateGroupNames()
        {
            AssertTemplateGroupNamesAreComplete();
        }

        [TestCase(CreatureConstants.Groups.HalfDragon,
            CreatureConstants.Templates.HalfDragon_Black,
            CreatureConstants.Templates.HalfDragon_Blue,
            CreatureConstants.Templates.HalfDragon_Brass,
            CreatureConstants.Templates.HalfDragon_Bronze,
            CreatureConstants.Templates.HalfDragon_Copper,
            CreatureConstants.Templates.HalfDragon_Gold,
            CreatureConstants.Templates.HalfDragon_Green,
            CreatureConstants.Templates.HalfDragon_Red,
            CreatureConstants.Templates.HalfDragon_Silver,
            CreatureConstants.Templates.HalfDragon_White)]
        [TestCase(CreatureConstants.Groups.Lycanthrope,
            CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural,
            CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural,
            CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural,
            CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural,
            CreatureConstants.Templates.Lycanthrope_Boar_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Boar_Natural,
            CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural,
            CreatureConstants.Templates.Lycanthrope_Rat_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Rat_Natural,
            CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural,
            CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Tiger_Natural,
            CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural,
            CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Wolf_Natural,
            CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted,
            CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural)]
        public void TemplateSubgroup(string creature, params string[] subgroup)
        {
            AssertDistinctCollection(creature, subgroup);
        }

        [Test]
        public void AllGroup()
        {
            var entries = new[]
            {
                CreatureConstants.Groups.Lycanthrope,
                CreatureConstants.Groups.HalfDragon,
                CreatureConstants.Templates.Skeleton,
                CreatureConstants.Templates.Zombie,
                CreatureConstants.Templates.CelestialCreature,
                CreatureConstants.Templates.FiendishCreature,
                CreatureConstants.Templates.Ghost,
                CreatureConstants.Templates.Vampire,
                CreatureConstants.Templates.Lich,
                CreatureConstants.Templates.HalfCelestial,
                CreatureConstants.Templates.HalfFiend,
            };

            AssertTemplateGroup(GroupConstants.All, entries);

            var allTemplates = CreatureConstants.Templates.GetAll();
            AssertDistinctCollection(GroupConstants.All, [.. allTemplates]);
        }

        [Test]
        public void AnimatedObjectGroup()
        {
            AssertDistinctCollection(CreatureConstants.Groups.AnimatedObject, []);
        }

        [Test]
        public void NoCircularSubgroups()
        {
            foreach (var kvp in table)
            {
                AssertGroupDoesNotContain(kvp.Key, kvp.Key);
            }
        }

        private void AssertGroupDoesNotContain(string name, string forbiddenEntry)
        {
            var group = table[name];

            //INFO: A group is allowed to contain itself as an immediate child
            //Example is the Orc subtype group containing the Orc creature
            if (name != forbiddenEntry)
                Assert.That(group, Does.Not.Contain(forbiddenEntry), name);

            //INFO: Remove the name from the group, or we get infinite recursion traversing itself as a subgroup
            var subgroupNames = group.Intersect(table.Keys).Except([name]);

            foreach (var subgroupName in subgroupNames)
            {
                AssertGroupDoesNotContain(subgroupName, forbiddenEntry);
                AssertGroupDoesNotContain(subgroupName, subgroupName);
            }
        }
    }
}
