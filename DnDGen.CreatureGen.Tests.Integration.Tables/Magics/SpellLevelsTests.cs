using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Magics
{
    [TestFixture]
    public class SpellLevelsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.SpellLevels;

        [Test]
        public void SpellLevelsHaveAllNames()
        {
            var casters = new[]
            {
                SpellConstants.Casters.Bard,
                SpellConstants.Casters.Cleric,
                SpellConstants.Casters.Druid,
                SpellConstants.Casters.Sorcerer,
            };

            var domains = new[]
            {
                SpellConstants.Domains.Air,
                SpellConstants.Domains.Animal,
                SpellConstants.Domains.Chaos,
                SpellConstants.Domains.Destruction,
                SpellConstants.Domains.Earth,
                SpellConstants.Domains.Enchantment,
                SpellConstants.Domains.Evil,
                SpellConstants.Domains.Fire,
                SpellConstants.Domains.Good,
                SpellConstants.Domains.Healing,
                SpellConstants.Domains.Illusion,
                SpellConstants.Domains.Knowledge,
                SpellConstants.Domains.Law,
                SpellConstants.Domains.Luck,
                SpellConstants.Domains.Plant,
                SpellConstants.Domains.Protection,
                SpellConstants.Domains.Sun,
                SpellConstants.Domains.Trickery,
                SpellConstants.Domains.War,
                SpellConstants.Domains.Water,
            };

            var names = casters.Union(domains);
            AssertCollectionNames(names);
        }

        [Test]
        public void BardSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            //0-Level Bard Spells (Cantrips)
            spellLevels[SpellConstants.DancingLights] = 0;
            spellLevels[SpellConstants.Daze] = 0;
            spellLevels[SpellConstants.DetectMagic] = 0;
            spellLevels[SpellConstants.Flare] = 0;
            spellLevels[SpellConstants.GhostSound] = 0;
            spellLevels[SpellConstants.KnowDirection] = 0;
            spellLevels[SpellConstants.Light] = 0;
            spellLevels[SpellConstants.Lullaby] = 0;
            spellLevels[SpellConstants.MageHand] = 0;
            spellLevels[SpellConstants.Mending] = 0;
            spellLevels[SpellConstants.Message] = 0;
            spellLevels[SpellConstants.OpenClose] = 0;
            spellLevels[SpellConstants.Prestidigitation] = 0;
            spellLevels[SpellConstants.ReadMagic] = 0;
            spellLevels[SpellConstants.Resistance] = 0;
            spellLevels[SpellConstants.SummonInstrument] = 0;

            //1st-Level Bard Spells
            spellLevels[SpellConstants.Alarm] = 1;
            spellLevels[SpellConstants.AnimateRope] = 1;
            spellLevels[SpellConstants.CauseFear] = 1;
            spellLevels[SpellConstants.CharmPerson] = 1;
            spellLevels[SpellConstants.ComprehendLanguages] = 1;
            spellLevels[SpellConstants.Confusion_Lesser] = 1;
            spellLevels[SpellConstants.CureLightWounds] = 1;
            spellLevels[SpellConstants.DetectSecretDoors] = 1;
            spellLevels[SpellConstants.DisguiseSelf] = 1;
            spellLevels[SpellConstants.Erase] = 1;
            spellLevels[SpellConstants.ExpeditiousRetreat] = 1;
            spellLevels[SpellConstants.FeatherFall] = 1;
            spellLevels[SpellConstants.Grease] = 1;
            spellLevels[SpellConstants.HideousLaughter] = 1;
            spellLevels[SpellConstants.Hypnotism] = 1;
            spellLevels[SpellConstants.Identify] = 1;
            spellLevels[SpellConstants.MagicMouth] = 1;
            spellLevels[SpellConstants.MagicAura] = 1;
            spellLevels[SpellConstants.ObscureObject] = 1;
            spellLevels[SpellConstants.RemoveFear] = 1;
            spellLevels[SpellConstants.SilentImage] = 1;
            spellLevels[SpellConstants.Sleep] = 1;
            spellLevels[SpellConstants.SummonMonsterI] = 1;
            spellLevels[SpellConstants.UndetectableAlignment] = 1;
            spellLevels[SpellConstants.UnseenServant] = 1;
            spellLevels[SpellConstants.Ventriloquism] = 1;

            //2nd-Level Bard Spells
            spellLevels[SpellConstants.AlterSelf] = 2;
            spellLevels[SpellConstants.AnimalMessenger] = 2;
            spellLevels[SpellConstants.AnimalTrance] = 2;
            spellLevels[SpellConstants.BlindnessDeafness] = 2;
            spellLevels[SpellConstants.Blur] = 2;
            spellLevels[SpellConstants.CalmEmotions] = 2;
            spellLevels[SpellConstants.CatsGrace] = 2;
            spellLevels[SpellConstants.CureModerateWounds] = 2;
            spellLevels[SpellConstants.Darkness] = 2;
            spellLevels[SpellConstants.DazeMonster] = 2;
            spellLevels[SpellConstants.DelayPoison] = 2;
            spellLevels[SpellConstants.DetectThoughts] = 2;
            spellLevels[SpellConstants.EaglesSplendor] = 2;
            spellLevels[SpellConstants.Enthrall] = 2;
            spellLevels[SpellConstants.FoxsCunning] = 2;
            spellLevels[SpellConstants.Glitterdust] = 2;
            spellLevels[SpellConstants.Heroism] = 2;
            spellLevels[SpellConstants.HoldPerson] = 2;
            spellLevels[SpellConstants.HypnoticPattern] = 2;
            spellLevels[SpellConstants.Invisibility] = 2;
            spellLevels[SpellConstants.LocateObject] = 2;
            spellLevels[SpellConstants.MinorImage] = 2;
            spellLevels[SpellConstants.MirrorImage] = 2;
            spellLevels[SpellConstants.Misdirection] = 2;
            spellLevels[SpellConstants.Pyrotechnics] = 2;
            spellLevels[SpellConstants.Rage] = 2;
            spellLevels[SpellConstants.Scare] = 2;
            spellLevels[SpellConstants.Shatter] = 2;
            spellLevels[SpellConstants.Silence] = 2;
            spellLevels[SpellConstants.SoundBurst] = 2;
            spellLevels[SpellConstants.Suggestion] = 2;
            spellLevels[SpellConstants.SummonMonsterII] = 2;
            spellLevels[SpellConstants.SummonSwarm] = 2;
            spellLevels[SpellConstants.Tongues] = 2;
            spellLevels[SpellConstants.WhisperingWind] = 2;

            //3rd-Level Bard Spells
            spellLevels[SpellConstants.Blink] = 3;
            spellLevels[SpellConstants.CharmMonster] = 3;
            spellLevels[SpellConstants.ClairaudienceClairvoyance] = 3;
            spellLevels[SpellConstants.Confusion] = 3;
            spellLevels[SpellConstants.CrushingDespair] = 3;
            spellLevels[SpellConstants.CureSeriousWounds] = 3;
            spellLevels[SpellConstants.Daylight] = 3;
            spellLevels[SpellConstants.DeepSlumber] = 3;
            spellLevels[SpellConstants.DispelMagic] = 3;
            spellLevels[SpellConstants.Displacement] = 3;
            spellLevels[SpellConstants.Fear] = 3;
            spellLevels[SpellConstants.GaseousForm] = 3;
            spellLevels[SpellConstants.Geas_Lesser] = 3;
            spellLevels[SpellConstants.Glibness] = 3;
            spellLevels[SpellConstants.GoodHope] = 3;
            spellLevels[SpellConstants.Haste] = 3;
            spellLevels[SpellConstants.IllusoryScript] = 3;
            spellLevels[SpellConstants.InvisibilitySphere] = 3;
            spellLevels[SpellConstants.MajorImage] = 3;
            spellLevels[SpellConstants.PhantomSteed] = 3;
            spellLevels[SpellConstants.RemoveCurse] = 3;
            spellLevels[SpellConstants.Scrying] = 3;
            spellLevels[SpellConstants.SculptSound] = 3;
            spellLevels[SpellConstants.SecretPage] = 3;
            spellLevels[SpellConstants.SeeInvisibility] = 3;
            spellLevels[SpellConstants.SepiaSnakeSigil] = 3;
            spellLevels[SpellConstants.Slow] = 3;
            spellLevels[SpellConstants.SpeakWithAnimals] = 3;
            spellLevels[SpellConstants.SummonMonsterIII] = 3;
            spellLevels[SpellConstants.TinyHut] = 3;

            //4th-Level Bard Spells
            spellLevels[SpellConstants.BreakEnchantment] = 4;
            spellLevels[SpellConstants.CureCriticalWounds] = 4;
            spellLevels[SpellConstants.DetectScrying] = 4;
            spellLevels[SpellConstants.DimensionDoor] = 4;
            spellLevels[SpellConstants.DominatePerson] = 4;
            spellLevels[SpellConstants.FreedomOfMovement] = 4;
            spellLevels[SpellConstants.HallucinatoryTerrain] = 4;
            spellLevels[SpellConstants.HoldMonster] = 4;
            spellLevels[SpellConstants.Invisibility_Greater] = 4;
            spellLevels[SpellConstants.LegendLore] = 4;
            spellLevels[SpellConstants.LocateCreature] = 4;
            spellLevels[SpellConstants.ModifyMemory] = 4;
            spellLevels[SpellConstants.NeutralizePoison] = 4;
            spellLevels[SpellConstants.RainbowPattern] = 4;
            spellLevels[SpellConstants.RepelVermin] = 4;
            spellLevels[SpellConstants.SecureShelter] = 4;
            spellLevels[SpellConstants.ShadowConjuration] = 4;
            spellLevels[SpellConstants.Shout] = 4;
            spellLevels[SpellConstants.SpeakWithPlants] = 4;
            spellLevels[SpellConstants.SummonMonsterIV] = 4;
            spellLevels[SpellConstants.ZoneOfSilence] = 4;

            //5th - Level Bard Spells
            spellLevels[SpellConstants.CureLightWounds_Mass] = 5;
            spellLevels[SpellConstants.DispelMagic_Greater] = 5;
            spellLevels[SpellConstants.Dream] = 5;
            spellLevels[SpellConstants.FalseVision] = 5;
            spellLevels[SpellConstants.Heroism_Greater] = 5;
            spellLevels[SpellConstants.MindFog] = 5;
            spellLevels[SpellConstants.MirageArcana] = 5;
            spellLevels[SpellConstants.Mislead] = 5;
            spellLevels[SpellConstants.Nightmare] = 5;
            spellLevels[SpellConstants.PersistentImage] = 5;
            spellLevels[SpellConstants.Seeming] = 5;
            spellLevels[SpellConstants.ShadowEvocation] = 5;
            spellLevels[SpellConstants.ShadowWalk] = 5;
            spellLevels[SpellConstants.SongOfDiscord] = 5;
            spellLevels[SpellConstants.Suggestion_Mass] = 5;
            spellLevels[SpellConstants.SummonMonsterV] = 5;

            //6th - Level Bard Spells
            spellLevels[SpellConstants.AnalyzeDweomer] = 6;
            spellLevels[SpellConstants.AnimateObjects] = 6;
            spellLevels[SpellConstants.CatsGrace_Mass] = 6;
            spellLevels[SpellConstants.CharmMonster_Mass] = 6;
            spellLevels[SpellConstants.CureModerateWounds_Mass] = 6;
            spellLevels[SpellConstants.EaglesSplendor_Mass] = 6;
            spellLevels[SpellConstants.Eyebite] = 6;
            spellLevels[SpellConstants.FindThePath] = 6;
            spellLevels[SpellConstants.FoxsCunning_Mass] = 6;
            spellLevels[SpellConstants.GeasQuest] = 6;
            spellLevels[SpellConstants.HeroesFeast] = 6;
            spellLevels[SpellConstants.IrresistibleDance] = 6;
            spellLevels[SpellConstants.PermanentImage] = 6;
            spellLevels[SpellConstants.ProgrammedImage] = 6;
            spellLevels[SpellConstants.ProjectImage] = 6;
            spellLevels[SpellConstants.Scrying_Greater] = 6;
            spellLevels[SpellConstants.Shout_Greater] = 6;
            spellLevels[SpellConstants.SummonMonsterVI] = 6;
            spellLevels[SpellConstants.SympatheticVibration] = 6;
            spellLevels[SpellConstants.Veil] = 6;

            AssertTypesAndAmounts(SpellConstants.Casters.Bard, spellLevels);
        }

        [Test]
        public void ClericSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            //0 - Level Cleric Spells(Orisons)
            spellLevels[SpellConstants.CreateWater] = 0;
            spellLevels[SpellConstants.CureMinorWounds] = 0;
            spellLevels[SpellConstants.InflictMinorWounds] = 0;
            spellLevels[SpellConstants.DetectMagic] = 0;
            spellLevels[SpellConstants.DetectPoison] = 0;
            spellLevels[SpellConstants.Guidance] = 0;
            spellLevels[SpellConstants.Light] = 0;
            spellLevels[SpellConstants.Mending] = 0;
            spellLevels[SpellConstants.PurifyFoodAndDrink] = 0;
            spellLevels[SpellConstants.ReadMagic] = 0;
            spellLevels[SpellConstants.Resistance] = 0;
            spellLevels[SpellConstants.Virtue] = 0;

            //1st - Level Cleric Spells
            spellLevels[SpellConstants.Bane] = 1;
            spellLevels[SpellConstants.Bless] = 1;
            spellLevels[SpellConstants.BlessWater] = 1;
            spellLevels[SpellConstants.CauseFear] = 1;
            spellLevels[SpellConstants.Command] = 1;
            spellLevels[SpellConstants.ComprehendLanguages] = 1;
            spellLevels[SpellConstants.CureLightWounds] = 1;
            spellLevels[SpellConstants.InflictLightWounds] = 1;
            spellLevels[SpellConstants.CurseWater] = 1;
            spellLevels[SpellConstants.Deathwatch] = 1;
            spellLevels[SpellConstants.DetectAlignment] = 1;
            spellLevels[SpellConstants.DetectUndead] = 1;
            spellLevels[SpellConstants.DivineFavor] = 1;
            spellLevels[SpellConstants.Doom] = 1;
            spellLevels[SpellConstants.EndureElements] = 1;
            spellLevels[SpellConstants.EntropicShield] = 1;
            spellLevels[SpellConstants.HideFromUndead] = 1;
            spellLevels[SpellConstants.MagicStone] = 1;
            spellLevels[SpellConstants.MagicWeapon] = 1;
            spellLevels[SpellConstants.ObscuringMist] = 1;
            spellLevels[SpellConstants.ProtectionFromAlignment] = 1;
            spellLevels[SpellConstants.RemoveFear] = 1;
            spellLevels[SpellConstants.Sanctuary] = 1;
            spellLevels[SpellConstants.ShieldOfFaith] = 1;
            spellLevels[SpellConstants.SummonMonsterI] = 1;

            //2nd - Level Cleric Spells
            spellLevels[SpellConstants.Aid] = 2;
            spellLevels[SpellConstants.AlignWeapon] = 2;
            spellLevels[SpellConstants.Augury] = 2;
            spellLevels[SpellConstants.BearsEndurance] = 2;
            spellLevels[SpellConstants.BullsStrength] = 2;
            spellLevels[SpellConstants.CalmEmotions] = 2;
            spellLevels[SpellConstants.Consecrate] = 2;
            spellLevels[SpellConstants.CureModerateWounds] = 2;
            spellLevels[SpellConstants.InflictModerateWounds] = 2;
            spellLevels[SpellConstants.Darkness] = 2;
            spellLevels[SpellConstants.DeathKnell] = 2;
            spellLevels[SpellConstants.DelayPoison] = 2;
            spellLevels[SpellConstants.Desecrate] = 2;
            spellLevels[SpellConstants.EaglesSplendor] = 2;
            spellLevels[SpellConstants.Enthrall] = 2;
            spellLevels[SpellConstants.FindTraps] = 2;
            spellLevels[SpellConstants.GentleRepose] = 2;
            spellLevels[SpellConstants.HoldPerson] = 2;
            spellLevels[SpellConstants.MakeWhole] = 2;
            spellLevels[SpellConstants.OwlsWisdom] = 2;
            spellLevels[SpellConstants.RemoveParalysis] = 2;
            spellLevels[SpellConstants.ResistEnergy] = 2;
            spellLevels[SpellConstants.Restoration_Lesser] = 2;
            spellLevels[SpellConstants.Shatter] = 2;
            spellLevels[SpellConstants.ShieldOther] = 2;
            spellLevels[SpellConstants.Silence] = 2;
            spellLevels[SpellConstants.SoundBurst] = 2;
            spellLevels[SpellConstants.SpiritualWeapon] = 2;
            spellLevels[SpellConstants.Status] = 2;
            spellLevels[SpellConstants.SummonMonsterII] = 2;
            spellLevels[SpellConstants.UndetectableAlignment] = 2;
            spellLevels[SpellConstants.ZoneOfTruth] = 2;

            //3rd - Level Cleric Spells
            spellLevels[SpellConstants.AnimateDead] = 3;
            spellLevels[SpellConstants.BestowCurse] = 3;
            spellLevels[SpellConstants.BlindnessDeafness] = 3;
            spellLevels[SpellConstants.Contagion] = 3;
            spellLevels[SpellConstants.ContinualFlame] = 3;
            spellLevels[SpellConstants.CreateFoodAndWater] = 3;
            spellLevels[SpellConstants.CureSeriousWounds] = 3;
            spellLevels[SpellConstants.InflictSeriousWounds] = 3;
            spellLevels[SpellConstants.Daylight] = 3;
            spellLevels[SpellConstants.DeeperDarkness] = 3;
            spellLevels[SpellConstants.DispelMagic] = 3;
            spellLevels[SpellConstants.GlyphOfWarding] = 3;
            spellLevels[SpellConstants.HelpingHand] = 3;
            spellLevels[SpellConstants.InvisibilityPurge] = 3;
            spellLevels[SpellConstants.LocateObject] = 3;
            spellLevels[SpellConstants.MagicCircleAgainstAlignment] = 3;
            spellLevels[SpellConstants.MagicVestment] = 3;
            spellLevels[SpellConstants.MeldIntoStone] = 3;
            spellLevels[SpellConstants.ObscureObject] = 3;
            spellLevels[SpellConstants.Prayer] = 3;
            spellLevels[SpellConstants.ProtectionFromEnergy] = 3;
            spellLevels[SpellConstants.RemoveBlindnessDeafness] = 3;
            spellLevels[SpellConstants.RemoveCurse] = 3;
            spellLevels[SpellConstants.RemoveDisease] = 3;
            spellLevels[SpellConstants.SearingLight] = 3;
            spellLevels[SpellConstants.SpeakWithDead] = 3;
            spellLevels[SpellConstants.StoneShape] = 3;
            spellLevels[SpellConstants.SummonMonsterIII] = 3;
            spellLevels[SpellConstants.WaterBreathing] = 3;
            spellLevels[SpellConstants.WaterWalk] = 3;
            spellLevels[SpellConstants.WindWall] = 3;

            //4th - Level Cleric Spells
            spellLevels[SpellConstants.AirWalk] = 4;
            spellLevels[SpellConstants.ControlWater] = 4;
            spellLevels[SpellConstants.CureCriticalWounds] = 4;
            spellLevels[SpellConstants.InflictCriticalWounds] = 4;
            spellLevels[SpellConstants.DeathWard] = 4;
            spellLevels[SpellConstants.DimensionalAnchor] = 4;
            spellLevels[SpellConstants.DiscernLies] = 4;
            spellLevels[SpellConstants.Dismissal] = 4;
            spellLevels[SpellConstants.Divination] = 4;
            spellLevels[SpellConstants.DivinePower] = 4;
            spellLevels[SpellConstants.FreedomOfMovement] = 4;
            spellLevels[SpellConstants.GiantVermin] = 4;
            spellLevels[SpellConstants.ImbueWithSpellAbility] = 4;
            spellLevels[SpellConstants.MagicWeapon_Greater] = 4;
            spellLevels[SpellConstants.NeutralizePoison] = 4;
            spellLevels[SpellConstants.PlanarAlly_Lesser] = 4;
            spellLevels[SpellConstants.Poison] = 4;
            spellLevels[SpellConstants.RepelVermin] = 4;
            spellLevels[SpellConstants.Restoration] = 4;
            spellLevels[SpellConstants.Sending] = 4;
            spellLevels[SpellConstants.SpellImmunity] = 4;
            spellLevels[SpellConstants.SummonMonsterIV] = 4;
            spellLevels[SpellConstants.Tongues] = 4;

            //5th - Level Cleric Spells
            spellLevels[SpellConstants.Atonement] = 5;
            spellLevels[SpellConstants.BreakEnchantment] = 5;
            spellLevels[SpellConstants.Command_Greater] = 5;
            spellLevels[SpellConstants.Commune] = 5;
            spellLevels[SpellConstants.InflictLightWounds_Mass] = 5;
            spellLevels[SpellConstants.CureLightWounds_Mass] = 5;
            spellLevels[SpellConstants.DispelAlignment] = 5;
            spellLevels[SpellConstants.DisruptingWeapon] = 5;
            spellLevels[SpellConstants.FlameStrike] = 5;
            spellLevels[SpellConstants.Hallow] = 5;
            spellLevels[SpellConstants.InsectPlague] = 5;
            spellLevels[SpellConstants.MarkOfJustice] = 5;
            spellLevels[SpellConstants.PlaneShift] = 5;
            spellLevels[SpellConstants.RaiseDead] = 5;
            spellLevels[SpellConstants.RighteousMight] = 5;
            spellLevels[SpellConstants.Scrying] = 5;
            spellLevels[SpellConstants.SlayLiving] = 5;
            spellLevels[SpellConstants.SpellResistance] = 5;
            spellLevels[SpellConstants.SummonMonsterV] = 5;
            spellLevels[SpellConstants.SymbolOfPain] = 5;
            spellLevels[SpellConstants.SymbolOfSleep] = 5;
            spellLevels[SpellConstants.TrueSeeing] = 5;
            spellLevels[SpellConstants.Unhallow] = 5;
            spellLevels[SpellConstants.WallOfStone] = 5;

            //6th - Level Cleric Spells
            spellLevels[SpellConstants.AnimateObjects] = 6;
            spellLevels[SpellConstants.AntilifeShell] = 6;
            spellLevels[SpellConstants.Banishment] = 6;
            spellLevels[SpellConstants.BearsEndurance_Mass] = 6;
            spellLevels[SpellConstants.BladeBarrier] = 6;
            spellLevels[SpellConstants.BullsStrength_Mass] = 6;
            spellLevels[SpellConstants.CreateUndead] = 6;
            spellLevels[SpellConstants.CureModerateWounds_Mass] = 6;
            spellLevels[SpellConstants.InflictModerateWounds_Mass] = 6;
            spellLevels[SpellConstants.DispelMagic_Greater] = 6;
            spellLevels[SpellConstants.EaglesSplendor_Mass] = 6;
            spellLevels[SpellConstants.FindThePath] = 6;
            spellLevels[SpellConstants.Forbiddance] = 6;
            spellLevels[SpellConstants.GeasQuest] = 6;
            spellLevels[SpellConstants.GlyphOfWarding_Greater] = 6;
            spellLevels[SpellConstants.Heal] = 6;
            spellLevels[SpellConstants.Harm] = 6;
            spellLevels[SpellConstants.HeroesFeast] = 6;
            spellLevels[SpellConstants.OwlsWisdom_Mass] = 6;
            spellLevels[SpellConstants.PlanarAlly] = 6;
            spellLevels[SpellConstants.SummonMonsterVI] = 6;
            spellLevels[SpellConstants.SymbolOfFear] = 6;
            spellLevels[SpellConstants.SymbolOfPersuasion] = 6;
            spellLevels[SpellConstants.UndeathToDeath] = 6;
            spellLevels[SpellConstants.WindWalk] = 6;
            spellLevels[SpellConstants.WordOfRecall] = 6;

            //7th - Level Cleric Spells
            spellLevels[SpellConstants.Blasphemy] = 7;
            spellLevels[SpellConstants.ControlWeather] = 7;
            spellLevels[SpellConstants.CureSeriousWounds_Mass] = 7;
            spellLevels[SpellConstants.InflictSeriousWounds_Mass] = 7;
            spellLevels[SpellConstants.Destruction] = 7;
            spellLevels[SpellConstants.Dictum] = 7;
            spellLevels[SpellConstants.EtherealJaunt] = 7;
            spellLevels[SpellConstants.HolyWord] = 7;
            spellLevels[SpellConstants.Refuge] = 7;
            spellLevels[SpellConstants.Regenerate] = 7;
            spellLevels[SpellConstants.Repulsion] = 7;
            spellLevels[SpellConstants.Restoration_Greater] = 7;
            spellLevels[SpellConstants.Resurrection] = 7;
            spellLevels[SpellConstants.Scrying_Greater] = 7;
            spellLevels[SpellConstants.SummonMonsterVII] = 7;
            spellLevels[SpellConstants.SymbolOfStunning] = 7;
            spellLevels[SpellConstants.SymbolOfWeakness] = 7;
            spellLevels[SpellConstants.WordOfChaos] = 7;

            //8th - Level Cleric Spells
            spellLevels[SpellConstants.AntimagicField] = 8;
            spellLevels[SpellConstants.CloakOfChaos] = 8;
            spellLevels[SpellConstants.CreateGreaterUndead] = 8;
            spellLevels[SpellConstants.CureCriticalWounds_Mass] = 8;
            spellLevels[SpellConstants.InflictCriticalWounds_Mass] = 8;
            spellLevels[SpellConstants.DimensionalLock] = 8;
            spellLevels[SpellConstants.DiscernLocation] = 8;
            spellLevels[SpellConstants.Earthquake] = 8;
            spellLevels[SpellConstants.FireStorm] = 8;
            spellLevels[SpellConstants.HolyAura] = 8;
            spellLevels[SpellConstants.PlanarAlly_Greater] = 8;
            spellLevels[SpellConstants.ShieldOfLaw] = 8;
            spellLevels[SpellConstants.SpellImmunity_Greater] = 8;
            spellLevels[SpellConstants.SummonMonsterVIII] = 8;
            spellLevels[SpellConstants.SymbolOfDeath] = 8;
            spellLevels[SpellConstants.SymbolOfInsanity] = 8;
            spellLevels[SpellConstants.UnholyAura] = 8;

            //9th - Level Cleric Spells
            spellLevels[SpellConstants.AstralProjection] = 9;
            spellLevels[SpellConstants.EnergyDrain] = 9;
            spellLevels[SpellConstants.Etherealness] = 9;
            spellLevels[SpellConstants.Gate] = 9;
            spellLevels[SpellConstants.HealHarm_Mass] = 9;
            spellLevels[SpellConstants.Implosion] = 9;
            spellLevels[SpellConstants.Miracle] = 9;
            spellLevels[SpellConstants.SoulBind] = 9;
            spellLevels[SpellConstants.StormOfVengeance] = 9;
            spellLevels[SpellConstants.SummonMonsterIX] = 9;
            spellLevels[SpellConstants.TrueResurrection] = 9;

            AssertTypesAndAmounts(SpellConstants.Casters.Cleric, spellLevels);
        }

        [Test]
        public void DruidSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            //0 - Level Druid Spells(Orisons)
            spellLevels[SpellConstants.CreateWater] = 0;
            spellLevels[SpellConstants.CureMinorWounds] = 0;
            spellLevels[SpellConstants.DetectMagic] = 0;
            spellLevels[SpellConstants.DetectPoison] = 0;
            spellLevels[SpellConstants.Flare] = 0;
            spellLevels[SpellConstants.Guidance] = 0;
            spellLevels[SpellConstants.KnowDirection] = 0;
            spellLevels[SpellConstants.Light] = 0;
            spellLevels[SpellConstants.Mending] = 0;
            spellLevels[SpellConstants.PurifyFoodAndDrink] = 0;
            spellLevels[SpellConstants.ReadMagic] = 0;
            spellLevels[SpellConstants.Resistance] = 0;
            spellLevels[SpellConstants.Virtue] = 0;

            //1st - Level Druid Spells
            spellLevels[SpellConstants.CalmAnimals] = 1;
            spellLevels[SpellConstants.CharmAnimal] = 1;
            spellLevels[SpellConstants.CureLightWounds] = 1;
            spellLevels[SpellConstants.DetectAnimalsOrPlants] = 1;
            spellLevels[SpellConstants.DetectSnaresAndPits] = 1;
            spellLevels[SpellConstants.EndureElements] = 1;
            spellLevels[SpellConstants.Entangle] = 1;
            spellLevels[SpellConstants.FaerieFire] = 1;
            spellLevels[SpellConstants.Goodberry] = 1;
            spellLevels[SpellConstants.HideFromAnimals] = 1;
            spellLevels[SpellConstants.Jump] = 1;
            spellLevels[SpellConstants.Longstrider] = 1;
            spellLevels[SpellConstants.MagicFang] = 1;
            spellLevels[SpellConstants.MagicStone] = 1;
            spellLevels[SpellConstants.ObscuringMist] = 1;
            spellLevels[SpellConstants.PassWithoutTrace] = 1;
            spellLevels[SpellConstants.ProduceFlame] = 1;
            spellLevels[SpellConstants.Shillelagh] = 1;
            spellLevels[SpellConstants.SpeakWithAnimals] = 1;
            spellLevels[SpellConstants.SummonNaturesAllyI] = 1;

            //2nd - Level Druid Spells
            spellLevels[SpellConstants.AnimalMessenger] = 2;
            spellLevels[SpellConstants.AnimalTrance] = 2;
            spellLevels[SpellConstants.Barkskin] = 2;
            spellLevels[SpellConstants.BearsEndurance] = 2;
            spellLevels[SpellConstants.BullsStrength] = 2;
            spellLevels[SpellConstants.CatsGrace] = 2;
            spellLevels[SpellConstants.ChillMetal] = 2;
            spellLevels[SpellConstants.DelayPoison] = 2;
            spellLevels[SpellConstants.FireTrap] = 2;
            spellLevels[SpellConstants.FlameBlade] = 2;
            spellLevels[SpellConstants.FlamingSphere] = 2;
            spellLevels[SpellConstants.FogCloud] = 2;
            spellLevels[SpellConstants.GustOfWind] = 2;
            spellLevels[SpellConstants.HeatMetal] = 2;
            spellLevels[SpellConstants.HoldAnimal] = 2;
            spellLevels[SpellConstants.OwlsWisdom] = 2;
            spellLevels[SpellConstants.ReduceAnimal] = 2;
            spellLevels[SpellConstants.ResistEnergy] = 2;
            spellLevels[SpellConstants.Restoration_Lesser] = 2;
            spellLevels[SpellConstants.SoftenEarthAndStone] = 2;
            spellLevels[SpellConstants.SpiderClimb] = 2;
            spellLevels[SpellConstants.SummonNaturesAllyII] = 2;
            spellLevels[SpellConstants.SummonSwarm] = 2;
            spellLevels[SpellConstants.TreeShape] = 2;
            spellLevels[SpellConstants.WarpWood] = 2;
            spellLevels[SpellConstants.WoodShape] = 2;

            //3rd - Level Druid Spells
            spellLevels[SpellConstants.CallLightning] = 3;
            spellLevels[SpellConstants.Contagion] = 3;
            spellLevels[SpellConstants.CureModerateWounds] = 3;
            spellLevels[SpellConstants.Daylight] = 3;
            spellLevels[SpellConstants.DiminishPlants] = 3;
            spellLevels[SpellConstants.DominateAnimal] = 3;
            spellLevels[SpellConstants.MagicFang_Greater] = 3;
            spellLevels[SpellConstants.MeldIntoStone] = 3;
            spellLevels[SpellConstants.NeutralizePoison] = 3;
            spellLevels[SpellConstants.PlantGrowth] = 3;
            spellLevels[SpellConstants.Poison] = 3;
            spellLevels[SpellConstants.ProtectionFromEnergy] = 3;
            spellLevels[SpellConstants.Quench] = 3;
            spellLevels[SpellConstants.RemoveDisease] = 3;
            spellLevels[SpellConstants.SleetStorm] = 3;
            spellLevels[SpellConstants.Snare] = 3;
            spellLevels[SpellConstants.SpeakWithPlants] = 3;
            spellLevels[SpellConstants.SpikeGrowth] = 3;
            spellLevels[SpellConstants.StoneShape] = 3;
            spellLevels[SpellConstants.SummonNaturesAllyIII] = 3;
            spellLevels[SpellConstants.WaterBreathing] = 3;
            spellLevels[SpellConstants.WindWall] = 3;

            //4th - Level Druid Spells
            spellLevels[SpellConstants.AirWalk] = 4;
            spellLevels[SpellConstants.AntiplantShell] = 4;
            spellLevels[SpellConstants.Blight] = 4;
            spellLevels[SpellConstants.CommandPlants] = 4;
            spellLevels[SpellConstants.ControlWater] = 4;
            spellLevels[SpellConstants.CureSeriousWounds] = 4;
            spellLevels[SpellConstants.DispelMagic] = 4;
            spellLevels[SpellConstants.FlameStrike] = 4;
            spellLevels[SpellConstants.FreedomOfMovement] = 4;
            spellLevels[SpellConstants.GiantVermin] = 4;
            spellLevels[SpellConstants.IceStorm] = 4;
            spellLevels[SpellConstants.Reincarnate] = 4;
            spellLevels[SpellConstants.RepelVermin] = 4;
            spellLevels[SpellConstants.RustingGrasp] = 4;
            spellLevels[SpellConstants.Scrying] = 4;
            spellLevels[SpellConstants.SpikeStones] = 4;
            spellLevels[SpellConstants.SummonNaturesAllyIV] = 4;

            //5th - Level Druid Spells
            spellLevels[SpellConstants.AnimalGrowth] = 5;
            spellLevels[SpellConstants.Atonement] = 5;
            spellLevels[SpellConstants.Awaken] = 5;
            spellLevels[SpellConstants.BalefulPolymorph] = 5;
            spellLevels[SpellConstants.CallLightningStorm] = 5;
            spellLevels[SpellConstants.CommuneWithNature] = 5;
            spellLevels[SpellConstants.ControlWinds] = 5;
            spellLevels[SpellConstants.CureCriticalWounds] = 5;
            spellLevels[SpellConstants.DeathWard] = 5;
            spellLevels[SpellConstants.Hallow] = 5;
            spellLevels[SpellConstants.InsectPlague] = 5;
            spellLevels[SpellConstants.Stoneskin] = 5;
            spellLevels[SpellConstants.SummonNaturesAllyV] = 5;
            spellLevels[SpellConstants.TransmuteMudToRock] = 5;
            spellLevels[SpellConstants.TransmuteRockToMud] = 5;
            spellLevels[SpellConstants.TreeStride] = 5;
            spellLevels[SpellConstants.Unhallow] = 5;
            spellLevels[SpellConstants.WallOfFire] = 5;
            spellLevels[SpellConstants.WallOfThorns] = 5;

            //6th - Level Druid Spells
            spellLevels[SpellConstants.AntilifeShell] = 6;
            spellLevels[SpellConstants.BearsEndurance_Mass] = 6;
            spellLevels[SpellConstants.BullsStrength_Mass] = 6;
            spellLevels[SpellConstants.CatsGrace_Mass] = 6;
            spellLevels[SpellConstants.CureLightWounds_Mass] = 6;
            spellLevels[SpellConstants.DispelMagic_Greater] = 6;
            spellLevels[SpellConstants.FindThePath] = 6;
            spellLevels[SpellConstants.FireSeeds] = 6;
            spellLevels[SpellConstants.Ironwood] = 6;
            spellLevels[SpellConstants.Liveoak] = 6;
            spellLevels[SpellConstants.MoveEarth] = 6;
            spellLevels[SpellConstants.OwlsWisdom_Mass] = 6;
            spellLevels[SpellConstants.RepelWood] = 6;
            spellLevels[SpellConstants.Spellstaff] = 6;
            spellLevels[SpellConstants.StoneTell] = 6;
            spellLevels[SpellConstants.SummonNaturesAllyVI] = 6;
            spellLevels[SpellConstants.TransportViaPlants] = 6;
            spellLevels[SpellConstants.WallOfStone] = 6;

            //7th - Level Druid Spells
            spellLevels[SpellConstants.AnimatePlants] = 7;
            spellLevels[SpellConstants.Changestaff] = 7;
            spellLevels[SpellConstants.ControlWeather] = 7;
            spellLevels[SpellConstants.CreepingDoom] = 7;
            spellLevels[SpellConstants.CureModerateWounds_Mass] = 7;
            spellLevels[SpellConstants.FireStorm] = 7;
            spellLevels[SpellConstants.Heal] = 7;
            spellLevels[SpellConstants.Scrying_Greater] = 7;
            spellLevels[SpellConstants.SummonNaturesAllyVII] = 7;
            spellLevels[SpellConstants.Sunbeam] = 7;
            spellLevels[SpellConstants.TransmuteMetalToWood] = 7;
            spellLevels[SpellConstants.TrueSeeing] = 7;
            spellLevels[SpellConstants.WindWalk] = 7;

            //8th - Level Druid Spells
            spellLevels[SpellConstants.AnimalShapes] = 8;
            spellLevels[SpellConstants.ControlPlants] = 8;
            spellLevels[SpellConstants.CureSeriousWounds_Mass] = 8;
            spellLevels[SpellConstants.Earthquake] = 8;
            spellLevels[SpellConstants.FingerOfDeath] = 8;
            spellLevels[SpellConstants.RepelMetalOrStone] = 8;
            spellLevels[SpellConstants.ReverseGravity] = 8;
            spellLevels[SpellConstants.SummonNaturesAllyVIII] = 8;
            spellLevels[SpellConstants.Sunburst] = 8;
            spellLevels[SpellConstants.Whirlwind] = 8;
            spellLevels[SpellConstants.WordOfRecall] = 8;

            //9th - Level Druid Spells
            spellLevels[SpellConstants.Antipathy] = 9;
            spellLevels[SpellConstants.CureCriticalWounds_Mass] = 9;
            spellLevels[SpellConstants.ElementalSwarm] = 9;
            spellLevels[SpellConstants.Foresight] = 9;
            spellLevels[SpellConstants.Regenerate] = 9;
            spellLevels[SpellConstants.Shambler] = 9;
            spellLevels[SpellConstants.Shapechange] = 9;
            spellLevels[SpellConstants.StormOfVengeance] = 9;
            spellLevels[SpellConstants.SummonNaturesAllyIX] = 9;
            spellLevels[SpellConstants.Sympathy] = 9;

            AssertTypesAndAmounts(SpellConstants.Casters.Druid, spellLevels);
        }

        [Test]
        public void SorcererSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Casters.Sorcerer, spellLevels);
        }

        [Test]
        public void AirSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Air, spellLevels);
        }

        [Test]
        public void AnimalSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Animal, spellLevels);
        }

        [Test]
        public void ChaospellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Chaos, spellLevels);
        }

        [Test]
        public void DestructionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Destruction, spellLevels);
        }

        [Test]
        public void EarthSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Earth, spellLevels);
        }

        [Test]
        public void EnchantmentSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Enchantment, spellLevels);
        }

        [Test]
        public void EvilSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Evil, spellLevels);
        }

        [Test]
        public void FireSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Fire, spellLevels);
        }

        [Test]
        public void GoodSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Good, spellLevels);
        }

        [Test]
        public void HealingSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Healing, spellLevels);
        }

        [Test]
        public void IllusionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Illusion, spellLevels);
        }

        [Test]
        public void KnowledgeSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Knowledge, spellLevels);
        }

        [Test]
        public void LawSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Law, spellLevels);
        }

        [Test]
        public void LuckSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Luck, spellLevels);
        }

        [Test]
        public void PlantSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Plant, spellLevels);
        }

        [Test]
        public void ProtectionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Protection, spellLevels);
        }

        [Test]
        public void SunSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Sun, spellLevels);
        }

        [Test]
        public void TrickerySpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Trickery, spellLevels);
        }

        [Test]
        public void WarSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.War, spellLevels);
        }

        [Test]
        public void WaterSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>();

            Assert.Fail("need to add spells");
            AssertTypesAndAmounts(SpellConstants.Domains.Water, spellLevels);
        }
    }
}
