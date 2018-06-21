using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class FeatDataTests : DataTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Collection.FeatData; }
        }

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = "Base Attack Requirement";
            indices[DataIndexConstants.FeatData.FocusTypeIndex] = "Focus Type";
            indices[DataIndexConstants.FeatData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = "Minimum Caster Level";
            indices[DataIndexConstants.FeatData.PowerIndex] = "Power";
            indices[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = "Hands";
            indices[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = "Natural Weapons";
            indices[DataIndexConstants.FeatData.RequiresNaturalArmor] = "Natural Armor";
            indices[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = "Special Attack";
            indices[DataIndexConstants.FeatData.RequiresSpellLikeAbility] = "Spell-Like Ability";
        }

        [Test]
        public void CollectionNames()
        {
            var feats = FeatConstants.All();
            var monsterFeats = FeatConstants.Monster.All();
            var craftFeats = FeatConstants.MagicItemCreation.All();
            var metamagicFeats = FeatConstants.Metamagic.All();

            var names = feats.Union(monsterFeats).Union(craftFeats).Union(metamagicFeats);

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Acrobatic, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Agile, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Alertness, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.AnimalAffinity, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.ArmorProficiency_Heavy, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.ArmorProficiency_Light, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.ArmorProficiency_Medium, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Athletic, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.AugmentSummoning, 0, "", 0, "", 4, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.BlindFight, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.BullRush_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Cleave, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Cleave_Great, 4, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.CombatCasting, 0, "", 0, "", 4, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.CombatExpertise, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.CombatReflexes, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Counterspell_Improved, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Critical_Improved, 8, FeatConstants.Foci.Weapon, 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Deceitful, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.DeflectArrows, 0, "", 1, FeatConstants.Frequencies.Round, 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.DeftHands, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Diehard, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Diligent, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Disarm_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Dodge, 0, "", 0, "", 1, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Endurance, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.EschewMaterials, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        //INFO: Creatures cannot get familiars without being a character class
        //[TestCase(FeatConstants.Familiar_Improved, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.FarShot, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Feint_Improved, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Grapple_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.GreatFortitude, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Initiative_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Investigator, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.IronWill, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        //INFO: Requires character level 6
        //[TestCase(FeatConstants.Leadership, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.LightningReflexes, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicalAptitude, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Manyshot, 6, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Mobility, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MountedArchery, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MountedCombat, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        //INFO: Wild Shape, a prerequisite, is only had by Druid classes
        //[TestCase(FeatConstants.NaturalSpell, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Negotiator, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.NimbleFingers, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Overrun_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Persuasive, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.PointBlankShot, 0, "", 0, "", 1, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.PowerAttack, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.PreciseShot, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.PreciseShot_Improved, 11, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.QuickDraw, 1, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.RapidReload, 0, GroupConstants.ManualCrossbows, 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.RapidShot, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.RideByAttack, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Run, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SelfSufficient, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.ShotOnTheRun, 4, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SkillFocus, 0, GroupConstants.Skills, 0, "", 3, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SnatchArrows, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpellFocus, 0, FeatConstants.Foci.School, 0, "", 1, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpellFocus_Greater, 0, FeatConstants.Foci.School, 0, "", 1, 1, 0, 0, false, false, false)]
        //INFO: Spell Mastery requires being a Wizard Level 1
        //[TestCase(FeatConstants.SpellMastery, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpellPenetration, 0, "", 0, "", 2, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpellPenetration_Greater, 0, "", 0, "", 2, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpiritedCharge, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.SpringAttack, 4, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Stealthy, 0, "", 0, "", 2, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.StunningFist, 8, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Sunder_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Toughness, 0, "", 0, "", 3, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Track, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Trample, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Trip_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        //INFO: No monsters can natively turn or rebuke
        //[TestCase(FeatConstants.Turning_Extra, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        //[TestCase(FeatConstants.Turning_Improved, 0, "", 0, "", 4, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.TwoWeaponFighting_Greater, 11, "", 0, "", 0, 0, 2, 0, false, false, false)]
        [TestCase(FeatConstants.TwoWeaponFighting_Improved, 6, "", 0, "", 0, 0, 2, 0, false, false, false)]
        [TestCase(FeatConstants.TwoWeaponDefense, 0, "", 0, "", 0, 0, 2, 0, false, false, false)]
        [TestCase(FeatConstants.TwoWeaponFighting, 0, "", 0, "", 0, 0, 2, 0, false, false, false)]
        [TestCase(FeatConstants.UnarmedStrike_Improved, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.WeaponFinesse, 1, "", 0, "", 0, 0, 0, 0, false, false, false)]
        //INFO: Being a Fighter is a requirement for these feats
        //[TestCase(FeatConstants.WeaponFocus, 0, FeatConstants.Foci.Weapon, 0, "", 0, 0, 0, 0, false, false, false)]
        //[TestCase(FeatConstants.WeaponFocus_Greater, 0, FeatConstants.Foci.Weapon, 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.WeaponProficiency_Exotic, 1, FeatConstants.Foci.All, 0, "", 0, 0, 1, 0, false, false, false)]
        [TestCase(FeatConstants.WeaponProficiency_Martial, 0, FeatConstants.Foci.All, 0, "", 0, 0, 1, 0, false, false, false)]
        [TestCase(FeatConstants.WeaponProficiency_Simple, 0, FeatConstants.Foci.All, 0, "", 0, 0, 1, 0, false, false, false)]
        //INFO: Being a Fighter is a requirement for these feats
        //[TestCase(FeatConstants.WeaponSpecialization, 0, FeatConstants.Foci.Weapon, 0, "", 0, 0, 0, 0, false, false, false)]
        //[TestCase(FeatConstants.WeaponSpecialization_Greater, 0, FeatConstants.Foci.Weapon, 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.WhirlwindAttack, 4, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.BrewPotion, 0, "", 0, "", 0, 3, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.CraftMagicArmsAndArmor, 0, "", 0, "", 0, 5, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.CraftRod, 0, "", 0, "", 0, 9, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.CraftStaff, 0, "", 0, "", 0, 12, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.CraftWand, 0, "", 0, "", 0, 5, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.CraftWondrousItem, 0, "", 0, "", 0, 3, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.ForgeRing, 0, "", 0, "", 0, 12, 0, 0, false, false, false)]
        [TestCase(FeatConstants.MagicItemCreation.ScribeScroll, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.EmpowerSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.EnlargeSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.ExtendSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.HeightenSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.MaximizeSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.QuickenSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.SilentSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.StillSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Metamagic.WidenSpell, 0, "", 0, "", 0, 1, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.AbilityFocus, 0, "", 0, "", 2, 0, 0, 0, false, true, false)]
        [TestCase(FeatConstants.Monster.AwesomeBlow, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.CraftConstruct, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.EmpowerSpellLikeAbility, 0, "", 3, FeatConstants.Frequencies.Day, 0, 6, 0, 0, false, false, true)]
        [TestCase(FeatConstants.Monster.FlybyAttack, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.FlybyAttack_Improved, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.Hover, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.Multiattack, 0, "", 0, "", 0, 0, 0, 3, false, false, false)]
        [TestCase(FeatConstants.Monster.Multiattack_Improved, 0, "", 0, "", 0, 0, 0, 3, false, false, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting, 0, "", 0, "", 0, 0, 3, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Greater, 15, "", 0, "", 0, 0, 3, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.MultiweaponFighting_Improved, 9, "", 0, "", 0, 0, 3, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.NaturalArmor_Improved, 0, "", 0, "", 1, 0, 0, 0, true, false, false)]
        [TestCase(FeatConstants.Monster.NaturalAttack_Improved, 4, "", 0, "", 0, 0, 0, 1, false, false, false)]
        [TestCase(FeatConstants.Monster.QuickenSpellLikeAbility, 0, "", 3, FeatConstants.Frequencies.Day, 0, 10, 0, 0, false, false, true)]
        [TestCase(FeatConstants.Monster.Snatch, 0, "", 0, "", 0, 0, 0, 0, false, false, false)]
        [TestCase(FeatConstants.Monster.Wingover, 0, "", 1, FeatConstants.Frequencies.Round, 0, 0, 0, 0, false, false, false)]
        public void FeatData(
            string name,
            int baseAttackRequirement,
            string focusType,
            int frequencyQuantity,
            string frequencyTimePeriod,
            int power,
            int casterLevel,
            int hands,
            int naturalWeapons,
            bool naturalArmor,
            bool specialAttacks,
            bool spellLikeAbility)
        {
            var data = DataIndexConstants.FeatData.InitializeData();

            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = Convert.ToString(baseAttackRequirement);
            data[DataIndexConstants.FeatData.FocusTypeIndex] = focusType;
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = Convert.ToString(casterLevel);
            data[DataIndexConstants.FeatData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = Convert.ToString(hands);
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = Convert.ToString(naturalWeapons);
            data[DataIndexConstants.FeatData.RequiresNaturalArmor] = Convert.ToString(naturalArmor);
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = Convert.ToString(specialAttacks);
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbility] = Convert.ToString(spellLikeAbility);

            Data(name, data);
        }
    }
}