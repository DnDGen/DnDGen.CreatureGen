using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;
using RollGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    public class SpecialQualityTestData
    {
        public const string None = "NONE";

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var creatures = CreatureConstants.All();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new List<string[]>();
                }

                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.MucusCloud, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveBaseValue: 14));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.HypnoticPattern, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.IllusoryWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.MirageArcana, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.ProgrammedImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Veil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Achaierai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Androsphinx].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UncannyDodge));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyWord, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyMace, requiresEquipment: true));

                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlameStrike, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RaiseDead, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic evil", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Imprisonment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Permanency, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Resurrection, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordBlind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordKill, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 17));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Ankheg].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Ant_Giant_Worker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Ant_Giant_Worker].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Ant_Giant_Soldier].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Ant_Giant_Queen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Ant_Giant_Queen].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Ape].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Ape_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Aranea].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFocus, focus: "Bite"));

                testCases[CreatureConstants.AssassinVine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 30));
                testCases[CreatureConstants.AssassinVine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Camouflage));
                testCases[CreatureConstants.AssassinVine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.AssassinVine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.AssassinVine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

                testCases[CreatureConstants.Athach].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil or silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LayOnHands));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));

                testCases[CreatureConstants.Azer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 13));

                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveSlime, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 14));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Baboon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Badger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Badger].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));
                testCases[CreatureConstants.Badger].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Badger_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Badger_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, cold iron weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FlamingBody));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Insanity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Implosion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Whip, requiresEquipment: true));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.BarbedDefense));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 2 rays only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilitySphere, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BullsStrength_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Basilisk].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                testCases[CreatureConstants.Bat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 20));

                testCases[CreatureConstants.Bat_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 40));

                testCases[CreatureConstants.Bat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 20));

                testCases[CreatureConstants.Bear_Black].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Bear_Brown].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Bear_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Bear_Polar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Glaive, requiresEquipment: true));

                testCases[CreatureConstants.Bebilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Bebilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Bebilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Bebilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Bee_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Behir].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Tripping"));
                testCases[CreatureConstants.Behir].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Behir].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Beholder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Beholder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AntimagicCone));
                testCases[CreatureConstants.Beholder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                testCases[CreatureConstants.Beholder].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));

                testCases[CreatureConstants.Beholder_Gauth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Beholder_Gauth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                testCases[CreatureConstants.Beholder_Gauth].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));

                testCases[CreatureConstants.Belker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SmokeForm));

                testCases[CreatureConstants.Bison].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.BlackPudding].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Split));

                testCases[CreatureConstants.BlackPudding_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Split));

                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Boar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Boar_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.BombardierBeetle_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid or whirlwind form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron or evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictSeriousWounds, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                testCases[CreatureConstants.Bugbear].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Bugbear].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Bugbear].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                testCases[CreatureConstants.Bugbear].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Bugbear].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Bulette].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Bulette].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Camel_Bactrian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Camel_Dromedary].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.CarrionCrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.CarrionCrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));

                testCases[CreatureConstants.Cat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cat].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Centaur].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Centaur].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Centaur].Add(SpecialQualityHelper.BuildData(FeatConstants.MountedCombat));

                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Centipede_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 30));
                testCases[CreatureConstants.Centipede_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.ChainDevil_Kyton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from silver weapons or good-aligned damage", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));

                testCases[CreatureConstants.ChaosBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.ChaosBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Transformation"));
                testCases[CreatureConstants.ChaosBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));

                testCases[CreatureConstants.Cheetah].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cheetah].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Sprint, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));

                testCases[CreatureConstants.Chimera_Black].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Chimera_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Chimera_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Chimera_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Chimera_White].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Choker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Quickness));
                testCases[CreatureConstants.Choker].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Chuul].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.Chuul].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));

                testCases[CreatureConstants.Cloaker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ShadowShift));

                testCases[CreatureConstants.Cockatrice].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 90));
                testCases[CreatureConstants.Couatl].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Criosphinx].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Crocodile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Crocodile_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Cryohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Cryohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Cryohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Cryohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Darkmantle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 90));
                testCases[CreatureConstants.Darkmantle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Deinonychus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Delver].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Delver].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Delver].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Madness));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.LightRepeatingCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.LightRepeatingCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Destrachan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 100));
                testCases[CreatureConstants.Destrachan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                testCases[CreatureConstants.Destrachan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                testCases[CreatureConstants.Destrachan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                testCases[CreatureConstants.Destrachan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));

                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellDeflection));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlUndead, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhoulTouch, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlanarAlly_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RayOfEnfeeblement, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpectralHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Devourer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Digester].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Digester].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.DisplacerBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Displacement));

                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Displacement));

                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": 3 wishes to any non-genie who captures it", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Djinni_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Dog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Dog].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Dog_Riding].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Dog_Riding].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Donkey].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Doppelganger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Doppelganger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Doppelganger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Doppelganger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm effects"));

                testCases[CreatureConstants.DragonTurtle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.DragonTurtle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Black_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));

                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));

                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 21));

                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 24));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 27));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CharmReptiles, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));

                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 20));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 20));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 23));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 23));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 26));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 26));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 29));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 29));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Green_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CommandPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 4, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 5, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 8, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 9, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 10, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 11, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 12, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLocation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));

                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 40 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 50 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));

                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 60 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 70 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 80 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 90 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 100 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 110 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 120 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": one Djinni", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));

                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Dragon_Copper_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MoveEarth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));

                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));

                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 33));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Foresight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));

                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));

                testCases[CreatureConstants.Dragonne].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Scare, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                testCases[CreatureConstants.Drider].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 5, focus: "Vulnerable to cold iron weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TreeDependent));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeShape, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeStride, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                testCases[CreatureConstants.Dryad].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Phantasms"));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Duergar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Dwarf_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Dwarf_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                testCases[CreatureConstants.Dwarf_Mountain].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Dwarf_Mountain].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Mountain].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Mountain].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                testCases[CreatureConstants.Dwarf_Mountain].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                testCases[CreatureConstants.Eagle].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Eagle_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Evasion));

                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProduceFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 1 ray only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 14));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": Grant up to 3 wishes to nongenies", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Efreeti].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Elasmosaurus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));

                testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));

                testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elephant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Gills));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision_Superior));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Net, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 11));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.HandCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ElvenBlood));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MinorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                testCases[CreatureConstants.EtherealFilcher].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.EtherealFilcher].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.EtherealMarauder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Ettercap].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Ettin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, requiresEquipment: true));
                testCases[CreatureConstants.Ettin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                testCases[CreatureConstants.Ettin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Ettin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Ettin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.FireBeetle_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictSeriousWounds + ": 8 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MakeWhole + ": 3 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWorker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianWarrior].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianTaskmaster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianMyrmarch].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));

                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CalmEmotions, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Divination, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOfLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, focus: "Any intelligent creature within 50 miles whose presence she is aware of"));
                testCases[CreatureConstants.FormianQueen].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.FrostWorm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DeathThroes, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveBaseValue: 17));

                testCases[CreatureConstants.Gargoyle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Gargoyle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Freeze));

                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Freeze));

                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid and globe forms", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ColorSpray, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Ghaele].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                testCases[CreatureConstants.Ghoul].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Ghoul_Ghast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Ghoul_Lacedon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate + ": self plus 2,000 pounds", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ObscuringMist, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Cloud].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Giant_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Giant_Frost].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Frost].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Frost].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Giant_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Hill].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Giant_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Giant_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneTell, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Giant_Storm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.GibberingMouther].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amorphous));
                testCases[CreatureConstants.GibberingMouther].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Girallon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 6));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Githyanki].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.MageHand, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 6));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InertialArmor, power: 4));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.FeatherFall, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Githzerai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Shatter, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": for a mortal humanoid. The demon can use this ability to offer a mortal whatever he or she desires - but unless the wish is used to create pain and suffering in the world, the glabrezu demands either terrible evil acts or great sacrifice as compensation.", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Month));

                testCases[CreatureConstants.Gnoll].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Gnoll].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Battleaxe, requiresEquipment: true));
                testCases[CreatureConstants.Gnoll].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Gnoll].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Orc, power: 1));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Reptilian, power: 1));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": on a very basic level with forest animals", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gnome_Forest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Gnome_Rock].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Gnome_Rock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": burrowing mammals only, duration 1 minute", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gnome_Rock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gnome_Rock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.Gnome_Rock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.HeavyPick, requiresEquipment: true));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Stonecunning));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 11));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BlindnessDeafness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Goblin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                testCases[CreatureConstants.Goblin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Goblin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Goblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));

                testCases[CreatureConstants.Golem_Clay].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine, bludgeoning weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Golem_Clay].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste + ": after at least 1 round of combat, lasts 3 rounds", frequencyTimePeriod: FeatConstants.Frequencies.Day, frequencyQuantity: 1));
                testCases[CreatureConstants.Golem_Clay].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                testCases[CreatureConstants.Golem_Flesh].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Golem_Flesh].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                testCases[CreatureConstants.Golem_Iron].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Golem_Iron].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                testCases[CreatureConstants.Golem_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Golem_Stone].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                testCases[CreatureConstants.Golem_Stone_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Golem_Stone_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                testCases[CreatureConstants.Gorgon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                testCases[CreatureConstants.GrayRender].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Grick].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Grick].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Grick].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Griffon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.Dodge, power: 1));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Grig].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.Dodge, power: 1));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Grig_WithFiddle].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 40));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attack forms that rely on sight"));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Grimlock].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReadMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LegendLore, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfInsanity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPersuasion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfSleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfStunning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));

                testCases[CreatureConstants.Halfling_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Halfling_Deep].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Stonecunning));

                testCases[CreatureConstants.Halfling_Lightfoot].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Halfling_Tallfellow].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Harpy].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));

                testCases[CreatureConstants.Hawk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.HellHound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.HellHound].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.HellHound_NessianWarhound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InvisibleInLight));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Hippogriff].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Hobgoblin].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Hobgoblin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Hobgoblin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Hobgoblin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));

                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good-aligned, silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));

                testCases[CreatureConstants.Horse_Heavy].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Horse_Heavy_War].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Horse_Light].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Horse_Light_War].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any canine form of Small to Large size", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HoundArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                testCases[CreatureConstants.Howler].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Human].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Hydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Hyena].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate good damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IceStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));

                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, randomFociQuantity: RollHelper.GetRoll(1, 2), focus: "Imp Alternate Form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.InvisibleStalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.NaturalInvisibility));
                testCases[CreatureConstants.InvisibleStalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tracking_Improved));

                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ElementalEndurance));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.Janni].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Kobold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Kobold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                testCases[CreatureConstants.Kobold].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.Kobold].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Sling, requiresEquipment: true));

                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Kolyarut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateAnimal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Kraken].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Krenshar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Krenshar].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Adhesive));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSight));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Slippery));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.PincerStaff, requiresEquipment: true));
                testCases[CreatureConstants.KuoToa].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));

                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Lamia].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Lammasu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Lammasu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lammasu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.LanternArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.LanternArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic, evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.LanternArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.LanternArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.LanternArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));

                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LayOnHands));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": does not require sound", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictCriticalWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Leonal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

                testCases[CreatureConstants.Leopard].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Knock, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Lillend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Bardic music ability as a 6th-level Bard", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //HACK: Once this is in a core project and incorporates class features as well, we will add it that way

                testCases[CreatureConstants.Lion].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Lion_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Lizard].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Lizard_Monitor].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Lizardfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));
                testCases[CreatureConstants.Lizardfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Lizardfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));
                testCases[CreatureConstants.Lizardfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));

                testCases[CreatureConstants.Locathah].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.Locathah].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Magmin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Magmin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.MeltWeapons, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveBaseValue: 11));

                testCases[CreatureConstants.MantaRay].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Manticore].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Manticore].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AlignWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.MultiweaponFighting, requiresEquipment: true));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AirWalk, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CircleOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 16));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Marut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

                testCases[CreatureConstants.Medusa].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Medusa].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                testCases[CreatureConstants.Megaraptor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to moving air", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In arid, dusty environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Underground or buried up to its waist in earth", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": self only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoftenEarthAndStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HeatMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChillMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching magma, lava, or a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "A pool of lava 3 feet in diameter and 6 inches deep", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In a wet or muddy environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In an arid environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Glitterdust, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Draw moisture from the air", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching boiling water or in a hot, humid area", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Rainstorm of boiling water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 12));

                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to rain or submerged up to its waist in water", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

                testCases[CreatureConstants.Merfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Merfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Merfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Merfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.Merfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Mimic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Mimic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.MimicShape));

                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

                testCases[CreatureConstants.Minotaur].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.NaturalCunning));
                testCases[CreatureConstants.Minotaur].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Minotaur].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));

                testCases[CreatureConstants.Mohrg].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Monkey].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Mule].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Mummy].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mummy].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Naga_Dark].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Naga_Dark].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Any form of mind reading"));
                testCases[CreatureConstants.Naga_Dark].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Naga_Dark].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Naga_Guardian].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Naga_Spirit].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Naga_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Feeblemind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Slow, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));

                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron, magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fear"));
                testCases[CreatureConstants.NightHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));

                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.Nightcrawler].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

                testCases[CreatureConstants.Nightmare].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightmare].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Nightmare_Cauchemar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
                testCases[CreatureConstants.Nightwalker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
                testCases[CreatureConstants.Nightwing].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.Dodge, power: 1));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Nixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Nymph].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nymph].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UnearthlyGrace));
                testCases[CreatureConstants.Nymph].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                testCases[CreatureConstants.Nymph].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Nymph].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Split));

                testCases[CreatureConstants.Octopus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
                testCases[CreatureConstants.Octopus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Octopus_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
                testCases[CreatureConstants.Octopus_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Ogre].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Ogre].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Ogre].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                testCases[CreatureConstants.Ogre].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Ogre].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

                testCases[CreatureConstants.Ogre_Merrow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Ogre_Merrow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Ogre_Merrow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.Ogre_Merrow].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Ogre_Merrow].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.OgreMage].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Falchion, requiresEquipment: true));
                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Orc].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Orc_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.OrcBlood));

                testCases[CreatureConstants.Otyugh].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Owl].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Owl_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision_Superior));

                testCases[CreatureConstants.Owlbear].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Pegasus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pegasus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment + ": within 60-foot radius", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.PhantomFungus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.PhaseSpider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Phasm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Any form Large size or smaller, including Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Phasm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amorphous));
                testCases[CreatureConstants.Phasm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Phasm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Phasm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good spells or effects, or from good-aligned silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateUndead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 19));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Year));

                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.Dodge, power: 1));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Pixie].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.Dodge, power: 1));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IrresistibleDance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                testCases[CreatureConstants.Pony].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Pony_War].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Porpoise].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Porpoise].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.PrayingMantis_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Pseudodragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Pseudodragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Pseudodragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 60));
                testCases[CreatureConstants.Pseudodragon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.PurpleWorm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Pyrohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_5Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_6Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_7Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_8Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_9Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_10Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_11Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Pyrohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Pyrohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Pyrohydra_12Heads].Add(SpecialQualityHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Bat, Small or Medium monstrous centipede, toad, and wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear + ": 30-foot radius area from the quasit", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": can ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.Rakshasa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Rakshasa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, piercing weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Rakshasa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Rakshasa].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

                testCases[CreatureConstants.Rast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));

                testCases[CreatureConstants.Rat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Rat].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Rat_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Rat_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Raven].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Ravid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Ravid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                testCases[CreatureConstants.Ravid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.RazorBoar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.RazorBoar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.RazorBoar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.RazorBoar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                testCases[CreatureConstants.Remorhaz].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Heat));
                testCases[CreatureConstants.Remorhaz].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Rhinoceras].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Roc].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Roper].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Roper].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Roper].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Roper].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.RustMonster].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Salamander_Flamebrother].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));
                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BurningHands, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlamingSphere, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 12));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": Huge fire elemental", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Satyr].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Satyr].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.Satyr].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Satyr].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                testCases[CreatureConstants.Satyr_WithPipes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Satyr_WithPipes].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.Satyr_WithPipes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Satyr_WithPipes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                testCases[CreatureConstants.SeaCat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));
                testCases[CreatureConstants.SeaCat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.SeaHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.SeaHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 14));

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Scorpionfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Scorpionfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Scorpionfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Scorpionfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Scorpionfolk].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Lance, requiresEquipment: true));

                testCases[CreatureConstants.Shadow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Shadow_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.ShadowMastiff].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ShadowBlend));
                testCases[CreatureConstants.ShadowMastiff].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.ShadowMastiff].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.ShamblingMound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.ShamblingMound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.ShamblingMound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Shark_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));

                testCases[CreatureConstants.Shark_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                testCases[CreatureConstants.Shark_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                testCases[CreatureConstants.Shark_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                testCases[CreatureConstants.Shark_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                testCases[CreatureConstants.Shark_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                testCases[CreatureConstants.Shark_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                testCases[CreatureConstants.ShieldGuardian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.ShieldGuardian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FindMaster));
                testCases[CreatureConstants.ShieldGuardian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Guard));
                testCases[CreatureConstants.ShieldGuardian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellStoring));
                testCases[CreatureConstants.ShieldGuardian].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOther + ": within 100 feet of the amulet.  Does not provide spell's AC or save bonuses", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.ShockerLizard].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ElectricitySense));
                testCases[CreatureConstants.ShockerLizard].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

                testCases[CreatureConstants.Shrieker].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Skum].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));

                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Passwall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Medium or Large humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProtectionFromAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));

                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Identify, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Identify, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CircleOfDeath, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 16));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CloakOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WordOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Implosion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 19));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordBlind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Snake_Constrictor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Snake_Viper_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Snake_Viper_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Snake_Viper_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Snake_Viper_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Spectre].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                testCases[CreatureConstants.Spectre].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SunlightPowerlessness));
                testCases[CreatureConstants.Spectre].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.SpiderEater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.SpiderEater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 30));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Squid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
                testCases[CreatureConstants.Squid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Squid_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
                testCases[CreatureConstants.Squid_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.StagBeetle_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Stirge].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Carapace));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "No form of attack deals lethal damage to the tarrasque", power: 40, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Tarrasque].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));

                testCases[CreatureConstants.Tendriculos].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Bludgeoning weapons and acid deal normal damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Thoqqua].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Thoqqua].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Heat));

                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Tiger].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Tiger_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to lawful weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictCriticalWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilityPurge, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WordOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIX, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Gate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Maze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 19));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BestowCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Titan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Toad].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));

                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Treant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to slashing weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Treant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Triceratops].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Triton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIV, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Troglodyte].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));
                testCases[CreatureConstants.Troglodyte].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                testCases[CreatureConstants.Troglodyte].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.Troglodyte].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Troll].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.Troll].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Troll].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Troll_Scrag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.Troll_Scrag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage; only regenerates when immersed in water", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Troll_Scrag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.TrumpetArchon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                testCases[CreatureConstants.Tyrannosaurus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.UmberHulk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": within its forest home", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictModerateWounds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
                testCases[CreatureConstants.Unicorn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Compulsion"));

                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.LightningReflexes, power: 2));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.VampireSpawn].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Vargouille].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.VioletFungus].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heroism, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Wasp_Giant].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Weasel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Weasel].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Weasel_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Weasel_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Whale_Baleen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Baleen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Whale_Cachalot].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Cachalot].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Whale_Orca].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Orca].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Wight].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.WillOWisp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Spells and spell-like effects that allow spell resistance, except Magic Missile and Maze"));
                testCases[CreatureConstants.WillOWisp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.NaturalInvisibility));
                testCases[CreatureConstants.WillOWisp].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.WinterWolf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Wolf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Wolf].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Wolf_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Wolf_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Wolverine].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Wolverine].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Wolverine_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Wolverine_Dire].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Worg].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Wraith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DaylightPowerlessness));
                testCases[CreatureConstants.Wraith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                testCases[CreatureConstants.Wraith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));
                testCases[CreatureConstants.Wraith].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.Wraith].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Lifesense, power: 60));
                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DaylightPowerlessness));
                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));
                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.Wraith_Dread].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Wyvern].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Wyvern].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Xill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Xill].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Planewalk));
                testCases[CreatureConstants.Xill].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Xorn_Minor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Xorn_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.Cleave));

                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                testCases[CreatureConstants.Xorn_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.Cleave));

                testCases[CreatureConstants.YethHound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.YethHound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                testCases[CreatureConstants.YethHound].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));
                testCases[CreatureConstants.Yrthak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Sonic));

                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 14));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.Alertness, power: 2));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.BlindFight, power: 2));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BalefulPolymorph + ": into snake form only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.YuanTi_Abomination].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Geas_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
                testCases[CreatureConstants.Zelekhut].Add(SpecialQualityHelper.BuildData(FeatConstants.MountedCombat));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static class TestDataHelper
        {
            public static IEnumerable EnumerateTestCases(Dictionary<string, List<string[]>> testCases)
            {
                foreach (var testCase in testCases)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }

        public static IEnumerable Types
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var types = CreatureConstants.Types.All();

                foreach (var type in types)
                {
                    testCases[type] = new List<string[]>();
                }

                testCases[CreatureConstants.Types.Aberration].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Aberration].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Aberration].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Animal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Effect that requires a Fortitude save"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death from massive damage"));
                testCases[CreatureConstants.Types.Construct].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Being raised or resurrected"));

                testCases[CreatureConstants.Types.Dragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Dragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Dragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Dragon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Dragon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));

                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Elemental].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Fey].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Fey].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Fey].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Humanoid].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Humanoid].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.MagicalBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.MagicalBeast].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Types.MonstrousHumanoid].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.MonstrousHumanoid].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.MonstrousHumanoid].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 60));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks, visual effects, illusions, and other attack forms that rely on sight"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Types.Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                testCases[CreatureConstants.Types.Outsider].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Outsider].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Outsider].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Outsider].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Types.Plant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));

                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage to Strength, Dexterity, or Constitution"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Effect that requires a Fortitude save"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death from massive damage"));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Undead].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Vermin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Vermin].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable Subtypes
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var subtypes = CreatureConstants.Types.Subtypes.All()
                    .Except(new[]
                    {
                        CreatureConstants.Types.Subtypes.Gnoll,
                        CreatureConstants.Types.Subtypes.Human,
                        CreatureConstants.Types.Subtypes.Orc,
                    }); //INFO: This is duplicated from the creature entry

                foreach (var subtype in subtypes)
                {
                    testCases[subtype] = new List<string[]>();
                }

                testCases[CreatureConstants.Types.Subtypes.Air].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                testCases[CreatureConstants.Types.Subtypes.Angel].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Types.Subtypes.Aquatic].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Types.Subtypes.Augmented].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Chaotic].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Cold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Types.Subtypes.Cold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Stonecunning));
                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Stability));
                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Orc, power: 1));
                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Goblinoid, power: 1));
                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Types.Subtypes.Earth].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Elf].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Types.Subtypes.Elf].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Types.Subtypes.Evil].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Extraplanar].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Types.Subtypes.Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.GnomeHookedHammer, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Kobold, power: 1));
                testCases[CreatureConstants.Types.Subtypes.Gnome].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Goblinoid, power: 1));

                testCases[CreatureConstants.Types.Subtypes.Goblinoid].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Good].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Halfling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: "thrown weapons and slings", power: 1));
                testCases[CreatureConstants.Types.Subtypes.Halfling].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Halfling].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Halfling].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "50% chance to ignore any damage from a corporeal source (except for positive energy, negative energy, force effects such as magic missiles, or attacks made with ghost touch weapons)"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Trip"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Grapple"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Falling or falling damage"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Blindsense));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Blindsight));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Tremorsense));

                testCases[CreatureConstants.Types.Subtypes.Lawful].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Native].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Reptilian].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Shapechanger].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
                testCases[CreatureConstants.Types.Subtypes.Shapechanger].Add(SpecialQualityHelper.BuildData(FeatConstants.ShieldProficiency, requiresEquipment: true));

                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HalfDamage, focus: AttributeConstants.DamageTypes.Piercing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HalfDamage, focus: AttributeConstants.DamageTypes.Slashing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Staggering"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Dying state"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Trip"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Grapple"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Bull Rush"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Any spell that targets a specific number of creatures, including single-target spells"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Area-of-effect spells"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Splash damage"));
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "High winds"));

                testCases[CreatureConstants.Types.Subtypes.Water].Add(SpecialQualityHelper.BuildData(None));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }
    }
}
