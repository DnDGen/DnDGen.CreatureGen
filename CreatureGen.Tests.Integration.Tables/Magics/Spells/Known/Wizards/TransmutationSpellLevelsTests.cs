using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class TransmutationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Transmutation);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.MageHand,
                SpellConstants.Mending,
                SpellConstants.Message,
                SpellConstants.OpenClose,
                SpellConstants.AnimateRope,
                SpellConstants.EnlargePerson,
                SpellConstants.Erase,
                SpellConstants.ExpeditiousRetreat,
                SpellConstants.FeatherFall,
                SpellConstants.Jump,
                SpellConstants.MagicWeapon,
                SpellConstants.ReducePerson,
                SpellConstants.AlterSelf,
                SpellConstants.BearsEndurance,
                SpellConstants.BullsStrength,
                SpellConstants.CatsGrace,
                SpellConstants.Darkvision,
                SpellConstants.EaglesSplendor,
                SpellConstants.FoxsCunning,
                SpellConstants.Knock,
                SpellConstants.Levitate,
                SpellConstants.OwlsWisdom,
                SpellConstants.Pyrotechnics,
                SpellConstants.RopeTrick,
                SpellConstants.SpiderClimb,
                SpellConstants.WhisperingWind,
                SpellConstants.Blink,
                SpellConstants.FlameArrow,
                SpellConstants.Fly,
                SpellConstants.GaseousForm,
                SpellConstants.Haste,
                SpellConstants.KeenEdge,
                SpellConstants.MagicWeapon_Greater,
                SpellConstants.SecretPage,
                SpellConstants.ShrinkItem,
                SpellConstants.Slow,
                SpellConstants.WaterBreathing,
                SpellConstants.EnlargePerson_Mass,
                SpellConstants.Polymorph,
                SpellConstants.ReducePerson_Mass,
                SpellConstants.StoneShape,
                SpellConstants.AnimalGrowth,
                SpellConstants.Fabricate,
                SpellConstants.OverlandFlight,
                SpellConstants.Passwall,
                SpellConstants.Telekinesis,
                SpellConstants.TransmuteMudToRock,
                SpellConstants.TransmuteRockToMud,
                SpellConstants.BearsEndurance_Mass,
                SpellConstants.BullsStrength_Mass,
                SpellConstants.CatsGrace_Mass,
                SpellConstants.ControlWater,
                SpellConstants.Disintegrate,
                SpellConstants.EaglesSplendor_Mass,
                SpellConstants.FleshToStone,
                SpellConstants.FoxsCunning_Mass,
                SpellConstants.MoveEarth,
                SpellConstants.OwlsWisdom_Mass,
                SpellConstants.StoneToFlesh,
                SpellConstants.Transformation,
                SpellConstants.ControlWeather,
                SpellConstants.EtherealJaunt,
                SpellConstants.ReverseGravity,
                SpellConstants.Statue,
                SpellConstants.IronBody,
                SpellConstants.PolymorphAnyObject,
                SpellConstants.TemporalStasis,
                SpellConstants.Etherealness,
                SpellConstants.Shapechange,
                SpellConstants.TimeStop,
                SpellConstants.MnemonicEnhancer,
                SpellConstants.MagesLucubration
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllTransmutationSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Transmutation]);
        }

        [TestCase(SpellConstants.MageHand, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.Message, 0)]
        [TestCase(SpellConstants.OpenClose, 0)]
        [TestCase(SpellConstants.AnimateRope, 1)]
        [TestCase(SpellConstants.EnlargePerson, 1)]
        [TestCase(SpellConstants.Erase, 1)]
        [TestCase(SpellConstants.ExpeditiousRetreat, 1)]
        [TestCase(SpellConstants.FeatherFall, 1)]
        [TestCase(SpellConstants.Jump, 1)]
        [TestCase(SpellConstants.MagicWeapon, 1)]
        [TestCase(SpellConstants.ReducePerson, 1)]
        [TestCase(SpellConstants.AlterSelf, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.Darkvision, 2)]
        [TestCase(SpellConstants.EaglesSplendor, 2)]
        [TestCase(SpellConstants.FoxsCunning, 2)]
        [TestCase(SpellConstants.Knock, 2)]
        [TestCase(SpellConstants.Levitate, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.Pyrotechnics, 2)]
        [TestCase(SpellConstants.RopeTrick, 2)]
        [TestCase(SpellConstants.SpiderClimb, 2)]
        [TestCase(SpellConstants.WhisperingWind, 2)]
        [TestCase(SpellConstants.Blink, 3)]
        [TestCase(SpellConstants.FlameArrow, 3)]
        [TestCase(SpellConstants.Fly, 3)]
        [TestCase(SpellConstants.GaseousForm, 3)]
        [TestCase(SpellConstants.Haste, 3)]
        [TestCase(SpellConstants.KeenEdge, 3)]
        [TestCase(SpellConstants.MagicWeapon_Greater, 3)]
        [TestCase(SpellConstants.SecretPage, 3)]
        [TestCase(SpellConstants.ShrinkItem, 3)]
        [TestCase(SpellConstants.Slow, 3)]
        [TestCase(SpellConstants.WaterBreathing, 3)]
        [TestCase(SpellConstants.EnlargePerson_Mass, 4)]
        [TestCase(SpellConstants.MnemonicEnhancer, 4)]
        [TestCase(SpellConstants.Polymorph, 4)]
        [TestCase(SpellConstants.ReducePerson_Mass, 4)]
        [TestCase(SpellConstants.StoneShape, 4)]
        [TestCase(SpellConstants.AnimalGrowth, 5)]
        [TestCase(SpellConstants.Fabricate, 5)]
        [TestCase(SpellConstants.OverlandFlight, 5)]
        [TestCase(SpellConstants.Passwall, 5)]
        [TestCase(SpellConstants.Telekinesis, 5)]
        [TestCase(SpellConstants.TransmuteMudToRock, 5)]
        [TestCase(SpellConstants.TransmuteRockToMud, 5)]
        [TestCase(SpellConstants.BearsEndurance_Mass, 6)]
        [TestCase(SpellConstants.BullsStrength_Mass, 6)]
        [TestCase(SpellConstants.CatsGrace_Mass, 6)]
        [TestCase(SpellConstants.ControlWater, 6)]
        [TestCase(SpellConstants.Disintegrate, 6)]
        [TestCase(SpellConstants.EaglesSplendor_Mass, 6)]
        [TestCase(SpellConstants.FleshToStone, 6)]
        [TestCase(SpellConstants.FoxsCunning_Mass, 6)]
        [TestCase(SpellConstants.MagesLucubration, 6)]
        [TestCase(SpellConstants.MoveEarth, 6)]
        [TestCase(SpellConstants.OwlsWisdom_Mass, 6)]
        [TestCase(SpellConstants.StoneToFlesh, 6)]
        [TestCase(SpellConstants.Transformation, 6)]
        [TestCase(SpellConstants.ControlWeather, 7)]
        [TestCase(SpellConstants.EtherealJaunt, 7)]
        [TestCase(SpellConstants.ReverseGravity, 7)]
        [TestCase(SpellConstants.Statue, 7)]
        [TestCase(SpellConstants.IronBody, 8)]
        [TestCase(SpellConstants.PolymorphAnyObject, 8)]
        [TestCase(SpellConstants.TemporalStasis, 8)]
        [TestCase(SpellConstants.Etherealness, 9)]
        [TestCase(SpellConstants.Shapechange, 9)]
        [TestCase(SpellConstants.TimeStop, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
