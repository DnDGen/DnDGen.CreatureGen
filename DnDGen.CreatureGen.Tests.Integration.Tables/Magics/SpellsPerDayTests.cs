using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Magics
{
    [TestFixture]
    public class SpellsPerDayTests : TypesAndAmountsTests
    {
        protected override string TableName => TableNameConstants.TypeAndAmount.SpellsPerDay;

        private Dictionary<string, Dictionary<string, int>> spellsPerDay;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            spellsPerDay = GetSpellsPerDayData();
        }

        [Test]
        public void SpellsPerDayContainsAllCastersAndLevels()
        {
            var names = GetNames();
            Assert.That(spellsPerDay.Keys, Is.EquivalentTo(names));

            AssertCollectionNames(names);
        }

        [TestCaseSource(nameof(AllNames))]
        public void SpellsPerDay(string name)
        {
            Assert.That(spellsPerDay, Contains.Key(name));
            AssertTypesAndAmounts(name, spellsPerDay[name]);
        }

        public static IEnumerable AllNames => GetNames().Select(name => new TestCaseData(name));

        public static IEnumerable<string> GetNames()
        {
            var casters = new[]
            {
                SpellConstants.Casters.Bard,
                SpellConstants.Casters.Cleric,
                SpellConstants.Casters.Druid,
                SpellConstants.Casters.Sorcerer,
            };

            var levels = Enumerable.Range(1, 20);
            var names = new List<string>();

            foreach (var level in levels)
            {
                foreach (var caster in casters)
                {
                    names.Add($"{caster}:{level}");
                }
            }

            return names;
        }

        private static readonly int[][] BardSpellsPerDay =
        [
            [2],
            [3, 0],
            [3, 1],
            [3, 2, 0],
            [3, 3, 1],
            [3, 3, 2],
            [3, 3, 2, 0],
            [3, 3, 3, 1],
            [3, 3, 3, 2],
            [3, 3, 3, 2, 0],
            [3, 3, 3, 3, 1],
            [3, 3, 3, 3, 2],
            [3, 3, 3, 3, 2, 0],
            [4, 3, 3, 3, 3, 1],
            [4, 4, 3, 3, 3, 2],
            [4, 4, 4, 3, 3, 2, 0],
            [4, 4, 4, 4, 3, 3, 1],
            [4, 4, 4, 4, 4, 3, 2],
            [4, 4, 4, 4, 4, 4, 3],
            [4, 4, 4, 4, 4, 4, 4],
        ];

        private static readonly int[][] ClericDruidSpellsPerDay =
        [
            [3, 1],
            [4, 2],
            [4, 2, 1],
            [5, 3, 2],
            [5, 3, 2, 1],
            [5, 3, 3, 2],
            [6, 4, 3, 2, 1],
            [6, 4, 3, 3, 2],
            [6, 4, 4, 3, 2, 1],
            [6, 4, 4, 3, 3, 2],
            [6, 5, 4, 4, 3, 2, 1],
            [6, 5, 4, 4, 3, 3, 2],
            [6, 5, 5, 4, 4, 3, 2, 1],
            [6, 5, 5, 4, 4, 3, 3, 2],
            [6, 5, 5, 5, 4, 4, 3, 2, 1],
            [6, 5, 5, 5, 4, 4, 3, 3, 2],
            [6, 5, 5, 5, 5, 4, 4, 3, 2, 1],
            [6, 5, 5, 5, 5, 4, 4, 3, 3, 2],
            [6, 5, 5, 5, 5, 5, 4, 4, 3, 3],
            [6, 5, 5, 5, 5, 5, 4, 4, 4, 4],
        ];

        private static readonly int[][] SorcererSpellsPerDay =
        [
            [5, 3],
            [6, 4],
            [6, 5],
            [6, 6, 3],
            [6, 6, 4],
            [6, 6, 5, 3],
            [6, 6, 6, 4],
            [6, 6, 6, 5, 3],
            [6, 6, 6, 6, 4],
            [6, 6, 6, 6, 5, 3],
            [6, 6, 6, 6, 6, 4],
            [6, 6, 6, 6, 6, 5, 3],
            [6, 6, 6, 6, 6, 6, 4],
            [6, 6, 6, 6, 6, 6, 5, 3],
            [6, 6, 6, 6, 6, 6, 6, 4],
            [6, 6, 6, 6, 6, 6, 6, 5, 3],
            [6, 6, 6, 6, 6, 6, 6, 6, 4],
            [6, 6, 6, 6, 6, 6, 6, 6, 5, 3],
            [6, 6, 6, 6, 6, 6, 6, 6, 6, 4],
            [6, 6, 6, 6, 6, 6, 6, 6, 6, 6],
        ];

        private static Dictionary<string, Dictionary<string, int>> GetSpellsPerDayData()
        {
            var testCases = GetNames().ToDictionary(n => n, n => new Dictionary<string, int>());

            var casterMappings = new Dictionary<string, int[][]>
            {
                [SpellConstants.Casters.Bard] = BardSpellsPerDay,
                [SpellConstants.Casters.Cleric] = ClericDruidSpellsPerDay,
                [SpellConstants.Casters.Druid] = ClericDruidSpellsPerDay,
                [SpellConstants.Casters.Sorcerer] = SorcererSpellsPerDay,
            };

            foreach (var kvp in testCases)
            {
                var keySections = kvp.Key.Split(':');
                var className = keySections[0];
                var classLevel = Convert.ToInt32(keySections[1]);

                var classSpellsPerDay = casterMappings[className];

                for (var spellLevel = 0; spellLevel < classSpellsPerDay[classLevel - 1].Length; spellLevel++)
                {
                    testCases[kvp.Key][spellLevel.ToString()] = classSpellsPerDay[classLevel - 1][spellLevel];
                }
            }

            return testCases;
        }
    }
}
