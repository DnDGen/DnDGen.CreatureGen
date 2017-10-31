using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class SkillPointsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SkillPoints; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Elf_Aquatic, 0)]
        [TestCase(CreatureConstants.Azer, 8)]
        [TestCase(CreatureConstants.Slaad_Blue, 2)]
        [TestCase(CreatureConstants.Bugbear, 2)]
        [TestCase(CreatureConstants.Centaur, 2)]
        [TestCase(CreatureConstants.Giant_Cloud, 2)]
        [TestCase(CreatureConstants.Slaad_Death, 2)]
        [TestCase(CreatureConstants.Derro, 2)]
        [TestCase(CreatureConstants.Doppelganger, 2)]
        [TestCase(CreatureConstants.Giant_Fire, 2)]
        [TestCase(CreatureConstants.Giant_Frost, 2)]
        [TestCase(CreatureConstants.Gargoyle, 2)]
        [TestCase(CreatureConstants.Githyanki, 0)]
        [TestCase(CreatureConstants.Githzerai, 0)]
        [TestCase(CreatureConstants.Gnoll, 2)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 2)]
        [TestCase(CreatureConstants.Slaad_Green, 2)]
        [TestCase(CreatureConstants.Grimlock, 2)]
        [TestCase(CreatureConstants.Harpy, 2)]
        [TestCase(CreatureConstants.Giant_Hill, 2)]
        [TestCase(CreatureConstants.Hobgoblin, 0)]
        [TestCase(CreatureConstants.HoundArchon, 8)]
        [TestCase(CreatureConstants.Janni, 8)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 2)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 2)]
        [TestCase(CreatureConstants.Lizardfolk, 2)]
        [TestCase(CreatureConstants.Locathah, 0)]
        [TestCase(CreatureConstants.Merfolk, 0)]
        [TestCase(CreatureConstants.Ogre_Merrow, 2)]
        [TestCase(CreatureConstants.MindFlayer, 2)]
        [TestCase(CreatureConstants.Minotaur, 2)]
        [TestCase(CreatureConstants.Ogre, 2)]
        [TestCase(CreatureConstants.OgreMage, 2)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 2)]
        [TestCase(CreatureConstants.Rakshasa, 8)]
        [TestCase(CreatureConstants.Slaad_Red, 2)]
        [TestCase(CreatureConstants.Sahuagin, 2)]
        [TestCase(CreatureConstants.Satyr, 6)]
        [TestCase(CreatureConstants.Scorpionfolk, 2)]
        [TestCase(CreatureConstants.Troll_Scrag, 2)]
        [TestCase(CreatureConstants.Giant_Stone, 2)]
        [TestCase(CreatureConstants.Giant_Storm, 2)]
        [TestCase(CreatureConstants.Troglodyte, 2)]
        [TestCase(CreatureConstants.Troll, 2)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 2)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 2)]
        public void SkillPoints(string name, int points)
        {
            base.Adjustment(name, points);
        }
    }
}