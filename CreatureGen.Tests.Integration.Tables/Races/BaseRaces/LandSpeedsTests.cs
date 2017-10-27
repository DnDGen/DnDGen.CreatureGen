using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class LandSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.LandSpeeds; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            AssertCollectionNames(baseRaceGroups[GroupConstants.All]);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 30)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 30)]
        [TestCase(SizeConstants.BaseRaces.Azer, 30)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 30)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 30)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 50)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 50)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 30)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 20)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 20)]
        [TestCase(SizeConstants.BaseRaces.Derro, 20)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 30)]
        [TestCase(SizeConstants.BaseRaces.Drow, 30)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 20)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 40)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 20)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 40)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 40)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 30)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 30)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 30)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 30)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 30)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 30)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 30)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 30)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 30)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 30)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 20)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 30)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 20)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 40)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 30)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 40)]
        [TestCase(SizeConstants.BaseRaces.Human, 30)]
        [TestCase(SizeConstants.BaseRaces.Janni, 30)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 40)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 30)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 20)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 20)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 30)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 10)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 5)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 30)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 30)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 30)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 20)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 40)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 50)]
        [TestCase(SizeConstants.BaseRaces.Orc, 30)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 20)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 40)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 30)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 20)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 30)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 40)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 40)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 20)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 40)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 50)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 20)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 20)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 30)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 30)]
        [TestCase(SizeConstants.BaseRaces.Troll, 30)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 30)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 30)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 30)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 30)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 30)]
        public void LandSpeed(string name, int speed)
        {
            base.Adjustment(name, speed);
        }
    }
}