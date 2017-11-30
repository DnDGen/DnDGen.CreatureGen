using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureSubgroupsTests : CreatureGroupsTableTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void EntriesAreComplete()
        {
            AssertCreatureGroupEntriesAreComplete();
        }

        [TestCase(CreatureConstants.Groups.Angel,
            CreatureConstants.AstralDeva,
            CreatureConstants.Planetar,
            CreatureConstants.Solar)]
        [TestCase(CreatureConstants.Groups.AnimatedObject,
            CreatureConstants.AnimatedObject_Colossal,
            CreatureConstants.AnimatedObject_Gargantuan,
            CreatureConstants.AnimatedObject_Huge,
            CreatureConstants.AnimatedObject_Large,
            CreatureConstants.AnimatedObject_Medium,
            CreatureConstants.AnimatedObject_Small,
            CreatureConstants.AnimatedObject_Tiny)]
        [TestCase(CreatureConstants.Groups.Ant_Giant,
            CreatureConstants.Ant_Giant_Queen,
            CreatureConstants.Ant_Giant_Soldier,
            CreatureConstants.Ant_Giant_Worker)]
        [TestCase(CreatureConstants.Groups.Archon,
            CreatureConstants.HoundArchon,
            CreatureConstants.LanternArchon,
            CreatureConstants.TrumpetArchon)]
        [TestCase(CreatureConstants.Groups.Arrowhawk,
            CreatureConstants.Arrowhawk_Adult,
            CreatureConstants.Arrowhawk_Elder,
            CreatureConstants.Arrowhawk_Juvenile)]
        [TestCase(CreatureConstants.Groups.Bear,
            CreatureConstants.Bear_Black,
            CreatureConstants.Bear_Brown,
            CreatureConstants.Bear_Dire,
            CreatureConstants.Bear_Polar)]
        [TestCase(CreatureConstants.Groups.Centipede_Monstrous,
            CreatureConstants.Centipede_Monstrous_Colossal,
            CreatureConstants.Centipede_Monstrous_Gargantuan,
            CreatureConstants.Centipede_Monstrous_Huge,
            CreatureConstants.Centipede_Monstrous_Large,
            CreatureConstants.Centipede_Monstrous_Medium,
            CreatureConstants.Centipede_Monstrous_Small,
            CreatureConstants.Centipede_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Groups.Cryohydra,
            CreatureConstants.Cryohydra_10Heads,
            CreatureConstants.Cryohydra_11Heads,
            CreatureConstants.Cryohydra_12Heads,
            CreatureConstants.Cryohydra_5Heads,
            CreatureConstants.Cryohydra_6Heads,
            CreatureConstants.Cryohydra_7Heads,
            CreatureConstants.Cryohydra_8Heads,
            CreatureConstants.Cryohydra_9Heads)]
        [TestCase(CreatureConstants.Groups.Demon,
            CreatureConstants.Babau,
            CreatureConstants.Balor,
            CreatureConstants.Bebilith,
            CreatureConstants.Dretch,
            CreatureConstants.Glabrezu,
            CreatureConstants.Hezrou,
            CreatureConstants.Marilith,
            CreatureConstants.Nalfeshnee,
            CreatureConstants.Quasit,
            CreatureConstants.Retriever,
            CreatureConstants.Succubus,
            CreatureConstants.Vrock)]
        [TestCase(CreatureConstants.Groups.Devil,
            CreatureConstants.BarbedDevil_Hamatula,
            CreatureConstants.BeardedDevil_Barbazu,
            CreatureConstants.BoneDevil_Osyluth,
            CreatureConstants.ChainDevil_Kyton,
            CreatureConstants.Erinyes,
            CreatureConstants.Hellcat_Bezekira,
            CreatureConstants.HornedDevil_Cornugon,
            CreatureConstants.IceDevil_Gelugon,
            CreatureConstants.Imp,
            CreatureConstants.Lemure,
            CreatureConstants.PitFiend)]
        [TestCase(CreatureConstants.Groups.Dinosaur,
            CreatureConstants.Deinonychus,
            CreatureConstants.Elasmosaurus,
            CreatureConstants.Megaraptor,
            CreatureConstants.Triceratops,
            CreatureConstants.Tyrannosaurus)]
        [TestCase(CreatureConstants.Groups.Dragon_Black,
            CreatureConstants.Dragon_Black_Adult,
            CreatureConstants.Dragon_Black_Ancient,
            CreatureConstants.Dragon_Black_GreatWyrm,
            CreatureConstants.Dragon_Black_Juvenile,
            CreatureConstants.Dragon_Black_MatureAdult,
            CreatureConstants.Dragon_Black_Old,
            CreatureConstants.Dragon_Black_VeryOld,
            CreatureConstants.Dragon_Black_VeryYoung,
            CreatureConstants.Dragon_Black_Wyrm,
            CreatureConstants.Dragon_Black_Wyrmling,
            CreatureConstants.Dragon_Black_Young,
            CreatureConstants.Dragon_Black_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Blue,
            CreatureConstants.Dragon_Blue_Adult,
            CreatureConstants.Dragon_Blue_Ancient,
            CreatureConstants.Dragon_Blue_GreatWyrm,
            CreatureConstants.Dragon_Blue_Juvenile,
            CreatureConstants.Dragon_Blue_MatureAdult,
            CreatureConstants.Dragon_Blue_Old,
            CreatureConstants.Dragon_Blue_VeryOld,
            CreatureConstants.Dragon_Blue_VeryYoung,
            CreatureConstants.Dragon_Blue_Wyrm,
            CreatureConstants.Dragon_Blue_Wyrmling,
            CreatureConstants.Dragon_Blue_Young,
            CreatureConstants.Dragon_Blue_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Brass,
            CreatureConstants.Dragon_Brass_Adult,
            CreatureConstants.Dragon_Brass_Ancient,
            CreatureConstants.Dragon_Brass_GreatWyrm,
            CreatureConstants.Dragon_Brass_Juvenile,
            CreatureConstants.Dragon_Brass_MatureAdult,
            CreatureConstants.Dragon_Brass_Old,
            CreatureConstants.Dragon_Brass_VeryOld,
            CreatureConstants.Dragon_Brass_VeryYoung,
            CreatureConstants.Dragon_Brass_Wyrm,
            CreatureConstants.Dragon_Brass_Wyrmling,
            CreatureConstants.Dragon_Brass_Young,
            CreatureConstants.Dragon_Brass_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Bronze,
            CreatureConstants.Dragon_Bronze_Adult,
            CreatureConstants.Dragon_Bronze_Ancient,
            CreatureConstants.Dragon_Bronze_GreatWyrm,
            CreatureConstants.Dragon_Bronze_Juvenile,
            CreatureConstants.Dragon_Bronze_MatureAdult,
            CreatureConstants.Dragon_Bronze_Old,
            CreatureConstants.Dragon_Bronze_VeryOld,
            CreatureConstants.Dragon_Bronze_VeryYoung,
            CreatureConstants.Dragon_Bronze_Wyrm,
            CreatureConstants.Dragon_Bronze_Wyrmling,
            CreatureConstants.Dragon_Bronze_Young,
            CreatureConstants.Dragon_Bronze_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Copper,
            CreatureConstants.Dragon_Copper_Adult,
            CreatureConstants.Dragon_Copper_Ancient,
            CreatureConstants.Dragon_Copper_GreatWyrm,
            CreatureConstants.Dragon_Copper_Juvenile,
            CreatureConstants.Dragon_Copper_MatureAdult,
            CreatureConstants.Dragon_Copper_Old,
            CreatureConstants.Dragon_Copper_VeryOld,
            CreatureConstants.Dragon_Copper_VeryYoung,
            CreatureConstants.Dragon_Copper_Wyrm,
            CreatureConstants.Dragon_Copper_Wyrmling,
            CreatureConstants.Dragon_Copper_Young,
            CreatureConstants.Dragon_Copper_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Gold,
            CreatureConstants.Dragon_Gold_Adult,
            CreatureConstants.Dragon_Gold_Ancient,
            CreatureConstants.Dragon_Gold_GreatWyrm,
            CreatureConstants.Dragon_Gold_Juvenile,
            CreatureConstants.Dragon_Gold_MatureAdult,
            CreatureConstants.Dragon_Gold_Old,
            CreatureConstants.Dragon_Gold_VeryOld,
            CreatureConstants.Dragon_Gold_VeryYoung,
            CreatureConstants.Dragon_Gold_Wyrm,
            CreatureConstants.Dragon_Gold_Wyrmling,
            CreatureConstants.Dragon_Gold_Young,
            CreatureConstants.Dragon_Gold_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Green,
            CreatureConstants.Dragon_Green_Adult,
            CreatureConstants.Dragon_Green_Ancient,
            CreatureConstants.Dragon_Green_GreatWyrm,
            CreatureConstants.Dragon_Green_Juvenile,
            CreatureConstants.Dragon_Green_MatureAdult,
            CreatureConstants.Dragon_Green_Old,
            CreatureConstants.Dragon_Green_VeryOld,
            CreatureConstants.Dragon_Green_VeryYoung,
            CreatureConstants.Dragon_Green_Wyrm,
            CreatureConstants.Dragon_Green_Wyrmling,
            CreatureConstants.Dragon_Green_Young,
            CreatureConstants.Dragon_Green_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Red,
            CreatureConstants.Dragon_Red_Adult,
            CreatureConstants.Dragon_Red_Ancient,
            CreatureConstants.Dragon_Red_GreatWyrm,
            CreatureConstants.Dragon_Red_Juvenile,
            CreatureConstants.Dragon_Red_MatureAdult,
            CreatureConstants.Dragon_Red_Old,
            CreatureConstants.Dragon_Red_VeryOld,
            CreatureConstants.Dragon_Red_VeryYoung,
            CreatureConstants.Dragon_Red_Wyrm,
            CreatureConstants.Dragon_Red_Wyrmling,
            CreatureConstants.Dragon_Red_Young,
            CreatureConstants.Dragon_Red_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Silver,
            CreatureConstants.Dragon_Silver_Adult,
            CreatureConstants.Dragon_Silver_Ancient,
            CreatureConstants.Dragon_Silver_GreatWyrm,
            CreatureConstants.Dragon_Silver_Juvenile,
            CreatureConstants.Dragon_Silver_MatureAdult,
            CreatureConstants.Dragon_Silver_Old,
            CreatureConstants.Dragon_Silver_VeryOld,
            CreatureConstants.Dragon_Silver_VeryYoung,
            CreatureConstants.Dragon_Silver_Wyrm,
            CreatureConstants.Dragon_Silver_Wyrmling,
            CreatureConstants.Dragon_Silver_Young,
            CreatureConstants.Dragon_Silver_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_White,
            CreatureConstants.Dragon_White_Adult,
            CreatureConstants.Dragon_White_Ancient,
            CreatureConstants.Dragon_White_GreatWyrm,
            CreatureConstants.Dragon_White_Juvenile,
            CreatureConstants.Dragon_White_MatureAdult,
            CreatureConstants.Dragon_White_Old,
            CreatureConstants.Dragon_White_VeryOld,
            CreatureConstants.Dragon_White_VeryYoung,
            CreatureConstants.Dragon_White_Wyrm,
            CreatureConstants.Dragon_White_Wyrmling,
            CreatureConstants.Dragon_White_Young,
            CreatureConstants.Dragon_White_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dwarf,
            CreatureConstants.Dwarf_Deep,
            CreatureConstants.Dwarf_Hill,
            CreatureConstants.Dwarf_Mountain)]
        [TestCase(CreatureConstants.Groups.Elemental_Air,
            CreatureConstants.Elemental_Air_Elder,
            CreatureConstants.Elemental_Air_Greater,
            CreatureConstants.Elemental_Air_Huge,
            CreatureConstants.Elemental_Air_Large,
            CreatureConstants.Elemental_Air_Medium,
            CreatureConstants.Elemental_Air_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Earth,
            CreatureConstants.Elemental_Earth_Elder,
            CreatureConstants.Elemental_Earth_Greater,
            CreatureConstants.Elemental_Earth_Huge,
            CreatureConstants.Elemental_Earth_Large,
            CreatureConstants.Elemental_Earth_Medium,
            CreatureConstants.Elemental_Earth_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Fire,
            CreatureConstants.Elemental_Fire_Elder,
            CreatureConstants.Elemental_Fire_Greater,
            CreatureConstants.Elemental_Fire_Huge,
            CreatureConstants.Elemental_Fire_Large,
            CreatureConstants.Elemental_Fire_Medium,
            CreatureConstants.Elemental_Fire_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Water,
            CreatureConstants.Elemental_Water_Elder,
            CreatureConstants.Elemental_Water_Greater,
            CreatureConstants.Elemental_Water_Huge,
            CreatureConstants.Elemental_Water_Large,
            CreatureConstants.Elemental_Water_Medium,
            CreatureConstants.Elemental_Water_Small)]
        [TestCase(CreatureConstants.Groups.Elf,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Elf_Drow,
            CreatureConstants.Elf_Gray,
            CreatureConstants.Elf_Half,
            CreatureConstants.Elf_High,
            CreatureConstants.Elf_Wild,
            CreatureConstants.Elf_Wood)]
        [TestCase(CreatureConstants.Groups.Formian,
            CreatureConstants.FormianMyrmarch,
            CreatureConstants.FormianQueen,
            CreatureConstants.FormianTaskmaster,
            CreatureConstants.FormianWarrior,
            CreatureConstants.FormianWorker)]
        [TestCase(CreatureConstants.Groups.Fungus,
            CreatureConstants.Shrieker,
            CreatureConstants.VioletFungus)]
        [TestCase(CreatureConstants.Groups.Genie,
            CreatureConstants.Djinni,
            CreatureConstants.Djinni_Noble,
            CreatureConstants.Efreeti,
            CreatureConstants.Janni)]
        [TestCase(CreatureConstants.Groups.Gnome,
            CreatureConstants.Gnome_Forest,
            CreatureConstants.Gnome_Rock,
            CreatureConstants.Gnome_Svirfneblin)]
        [TestCase(CreatureConstants.Groups.Golem,
            CreatureConstants.Golem_Clay,
            CreatureConstants.Golem_Flesh,
            CreatureConstants.Golem_Iron,
            CreatureConstants.Golem_Stone,
            CreatureConstants.Golem_Stone_Greater)]
        [TestCase(CreatureConstants.Groups.Hag,
            CreatureConstants.Annis,
            CreatureConstants.GreenHag,
            CreatureConstants.SeaHag)]
        [TestCase(CreatureConstants.Groups.Halfling,
            CreatureConstants.Halfling_Deep,
            CreatureConstants.Halfling_Lightfoot,
            CreatureConstants.Halfling_Tallfellow)]
        [TestCase(CreatureConstants.Groups.Horse,
            CreatureConstants.Horse_Heavy,
            CreatureConstants.Horse_Heavy_War,
            CreatureConstants.Horse_Light,
            CreatureConstants.Horse_Light_War)]
        [TestCase(CreatureConstants.Groups.Hydra,
            CreatureConstants.Groups.Cryohydra,
            CreatureConstants.Groups.Pyrohydra,
            CreatureConstants.Hydra_10Heads,
            CreatureConstants.Hydra_11Heads,
            CreatureConstants.Hydra_12Heads,
            CreatureConstants.Hydra_5Heads,
            CreatureConstants.Hydra_6Heads,
            CreatureConstants.Hydra_7Heads,
            CreatureConstants.Hydra_8Heads,
            CreatureConstants.Hydra_9Heads)]
        [TestCase(CreatureConstants.Groups.Inevitable,
            CreatureConstants.Kolyarut,
            CreatureConstants.Marut,
            CreatureConstants.Zelekhut)]
        [TestCase(CreatureConstants.Groups.Lycanthrope,
            CreatureConstants.Werebear,
            CreatureConstants.Wereboar,
            CreatureConstants.Wererat,
            CreatureConstants.Weretiger,
            CreatureConstants.Werewolf)]
        [TestCase(CreatureConstants.Groups.Mephit,
            CreatureConstants.Mephit_Air,
            CreatureConstants.Mephit_Dust,
            CreatureConstants.Mephit_Earth,
            CreatureConstants.Mephit_Fire,
            CreatureConstants.Mephit_Ice,
            CreatureConstants.Mephit_Magma,
            CreatureConstants.Mephit_Ooze,
            CreatureConstants.Mephit_Salt,
            CreatureConstants.Mephit_Steam,
            CreatureConstants.Mephit_Water)]
        [TestCase(CreatureConstants.Groups.Naga,
            CreatureConstants.Naga_Dark,
            CreatureConstants.Naga_Guardian,
            CreatureConstants.Naga_Spirit,
            CreatureConstants.Naga_Water)]
        [TestCase(CreatureConstants.Groups.Nightshade,
            CreatureConstants.Nightcrawler,
            CreatureConstants.Nightwalker,
            CreatureConstants.Nightwing)]
        [TestCase(CreatureConstants.Groups.Pyrohydra,
            CreatureConstants.Pyrohydra_10Heads,
            CreatureConstants.Pyrohydra_11Heads,
            CreatureConstants.Pyrohydra_12Heads,
            CreatureConstants.Pyrohydra_5Heads,
            CreatureConstants.Pyrohydra_6Heads,
            CreatureConstants.Pyrohydra_7Heads,
            CreatureConstants.Pyrohydra_8Heads,
            CreatureConstants.Pyrohydra_9Heads)]
        [TestCase(CreatureConstants.Groups.Salamander,
            CreatureConstants.Salamander_Average,
            CreatureConstants.Salamander_Flamebrother,
            CreatureConstants.Salamander_Noble)]
        [TestCase(CreatureConstants.Groups.Scorpion_Monstrous,
            CreatureConstants.Scorpion_Monstrous_Colossal,
            CreatureConstants.Scorpion_Monstrous_Gargantuan,
            CreatureConstants.Scorpion_Monstrous_Huge,
            CreatureConstants.Scorpion_Monstrous_Large,
            CreatureConstants.Scorpion_Monstrous_Medium,
            CreatureConstants.Scorpion_Monstrous_Small,
            CreatureConstants.Scorpion_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Groups.Shark,
            CreatureConstants.Shark_Dire,
            CreatureConstants.Shark_Medium,
            CreatureConstants.Shark_Large,
            CreatureConstants.Shark_Huge)]
        [TestCase(CreatureConstants.Groups.Slaad,
            CreatureConstants.Slaad_Blue,
            CreatureConstants.Slaad_Death,
            CreatureConstants.Slaad_Gray,
            CreatureConstants.Slaad_Green,
            CreatureConstants.Slaad_Red)]
        [TestCase(CreatureConstants.Groups.Snake_Viper,
            CreatureConstants.Snake_Viper_Huge,
            CreatureConstants.Snake_Viper_Large,
            CreatureConstants.Snake_Viper_Medium,
            CreatureConstants.Snake_Viper_Small,
            CreatureConstants.Snake_Viper_Tiny)]
        [TestCase(CreatureConstants.Groups.Sphinx,
            CreatureConstants.Androsphinx,
            CreatureConstants.Criosphinx,
            CreatureConstants.Gynosphinx,
            CreatureConstants.Hieracosphinx)]
        [TestCase(CreatureConstants.Groups.Spider_Monstrous,
            CreatureConstants.Spider_Monstrous_Colossal,
            CreatureConstants.Spider_Monstrous_Gargantuan,
            CreatureConstants.Spider_Monstrous_Huge,
            CreatureConstants.Spider_Monstrous_Large,
            CreatureConstants.Spider_Monstrous_Medium,
            CreatureConstants.Spider_Monstrous_Small,
            CreatureConstants.Spider_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Groups.Sprite,
            CreatureConstants.Grig,
            CreatureConstants.Pixie,
            CreatureConstants.Pixie_WithIrresistableDance,
            CreatureConstants.Nixie)]
        [TestCase(CreatureConstants.Groups.Tojanida,
            CreatureConstants.Tojanida_Juvenile,
            CreatureConstants.Tojanida_Adult,
            CreatureConstants.Tojanida_Elder)]
        [TestCase(CreatureConstants.Groups.Whale,
            CreatureConstants.Whale_Baleen,
            CreatureConstants.Whale_Cachalot,
            CreatureConstants.Whale_Orca)]
        [TestCase(CreatureConstants.Groups.Xorn,
            CreatureConstants.Xorn_Average,
            CreatureConstants.Xorn_Elder,
            CreatureConstants.Xorn_Minor)]
        [TestCase(CreatureConstants.Groups.YuanTi,
            CreatureConstants.YuanTi_Abomination,
            CreatureConstants.YuanTi_Halfblood,
            CreatureConstants.YuanTi_Pureblood)]
        [TestCase(CreatureConstants.Templates.Ghost,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Templates.HalfDragon,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Templates.Lich,
            CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Templates.None)]
        [TestCase(CreatureConstants.Templates.Skeleton,
            CreatureConstants.Groups.HasSkeleton)]
        [TestCase(CreatureConstants.Templates.Vampire,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Templates.Werebear,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Wereboar,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Wererat,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Weretiger,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Werewolf,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Zombie,
            CreatureConstants.Groups.HasSkeleton)]
        public void CreatureSubgroup(string creature, params string[] subgroup)
        {
            base.DistinctCollection(creature, subgroup);
        }

        [Test]
        public void CelestialCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var alignments = new List<string>();
            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            };

            foreach (var alignmentGroup in alignmentGroups)
            {
                var explodedAlignments = CollectionSelector.Explode(TableNameConstants.Set.Collection.AlignmentGroups, alignmentGroup);
                alignments.AddRange(explodedAlignments);
            }

            var typeCreatures = new List<string>();

            foreach (var creatureType in types)
            {
                var explodedType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);
                typeCreatures.AddRange(explodedType);
            }

            var alignmentCreatures = new List<string>();

            foreach (var creature in typeCreatures)
            {
                var creatureAlignments = CollectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, creature);
                if (!creatureAlignments.Any())
                    creatureAlignments = new[] { AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral };

                if (creatureAlignments.Any(ca => alignments.Any(a => ca.Contains(a))))
                    alignmentCreatures.Add(creature);
            }

            DistinctCollection(CreatureConstants.Templates.CelestialCreature, alignmentCreatures.ToArray());

            Assert.Fail("Verify all creatures are corporeal");
        }

        [Test]
        public void HalfCelestialCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var alignments = new List<string>();
            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            };

            foreach (var alignmentGroup in alignmentGroups)
            {
                var explodedAlignments = CollectionSelector.Explode(TableNameConstants.Set.Collection.AlignmentGroups, alignmentGroup);
                alignments.AddRange(explodedAlignments);
            }

            var typeCreatures = new List<string>();

            foreach (var creatureType in types)
            {
                var explodedType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);
                typeCreatures.AddRange(explodedType);
            }

            var alignmentCreatures = new List<string>();

            foreach (var creature in typeCreatures)
            {
                var creatureAlignments = CollectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, creature);
                if (!creatureAlignments.Any())
                    creatureAlignments = new[] { AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral };

                if (creatureAlignments.Any(ca => alignments.Any(a => ca.Contains(a))))
                    alignmentCreatures.Add(creature);
            }

            DistinctCollection(CreatureConstants.Templates.HalfCelestial, alignmentCreatures.ToArray());

            Assert.Fail("Verify all creatures are corporeal");
        }

        [Test]
        public void HalfDragonCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var typeCreatures = new List<string>();

            foreach (var creatureType in types)
            {
                var explodedType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);
                typeCreatures.AddRange(explodedType);
            }

            DistinctCollection(CreatureConstants.Templates.HalfDragon, typeCreatures.ToArray());

            Assert.Fail("Verify all creatures are corporeal");
        }

        [Test]
        public void FiendishCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var alignments = new List<string>();
            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            };

            foreach (var alignmentGroup in alignmentGroups)
            {
                var explodedAlignments = CollectionSelector.Explode(TableNameConstants.Set.Collection.AlignmentGroups, alignmentGroup);
                alignments.AddRange(explodedAlignments);
            }

            var typeCreatures = new List<string>();

            foreach (var creatureType in types)
            {
                var explodedType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);
                typeCreatures.AddRange(explodedType);
            }

            var alignmentCreatures = new List<string>();

            foreach (var creature in typeCreatures)
            {
                var creatureAlignments = CollectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, creature);
                if (!creatureAlignments.Any())
                    creatureAlignments = new[] { AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral };

                if (creatureAlignments.Any(ca => alignments.Any(a => ca.Contains(a))))
                    alignmentCreatures.Add(creature);
            }

            DistinctCollection(CreatureConstants.Templates.FiendishCreature, alignmentCreatures.ToArray());

            Assert.Fail("Verify all creatures are corporeal");
        }

        [Test]
        public void HalfFiendCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var alignments = new List<string>();
            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            };

            foreach (var alignmentGroup in alignmentGroups)
            {
                var explodedAlignments = CollectionSelector.Explode(TableNameConstants.Set.Collection.AlignmentGroups, alignmentGroup);
                alignments.AddRange(explodedAlignments);
            }

            var typeCreatures = new List<string>();

            foreach (var creatureType in types)
            {
                var explodedType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);
                typeCreatures.AddRange(explodedType);
            }

            var alignmentCreatures = new List<string>();

            foreach (var creature in typeCreatures)
            {
                var creatureAlignments = CollectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, creature);
                if (!creatureAlignments.Any())
                    creatureAlignments = new[] { AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral };

                if (creatureAlignments.Any(ca => alignments.Any(a => ca.Contains(a))))
                    alignmentCreatures.Add(creature);
            }

            DistinctCollection(CreatureConstants.Templates.HalfFiend, alignmentCreatures.ToArray());

            Assert.Fail("Verify all creatures are corporeal");
        }

        [Test]
        public void HasSkeletonGroup()
        {
            var entries = new[]
            {
                //Aberration
                CreatureConstants.Aboleth,
                CreatureConstants.Aboleth_Mage,
                CreatureConstants.Athach,
                CreatureConstants.Beholder,
                CreatureConstants.CarrionCrawler,
                CreatureConstants.Choker,
                CreatureConstants.Chuul,
                CreatureConstants.Cloaker,
                CreatureConstants.Delver,
                CreatureConstants.Destrachan,
                CreatureConstants.Drider,
                CreatureConstants.EtherealFilcher,
                CreatureConstants.Ettercap,
                CreatureConstants.GibberingMouther,
                CreatureConstants.Grick,
                CreatureConstants.MindFlayer,
                CreatureConstants.Groups.Naga,
                CreatureConstants.Otyugh,
                CreatureConstants.RustMonster,
                CreatureConstants.Skum,
                CreatureConstants.UmberHulk,
                
                //Animal
                CreatureConstants.Ape,
                CreatureConstants.Ape_Dire,
                CreatureConstants.Baboon,
                CreatureConstants.Badger,
                CreatureConstants.Badger_Dire,
                CreatureConstants.Bat,
                CreatureConstants.Bat_Dire,
                CreatureConstants.Groups.Bear,
                CreatureConstants.Bison,
                CreatureConstants.Boar,
                CreatureConstants.Boar_Dire,
                CreatureConstants.Camel,
                CreatureConstants.Cat,
                CreatureConstants.Cheetah,
                CreatureConstants.Crocodile,
                CreatureConstants.Crocodile_Giant,
                CreatureConstants.Groups.Dinosaur,
                CreatureConstants.Dog,
                CreatureConstants.Donkey,
                CreatureConstants.Eagle,
                CreatureConstants.Elephant,
                CreatureConstants.Hawk,
                CreatureConstants.Groups.Horse,
                CreatureConstants.Hyena,
                CreatureConstants.Leopard,
                CreatureConstants.Lion,
                CreatureConstants.Lion_Dire,
                CreatureConstants.Lizard,
                CreatureConstants.Lizard_Monitor,
                CreatureConstants.MantaRay,
                CreatureConstants.Monkey,
                CreatureConstants.Mule,
                CreatureConstants.Owl,
                CreatureConstants.Pony,
                CreatureConstants.Porpoise,
                CreatureConstants.Rat,
                CreatureConstants.Rat_Dire,
                CreatureConstants.Raven,
                CreatureConstants.Rhinoceras,
                CreatureConstants.Roc,
                CreatureConstants.Snake_Constrictor,
                CreatureConstants.Snake_Constrictor_Giant,
                CreatureConstants.Groups.Snake_Viper,
                CreatureConstants.Groups.Shark,
                CreatureConstants.Tiger,
                CreatureConstants.Tiger_Dire,
                CreatureConstants.Toad,
                CreatureConstants.Weasel,
                CreatureConstants.Weasel_Dire,
                CreatureConstants.Groups.Whale,
                CreatureConstants.Wolf,
                CreatureConstants.Wolf_Dire,
                CreatureConstants.Wolverine,
                CreatureConstants.Wolverine_Dire,

                //Magical Beasts
                CreatureConstants.Ankheg,
                CreatureConstants.Aranea,
                CreatureConstants.Basilisk,
                CreatureConstants.Behir,
                CreatureConstants.BlinkDog,
                CreatureConstants.Bulette,
                CreatureConstants.Chimera,
                CreatureConstants.Cockatrice,
                CreatureConstants.Darkmantle,
                CreatureConstants.Digester,
                CreatureConstants.DisplacerBeast,
                CreatureConstants.DisplacerBeast_PackLord,
                CreatureConstants.Dragonne,
                CreatureConstants.Eagle_Giant,
                CreatureConstants.EtherealMarauder,
                CreatureConstants.Girallon,
                CreatureConstants.Gorgon,
                CreatureConstants.GrayRender,
                CreatureConstants.Griffon,
                CreatureConstants.Hippogriff,
                CreatureConstants.Groups.Hydra,
                CreatureConstants.Krenshar,
                CreatureConstants.Lamia,
                CreatureConstants.Lammasu,
                CreatureConstants.Manticore,
                CreatureConstants.Owl_Giant,
                CreatureConstants.Owlbear,
                CreatureConstants.Pegasus,
                CreatureConstants.RazorBoar,
                CreatureConstants.SeaCat,
                CreatureConstants.ShockerLizard,
                CreatureConstants.Androsphinx,
                CreatureConstants.Criosphinx,
                CreatureConstants.Gynosphinx,
                CreatureConstants.Hieracosphinx,
                CreatureConstants.Tarrasque,
                CreatureConstants.Unicorn,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Yrthak,

                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            };

            DistinctCollection(CreatureConstants.Groups.HasSkeleton, entries);
        }

        [Test]
        public void NoCircularSubgroups()
        {
            foreach (var kvp in table)
            {
                AssertGroupDoesNotContain(kvp.Key, kvp.Key);
            }
        }

        private void AssertGroupDoesNotContain(string name, string forbiddenEntry)
        {
            var group = table[name];

            if (name != forbiddenEntry)
                Assert.That(group, Does.Not.Contain(forbiddenEntry));

            var subgroupNames = group.Intersect(table.Keys);

            foreach (var subgroupName in subgroupNames)
            {
                AssertGroupDoesNotContain(subgroupName, forbiddenEntry);
                AssertGroupDoesNotContain(subgroupName, subgroupName);
            }
        }
    }
}
