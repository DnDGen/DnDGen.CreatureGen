using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class MonsterBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                //INFO: Not including aquatic, as they cannot randomly appear
                return new[]
                {
                    SizeConstants.BaseRaces.Azer,
                    SizeConstants.BaseRaces.BlueSlaad,
                    SizeConstants.BaseRaces.Bugbear,
                    SizeConstants.BaseRaces.Centaur,
                    SizeConstants.BaseRaces.CloudGiant,
                    SizeConstants.BaseRaces.DeathSlaad,
                    SizeConstants.BaseRaces.Derro,
                    SizeConstants.BaseRaces.Doppelganger,
                    SizeConstants.BaseRaces.FireGiant,
                    SizeConstants.BaseRaces.FrostGiant,
                    SizeConstants.BaseRaces.Gargoyle,
                    SizeConstants.BaseRaces.Githyanki,
                    SizeConstants.BaseRaces.Githzerai,
                    SizeConstants.BaseRaces.Gnoll,
                    SizeConstants.BaseRaces.Goblin,
                    SizeConstants.BaseRaces.GraySlaad,
                    SizeConstants.BaseRaces.GreenSlaad,
                    SizeConstants.BaseRaces.Grimlock,
                    SizeConstants.BaseRaces.Harpy,
                    SizeConstants.BaseRaces.HillGiant,
                    SizeConstants.BaseRaces.Hobgoblin,
                    SizeConstants.BaseRaces.HoundArchon,
                    SizeConstants.BaseRaces.Janni,
                    SizeConstants.BaseRaces.Kobold,
                    SizeConstants.BaseRaces.Lizardfolk,
                    SizeConstants.BaseRaces.MindFlayer,
                    SizeConstants.BaseRaces.Minotaur,
                    SizeConstants.BaseRaces.Ogre,
                    SizeConstants.BaseRaces.OgreMage,
                    SizeConstants.BaseRaces.Orc,
                    SizeConstants.BaseRaces.Pixie,
                    SizeConstants.BaseRaces.Rakshasa,
                    SizeConstants.BaseRaces.RedSlaad,
                    SizeConstants.BaseRaces.Satyr,
                    SizeConstants.BaseRaces.Scorpionfolk,
                    SizeConstants.BaseRaces.StoneGiant,
                    SizeConstants.BaseRaces.StormGiant,
                    SizeConstants.BaseRaces.Troglodyte,
                    SizeConstants.BaseRaces.Troll,
                    SizeConstants.BaseRaces.YuanTiAbomination,
                    SizeConstants.BaseRaces.YuanTiHalfblood,
                    SizeConstants.BaseRaces.YuanTiPureblood,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.MonsterBase);
        }

        [Test]
        public void StressMonsterBaseRace()
        {
            stressor.Stress(AssertBaseRace);
        }
    }
}