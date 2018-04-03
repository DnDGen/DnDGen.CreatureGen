using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

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
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();

            var names = feats.Union(metamagic).Union(monster).Union(craft);

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Cleave, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.DeflectArrows, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.Diehard, FeatConstants.Endurance)]
        //[TestCase(FeatConstants.Turning_Extra, FeatConstants.Turn)]
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
        [TestCase(FeatConstants.Overrun_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.PreciseShot_Improved, FeatConstants.PreciseShot)]
        [TestCase(FeatConstants.PreciseShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Sunder_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Trip_Improved, FeatConstants.CombatExpertise)]
        //[TestCase(FeatConstants.Turning_Improved, FeatConstants.Turn)]
        [TestCase(FeatConstants.TwoWeaponFighting_Improved, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.Manyshot, FeatConstants.RapidShot)]
        [TestCase(FeatConstants.RapidShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Mobility, FeatConstants.Dodge)]
        [TestCase(FeatConstants.MountedArchery, FeatConstants.MountedCombat)]
        //[TestCase(FeatConstants.NaturalSpell, FeatConstants.WildShape)]
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
    }
}
