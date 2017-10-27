using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Paladins
{
    [TestFixture]
    public class PaladinSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Paladin);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Bless,
                SpellConstants.BlessWater,
                SpellConstants.BlessWeapon,
                SpellConstants.CreateWater,
                SpellConstants.CureInflictLightWounds,
                SpellConstants.DetectPoison,
                SpellConstants.DetectUndead,
                SpellConstants.DivineFavor,
                SpellConstants.EndureElements,
                SpellConstants.MagicWeapon,
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.ReadMagic,
                SpellConstants.Resistance,
                SpellConstants.Restoration_Lesser,
                SpellConstants.Virtue,
                SpellConstants.BullsStrength,
                SpellConstants.DelayPoison,
                SpellConstants.EaglesSplendor,
                SpellConstants.OwlsWisdom,
                SpellConstants.RemoveParalysis,
                SpellConstants.ResistEnergy,
                SpellConstants.ShieldOther,
                SpellConstants.UndetectableAlignment,
                SpellConstants.ZoneOfTruth,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.Daylight,
                SpellConstants.DiscernLies,
                SpellConstants.DispelMagic,
                SpellConstants.HealMount,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.MagicWeapon_Greater,
                SpellConstants.Prayer,
                SpellConstants.RemoveBlindnessDeafness,
                SpellConstants.RemoveCurse,
                SpellConstants.BreakEnchantment,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.DeathWard,
                SpellConstants.DispelAlignment,
                SpellConstants.HolySword,
                SpellConstants.MarkOfJustice,
                SpellConstants.NeutralizePoison,
                SpellConstants.Restoration
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllPaladinSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Paladin]);
        }

        [TestCase(SpellConstants.Bless, 1)]
        [TestCase(SpellConstants.BlessWater, 1)]
        [TestCase(SpellConstants.BlessWeapon, 1)]
        [TestCase(SpellConstants.CreateWater, 1)]
        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.DetectPoison, 1)]
        [TestCase(SpellConstants.DetectUndead, 1)]
        [TestCase(SpellConstants.DivineFavor, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.MagicWeapon, 1)]
        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.ReadMagic, 1)]
        [TestCase(SpellConstants.Resistance, 1)]
        [TestCase(SpellConstants.Restoration_Lesser, 1)]
        [TestCase(SpellConstants.Virtue, 1)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.DelayPoison, 2)]
        [TestCase(SpellConstants.EaglesSplendor, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.RemoveParalysis, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.ShieldOther, 2)]
        [TestCase(SpellConstants.UndetectableAlignment, 2)]
        [TestCase(SpellConstants.ZoneOfTruth, 2)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.DiscernLies, 3)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.HealMount, 3)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.MagicWeapon_Greater, 3)]
        [TestCase(SpellConstants.Prayer, 3)]
        [TestCase(SpellConstants.RemoveBlindnessDeafness, 3)]
        [TestCase(SpellConstants.RemoveCurse, 3)]
        [TestCase(SpellConstants.BreakEnchantment, 4)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 4)]
        [TestCase(SpellConstants.DeathWard, 4)]
        [TestCase(SpellConstants.DispelAlignment, 4)]
        [TestCase(SpellConstants.HolySword, 4)]
        [TestCase(SpellConstants.MarkOfJustice, 4)]
        [TestCase(SpellConstants.NeutralizePoison, 4)]
        [TestCase(SpellConstants.Restoration, 4)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
