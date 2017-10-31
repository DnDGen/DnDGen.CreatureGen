using CreatureGen.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class MonsterHitDiceTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.MonsterHitDice; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var monsters = baseRaceGroups[GroupConstants.Monsters];

            AssertCollectionNames(monsters);
        }

        [TestCase(CreatureConstants.Elf_Aquatic, 0)]
        [TestCase(CreatureConstants.Azer, 2)]
        [TestCase(CreatureConstants.Slaad_Blue, 8)]
        [TestCase(CreatureConstants.Bugbear, 3)]
        [TestCase(CreatureConstants.Centaur, 4)]
        [TestCase(CreatureConstants.Giant_Cloud, 17)]
        [TestCase(CreatureConstants.Slaad_Death, 15)]
        [TestCase(CreatureConstants.Derro, 3)]
        [TestCase(CreatureConstants.Doppelganger, 4)]
        [TestCase(CreatureConstants.Giant_Fire, 15)]
        [TestCase(CreatureConstants.Giant_Frost, 14)]
        [TestCase(CreatureConstants.Gargoyle, 4)]
        [TestCase(CreatureConstants.Githyanki, 0)]
        [TestCase(CreatureConstants.Githzerai, 0)]
        [TestCase(CreatureConstants.Gnoll, 2)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 10)]
        [TestCase(CreatureConstants.Slaad_Green, 9)]
        [TestCase(CreatureConstants.Grimlock, 2)]
        [TestCase(CreatureConstants.Harpy, 7)]
        [TestCase(CreatureConstants.Giant_Hill, 12)]
        [TestCase(CreatureConstants.Hobgoblin, 0)]
        [TestCase(CreatureConstants.HoundArchon, 6)]
        [TestCase(CreatureConstants.Janni, 6)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 4)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 2)]
        [TestCase(CreatureConstants.Lizardfolk, 2)]
        [TestCase(CreatureConstants.Locathah, 0)]
        [TestCase(CreatureConstants.Merfolk, 0)]
        [TestCase(CreatureConstants.Ogre_Merrow, 4)]
        [TestCase(CreatureConstants.MindFlayer, 8)]
        [TestCase(CreatureConstants.Minotaur, 6)]
        [TestCase(CreatureConstants.Ogre, 4)]
        [TestCase(CreatureConstants.OgreMage, 5)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 0)]
        [TestCase(CreatureConstants.Rakshasa, 7)]
        [TestCase(CreatureConstants.Slaad_Red, 7)]
        [TestCase(CreatureConstants.Sahuagin, 2)]
        [TestCase(CreatureConstants.Satyr, 5)]
        [TestCase(CreatureConstants.Scorpionfolk, 12)]
        [TestCase(CreatureConstants.Troll_Scrag, 6)]
        [TestCase(CreatureConstants.Giant_Stone, 14)]
        [TestCase(CreatureConstants.Giant_Storm, 19)]
        [TestCase(CreatureConstants.Troglodyte, 2)]
        [TestCase(CreatureConstants.Troll, 6)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 9)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 7)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 4)]
        public void MonsterHitDice(string monster, int hitDice)
        {
            base.Adjustment(monster, hitDice);
        }
    }
}