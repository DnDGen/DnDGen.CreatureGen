using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class SpecialQualityDataTests : CollectionTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get { return TableNameConstants.Collection.SpecialQualityData; }
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();

            AssertCollectionNames(creatures);
        }

        public class SpecialQualityTestData
        {
            private const string None = "NONE";

            public static IEnumerable TestCases
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
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Spot, power: 2));
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Listen, power: 2));
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Aasimar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.MucusCloud));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HypnoticPattern, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IllusoryWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProgrammedImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Aboleth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                    testCases[CreatureConstants.Achaierai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Achaierai].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.Allip].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.Androsphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Androsphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.UncannyDodge));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SaveBonus, focus: SaveConstants.Fortitude + ": against poison", power: 4));
                    testCases[CreatureConstants.Angel_AstralDeva].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
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

                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SaveBonus, focus: SaveConstants.Fortitude + ": against poison", power: 4));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                    testCases[CreatureConstants.Angel_Planetar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
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

                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic evil", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SaveBonus, focus: SaveConstants.Fortitude + ": against poison", power: 4));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Angel_Solar].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
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

                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Tiny].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
                    testCases[CreatureConstants.AnimatedObject_Colossal].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));

                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on Plane of Air", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on Plane of Air", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on Plane of Air", power: 2));
                    testCases[CreatureConstants.Arrowhawk_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Basilisk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Basilisk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Basilisk].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Hide + ": in natural settings", power: 4));

                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Hide + ": in natural settings", power: 4));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                    testCases[CreatureConstants.Criosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Criosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Air_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Earth_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                    testCases[CreatureConstants.Elemental_Fire_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.WeaponFinesse));

                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Small].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Medium].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Large].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Huge].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Greater].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Elemental_Water_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

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

                    testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                    testCases[CreatureConstants.Hieracosphinx].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Spot, power: 4));

                    testCases[CreatureConstants.Human].Add(SpecialQualityHelper.BuildData(None));

                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to moving air", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Air].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In arid, dusty environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Dust].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Underground or buried up to its waist in earth", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + " (self)", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoftenEarthAndStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Earth].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HeatMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                    testCases[CreatureConstants.Mephit_Fire].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChillMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Mephit_Ice].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching magma, lava, or a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Magma].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In a wet or muddy environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Mephit_Ooze].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In an arid environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Glitterdust, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Draw moisture from the air", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Salt].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching boiling water or in a hot, humid area", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Rainstorm of boiling water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Steam].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));

                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to rain or submerged up to its waist in water", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Disguise + ": while acting", power: 2));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Mephit_Water].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Bluff, power: 2));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Hide, power: 2));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tiefling].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on other planes", power: 2));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Tojanida_Juvenile].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on other planes", power: 2));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Tojanida_Adult].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.DamageResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": following tracks", power: 2));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Survival + ": on other planes", power: 2));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.UseRope + ": with bindings", power: 2));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8));
                    testCases[CreatureConstants.Tojanida_Elder].Add(SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10));

                    foreach (var testCase in testCases)
                    {
                        var featNames = testCase.Value.Select(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex]);
                        var description = string.Join(", ", featNames);

                        if (testCase.Value.Any() && testCase.Value[0][DataIndexConstants.SpecialQualityData.FeatNameIndex] == None)
                        {
                            testCase.Value.Clear();
                        }

                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SpecialQualityData({testCase.Key}, [{description}])");
                    }
                }
            }
        }

        public static class SpecialQualityHelper
        {
            public static string[] BuildData(string featName, string focus = "", int frequencyQuantity = 0, string frequencyTimePeriod = "", int minimumHitDice = 0, int maximumHitDice = 0, int power = 0, int randomFociQuantity = 0, string size = "")
            {
                var data = DataIndexConstants.SpecialQualityData.InitializeData();

                data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = featName;
                data[DataIndexConstants.SpecialQualityData.FocusIndex] = focus;
                data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
                data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
                data[DataIndexConstants.SpecialQualityData.MaximumHitDiceRequirementIndex] = Convert.ToString(minimumHitDice);
                data[DataIndexConstants.SpecialQualityData.MinimumHitDiceRequirementIndex] = Convert.ToString(maximumHitDice);
                data[DataIndexConstants.SpecialQualityData.PowerIndex] = Convert.ToString(power);
                data[DataIndexConstants.SpecialQualityData.RandomFociQuantity] = Convert.ToString(randomFociQuantity);
                data[DataIndexConstants.SpecialQualityData.SizeRequirementIndex] = size;

                return data;
            }

            public static string BuildData(string[] data)
            {
                return string.Join("/", data);
            }

            public static string[] ParseData(string input)
            {
                return input.Split('/');
            }
        }

        [TestCaseSource(typeof(SpecialQualityTestData), "TestCases")]
        public void SpecialQualityData(string creature, List<string[]> entries)
        {
            var data = entries.Select(e => SpecialQualityHelper.BuildData(e)).ToArray();

            DistinctCollection(creature, data);
        }

        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        public void CreaturesOfSubtypeHaveSwimSkillBonus(string subtype)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, subtype);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var swimBonusData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8);
            var swimTake10Data = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10);

            var swimBonus = SpecialQualityHelper.BuildData(swimBonusData);
            var swimTake10 = SpecialQualityHelper.BuildData(swimTake10Data);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(swimBonus), creature);
                Assert.That(specialQualities, Contains.Item(swimTake10), creature);
            }
        }

        [Test]
        public void ElementalsHaveElementalTraits()
        {
            var elementals = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Elemental);

            AssertCollection(elementals.Intersect(table.Keys), elementals);

            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityFlankingData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking");

            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityFlanking = SpecialQualityHelper.BuildData(immunityFlankingData);

            foreach (var elemental in elementals)
            {
                var specialQualities = table[elemental];

                Assert.That(specialQualities, Is.Not.Empty, elemental);
                Assert.That(specialQualities, Contains.Item(immunityPoison), elemental);
                Assert.That(specialQualities, Contains.Item(immunitySleep), elemental);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), elemental);
                Assert.That(specialQualities, Contains.Item(immunityStunning), elemental);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), elemental);
                Assert.That(specialQualities, Contains.Item(immunityFlanking), elemental);
            }
        }

        [Test]
        public void UndeadHaveUndeadTraits()
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Undead);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var immunityMindData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects");
            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityDiseaseData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease");
            var immunityDeathData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityNonlethalData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage");
            var immunityAbilityDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Abiltiy Drain");
            var immunityEnergyDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain");
            var immunityFatigueData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue");
            var immunityExhaustionData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion");

            var immunityMind = SpecialQualityHelper.BuildData(immunityMindData);
            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityDisease = SpecialQualityHelper.BuildData(immunityDiseaseData);
            var immunityDeath = SpecialQualityHelper.BuildData(immunityDeathData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityNonlethal = SpecialQualityHelper.BuildData(immunityNonlethalData);
            var immunityAbilityDrain = SpecialQualityHelper.BuildData(immunityAbilityDrainData);
            var immunityEnergyDrain = SpecialQualityHelper.BuildData(immunityEnergyDrainData);
            var immunityFatigue = SpecialQualityHelper.BuildData(immunityFatigueData);
            var immunityExhaustion = SpecialQualityHelper.BuildData(immunityExhaustionData);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(immunityMind), creature);
                Assert.That(specialQualities, Contains.Item(immunityPoison), creature);
                Assert.That(specialQualities, Contains.Item(immunitySleep), creature);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), creature);
                Assert.That(specialQualities, Contains.Item(immunityStunning), creature);
                Assert.That(specialQualities, Contains.Item(immunityDisease), creature);
                Assert.That(specialQualities, Contains.Item(immunityDeath), creature);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), creature);
                Assert.That(specialQualities, Contains.Item(immunityNonlethal), creature);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDrain), creature);
                Assert.That(specialQualities, Contains.Item(immunityEnergyDrain), creature);
                Assert.That(specialQualities, Contains.Item(immunityFatigue), creature);
                Assert.That(specialQualities, Contains.Item(immunityExhaustion), creature);
            }
        }

        [Test]
        public void IncorporealHaveIncorporealTraits()
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var immunityNonmagicalAttacksData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks");

            var immunityNonmagicalAttacks = SpecialQualityHelper.BuildData(immunityNonmagicalAttacksData);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(immunityNonmagicalAttacks), creature);
            }
        }

        [Test]
        public void ConstructsHaveConstructTraits()
        {
            var constructs = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Construct);

            AssertCollection(constructs.Intersect(table.Keys), constructs);

            var immunityMindData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects");
            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityDiseaseData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease");
            var immunityDeathData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death");
            var immunityNecromancyData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityNonlethalData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage");
            var immunityAbilityDamageData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Abiltiy Damage");
            var immunityAbilityDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Abiltiy Drain");
            var immunityEnergyDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain");
            var immunityFatigueData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue");
            var immunityExhaustionData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion");

            var immunityMind = SpecialQualityHelper.BuildData(immunityMindData);
            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityDisease = SpecialQualityHelper.BuildData(immunityDiseaseData);
            var immunityDeath = SpecialQualityHelper.BuildData(immunityDeathData);
            var immunityNecromancy = SpecialQualityHelper.BuildData(immunityNecromancyData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityNonlethal = SpecialQualityHelper.BuildData(immunityNonlethalData);
            var immunityAbilityDamage = SpecialQualityHelper.BuildData(immunityAbilityDamageData);
            var immunityAbilityDrain = SpecialQualityHelper.BuildData(immunityAbilityDrainData);
            var immunityEnergyDrain = SpecialQualityHelper.BuildData(immunityEnergyDrainData);
            var immunityFatigue = SpecialQualityHelper.BuildData(immunityFatigueData);
            var immunityExhaustion = SpecialQualityHelper.BuildData(immunityExhaustionData);

            foreach (var construct in constructs)
            {
                var specialQualities = table[construct];

                Assert.That(specialQualities, Is.Not.Empty, construct);
                Assert.That(specialQualities, Contains.Item(immunityMind), construct);
                Assert.That(specialQualities, Contains.Item(immunityPoison), construct);
                Assert.That(specialQualities, Contains.Item(immunitySleep), construct);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), construct);
                Assert.That(specialQualities, Contains.Item(immunityStunning), construct);
                Assert.That(specialQualities, Contains.Item(immunityDisease), construct);
                Assert.That(specialQualities, Contains.Item(immunityDeath), construct);
                Assert.That(specialQualities, Contains.Item(immunityNecromancy), construct);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), construct);
                Assert.That(specialQualities, Contains.Item(immunityNonlethal), construct);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDamage), construct);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDrain), construct);
                Assert.That(specialQualities, Contains.Item(immunityEnergyDrain), construct);
                Assert.That(specialQualities, Contains.Item(immunityFatigue), construct);
                Assert.That(specialQualities, Contains.Item(immunityExhaustion), construct);
            }
        }

        [Test]
        public void BonusFeatsHaveCorrectData()
        {
            var feats = FeatConstants.All();
            Assert.Fail("not yet written");
        }

        [Test]
        public void FastHealingHasCorrectFrequency()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void DamageReductionHasCorrectData()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void DamageResistanceHasCorrectData()
        {
            Assert.Fail("not yet written");
        }
    }
}
