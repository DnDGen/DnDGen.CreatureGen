using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class MaleHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 62)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 53)]
        [TestCase(SizeConstants.BaseRaces.Azer, 45)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 114)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 66)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 71)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 204)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 58)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 45)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 32)]
        [TestCase(SizeConstants.BaseRaces.Derro, 45)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(SizeConstants.BaseRaces.Drow, 53)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 45)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 132)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 36)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 168)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 58)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 66)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 66)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 66)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 32)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 53)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 58)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 114)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 58)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 55)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 58)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 58)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 53)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 45)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 114)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 50)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 72)]
        [TestCase(SizeConstants.BaseRaces.Human, 58)]
        [TestCase(SizeConstants.BaseRaces.Janni, 58)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 58)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 30)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 57)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 32)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 58)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 50)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 57)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 108)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 74)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 78)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 45)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 108)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 108)]
        [TestCase(SizeConstants.BaseRaces.Orc, 61)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 18)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 58)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 90)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 36)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 56)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 70)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 70)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 96)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 132)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 240)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 36)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 48)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 62)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(SizeConstants.BaseRaces.Troll, 96)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 53)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 53)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 96)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 53)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 53)]
        public void MaleHeight(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
