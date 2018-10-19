using CreatureGen.Creatures;
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

                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.MucusCloud));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HypnoticPattern, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IllusoryWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProgrammedImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Achaierai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                testCases[CreatureConstants.Androsphinx].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid"));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UncannyDodge));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyWord, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyMace, requiresEquipment: true));

                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid"));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlameStrike, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RaiseDead, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid"));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic evil", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Imprisonment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Permanency, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Resurrection, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordBlind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordKill, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Ankheg].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Annis].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Ape].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Aranea].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)"));

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
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Avoral].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));

                testCases[CreatureConstants.Azer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Azer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Azer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 13));

                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Babau].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveSlime));
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

                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good and cold iron weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FlamingBody));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Insanity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Implosion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Balor].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Whip, requiresEquipment: true));

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
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 2 rays only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf"));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf"));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilitySphere, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BullsStrength_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Barghest_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Basilisk].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                testCases[CreatureConstants.Bat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 20));

                testCases[CreatureConstants.Bat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 20));
                testCases[CreatureConstants.Bat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
                testCases[CreatureConstants.Bat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Bat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                testCases[CreatureConstants.Bear_Black].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

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

                testCases[CreatureConstants.BlackPudding_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 60));
                testCases[CreatureConstants.BlackPudding_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Split));

                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.BlinkDog].Add(SpecialQualityHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Boar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Bodak].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

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
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
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
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Bralani].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictSeriousWounds, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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

                testCases[CreatureConstants.Cat].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Centaur].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Centaur].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

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

                testCases[CreatureConstants.Chimera].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Choker].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Quickness));
                testCases[CreatureConstants.Choker].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                testCases[CreatureConstants.Chuul].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.Chuul].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));

                testCases[CreatureConstants.Criosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Criosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Madness));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Derro].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Derro_Sane].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

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
                testCases[CreatureConstants.Dragon_Black_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

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
                testCases[CreatureConstants.Dragon_Green_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CommandPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 4, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 5, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 8, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Red_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 9, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 10, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 11, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 12, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLocation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));

                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Dragon_White_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 40 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 50 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));

                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 60 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 70 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 80 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 90 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 100 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 110 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 120 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

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
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

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
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

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
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

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
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
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
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MoveEarth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
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
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
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
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 33));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Foresight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));

                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
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
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
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
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Scare, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Dretch].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

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

                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Gills));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision_Superior));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Net, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Aquatic].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 11));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.HandCrossbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Drow].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ElvenBlood));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Half].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_High].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wild].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));
                testCases[CreatureConstants.Elf_Wood].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MinorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                testCases[CreatureConstants.Erinyes].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 60));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.GelatinousCube].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Glabrezu].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": for a mortal humanoid. The demon can use this ability to offer a mortal whatever he or she desires - but unless the wish is used to create pain and suffering in the world, the glabrezu demands either terrible evil acts or great sacrifice as compensation.", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Month));

                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 60));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.GrayOoze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.GreenHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReadMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LegendLore, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfInsanity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPersuasion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfSleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                testCases[CreatureConstants.Gynosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfStunning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.InvisibleInLight));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Hezrou].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                testCases[CreatureConstants.Homunculus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good-aligned, silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));

                testCases[CreatureConstants.Human].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate good damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IceStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));

                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, randomFociQuantity: RollHelper.GetRoll(1, 2), focus: "Imp Alternate Form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Imp].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Lemure].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));

                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.Locust_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));

                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good and cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AlignWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Marilith].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.MultiweaponFighting));

                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to moving air", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In arid, dusty environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Underground or buried up to its waist in earth", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + " (self)", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoftenEarthAndStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HeatMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChillMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching magma, lava, or a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In a wet or muddy environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In an arid environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Glitterdust, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Draw moisture from the air", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching boiling water or in a hot, humid area", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Rainstorm of boiling water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to rain or submerged up to its waist in water", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.MindFlayer].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Feeblemind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Slow, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Nalfeshnee].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 60));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Split));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.OchreJelly].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good spells or effects, or from good-aligned silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateUndead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.PitFiend].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Year));

                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Bat, Small or Medium monstrous centipede, toad, and wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear + ": 30-foot radius area from the quasit", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Quasit].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": can ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HalfDamage, focus: "Slashing weapons"));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HalfDamage, focus: "Piercing weapons"));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                testCases[CreatureConstants.Rat_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                testCases[CreatureConstants.Retriever].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                testCases[CreatureConstants.Salamander_Flamebrother].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));

                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));
                testCases[CreatureConstants.Salamander_Average].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.Monster.Multiattack));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BurningHands, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlamingSphere, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Salamander_Noble].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": Huge fire elemental", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.SeaHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                testCases[CreatureConstants.SeaHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.SeaHag].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 14));

                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Red].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Blue].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Medium or Large humanoid form"));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Green].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form"));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Gray].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form"));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Slaad_Death].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));

                testCases[CreatureConstants.Snake_Constrictor].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Snake_Viper_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 30));
                testCases[CreatureConstants.Spider_Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "any Small or Medium humanoid"));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Succubus].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

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

                testCases[CreatureConstants.Triton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Triton].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIV, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Vrock].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heroism, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Whale_Baleen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Baleen].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Whale_Cachalot].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Cachalot].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                testCases[CreatureConstants.Whale_Orca].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                testCases[CreatureConstants.Whale_Orca].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

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

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static class TestDataHelper
        {
            public static IEnumerable EnumerateTestCases(Dictionary<string, List<string[]>> testCases)
            {
                foreach (var testCase in testCases)
                {
                    var total = testCase.Value.Count();
                    var description = total.ToString();

                    if (testCase.Value.FirstOrDefault()?.FirstOrDefault() == None)
                        description = None;

                    yield return new TestCaseData(testCase.Key, testCase.Value)
                        .SetName($"SpecialQualityData({testCase.Key}, {description})");
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
                var subtypes = CreatureConstants.Types.Subtypes.All();

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
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AuraOfMenace));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                testCases[CreatureConstants.Types.Subtypes.Archon].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                testCases[CreatureConstants.Types.Subtypes.Augmented].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Chaotic].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Cold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                testCases[CreatureConstants.Types.Subtypes.Cold].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                testCases[CreatureConstants.Types.Subtypes.Earth].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Evil].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Extraplanar].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                testCases[CreatureConstants.Types.Subtypes.Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                testCases[CreatureConstants.Types.Subtypes.Goblinoid].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Good].Add(SpecialQualityHelper.BuildData(None));

                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks"));
                testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "50% chance to ignore any damage from a corporeal source (except for positive energy, negative energy, force effects such as magic missile, or attacks made with ghost touch weapons)"));
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

                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Half damage from slashing and piercing weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit)); //TODO: Tiny creatures
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage")); //TODO: Only fine or diminutive
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
                testCases[CreatureConstants.Types.Subtypes.Swarm].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "High winds")); //TODO: Only fine or diminutive

                testCases[CreatureConstants.Types.Subtypes.Water].Add(SpecialQualityHelper.BuildData(None));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }
    }
}
