using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class MaleWeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Male"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 110)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 85)]
        [TestCase(SizeConstants.BaseRaces.Azer, 130)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 900)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 250)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 400)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 4300)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 120)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 130)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 30)]
        [TestCase(SizeConstants.BaseRaces.Derro, 130)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 120)]
        [TestCase(SizeConstants.BaseRaces.Drow, 85)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 130)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 6300)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 40)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 2100)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 300)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 140)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 140)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 170)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 30)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 85)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 120)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 900)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 120)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 100)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 150)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 65)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 85)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 130)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 400)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 165)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 180)]
        [TestCase(SizeConstants.BaseRaces.Human, 120)]
        [TestCase(SizeConstants.BaseRaces.Janni, 120)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 300)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 25)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 85)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 30)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 150)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 120)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 40)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 620)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 110)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 500)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 130)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 620)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 620)]
        [TestCase(SizeConstants.BaseRaces.Orc, 160)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 6)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 120)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 550)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 40)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 120)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 170)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 240)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 440)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 800)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 11300)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 40)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 30)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 110)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 120)]
        [TestCase(SizeConstants.BaseRaces.Troll, 440)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 85)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 85)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 196)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 120)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 120)]
        public void MaleWeight(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
