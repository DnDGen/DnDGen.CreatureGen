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
        protected override string TableName => TableNameConstants.TypeAndAmount.SpellLevels;

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
            var spellLevels = new Dictionary<string, int>
            {
                //0-Level Bard Spells (Cantrips)
                [SpellConstants.DancingLights] = 0,
                [SpellConstants.Daze] = 0,
                [SpellConstants.DetectMagic] = 0,
                [SpellConstants.Flare] = 0,
                [SpellConstants.GhostSound] = 0,
                [SpellConstants.KnowDirection] = 0,
                [SpellConstants.Light] = 0,
                [SpellConstants.Lullaby] = 0,
                [SpellConstants.MageHand] = 0,
                [SpellConstants.Mending] = 0,
                [SpellConstants.Message] = 0,
                [SpellConstants.OpenClose] = 0,
                [SpellConstants.Prestidigitation] = 0,
                [SpellConstants.ReadMagic] = 0,
                [SpellConstants.Resistance] = 0,
                [SpellConstants.SummonInstrument] = 0,

                //1st-Level Bard Spells
                [SpellConstants.Alarm] = 1,
                [SpellConstants.AnimateRope] = 1,
                [SpellConstants.CauseFear] = 1,
                [SpellConstants.CharmPerson] = 1,
                [SpellConstants.ComprehendLanguages] = 1,
                [SpellConstants.Confusion_Lesser] = 1,
                [SpellConstants.CureLightWounds] = 1,
                [SpellConstants.DetectSecretDoors] = 1,
                [SpellConstants.DisguiseSelf] = 1,
                [SpellConstants.Erase] = 1,
                [SpellConstants.ExpeditiousRetreat] = 1,
                [SpellConstants.FeatherFall] = 1,
                [SpellConstants.Grease] = 1,
                [SpellConstants.HideousLaughter] = 1,
                [SpellConstants.Hypnotism] = 1,
                [SpellConstants.Identify] = 1,
                [SpellConstants.MagicMouth] = 1,
                [SpellConstants.MagicAura] = 1,
                [SpellConstants.ObscureObject] = 1,
                [SpellConstants.RemoveFear] = 1,
                [SpellConstants.SilentImage] = 1,
                [SpellConstants.Sleep] = 1,
                [SpellConstants.SummonMonsterI] = 1,
                [SpellConstants.UndetectableAlignment] = 1,
                [SpellConstants.UnseenServant] = 1,
                [SpellConstants.Ventriloquism] = 1,

                //2nd-Level Bard Spells
                [SpellConstants.AlterSelf] = 2,
                [SpellConstants.AnimalMessenger] = 2,
                [SpellConstants.AnimalTrance] = 2,
                [SpellConstants.BlindnessDeafness] = 2,
                [SpellConstants.Blur] = 2,
                [SpellConstants.CalmEmotions] = 2,
                [SpellConstants.CatsGrace] = 2,
                [SpellConstants.CureModerateWounds] = 2,
                [SpellConstants.Darkness] = 2,
                [SpellConstants.DazeMonster] = 2,
                [SpellConstants.DelayPoison] = 2,
                [SpellConstants.DetectThoughts] = 2,
                [SpellConstants.EaglesSplendor] = 2,
                [SpellConstants.Enthrall] = 2,
                [SpellConstants.FoxsCunning] = 2,
                [SpellConstants.Glitterdust] = 2,
                [SpellConstants.Heroism] = 2,
                [SpellConstants.HoldPerson] = 2,
                [SpellConstants.HypnoticPattern] = 2,
                [SpellConstants.Invisibility] = 2,
                [SpellConstants.LocateObject] = 2,
                [SpellConstants.MinorImage] = 2,
                [SpellConstants.MirrorImage] = 2,
                [SpellConstants.Misdirection] = 2,
                [SpellConstants.Pyrotechnics] = 2,
                [SpellConstants.Rage] = 2,
                [SpellConstants.Scare] = 2,
                [SpellConstants.Shatter] = 2,
                [SpellConstants.Silence] = 2,
                [SpellConstants.SoundBurst] = 2,
                [SpellConstants.Suggestion] = 2,
                [SpellConstants.SummonMonsterII] = 2,
                [SpellConstants.SummonSwarm] = 2,
                [SpellConstants.Tongues] = 2,
                [SpellConstants.WhisperingWind] = 2,

                //3rd-Level Bard Spells
                [SpellConstants.Blink] = 3,
                [SpellConstants.CharmMonster] = 3,
                [SpellConstants.ClairaudienceClairvoyance] = 3,
                [SpellConstants.Confusion] = 3,
                [SpellConstants.CrushingDespair] = 3,
                [SpellConstants.CureSeriousWounds] = 3,
                [SpellConstants.Daylight] = 3,
                [SpellConstants.DeepSlumber] = 3,
                [SpellConstants.DispelMagic] = 3,
                [SpellConstants.Displacement] = 3,
                [SpellConstants.Fear] = 3,
                [SpellConstants.GaseousForm] = 3,
                [SpellConstants.Geas_Lesser] = 3,
                [SpellConstants.Glibness] = 3,
                [SpellConstants.GoodHope] = 3,
                [SpellConstants.Haste] = 3,
                [SpellConstants.IllusoryScript] = 3,
                [SpellConstants.InvisibilitySphere] = 3,
                [SpellConstants.MajorImage] = 3,
                [SpellConstants.PhantomSteed] = 3,
                [SpellConstants.RemoveCurse] = 3,
                [SpellConstants.Scrying] = 3,
                [SpellConstants.SculptSound] = 3,
                [SpellConstants.SecretPage] = 3,
                [SpellConstants.SeeInvisibility] = 3,
                [SpellConstants.SepiaSnakeSigil] = 3,
                [SpellConstants.Slow] = 3,
                [SpellConstants.SpeakWithAnimals] = 3,
                [SpellConstants.SummonMonsterIII] = 3,
                [SpellConstants.TinyHut] = 3,

                //4th-Level Bard Spells
                [SpellConstants.BreakEnchantment] = 4,
                [SpellConstants.CureCriticalWounds] = 4,
                [SpellConstants.DetectScrying] = 4,
                [SpellConstants.DimensionDoor] = 4,
                [SpellConstants.DominatePerson] = 4,
                [SpellConstants.FreedomOfMovement] = 4,
                [SpellConstants.HallucinatoryTerrain] = 4,
                [SpellConstants.HoldMonster] = 4,
                [SpellConstants.Invisibility_Greater] = 4,
                [SpellConstants.LegendLore] = 4,
                [SpellConstants.LocateCreature] = 4,
                [SpellConstants.ModifyMemory] = 4,
                [SpellConstants.NeutralizePoison] = 4,
                [SpellConstants.RainbowPattern] = 4,
                [SpellConstants.RepelVermin] = 4,
                [SpellConstants.SecureShelter] = 4,
                [SpellConstants.ShadowConjuration] = 4,
                [SpellConstants.Shout] = 4,
                [SpellConstants.SpeakWithPlants] = 4,
                [SpellConstants.SummonMonsterIV] = 4,
                [SpellConstants.ZoneOfSilence] = 4,

                //5th - Level Bard Spells
                [SpellConstants.CureLightWounds_Mass] = 5,
                [SpellConstants.DispelMagic_Greater] = 5,
                [SpellConstants.Dream] = 5,
                [SpellConstants.FalseVision] = 5,
                [SpellConstants.Heroism_Greater] = 5,
                [SpellConstants.MindFog] = 5,
                [SpellConstants.MirageArcana] = 5,
                [SpellConstants.Mislead] = 5,
                [SpellConstants.Nightmare] = 5,
                [SpellConstants.PersistentImage] = 5,
                [SpellConstants.Seeming] = 5,
                [SpellConstants.ShadowEvocation] = 5,
                [SpellConstants.ShadowWalk] = 5,
                [SpellConstants.SongOfDiscord] = 5,
                [SpellConstants.Suggestion_Mass] = 5,
                [SpellConstants.SummonMonsterV] = 5,

                //6th - Level Bard Spells
                [SpellConstants.AnalyzeDweomer] = 6,
                [SpellConstants.AnimateObjects] = 6,
                [SpellConstants.CatsGrace_Mass] = 6,
                [SpellConstants.CharmMonster_Mass] = 6,
                [SpellConstants.CureModerateWounds_Mass] = 6,
                [SpellConstants.EaglesSplendor_Mass] = 6,
                [SpellConstants.Eyebite] = 6,
                [SpellConstants.FindThePath] = 6,
                [SpellConstants.FoxsCunning_Mass] = 6,
                [SpellConstants.GeasQuest] = 6,
                [SpellConstants.HeroesFeast] = 6,
                [SpellConstants.IrresistibleDance] = 6,
                [SpellConstants.PermanentImage] = 6,
                [SpellConstants.ProgrammedImage] = 6,
                [SpellConstants.ProjectImage] = 6,
                [SpellConstants.Scrying_Greater] = 6,
                [SpellConstants.Shout_Greater] = 6,
                [SpellConstants.SummonMonsterVI] = 6,
                [SpellConstants.SympatheticVibration] = 6,
                [SpellConstants.Veil] = 6
            };

            AssertTypesAndAmounts(SpellConstants.Casters.Bard, spellLevels);
        }

        [Test]
        public void ClericSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                //0 - Level Cleric Spells(Orisons)
                [SpellConstants.CreateWater] = 0,
                [SpellConstants.CureMinorWounds] = 0,
                [SpellConstants.InflictMinorWounds] = 0,
                [SpellConstants.DetectMagic] = 0,
                [SpellConstants.DetectPoison] = 0,
                [SpellConstants.Guidance] = 0,
                [SpellConstants.Light] = 0,
                [SpellConstants.Mending] = 0,
                [SpellConstants.PurifyFoodAndDrink] = 0,
                [SpellConstants.ReadMagic] = 0,
                [SpellConstants.Resistance] = 0,
                [SpellConstants.Virtue] = 0,

                //1st - Level Cleric Spells
                [SpellConstants.Bane] = 1,
                [SpellConstants.Bless] = 1,
                [SpellConstants.BlessWater] = 1,
                [SpellConstants.CauseFear] = 1,
                [SpellConstants.Command] = 1,
                [SpellConstants.ComprehendLanguages] = 1,
                [SpellConstants.CureLightWounds] = 1,
                [SpellConstants.InflictLightWounds] = 1,
                [SpellConstants.CurseWater] = 1,
                [SpellConstants.Deathwatch] = 1,
                [SpellConstants.DetectChaos] = 1,
                [SpellConstants.DetectEvil] = 1,
                [SpellConstants.DetectGood] = 1,
                [SpellConstants.DetectLaw] = 1,
                [SpellConstants.DetectUndead] = 1,
                [SpellConstants.DivineFavor] = 1,
                [SpellConstants.Doom] = 1,
                [SpellConstants.EndureElements] = 1,
                [SpellConstants.EntropicShield] = 1,
                [SpellConstants.HideFromUndead] = 1,
                [SpellConstants.MagicStone] = 1,
                [SpellConstants.MagicWeapon] = 1,
                [SpellConstants.ObscuringMist] = 1,
                [SpellConstants.ProtectionFromChaos] = 1,
                [SpellConstants.ProtectionFromEvil] = 1,
                [SpellConstants.ProtectionFromGood] = 1,
                [SpellConstants.ProtectionFromLaw] = 1,
                [SpellConstants.RemoveFear] = 1,
                [SpellConstants.Sanctuary] = 1,
                [SpellConstants.ShieldOfFaith] = 1,
                [SpellConstants.SummonMonsterI] = 1,

                //2nd - Level Cleric Spells
                [SpellConstants.Aid] = 2,
                [SpellConstants.AlignWeapon] = 2,
                [SpellConstants.Augury] = 2,
                [SpellConstants.BearsEndurance] = 2,
                [SpellConstants.BullsStrength] = 2,
                [SpellConstants.CalmEmotions] = 2,
                [SpellConstants.Consecrate] = 2,
                [SpellConstants.CureModerateWounds] = 2,
                [SpellConstants.InflictModerateWounds] = 2,
                [SpellConstants.Darkness] = 2,
                [SpellConstants.DeathKnell] = 2,
                [SpellConstants.DelayPoison] = 2,
                [SpellConstants.Desecrate] = 2,
                [SpellConstants.EaglesSplendor] = 2,
                [SpellConstants.Enthrall] = 2,
                [SpellConstants.FindTraps] = 2,
                [SpellConstants.GentleRepose] = 2,
                [SpellConstants.HoldPerson] = 2,
                [SpellConstants.MakeWhole] = 2,
                [SpellConstants.OwlsWisdom] = 2,
                [SpellConstants.RemoveParalysis] = 2,
                [SpellConstants.ResistEnergy] = 2,
                [SpellConstants.Restoration_Lesser] = 2,
                [SpellConstants.Shatter] = 2,
                [SpellConstants.ShieldOther] = 2,
                [SpellConstants.Silence] = 2,
                [SpellConstants.SoundBurst] = 2,
                [SpellConstants.SpiritualWeapon] = 2,
                [SpellConstants.Status] = 2,
                [SpellConstants.SummonMonsterII] = 2,
                [SpellConstants.UndetectableAlignment] = 2,
                [SpellConstants.ZoneOfTruth] = 2,

                //3rd - Level Cleric Spells
                [SpellConstants.AnimateDead] = 3,
                [SpellConstants.BestowCurse] = 3,
                [SpellConstants.BlindnessDeafness] = 3,
                [SpellConstants.Contagion] = 3,
                [SpellConstants.ContinualFlame] = 3,
                [SpellConstants.CreateFoodAndWater] = 3,
                [SpellConstants.CureSeriousWounds] = 3,
                [SpellConstants.InflictSeriousWounds] = 3,
                [SpellConstants.Daylight] = 3,
                [SpellConstants.DeeperDarkness] = 3,
                [SpellConstants.DispelMagic] = 3,
                [SpellConstants.GlyphOfWarding] = 3,
                [SpellConstants.HelpingHand] = 3,
                [SpellConstants.InvisibilityPurge] = 3,
                [SpellConstants.LocateObject] = 3,
                [SpellConstants.MagicCircleAgainstChaos] = 3,
                [SpellConstants.MagicCircleAgainstEvil] = 3,
                [SpellConstants.MagicCircleAgainstGood] = 3,
                [SpellConstants.MagicCircleAgainstLaw] = 3,
                [SpellConstants.MagicVestment] = 3,
                [SpellConstants.MeldIntoStone] = 3,
                [SpellConstants.ObscureObject] = 3,
                [SpellConstants.Prayer] = 3,
                [SpellConstants.ProtectionFromEnergy] = 3,
                [SpellConstants.RemoveBlindnessDeafness] = 3,
                [SpellConstants.RemoveCurse] = 3,
                [SpellConstants.RemoveDisease] = 3,
                [SpellConstants.SearingLight] = 3,
                [SpellConstants.SpeakWithDead] = 3,
                [SpellConstants.StoneShape] = 3,
                [SpellConstants.SummonMonsterIII] = 3,
                [SpellConstants.WaterBreathing] = 3,
                [SpellConstants.WaterWalk] = 3,
                [SpellConstants.WindWall] = 3,

                //4th - Level Cleric Spells
                [SpellConstants.AirWalk] = 4,
                [SpellConstants.ControlWater] = 4,
                [SpellConstants.CureCriticalWounds] = 4,
                [SpellConstants.InflictCriticalWounds] = 4,
                [SpellConstants.DeathWard] = 4,
                [SpellConstants.DimensionalAnchor] = 4,
                [SpellConstants.DiscernLies] = 4,
                [SpellConstants.Dismissal] = 4,
                [SpellConstants.Divination] = 4,
                [SpellConstants.DivinePower] = 4,
                [SpellConstants.FreedomOfMovement] = 4,
                [SpellConstants.GiantVermin] = 4,
                [SpellConstants.ImbueWithSpellAbility] = 4,
                [SpellConstants.MagicWeapon_Greater] = 4,
                [SpellConstants.NeutralizePoison] = 4,
                [SpellConstants.PlanarAlly_Lesser] = 4,
                [SpellConstants.Poison] = 4,
                [SpellConstants.RepelVermin] = 4,
                [SpellConstants.Restoration] = 4,
                [SpellConstants.Sending] = 4,
                [SpellConstants.SpellImmunity] = 4,
                [SpellConstants.SummonMonsterIV] = 4,
                [SpellConstants.Tongues] = 4,

                //5th - Level Cleric Spells
                [SpellConstants.Atonement] = 5,
                [SpellConstants.BreakEnchantment] = 5,
                [SpellConstants.Command_Greater] = 5,
                [SpellConstants.Commune] = 5,
                [SpellConstants.InflictLightWounds_Mass] = 5,
                [SpellConstants.CureLightWounds_Mass] = 5,
                [SpellConstants.DispelChaos] = 5,
                [SpellConstants.DispelEvil] = 5,
                [SpellConstants.DispelGood] = 5,
                [SpellConstants.DispelLaw] = 5,
                [SpellConstants.DisruptingWeapon] = 5,
                [SpellConstants.FlameStrike] = 5,
                [SpellConstants.Hallow] = 5,
                [SpellConstants.InsectPlague] = 5,
                [SpellConstants.MarkOfJustice] = 5,
                [SpellConstants.PlaneShift] = 5,
                [SpellConstants.RaiseDead] = 5,
                [SpellConstants.RighteousMight] = 5,
                [SpellConstants.Scrying] = 5,
                [SpellConstants.SlayLiving] = 5,
                [SpellConstants.SpellResistance] = 5,
                [SpellConstants.SummonMonsterV] = 5,
                [SpellConstants.SymbolOfPain] = 5,
                [SpellConstants.SymbolOfSleep] = 5,
                [SpellConstants.TrueSeeing] = 5,
                [SpellConstants.Unhallow] = 5,
                [SpellConstants.WallOfStone] = 5,

                //6th - Level Cleric Spells
                [SpellConstants.AnimateObjects] = 6,
                [SpellConstants.AntilifeShell] = 6,
                [SpellConstants.Banishment] = 6,
                [SpellConstants.BearsEndurance_Mass] = 6,
                [SpellConstants.BladeBarrier] = 6,
                [SpellConstants.BullsStrength_Mass] = 6,
                [SpellConstants.CreateUndead] = 6,
                [SpellConstants.CureModerateWounds_Mass] = 6,
                [SpellConstants.InflictModerateWounds_Mass] = 6,
                [SpellConstants.DispelMagic_Greater] = 6,
                [SpellConstants.EaglesSplendor_Mass] = 6,
                [SpellConstants.FindThePath] = 6,
                [SpellConstants.Forbiddance] = 6,
                [SpellConstants.GeasQuest] = 6,
                [SpellConstants.GlyphOfWarding_Greater] = 6,
                [SpellConstants.Heal] = 6,
                [SpellConstants.Harm] = 6,
                [SpellConstants.HeroesFeast] = 6,
                [SpellConstants.OwlsWisdom_Mass] = 6,
                [SpellConstants.PlanarAlly] = 6,
                [SpellConstants.SummonMonsterVI] = 6,
                [SpellConstants.SymbolOfFear] = 6,
                [SpellConstants.SymbolOfPersuasion] = 6,
                [SpellConstants.UndeathToDeath] = 6,
                [SpellConstants.WindWalk] = 6,
                [SpellConstants.WordOfRecall] = 6,

                //7th - Level Cleric Spells
                [SpellConstants.Blasphemy] = 7,
                [SpellConstants.ControlWeather] = 7,
                [SpellConstants.CureSeriousWounds_Mass] = 7,
                [SpellConstants.InflictSeriousWounds_Mass] = 7,
                [SpellConstants.Destruction] = 7,
                [SpellConstants.Dictum] = 7,
                [SpellConstants.EtherealJaunt] = 7,
                [SpellConstants.HolyWord] = 7,
                [SpellConstants.Refuge] = 7,
                [SpellConstants.Regenerate] = 7,
                [SpellConstants.Repulsion] = 7,
                [SpellConstants.Restoration_Greater] = 7,
                [SpellConstants.Resurrection] = 7,
                [SpellConstants.Scrying_Greater] = 7,
                [SpellConstants.SummonMonsterVII] = 7,
                [SpellConstants.SymbolOfStunning] = 7,
                [SpellConstants.SymbolOfWeakness] = 7,
                [SpellConstants.WordOfChaos] = 7,

                //8th - Level Cleric Spells
                [SpellConstants.AntimagicField] = 8,
                [SpellConstants.CloakOfChaos] = 8,
                [SpellConstants.CreateGreaterUndead] = 8,
                [SpellConstants.CureCriticalWounds_Mass] = 8,
                [SpellConstants.InflictCriticalWounds_Mass] = 8,
                [SpellConstants.DimensionalLock] = 8,
                [SpellConstants.DiscernLocation] = 8,
                [SpellConstants.Earthquake] = 8,
                [SpellConstants.FireStorm] = 8,
                [SpellConstants.HolyAura] = 8,
                [SpellConstants.PlanarAlly_Greater] = 8,
                [SpellConstants.ShieldOfLaw] = 8,
                [SpellConstants.SpellImmunity_Greater] = 8,
                [SpellConstants.SummonMonsterVIII] = 8,
                [SpellConstants.SymbolOfDeath] = 8,
                [SpellConstants.SymbolOfInsanity] = 8,
                [SpellConstants.UnholyAura] = 8,

                //9th - Level Cleric Spells
                [SpellConstants.AstralProjection] = 9,
                [SpellConstants.EnergyDrain] = 9,
                [SpellConstants.Etherealness] = 9,
                [SpellConstants.Gate] = 9,
                [SpellConstants.Heal_Mass] = 9,
                [SpellConstants.Implosion] = 9,
                [SpellConstants.Miracle] = 9,
                [SpellConstants.SoulBind] = 9,
                [SpellConstants.StormOfVengeance] = 9,
                [SpellConstants.SummonMonsterIX] = 9,
                [SpellConstants.TrueResurrection] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Casters.Cleric, spellLevels);
        }

        [Test]
        public void DruidSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                //0 - Level Druid Spells(Orisons)
                [SpellConstants.CreateWater] = 0,
                [SpellConstants.CureMinorWounds] = 0,
                [SpellConstants.DetectMagic] = 0,
                [SpellConstants.DetectPoison] = 0,
                [SpellConstants.Flare] = 0,
                [SpellConstants.Guidance] = 0,
                [SpellConstants.KnowDirection] = 0,
                [SpellConstants.Light] = 0,
                [SpellConstants.Mending] = 0,
                [SpellConstants.PurifyFoodAndDrink] = 0,
                [SpellConstants.ReadMagic] = 0,
                [SpellConstants.Resistance] = 0,
                [SpellConstants.Virtue] = 0,

                //1st - Level Druid Spells
                [SpellConstants.CalmAnimals] = 1,
                [SpellConstants.CharmAnimal] = 1,
                [SpellConstants.CureLightWounds] = 1,
                [SpellConstants.DetectAnimalsOrPlants] = 1,
                [SpellConstants.DetectSnaresAndPits] = 1,
                [SpellConstants.EndureElements] = 1,
                [SpellConstants.Entangle] = 1,
                [SpellConstants.FaerieFire] = 1,
                [SpellConstants.Goodberry] = 1,
                [SpellConstants.HideFromAnimals] = 1,
                [SpellConstants.Jump] = 1,
                [SpellConstants.Longstrider] = 1,
                [SpellConstants.MagicFang] = 1,
                [SpellConstants.MagicStone] = 1,
                [SpellConstants.ObscuringMist] = 1,
                [SpellConstants.PassWithoutTrace] = 1,
                [SpellConstants.ProduceFlame] = 1,
                [SpellConstants.Shillelagh] = 1,
                [SpellConstants.SpeakWithAnimals] = 1,
                [SpellConstants.SummonNaturesAllyI] = 1,

                //2nd - Level Druid Spells
                [SpellConstants.AnimalMessenger] = 2,
                [SpellConstants.AnimalTrance] = 2,
                [SpellConstants.Barkskin] = 2,
                [SpellConstants.BearsEndurance] = 2,
                [SpellConstants.BullsStrength] = 2,
                [SpellConstants.CatsGrace] = 2,
                [SpellConstants.ChillMetal] = 2,
                [SpellConstants.DelayPoison] = 2,
                [SpellConstants.FireTrap] = 2,
                [SpellConstants.FlameBlade] = 2,
                [SpellConstants.FlamingSphere] = 2,
                [SpellConstants.FogCloud] = 2,
                [SpellConstants.GustOfWind] = 2,
                [SpellConstants.HeatMetal] = 2,
                [SpellConstants.HoldAnimal] = 2,
                [SpellConstants.OwlsWisdom] = 2,
                [SpellConstants.ReduceAnimal] = 2,
                [SpellConstants.ResistEnergy] = 2,
                [SpellConstants.Restoration_Lesser] = 2,
                [SpellConstants.SoftenEarthAndStone] = 2,
                [SpellConstants.SpiderClimb] = 2,
                [SpellConstants.SummonNaturesAllyII] = 2,
                [SpellConstants.SummonSwarm] = 2,
                [SpellConstants.TreeShape] = 2,
                [SpellConstants.WarpWood] = 2,
                [SpellConstants.WoodShape] = 2,

                //3rd - Level Druid Spells
                [SpellConstants.CallLightning] = 3,
                [SpellConstants.Contagion] = 3,
                [SpellConstants.CureModerateWounds] = 3,
                [SpellConstants.Daylight] = 3,
                [SpellConstants.DiminishPlants] = 3,
                [SpellConstants.DominateAnimal] = 3,
                [SpellConstants.MagicFang_Greater] = 3,
                [SpellConstants.MeldIntoStone] = 3,
                [SpellConstants.NeutralizePoison] = 3,
                [SpellConstants.PlantGrowth] = 3,
                [SpellConstants.Poison] = 3,
                [SpellConstants.ProtectionFromEnergy] = 3,
                [SpellConstants.Quench] = 3,
                [SpellConstants.RemoveDisease] = 3,
                [SpellConstants.SleetStorm] = 3,
                [SpellConstants.Snare] = 3,
                [SpellConstants.SpeakWithPlants] = 3,
                [SpellConstants.SpikeGrowth] = 3,
                [SpellConstants.StoneShape] = 3,
                [SpellConstants.SummonNaturesAllyIII] = 3,
                [SpellConstants.WaterBreathing] = 3,
                [SpellConstants.WindWall] = 3,

                //4th - Level Druid Spells
                [SpellConstants.AirWalk] = 4,
                [SpellConstants.AntiplantShell] = 4,
                [SpellConstants.Blight] = 4,
                [SpellConstants.CommandPlants] = 4,
                [SpellConstants.ControlWater] = 4,
                [SpellConstants.CureSeriousWounds] = 4,
                [SpellConstants.DispelMagic] = 4,
                [SpellConstants.FlameStrike] = 4,
                [SpellConstants.FreedomOfMovement] = 4,
                [SpellConstants.GiantVermin] = 4,
                [SpellConstants.IceStorm] = 4,
                [SpellConstants.Reincarnate] = 4,
                [SpellConstants.RepelVermin] = 4,
                [SpellConstants.RustingGrasp] = 4,
                [SpellConstants.Scrying] = 4,
                [SpellConstants.SpikeStones] = 4,
                [SpellConstants.SummonNaturesAllyIV] = 4,

                //5th - Level Druid Spells
                [SpellConstants.AnimalGrowth] = 5,
                [SpellConstants.Atonement] = 5,
                [SpellConstants.Awaken] = 5,
                [SpellConstants.BalefulPolymorph] = 5,
                [SpellConstants.CallLightningStorm] = 5,
                [SpellConstants.CommuneWithNature] = 5,
                [SpellConstants.ControlWinds] = 5,
                [SpellConstants.CureCriticalWounds] = 5,
                [SpellConstants.DeathWard] = 5,
                [SpellConstants.Hallow] = 5,
                [SpellConstants.InsectPlague] = 5,
                [SpellConstants.Stoneskin] = 5,
                [SpellConstants.SummonNaturesAllyV] = 5,
                [SpellConstants.TransmuteMudToRock] = 5,
                [SpellConstants.TransmuteRockToMud] = 5,
                [SpellConstants.TreeStride] = 5,
                [SpellConstants.Unhallow] = 5,
                [SpellConstants.WallOfFire] = 5,
                [SpellConstants.WallOfThorns] = 5,

                //6th - Level Druid Spells
                [SpellConstants.AntilifeShell] = 6,
                [SpellConstants.BearsEndurance_Mass] = 6,
                [SpellConstants.BullsStrength_Mass] = 6,
                [SpellConstants.CatsGrace_Mass] = 6,
                [SpellConstants.CureLightWounds_Mass] = 6,
                [SpellConstants.DispelMagic_Greater] = 6,
                [SpellConstants.FindThePath] = 6,
                [SpellConstants.FireSeeds] = 6,
                [SpellConstants.Ironwood] = 6,
                [SpellConstants.Liveoak] = 6,
                [SpellConstants.MoveEarth] = 6,
                [SpellConstants.OwlsWisdom_Mass] = 6,
                [SpellConstants.RepelWood] = 6,
                [SpellConstants.Spellstaff] = 6,
                [SpellConstants.StoneTell] = 6,
                [SpellConstants.SummonNaturesAllyVI] = 6,
                [SpellConstants.TransportViaPlants] = 6,
                [SpellConstants.WallOfStone] = 6,

                //7th - Level Druid Spells
                [SpellConstants.AnimatePlants] = 7,
                [SpellConstants.Changestaff] = 7,
                [SpellConstants.ControlWeather] = 7,
                [SpellConstants.CreepingDoom] = 7,
                [SpellConstants.CureModerateWounds_Mass] = 7,
                [SpellConstants.FireStorm] = 7,
                [SpellConstants.Heal] = 7,
                [SpellConstants.Scrying_Greater] = 7,
                [SpellConstants.SummonNaturesAllyVII] = 7,
                [SpellConstants.Sunbeam] = 7,
                [SpellConstants.TransmuteMetalToWood] = 7,
                [SpellConstants.TrueSeeing] = 7,
                [SpellConstants.WindWalk] = 7,

                //8th - Level Druid Spells
                [SpellConstants.AnimalShapes] = 8,
                [SpellConstants.ControlPlants] = 8,
                [SpellConstants.CureSeriousWounds_Mass] = 8,
                [SpellConstants.Earthquake] = 8,
                [SpellConstants.FingerOfDeath] = 8,
                [SpellConstants.RepelMetalOrStone] = 8,
                [SpellConstants.ReverseGravity] = 8,
                [SpellConstants.SummonNaturesAllyVIII] = 8,
                [SpellConstants.Sunburst] = 8,
                [SpellConstants.Whirlwind] = 8,
                [SpellConstants.WordOfRecall] = 8,

                //9th - Level Druid Spells
                [SpellConstants.Antipathy] = 9,
                [SpellConstants.CureCriticalWounds_Mass] = 9,
                [SpellConstants.ElementalSwarm] = 9,
                [SpellConstants.Foresight] = 9,
                [SpellConstants.Regenerate] = 9,
                [SpellConstants.Shambler] = 9,
                [SpellConstants.Shapechange] = 9,
                [SpellConstants.StormOfVengeance] = 9,
                [SpellConstants.SummonNaturesAllyIX] = 9,
                [SpellConstants.Sympathy] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Casters.Druid, spellLevels);
        }

        [Test]
        public void SorcererSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                //0 - Level Sorcerer / Wizard Spells(Cantrips)
                //Abjur
                [SpellConstants.Resistance] = 0,
                //Conj
                [SpellConstants.AcidSplash] = 0,
                //Div
                [SpellConstants.DetectPoison] = 0,
                [SpellConstants.DetectMagic] = 0,
                [SpellConstants.ReadMagic] = 0,
                //Ench
                [SpellConstants.Daze] = 0,
                //Evoc
                [SpellConstants.DancingLights] = 0,
                [SpellConstants.Flare] = 0,
                [SpellConstants.Light] = 0,
                [SpellConstants.RayOfFrost] = 0,
                //Illus
                [SpellConstants.GhostSound] = 0,
                //Necro
                [SpellConstants.DisruptUndead] = 0,
                [SpellConstants.TouchOfFatigue] = 0,
                //Trans
                [SpellConstants.MageHand] = 0,
                [SpellConstants.Mending] = 0,
                [SpellConstants.Message] = 0,
                [SpellConstants.OpenClose] = 0,
                //Univ
                [SpellConstants.ArcaneMark] = 0,
                [SpellConstants.Prestidigitation] = 0,

                //1st - Level Sorcerer / Wizard Spells
                //Abjur
                [SpellConstants.Alarm] = 1,
                [SpellConstants.EndureElements] = 1,
                [SpellConstants.HoldPortal] = 1,
                [SpellConstants.ProtectionFromChaos] = 1,
                [SpellConstants.ProtectionFromEvil] = 1,
                [SpellConstants.ProtectionFromGood] = 1,
                [SpellConstants.ProtectionFromLaw] = 1,
                [SpellConstants.Shield] = 1,
                //Conj
                [SpellConstants.Grease] = 1,
                [SpellConstants.MageArmor] = 1,
                [SpellConstants.Mount] = 1,
                [SpellConstants.ObscuringMist] = 1,
                [SpellConstants.SummonMonsterI] = 1,
                [SpellConstants.UnseenServant] = 1,
                //Div
                [SpellConstants.ComprehendLanguages] = 1,
                [SpellConstants.DetectSecretDoors] = 1,
                [SpellConstants.DetectUndead] = 1,
                [SpellConstants.Identify] = 1,
                [SpellConstants.TrueStrike] = 1,
                //Ench
                [SpellConstants.CharmPerson] = 1,
                [SpellConstants.Hypnotism] = 1,
                [SpellConstants.Sleep] = 1,
                //Evoc
                [SpellConstants.BurningHands] = 1,
                [SpellConstants.FloatingDisk] = 1,
                [SpellConstants.MagicMissile] = 1,
                [SpellConstants.ShockingGrasp] = 1,
                //Illus
                [SpellConstants.ColorSpray] = 1,
                [SpellConstants.DisguiseSelf] = 1,
                [SpellConstants.MagicAura] = 1,
                [SpellConstants.SilentImage] = 1,
                [SpellConstants.Ventriloquism] = 1,
                //Necro
                [SpellConstants.CauseFear] = 1,
                [SpellConstants.ChillTouch] = 1,
                [SpellConstants.RayOfEnfeeblement] = 1,
                //Trans
                [SpellConstants.AnimateRope] = 1,
                [SpellConstants.EnlargePerson] = 1,
                [SpellConstants.Erase] = 1,
                [SpellConstants.ExpeditiousRetreat] = 1,
                [SpellConstants.FeatherFall] = 1,
                [SpellConstants.Jump] = 1,
                [SpellConstants.MagicWeapon] = 1,
                [SpellConstants.ReducePerson] = 1,

                //2nd - Level Sorcerer / Wizard Spells
                //Abjur
                [SpellConstants.ArcaneLock] = 2,
                [SpellConstants.ObscureObject] = 2,
                [SpellConstants.ProtectionFromArrows] = 2,
                [SpellConstants.ResistEnergy] = 2,
                //Conj
                [SpellConstants.AcidArrow] = 2,
                [SpellConstants.FogCloud] = 2,
                [SpellConstants.Glitterdust] = 2,
                [SpellConstants.SummonMonsterII] = 2,
                [SpellConstants.SummonSwarm] = 2,
                [SpellConstants.Web] = 2,
                //Div
                [SpellConstants.DetectThoughts] = 2,
                [SpellConstants.LocateObject] = 2,
                [SpellConstants.SeeInvisibility] = 2,
                //Ench
                [SpellConstants.DazeMonster] = 2,
                [SpellConstants.HideousLaughter] = 2,
                [SpellConstants.TouchOfIdiocy] = 2,
                //Evoc
                [SpellConstants.ContinualFlame] = 2,
                [SpellConstants.Darkness] = 2,
                [SpellConstants.FlamingSphere] = 2,
                [SpellConstants.GustOfWind] = 2,
                [SpellConstants.ScorchingRay] = 2,
                [SpellConstants.Shatter] = 2,
                //Illus
                [SpellConstants.Blur] = 2,
                [SpellConstants.HypnoticPattern] = 2,
                [SpellConstants.Invisibility] = 2,
                [SpellConstants.MagicMouth] = 2,
                [SpellConstants.MinorImage] = 2,
                [SpellConstants.MirrorImage] = 2,
                [SpellConstants.Misdirection] = 2,
                [SpellConstants.PhantomTrap] = 2,
                //Necro
                [SpellConstants.BlindnessDeafness] = 2,
                [SpellConstants.CommandUndead] = 2,
                [SpellConstants.FalseLife] = 2,
                [SpellConstants.GhoulTouch] = 2,
                [SpellConstants.Scare] = 2,
                [SpellConstants.SpectralHand] = 2,
                //Trans
                [SpellConstants.AlterSelf] = 2,
                [SpellConstants.BearsEndurance] = 2,
                [SpellConstants.BullsStrength] = 2,
                [SpellConstants.CatsGrace] = 2,
                [SpellConstants.Darkvision] = 2,
                [SpellConstants.EaglesSplendor] = 2,
                [SpellConstants.FoxsCunning] = 2,
                [SpellConstants.Knock] = 2,
                [SpellConstants.Levitate] = 2,
                [SpellConstants.OwlsWisdom] = 2,
                [SpellConstants.Pyrotechnics] = 2,
                [SpellConstants.RopeTrick] = 2,
                [SpellConstants.SpiderClimb] = 2,
                [SpellConstants.WhisperingWind] = 2,

                //3rd-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.DispelMagic] = 3,
                [SpellConstants.ExplosiveRunes] = 3,
                [SpellConstants.MagicCircleAgainstChaos] = 3,
                [SpellConstants.MagicCircleAgainstEvil] = 3,
                [SpellConstants.MagicCircleAgainstGood] = 3,
                [SpellConstants.MagicCircleAgainstLaw] = 3,
                [SpellConstants.Nondetection] = 3,
                [SpellConstants.ProtectionFromEnergy] = 3,
                //Conj
                [SpellConstants.PhantomSteed] = 3,
                [SpellConstants.SepiaSnakeSigil] = 3,
                [SpellConstants.SleetStorm] = 3,
                [SpellConstants.StinkingCloud] = 3,
                [SpellConstants.SummonMonsterIII] = 3,
                //Div
                [SpellConstants.ArcaneSight] = 3,
                [SpellConstants.ClairaudienceClairvoyance] = 3,
                [SpellConstants.Tongues] = 3,
                //Ench
                [SpellConstants.DeepSlumber] = 3,
                [SpellConstants.Heroism] = 3,
                [SpellConstants.HoldPerson] = 3,
                [SpellConstants.Rage] = 3,
                [SpellConstants.Suggestion] = 3,
                //Evoc
                [SpellConstants.Daylight] = 3,
                [SpellConstants.Fireball] = 3,
                [SpellConstants.LightningBolt] = 3,
                [SpellConstants.TinyHut] = 3,
                [SpellConstants.WindWall] = 3,
                //Illus
                [SpellConstants.Displacement] = 3,
                [SpellConstants.IllusoryScript] = 3,
                [SpellConstants.InvisibilitySphere] = 3,
                [SpellConstants.MajorImage] = 3,
                //Necro
                [SpellConstants.GentleRepose] = 3,
                [SpellConstants.HaltUndead] = 3,
                [SpellConstants.RayOfExhaustion] = 3,
                [SpellConstants.VampiricTouch] = 3,
                //Trans
                [SpellConstants.Blink] = 3,
                [SpellConstants.FlameArrow] = 3,
                [SpellConstants.Fly] = 3,
                [SpellConstants.GaseousForm] = 3,
                [SpellConstants.Haste] = 3,
                [SpellConstants.KeenEdge] = 3,
                [SpellConstants.MagicWeapon_Greater] = 3,
                [SpellConstants.SecretPage] = 3,
                [SpellConstants.ShrinkItem] = 3,
                [SpellConstants.Slow] = 3,
                [SpellConstants.WaterBreathing] = 3,

                //4th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.DimensionalAnchor] = 4,
                [SpellConstants.FireTrap] = 4,
                [SpellConstants.GlobeOfInvulnerability_Lesser] = 4,
                [SpellConstants.RemoveCurse] = 4,
                [SpellConstants.Stoneskin] = 4,
                //Conj
                [SpellConstants.BlackTentacles] = 4,
                [SpellConstants.DimensionDoor] = 4,
                [SpellConstants.MinorCreation] = 4,
                [SpellConstants.SecureShelter] = 4,
                [SpellConstants.SolidFog] = 4,
                [SpellConstants.SummonMonsterIV] = 4,
                //Div
                [SpellConstants.ArcaneEye] = 4,
                [SpellConstants.DetectScrying] = 4,
                [SpellConstants.LocateCreature] = 4,
                [SpellConstants.Scrying] = 4,
                //Ench
                [SpellConstants.CharmMonster] = 4,
                [SpellConstants.Confusion] = 4,
                [SpellConstants.CrushingDespair] = 4,
                [SpellConstants.Geas_Lesser] = 4,
                //Evoc
                [SpellConstants.FireShield] = 4,
                [SpellConstants.IceStorm] = 4,
                [SpellConstants.ResilientSphere] = 4,
                [SpellConstants.Shout] = 4,
                [SpellConstants.WallOfFire] = 4,
                [SpellConstants.WallOfIce] = 4,
                //Illus
                [SpellConstants.HallucinatoryTerrain] = 4,
                [SpellConstants.IllusoryWall] = 4,
                [SpellConstants.Invisibility_Greater] = 4,
                [SpellConstants.PhantasmalKiller] = 4,
                [SpellConstants.RainbowPattern] = 4,
                [SpellConstants.ShadowConjuration] = 4,
                //Necro
                [SpellConstants.AnimateDead] = 4,
                [SpellConstants.BestowCurse] = 4,
                [SpellConstants.Contagion] = 4,
                [SpellConstants.Enervation] = 4,
                [SpellConstants.Fear] = 4,
                //Trans
                [SpellConstants.EnlargePerson_Mass] = 4,
                [SpellConstants.MnemonicEnhancer] = 4,
                [SpellConstants.Polymorph] = 4,
                [SpellConstants.ReducePerson_Mass] = 4,
                [SpellConstants.StoneShape] = 4,

                //5th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.BreakEnchantment] = 5,
                [SpellConstants.Dismissal] = 5,
                [SpellConstants.MagesPrivateSanctum] = 5,
                //Conj
                [SpellConstants.Cloudkill] = 5,
                [SpellConstants.MagesFaithfulHound] = 5,
                [SpellConstants.MajorCreation] = 5,
                [SpellConstants.PlanarBinding_Lesser] = 5,
                [SpellConstants.SecretChest] = 5,
                [SpellConstants.SummonMonsterV] = 5,
                [SpellConstants.Teleport] = 5,
                [SpellConstants.WallOfStone] = 5,
                //Div
                [SpellConstants.ContactOtherPlane] = 5,
                [SpellConstants.PryingEyes] = 5,
                [SpellConstants.TelepathicBond] = 5,
                //Ench
                [SpellConstants.DominatePerson] = 5,
                [SpellConstants.Feeblemind] = 5,
                [SpellConstants.HoldMonster] = 5,
                [SpellConstants.MindFog] = 5,
                [SpellConstants.SymbolOfSleep] = 5,
                //Evoc
                [SpellConstants.ConeOfCold] = 5,
                [SpellConstants.InterposingHand] = 5,
                [SpellConstants.Sending] = 5,
                [SpellConstants.WallOfForce] = 5,
                //Illus
                [SpellConstants.Dream] = 5,
                [SpellConstants.FalseVision] = 5,
                [SpellConstants.MirageArcana] = 5,
                [SpellConstants.Nightmare] = 5,
                [SpellConstants.PersistentImage] = 5,
                [SpellConstants.Seeming] = 5,
                [SpellConstants.ShadowEvocation] = 5,
                //Necro
                [SpellConstants.Blight] = 5,
                [SpellConstants.MagicJar] = 5,
                [SpellConstants.SymbolOfPain] = 5,
                [SpellConstants.WavesOfFatigue] = 5,
                //Trans
                [SpellConstants.AnimalGrowth] = 5,
                [SpellConstants.BalefulPolymorph] = 5,
                [SpellConstants.Fabricate] = 5,
                [SpellConstants.OverlandFlight] = 5,
                [SpellConstants.Passwall] = 5,
                [SpellConstants.Telekinesis] = 5,
                [SpellConstants.TransmuteMudToRock] = 5,
                [SpellConstants.TransmuteRockToMud] = 5,
                //Univ
                [SpellConstants.Permanency] = 5,

                //6th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.AntimagicField] = 6,
                [SpellConstants.DispelMagic_Greater] = 6,
                [SpellConstants.GlobeOfInvulnerability] = 6,
                [SpellConstants.GuardsAndWards] = 6,
                [SpellConstants.Repulsion] = 6,
                //Conj
                [SpellConstants.AcidFog] = 6,
                [SpellConstants.PlanarBinding] = 6,
                [SpellConstants.SummonMonsterVI] = 6,
                [SpellConstants.WallOfIron] = 6,
                //Div
                [SpellConstants.AnalyzeDweomer] = 6,
                [SpellConstants.LegendLore] = 6,
                [SpellConstants.TrueSeeing] = 6,
                //Ench
                [SpellConstants.GeasQuest] = 6,
                [SpellConstants.Heroism_Greater] = 6,
                [SpellConstants.Suggestion_Mass] = 6,
                [SpellConstants.SymbolOfPersuasion] = 6,
                //Evoc
                [SpellConstants.ChainLightning] = 6,
                [SpellConstants.Contingency] = 6,
                [SpellConstants.ForcefulHand] = 6,
                [SpellConstants.FreezingSphere] = 6,
                //Illus
                [SpellConstants.Mislead] = 6,
                [SpellConstants.PermanentImage] = 6,
                [SpellConstants.ProgrammedImage] = 6,
                [SpellConstants.ShadowWalk] = 6,
                [SpellConstants.Veil] = 6,
                //Necro
                [SpellConstants.CircleOfDeath] = 6,
                [SpellConstants.CreateUndead] = 6,
                [SpellConstants.Eyebite] = 6,
                [SpellConstants.SymbolOfFear] = 6,
                [SpellConstants.UndeathToDeath] = 6,
                //Trans
                [SpellConstants.BearsEndurance_Mass] = 6,
                [SpellConstants.BullsStrength_Mass] = 6,
                [SpellConstants.CatsGrace_Mass] = 6,
                [SpellConstants.ControlWater] = 6,
                [SpellConstants.Disintegrate] = 6,
                [SpellConstants.EaglesSplendor_Mass] = 6,
                [SpellConstants.FleshToStone] = 6,
                [SpellConstants.FoxsCunning_Mass] = 6,
                [SpellConstants.MagesLucubration] = 6,
                [SpellConstants.MoveEarth] = 6,
                [SpellConstants.OwlsWisdom_Mass] = 6,
                [SpellConstants.StoneToFlesh] = 6,
                [SpellConstants.Transformation] = 6,

                //7th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.Banishment] = 7,
                [SpellConstants.Sequester] = 7,
                [SpellConstants.SpellTurning] = 7,
                //Conj
                [SpellConstants.InstantSummons] = 7,
                [SpellConstants.MagesMagnificentMansion] = 7,
                [SpellConstants.PhaseDoor] = 7,
                [SpellConstants.PlaneShift] = 7,
                [SpellConstants.SummonMonsterVII] = 7,
                [SpellConstants.Teleport_Greater] = 7,
                [SpellConstants.TeleportObject] = 7,
                //Div
                [SpellConstants.ArcaneSight_Greater] = 7,
                [SpellConstants.Scrying_Greater] = 7,
                [SpellConstants.Vision] = 7,
                //Ench
                [SpellConstants.HoldPerson_Mass] = 7,
                [SpellConstants.Insanity] = 7,
                [SpellConstants.PowerWordBlind] = 7,
                [SpellConstants.SymbolOfStunning] = 7,
                //Evoc
                [SpellConstants.DelayedBlastFireball] = 7,
                [SpellConstants.Forcecage] = 7,
                [SpellConstants.GraspingHand] = 7,
                [SpellConstants.MagesSword] = 7,
                [SpellConstants.PrismaticSpray] = 7,
                //Illus
                [SpellConstants.Invisibility_Mass] = 7,
                [SpellConstants.ProjectImage] = 7,
                [SpellConstants.ShadowConjuration_Greater] = 7,
                [SpellConstants.Simulacrum] = 7,
                //Necro
                [SpellConstants.ControlUndead] = 7,
                [SpellConstants.FingerOfDeath] = 7,
                [SpellConstants.SymbolOfWeakness] = 7,
                [SpellConstants.WavesOfExhaustion] = 7,
                //Trans
                [SpellConstants.ControlWeather] = 7,
                [SpellConstants.EtherealJaunt] = 7,
                [SpellConstants.ReverseGravity] = 7,
                [SpellConstants.Statue] = 7,
                //Univ
                [SpellConstants.LimitedWish] = 7,

                //8th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.DimensionalLock] = 8,
                [SpellConstants.MindBlank] = 8,
                [SpellConstants.PrismaticWall] = 8,
                [SpellConstants.ProtectionFromSpells] = 8,
                //Conj
                [SpellConstants.IncendiaryCloud] = 8,
                [SpellConstants.Maze] = 8,
                [SpellConstants.PlanarBinding_Greater] = 8,
                [SpellConstants.SummonMonsterVIII] = 8,
                [SpellConstants.TrapTheSoul] = 8,
                //Div
                [SpellConstants.DiscernLocation] = 8,
                [SpellConstants.MomentOfPrescience] = 8,
                [SpellConstants.PryingEyes_Greater] = 8,
                //Ench
                [SpellConstants.Antipathy] = 8,
                [SpellConstants.Binding] = 8,
                [SpellConstants.CharmMonster_Mass] = 8,
                [SpellConstants.Demand] = 8,
                [SpellConstants.IrresistibleDance] = 8,
                [SpellConstants.PowerWordStun] = 8,
                [SpellConstants.SymbolOfInsanity] = 8,
                [SpellConstants.Sympathy] = 8,
                //Evoc
                [SpellConstants.ClenchedFist] = 8,
                [SpellConstants.PolarRay] = 8,
                [SpellConstants.Shout_Greater] = 8,
                [SpellConstants.Sunburst] = 8,
                [SpellConstants.TelekineticSphere] = 8,
                //Illus
                [SpellConstants.ScintillatingPattern] = 8,
                [SpellConstants.Screen] = 8,
                [SpellConstants.ShadowEvocation_Greater] = 8,
                //Necro
                [SpellConstants.Clone] = 8,
                [SpellConstants.CreateGreaterUndead] = 8,
                [SpellConstants.HorridWilting] = 8,
                [SpellConstants.SymbolOfDeath] = 8,
                //Trans
                [SpellConstants.IronBody] = 8,
                [SpellConstants.PolymorphAnyObject] = 8,
                [SpellConstants.TemporalStasis] = 8,

                //9th-Level Sorcerer/Wizard Spells
                //Abjur
                [SpellConstants.Freedom] = 9,
                [SpellConstants.Imprisonment] = 9,
                [SpellConstants.MagesDisjunction] = 9,
                [SpellConstants.PrismaticSphere] = 9,
                //Conj
                [SpellConstants.Gate] = 9,
                [SpellConstants.Refuge] = 9,
                [SpellConstants.SummonMonsterIX] = 9,
                [SpellConstants.TeleportationCircle] = 9,
                //Div
                [SpellConstants.Foresight] = 9,
                //Ench
                [SpellConstants.DominateMonster] = 9,
                [SpellConstants.HoldMonster_Mass] = 9,
                [SpellConstants.PowerWordKill] = 9,
                //Evoc
                [SpellConstants.CrushingHand] = 9,
                [SpellConstants.MeteorSwarm] = 9,
                //Illus
                [SpellConstants.Shades] = 9,
                [SpellConstants.Weird] = 9,
                //Necro
                [SpellConstants.AstralProjection] = 9,
                [SpellConstants.EnergyDrain] = 9,
                [SpellConstants.SoulBind] = 9,
                [SpellConstants.WailOfTheBanshee] = 9,
                //Trans
                [SpellConstants.Etherealness] = 9,
                [SpellConstants.Shapechange] = 9,
                [SpellConstants.TimeStop] = 9,
                //Univ
                [SpellConstants.Wish] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Casters.Sorcerer, spellLevels);
        }

        [Test]
        public void AirSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ObscuringMist] = 1,
                [SpellConstants.WindWall] = 2,
                [SpellConstants.GaseousForm] = 3,
                [SpellConstants.AirWalk] = 4,
                [SpellConstants.ControlWinds] = 5,
                [SpellConstants.ChainLightning] = 6,
                [SpellConstants.ControlWeather] = 7,
                [SpellConstants.Whirlwind] = 8,
                [SpellConstants.ElementalSwarm] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Air, spellLevels);
        }

        [Test]
        public void AnimalSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.CalmAnimals] = 1,
                [SpellConstants.HoldAnimal] = 2,
                [SpellConstants.DominateAnimal] = 3,
                [SpellConstants.SummonNaturesAllyIV] = 4,
                [SpellConstants.CommuneWithNature] = 5,
                [SpellConstants.AntilifeShell] = 6,
                [SpellConstants.AnimalShapes] = 7,
                [SpellConstants.SummonNaturesAllyVIII] = 8,
                [SpellConstants.Shapechange] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Animal, spellLevels);
        }

        [Test]
        public void ChaosSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ProtectionFromLaw] = 1,
                [SpellConstants.Shatter] = 2,
                [SpellConstants.MagicCircleAgainstLaw] = 3,
                [SpellConstants.ChaosHammer] = 4,
                [SpellConstants.DispelLaw] = 5,
                [SpellConstants.AnimateObjects] = 6,
                [SpellConstants.WordOfChaos] = 7,
                [SpellConstants.CloakOfChaos] = 8,
                [SpellConstants.SummonMonsterIX] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Chaos, spellLevels);
        }

        [Test]
        public void DestructionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.InflictLightWounds] = 1,
                [SpellConstants.Shatter] = 2,
                [SpellConstants.Contagion] = 3,
                [SpellConstants.InflictCriticalWounds] = 4,
                [SpellConstants.InflictLightWounds_Mass] = 5,
                [SpellConstants.Harm] = 6,
                [SpellConstants.Disintegrate] = 7,
                [SpellConstants.Earthquake] = 8,
                [SpellConstants.Implosion] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Destruction, spellLevels);
        }

        [Test]
        public void EarthSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.MagicStone] = 1,
                [SpellConstants.SoftenEarthAndStone] = 2,
                [SpellConstants.StoneShape] = 3,
                [SpellConstants.SpikeStones] = 4,
                [SpellConstants.WallOfStone] = 5,
                [SpellConstants.Stoneskin] = 6,
                [SpellConstants.Earthquake] = 7,
                [SpellConstants.IronBody] = 8,
                [SpellConstants.ElementalSwarm] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Earth, spellLevels);
        }

        [Test]
        public void EnchantmentSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                //0 - Level Sorcerer / Wizard Spells(Cantrips)
                //Ench
                [SpellConstants.Daze] = 0,

                //1st - Level Sorcerer / Wizard Spells
                //Ench
                [SpellConstants.CharmPerson] = 1,
                [SpellConstants.Hypnotism] = 1,
                [SpellConstants.Sleep] = 1,

                //2nd - Level Sorcerer / Wizard Spells
                //Ench
                [SpellConstants.DazeMonster] = 2,
                [SpellConstants.HideousLaughter] = 2,
                [SpellConstants.TouchOfIdiocy] = 2,

                //3rd-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.DeepSlumber] = 3,
                [SpellConstants.Heroism] = 3,
                [SpellConstants.HoldPerson] = 3,
                [SpellConstants.Rage] = 3,
                [SpellConstants.Suggestion] = 3,

                //4th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.CharmMonster] = 4,
                [SpellConstants.Confusion] = 4,
                [SpellConstants.CrushingDespair] = 4,
                [SpellConstants.Geas_Lesser] = 4,

                //5th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.DominatePerson] = 5,
                [SpellConstants.Feeblemind] = 5,
                [SpellConstants.HoldMonster] = 5,
                [SpellConstants.MindFog] = 5,
                [SpellConstants.SymbolOfSleep] = 5,

                //6th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.GeasQuest] = 6,
                [SpellConstants.Heroism_Greater] = 6,
                [SpellConstants.Suggestion_Mass] = 6,
                [SpellConstants.SymbolOfPersuasion] = 6,

                //7th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.HoldPerson_Mass] = 7,
                [SpellConstants.Insanity] = 7,
                [SpellConstants.PowerWordBlind] = 7,
                [SpellConstants.SymbolOfStunning] = 7,

                //8th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.Antipathy] = 8,
                [SpellConstants.Binding] = 8,
                [SpellConstants.CharmMonster_Mass] = 8,
                [SpellConstants.Demand] = 8,
                [SpellConstants.IrresistibleDance] = 8,
                [SpellConstants.PowerWordStun] = 8,
                [SpellConstants.SymbolOfInsanity] = 8,
                [SpellConstants.Sympathy] = 8,

                //9th-Level Sorcerer/Wizard Spells
                //Ench
                [SpellConstants.DominateMonster] = 9,
                [SpellConstants.HoldMonster_Mass] = 9,
                [SpellConstants.PowerWordKill] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Enchantment, spellLevels);
        }

        [Test]
        public void EvilSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ProtectionFromGood] = 1,
                [SpellConstants.Desecrate] = 2,
                [SpellConstants.MagicCircleAgainstGood] = 3,
                [SpellConstants.UnholyBlight] = 4,
                [SpellConstants.DispelGood] = 5,
                [SpellConstants.CreateUndead] = 6,
                [SpellConstants.Blasphemy] = 7,
                [SpellConstants.UnholyAura] = 8,
                [SpellConstants.SummonMonsterIX] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Evil, spellLevels);
        }

        [Test]
        public void FireSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.BurningHands] = 1,
                [SpellConstants.ProduceFlame] = 2,
                [SpellConstants.ResistEnergy] = 3,
                [SpellConstants.WallOfFire] = 4,
                [SpellConstants.FireShield] = 5,
                [SpellConstants.FireSeeds] = 6,
                [SpellConstants.FireStorm] = 7,
                [SpellConstants.IncendiaryCloud] = 8,
                [SpellConstants.ElementalSwarm] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Fire, spellLevels);
        }

        [Test]
        public void GoodSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ProtectionFromEvil] = 1,
                [SpellConstants.Aid] = 2,
                [SpellConstants.MagicCircleAgainstEvil] = 3,
                [SpellConstants.HolySmite] = 4,
                [SpellConstants.DispelEvil] = 5,
                [SpellConstants.BladeBarrier] = 6,
                [SpellConstants.HolyWord] = 7,
                [SpellConstants.HolyAura] = 8,
                [SpellConstants.SummonMonsterIX] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Good, spellLevels);
        }

        [Test]
        public void HealingSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.CureLightWounds] = 1,
                [SpellConstants.CureModerateWounds] = 2,
                [SpellConstants.CureSeriousWounds] = 3,
                [SpellConstants.CureCriticalWounds] = 4,
                [SpellConstants.CureLightWounds_Mass] = 5,
                [SpellConstants.Heal] = 6,
                [SpellConstants.Regenerate] = 7,
                [SpellConstants.CureCriticalWounds_Mass] = 8,
                [SpellConstants.Heal_Mass] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Healing, spellLevels);
        }

        [Test]
        public void IllusionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                //0 - Level Sorcerer / Wizard Spells(Cantrips)
                //Illus
                [SpellConstants.GhostSound] = 0,

                //1st - Level Sorcerer / Wizard Spells
                //Illus
                [SpellConstants.ColorSpray] = 1,
                [SpellConstants.DisguiseSelf] = 1,
                [SpellConstants.MagicAura] = 1,
                [SpellConstants.SilentImage] = 1,
                [SpellConstants.Ventriloquism] = 1,

                //2nd - Level Sorcerer / Wizard Spells
                //Illus
                [SpellConstants.Blur] = 2,
                [SpellConstants.HypnoticPattern] = 2,
                [SpellConstants.Invisibility] = 2,
                [SpellConstants.MagicMouth] = 2,
                [SpellConstants.MinorImage] = 2,
                [SpellConstants.MirrorImage] = 2,
                [SpellConstants.Misdirection] = 2,
                [SpellConstants.PhantomTrap] = 2,

                //3rd-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.Displacement] = 3,
                [SpellConstants.IllusoryScript] = 3,
                [SpellConstants.InvisibilitySphere] = 3,
                [SpellConstants.MajorImage] = 3,

                //4th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.HallucinatoryTerrain] = 4,
                [SpellConstants.IllusoryWall] = 4,
                [SpellConstants.Invisibility_Greater] = 4,
                [SpellConstants.PhantasmalKiller] = 4,
                [SpellConstants.RainbowPattern] = 4,
                [SpellConstants.ShadowConjuration] = 4,

                //5th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.Dream] = 5,
                [SpellConstants.FalseVision] = 5,
                [SpellConstants.MirageArcana] = 5,
                [SpellConstants.Nightmare] = 5,
                [SpellConstants.PersistentImage] = 5,
                [SpellConstants.Seeming] = 5,
                [SpellConstants.ShadowEvocation] = 5,

                //6th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.Mislead] = 6,
                [SpellConstants.PermanentImage] = 6,
                [SpellConstants.ProgrammedImage] = 6,
                [SpellConstants.ShadowWalk] = 6,
                [SpellConstants.Veil] = 6,

                //7th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.Invisibility_Mass] = 7,
                [SpellConstants.ProjectImage] = 7,
                [SpellConstants.ShadowConjuration_Greater] = 7,
                [SpellConstants.Simulacrum] = 7,

                //8th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.ScintillatingPattern] = 8,
                [SpellConstants.Screen] = 8,
                [SpellConstants.ShadowEvocation_Greater] = 8,

                //9th-Level Sorcerer/Wizard Spells
                //Illus
                [SpellConstants.Shades] = 9,
                [SpellConstants.Weird] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Illusion, spellLevels);
        }

        [Test]
        public void KnowledgeSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.DetectSecretDoors] = 1,
                [SpellConstants.DetectThoughts] = 2,
                [SpellConstants.ClairaudienceClairvoyance] = 3,
                [SpellConstants.Divination] = 4,
                [SpellConstants.TrueSeeing] = 5,
                [SpellConstants.FindThePath] = 6,
                [SpellConstants.LegendLore] = 7,
                [SpellConstants.DiscernLocation] = 8,
                [SpellConstants.Foresight] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Knowledge, spellLevels);
        }

        [Test]
        public void LawSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ProtectionFromChaos] = 1,
                [SpellConstants.CalmEmotions] = 2,
                [SpellConstants.MagicCircleAgainstChaos] = 3,
                [SpellConstants.OrdersWrath] = 4,
                [SpellConstants.DispelChaos] = 5,
                [SpellConstants.HoldMonster] = 6,
                [SpellConstants.Dictum] = 7,
                [SpellConstants.ShieldOfLaw] = 8,
                [SpellConstants.SummonMonsterIX] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Law, spellLevels);
        }

        [Test]
        public void LuckSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.EntropicShield] = 1,
                [SpellConstants.Aid] = 2,
                [SpellConstants.ProtectionFromEnergy] = 3,
                [SpellConstants.FreedomOfMovement] = 4,
                [SpellConstants.BreakEnchantment] = 5,
                [SpellConstants.Mislead] = 6,
                [SpellConstants.SpellTurning] = 7,
                [SpellConstants.MomentOfPrescience] = 8,
                [SpellConstants.Miracle] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Luck, spellLevels);
        }

        [Test]
        public void PlantSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.Entangle] = 1,
                [SpellConstants.Barkskin] = 2,
                [SpellConstants.PlantGrowth] = 3,
                [SpellConstants.CommandPlants] = 4,
                [SpellConstants.WallOfThorns] = 5,
                [SpellConstants.RepelWood] = 6,
                [SpellConstants.AnimatePlants] = 7,
                [SpellConstants.ControlPlants] = 8,
                [SpellConstants.Shambler] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Plant, spellLevels);
        }

        [Test]
        public void ProtectionSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.Sanctuary] = 1,
                [SpellConstants.ShieldOther] = 2,
                [SpellConstants.ProtectionFromEnergy] = 3,
                [SpellConstants.SpellImmunity] = 4,
                [SpellConstants.SpellResistance] = 5,
                [SpellConstants.AntimagicField] = 6,
                [SpellConstants.Repulsion] = 7,
                [SpellConstants.MindBlank] = 8,
                [SpellConstants.PrismaticSphere] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Protection, spellLevels);
        }

        [Test]
        public void SunSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.EndureElements] = 1,
                [SpellConstants.HeatMetal] = 2,
                [SpellConstants.SearingLight] = 3,
                [SpellConstants.FireShield] = 4,
                [SpellConstants.FlameStrike] = 5,
                [SpellConstants.FireSeeds] = 6,
                [SpellConstants.Sunbeam] = 7,
                [SpellConstants.Sunburst] = 8,
                [SpellConstants.PrismaticSphere] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Sun, spellLevels);
        }

        [Test]
        public void TrickerySpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.DisguiseSelf] = 1,
                [SpellConstants.Invisibility] = 2,
                [SpellConstants.Nondetection] = 3,
                [SpellConstants.Confusion] = 4,
                [SpellConstants.FalseVision] = 5,
                [SpellConstants.Mislead] = 6,
                [SpellConstants.Screen] = 7,
                [SpellConstants.PolymorphAnyObject] = 8,
                [SpellConstants.TimeStop] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Trickery, spellLevels);
        }

        [Test]
        public void WarSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.MagicWeapon] = 1,
                [SpellConstants.SpiritualWeapon] = 2,
                [SpellConstants.MagicVestment] = 3,
                [SpellConstants.DivinePower] = 4,
                [SpellConstants.FlameStrike] = 5,
                [SpellConstants.BladeBarrier] = 6,
                [SpellConstants.PowerWordBlind] = 7,
                [SpellConstants.PowerWordStun] = 8,
                [SpellConstants.PowerWordKill] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.War, spellLevels);
        }

        [Test]
        public void WaterSpellLevels()
        {
            var spellLevels = new Dictionary<string, int>
            {
                [SpellConstants.ObscuringMist] = 1,
                [SpellConstants.FogCloud] = 2,
                [SpellConstants.WaterBreathing] = 3,
                [SpellConstants.ControlWater] = 4,
                [SpellConstants.IceStorm] = 5,
                [SpellConstants.ConeOfCold] = 6,
                [SpellConstants.AcidFog] = 7,
                [SpellConstants.HorridWilting] = 8,
                [SpellConstants.ElementalSwarm] = 9
            };

            AssertTypesAndAmounts(SpellConstants.Domains.Water, spellLevels);
        }
    }
}
