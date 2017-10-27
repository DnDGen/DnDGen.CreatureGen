using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class ConjurationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Conjuration);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.AcidSplash,
                SpellConstants.Grease,
                SpellConstants.MageArmor,
                SpellConstants.Mount,
                SpellConstants.ObscuringMist,
                SpellConstants.SummonMonsterI,
                SpellConstants.UnseenServant,
                SpellConstants.AcidArrow,
                SpellConstants.FogCloud,
                SpellConstants.Glitterdust,
                SpellConstants.SummonMonsterII,
                SpellConstants.SummonSwarm,
                SpellConstants.Web,
                SpellConstants.PhantomSteed,
                SpellConstants.SepiaSnakeSigil,
                SpellConstants.SleetStorm,
                SpellConstants.StinkingCloud,
                SpellConstants.SummonMonsterIII,
                SpellConstants.BlackTentacles,
                SpellConstants.DimensionDoor,
                SpellConstants.MinorCreation,
                SpellConstants.SecureShelter,
                SpellConstants.SolidFog,
                SpellConstants.SummonMonsterIV,
                SpellConstants.Cloudkill,
                SpellConstants.MagesFaithfulHound,
                SpellConstants.MajorCreation,
                SpellConstants.PlanarBinding_Lesser,
                SpellConstants.SecretChest,
                SpellConstants.SummonMonsterV,
                SpellConstants.Teleport,
                SpellConstants.WallOfStone,
                SpellConstants.AcidFog,
                SpellConstants.PlanarBinding,
                SpellConstants.SummonMonsterVI,
                SpellConstants.WallOfIron,
                SpellConstants.InstantSummons,
                SpellConstants.MagesMagnificentMansion,
                SpellConstants.PhaseDoor,
                SpellConstants.PlaneShift,
                SpellConstants.SummonMonsterVII,
                SpellConstants.Teleport_Greater,
                SpellConstants.TeleportObject,
                SpellConstants.IncendiaryCloud,
                SpellConstants.Maze,
                SpellConstants.PlanarBinding_Greater,
                SpellConstants.SummonMonsterVIII,
                SpellConstants.TrapTheSoul,
                SpellConstants.Gate,
                SpellConstants.Refuge,
                SpellConstants.SummonMonsterIX,
                SpellConstants.TeleportationCircle
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllConjurationSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Conjuration]);
        }

        [TestCase(SpellConstants.AcidSplash, 0)]
        [TestCase(SpellConstants.Grease, 1)]
        [TestCase(SpellConstants.MageArmor, 1)]
        [TestCase(SpellConstants.Mount, 1)]
        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.SummonMonsterI, 1)]
        [TestCase(SpellConstants.UnseenServant, 1)]
        [TestCase(SpellConstants.AcidArrow, 2)]
        [TestCase(SpellConstants.FogCloud, 2)]
        [TestCase(SpellConstants.Glitterdust, 2)]
        [TestCase(SpellConstants.SummonMonsterII, 2)]
        [TestCase(SpellConstants.SummonSwarm, 2)]
        [TestCase(SpellConstants.Web, 2)]
        [TestCase(SpellConstants.PhantomSteed, 3)]
        [TestCase(SpellConstants.SepiaSnakeSigil, 3)]
        [TestCase(SpellConstants.SleetStorm, 3)]
        [TestCase(SpellConstants.StinkingCloud, 3)]
        [TestCase(SpellConstants.SummonMonsterIII, 3)]
        [TestCase(SpellConstants.BlackTentacles, 4)]
        [TestCase(SpellConstants.DimensionDoor, 4)]
        [TestCase(SpellConstants.MinorCreation, 4)]
        [TestCase(SpellConstants.SecureShelter, 4)]
        [TestCase(SpellConstants.SolidFog, 4)]
        [TestCase(SpellConstants.SummonMonsterIV, 4)]
        [TestCase(SpellConstants.Cloudkill, 5)]
        [TestCase(SpellConstants.MagesFaithfulHound, 5)]
        [TestCase(SpellConstants.MajorCreation, 5)]
        [TestCase(SpellConstants.PlanarBinding_Lesser, 5)]
        [TestCase(SpellConstants.SecretChest, 5)]
        [TestCase(SpellConstants.SummonMonsterV, 5)]
        [TestCase(SpellConstants.Teleport, 5)]
        [TestCase(SpellConstants.WallOfStone, 5)]
        [TestCase(SpellConstants.AcidFog, 6)]
        [TestCase(SpellConstants.PlanarBinding, 6)]
        [TestCase(SpellConstants.SummonMonsterVI, 6)]
        [TestCase(SpellConstants.WallOfIron, 6)]
        [TestCase(SpellConstants.InstantSummons, 7)]
        [TestCase(SpellConstants.MagesMagnificentMansion, 7)]
        [TestCase(SpellConstants.PhaseDoor, 7)]
        [TestCase(SpellConstants.PlaneShift, 7)]
        [TestCase(SpellConstants.SummonMonsterVII, 7)]
        [TestCase(SpellConstants.Teleport_Greater, 7)]
        [TestCase(SpellConstants.TeleportObject, 7)]
        [TestCase(SpellConstants.IncendiaryCloud, 8)]
        [TestCase(SpellConstants.Maze, 8)]
        [TestCase(SpellConstants.PlanarBinding_Greater, 8)]
        [TestCase(SpellConstants.SummonMonsterVIII, 8)]
        [TestCase(SpellConstants.TrapTheSoul, 8)]
        [TestCase(SpellConstants.Gate, 9)]
        [TestCase(SpellConstants.Refuge, 9)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        [TestCase(SpellConstants.TeleportationCircle, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
