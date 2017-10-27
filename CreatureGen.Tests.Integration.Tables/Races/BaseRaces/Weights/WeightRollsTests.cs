using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class WeightRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.WeightRolls; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "2d4")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Azer, "2d6")]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "2d4")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "1")]
        [TestCase(SizeConstants.BaseRaces.Derro, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Drow, "1d6")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "1")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "1")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "1d6")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "2d4")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "2d4")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Human, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Janni, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "1")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "1d4")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "1")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "4d6")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "4d6")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "4d6")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Orc, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "2d6")]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, "4d6")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "1")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "8d8")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "3d4")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "2d4")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "15d6")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "1")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "1")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Troll, "2d4")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "1d2")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "2d4")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "2d4")]
        public void WeightRoll(string name, string weightRoll)
        {
            base.DistinctCollection(name, new[] { weightRoll });
        }
    }
}
