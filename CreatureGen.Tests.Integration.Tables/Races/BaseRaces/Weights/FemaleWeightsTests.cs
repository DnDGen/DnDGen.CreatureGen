using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class FemaleWeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 90)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 80)]
        [TestCase(SizeConstants.BaseRaces.Azer, 100)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 900)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 250)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 365)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 4200)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 85)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 100)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 25)]
        [TestCase(SizeConstants.BaseRaces.Derro, 100)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 120)]
        [TestCase(SizeConstants.BaseRaces.Drow, 80)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 100)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 6200)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 35)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 2000)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 220)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 130)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 130)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 150)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 25)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 80)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 85)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 900)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 85)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 80)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 110)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 50)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 80)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 100)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 300)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 145)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 150)]
        [TestCase(SizeConstants.BaseRaces.Human, 85)]
        [TestCase(SizeConstants.BaseRaces.Janni, 85)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 220)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 20)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 90)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 25)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 130)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 120)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 35)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 600)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 105)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 400)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 100)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 600)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 600)]
        [TestCase(SizeConstants.BaseRaces.Orc, 120)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 4)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 85)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 550)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 35)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 85)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 130)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 260)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 450)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 700)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 11200)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 35)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 25)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 90)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 120)]
        [TestCase(SizeConstants.BaseRaces.Troll, 450)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 80)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 80)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 196)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 85)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 85)]
        public void FemaleWeight(string name, int adjustment)
        {
            Adjustment(name, adjustment);
        }
    }
}
