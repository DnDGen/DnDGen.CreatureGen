using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class ClericSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Cleric);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CreateWater,
                SpellConstants.CureInflictMinorWounds,
                SpellConstants.DetectMagic,
                SpellConstants.DetectPoison,
                SpellConstants.Guidance,
                SpellConstants.Light,
                SpellConstants.Mending,
                SpellConstants.PurifyFoodAndDrink,
                SpellConstants.ReadMagic,
                SpellConstants.Resistance,
                SpellConstants.Virtue,
                SpellConstants.Bane,
                SpellConstants.Bless,
                SpellConstants.BlessWater,
                SpellConstants.CauseFear,
                SpellConstants.Command,
                SpellConstants.ComprehendLanguages,
                SpellConstants.CureInflictLightWounds,
                SpellConstants.CurseWater,
                SpellConstants.Deathwatch,
                SpellConstants.DetectAlignment,
                SpellConstants.DetectUndead,
                SpellConstants.DivineFavor,
                SpellConstants.Doom,
                SpellConstants.EndureElements,
                SpellConstants.EntropicShield,
                SpellConstants.HideFromUndead,
                SpellConstants.MagicStone,
                SpellConstants.MagicWeapon,
                SpellConstants.ObscuringMist,
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.RemoveFear,
                SpellConstants.Sanctuary,
                SpellConstants.ShieldOfFaith,
                SpellConstants.SummonMonsterI,
                SpellConstants.Aid,
                SpellConstants.AlignWeapon,
                SpellConstants.Augury,
                SpellConstants.BearsEndurance,
                SpellConstants.BullsStrength,
                SpellConstants.CalmEmotions,
                SpellConstants.Consecrate,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.Darkness,
                SpellConstants.DeathKnell,
                SpellConstants.DelayPoison,
                SpellConstants.Desecrate,
                SpellConstants.EaglesSplendor,
                SpellConstants.Enthrall,
                SpellConstants.FindTraps,
                SpellConstants.GentleRepose,
                SpellConstants.HoldPerson,
                SpellConstants.MakeWhole,
                SpellConstants.OwlsWisdom,
                SpellConstants.RemoveParalysis,
                SpellConstants.ResistEnergy,
                SpellConstants.Restoration_Lesser,
                SpellConstants.Shatter,
                SpellConstants.ShieldOther,
                SpellConstants.Silence,
                SpellConstants.SoundBurst,
                SpellConstants.SpiritualWeapon,
                SpellConstants.Status,
                SpellConstants.SummonMonsterII,
                SpellConstants.UndetectableAlignment,
                SpellConstants.ZoneOfTruth,
                SpellConstants.AnimateDead,
                SpellConstants.BestowCurse,
                SpellConstants.BlindnessDeafness,
                SpellConstants.Contagion,
                SpellConstants.ContinualFlame,
                SpellConstants.CreateFoodAndWater,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.Daylight,
                SpellConstants.DeeperDarkness,
                SpellConstants.DispelMagic,
                SpellConstants.GlyphOfWarding,
                SpellConstants.HelpingHand,
                SpellConstants.InvisibilityPurge,
                SpellConstants.LocateObject,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.MagicVestment,
                SpellConstants.MeldIntoStone,
                SpellConstants.ObscureObject,
                SpellConstants.Prayer,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.RemoveBlindnessDeafness,
                SpellConstants.RemoveCurse,
                SpellConstants.RemoveDisease,
                SpellConstants.SearingLight,
                SpellConstants.SpeakWithDead,
                SpellConstants.StoneShape,
                SpellConstants.SummonMonsterIII,
                SpellConstants.WaterBreathing,
                SpellConstants.WaterWalk,
                SpellConstants.WindWall,
                SpellConstants.AirWalk,
                SpellConstants.ControlWater,
                SpellConstants.CureInflictCriticalWounds,
                SpellConstants.DeathWard,
                SpellConstants.DimensionalAnchor,
                SpellConstants.DiscernLies,
                SpellConstants.Dismissal,
                SpellConstants.Divination,
                SpellConstants.DivinePower,
                SpellConstants.FreedomOfMovement,
                SpellConstants.GiantVermin,
                SpellConstants.ImbueWithSpellAbility,
                SpellConstants.MagicWeapon_Greater,
                SpellConstants.NeutralizePoison,
                SpellConstants.PlanarAlly_Lesser,
                SpellConstants.Poison,
                SpellConstants.RepelVermin,
                SpellConstants.Restoration,
                SpellConstants.Sending,
                SpellConstants.SpellImmunity,
                SpellConstants.SummonMonsterIV,
                SpellConstants.Tongues,
                SpellConstants.Atonement,
                SpellConstants.BreakEnchantment,
                SpellConstants.Command_Greater,
                SpellConstants.Commune,
                SpellConstants.CureInflictLightWounds_Mass,
                SpellConstants.DispelAlignment,
                SpellConstants.DisruptingWeapon,
                SpellConstants.FlameStrike,
                SpellConstants.Hallow,
                SpellConstants.InsectPlague,
                SpellConstants.MarkOfJustice,
                SpellConstants.PlaneShift,
                SpellConstants.RaiseDead,
                SpellConstants.RighteousMight,
                SpellConstants.Scrying,
                SpellConstants.SlayLiving,
                SpellConstants.SpellResistance,
                SpellConstants.SummonMonsterV,
                SpellConstants.SymbolOfPain,
                SpellConstants.SymbolOfSleep,
                SpellConstants.TrueSeeing,
                SpellConstants.Unhallow,
                SpellConstants.WallOfStone,
                SpellConstants.AnimateObjects,
                SpellConstants.AntilifeShell,
                SpellConstants.Banishment,
                SpellConstants.BearsEndurance_Mass,
                SpellConstants.BladeBarrier,
                SpellConstants.BullsStrength_Mass,
                SpellConstants.CreateUndead,
                SpellConstants.CureInflictModerateWounds_Mass,
                SpellConstants.DispelMagic_Greater,
                SpellConstants.EaglesSplendor_Mass,
                SpellConstants.FindThePath,
                SpellConstants.Forbiddance,
                SpellConstants.GeasQuest,
                SpellConstants.GlyphOfWarding_Greater,
                SpellConstants.HealHarm,
                SpellConstants.HeroesFeast,
                SpellConstants.OwlsWisdom_Mass,
                SpellConstants.PlanarAlly,
                SpellConstants.SummonMonsterVI,
                SpellConstants.SymbolOfFear,
                SpellConstants.SymbolOfPersuasion,
                SpellConstants.UndeathToDeath,
                SpellConstants.WindWalk,
                SpellConstants.WordOfRecall,
                SpellConstants.Blasphemy,
                SpellConstants.ControlWeather,
                SpellConstants.CureInflictSeriousWounds_Mass,
                SpellConstants.Destruction,
                SpellConstants.Dictum,
                SpellConstants.EtherealJaunt,
                SpellConstants.HolyWord,
                SpellConstants.Refuge,
                SpellConstants.Regenerate,
                SpellConstants.Repulsion,
                SpellConstants.Restoration_Greater,
                SpellConstants.Resurrection,
                SpellConstants.Scrying_Greater,
                SpellConstants.SummonMonsterVII,
                SpellConstants.SymbolOfStunning,
                SpellConstants.SymbolOfWeakness,
                SpellConstants.WordOfChaos,
                SpellConstants.AntimagicField,
                SpellConstants.CloakOfChaos,
                SpellConstants.CreateGreaterUndead,
                SpellConstants.CureInflictCriticalWounds_Mass,
                SpellConstants.DimensionalLock,
                SpellConstants.DiscernLocation,
                SpellConstants.Earthquake,
                SpellConstants.FireStorm,
                SpellConstants.HolyAura,
                SpellConstants.PlanarAlly_Greater,
                SpellConstants.ShieldOfLaw,
                SpellConstants.SpellImmunity_Greater,
                SpellConstants.SummonMonsterVIII,
                SpellConstants.SymbolOfDeath,
                SpellConstants.SymbolOfInsanity,
                SpellConstants.UnholyAura,
                SpellConstants.AstralProjection,
                SpellConstants.EnergyDrain,
                SpellConstants.Etherealness,
                SpellConstants.Gate,
                SpellConstants.HealHarm_Mass,
                SpellConstants.Implosion,
                SpellConstants.Miracle,
                SpellConstants.SoulBind,
                SpellConstants.StormOfVengeance,
                SpellConstants.SummonMonsterIX,
                SpellConstants.TrueResurrection
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllClericSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Cleric]);
        }

        [TestCase(SpellConstants.CreateWater, 0)]
        [TestCase(SpellConstants.CureInflictMinorWounds, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.DetectPoison, 0)]
        [TestCase(SpellConstants.Guidance, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.PurifyFoodAndDrink, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.Resistance, 0)]
        [TestCase(SpellConstants.Virtue, 0)]
        [TestCase(SpellConstants.Bane, 1)]
        [TestCase(SpellConstants.Bless, 1)]
        [TestCase(SpellConstants.BlessWater, 1)]
        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.Command, 1)]
        [TestCase(SpellConstants.ComprehendLanguages, 1)]
        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.CurseWater, 1)]
        [TestCase(SpellConstants.Deathwatch, 1)]
        [TestCase(SpellConstants.DetectAlignment, 1)]
        [TestCase(SpellConstants.DetectUndead, 1)]
        [TestCase(SpellConstants.DivineFavor, 1)]
        [TestCase(SpellConstants.Doom, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.EntropicShield, 1)]
        [TestCase(SpellConstants.HideFromUndead, 1)]
        [TestCase(SpellConstants.MagicStone, 1)]
        [TestCase(SpellConstants.MagicWeapon, 1)]
        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.RemoveFear, 1)]
        [TestCase(SpellConstants.Sanctuary, 1)]
        [TestCase(SpellConstants.ShieldOfFaith, 1)]
        [TestCase(SpellConstants.SummonMonsterI, 1)]
        [TestCase(SpellConstants.Aid, 2)]
        [TestCase(SpellConstants.AlignWeapon, 2)]
        [TestCase(SpellConstants.Augury, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.CalmEmotions, 2)]
        [TestCase(SpellConstants.Consecrate, 2)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.DeathKnell, 2)]
        [TestCase(SpellConstants.DelayPoison, 2)]
        [TestCase(SpellConstants.Desecrate, 2)]
        [TestCase(SpellConstants.EaglesSplendor, 2)]
        [TestCase(SpellConstants.Enthrall, 2)]
        [TestCase(SpellConstants.FindTraps, 2)]
        [TestCase(SpellConstants.GentleRepose, 2)]
        [TestCase(SpellConstants.HoldPerson, 2)]
        [TestCase(SpellConstants.MakeWhole, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.RemoveParalysis, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.Restoration_Lesser, 2)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.ShieldOther, 2)]
        [TestCase(SpellConstants.Silence, 2)]
        [TestCase(SpellConstants.SoundBurst, 2)]
        [TestCase(SpellConstants.SpiritualWeapon, 2)]
        [TestCase(SpellConstants.Status, 2)]
        [TestCase(SpellConstants.SummonMonsterII, 2)]
        [TestCase(SpellConstants.UndetectableAlignment, 2)]
        [TestCase(SpellConstants.ZoneOfTruth, 2)]
        [TestCase(SpellConstants.AnimateDead, 3)]
        [TestCase(SpellConstants.BestowCurse, 3)]
        [TestCase(SpellConstants.BlindnessDeafness, 3)]
        [TestCase(SpellConstants.Contagion, 3)]
        [TestCase(SpellConstants.ContinualFlame, 3)]
        [TestCase(SpellConstants.CreateFoodAndWater, 3)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.DeeperDarkness, 3)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.GlyphOfWarding, 3)]
        [TestCase(SpellConstants.HelpingHand, 3)]
        [TestCase(SpellConstants.InvisibilityPurge, 3)]
        [TestCase(SpellConstants.LocateObject, 3)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.MagicVestment, 3)]
        [TestCase(SpellConstants.MeldIntoStone, 3)]
        [TestCase(SpellConstants.ObscureObject, 3)]
        [TestCase(SpellConstants.Prayer, 3)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.RemoveBlindnessDeafness, 3)]
        [TestCase(SpellConstants.RemoveCurse, 3)]
        [TestCase(SpellConstants.RemoveDisease, 3)]
        [TestCase(SpellConstants.SearingLight, 3)]
        [TestCase(SpellConstants.SpeakWithDead, 3)]
        [TestCase(SpellConstants.StoneShape, 3)]
        [TestCase(SpellConstants.SummonMonsterIII, 3)]
        [TestCase(SpellConstants.WaterBreathing, 3)]
        [TestCase(SpellConstants.WaterWalk, 3)]
        [TestCase(SpellConstants.WindWall, 3)]
        [TestCase(SpellConstants.AirWalk, 4)]
        [TestCase(SpellConstants.ControlWater, 4)]
        [TestCase(SpellConstants.CureInflictCriticalWounds, 4)]
        [TestCase(SpellConstants.DeathWard, 4)]
        [TestCase(SpellConstants.DimensionalAnchor, 4)]
        [TestCase(SpellConstants.DiscernLies, 4)]
        [TestCase(SpellConstants.Dismissal, 4)]
        [TestCase(SpellConstants.Divination, 4)]
        [TestCase(SpellConstants.DivinePower, 4)]
        [TestCase(SpellConstants.FreedomOfMovement, 4)]
        [TestCase(SpellConstants.GiantVermin, 4)]
        [TestCase(SpellConstants.ImbueWithSpellAbility, 4)]
        [TestCase(SpellConstants.MagicWeapon_Greater, 4)]
        [TestCase(SpellConstants.NeutralizePoison, 4)]
        [TestCase(SpellConstants.PlanarAlly_Lesser, 4)]
        [TestCase(SpellConstants.Poison, 4)]
        [TestCase(SpellConstants.RepelVermin, 4)]
        [TestCase(SpellConstants.Restoration, 4)]
        [TestCase(SpellConstants.Sending, 4)]
        [TestCase(SpellConstants.SpellImmunity, 4)]
        [TestCase(SpellConstants.SummonMonsterIV, 4)]
        [TestCase(SpellConstants.Tongues, 4)]
        [TestCase(SpellConstants.Atonement, 5)]
        [TestCase(SpellConstants.BreakEnchantment, 5)]
        [TestCase(SpellConstants.Command_Greater, 5)]
        [TestCase(SpellConstants.Commune, 5)]
        [TestCase(SpellConstants.CureInflictLightWounds_Mass, 5)]
        [TestCase(SpellConstants.DispelAlignment, 5)]
        [TestCase(SpellConstants.DisruptingWeapon, 5)]
        [TestCase(SpellConstants.FlameStrike, 5)]
        [TestCase(SpellConstants.Hallow, 5)]
        [TestCase(SpellConstants.InsectPlague, 5)]
        [TestCase(SpellConstants.MarkOfJustice, 5)]
        [TestCase(SpellConstants.PlaneShift, 5)]
        [TestCase(SpellConstants.RaiseDead, 5)]
        [TestCase(SpellConstants.RighteousMight, 5)]
        [TestCase(SpellConstants.Scrying, 5)]
        [TestCase(SpellConstants.SlayLiving, 5)]
        [TestCase(SpellConstants.SpellResistance, 5)]
        [TestCase(SpellConstants.SummonMonsterV, 5)]
        [TestCase(SpellConstants.SymbolOfPain, 5)]
        [TestCase(SpellConstants.SymbolOfSleep, 5)]
        [TestCase(SpellConstants.TrueSeeing, 5)]
        [TestCase(SpellConstants.Unhallow, 5)]
        [TestCase(SpellConstants.WallOfStone, 5)]
        [TestCase(SpellConstants.AnimateObjects, 6)]
        [TestCase(SpellConstants.AntilifeShell, 6)]
        [TestCase(SpellConstants.Banishment, 6)]
        [TestCase(SpellConstants.BearsEndurance_Mass, 6)]
        [TestCase(SpellConstants.BladeBarrier, 6)]
        [TestCase(SpellConstants.BullsStrength_Mass, 6)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.CureInflictModerateWounds_Mass, 6)]
        [TestCase(SpellConstants.DispelMagic_Greater, 6)]
        [TestCase(SpellConstants.EaglesSplendor_Mass, 6)]
        [TestCase(SpellConstants.FindThePath, 6)]
        [TestCase(SpellConstants.Forbiddance, 6)]
        [TestCase(SpellConstants.GeasQuest, 6)]
        [TestCase(SpellConstants.GlyphOfWarding_Greater, 6)]
        [TestCase(SpellConstants.HealHarm, 6)]
        [TestCase(SpellConstants.HeroesFeast, 6)]
        [TestCase(SpellConstants.OwlsWisdom_Mass, 6)]
        [TestCase(SpellConstants.PlanarAlly, 6)]
        [TestCase(SpellConstants.SummonMonsterVI, 6)]
        [TestCase(SpellConstants.SymbolOfFear, 6)]
        [TestCase(SpellConstants.SymbolOfPersuasion, 6)]
        [TestCase(SpellConstants.UndeathToDeath, 6)]
        [TestCase(SpellConstants.WindWalk, 6)]
        [TestCase(SpellConstants.WordOfRecall, 6)]
        [TestCase(SpellConstants.Blasphemy, 7)]
        [TestCase(SpellConstants.ControlWeather, 7)]
        [TestCase(SpellConstants.CureInflictSeriousWounds_Mass, 7)]
        [TestCase(SpellConstants.Destruction, 7)]
        [TestCase(SpellConstants.Dictum, 7)]
        [TestCase(SpellConstants.EtherealJaunt, 7)]
        [TestCase(SpellConstants.HolyWord, 7)]
        [TestCase(SpellConstants.Refuge, 7)]
        [TestCase(SpellConstants.Regenerate, 7)]
        [TestCase(SpellConstants.Repulsion, 7)]
        [TestCase(SpellConstants.Restoration_Greater, 7)]
        [TestCase(SpellConstants.Resurrection, 7)]
        [TestCase(SpellConstants.Scrying_Greater, 7)]
        [TestCase(SpellConstants.SummonMonsterVII, 7)]
        [TestCase(SpellConstants.SymbolOfStunning, 7)]
        [TestCase(SpellConstants.SymbolOfWeakness, 7)]
        [TestCase(SpellConstants.WordOfChaos, 7)]
        [TestCase(SpellConstants.AntimagicField, 8)]
        [TestCase(SpellConstants.CloakOfChaos, 8)]
        [TestCase(SpellConstants.CreateGreaterUndead, 8)]
        [TestCase(SpellConstants.CureInflictCriticalWounds_Mass, 8)]
        [TestCase(SpellConstants.DimensionalLock, 8)]
        [TestCase(SpellConstants.DiscernLocation, 8)]
        [TestCase(SpellConstants.Earthquake, 8)]
        [TestCase(SpellConstants.FireStorm, 8)]
        [TestCase(SpellConstants.HolyAura, 8)]
        [TestCase(SpellConstants.PlanarAlly_Greater, 8)]
        [TestCase(SpellConstants.ShieldOfLaw, 8)]
        [TestCase(SpellConstants.SpellImmunity_Greater, 8)]
        [TestCase(SpellConstants.SummonMonsterVIII, 8)]
        [TestCase(SpellConstants.SymbolOfDeath, 8)]
        [TestCase(SpellConstants.SymbolOfInsanity, 8)]
        [TestCase(SpellConstants.UnholyAura, 8)]
        [TestCase(SpellConstants.AstralProjection, 9)]
        [TestCase(SpellConstants.EnergyDrain, 9)]
        [TestCase(SpellConstants.Etherealness, 9)]
        [TestCase(SpellConstants.Gate, 9)]
        [TestCase(SpellConstants.HealHarm_Mass, 9)]
        [TestCase(SpellConstants.Implosion, 9)]
        [TestCase(SpellConstants.Miracle, 9)]
        [TestCase(SpellConstants.SoulBind, 9)]
        [TestCase(SpellConstants.StormOfVengeance, 9)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        [TestCase(SpellConstants.TrueResurrection, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
