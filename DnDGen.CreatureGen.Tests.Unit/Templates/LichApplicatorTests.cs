using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class LichApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            applicator = new LichApplicator();
        }

        [TestCase(CreatureConstants.Types.Aberration, false)]
        [TestCase(CreatureConstants.Types.Animal, false)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, false)]
        [TestCase(CreatureConstants.Types.Elemental, false)]
        [TestCase(CreatureConstants.Types.Fey, false)]
        [TestCase(CreatureConstants.Types.Giant, false)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, false)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, false)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, false)]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void IsCompatible_HasCasterLevel(int casterLevel)
        {
            var creatureData = new CreatureDataSelection { CasterLevel = casterLevel, LevelAdjustment = null };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(casterLevel >= 11));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void IsCompatible_CanBeCharacter(int levelAdjustment)
        {
            var creatureData = new CreatureDataSelection { CasterLevel = 0, LevelAdjustment = levelAdjustment };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void ApplyTo_GainsCommon()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainsCommon_AlreadyKnows()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_CreatureTypeChangesToUndead()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_HitDiceChangeToD12AndReroll()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainsNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ImprovesNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_UsesExistingNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainAttacks()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainSpecialQualities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ModifyAbilities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainsSkillBonuses()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_SwapConsitutionForCharismaForConcentration()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ImproveChallengeRating()
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
        public void ApplyTo_ImproveLevelAdjustment(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_Tests()
        {
            Assert.Fail("need to copy");
        }
    }
}
