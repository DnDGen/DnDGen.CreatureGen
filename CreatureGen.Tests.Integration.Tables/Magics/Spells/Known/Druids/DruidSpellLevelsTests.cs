using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Druids
{
    [TestFixture]
    public class DruidSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Druid);
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
                SpellConstants.Flare,
                SpellConstants.Guidance,
                SpellConstants.KnowDirection,
                SpellConstants.Light,
                SpellConstants.Mending,
                SpellConstants.PurifyFoodAndDrink,
                SpellConstants.ReadMagic,
                SpellConstants.Resistance,
                SpellConstants.Virtue,
                SpellConstants.CalmAnimals,
                SpellConstants.CharmAnimal,
                SpellConstants.CureInflictLightWounds,
                SpellConstants.DetectAnimalsOrPlants,
                SpellConstants.DetectSnaresAndPits,
                SpellConstants.EndureElements,
                SpellConstants.Entangle,
                SpellConstants.FaerieFire,
                SpellConstants.Goodberry,
                SpellConstants.HideFromAnimals,
                SpellConstants.Jump,
                SpellConstants.Longstrider,
                SpellConstants.MagicFang,
                SpellConstants.MagicStone,
                SpellConstants.ObscuringMist,
                SpellConstants.PassWithoutTrace,
                SpellConstants.ProduceFlame,
                SpellConstants.Shillelagh,
                SpellConstants.SpeakWithAnimals,
                SpellConstants.SummonNaturesAllyI,
                SpellConstants.AnimalMessenger,
                SpellConstants.AnimalTrance,
                SpellConstants.Barkskin,
                SpellConstants.BearsEndurance,
                SpellConstants.BullsStrength,
                SpellConstants.CatsGrace,
                SpellConstants.ChillMetal,
                SpellConstants.DelayPoison,
                SpellConstants.FireTrap,
                SpellConstants.FlameBlade,
                SpellConstants.FlamingSphere,
                SpellConstants.FogCloud,
                SpellConstants.GustOfWind,
                SpellConstants.HeatMetal,
                SpellConstants.HoldAnimal,
                SpellConstants.OwlsWisdom,
                SpellConstants.ReduceAnimal,
                SpellConstants.ResistEnergy,
                SpellConstants.Restoration_Lesser,
                SpellConstants.SoftenEarthAndStone,
                SpellConstants.SpiderClimb,
                SpellConstants.SummonNaturesAllyII,
                SpellConstants.SummonSwarm,
                SpellConstants.TreeShape,
                SpellConstants.WarpWood,
                SpellConstants.WoodShape,
                SpellConstants.CallLightning,
                SpellConstants.Contagion,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.Daylight,
                SpellConstants.DiminishPlants,
                SpellConstants.DominateAnimal,
                SpellConstants.MagicFang_Greater,
                SpellConstants.MeldIntoStone,
                SpellConstants.NeutralizePoison,
                SpellConstants.PlantGrowth,
                SpellConstants.Poison,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.Quench,
                SpellConstants.RemoveDisease,
                SpellConstants.SleetStorm,
                SpellConstants.Snare,
                SpellConstants.SpeakWithPlants,
                SpellConstants.SpikeGrowth,
                SpellConstants.StoneShape,
                SpellConstants.SummonNaturesAllyIII,
                SpellConstants.WaterBreathing,
                SpellConstants.WindWall,
                SpellConstants.AirWalk,
                SpellConstants.AntiplantShell,
                SpellConstants.Blight,
                SpellConstants.CommandPlants,
                SpellConstants.ControlWater,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.DispelMagic,
                SpellConstants.FlameStrike,
                SpellConstants.FreedomOfMovement,
                SpellConstants.GiantVermin,
                SpellConstants.IceStorm,
                SpellConstants.Reincarnate,
                SpellConstants.RepelVermin,
                SpellConstants.RustingGrasp,
                SpellConstants.Scrying,
                SpellConstants.SpikeStones,
                SpellConstants.SummonNaturesAllyIV,
                SpellConstants.AnimalGrowth,
                SpellConstants.Atonement,
                SpellConstants.Awaken,
                SpellConstants.BalefulPolymorph,
                SpellConstants.CallLightningStorm,
                SpellConstants.CommuneWithNature,
                SpellConstants.ControlWinds,
                SpellConstants.CureInflictCriticalWounds,
                SpellConstants.DeathWard,
                SpellConstants.Hallow,
                SpellConstants.InsectPlague,
                SpellConstants.Stoneskin,
                SpellConstants.SummonNaturesAllyV,
                SpellConstants.TransmuteMudToRock,
                SpellConstants.TransmuteRockToMud,
                SpellConstants.TreeStride,
                SpellConstants.Unhallow,
                SpellConstants.WallOfFire,
                SpellConstants.WallOfThorns,
                SpellConstants.AntilifeShell,
                SpellConstants.BearsEndurance_Mass,
                SpellConstants.BullsStrength_Mass,
                SpellConstants.CatsGrace_Mass,
                SpellConstants.CureInflictLightWounds_Mass,
                SpellConstants.DispelMagic_Greater,
                SpellConstants.FindThePath,
                SpellConstants.FireSeeds,
                SpellConstants.Ironwood,
                SpellConstants.Liveoak,
                SpellConstants.MoveEarth,
                SpellConstants.OwlsWisdom_Mass,
                SpellConstants.RepelWood,
                SpellConstants.Spellstaff,
                SpellConstants.StoneTell,
                SpellConstants.SummonNaturesAllyVI,
                SpellConstants.TransportViaPlants,
                SpellConstants.WallOfStone,
                SpellConstants.AnimatePlants,
                SpellConstants.Changestaff,
                SpellConstants.ControlWeather,
                SpellConstants.CreepingDoom,
                SpellConstants.CureInflictModerateWounds_Mass,
                SpellConstants.FireStorm,
                SpellConstants.HealHarm,
                SpellConstants.Scrying_Greater,
                SpellConstants.SummonNaturesAllyVII,
                SpellConstants.Sunbeam,
                SpellConstants.TransmuteMetalToWood,
                SpellConstants.TrueSeeing,
                SpellConstants.WindWalk,
                SpellConstants.AnimalShapes,
                SpellConstants.ControlPlants,
                SpellConstants.CureInflictSeriousWounds_Mass,
                SpellConstants.Earthquake,
                SpellConstants.FingerOfDeath,
                SpellConstants.RepelMetalOrStone,
                SpellConstants.ReverseGravity,
                SpellConstants.SummonNaturesAllyVIII,
                SpellConstants.Sunburst,
                SpellConstants.Whirlwind,
                SpellConstants.WordOfRecall,
                SpellConstants.Antipathy,
                SpellConstants.CureInflictCriticalWounds_Mass,
                SpellConstants.ElementalSwarm,
                SpellConstants.Foresight,
                SpellConstants.Regenerate,
                SpellConstants.Shambler,
                SpellConstants.Shapechange,
                SpellConstants.StormOfVengeance,
                SpellConstants.SummonNaturesAllyIX,
                SpellConstants.Sympathy
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllDruidSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Druid]);
        }

        [TestCase(SpellConstants.CreateWater, 0)]
        [TestCase(SpellConstants.CureInflictMinorWounds, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.DetectPoison, 0)]
        [TestCase(SpellConstants.Flare, 0)]
        [TestCase(SpellConstants.Guidance, 0)]
        [TestCase(SpellConstants.KnowDirection, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.PurifyFoodAndDrink, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.Resistance, 0)]
        [TestCase(SpellConstants.Virtue, 0)]
        [TestCase(SpellConstants.CalmAnimals, 1)]
        [TestCase(SpellConstants.CharmAnimal, 1)]
        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.DetectAnimalsOrPlants, 1)]
        [TestCase(SpellConstants.DetectSnaresAndPits, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.Entangle, 1)]
        [TestCase(SpellConstants.FaerieFire, 1)]
        [TestCase(SpellConstants.Goodberry, 1)]
        [TestCase(SpellConstants.HideFromAnimals, 1)]
        [TestCase(SpellConstants.Jump, 1)]
        [TestCase(SpellConstants.Longstrider, 1)]
        [TestCase(SpellConstants.MagicFang, 1)]
        [TestCase(SpellConstants.MagicStone, 1)]
        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.PassWithoutTrace, 1)]
        [TestCase(SpellConstants.ProduceFlame, 1)]
        [TestCase(SpellConstants.Shillelagh, 1)]
        [TestCase(SpellConstants.SpeakWithAnimals, 1)]
        [TestCase(SpellConstants.SummonNaturesAllyI, 1)]
        [TestCase(SpellConstants.AnimalMessenger, 2)]
        [TestCase(SpellConstants.AnimalTrance, 2)]
        [TestCase(SpellConstants.Barkskin, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.ChillMetal, 2)]
        [TestCase(SpellConstants.DelayPoison, 2)]
        [TestCase(SpellConstants.FireTrap, 2)]
        [TestCase(SpellConstants.FlameBlade, 2)]
        [TestCase(SpellConstants.FlamingSphere, 2)]
        [TestCase(SpellConstants.FogCloud, 2)]
        [TestCase(SpellConstants.GustOfWind, 2)]
        [TestCase(SpellConstants.HeatMetal, 2)]
        [TestCase(SpellConstants.HoldAnimal, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.ReduceAnimal, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.Restoration_Lesser, 2)]
        [TestCase(SpellConstants.SoftenEarthAndStone, 2)]
        [TestCase(SpellConstants.SpiderClimb, 2)]
        [TestCase(SpellConstants.SummonNaturesAllyII, 2)]
        [TestCase(SpellConstants.SummonSwarm, 2)]
        [TestCase(SpellConstants.TreeShape, 2)]
        [TestCase(SpellConstants.WarpWood, 2)]
        [TestCase(SpellConstants.WoodShape, 2)]
        [TestCase(SpellConstants.CallLightning, 3)]
        [TestCase(SpellConstants.Contagion, 3)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.DiminishPlants, 3)]
        [TestCase(SpellConstants.DominateAnimal, 3)]
        [TestCase(SpellConstants.MagicFang_Greater, 3)]
        [TestCase(SpellConstants.MeldIntoStone, 3)]
        [TestCase(SpellConstants.NeutralizePoison, 3)]
        [TestCase(SpellConstants.PlantGrowth, 3)]
        [TestCase(SpellConstants.Poison, 3)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.Quench, 3)]
        [TestCase(SpellConstants.RemoveDisease, 3)]
        [TestCase(SpellConstants.SleetStorm, 3)]
        [TestCase(SpellConstants.Snare, 3)]
        [TestCase(SpellConstants.SpeakWithPlants, 3)]
        [TestCase(SpellConstants.SpikeGrowth, 3)]
        [TestCase(SpellConstants.StoneShape, 3)]
        [TestCase(SpellConstants.SummonNaturesAllyIII, 3)]
        [TestCase(SpellConstants.WaterBreathing, 3)]
        [TestCase(SpellConstants.WindWall, 3)]
        [TestCase(SpellConstants.AirWalk, 4)]
        [TestCase(SpellConstants.AntiplantShell, 4)]
        [TestCase(SpellConstants.Blight, 4)]
        [TestCase(SpellConstants.CommandPlants, 4)]
        [TestCase(SpellConstants.ControlWater, 4)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 4)]
        [TestCase(SpellConstants.DispelMagic, 4)]
        [TestCase(SpellConstants.FlameStrike, 4)]
        [TestCase(SpellConstants.FreedomOfMovement, 4)]
        [TestCase(SpellConstants.GiantVermin, 4)]
        [TestCase(SpellConstants.IceStorm, 4)]
        [TestCase(SpellConstants.Reincarnate, 4)]
        [TestCase(SpellConstants.RepelVermin, 4)]
        [TestCase(SpellConstants.RustingGrasp, 4)]
        [TestCase(SpellConstants.Scrying, 4)]
        [TestCase(SpellConstants.SpikeStones, 4)]
        [TestCase(SpellConstants.SummonNaturesAllyIV, 4)]
        [TestCase(SpellConstants.AnimalGrowth, 5)]
        [TestCase(SpellConstants.Atonement, 5)]
        [TestCase(SpellConstants.Awaken, 5)]
        [TestCase(SpellConstants.BalefulPolymorph, 5)]
        [TestCase(SpellConstants.CallLightningStorm, 5)]
        [TestCase(SpellConstants.CommuneWithNature, 5)]
        [TestCase(SpellConstants.ControlWinds, 5)]
        [TestCase(SpellConstants.CureInflictCriticalWounds, 5)]
        [TestCase(SpellConstants.DeathWard, 5)]
        [TestCase(SpellConstants.Hallow, 5)]
        [TestCase(SpellConstants.InsectPlague, 5)]
        [TestCase(SpellConstants.Stoneskin, 5)]
        [TestCase(SpellConstants.SummonNaturesAllyV, 5)]
        [TestCase(SpellConstants.TransmuteMudToRock, 5)]
        [TestCase(SpellConstants.TransmuteRockToMud, 5)]
        [TestCase(SpellConstants.TreeStride, 5)]
        [TestCase(SpellConstants.Unhallow, 5)]
        [TestCase(SpellConstants.WallOfFire, 5)]
        [TestCase(SpellConstants.WallOfThorns, 5)]
        [TestCase(SpellConstants.AntilifeShell, 6)]
        [TestCase(SpellConstants.BearsEndurance_Mass, 6)]
        [TestCase(SpellConstants.BullsStrength_Mass, 6)]
        [TestCase(SpellConstants.CatsGrace_Mass, 6)]
        [TestCase(SpellConstants.CureInflictLightWounds_Mass, 6)]
        [TestCase(SpellConstants.DispelMagic_Greater, 6)]
        [TestCase(SpellConstants.FindThePath, 6)]
        [TestCase(SpellConstants.FireSeeds, 6)]
        [TestCase(SpellConstants.Ironwood, 6)]
        [TestCase(SpellConstants.Liveoak, 6)]
        [TestCase(SpellConstants.MoveEarth, 6)]
        [TestCase(SpellConstants.OwlsWisdom_Mass, 6)]
        [TestCase(SpellConstants.RepelWood, 6)]
        [TestCase(SpellConstants.Spellstaff, 6)]
        [TestCase(SpellConstants.StoneTell, 6)]
        [TestCase(SpellConstants.SummonNaturesAllyVI, 6)]
        [TestCase(SpellConstants.TransportViaPlants, 6)]
        [TestCase(SpellConstants.WallOfStone, 6)]
        [TestCase(SpellConstants.AnimatePlants, 7)]
        [TestCase(SpellConstants.Changestaff, 7)]
        [TestCase(SpellConstants.ControlWeather, 7)]
        [TestCase(SpellConstants.CreepingDoom, 7)]
        [TestCase(SpellConstants.CureInflictModerateWounds_Mass, 7)]
        [TestCase(SpellConstants.FireStorm, 7)]
        [TestCase(SpellConstants.HealHarm, 7)]
        [TestCase(SpellConstants.Scrying_Greater, 7)]
        [TestCase(SpellConstants.SummonNaturesAllyVII, 7)]
        [TestCase(SpellConstants.Sunbeam, 7)]
        [TestCase(SpellConstants.TransmuteMetalToWood, 7)]
        [TestCase(SpellConstants.TrueSeeing, 7)]
        [TestCase(SpellConstants.WindWalk, 7)]
        [TestCase(SpellConstants.AnimalShapes, 8)]
        [TestCase(SpellConstants.ControlPlants, 8)]
        [TestCase(SpellConstants.CureInflictSeriousWounds_Mass, 8)]
        [TestCase(SpellConstants.Earthquake, 8)]
        [TestCase(SpellConstants.FingerOfDeath, 8)]
        [TestCase(SpellConstants.RepelMetalOrStone, 8)]
        [TestCase(SpellConstants.ReverseGravity, 8)]
        [TestCase(SpellConstants.SummonNaturesAllyVIII, 8)]
        [TestCase(SpellConstants.Sunburst, 8)]
        [TestCase(SpellConstants.Whirlwind, 8)]
        [TestCase(SpellConstants.WordOfRecall, 8)]
        [TestCase(SpellConstants.Antipathy, 9)]
        [TestCase(SpellConstants.CureInflictCriticalWounds_Mass, 9)]
        [TestCase(SpellConstants.ElementalSwarm, 9)]
        [TestCase(SpellConstants.Foresight, 9)]
        [TestCase(SpellConstants.Regenerate, 9)]
        [TestCase(SpellConstants.Shambler, 9)]
        [TestCase(SpellConstants.Shapechange, 9)]
        [TestCase(SpellConstants.StormOfVengeance, 9)]
        [TestCase(SpellConstants.SummonNaturesAllyIX, 9)]
        [TestCase(SpellConstants.Sympathy, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
