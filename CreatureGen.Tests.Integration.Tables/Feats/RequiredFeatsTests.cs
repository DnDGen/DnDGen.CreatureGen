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

        [TestCase(FeatConstants.Acrobatic)]
        [TestCase(FeatConstants.Agile)]
        [TestCase(FeatConstants.Alertness)]
        [TestCase(FeatConstants.AnimalAffinity)]
        [TestCase(FeatConstants.ArmorProficiency_Heavy, FeatConstants.ArmorProficiency_Light, FeatConstants.ArmorProficiency_Medium)]
        [TestCase(FeatConstants.ArmorProficiency_Light)]
        [TestCase(FeatConstants.ArmorProficiency_Medium, FeatConstants.ArmorProficiency_Light)]
        [TestCase(FeatConstants.Athletic)]
        [TestCase(FeatConstants.BlindFight)]
        [TestCase(FeatConstants.BullRush_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Cleave, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Cleave_Great, FeatConstants.Cleave, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.CombatCasting)]
        [TestCase(FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.CombatReflexes)]
        [TestCase(FeatConstants.Counterspell_Improved)]
        [TestCase(FeatConstants.Deceitful)]
        [TestCase(FeatConstants.DeflectArrows, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.DeftHands)]
        [TestCase(FeatConstants.Diehard, FeatConstants.Endurance)]
        [TestCase(FeatConstants.Diligent)]
        [TestCase(FeatConstants.Disarm_Improved, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.Dodge)]
        [TestCase(FeatConstants.Endurance)]
        [TestCase(FeatConstants.EschewMaterials)]
        [TestCase(FeatConstants.FarShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.Feint_Improved, FeatConstants.CombatExpertise)]
        [TestCase(FeatConstants.Grapple_Improved, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.GreatFortitude)]
        [TestCase(FeatConstants.Initiative_Improved)]
        [TestCase(FeatConstants.Investigator)]
        [TestCase(FeatConstants.IronWill)]
        [TestCase(FeatConstants.LightningReflexes)]
        [TestCase(FeatConstants.MagicalAptitude)]
        [TestCase(FeatConstants.Manyshot, FeatConstants.PointBlankShot, FeatConstants.RapidShot)]
        [TestCase(FeatConstants.Mobility, FeatConstants.Dodge)]
        [TestCase(FeatConstants.MountedArchery, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.MountedCombat)]
        //INFO: Wild Shape is only had by Druid classes
        //[TestCase(FeatConstants.NaturalSpell, FeatConstants.WildShape)]
        [TestCase(FeatConstants.Negotiator)]
        [TestCase(FeatConstants.NimbleFingers)]
        [TestCase(FeatConstants.Overrun_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Persuasive)]
        [TestCase(FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.PreciseShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.PreciseShot_Improved, FeatConstants.PointBlankShot, FeatConstants.PreciseShot)]
        [TestCase(FeatConstants.QuickDraw)]
        [TestCase(FeatConstants.RapidShot, FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.RideByAttack, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.Run)]
        [TestCase(FeatConstants.SelfSufficient)]
        [TestCase(FeatConstants.ShieldBash_Improved, FeatConstants.ShieldProficiency)]
        [TestCase(FeatConstants.ShieldProficiency)]
        [TestCase(FeatConstants.ShieldProficiency_Tower, FeatConstants.ShieldProficiency)]
        [TestCase(FeatConstants.ShotOnTheRun,
            FeatConstants.Dodge,
            FeatConstants.Mobility,
            FeatConstants.PointBlankShot)]
        [TestCase(FeatConstants.SkillFocus)]
        [TestCase(FeatConstants.SnatchArrows,
            FeatConstants.DeflectArrows,
            FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.SpellFocus)]
        [TestCase(FeatConstants.SpellFocus_Greater, FeatConstants.SpellFocus)]
        [TestCase(FeatConstants.SpellPenetration)]
        [TestCase(FeatConstants.SpellPenetration_Greater, FeatConstants.SpellPenetration)]
        [TestCase(FeatConstants.SpiritedCharge,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack)]
        [TestCase(FeatConstants.SpringAttack,
            FeatConstants.Dodge,
            FeatConstants.Mobility)]
        [TestCase(FeatConstants.Stealthy)]
        [TestCase(FeatConstants.StunningFist, FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.Sunder_Improved, FeatConstants.PowerAttack)]
        [TestCase(FeatConstants.Toughness)]
        [TestCase(FeatConstants.Track)]
        [TestCase(FeatConstants.Trample, FeatConstants.MountedCombat)]
        [TestCase(FeatConstants.Trip_Improved, FeatConstants.CombatExpertise)]
        //INFO: No monsters can natively turn or rebuke
        //[TestCase(FeatConstants.Turning_Extra, FeatConstants.Turn)]
        //[TestCase(FeatConstants.Turning_Improved, FeatConstants.Turn)]
        [TestCase(FeatConstants.TwoWeaponDefense, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.TwoWeaponFighting_Greater, FeatConstants.TwoWeaponFighting_Improved, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.TwoWeaponFighting_Improved, FeatConstants.TwoWeaponFighting)]
        [TestCase(FeatConstants.UnarmedStrike_Improved)]
        [TestCase(FeatConstants.WeaponFinesse)]
        //INFO: Being a Fighter is a requirement for these feats
        //[TestCase(FeatConstants.WeaponFocus)]
        //[TestCase(FeatConstants.WeaponFocus_Greater, FeatConstants.WeaponFocus)]
        [TestCase(FeatConstants.WeaponProficiency_Exotic)]
        [TestCase(FeatConstants.WeaponProficiency_Martial)]
        [TestCase(FeatConstants.WeaponProficiency_Simple)]
        //INFO: Being a Fighter is a requirement for these feats
        //[TestCase(FeatConstants.WeaponSpecialization)]
        //[TestCase(FeatConstants.WeaponSpecialization_Greater, FeatConstants.WeaponSpecialization)]
        [TestCase(FeatConstants.WhirlwindAttack,
            FeatConstants.CombatExpertise,
            FeatConstants.Dodge,
            FeatConstants.Mobility,
            FeatConstants.SpringAttack)]
        [TestCase(FeatConstants.MagicItemCreation.BrewPotion)]
        [TestCase(FeatConstants.MagicItemCreation.CraftMagicArmsAndArmor)]
        [TestCase(FeatConstants.MagicItemCreation.CraftRod)]
        [TestCase(FeatConstants.MagicItemCreation.CraftStaff)]
        [TestCase(FeatConstants.MagicItemCreation.CraftWand)]
        [TestCase(FeatConstants.MagicItemCreation.CraftWondrousItem)]
        [TestCase(FeatConstants.MagicItemCreation.ForgeRing)]
        [TestCase(FeatConstants.MagicItemCreation.ScribeScroll)]
        [TestCase(FeatConstants.Metamagic.EmpowerSpell)]
        [TestCase(FeatConstants.Metamagic.EnlargeSpell)]
        [TestCase(FeatConstants.Metamagic.ExtendSpell)]
        [TestCase(FeatConstants.Metamagic.HeightenSpell)]
        [TestCase(FeatConstants.Metamagic.MaximizeSpell)]
        [TestCase(FeatConstants.Metamagic.QuickenSpell)]
        [TestCase(FeatConstants.Metamagic.SilentSpell)]
        [TestCase(FeatConstants.Metamagic.StillSpell)]
        [TestCase(FeatConstants.Metamagic.WidenSpell)]
        [TestCase(FeatConstants.Monster.AbilityFocus)]
        [TestCase(FeatConstants.Monster.AwesomeBlow,
            FeatConstants.PowerAttack,
            FeatConstants.BullRush_Improved)]
        [TestCase(FeatConstants.Monster.CraftConstruct,
            FeatConstants.MagicItemCreation.CraftMagicArmsAndArmor,
            FeatConstants.MagicItemCreation.CraftWondrousItem)]
        [TestCase(FeatConstants.Monster.EmpowerSpellLikeAbility)]
        [TestCase(FeatConstants.Monster.FlybyAttack)]
        [TestCase(FeatConstants.Monster.FlybyAttack_Improved,
            FeatConstants.Dodge,
            FeatConstants.Mobility,
            FeatConstants.Monster.FlybyAttack)]
        [TestCase(FeatConstants.Monster.Hover)]
        [TestCase(FeatConstants.Monster.Multiattack)]
        [TestCase(FeatConstants.Monster.Multiattack_Improved,
            FeatConstants.Monster.Multiattack)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Greater,
            FeatConstants.Monster.MultiweaponFighting,
            FeatConstants.Monster.MultiweaponFighting_Improved)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Improved, FeatConstants.Monster.MultiweaponFighting)]
        [TestCase(FeatConstants.Monster.NaturalArmor_Improved)]
        [TestCase(FeatConstants.Monster.NaturalAttack_Improved)]
        [TestCase(FeatConstants.Monster.QuickenSpellLikeAbility)]
        [TestCase(FeatConstants.Monster.Snatch)]
        [TestCase(FeatConstants.Monster.Wingover)]
        public void RequiredFeats(string name, params string[] requiredFeats)
        {
            DistinctCollection(name, requiredFeats);
        }

        [TestCase(FeatConstants.AugmentSummoning,
            FeatConstants.SpellFocus, FeatConstants.Foci.Schools.Conjuration)]
        //[TestCase(FeatConstants.CorruptingGaze,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingGaze)]
        //[TestCase(FeatConstants.CorruptingTouch,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingTouch)]
        //[TestCase(FeatConstants.DrainingTouch,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.DrainingTouch)]
        //[TestCase(FeatConstants.FrightfulMoan,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.FrightfulMoan)]
        //[TestCase(FeatConstants.HorrificAppearance,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.HorrificAppearance)]
        //[TestCase(FeatConstants.Malevolence,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.Malevolence)]
        //[TestCase(FeatConstants.Telekinesis,
        //    FeatConstants.GhostSpecialAttack, FeatConstants.Telekinesis)]
        public void RequiredFeat(string name, string requiredFeat, string requiredFocus)
        {
            var collection = new[] { $"{requiredFeat}/{requiredFocus}" };
            DistinctCollection(name, collection);
        }
    }
}
