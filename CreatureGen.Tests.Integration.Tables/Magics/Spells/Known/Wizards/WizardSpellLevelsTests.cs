using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class WizardSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Wizard);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DisguiseSelf,
                SpellConstants.AcidSplash,
                SpellConstants.Resistance,
                SpellConstants.DetectPoison,
                SpellConstants.DetectMagic,
                SpellConstants.ReadMagic,
                SpellConstants.Daze,
                SpellConstants.DancingLights,
                SpellConstants.Flare,
                SpellConstants.Light,
                SpellConstants.RayOfFrost,
                SpellConstants.GhostSound,
                SpellConstants.DisruptUndead,
                SpellConstants.TouchOfFatigue,
                SpellConstants.MageHand,
                SpellConstants.Mending,
                SpellConstants.Message,
                SpellConstants.OpenClose,
                SpellConstants.ArcaneMark,
                SpellConstants.Prestidigitation,
                SpellConstants.Alarm,
                SpellConstants.EndureElements,
                SpellConstants.HoldPortal,
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.Shield,
                SpellConstants.Grease,
                SpellConstants.MageArmor,
                SpellConstants.Mount,
                SpellConstants.ObscuringMist,
                SpellConstants.SummonMonsterI,
                SpellConstants.UnseenServant,
                SpellConstants.ComprehendLanguages,
                SpellConstants.DetectSecretDoors,
                SpellConstants.DetectUndead,
                SpellConstants.Identify,
                SpellConstants.TrueStrike,
                SpellConstants.CharmPerson,
                SpellConstants.Hypnotism,
                SpellConstants.Sleep,
                SpellConstants.BurningHands,
                SpellConstants.FloatingDisk,
                SpellConstants.MagicMissile,
                SpellConstants.ShockingGrasp,
                SpellConstants.ColorSpray,
                SpellConstants.MagicAura,
                SpellConstants.SilentImage,
                SpellConstants.Ventriloquism,
                SpellConstants.CauseFear,
                SpellConstants.ChillTouch,
                SpellConstants.RayOfEnfeeblement,
                SpellConstants.AnimateRope,
                SpellConstants.EnlargePerson,
                SpellConstants.Erase,
                SpellConstants.ExpeditiousRetreat,
                SpellConstants.FeatherFall,
                SpellConstants.Jump,
                SpellConstants.MagicWeapon,
                SpellConstants.ReducePerson,
                SpellConstants.ArcaneLock,
                SpellConstants.ObscureObject,
                SpellConstants.ProtectionFromArrows,
                SpellConstants.ResistEnergy,
                SpellConstants.AcidArrow,
                SpellConstants.FogCloud,
                SpellConstants.Glitterdust,
                SpellConstants.SummonMonsterII,
                SpellConstants.SummonSwarm,
                SpellConstants.Web,
                SpellConstants.DetectThoughts,
                SpellConstants.LocateObject,
                SpellConstants.SeeInvisibility,
                SpellConstants.DazeMonster,
                SpellConstants.HideousLaughter,
                SpellConstants.TouchOfIdiocy,
                SpellConstants.ContinualFlame,
                SpellConstants.Darkness,
                SpellConstants.FlamingSphere,
                SpellConstants.GustOfWind,
                SpellConstants.ScorchingRay,
                SpellConstants.Shatter,
                SpellConstants.Blur,
                SpellConstants.HypnoticPattern,
                SpellConstants.Invisibility,
                SpellConstants.MagicMouth,
                SpellConstants.MinorImage,
                SpellConstants.MirrorImage,
                SpellConstants.Misdirection,
                SpellConstants.PhantomTrap,
                SpellConstants.BlindnessDeafness,
                SpellConstants.CommandUndead,
                SpellConstants.FalseLife,
                SpellConstants.GhoulTouch,
                SpellConstants.Scare,
                SpellConstants.SpectralHand,
                SpellConstants.AlterSelf,
                SpellConstants.BearsEndurance,
                SpellConstants.BullsStrength,
                SpellConstants.CatsGrace,
                SpellConstants.Darkvision,
                SpellConstants.EaglesSplendor,
                SpellConstants.FoxsCunning,
                SpellConstants.Knock,
                SpellConstants.Levitate,
                SpellConstants.OwlsWisdom,
                SpellConstants.Pyrotechnics,
                SpellConstants.RopeTrick,
                SpellConstants.SpiderClimb,
                SpellConstants.WhisperingWind,
                SpellConstants.DispelMagic,
                SpellConstants.ExplosiveRunes,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.Nondetection,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.PhantomSteed,
                SpellConstants.SepiaSnakeSigil,
                SpellConstants.SleetStorm,
                SpellConstants.StinkingCloud,
                SpellConstants.SummonMonsterIII,
                SpellConstants.ArcaneSight,
                SpellConstants.ClairaudienceClairvoyance,
                SpellConstants.Tongues,
                SpellConstants.DeepSlumber,
                SpellConstants.Heroism,
                SpellConstants.HoldPerson,
                SpellConstants.Rage,
                SpellConstants.Suggestion,
                SpellConstants.Daylight,
                SpellConstants.Fireball,
                SpellConstants.LightningBolt,
                SpellConstants.TinyHut,
                SpellConstants.WindWall,
                SpellConstants.Displacement,
                SpellConstants.IllusoryScript,
                SpellConstants.InvisibilitySphere,
                SpellConstants.MajorImage,
                SpellConstants.GentleRepose,
                SpellConstants.HaltUndead,
                SpellConstants.RayOfExhaustion,
                SpellConstants.VampiricTouch,
                SpellConstants.Blink,
                SpellConstants.FlameArrow,
                SpellConstants.Fly,
                SpellConstants.GaseousForm,
                SpellConstants.Haste,
                SpellConstants.KeenEdge,
                SpellConstants.MagicWeapon_Greater,
                SpellConstants.SecretPage,
                SpellConstants.ShrinkItem,
                SpellConstants.Slow,
                SpellConstants.WaterBreathing,
                SpellConstants.DimensionalAnchor,
                SpellConstants.FireTrap,
                SpellConstants.GlobeOfInvulnerability_Lesser,
                SpellConstants.RemoveCurse,
                SpellConstants.Stoneskin,
                SpellConstants.BlackTentacles,
                SpellConstants.DimensionDoor,
                SpellConstants.MinorCreation,
                SpellConstants.SecureShelter,
                SpellConstants.SolidFog,
                SpellConstants.SummonMonsterIV,
                SpellConstants.ArcaneEye,
                SpellConstants.DetectScrying,
                SpellConstants.LocateCreature,
                SpellConstants.Scrying,
                SpellConstants.CharmMonster,
                SpellConstants.Confusion,
                SpellConstants.CrushingDespair,
                SpellConstants.Geas_Lesser,
                SpellConstants.FireShield,
                SpellConstants.IceStorm,
                SpellConstants.ResilientSphere,
                SpellConstants.Shout,
                SpellConstants.WallOfFire,
                SpellConstants.WallOfIce,
                SpellConstants.HallucinatoryTerrain,
                SpellConstants.IllusoryWall,
                SpellConstants.Invisibility_Greater,
                SpellConstants.PhantasmalKiller,
                SpellConstants.RainbowPattern,
                SpellConstants.AnimateDead,
                SpellConstants.BestowCurse,
                SpellConstants.Contagion,
                SpellConstants.Enervation,
                SpellConstants.Fear,
                SpellConstants.EnlargePerson_Mass,
                SpellConstants.Polymorph,
                SpellConstants.ReducePerson_Mass,
                SpellConstants.StoneShape,
                SpellConstants.BreakEnchantment,
                SpellConstants.Dismissal,
                SpellConstants.MagesPrivateSanctum,
                SpellConstants.Cloudkill,
                SpellConstants.MagesFaithfulHound,
                SpellConstants.MajorCreation,
                SpellConstants.PlanarBinding_Lesser,
                SpellConstants.SecretChest,
                SpellConstants.SummonMonsterV,
                SpellConstants.Teleport,
                SpellConstants.WallOfStone,
                SpellConstants.ContactOtherPlane,
                SpellConstants.PryingEyes,
                SpellConstants.TelepathicBond,
                SpellConstants.DominatePerson,
                SpellConstants.Feeblemind,
                SpellConstants.HoldMonster,
                SpellConstants.MindFog,
                SpellConstants.SymbolOfSleep,
                SpellConstants.ConeOfCold,
                SpellConstants.InterposingHand,
                SpellConstants.Sending,
                SpellConstants.WallOfForce,
                SpellConstants.Dream,
                SpellConstants.FalseVision,
                SpellConstants.MirageArcana,
                SpellConstants.Nightmare,
                SpellConstants.PersistentImage,
                SpellConstants.Seeming,
                SpellConstants.ShadowEvocation,
                SpellConstants.Blight,
                SpellConstants.MagicJar,
                SpellConstants.SymbolOfPain,
                SpellConstants.WavesOfFatigue,
                SpellConstants.AnimalGrowth,
                SpellConstants.Fabricate,
                SpellConstants.OverlandFlight,
                SpellConstants.Passwall,
                SpellConstants.Telekinesis,
                SpellConstants.TransmuteMudToRock,
                SpellConstants.TransmuteRockToMud,
                SpellConstants.Permanency,
                SpellConstants.AntimagicField,
                SpellConstants.DispelMagic_Greater,
                SpellConstants.GlobeOfInvulnerability,
                SpellConstants.GuardsAndWards,
                SpellConstants.Repulsion,
                SpellConstants.AcidFog,
                SpellConstants.PlanarBinding,
                SpellConstants.SummonMonsterVI,
                SpellConstants.WallOfIron,
                SpellConstants.AnalyzeDweomer,
                SpellConstants.LegendLore,
                SpellConstants.TrueSeeing,
                SpellConstants.GeasQuest,
                SpellConstants.Heroism_Greater,
                SpellConstants.Suggestion_Mass,
                SpellConstants.SymbolOfPersuasion,
                SpellConstants.ChainLightning,
                SpellConstants.Contingency,
                SpellConstants.ForcefulHand,
                SpellConstants.FreezingSphere,
                SpellConstants.Mislead,
                SpellConstants.PermanentImage,
                SpellConstants.ProgrammedImage,
                SpellConstants.ShadowWalk,
                SpellConstants.Veil,
                SpellConstants.CircleOfDeath,
                SpellConstants.Eyebite,
                SpellConstants.SymbolOfFear,
                SpellConstants.UndeathToDeath,
                SpellConstants.BearsEndurance_Mass,
                SpellConstants.BullsStrength_Mass,
                SpellConstants.CatsGrace_Mass,
                SpellConstants.ControlWater,
                SpellConstants.Disintegrate,
                SpellConstants.EaglesSplendor_Mass,
                SpellConstants.FleshToStone,
                SpellConstants.FoxsCunning_Mass,
                SpellConstants.MoveEarth,
                SpellConstants.OwlsWisdom_Mass,
                SpellConstants.StoneToFlesh,
                SpellConstants.Transformation,
                SpellConstants.Banishment,
                SpellConstants.Sequester,
                SpellConstants.SpellTurning,
                SpellConstants.InstantSummons,
                SpellConstants.MagesMagnificentMansion,
                SpellConstants.PhaseDoor,
                SpellConstants.PlaneShift,
                SpellConstants.SummonMonsterVII,
                SpellConstants.Teleport_Greater,
                SpellConstants.TeleportObject,
                SpellConstants.ArcaneSight_Greater,
                SpellConstants.Scrying_Greater,
                SpellConstants.Vision,
                SpellConstants.HoldPerson_Mass,
                SpellConstants.Insanity,
                SpellConstants.PowerWordBlind,
                SpellConstants.SymbolOfStunning,
                SpellConstants.DelayedBlastFireball,
                SpellConstants.Forcecage,
                SpellConstants.GraspingHand,
                SpellConstants.MagesSword,
                SpellConstants.PrismaticSpray,
                SpellConstants.Invisibility_Mass,
                SpellConstants.ProjectImage,
                SpellConstants.ShadowConjuration_Greater,
                SpellConstants.Simulacrum,
                SpellConstants.ControlUndead,
                SpellConstants.FingerOfDeath,
                SpellConstants.SymbolOfWeakness,
                SpellConstants.WavesOfExhaustion,
                SpellConstants.ControlWeather,
                SpellConstants.EtherealJaunt,
                SpellConstants.ReverseGravity,
                SpellConstants.Statue,
                SpellConstants.LimitedWish,
                SpellConstants.DimensionalLock,
                SpellConstants.MindBlank,
                SpellConstants.PrismaticWall,
                SpellConstants.ProtectionFromSpells,
                SpellConstants.IncendiaryCloud,
                SpellConstants.Maze,
                SpellConstants.PlanarBinding_Greater,
                SpellConstants.SummonMonsterVIII,
                SpellConstants.TrapTheSoul,
                SpellConstants.DiscernLocation,
                SpellConstants.MomentOfPrescience,
                SpellConstants.PryingEyes_Greater,
                SpellConstants.Antipathy,
                SpellConstants.Binding,
                SpellConstants.CharmMonster_Mass,
                SpellConstants.Demand,
                SpellConstants.IrresistibleDance,
                SpellConstants.PowerWordStun,
                SpellConstants.SymbolOfInsanity,
                SpellConstants.Sympathy,
                SpellConstants.ClenchedFist,
                SpellConstants.PolarRay,
                SpellConstants.Shout_Greater,
                SpellConstants.Sunburst,
                SpellConstants.TelekineticSphere,
                SpellConstants.ScintillatingPattern,
                SpellConstants.Screen,
                SpellConstants.ShadowEvocation_Greater,
                SpellConstants.Clone,
                SpellConstants.CreateGreaterUndead,
                SpellConstants.HorridWilting,
                SpellConstants.SymbolOfDeath,
                SpellConstants.IronBody,
                SpellConstants.PolymorphAnyObject,
                SpellConstants.TemporalStasis,
                SpellConstants.Freedom,
                SpellConstants.Imprisonment,
                SpellConstants.MagesDisjunction,
                SpellConstants.PrismaticSphere,
                SpellConstants.Gate,
                SpellConstants.Refuge,
                SpellConstants.SummonMonsterIX,
                SpellConstants.TeleportationCircle,
                SpellConstants.Foresight,
                SpellConstants.DominateMonster,
                SpellConstants.HoldMonster_Mass,
                SpellConstants.PowerWordKill,
                SpellConstants.CrushingHand,
                SpellConstants.MeteorSwarm,
                SpellConstants.Shades,
                SpellConstants.Weird,
                SpellConstants.AstralProjection,
                SpellConstants.EnergyDrain,
                SpellConstants.SoulBind,
                SpellConstants.WailOfTheBanshee,
                SpellConstants.Etherealness,
                SpellConstants.Shapechange,
                SpellConstants.TimeStop,
                SpellConstants.Wish,
                SpellConstants.ShadowConjuration,
                SpellConstants.CreateUndead,
                SpellConstants.MnemonicEnhancer,
                SpellConstants.MagesLucubration
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllWizardSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Wizard]);
        }

        [TestCase(SpellConstants.AcidSplash, 0)]
        [TestCase(SpellConstants.Resistance, 0)]
        [TestCase(SpellConstants.DetectPoison, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.Daze, 0)]
        [TestCase(SpellConstants.DancingLights, 0)]
        [TestCase(SpellConstants.Flare, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.RayOfFrost, 0)]
        [TestCase(SpellConstants.GhostSound, 0)]
        [TestCase(SpellConstants.DisruptUndead, 0)]
        [TestCase(SpellConstants.TouchOfFatigue, 0)]
        [TestCase(SpellConstants.MageHand, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.Message, 0)]
        [TestCase(SpellConstants.OpenClose, 0)]
        [TestCase(SpellConstants.ArcaneMark, 0)]
        [TestCase(SpellConstants.Prestidigitation, 0)]
        [TestCase(SpellConstants.Alarm, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.HoldPortal, 1)]
        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.Shield, 1)]
        [TestCase(SpellConstants.Grease, 1)]
        [TestCase(SpellConstants.MageArmor, 1)]
        [TestCase(SpellConstants.Mount, 1)]
        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.SummonMonsterI, 1)]
        [TestCase(SpellConstants.UnseenServant, 1)]
        [TestCase(SpellConstants.ComprehendLanguages, 1)]
        [TestCase(SpellConstants.DetectSecretDoors, 1)]
        [TestCase(SpellConstants.DetectUndead, 1)]
        [TestCase(SpellConstants.Identify, 1)]
        [TestCase(SpellConstants.TrueStrike, 1)]
        [TestCase(SpellConstants.CharmPerson, 1)]
        [TestCase(SpellConstants.Hypnotism, 1)]
        [TestCase(SpellConstants.Sleep, 1)]
        [TestCase(SpellConstants.BurningHands, 1)]
        [TestCase(SpellConstants.FloatingDisk, 1)]
        [TestCase(SpellConstants.MagicMissile, 1)]
        [TestCase(SpellConstants.ShockingGrasp, 1)]
        [TestCase(SpellConstants.ColorSpray, 1)]
        [TestCase(SpellConstants.DisguiseSelf, 1)]
        [TestCase(SpellConstants.MagicAura, 1)]
        [TestCase(SpellConstants.SilentImage, 1)]
        [TestCase(SpellConstants.Ventriloquism, 1)]
        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.ChillTouch, 1)]
        [TestCase(SpellConstants.RayOfEnfeeblement, 1)]
        [TestCase(SpellConstants.AnimateRope, 1)]
        [TestCase(SpellConstants.EnlargePerson, 1)]
        [TestCase(SpellConstants.Erase, 1)]
        [TestCase(SpellConstants.ExpeditiousRetreat, 1)]
        [TestCase(SpellConstants.FeatherFall, 1)]
        [TestCase(SpellConstants.Jump, 1)]
        [TestCase(SpellConstants.MagicWeapon, 1)]
        [TestCase(SpellConstants.ReducePerson, 1)]
        [TestCase(SpellConstants.ArcaneLock, 2)]
        [TestCase(SpellConstants.ObscureObject, 2)]
        [TestCase(SpellConstants.ProtectionFromArrows, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.AcidArrow, 2)]
        [TestCase(SpellConstants.FogCloud, 2)]
        [TestCase(SpellConstants.Glitterdust, 2)]
        [TestCase(SpellConstants.SummonMonsterII, 2)]
        [TestCase(SpellConstants.SummonSwarm, 2)]
        [TestCase(SpellConstants.Web, 2)]
        [TestCase(SpellConstants.DetectThoughts, 2)]
        [TestCase(SpellConstants.LocateObject, 2)]
        [TestCase(SpellConstants.SeeInvisibility, 2)]
        [TestCase(SpellConstants.DazeMonster, 2)]
        [TestCase(SpellConstants.HideousLaughter, 2)]
        [TestCase(SpellConstants.TouchOfIdiocy, 2)]
        [TestCase(SpellConstants.ContinualFlame, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.FlamingSphere, 2)]
        [TestCase(SpellConstants.GustOfWind, 2)]
        [TestCase(SpellConstants.ScorchingRay, 2)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Blur, 2)]
        [TestCase(SpellConstants.HypnoticPattern, 2)]
        [TestCase(SpellConstants.Invisibility, 2)]
        [TestCase(SpellConstants.MagicMouth, 2)]
        [TestCase(SpellConstants.MinorImage, 2)]
        [TestCase(SpellConstants.MirrorImage, 2)]
        [TestCase(SpellConstants.Misdirection, 2)]
        [TestCase(SpellConstants.PhantomTrap, 2)]
        [TestCase(SpellConstants.BlindnessDeafness, 2)]
        [TestCase(SpellConstants.CommandUndead, 2)]
        [TestCase(SpellConstants.FalseLife, 2)]
        [TestCase(SpellConstants.GhoulTouch, 2)]
        [TestCase(SpellConstants.Scare, 2)]
        [TestCase(SpellConstants.SpectralHand, 2)]
        [TestCase(SpellConstants.AlterSelf, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.Darkvision, 2)]
        [TestCase(SpellConstants.EaglesSplendor, 2)]
        [TestCase(SpellConstants.FoxsCunning, 2)]
        [TestCase(SpellConstants.Knock, 2)]
        [TestCase(SpellConstants.Levitate, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.Pyrotechnics, 2)]
        [TestCase(SpellConstants.RopeTrick, 2)]
        [TestCase(SpellConstants.SpiderClimb, 2)]
        [TestCase(SpellConstants.WhisperingWind, 2)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.ExplosiveRunes, 3)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.Nondetection, 3)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.PhantomSteed, 3)]
        [TestCase(SpellConstants.SepiaSnakeSigil, 3)]
        [TestCase(SpellConstants.SleetStorm, 3)]
        [TestCase(SpellConstants.StinkingCloud, 3)]
        [TestCase(SpellConstants.SummonMonsterIII, 3)]
        [TestCase(SpellConstants.ArcaneSight, 3)]
        [TestCase(SpellConstants.ClairaudienceClairvoyance, 3)]
        [TestCase(SpellConstants.Tongues, 3)]
        [TestCase(SpellConstants.DeepSlumber, 3)]
        [TestCase(SpellConstants.Heroism, 3)]
        [TestCase(SpellConstants.HoldPerson, 3)]
        [TestCase(SpellConstants.Rage, 3)]
        [TestCase(SpellConstants.Suggestion, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.Fireball, 3)]
        [TestCase(SpellConstants.LightningBolt, 3)]
        [TestCase(SpellConstants.TinyHut, 3)]
        [TestCase(SpellConstants.WindWall, 3)]
        [TestCase(SpellConstants.Displacement, 3)]
        [TestCase(SpellConstants.IllusoryScript, 3)]
        [TestCase(SpellConstants.InvisibilitySphere, 3)]
        [TestCase(SpellConstants.MajorImage, 3)]
        [TestCase(SpellConstants.GentleRepose, 3)]
        [TestCase(SpellConstants.HaltUndead, 3)]
        [TestCase(SpellConstants.RayOfExhaustion, 3)]
        [TestCase(SpellConstants.VampiricTouch, 3)]
        [TestCase(SpellConstants.Blink, 3)]
        [TestCase(SpellConstants.FlameArrow, 3)]
        [TestCase(SpellConstants.Fly, 3)]
        [TestCase(SpellConstants.GaseousForm, 3)]
        [TestCase(SpellConstants.Haste, 3)]
        [TestCase(SpellConstants.KeenEdge, 3)]
        [TestCase(SpellConstants.MagicWeapon_Greater, 3)]
        [TestCase(SpellConstants.SecretPage, 3)]
        [TestCase(SpellConstants.ShrinkItem, 3)]
        [TestCase(SpellConstants.Slow, 3)]
        [TestCase(SpellConstants.WaterBreathing, 3)]
        [TestCase(SpellConstants.DimensionalAnchor, 4)]
        [TestCase(SpellConstants.FireTrap, 4)]
        [TestCase(SpellConstants.GlobeOfInvulnerability_Lesser, 4)]
        [TestCase(SpellConstants.RemoveCurse, 4)]
        [TestCase(SpellConstants.Stoneskin, 4)]
        [TestCase(SpellConstants.BlackTentacles, 4)]
        [TestCase(SpellConstants.DimensionDoor, 4)]
        [TestCase(SpellConstants.MinorCreation, 4)]
        [TestCase(SpellConstants.SecureShelter, 4)]
        [TestCase(SpellConstants.SolidFog, 4)]
        [TestCase(SpellConstants.SummonMonsterIV, 4)]
        [TestCase(SpellConstants.ArcaneEye, 4)]
        [TestCase(SpellConstants.DetectScrying, 4)]
        [TestCase(SpellConstants.LocateCreature, 4)]
        [TestCase(SpellConstants.Scrying, 4)]
        [TestCase(SpellConstants.CharmMonster, 4)]
        [TestCase(SpellConstants.Confusion, 4)]
        [TestCase(SpellConstants.CrushingDespair, 4)]
        [TestCase(SpellConstants.Geas_Lesser, 4)]
        [TestCase(SpellConstants.FireShield, 4)]
        [TestCase(SpellConstants.IceStorm, 4)]
        [TestCase(SpellConstants.ResilientSphere, 4)]
        [TestCase(SpellConstants.Shout, 4)]
        [TestCase(SpellConstants.WallOfFire, 4)]
        [TestCase(SpellConstants.WallOfIce, 4)]
        [TestCase(SpellConstants.HallucinatoryTerrain, 4)]
        [TestCase(SpellConstants.IllusoryWall, 4)]
        [TestCase(SpellConstants.Invisibility_Greater, 4)]
        [TestCase(SpellConstants.PhantasmalKiller, 4)]
        [TestCase(SpellConstants.RainbowPattern, 4)]
        [TestCase(SpellConstants.ShadowConjuration, 4)]
        [TestCase(SpellConstants.AnimateDead, 4)]
        [TestCase(SpellConstants.BestowCurse, 4)]
        [TestCase(SpellConstants.Contagion, 4)]
        [TestCase(SpellConstants.Enervation, 4)]
        [TestCase(SpellConstants.Fear, 4)]
        [TestCase(SpellConstants.EnlargePerson_Mass, 4)]
        [TestCase(SpellConstants.MnemonicEnhancer, 4)]
        [TestCase(SpellConstants.Polymorph, 4)]
        [TestCase(SpellConstants.ReducePerson_Mass, 4)]
        [TestCase(SpellConstants.StoneShape, 4)]
        [TestCase(SpellConstants.BreakEnchantment, 5)]
        [TestCase(SpellConstants.Dismissal, 5)]
        [TestCase(SpellConstants.MagesPrivateSanctum, 5)]
        [TestCase(SpellConstants.Cloudkill, 5)]
        [TestCase(SpellConstants.MagesFaithfulHound, 5)]
        [TestCase(SpellConstants.MajorCreation, 5)]
        [TestCase(SpellConstants.PlanarBinding_Lesser, 5)]
        [TestCase(SpellConstants.SecretChest, 5)]
        [TestCase(SpellConstants.SummonMonsterV, 5)]
        [TestCase(SpellConstants.Teleport, 5)]
        [TestCase(SpellConstants.WallOfStone, 5)]
        [TestCase(SpellConstants.ContactOtherPlane, 5)]
        [TestCase(SpellConstants.PryingEyes, 5)]
        [TestCase(SpellConstants.TelepathicBond, 5)]
        [TestCase(SpellConstants.DominatePerson, 5)]
        [TestCase(SpellConstants.Feeblemind, 5)]
        [TestCase(SpellConstants.HoldMonster, 5)]
        [TestCase(SpellConstants.MindFog, 5)]
        [TestCase(SpellConstants.SymbolOfSleep, 5)]
        [TestCase(SpellConstants.ConeOfCold, 5)]
        [TestCase(SpellConstants.InterposingHand, 5)]
        [TestCase(SpellConstants.Sending, 5)]
        [TestCase(SpellConstants.WallOfForce, 5)]
        [TestCase(SpellConstants.Dream, 5)]
        [TestCase(SpellConstants.FalseVision, 5)]
        [TestCase(SpellConstants.MirageArcana, 5)]
        [TestCase(SpellConstants.Nightmare, 5)]
        [TestCase(SpellConstants.PersistentImage, 5)]
        [TestCase(SpellConstants.Seeming, 5)]
        [TestCase(SpellConstants.ShadowEvocation, 5)]
        [TestCase(SpellConstants.Blight, 5)]
        [TestCase(SpellConstants.MagicJar, 5)]
        [TestCase(SpellConstants.SymbolOfPain, 5)]
        [TestCase(SpellConstants.WavesOfFatigue, 5)]
        [TestCase(SpellConstants.AnimalGrowth, 5)]
        [TestCase(SpellConstants.Fabricate, 5)]
        [TestCase(SpellConstants.OverlandFlight, 5)]
        [TestCase(SpellConstants.Passwall, 5)]
        [TestCase(SpellConstants.Telekinesis, 5)]
        [TestCase(SpellConstants.TransmuteMudToRock, 5)]
        [TestCase(SpellConstants.TransmuteRockToMud, 5)]
        [TestCase(SpellConstants.Permanency, 5)]
        [TestCase(SpellConstants.AntimagicField, 6)]
        [TestCase(SpellConstants.DispelMagic_Greater, 6)]
        [TestCase(SpellConstants.GlobeOfInvulnerability, 6)]
        [TestCase(SpellConstants.GuardsAndWards, 6)]
        [TestCase(SpellConstants.Repulsion, 6)]
        [TestCase(SpellConstants.AcidFog, 6)]
        [TestCase(SpellConstants.PlanarBinding, 6)]
        [TestCase(SpellConstants.SummonMonsterVI, 6)]
        [TestCase(SpellConstants.WallOfIron, 6)]
        [TestCase(SpellConstants.AnalyzeDweomer, 6)]
        [TestCase(SpellConstants.LegendLore, 6)]
        [TestCase(SpellConstants.TrueSeeing, 6)]
        [TestCase(SpellConstants.GeasQuest, 6)]
        [TestCase(SpellConstants.Heroism_Greater, 6)]
        [TestCase(SpellConstants.Suggestion_Mass, 6)]
        [TestCase(SpellConstants.SymbolOfPersuasion, 6)]
        [TestCase(SpellConstants.ChainLightning, 6)]
        [TestCase(SpellConstants.Contingency, 6)]
        [TestCase(SpellConstants.ForcefulHand, 6)]
        [TestCase(SpellConstants.FreezingSphere, 6)]
        [TestCase(SpellConstants.Mislead, 6)]
        [TestCase(SpellConstants.PermanentImage, 6)]
        [TestCase(SpellConstants.ProgrammedImage, 6)]
        [TestCase(SpellConstants.ShadowWalk, 6)]
        [TestCase(SpellConstants.Veil, 6)]
        [TestCase(SpellConstants.CircleOfDeath, 6)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.Eyebite, 6)]
        [TestCase(SpellConstants.SymbolOfFear, 6)]
        [TestCase(SpellConstants.UndeathToDeath, 6)]
        [TestCase(SpellConstants.BearsEndurance_Mass, 6)]
        [TestCase(SpellConstants.BullsStrength_Mass, 6)]
        [TestCase(SpellConstants.CatsGrace_Mass, 6)]
        [TestCase(SpellConstants.ControlWater, 6)]
        [TestCase(SpellConstants.Disintegrate, 6)]
        [TestCase(SpellConstants.EaglesSplendor_Mass, 6)]
        [TestCase(SpellConstants.FleshToStone, 6)]
        [TestCase(SpellConstants.FoxsCunning_Mass, 6)]
        [TestCase(SpellConstants.MagesLucubration, 6)]
        [TestCase(SpellConstants.MoveEarth, 6)]
        [TestCase(SpellConstants.OwlsWisdom_Mass, 6)]
        [TestCase(SpellConstants.StoneToFlesh, 6)]
        [TestCase(SpellConstants.Transformation, 6)]
        [TestCase(SpellConstants.Banishment, 7)]
        [TestCase(SpellConstants.Sequester, 7)]
        [TestCase(SpellConstants.SpellTurning, 7)]
        [TestCase(SpellConstants.InstantSummons, 7)]
        [TestCase(SpellConstants.MagesMagnificentMansion, 7)]
        [TestCase(SpellConstants.PhaseDoor, 7)]
        [TestCase(SpellConstants.PlaneShift, 7)]
        [TestCase(SpellConstants.SummonMonsterVII, 7)]
        [TestCase(SpellConstants.Teleport_Greater, 7)]
        [TestCase(SpellConstants.TeleportObject, 7)]
        [TestCase(SpellConstants.ArcaneSight_Greater, 7)]
        [TestCase(SpellConstants.Scrying_Greater, 7)]
        [TestCase(SpellConstants.Vision, 7)]
        [TestCase(SpellConstants.HoldPerson_Mass, 7)]
        [TestCase(SpellConstants.Insanity, 7)]
        [TestCase(SpellConstants.PowerWordBlind, 7)]
        [TestCase(SpellConstants.SymbolOfStunning, 7)]
        [TestCase(SpellConstants.DelayedBlastFireball, 7)]
        [TestCase(SpellConstants.Forcecage, 7)]
        [TestCase(SpellConstants.GraspingHand, 7)]
        [TestCase(SpellConstants.MagesSword, 7)]
        [TestCase(SpellConstants.PrismaticSpray, 7)]
        [TestCase(SpellConstants.Invisibility_Mass, 7)]
        [TestCase(SpellConstants.ProjectImage, 7)]
        [TestCase(SpellConstants.ShadowConjuration_Greater, 7)]
        [TestCase(SpellConstants.Simulacrum, 7)]
        [TestCase(SpellConstants.ControlUndead, 7)]
        [TestCase(SpellConstants.FingerOfDeath, 7)]
        [TestCase(SpellConstants.SymbolOfWeakness, 7)]
        [TestCase(SpellConstants.WavesOfExhaustion, 7)]
        [TestCase(SpellConstants.ControlWeather, 7)]
        [TestCase(SpellConstants.EtherealJaunt, 7)]
        [TestCase(SpellConstants.ReverseGravity, 7)]
        [TestCase(SpellConstants.Statue, 7)]
        [TestCase(SpellConstants.LimitedWish, 7)]
        [TestCase(SpellConstants.DimensionalLock, 8)]
        [TestCase(SpellConstants.MindBlank, 8)]
        [TestCase(SpellConstants.PrismaticWall, 8)]
        [TestCase(SpellConstants.ProtectionFromSpells, 8)]
        [TestCase(SpellConstants.IncendiaryCloud, 8)]
        [TestCase(SpellConstants.Maze, 8)]
        [TestCase(SpellConstants.PlanarBinding_Greater, 8)]
        [TestCase(SpellConstants.SummonMonsterVIII, 8)]
        [TestCase(SpellConstants.TrapTheSoul, 8)]
        [TestCase(SpellConstants.DiscernLocation, 8)]
        [TestCase(SpellConstants.MomentOfPrescience, 8)]
        [TestCase(SpellConstants.PryingEyes_Greater, 8)]
        [TestCase(SpellConstants.Antipathy, 8)]
        [TestCase(SpellConstants.Binding, 8)]
        [TestCase(SpellConstants.CharmMonster_Mass, 8)]
        [TestCase(SpellConstants.Demand, 8)]
        [TestCase(SpellConstants.IrresistibleDance, 8)]
        [TestCase(SpellConstants.PowerWordStun, 8)]
        [TestCase(SpellConstants.SymbolOfInsanity, 8)]
        [TestCase(SpellConstants.Sympathy, 8)]
        [TestCase(SpellConstants.ClenchedFist, 8)]
        [TestCase(SpellConstants.PolarRay, 8)]
        [TestCase(SpellConstants.Shout_Greater, 8)]
        [TestCase(SpellConstants.Sunburst, 8)]
        [TestCase(SpellConstants.TelekineticSphere, 8)]
        [TestCase(SpellConstants.ScintillatingPattern, 8)]
        [TestCase(SpellConstants.Screen, 8)]
        [TestCase(SpellConstants.ShadowEvocation_Greater, 8)]
        [TestCase(SpellConstants.Clone, 8)]
        [TestCase(SpellConstants.CreateGreaterUndead, 8)]
        [TestCase(SpellConstants.HorridWilting, 8)]
        [TestCase(SpellConstants.SymbolOfDeath, 8)]
        [TestCase(SpellConstants.IronBody, 8)]
        [TestCase(SpellConstants.PolymorphAnyObject, 8)]
        [TestCase(SpellConstants.TemporalStasis, 8)]
        [TestCase(SpellConstants.Freedom, 9)]
        [TestCase(SpellConstants.Imprisonment, 9)]
        [TestCase(SpellConstants.MagesDisjunction, 9)]
        [TestCase(SpellConstants.PrismaticSphere, 9)]
        [TestCase(SpellConstants.Gate, 9)]
        [TestCase(SpellConstants.Refuge, 9)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        [TestCase(SpellConstants.TeleportationCircle, 9)]
        [TestCase(SpellConstants.Foresight, 9)]
        [TestCase(SpellConstants.DominateMonster, 9)]
        [TestCase(SpellConstants.HoldMonster_Mass, 9)]
        [TestCase(SpellConstants.PowerWordKill, 9)]
        [TestCase(SpellConstants.CrushingHand, 9)]
        [TestCase(SpellConstants.MeteorSwarm, 9)]
        [TestCase(SpellConstants.Shades, 9)]
        [TestCase(SpellConstants.Weird, 9)]
        [TestCase(SpellConstants.AstralProjection, 9)]
        [TestCase(SpellConstants.EnergyDrain, 9)]
        [TestCase(SpellConstants.SoulBind, 9)]
        [TestCase(SpellConstants.WailOfTheBanshee, 9)]
        [TestCase(SpellConstants.Etherealness, 9)]
        [TestCase(SpellConstants.Shapechange, 9)]
        [TestCase(SpellConstants.TimeStop, 9)]
        [TestCase(SpellConstants.Wish, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
