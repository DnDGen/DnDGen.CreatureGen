using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class HeightRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.HeightRolls; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "2d8")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Azer, "2d4")]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "3d4")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, "2d10")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "2d4")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Derro, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Drow, "2d6")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "2d4")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "2d4")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "2d4")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, "2d10")]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "2d4")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "2d8")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "2d4")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "2d8")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "1d12")]
        [TestCase(SizeConstants.BaseRaces.Human, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Janni, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "2d4")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "2d6")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "2d8")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "2d6")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "2d8")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "2d6")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Orc, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "3d8")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "2d10")]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, "2d6")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "1d10")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "3d10")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "2d10")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "2d4")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "2d8")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Troll, "2d10")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "4d12")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "2d10")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "2d10")]
        public void HeightRoll(string name, string heightRoll)
        {
            base.DistinctCollection(name, new[] { heightRoll });
        }
    }
}
