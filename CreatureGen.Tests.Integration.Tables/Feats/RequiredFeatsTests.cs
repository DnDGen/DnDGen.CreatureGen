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
                FeatConstants.Turning_Extra,
                FeatConstants.FarShot,
                FeatConstants.Cleave_Great,
                FeatConstants.SpellFocus_Greater,
                FeatConstants.SpellPenetration_Greater,
                FeatConstants.TwoWeaponFighting_Greater,
                FeatConstants.WeaponFocus_Greater,
                FeatConstants.WeaponSpecialization_Greater,
                FeatConstants.BullRush_Improved,
                FeatConstants.Critical_Improved,
                FeatConstants.Disarm_Improved,
                FeatConstants.Feint_Improved,
                FeatConstants.Grapple_Improved,
                FeatConstants.ImprovedOverrun,
                FeatConstants.PreciseShot_Improved,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.Sunder_Improved,
                FeatConstants.Trip_Improved,
                FeatConstants.Turning_Improved,
                FeatConstants.TwoWeaponFighting_Improved,
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
        [TestCase(FeatConstants.DeflectArrows, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.Diehard, FeatConstants.Endurance)]
        [TestCase(FeatConstants.Turning_Extra, FeatConstants.Turn)]
        [TestCase(FeatConstants.FarShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Cleave_Great, FeatConstants.Cleave)]
        [TestCase(FeatConstants.SpellFocus_Greater, FeatConstants.SpellFocus)]
        [TestCase(FeatConstants.SpellPenetration_Greater, FeatConstants.SpellPenetration)]
        [TestCase(FeatConstants.TwoWeaponFighting_Greater, FeatConstants.TwoWeaponFighting_Improved)]
        [TestCase(FeatConstants.WeaponFocus_Greater, FeatConstants.WeaponFocus)]
        [TestCase(FeatConstants.WeaponSpecialization_Greater, FeatConstants.WeaponFocus_Greater, FeatConstants.WeaponSpecialization)]
        [TestCase(FeatConstants.BullRush_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Disarm_Improved, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.Feint_Improved, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.Grapple_Improved, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.ImprovedOverrun, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.PreciseShot_Improved, FeatConstants.PreciseShot)]
        [TestCase(FeatConstants.PreciseShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Sunder_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Trip_Improved, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.Turning_Improved, FeatConstants.Turn)]
        [TestCase(FeatConstants.TwoWeaponFighting_Improved, FeatConstants.TwoWeaponFighting)]
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
            FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.SpiritedCharge,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack)]
        [TestCase(FeatConstants.SpringAttack,
            FeatConstants.Dodge,
            FeatConstants.Mobility)]
        [TestCase(FeatConstants.StunningFist, FeatConstants.UnarmedStrike_Improved)]
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
