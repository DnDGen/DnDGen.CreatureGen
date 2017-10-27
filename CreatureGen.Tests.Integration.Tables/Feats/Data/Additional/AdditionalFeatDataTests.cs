using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Additional
{
    [TestFixture]
    public class AdditionalFeatDataTests : DataTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.AdditionalFeatData; }
        }

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = "Base Attack Requirement";
            indices[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = "Focus Type";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.AdditionalFeatData.PowerIndex] = "Power";
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Acrobatic,
                FeatConstants.Agile,
                FeatConstants.Alertness,
                FeatConstants.AnimalAffinity,
                FeatConstants.Athletic,
                FeatConstants.AugmentSummoning,
                FeatConstants.BlindFight,
                FeatConstants.Cleave,
                FeatConstants.CombatCasting,
                FeatConstants.CombatExpertise,
                FeatConstants.CombatReflexes,
                FeatConstants.CripplingStrike,
                FeatConstants.Deceitful,
                FeatConstants.DefensiveRoll,
                FeatConstants.DeflectArrows,
                FeatConstants.DeftHands,
                FeatConstants.Diehard,
                FeatConstants.Diligent,
                FeatConstants.Dodge,
                FeatConstants.EmpowerSpell,
                FeatConstants.Endurance,
                FeatConstants.EnlargeSpell,
                FeatConstants.EschewMaterials,
                FeatConstants.ExoticWeaponProficiency,
                FeatConstants.ExtendSpell,
                FeatConstants.ExtraTurning,
                FeatConstants.FarShot,
                FeatConstants.GreatCleave,
                FeatConstants.GreaterSpellFocus,
                FeatConstants.GreaterSpellPenetration,
                FeatConstants.GreaterTwoWeaponFighting,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization,
                FeatConstants.GreatFortitude,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.HeightenSpell,
                FeatConstants.ImprovedBullRush,
                FeatConstants.ImprovedCounterspell,
                FeatConstants.ImprovedCritical,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedEvasion,
                FeatConstants.ImprovedFamiliar,
                FeatConstants.ImprovedFeint,
                FeatConstants.ImprovedGrapple,
                FeatConstants.ImprovedInitiative,
                FeatConstants.ImprovedOverrun,
                FeatConstants.ImprovedPreciseShot,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.ImprovedSunder,
                FeatConstants.ImprovedTrip,
                FeatConstants.ImprovedTurning,
                FeatConstants.ImprovedTwoWeaponFighting,
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.Investigator,
                FeatConstants.IronWill,
                FeatConstants.Leadership,
                FeatConstants.LightArmorProficiency,
                FeatConstants.LightningReflexes,
                FeatConstants.MagicalAptitude,
                FeatConstants.Manyshot,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.MaximizeSpell,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.Mobility,
                FeatConstants.MountedArchery,
                FeatConstants.MountedCombat,
                FeatConstants.NaturalSpell,
                FeatConstants.Negotiator,
                FeatConstants.NimbleFingers,
                FeatConstants.Opportunist,
                FeatConstants.Persuasive,
                FeatConstants.PointBlankShot,
                FeatConstants.PowerAttack,
                FeatConstants.PreciseShot,
                FeatConstants.QuickDraw,
                FeatConstants.QuickenSpell,
                FeatConstants.RapidReload,
                FeatConstants.RapidShot,
                FeatConstants.RideByAttack,
                FeatConstants.Run,
                FeatConstants.ScribeScroll,
                FeatConstants.SelfSufficient,
                FeatConstants.ShieldProficiency,
                FeatConstants.ShotOnTheRun,
                FeatConstants.SilentSpell,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.SkillFocus,
                FeatConstants.SkillMastery,
                FeatConstants.SlipperyMind,
                FeatConstants.SnatchArrows,
                FeatConstants.SpellFocus,
                FeatConstants.SpellMastery,
                FeatConstants.SpellPenetration,
                FeatConstants.SpiritedCharge,
                FeatConstants.SpringAttack,
                FeatConstants.Stealthy,
                FeatConstants.StillSpell,
                FeatConstants.StunningFist,
                FeatConstants.Toughness,
                FeatConstants.TowerShieldProficiency,
                FeatConstants.Track,
                FeatConstants.Trample,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.TwoWeaponFighting,
                FeatConstants.WeaponFocus,
                FeatConstants.WeaponFinesse,
                FeatConstants.WeaponSpecialization,
                FeatConstants.WhirlwindAttack,
                FeatConstants.WidenSpell
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Acrobatic, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Agile, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Alertness, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AnimalAffinity, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.HeavyArmorProficiency, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.MediumArmorProficiency, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.LightArmorProficiency, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Athletic, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AugmentSummoning, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.BlindFight, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Cleave, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.CombatCasting, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.CombatExpertise, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.CombatReflexes, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.CripplingStrike, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Deceitful, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.DefensiveRoll, 0, "", 1, FeatConstants.Frequencies.Day, 0)]
        [TestCase(FeatConstants.DeflectArrows, 0, "", 1, FeatConstants.Frequencies.Round, 0)]
        [TestCase(FeatConstants.DeftHands, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Diehard, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Diligent, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Dodge, 0, "", 0, "", 1)]
        [TestCase(FeatConstants.EmpowerSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Endurance, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.EnlargeSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.EschewMaterials, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ExoticWeaponProficiency, 1, FeatConstants.Foci.All, 0, "", 0)]
        [TestCase(FeatConstants.ExtendSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ExtraTurning, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.FarShot, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.GreatCleave, 4, "", 0, "", 0)]
        [TestCase(FeatConstants.GreaterSpellFocus, 0, GroupConstants.SchoolsOfMagic, 0, "", 1)]
        [TestCase(FeatConstants.GreaterSpellPenetration, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.GreaterTwoWeaponFighting, 11, "", 0, "", 0)]
        [TestCase(FeatConstants.GreaterWeaponFocus, 0, FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, 0, "", 1)]
        [TestCase(FeatConstants.GreaterWeaponSpecialization, 0, FeatConstants.Foci.WeaponsWithUnarmedAndGrapple, 0, "", 2)]
        [TestCase(FeatConstants.GreatFortitude, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.HeightenSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedBullRush, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedCounterspell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedCritical, 8, FeatConstants.Foci.WeaponsWithUnarmed, 0, "", 0)]
        [TestCase(FeatConstants.ImprovedDisarm, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedEvasion, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedFamiliar, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedFeint, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedGrapple, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedInitiative, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedOverrun, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedPreciseShot, 11, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedShieldBash, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedSunder, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTrip, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTurning, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTwoWeaponFighting, 6, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedUnarmedStrike, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Investigator, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.IronWill, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Leadership, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.LightningReflexes, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.MagicalAptitude, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Manyshot, 6, "", 0, "", 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency, 0, FeatConstants.Foci.All, 0, "", 0)]
        [TestCase(FeatConstants.MaximizeSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Mobility, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.MountedArchery, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.MountedCombat, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.NaturalSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Negotiator, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.NimbleFingers, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.Opportunist, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Persuasive, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.PointBlankShot, 0, "", 0, "", 1)]
        [TestCase(FeatConstants.PowerAttack, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.PreciseShot, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.QuickDraw, 1, "", 0, "", 0)]
        [TestCase(FeatConstants.QuickenSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.RapidReload, 0, GroupConstants.ManualCrossbows, 0, "", 0)]
        [TestCase(FeatConstants.RapidShot, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.RideByAttack, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Run, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ScribeScroll, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SelfSufficient, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.ShieldProficiency, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ShotOnTheRun, 4, "", 0, "", 0)]
        [TestCase(FeatConstants.SilentSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency, 0, FeatConstants.Foci.All, 0, "", 0)]
        [TestCase(FeatConstants.SkillFocus, 0, GroupConstants.Skills, 0, "", 3)]
        [TestCase(FeatConstants.SkillMastery, 0, GroupConstants.Skills, 0, "", 3)]
        [TestCase(FeatConstants.SlipperyMind, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SnatchArrows, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SpellFocus, 0, GroupConstants.SchoolsOfMagic, 0, "", 1)]
        [TestCase(FeatConstants.SpellMastery, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SpellPenetration, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.SpiritedCharge, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.SpringAttack, 4, "", 0, "", 0)]
        [TestCase(FeatConstants.Stealthy, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.StillSpell, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.StunningFist, 8, "", 0, "", 0)]
        [TestCase(FeatConstants.Toughness, 0, "", 0, "", 3)]
        [TestCase(FeatConstants.TowerShieldProficiency, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Track, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.Trample, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.TwoWeaponDefense, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.TwoWeaponFighting, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.WeaponFocus, 1, FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, 0, "", 1)]
        [TestCase(FeatConstants.WeaponFinesse, 1, "", 0, "", 0)]
        [TestCase(FeatConstants.WeaponSpecialization, 0, FeatConstants.Foci.WeaponsWithUnarmedAndGrapple, 0, "", 2)]
        [TestCase(FeatConstants.WhirlwindAttack, 4, "", 0, "", 0)]
        [TestCase(FeatConstants.WidenSpell, 0, "", 0, "", 0)]
        public void AdditionalFeatData(string name, int baseAttackRequirement, string focusType, int frequencyQuantity, string frequencyTimePeriod, int power)
        {
            var data = new List<string>();
            for (var i = 0; i < 5; i++)
                data.Add(string.Empty);

            data[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = Convert.ToString(baseAttackRequirement);
            data[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = focusType;
            data[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.AdditionalFeatData.PowerIndex] = Convert.ToString(power);

            Data(name, data);
        }
    }
}