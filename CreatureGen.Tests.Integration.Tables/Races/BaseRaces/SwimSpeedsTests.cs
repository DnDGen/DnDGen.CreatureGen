using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class SwimSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SwimSpeeds; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            AssertCollectionNames(baseRaceGroups[GroupConstants.All]);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 0)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 40)]
        [TestCase(SizeConstants.BaseRaces.Azer, 0)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 0)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 0)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Derro, 0)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 0)]
        [TestCase(SizeConstants.BaseRaces.Drow, 0)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 0)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 0)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 0)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 0)]
        [TestCase(SizeConstants.BaseRaces.Human, 0)]
        [TestCase(SizeConstants.BaseRaces.Janni, 0)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 60)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 50)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 60)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 50)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 40)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 0)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 0)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 0)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 0)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 0)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 0)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 60)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 0)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 40)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 40)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 0)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(SizeConstants.BaseRaces.Troll, 0)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 0)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 20)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 0)]
        public void SwimSpeed(string name, int speed)
        {
            base.Adjustment(name, speed);
        }
    }
}