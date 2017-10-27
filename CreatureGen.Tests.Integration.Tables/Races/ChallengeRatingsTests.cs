using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class ChallengeRatingsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.ChallengeRatings;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]);

            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 0)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 2)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 8)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 2)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 3)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 11)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 13)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Derro, 3)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 3)]
        [TestCase(SizeConstants.BaseRaces.Drow, 0)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 10)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 9)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 4)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 1)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 10)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 9)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 1)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 4)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 7)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 5)]
        [TestCase(SizeConstants.BaseRaces.Human, 0)]
        [TestCase(SizeConstants.BaseRaces.Janni, 4)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 4)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 2)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 1)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 0)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 3)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 4)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 3)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 8)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 4)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 10)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 7)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 2)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 7)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 5)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 8)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 13)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 0)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 1)]
        [TestCase(SizeConstants.BaseRaces.Troll, 5)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 0)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 7)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 5)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 3)]
        [TestCase(SizeConstants.Metaraces.Ghost, 2)]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, 1)]
        [TestCase(SizeConstants.Metaraces.HalfDragon, 2)]
        [TestCase(SizeConstants.Metaraces.HalfFiend, 1)]
        [TestCase(SizeConstants.Metaraces.Lich, 2)]
        [TestCase(SizeConstants.Metaraces.Mummy, 5)]
        [TestCase(SizeConstants.Metaraces.None, 0)]
        [TestCase(SizeConstants.Metaraces.Vampire, 2)]
        [TestCase(SizeConstants.Metaraces.Werebear, 5)]
        [TestCase(SizeConstants.Metaraces.Wereboar, 4)]
        [TestCase(SizeConstants.Metaraces.Wererat, 2)]
        [TestCase(SizeConstants.Metaraces.Weretiger, 5)]
        [TestCase(SizeConstants.Metaraces.Werewolf, 3)]
        public void ChallengeRating(string name, int challengeRating)
        {
            base.Adjustment(name, challengeRating);
        }
    }
}
