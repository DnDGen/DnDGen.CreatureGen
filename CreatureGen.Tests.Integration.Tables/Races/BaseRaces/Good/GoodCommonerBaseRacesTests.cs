﻿using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodCommonerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Commoner); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(11, 15, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(16, 25, SizeConstants.BaseRaces.HighElf)]
        [TestCase(26, 35, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(36, 45, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(46, 50, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(51, 55, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(56, 94, SizeConstants.BaseRaces.Human)]
        [TestCase(95, 95, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(96, 96, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(98, 98, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(99, 99, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(100, 100, SizeConstants.BaseRaces.WildElf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
