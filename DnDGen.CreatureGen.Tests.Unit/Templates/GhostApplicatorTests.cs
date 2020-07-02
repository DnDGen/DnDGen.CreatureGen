using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Templates;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class GhostApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;

        [SetUp]
        public void SetUp()
        {
            applicator = new GhostApplicator();

            baseCreature = new CreatureBuilder().WithTestValues().Build();
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Undead)]
        public void CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Incorporeal));
        }

        [Test]
        public void CharismaIncreasesBy4()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CharismaLessThan6_AdjustedUpTo6_ThenIncreased()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ConstitutionGoesAway()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void HitDiceChangeToD12_AndRerolled()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void HitDiceChangeToD12_AndRerolled_WithoutConstitution()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsFlySpeed()
        {
            //30, perfect maneuverability
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsFlySpeed_BetterThanOriginalFlySpeed()
        {
            //30, perfect maneuverability - replace
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsFlySpeed_SlowerThanOriginalFlySpeed()
        {
            //30, perfect maneuverability - keep higher speed
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsFlySpeed_OriginalFlySpeedLessManeuverable()
        {
            //30, perfect maneuverability - keep higher speed
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureArmorClass_DeflectionBonusOfCharisma()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureArmorClass_DeflectionBonusOfCharisma_AtLeast1()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsSpecialAttacks_RandomAmount()
        {
            //one to three
            //use attack generator to get all
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsSpecialAttacks_AlwaysGetsManifestation()
        {
            //one to three
            //use attack generator to get all
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureGainsSpecialQualities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureSkills_GainRacialBonuses()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureChallengeRating_IncreasesBy2()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(null, null)]
        [TestCase(0, 5)]
        [TestCase(1, 6)]
        [TestCase(2, 7)]
        [TestCase(10, 15)]
        [TestCase(20, 25)]
        [TestCase(42, 47)]
        public void CreatureLevelAdjustment_Increases(int? original, int? adjusted)
        {
            Assert.Fail("not yet written");
        }
    }
}
