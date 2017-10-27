using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class AirSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Air);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ObscuringMist,
                SpellConstants.WindWall,
                SpellConstants.GaseousForm,
                SpellConstants.AirWalk,
                SpellConstants.ControlWinds,
                SpellConstants.ChainLightning,
                SpellConstants.ControlWeather,
                SpellConstants.Whirlwind,
                SpellConstants.ElementalSwarm
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllAirSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Air]);
        }

        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.WindWall, 2)]
        [TestCase(SpellConstants.GaseousForm, 3)]
        [TestCase(SpellConstants.AirWalk, 4)]
        [TestCase(SpellConstants.ControlWinds, 5)]
        [TestCase(SpellConstants.ChainLightning, 6)]
        [TestCase(SpellConstants.ControlWeather, 7)]
        [TestCase(SpellConstants.Whirlwind, 8)]
        [TestCase(SpellConstants.ElementalSwarm, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
