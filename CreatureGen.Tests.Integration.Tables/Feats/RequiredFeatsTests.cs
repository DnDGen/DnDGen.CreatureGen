using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats
{
    [TestFixture]
    public class RequiredFeatsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.RequiredFeats; }
        }

        [Test]
        public void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.AugmentSummoning,
                FeatConstants.Cleave,
                FeatConstants.DeflectArrows,
                FeatConstants.Diehard,
                FeatConstants.ExtraTurning,
                FeatConstants.FarShot,
                FeatConstants.GreatCleave,
                FeatConstants.GreaterSpellFocus,
                FeatConstants.GreaterSpellPenetration,
                FeatConstants.GreaterTwoWeaponFighting,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization,
                FeatConstants.ImprovedBullRush,
                FeatConstants.ImprovedCritical,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedFeint,
                FeatConstants.ImprovedGrapple,
                FeatConstants.ImprovedOverrun,
                FeatConstants.ImprovedPreciseShot,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.ImprovedSunder,
                FeatConstants.ImprovedTrip,
                FeatConstants.ImprovedTurning,
                FeatConstants.ImprovedTwoWeaponFighting,
                FeatConstants.Manyshot,
                FeatConstants.MountedArchery,
                FeatConstants.Mobility,
                FeatConstants.NaturalSpell,
                FeatConstants.PreciseShot,
                FeatConstants.RapidReload,
                FeatConstants.RapidShot,
                FeatConstants.RideByAttack,
                FeatConstants.ShotOnTheRun,
                FeatConstants.SnatchArrows,
                FeatConstants.SpiritedCharge,
                FeatConstants.SpringAttack,
                FeatConstants.StunningFist,
                FeatConstants.Trample,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.WhirlwindAttack,
                FeatConstants.WeaponFocus,
                FeatConstants.WeaponSpecialization,
                FeatConstants.CorruptingGaze,
                FeatConstants.CorruptingTouch,
                FeatConstants.DrainingTouch,
                FeatConstants.FrightfulMoan,
                FeatConstants.HorrificAppearance,
                FeatConstants.Malevolence,
                FeatConstants.Telekinesis,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Cleave, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.DeflectArrows, FeatConstants.ImprovedUnarmedStrike)]
        [TestCase(FeatConstants.Diehard, FeatConstants.Endurance)]
        [TestCase(FeatConstants.ExtraTurning, FeatConstants.Turn)]
        [TestCase(FeatConstants.FarShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.GreatCleave, FeatConstants.Cleave)]
        [TestCase(FeatConstants.GreaterSpellFocus, FeatConstants.SpellFocus)]
        [TestCase(FeatConstants.GreaterSpellPenetration, FeatConstants.SpellPenetration)]
        [TestCase(FeatConstants.GreaterTwoWeaponFighting, FeatConstants.ImprovedTwoWeaponFighting)]
        [TestCase(FeatConstants.GreaterWeaponFocus, FeatConstants.WeaponFocus)]
        [TestCase(FeatConstants.GreaterWeaponSpecialization, FeatConstants.GreaterWeaponFocus, FeatConstants.WeaponSpecialization)]
        [TestCase(FeatConstants.ImprovedBullRush, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.ImprovedDisarm, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.ImprovedFeint, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.ImprovedGrapple, FeatConstants.ImprovedUnarmedStrike)]
        [TestCase(FeatConstants.ImprovedOverrun, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.ImprovedPreciseShot, FeatConstants.PreciseShot)]
        [TestCase(FeatConstants.PreciseShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.ImprovedSunder, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.ImprovedTrip, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.ImprovedTurning, FeatConstants.Turn)]
        [TestCase(FeatConstants.ImprovedTwoWeaponFighting, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.Manyshot, FeatConstants.RapidShot)]
        [TestCase(FeatConstants.RapidShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Mobility, FeatConstants.Dodge)]
        [TestCase(FeatConstants.MountedArchery, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.NaturalSpell, FeatConstants.WildShape)]
        [TestCase(FeatConstants.RideByAttack, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.ShotOnTheRun,
            FeatConstants.Dodge,
            FeatConstants.Mobility,
            FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.SnatchArrows,
            FeatConstants.DeflectArrows,
            FeatConstants.ImprovedUnarmedStrike)]
        [TestCase(FeatConstants.SpiritedCharge,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack)]
        [TestCase(FeatConstants.SpringAttack,
            FeatConstants.Dodge,
            FeatConstants.Mobility)]
        [TestCase(FeatConstants.StunningFist, FeatConstants.ImprovedUnarmedStrike)]
        [TestCase(FeatConstants.Trample, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.TwoWeaponDefense, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.WeaponSpecialization, FeatConstants.WeaponFocus)]
        [TestCase(FeatConstants.WhirlwindAttack,
            FeatConstants.CombatExpertise,
            FeatConstants.Dodge,
            FeatConstants.Mobility,
            FeatConstants.SpringAttack)]
        public void RequiredFeats(string name, params string[] requiredFeats)
        {
            DistinctCollection(name, requiredFeats);
        }

        [TestCase(FeatConstants.CorruptingGaze, FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingGaze)]
        [TestCase(FeatConstants.CorruptingTouch, FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingTouch)]
        [TestCase(FeatConstants.DrainingTouch, FeatConstants.GhostSpecialAttack, FeatConstants.DrainingTouch)]
        [TestCase(FeatConstants.FrightfulMoan, FeatConstants.GhostSpecialAttack, FeatConstants.FrightfulMoan)]
        [TestCase(FeatConstants.HorrificAppearance, FeatConstants.GhostSpecialAttack, FeatConstants.HorrificAppearance)]
        [TestCase(FeatConstants.Malevolence, FeatConstants.GhostSpecialAttack, FeatConstants.Malevolence)]
        [TestCase(FeatConstants.Telekinesis, FeatConstants.GhostSpecialAttack, FeatConstants.Telekinesis)]
        public void RequiredFeat(string name, string requiredFeat, string requiredFocus)
        {
            var collection = new[] { $"{requiredFeat}/{requiredFocus}" };
            DistinctCollection(name, collection);
        }
    }
}
