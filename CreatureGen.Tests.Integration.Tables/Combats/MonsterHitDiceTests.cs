using CreatureGen.Domain.Tables;
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

        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 2)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 8)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 3)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 4)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 17)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 15)]
        [TestCase(SizeConstants.BaseRaces.Derro, 3)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 15)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 14)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 4)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 2)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 10)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 9)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 2)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 7)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 12)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 6)]
        [TestCase(SizeConstants.BaseRaces.Janni, 6)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 4)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 2)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 0)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 4)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 6)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 4)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 5)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 0)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 7)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 7)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 5)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 12)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 6)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 14)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 19)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(SizeConstants.BaseRaces.Troll, 6)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 9)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 7)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 4)]
        public void MonsterHitDice(string monster, int hitDice)
        {
            base.Adjustment(monster, hitDice);
        }
    }
}