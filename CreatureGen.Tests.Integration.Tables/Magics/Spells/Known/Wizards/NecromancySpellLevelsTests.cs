using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class NecromancySpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Necromancy);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DisruptUndead,
                SpellConstants.TouchOfFatigue,
                SpellConstants.CauseFear,
                SpellConstants.ChillTouch,
                SpellConstants.RayOfEnfeeblement,
                SpellConstants.BlindnessDeafness,
                SpellConstants.CommandUndead,
                SpellConstants.FalseLife,
                SpellConstants.GhoulTouch,
                SpellConstants.Scare,
                SpellConstants.SpectralHand,
                SpellConstants.GentleRepose,
                SpellConstants.HaltUndead,
                SpellConstants.RayOfExhaustion,
                SpellConstants.VampiricTouch,
                SpellConstants.AnimateDead,
                SpellConstants.BestowCurse,
                SpellConstants.Contagion,
                SpellConstants.Enervation,
                SpellConstants.Fear,
                SpellConstants.Blight,
                SpellConstants.MagicJar,
                SpellConstants.SymbolOfPain,
                SpellConstants.WavesOfFatigue,
                SpellConstants.CircleOfDeath,
                SpellConstants.CreateUndead,
                SpellConstants.Eyebite,
                SpellConstants.SymbolOfFear,
                SpellConstants.UndeathToDeath,
                SpellConstants.ControlUndead,
                SpellConstants.FingerOfDeath,
                SpellConstants.SymbolOfWeakness,
                SpellConstants.WavesOfExhaustion,
                SpellConstants.Clone,
                SpellConstants.CreateGreaterUndead,
                SpellConstants.HorridWilting,
                SpellConstants.SymbolOfDeath,
                SpellConstants.AstralProjection,
                SpellConstants.EnergyDrain,
                SpellConstants.SoulBind,
                SpellConstants.WailOfTheBanshee
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllNecromancySpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Necromancy]);
        }

        [TestCase(SpellConstants.DisruptUndead, 0)]
        [TestCase(SpellConstants.TouchOfFatigue, 0)]
        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.ChillTouch, 1)]
        [TestCase(SpellConstants.RayOfEnfeeblement, 1)]
        [TestCase(SpellConstants.BlindnessDeafness, 2)]
        [TestCase(SpellConstants.CommandUndead, 2)]
        [TestCase(SpellConstants.FalseLife, 2)]
        [TestCase(SpellConstants.GhoulTouch, 2)]
        [TestCase(SpellConstants.Scare, 2)]
        [TestCase(SpellConstants.SpectralHand, 2)]
        [TestCase(SpellConstants.GentleRepose, 3)]
        [TestCase(SpellConstants.HaltUndead, 3)]
        [TestCase(SpellConstants.RayOfExhaustion, 3)]
        [TestCase(SpellConstants.VampiricTouch, 3)]
        [TestCase(SpellConstants.AnimateDead, 4)]
        [TestCase(SpellConstants.BestowCurse, 4)]
        [TestCase(SpellConstants.Contagion, 4)]
        [TestCase(SpellConstants.Enervation, 4)]
        [TestCase(SpellConstants.Fear, 4)]
        [TestCase(SpellConstants.Blight, 5)]
        [TestCase(SpellConstants.MagicJar, 5)]
        [TestCase(SpellConstants.SymbolOfPain, 5)]
        [TestCase(SpellConstants.WavesOfFatigue, 5)]
        [TestCase(SpellConstants.CircleOfDeath, 6)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.Eyebite, 6)]
        [TestCase(SpellConstants.SymbolOfFear, 6)]
        [TestCase(SpellConstants.UndeathToDeath, 6)]
        [TestCase(SpellConstants.ControlUndead, 7)]
        [TestCase(SpellConstants.FingerOfDeath, 7)]
        [TestCase(SpellConstants.SymbolOfWeakness, 7)]
        [TestCase(SpellConstants.WavesOfExhaustion, 7)]
        [TestCase(SpellConstants.Clone, 8)]
        [TestCase(SpellConstants.CreateGreaterUndead, 8)]
        [TestCase(SpellConstants.HorridWilting, 8)]
        [TestCase(SpellConstants.SymbolOfDeath, 8)]
        [TestCase(SpellConstants.AstralProjection, 9)]
        [TestCase(SpellConstants.EnergyDrain, 9)]
        [TestCase(SpellConstants.SoulBind, 9)]
        [TestCase(SpellConstants.WailOfTheBanshee, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
