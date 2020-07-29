using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class SkeletonApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();

            applicator = new SkeletonApplicator();

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .Build();
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, true)]
        [TestCase(CreatureConstants.Types.Elemental, true)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_ByCreatureType(string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void IsCompatible_MustHaveSkeleton()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(0.1, true)]
        [TestCase(0.25, true)]
        [TestCase(0.5, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(10, true)]
        [TestCase(19, true)]
        [TestCase(20, true)]
        [TestCase(21, false)]
        [TestCase(22, false)]
        [TestCase(42, false)]
        public void IsCompatible_FewerThan20HitDice(double hitDice, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(hitDice);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_ChangeCreatureType(string original)
        {
            baseCreature.Type.Name = original;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_KeepSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes)
                .And.Contains(subtype)
                .And.Length.EqualTo(3));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public void ApplyTo_LoseSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes.Except(new[] { subtype }))
                .And.Not.Contains(subtype)
                .And.Length.EqualTo(2));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public void ApplyTo_HitDiceBecomeD12(int hitDie)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_LoseFlySpeed_Wings()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_KeepFlySpeed_Magic()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void ApplyTo_GainsNaturalArmorBasedOnSize(string size, int bonus)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void ApplyTo_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(.1, 0)]
        [TestCase(.25, 0)]
        [TestCase(.5, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 4)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 6)]
        [TestCase(14, 7)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(17, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 9)]
        [TestCase(20, 10)]
        public void ApplyTo_BaseAttackBonus(double hitDice, int attackBonus)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public void ApplyTo_ReplacesClawAttack_DamageBasedOnSize(string size, string damage)
        {
            //Claw is primary
            Assert.Fail("not yet written");
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
        public void ApplyTo_GainClawAttacks_PerNumberOfHands(int numberOfHands)
        {
            //Claw is primary
            Assert.Fail("not yet written");
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public void ApplyTo_ReplacesClawAttack_KeepOriginalClawDamage(string size, string damage)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_LoseSpecialAttacks()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_SetSavingThrows()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_SetAbilities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_LoseAllSkills()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_LoseAllFeats_GainImprovedInitiative()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(.1, ChallengeRatingConstants.OneSixth)]
        [TestCase(.25, ChallengeRatingConstants.OneSixth)]
        [TestCase(.5, ChallengeRatingConstants.OneSixth)]
        [TestCase(1, ChallengeRatingConstants.OneThird)]
        [TestCase(2, ChallengeRatingConstants.One)]
        [TestCase(3, ChallengeRatingConstants.One)]
        [TestCase(4, ChallengeRatingConstants.Two)]
        [TestCase(5, ChallengeRatingConstants.Two)]
        [TestCase(6, ChallengeRatingConstants.Three)]
        [TestCase(7, ChallengeRatingConstants.Three)]
        [TestCase(8, ChallengeRatingConstants.Four)]
        [TestCase(9, ChallengeRatingConstants.Four)]
        [TestCase(10, ChallengeRatingConstants.Five)]
        [TestCase(11, ChallengeRatingConstants.Five)]
        [TestCase(12, ChallengeRatingConstants.Six)]
        [TestCase(13, ChallengeRatingConstants.Six)]
        [TestCase(14, ChallengeRatingConstants.Six)]
        [TestCase(15, ChallengeRatingConstants.Seven)]
        [TestCase(16, ChallengeRatingConstants.Seven)]
        [TestCase(17, ChallengeRatingConstants.Seven)]
        [TestCase(18, ChallengeRatingConstants.Eight)]
        [TestCase(19, ChallengeRatingConstants.Eight)]
        [TestCase(20, ChallengeRatingConstants.Eight)]
        public void ApplyTo_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil)]
        public void ApplyTo_ChangeAlignment(string lawfulness, string goodness)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_SetNoLevelAdjustment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public async Task ApplyToAsync_()
        {
            Assert.Fail("need to copy");
        }
    }
}
