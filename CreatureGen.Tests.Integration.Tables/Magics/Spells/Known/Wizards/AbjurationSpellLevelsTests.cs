using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class AbjurationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Abjuration);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Resistance,
                SpellConstants.Alarm,
                SpellConstants.EndureElements,
                SpellConstants.HoldPortal,
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.Shield,
                SpellConstants.ArcaneLock,
                SpellConstants.ObscureObject,
                SpellConstants.ProtectionFromArrows,
                SpellConstants.ResistEnergy,
                SpellConstants.DispelMagic,
                SpellConstants.ExplosiveRunes,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.Nondetection,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.DimensionalAnchor,
                SpellConstants.FireTrap,
                SpellConstants.GlobeOfInvulnerability_Lesser,
                SpellConstants.RemoveCurse,
                SpellConstants.Stoneskin,
                SpellConstants.BreakEnchantment,
                SpellConstants.Dismissal,
                SpellConstants.MagesPrivateSanctum,
                SpellConstants.AntimagicField,
                SpellConstants.DispelMagic_Greater,
                SpellConstants.GlobeOfInvulnerability,
                SpellConstants.GuardsAndWards,
                SpellConstants.Repulsion,
                SpellConstants.Banishment,
                SpellConstants.Sequester,
                SpellConstants.SpellTurning,
                SpellConstants.DimensionalLock,
                SpellConstants.MindBlank,
                SpellConstants.PrismaticWall,
                SpellConstants.ProtectionFromSpells,
                SpellConstants.Freedom,
                SpellConstants.Imprisonment,
                SpellConstants.MagesDisjunction,
                SpellConstants.PrismaticSphere
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllAbjurationSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Abjuration]);
        }

        [TestCase(SpellConstants.Resistance, 0)]
        [TestCase(SpellConstants.Alarm, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.HoldPortal, 1)]
        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.Shield, 1)]
        [TestCase(SpellConstants.ArcaneLock, 2)]
        [TestCase(SpellConstants.ObscureObject, 2)]
        [TestCase(SpellConstants.ProtectionFromArrows, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.ExplosiveRunes, 3)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.Nondetection, 3)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.DimensionalAnchor, 4)]
        [TestCase(SpellConstants.FireTrap, 4)]
        [TestCase(SpellConstants.GlobeOfInvulnerability_Lesser, 4)]
        [TestCase(SpellConstants.RemoveCurse, 4)]
        [TestCase(SpellConstants.Stoneskin, 4)]
        [TestCase(SpellConstants.BreakEnchantment, 5)]
        [TestCase(SpellConstants.Dismissal, 5)]
        [TestCase(SpellConstants.MagesPrivateSanctum, 5)]
        [TestCase(SpellConstants.AntimagicField, 6)]
        [TestCase(SpellConstants.DispelMagic_Greater, 6)]
        [TestCase(SpellConstants.GlobeOfInvulnerability, 6)]
        [TestCase(SpellConstants.GuardsAndWards, 6)]
        [TestCase(SpellConstants.Repulsion, 6)]
        [TestCase(SpellConstants.Banishment, 7)]
        [TestCase(SpellConstants.Sequester, 7)]
        [TestCase(SpellConstants.SpellTurning, 7)]
        [TestCase(SpellConstants.DimensionalLock, 8)]
        [TestCase(SpellConstants.MindBlank, 8)]
        [TestCase(SpellConstants.PrismaticWall, 8)]
        [TestCase(SpellConstants.ProtectionFromSpells, 8)]
        [TestCase(SpellConstants.Freedom, 9)]
        [TestCase(SpellConstants.Imprisonment, 9)]
        [TestCase(SpellConstants.MagesDisjunction, 9)]
        [TestCase(SpellConstants.PrismaticSphere, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
