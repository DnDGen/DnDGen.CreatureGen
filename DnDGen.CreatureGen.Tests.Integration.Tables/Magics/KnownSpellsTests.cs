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
    public class KnownSpellsTests : TypesAndAmountsTests
    {
        protected override string TableName => TableNameConstants.TypeAndAmount.KnownSpells;

        private Dictionary<string, Dictionary<string, int>> knownSpells;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            knownSpells = GetKnownSpellsData();
        }

        [Test]
        public void KnownSpellsContainsAllCastersAndLevels()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        [TestCaseSource(nameof(All))]
        public void KnownSpells(string name)
        {
            AssertTypesAndAmounts(name, knownSpells[name]);
        }

        public static IEnumerable All => GetNames().Select(name => new TestCaseData(name).SetName(name));

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

        private static readonly int[][] BardKnownSpells =
        [
            [4],
            [5, 2],
            [6, 3],
            [6, 3, 2],
            [6, 4, 3],
            [6, 4, 3],
            [6, 4, 4, 2],
            [6, 4, 4, 3],
            [6, 4, 4, 3],
            [6, 4, 4, 4, 2],
            [6, 4, 4, 4, 3],
            [6, 4, 4, 4, 3],
            [6, 4, 4, 4, 4, 2],
            [6, 4, 4, 4, 4, 3],
            [6, 4, 4, 4, 4, 3],
            [6, 5, 4, 4, 4, 4, 2],
            [6, 5, 5, 4, 4, 4, 3],
            [6, 5, 5, 5, 4, 4, 3],
            [6, 5, 5, 5, 5, 4, 4],
            [6, 5, 5, 5, 5, 5, 4],
        ];

        private static readonly int[][] ClericDruidKnownSpells =
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

        private static readonly int[][] SorcererKnownSpells =
        [
            [4, 2],
            [5, 2],
            [5, 3],
            [6, 3, 1],
            [6, 4, 2],
            [7, 4, 2, 1],
            [7, 5, 3, 2],
            [8, 5, 3, 2, 1],
            [8, 5, 4, 3, 2],
            [9, 5, 4, 3, 2, 1],
            [9, 5, 5, 4, 3, 2],
            [9, 5, 5, 4, 3, 2, 1],
            [9, 5, 5, 4, 4, 3, 2],
            [9, 5, 5, 4, 4, 3, 2, 1],
            [9, 5, 5, 4, 4, 4, 3, 2],
            [9, 5, 5, 4, 4, 4, 3, 2, 1],
            [9, 5, 5, 4, 4, 4, 3, 3, 2],
            [9, 5, 5, 4, 4, 4, 3, 3, 2, 1],
            [9, 5, 5, 4, 4, 4, 3, 3, 3, 2],
            [9, 5, 5, 4, 4, 4, 3, 3, 3, 3],
        ];

        private static Dictionary<string, Dictionary<string, int>> GetKnownSpellsData()
        {
            var testCases = GetNames().ToDictionary(n => n, n => new Dictionary<string, int>());

            var casterMappings = new Dictionary<string, int[][]>
            {
                [SpellConstants.Casters.Bard] = BardKnownSpells,
                [SpellConstants.Casters.Cleric] = ClericDruidKnownSpells,
                [SpellConstants.Casters.Druid] = ClericDruidKnownSpells,
                [SpellConstants.Casters.Sorcerer] = SorcererKnownSpells,
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
