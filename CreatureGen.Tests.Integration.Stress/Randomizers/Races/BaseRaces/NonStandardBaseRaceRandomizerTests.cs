using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    SizeConstants.BaseRaces.Aasimar,
                    SizeConstants.BaseRaces.Azer,
                    SizeConstants.BaseRaces.BlueSlaad,
                    SizeConstants.BaseRaces.Bugbear,
                    SizeConstants.BaseRaces.Centaur,
                    SizeConstants.BaseRaces.CloudGiant,
                    SizeConstants.BaseRaces.DeathSlaad,
                    SizeConstants.BaseRaces.DeepDwarf,
                    SizeConstants.BaseRaces.DeepHalfling,
                    SizeConstants.BaseRaces.Derro,
                    SizeConstants.BaseRaces.Doppelganger,
                    SizeConstants.BaseRaces.Drow,
                    SizeConstants.BaseRaces.DuergarDwarf,
                    SizeConstants.BaseRaces.FireGiant,
                    SizeConstants.BaseRaces.ForestGnome,
                    SizeConstants.BaseRaces.FrostGiant,
                    SizeConstants.BaseRaces.Gargoyle,
                    SizeConstants.BaseRaces.Githyanki,
                    SizeConstants.BaseRaces.Githzerai,
                    SizeConstants.BaseRaces.Gnoll,
                    SizeConstants.BaseRaces.Goblin,
                    SizeConstants.BaseRaces.GrayElf,
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
                    SizeConstants.BaseRaces.MountainDwarf,
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
                    SizeConstants.BaseRaces.Svirfneblin,
                    SizeConstants.BaseRaces.TallfellowHalfling,
                    SizeConstants.BaseRaces.Tiefling,
                    SizeConstants.BaseRaces.Troglodyte,
                    SizeConstants.BaseRaces.Troll,
                    SizeConstants.BaseRaces.WildElf,
                    SizeConstants.BaseRaces.WoodElf,
                    SizeConstants.BaseRaces.YuanTiAbomination,
                    SizeConstants.BaseRaces.YuanTiHalfblood,
                    SizeConstants.BaseRaces.YuanTiPureblood,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
        }

        [Test]
        public void StressNonStandardBaseRace()
        {
            stressor.Stress(AssertBaseRace);
        }
    }
}