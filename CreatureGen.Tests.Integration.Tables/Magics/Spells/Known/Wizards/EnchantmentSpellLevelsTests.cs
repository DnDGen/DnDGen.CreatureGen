using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class EnchantmentSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Enchantment);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Daze,
                SpellConstants.CharmPerson,
                SpellConstants.Hypnotism,
                SpellConstants.Sleep,
                SpellConstants.DazeMonster,
                SpellConstants.HideousLaughter,
                SpellConstants.TouchOfIdiocy,
                SpellConstants.DeepSlumber,
                SpellConstants.Heroism,
                SpellConstants.HoldPerson,
                SpellConstants.Rage,
                SpellConstants.Suggestion,
                SpellConstants.CharmMonster,
                SpellConstants.Confusion,
                SpellConstants.CrushingDespair,
                SpellConstants.Geas_Lesser,
                SpellConstants.DominatePerson,
                SpellConstants.Feeblemind,
                SpellConstants.HoldMonster,
                SpellConstants.MindFog,
                SpellConstants.SymbolOfSleep,
                SpellConstants.GeasQuest,
                SpellConstants.Heroism_Greater,
                SpellConstants.Suggestion_Mass,
                SpellConstants.SymbolOfPersuasion,
                SpellConstants.HoldPerson_Mass,
                SpellConstants.Insanity,
                SpellConstants.PowerWordBlind,
                SpellConstants.SymbolOfStunning,
                SpellConstants.Antipathy,
                SpellConstants.Binding,
                SpellConstants.CharmMonster_Mass,
                SpellConstants.Demand,
                SpellConstants.IrresistibleDance,
                SpellConstants.PowerWordStun,
                SpellConstants.SymbolOfInsanity,
                SpellConstants.Sympathy,
                SpellConstants.DominateMonster,
                SpellConstants.HoldMonster_Mass,
                SpellConstants.PowerWordKill
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEnchantmentSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Enchantment]);
        }

        [TestCase(SpellConstants.Daze, 0)]
        [TestCase(SpellConstants.CharmPerson, 1)]
        [TestCase(SpellConstants.Hypnotism, 1)]
        [TestCase(SpellConstants.Sleep, 1)]
        [TestCase(SpellConstants.DazeMonster, 2)]
        [TestCase(SpellConstants.HideousLaughter, 2)]
        [TestCase(SpellConstants.TouchOfIdiocy, 2)]
        [TestCase(SpellConstants.DeepSlumber, 3)]
        [TestCase(SpellConstants.Heroism, 3)]
        [TestCase(SpellConstants.HoldPerson, 3)]
        [TestCase(SpellConstants.Rage, 3)]
        [TestCase(SpellConstants.Suggestion, 3)]
        [TestCase(SpellConstants.CharmMonster, 4)]
        [TestCase(SpellConstants.Confusion, 4)]
        [TestCase(SpellConstants.CrushingDespair, 4)]
        [TestCase(SpellConstants.Geas_Lesser, 4)]
        [TestCase(SpellConstants.DominatePerson, 5)]
        [TestCase(SpellConstants.Feeblemind, 5)]
        [TestCase(SpellConstants.HoldMonster, 5)]
        [TestCase(SpellConstants.MindFog, 5)]
        [TestCase(SpellConstants.SymbolOfSleep, 5)]
        [TestCase(SpellConstants.GeasQuest, 6)]
        [TestCase(SpellConstants.Heroism_Greater, 6)]
        [TestCase(SpellConstants.Suggestion_Mass, 6)]
        [TestCase(SpellConstants.SymbolOfPersuasion, 6)]
        [TestCase(SpellConstants.HoldPerson_Mass, 7)]
        [TestCase(SpellConstants.Insanity, 7)]
        [TestCase(SpellConstants.PowerWordBlind, 7)]
        [TestCase(SpellConstants.SymbolOfStunning, 7)]
        [TestCase(SpellConstants.Antipathy, 8)]
        [TestCase(SpellConstants.Binding, 8)]
        [TestCase(SpellConstants.CharmMonster_Mass, 8)]
        [TestCase(SpellConstants.Demand, 8)]
        [TestCase(SpellConstants.IrresistibleDance, 8)]
        [TestCase(SpellConstants.PowerWordStun, 8)]
        [TestCase(SpellConstants.SymbolOfInsanity, 8)]
        [TestCase(SpellConstants.Sympathy, 8)]
        [TestCase(SpellConstants.DominateMonster, 9)]
        [TestCase(SpellConstants.HoldMonster_Mass, 9)]
        [TestCase(SpellConstants.PowerWordKill, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
