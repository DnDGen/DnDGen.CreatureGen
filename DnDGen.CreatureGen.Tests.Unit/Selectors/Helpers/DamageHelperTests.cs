using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class DamageHelperTests
    {
        private DamageHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new DamageHelper();
        }

        [Test]
        public void BuildDataIntoArray()
        {
            var data = helper.BuildData("my roll", "my damage type", "my condition");
            Assert.That(data[DataIndexConstants.AttackData.DamageData.RollIndex], Is.EqualTo("my roll"));
            Assert.That(data[DataIndexConstants.AttackData.DamageData.TypeIndex], Is.EqualTo("my damage type"));
            Assert.That(data[DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [Test]
        public void BuildDataIntoArray_NoCondition()
        {
            var data = helper.BuildData("my roll", "my damage type", string.Empty);
            Assert.That(data[DataIndexConstants.AttackData.DamageData.RollIndex], Is.EqualTo("my roll"));
            Assert.That(data[DataIndexConstants.AttackData.DamageData.TypeIndex], Is.EqualTo("my damage type"));
            Assert.That(data[DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.Empty);
        }

        [Test]
        public void BuildDataIntoArray_NoDamageType()
        {
            var data = helper.BuildData("my roll", string.Empty, "my condition");
            Assert.That(data[DataIndexConstants.AttackData.DamageData.RollIndex], Is.EqualTo("my roll"));
            Assert.That(data[DataIndexConstants.AttackData.DamageData.TypeIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [Test]
        public void BuildDataIntoString()
        {
            var data = new[] { "this", "is", "my", "data" };
            var dataString = helper.BuildEntry(data);
            Assert.That(dataString, Is.EqualTo("this#is#my#data"));
        }

        [Test]
        public void BuildDataIntoString_SingleDamages()
        {
            var dataString = helper.BuildEntries("1d3", "slashing", string.Empty);
            Assert.That(dataString, Is.EqualTo("1d3#slashing#"));
        }

        [Test]
        public void BuildDataIntoString_SingleDamages_WithImplicitEmptyConditional()
        {
            var implicitDataString = helper.BuildEntries("1d3", "slashing");
            var explicitDataString = helper.BuildEntries("1d3", "slashing", string.Empty);

            Assert.That(implicitDataString, Is.EqualTo("1d3#slashing#").And.EqualTo(explicitDataString));
        }

        [Test]
        public void BuildDataIntoString_SingleDamages_WithCondition()
        {
            var dataString = helper.BuildEntries("1d3", "slashing", "sometimes");
            Assert.That(dataString, Is.EqualTo("1d3#slashing#sometimes"));
        }

        [Test]
        public void BuildDataIntoString_MultipleDamages()
        {
            var dataString = helper.BuildEntries("1d3", "slashing", string.Empty, "1d4", "acid");
            Assert.That(dataString, Is.EqualTo("1d3#slashing#,1d4#acid#"));
        }

        [Test]
        public void BuildDataIntoString_MultipleDamages_WithConditions()
        {
            var dataString = helper.BuildEntries("1d3", "slashing", "sometimes", "1d4", "acid", "occasionally");
            Assert.That(dataString, Is.EqualTo("1d3#slashing#sometimes,1d4#acid#occasionally"));
        }

        [Test]
        public void BuildDataIntoString_MultipleDamages_WithoutDamageTypes()
        {
            var dataString = helper.BuildEntries("1d3", string.Empty, string.Empty, "1d4", "acid", "occasionally");
            Assert.That(dataString, Is.EqualTo("1d3##,1d4#acid#occasionally"));
        }

        [Test]
        public void BuildDataIntoString_NoDamages()
        {
            var dataString = helper.BuildEntries();
            Assert.That(dataString, Is.Empty);
        }

        [TestCase("2d10##,1d4#Charisma#", "2d10", "", "", "1d4", AbilityConstants.Charisma)]
        public void BuildEntries_RealExample(string expected, params string[] data)
        {
            var dataString = helper.BuildEntries(data);
            Assert.That(dataString, Is.EqualTo(expected));
        }

        [Test]
        public void ParseEntry()
        {
            var data = helper.ParseEntry("this#is#my#data");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data" }));
        }

        [Test]
        public void ParseEntries_NoDamages()
        {
            var data = helper.ParseEntries(string.Empty);
            Assert.That(data, Is.Empty);
        }

        [Test]
        public void ParseEntries_Damage()
        {
            var data = helper.ParseEntries("roll#damage type#");
            Assert.That(data, Is.EqualTo(new[]
            {
                new[] { "roll", "damage type", string.Empty },
            }));
        }

        [Test]
        public void ParseEntries_DamageWithCondition()
        {
            var data = helper.ParseEntries("roll#damage type#condition");
            Assert.That(data, Is.EqualTo(new[]
            {
                new[] { "roll", "damage type", "condition" },
            }));
        }

        [Test]
        public void ParseEntries_MultipleDamages()
        {
            var data = helper.ParseEntries("roll#damage type,other roll#other damage type");
            Assert.That(data, Is.EqualTo(new[]
            {
                new[] { "roll", "damage type" },
                new[] { "other roll", "other damage type" },
            }));
        }

        [Test]
        public void ParseEntries_MultipleDamagesWithConditions()
        {
            var data = helper.ParseEntries("roll#damage type#condition,other roll#other damage type#other condition");
            Assert.That(data, Is.EqualTo(new[]
            {
                new[] { "roll", "damage type", "condition" },
                new[] { "other roll", "other damage type", "other condition" },
            }));
        }

        [TestCase("0", "holy", "")]
        [TestCase("1d3", "", "sometimes")]
        [TestCase("1d4", "", "")]
        [TestCase("1d6", "emotional", "")]
        [TestCase("1d8", "spiritual", "on Sundays")]
        public void BuildKey_FromData(string roll, string type, string condition)
        {
            var data = helper.ParseEntry($"{roll}#{type}#{condition}");
            var key = helper.BuildKey("creature", data);
            Assert.That(key, Is.EqualTo($"creature{roll}{type}{condition}"));
        }

        [TestCase("0", "holy", "")]
        [TestCase("1d3", "", "sometimes")]
        [TestCase("1d4", "", "")]
        [TestCase("1d6", "emotional", "")]
        [TestCase("1d8", "spiritual", "on Sundays")]
        public void BuildKeyFromSections(string roll, string type, string condition)
        {
            var key = helper.BuildKeyFromSections("creature", roll, type, condition);
            Assert.That(key, Is.EqualTo($"creature{roll}{type}{condition}"));
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(7, false)]
        [TestCase(8, false)]
        [TestCase(9, false)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        [TestCase(12, false)]
        [TestCase(13, false)]
        [TestCase(14, false)]
        [TestCase(15, false)]
        [TestCase(16, false)]
        [TestCase(17, false)]
        [TestCase(18, false)]
        [TestCase(19, false)]
        [TestCase(20, false)]
        public void ValidateEntry_IsValid(int length, bool isValid)
        {
            var data = new string[length];
            var entry = helper.BuildEntry(data);
            var valid = helper.ValidateEntry(entry);
            Assert.That(valid, Is.EqualTo(isValid));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, false)]
        [TestCase(9, true)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        [TestCase(12, true)]
        [TestCase(13, false)]
        [TestCase(14, false)]
        [TestCase(15, true)]
        [TestCase(16, false)]
        [TestCase(17, false)]
        [TestCase(18, true)]
        [TestCase(19, false)]
        [TestCase(20, false)]
        public void ValidateEntries_IsValid(int length, bool isValid)
        {
            var data = Enumerable.Range(1, length).Select(i => i.ToString()).ToArray();
            var entry = BuildEntriesWithoutPadding(data);
            var valid = helper.ValidateEntries(entry);
            Assert.That(valid, Is.EqualTo(isValid));
        }

        private string BuildEntriesWithoutPadding(params string[] data)
        {
            var entries = new List<string>();
            var init = DataIndexConstants.AttackData.DamageData.InitializeData();

            for (var i = 0; i < data.Length; i += init.Length)
            {
                var subData = data.Skip(i).Take(init.Length).ToArray();
                var entry = helper.BuildEntry(subData);
                entries.Add(entry);
            }

            return string.Join(AttackSelection.DamageSplitDivider.ToString(), entries);
        }
    }
}
