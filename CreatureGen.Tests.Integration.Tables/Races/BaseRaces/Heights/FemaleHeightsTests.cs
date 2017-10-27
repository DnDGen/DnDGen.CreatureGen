using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class FemaleHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Female"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 60)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 53)]
        [TestCase(SizeConstants.BaseRaces.Azer, 43)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 114)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 66)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 71)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 198)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 53)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 43)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 30)]
        [TestCase(SizeConstants.BaseRaces.Derro, 43)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(SizeConstants.BaseRaces.Drow, 53)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 43)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 126)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 34)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 162)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 53)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 63)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 63)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 63)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 30)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 53)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 53)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 114)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 58)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 53)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 53)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 55)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 53)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 43)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 108)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 48)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 70)]
        [TestCase(SizeConstants.BaseRaces.Human, 53)]
        [TestCase(SizeConstants.BaseRaces.Janni, 53)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 53)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 28)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 59)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 30)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 50)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 50)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 57)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 100)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 72)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 74)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 43)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 100)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 100)]
        [TestCase(SizeConstants.BaseRaces.Orc, 57)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 14)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 53)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 90)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 34)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 56)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 65)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 72)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 102)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 126)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 234)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 34)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 46)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 60)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(SizeConstants.BaseRaces.Troll, 102)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 53)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 53)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 96)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 53)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 53)]
        public void FemaleHeight(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
