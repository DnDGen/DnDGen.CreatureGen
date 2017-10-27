using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class EvocationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Evocation);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DancingLights,
                SpellConstants.Flare,
                SpellConstants.Light,
                SpellConstants.RayOfFrost,
                SpellConstants.BurningHands,
                SpellConstants.FloatingDisk,
                SpellConstants.MagicMissile,
                SpellConstants.ShockingGrasp,
                SpellConstants.ContinualFlame,
                SpellConstants.Darkness,
                SpellConstants.FlamingSphere,
                SpellConstants.GustOfWind,
                SpellConstants.ScorchingRay,
                SpellConstants.Shatter,
                SpellConstants.Daylight,
                SpellConstants.Fireball,
                SpellConstants.LightningBolt,
                SpellConstants.TinyHut,
                SpellConstants.WindWall,
                SpellConstants.FireShield,
                SpellConstants.IceStorm,
                SpellConstants.ResilientSphere,
                SpellConstants.Shout,
                SpellConstants.WallOfFire,
                SpellConstants.WallOfIce,
                SpellConstants.ChainLightning,
                SpellConstants.Contingency,
                SpellConstants.ForcefulHand,
                SpellConstants.FreezingSphere,
                SpellConstants.DelayedBlastFireball,
                SpellConstants.Forcecage,
                SpellConstants.GraspingHand,
                SpellConstants.MagesSword,
                SpellConstants.PrismaticSpray,
                SpellConstants.ClenchedFist,
                SpellConstants.PolarRay,
                SpellConstants.Shout_Greater,
                SpellConstants.Sunburst,
                SpellConstants.TelekineticSphere,
                SpellConstants.CrushingHand,
                SpellConstants.ConeOfCold,
                SpellConstants.InterposingHand,
                SpellConstants.Sending,
                SpellConstants.WallOfForce,
                SpellConstants.MeteorSwarm
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEvocationSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Evocation]);
        }

        [TestCase(SpellConstants.DancingLights, 0)]
        [TestCase(SpellConstants.Flare, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.RayOfFrost, 0)]
        [TestCase(SpellConstants.BurningHands, 1)]
        [TestCase(SpellConstants.FloatingDisk, 1)]
        [TestCase(SpellConstants.MagicMissile, 1)]
        [TestCase(SpellConstants.ShockingGrasp, 1)]
        [TestCase(SpellConstants.ContinualFlame, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.FlamingSphere, 2)]
        [TestCase(SpellConstants.GustOfWind, 2)]
        [TestCase(SpellConstants.ScorchingRay, 2)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.Fireball, 3)]
        [TestCase(SpellConstants.LightningBolt, 3)]
        [TestCase(SpellConstants.TinyHut, 3)]
        [TestCase(SpellConstants.WindWall, 3)]
        [TestCase(SpellConstants.FireShield, 4)]
        [TestCase(SpellConstants.IceStorm, 4)]
        [TestCase(SpellConstants.ResilientSphere, 4)]
        [TestCase(SpellConstants.Shout, 4)]
        [TestCase(SpellConstants.WallOfFire, 4)]
        [TestCase(SpellConstants.WallOfIce, 4)]
        [TestCase(SpellConstants.ConeOfCold, 5)]
        [TestCase(SpellConstants.InterposingHand, 5)]
        [TestCase(SpellConstants.Sending, 5)]
        [TestCase(SpellConstants.WallOfForce, 5)]
        [TestCase(SpellConstants.ChainLightning, 6)]
        [TestCase(SpellConstants.Contingency, 6)]
        [TestCase(SpellConstants.ForcefulHand, 6)]
        [TestCase(SpellConstants.FreezingSphere, 6)]
        [TestCase(SpellConstants.DelayedBlastFireball, 7)]
        [TestCase(SpellConstants.Forcecage, 7)]
        [TestCase(SpellConstants.GraspingHand, 7)]
        [TestCase(SpellConstants.MagesSword, 7)]
        [TestCase(SpellConstants.PrismaticSpray, 7)]
        [TestCase(SpellConstants.ClenchedFist, 8)]
        [TestCase(SpellConstants.PolarRay, 8)]
        [TestCase(SpellConstants.Shout_Greater, 8)]
        [TestCase(SpellConstants.Sunburst, 8)]
        [TestCase(SpellConstants.TelekineticSphere, 8)]
        [TestCase(SpellConstants.CrushingHand, 9)]
        [TestCase(SpellConstants.MeteorSwarm, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
