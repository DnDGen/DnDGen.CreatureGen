using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class CelestialCreatureApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<IAttacksGenerator> mockAttackGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;

        [SetUp]
        public void SetUp()
        {
            mockAttackGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();

            applicator = new CelestialCreatureApplicator(
                mockAttackGenerator.Object,
                mockFeatsGenerator.Object,
                mockCollectionSelector.Object,
                mockMagicGenerator.Object,
                mockAdjustmentSelector.Object,
                mockCreatureDataSelector.Object,
                mockPrototypeFactory.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();
        }

        [Test]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.CelestialCreature}");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralGood, "Alignment filter 'Neutral Good' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulGood, "CR filter 2 does not match updated creature CR 1 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.CelestialCreature}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            Assert.That(() => applicator.ApplyTo(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_ReturnsCreature()
        {
            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("other template");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1;
            filters.Alignment = AlignmentConstants.LawfulGood;

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Fey, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.MagicalBeast)]
        public void ApplyTo_CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            if (original == adjusted)
            {
                Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
                Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                    .And.Contains("subtype 2")
                    .And.Contains(CreatureConstants.Types.Subtypes.Extraplanar)
                    .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
            }
            else
            {
                Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
                Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                    .And.Contains("subtype 2")
                    .And.Contains(original)
                    .And.Contains(CreatureConstants.Types.Subtypes.Extraplanar)
                    .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
            }
        }

        [Test]
        public void ApplyTo_CreatureSizeIsNotAdjusted()
        {
            baseCreature.Size = "my size";

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Size, Is.EqualTo("my size"));
        }

        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        [TestCase(21, 20)]
        [TestCase(22, 20)]
        [TestCase(42, 20)]
        public void CreatureGainsSmiteEvilSpecialAttack(double hitDiceQuantity, int smiteDamage)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;

            var originalAttacks = baseCreature.Attacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();
            var originalSpecialAttacks = baseCreature.SpecialAttacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalAttacks.Length + 1));
            Assert.That(creature.Attacks.Select(a => a.Name), Is.SupersetOf(originalAttacks.Select(a => a.Name)));
            Assert.That(creature.Attacks, Contains.Item(smiteEvil));
            Assert.That(creature.SpecialAttacks.Count(), Is.EqualTo(originalSpecialAttacks.Length + 1));
            Assert.That(creature.SpecialAttacks, Contains.Item(smiteEvil));

            Assert.That(smiteEvil.DamageDescription, Is.EqualTo(smiteDamage.ToString()));
        }

        [Test]
        public void CreatureGainSpecialQualities()
        {
            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
        }

        [Test]
        public void IfCreatureHasWeakerSpellResistance_Replace()
        {
            var spellResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.SpellResistance,
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { spellResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[5] }))
                .And.Not.Contains(specialQualities[5])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(spellResistance.Power, Is.EqualTo(5));
        }

        [Test]
        public void IfCreatureHasStrongerSpellResistance_DoNotReplace()
        {
            var spellResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.SpellResistance,
                Power = 10
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { spellResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[5] }))
                .And.Not.Contains(specialQualities[5])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(spellResistance.Power, Is.EqualTo(10));
        }

        [Test]
        public void IfCreatureHasWeakerDarkvision_Replace()
        {
            var darkvision = new Feat
            {
                Name = FeatConstants.SpecialQualities.Darkvision,
                Power = 30
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { darkvision });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Skip(1))
                .And.Not.Contains(specialQualities[0])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(darkvision.Power, Is.EqualTo(60));
        }

        [Test]
        public void IfCreatureHasStrongerDarkvision_DoNotReplace()
        {
            var darkvision = new Feat
            {
                Name = FeatConstants.SpecialQualities.Darkvision,
                Power = 90
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { darkvision });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Skip(1))
                .And.Not.Contains(specialQualities[0])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(darkvision.Power, Is.EqualTo(90));
        }

        [TestCase(FeatConstants.Foci.Elements.Acid)]
        [TestCase(FeatConstants.Foci.Elements.Cold)]
        [TestCase(FeatConstants.Foci.Elements.Electricity)]
        public void IfCreatureHasWeakerEnergyResistance_Replace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var celestialSpecialQuality = specialQualities.First(f =>
                f.Name == FeatConstants.SpecialQualities.EnergyResistance
                && f.Foci.Contains(energy));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { celestialSpecialQuality }))
                .And.Not.Contains(celestialSpecialQuality)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(5));
        }

        [TestCase(FeatConstants.Foci.Elements.Acid)]
        [TestCase(FeatConstants.Foci.Elements.Cold)]
        [TestCase(FeatConstants.Foci.Elements.Electricity)]
        public void IfCreatureHasStrongerEnergyResistance_DoNotReplace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 15
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var celestialSpecialQuality = specialQualities.First(f =>
                f.Name == FeatConstants.SpecialQualities.EnergyResistance
                && f.Foci.Contains(energy));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { celestialSpecialQuality }))
                .And.Not.Contains(celestialSpecialQuality)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(15));
        }

        [TestCase(FeatConstants.Foci.Elements.Fire)]
        [TestCase(FeatConstants.Foci.Elements.Sonic)]
        public void IfCreatureHasEnergyResistanceToDifferentEnergy_DoNotReplace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(2));
        }

        [Test]
        public void IfCreatureHasWeakerDamageReduction_Replace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic" },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[4] }))
                .And.Not.Contains(specialQualities[4])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(5));
        }

        [Test]
        public void IfCreatureHasStrongerDamageReduction_DoNotReplace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic" },
                Power = 10
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[4] }))
                .And.Not.Contains(specialQualities[4])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(10));
        }

        [Test]
        public void IfCreatureHasDamageReductionWithDifferentVulnerability_DoNotReplace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic, adamantine" },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AbilityAdjustments))]
        public void CreatureIntelligenceAdvancedToAtLeast3(int raceAdjust, int baseScore, int advanced, int adjusted)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = baseScore;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = raceAdjust;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = advanced;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(adjusted).And.AtLeast(3));

            if (baseScore + raceAdjust + advanced < 3)
            {
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            }
            else
            {
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            }
        }

        private static IEnumerable AbilityAdjustments
        {
            get
            {
                //INFO: Don't need every combination, just want to test the 3 threshold
                yield return new TestCaseData(0, 10, 0, 10);
                yield return new TestCaseData(0, 10, 2, 12);
                yield return new TestCaseData(-2, 10, 0, 8);
                yield return new TestCaseData(-8, 10, 0, 3);
                yield return new TestCaseData(-10, 10, 0, 3);
                yield return new TestCaseData(-8, 10, 2, 4);
            }
        }

        [Test]
        public void IfCreatureDoesNotHaveIntelligence_GainIntelligenceOf3()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].BaseScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var hitDice = new List<double>(Enumerable.Range(1, 20)
                    .Select(i => Convert.ToDouble(i)));

                hitDice.AddRange(new[]
                {
                    .1, .2, .3, .4, .5, .6, .7, .8, .9,
                });

                //INFO: Don't need to test every CR, since it is the basic Increase functionality, which is tested separately
                //So, we only need to test the amount it is increased, not every CR permutation
                var challengeRating = ChallengeRatingConstants.CR1;

                foreach (var hitDie in hitDice)
                {
                    var increase = 0;

                    if (hitDie > 7)
                    {
                        increase = 2;
                    }
                    else if (hitDie > 3)
                    {
                        increase = 1;
                    }

                    var newCr = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, increase);
                    yield return new TestCaseData(hitDie, challengeRating, newCr);
                }
            }
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good, AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good, AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good, AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulGood)]
        public void ApplyTo_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var filters = new Filters { Alignment = "preset Good" };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Good"));
        }

        [TestCase(null, null)]
        [TestCase(0, 2)]
        [TestCase(1, 3)]
        [TestCase(2, 4)]
        [TestCase(10, 12)]
        [TestCase(42, 44)]
        public void LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.CelestialCreature}");

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralGood, "Alignment filter 'Neutral Good' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulGood, "CR filter 2 does not match updated creature CR 1 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.CelestialCreature}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature()
        {
            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplates()
        {
            baseCreature.Templates.Add("other template");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1;
            filters.Alignment = AlignmentConstants.LawfulGood;

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.CelestialCreature));
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Fey, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.MagicalBeast)]
        public async Task ApplyToAsync_CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            if (original == adjusted)
            {
                Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
                Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                    .And.Contains("subtype 2")
                    .And.Contains(CreatureConstants.Types.Subtypes.Extraplanar)
                    .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
            }
            else
            {
                Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
                Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                    .And.Contains("subtype 2")
                    .And.Contains(original)
                    .And.Contains(CreatureConstants.Types.Subtypes.Extraplanar)
                    .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
            }
        }

        [Test]
        public async Task ApplyToAsync_CreatureSizeIsNotAdjusted()
        {
            baseCreature.Size = "my size";

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Size, Is.EqualTo("my size"));
        }

        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        [TestCase(21, 20)]
        [TestCase(22, 20)]
        [TestCase(42, 20)]
        public async Task ApplyToAsync_CreatureGainsSmiteEvilSpecialAttack(double hitDiceQuantity, int smiteDamage)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;

            var originalAttacks = baseCreature.Attacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();
            var originalSpecialAttacks = baseCreature.SpecialAttacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalAttacks.Length + 1));
            Assert.That(creature.Attacks.Select(a => a.Name), Is.SupersetOf(originalAttacks.Select(a => a.Name)));
            Assert.That(creature.Attacks, Contains.Item(smiteEvil));
            Assert.That(creature.SpecialAttacks.Count(), Is.EqualTo(originalSpecialAttacks.Length + 1));
            Assert.That(creature.SpecialAttacks, Contains.Item(smiteEvil));

            Assert.That(smiteEvil.DamageDescription, Is.EqualTo(smiteDamage.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainSpecialQualities()
        {
            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasWeakerSpellResistance_Replace()
        {
            var spellResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.SpellResistance,
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { spellResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[5] }))
                .And.Not.Contains(specialQualities[5])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(spellResistance.Power, Is.EqualTo(5));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasStrongerSpellResistance_DoNotReplace()
        {
            var spellResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.SpellResistance,
                Power = 10
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { spellResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[5] }))
                .And.Not.Contains(specialQualities[5])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(spellResistance.Power, Is.EqualTo(10));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasWeakerDarkvision_Replace()
        {
            var darkvision = new Feat
            {
                Name = FeatConstants.SpecialQualities.Darkvision,
                Power = 30
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { darkvision });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Skip(1))
                .And.Not.Contains(specialQualities[0])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(darkvision.Power, Is.EqualTo(60));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasStrongerDarkvision_DoNotReplace()
        {
            var darkvision = new Feat
            {
                Name = FeatConstants.SpecialQualities.Darkvision,
                Power = 90
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { darkvision });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Skip(1))
                .And.Not.Contains(specialQualities[0])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(darkvision.Power, Is.EqualTo(90));
        }

        [TestCase(FeatConstants.Foci.Elements.Acid)]
        [TestCase(FeatConstants.Foci.Elements.Cold)]
        [TestCase(FeatConstants.Foci.Elements.Electricity)]
        public async Task ApplyToAsync_IfCreatureHasWeakerEnergyResistance_Replace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var celestialSpecialQuality = specialQualities.First(f =>
                f.Name == FeatConstants.SpecialQualities.EnergyResistance
                && f.Foci.Contains(energy));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { celestialSpecialQuality }))
                .And.Not.Contains(celestialSpecialQuality)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(5));
        }

        [TestCase(FeatConstants.Foci.Elements.Acid)]
        [TestCase(FeatConstants.Foci.Elements.Cold)]
        [TestCase(FeatConstants.Foci.Elements.Electricity)]
        public async Task ApplyToAsync_IfCreatureHasStrongerEnergyResistance_DoNotReplace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 15
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var celestialSpecialQuality = specialQualities.First(f =>
                f.Name == FeatConstants.SpecialQualities.EnergyResistance
                && f.Foci.Contains(energy));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { celestialSpecialQuality }))
                .And.Not.Contains(celestialSpecialQuality)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(15));
        }

        [TestCase(FeatConstants.Foci.Elements.Fire)]
        [TestCase(FeatConstants.Foci.Elements.Sonic)]
        public async Task ApplyToAsync_IfCreatureHasEnergyResistanceToDifferentEnergy_DoNotReplace(string energy)
        {
            var energyResistance = new Feat
            {
                Name = FeatConstants.SpecialQualities.EnergyResistance,
                Foci = new[] { energy },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { energyResistance });

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 0 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(energyResistance.Power, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasWeakerDamageReduction_Replace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic" },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[4] }))
                .And.Not.Contains(specialQualities[4])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(5));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasStrongerDamageReduction_DoNotReplace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic" },
                Power = 10
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length - 1));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities.Except(new[] { specialQualities[4] }))
                .And.Not.Contains(specialQualities[4])
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(10));
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureHasDamageReductionWithDifferentVulnerability_DoNotReplace()
        {
            var damageReduction = new Feat
            {
                Name = FeatConstants.SpecialQualities.DamageReduction,
                Foci = new[] { "Vulnerable to magic, adamantine" },
                Power = 2
            };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { damageReduction });

            var originalSpecialQualities = baseCreature.SpecialQualities.ToArray();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var specialQualities = new[]
            {
                new Feat { Name = FeatConstants.SpecialQualities.Darkvision, Power = 60 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Acid }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Cold }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.EnergyResistance, Foci = new[] { FeatConstants.Foci.Elements.Electricity }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.DamageReduction, Foci = new[] { "Vulnerable to magic" }, Power = 5 },
                new Feat { Name = FeatConstants.SpecialQualities.SpellResistance, Power = 5 },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.CelestialCreature,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Extraplanar,
                            CreatureConstants.Types.Subtypes.Augmented,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(specialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities.Count(), Is.GreaterThan(originalSpecialQualities.Length)
                .And.EqualTo(originalSpecialQualities.Length + specialQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(specialQualities)
                .And.SupersetOf(originalSpecialQualities));
            Assert.That(damageReduction.Power, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AbilityAdjustments))]
        public async Task ApplyToAsync_CreatureIntelligenceAdvancedToAtLeast3(int raceAdjust, int baseScore, int advanced, int adjusted)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = baseScore;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = raceAdjust;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = advanced;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(adjusted).And.AtLeast(3));

            if (baseScore + raceAdjust + advanced < 3)
            {
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            }
            else
            {
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
                Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            }
        }

        [Test]
        public async Task ApplyToAsync_IfCreatureDoesNotHaveIntelligence_GainIntelligenceOf3()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].BaseScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good, AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good, AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good, AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulGood)]
        public async Task ApplyToAsync_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var filters = new Filters { Alignment = "preset Good" };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Good"));
        }

        [TestCase(null, null)]
        [TestCase(0, 2)]
        [TestCase(1, 3)]
        [TestCase(2, 4)]
        [TestCase(10, 12)]
        [TestCase(42, 44)]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GainARandomLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Angelic"));
        }

        [Test]
        public void ApplyTo_GainALanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public void ApplyTo_GainALanguage_AlreadyHas()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Angelic" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Angelic"));
        }

        [Test]
        public async Task ApplyToAsync_GainARandomLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Angelic"));
        }

        [Test]
        public async Task ApplyToAsync_GainALanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_GainALanguage_AlreadyHas()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Angelic" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.CelestialCreature + LanguageConstants.Groups.Automatic))
                .Returns("Angelic");

            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Angelic"));
        }

        [Test]
        public void ApplyTo_RegenerateMagic()
        {
            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var newMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good),
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(newMagic);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(newMagic));
        }

        [Test]
        public async Task ApplyToAsync_RegenerateMagic()
        {
            var smiteEvil = new Attack
            {
                Name = "Smite Evil",
                IsSpecial = true
            };
            mockAttackGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.CelestialCreature,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(new[] { smiteEvil });

            var newMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Good),
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(newMagic);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(newMagic));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnEmpty_WhenAlignmentFilterInvalid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset Good", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset Neutral", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = "preset Good" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(true, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(true, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(bool asCharacter, double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, -3) };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 3) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;
            hitDice["my other creature"] = hitDiceQuantity;
            hitDice["wrong creature 1"] = ChallengeRatingConstants.IsGreaterThan(challengeRating, original) ? 0 : 666;
            hitDice["wrong creature 2"] = hitDiceQuantity;
            hitDice["wrong creature 3"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, asCharacter, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 666;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
        }

        [Test]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 666;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { ChallengeRating = ChallengeRatingConstants.CR2, Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, true)]
        [TestCase(CreatureConstants.Types.Elemental, false)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, true)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void IsCompatible_IncorporealIsNotValid(string creatureType)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { creatureType, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(AlignmentConstants.LawfulGood, true)]
        [TestCase(AlignmentConstants.NeutralGood, true)]
        [TestCase(AlignmentConstants.ChaoticGood, true)]
        [TestCase(AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, false)]
        public void IsCompatible_MustHaveNonEvilAlignment(string alignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { alignment, "other Evil" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulGood, true)]
        [TestCase(AlignmentConstants.NeutralGood, true)]
        [TestCase(AlignmentConstants.ChaoticGood, true)]
        [TestCase(AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, false)]
        public void IsCompatible_MustHaveAnyNonEvilAlignment(string alignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other Evil", alignment };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Humanoid, null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Humanoid, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Humanoid, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Extraplanar, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Humanoid, "wrong type", false)]
        [TestCase(CreatureConstants.Types.Animal, null, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.Animal, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Animal, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Subtypes.Extraplanar, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Animal, "wrong type", false)]
        [TestCase(CreatureConstants.Types.Vermin, null, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Vermin, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.Vermin, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Vermin, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Subtypes.Extraplanar, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Vermin, "wrong type", false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, null, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Vermin, false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Animal, false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Subtypes.Extraplanar, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "wrong type", false)]
        public void IsCompatible_TypeMustMatch(string originalType, string type, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { originalType, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.LawfulGood, true)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.NeutralGood, true)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticGood, true)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticEvil, false)]
        public void IsCompatible_AlignmentMustMatch(string alignmentFilter, string creatureAlignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other Evil", creatureAlignment };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, AlignmentConstants.LawfulGood, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, AlignmentConstants.NeutralGood, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, AlignmentConstants.NeutralGood, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulGood, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralGood, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulGood, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralGood, false)]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", AlignmentConstants.LawfulNeutral };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = type, ChallengeRating = challengeRating, Alignment = alignment };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, CasterLevel = 9266, LevelAdjustment = 90210 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR3, CasterLevel = 0 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 9;
            hitDice["my other creature"] = 5;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(9));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment(AlignmentConstants.ChaoticGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnEmpty_WhenAlignmentFilterInvalid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(s => s.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset Good", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset Neutral", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Alignment = "preset Good" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCase(true, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(true, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnCompatibleCreatures(bool asCharacter, double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, -3) };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 3) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;
            hitDice["my other creature"] = hitDiceQuantity;
            hitDice["wrong creature 1"] = ChallengeRatingConstants.IsGreaterThan(challengeRating, original) ? 0 : 666;
            hitDice["wrong creature 2"] = hitDiceQuantity;
            hitDice["wrong creature 3"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(asCharacter && hitDiceQuantity <= 1 ? ChallengeRatingConstants.CR0 : data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(asCharacter && hitDiceQuantity <= 1  ? ChallengeRatingConstants.CR0 : data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), asCharacter))
                .Returns(prototypes);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, asCharacter, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment("different Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 666;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment("different Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood, "different alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 666;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { ChallengeRating = ChallengeRatingConstants.CR2, Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment("different Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithAllowedAlignments()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment", "other Evil" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "wrong Evil" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticGood, "different alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR4 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 666;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(9)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralEvil)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .WithCasterLevel(0)
                    .WithHitDiceQuantity(5)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(9));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment(AlignmentConstants.ChaoticGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnEmpty_WhenAlignmentFilterInvalid()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(9)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralEvil)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .WithCasterLevel(0)
                    .WithHitDiceQuantity(5)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("preset Good", "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(9)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralEvil)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments("preset Neutral", AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .WithCasterLevel(0)
                    .WithHitDiceQuantity(5)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset Good" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(9));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(5));
        }

        [TestCase(true, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(true, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(true, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(true, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(true, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(true, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(false, 1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(false, 1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(false, 4, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(false, 4, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 8, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 8, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(false, 20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(false, 20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnCompatibleCreatures(bool asCharacter, double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(asCharacter && hitDiceQuantity <= 1 ? ChallengeRatingConstants.CR0 : original)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(original, -3))
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 3))
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithChallengeRating(asCharacter && hitDiceQuantity <= 1 ? ChallengeRatingConstants.CR0 : original)
                    .WithCasterLevel(0)
                    .WithLevelAdjustment(null)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(original)
                    .WithHitDiceQuantity(ChallengeRatingConstants.IsGreaterThan(challengeRating, original) ? 0 : 666)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, asCharacter, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment(AlignmentConstants.ChaoticGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(9)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralEvil)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralGood, AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .WithCasterLevel(0)
                    .WithLevelAdjustment(null)
                    .WithHitDiceQuantity(5)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(1)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(9));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment(AlignmentConstants.ChaoticGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithCasterLevel(9266)
                    .WithLevelAdjustment(90210)
                    .WithHitDiceQuantity(4)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithChallengeRating(ChallengeRatingConstants.CR4)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "different alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .WithCasterLevel(0)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(666)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(9266));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(90212));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.ChaoticGood),
                new Alignment("different Good"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood, "other alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithChallengeRating(ChallengeRatingConstants.CR4)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulGood)
                    .WithChallengeRating(ChallengeRatingConstants.CR4)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, -2)
                    .WithAbility(AbilityConstants.Constitution, 3)
                    .WithAbility(AbilityConstants.Dexterity, -4)
                    .WithAbility(AbilityConstants.Intelligence, 5)
                    .WithAbility(AbilityConstants.Wisdom, -6)
                    .WithAbility(AbilityConstants.Charisma, 7)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood, "different alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticGood, "different alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(666)
                    .WithAbility(AbilityConstants.Strength, 8)
                    .WithAbility(AbilityConstants.Constitution, -9)
                    .WithAbility(AbilityConstants.Dexterity, 0)
                    .WithAbility(AbilityConstants.Intelligence, -1)
                    .WithAbility(AbilityConstants.Wisdom, 2)
                    .WithAbility(AbilityConstants.Charisma, -3)
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = ChallengeRatingConstants.CR2, Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
                new Alignment("different Good"),
            }));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithAllowedAlignments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulNeutral, "other alignment", "other Evil")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.TrueNeutral, "wrong Evil")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1346));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(610));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1347));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(52));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90220));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(3));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(793));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
            }));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithImprovedTemplateAdjustment()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulNeutral, "other alignment", "other Evil")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210, 69)
                    .WithAbility(AbilityConstants.Dexterity, 42, 420)
                    .WithAbility(AbilityConstants.Intelligence, 600, -1)
                    .WithAbility(AbilityConstants.Wisdom, 1337, -2)
                    .WithAbility(AbilityConstants.Charisma, 1336, -3)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithAlignments(AlignmentConstants.TrueNeutral, "wrong Evil")
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 96, -4)
                    .WithAbility(AbilityConstants.Constitution, 783, 5)
                    .WithAbility(AbilityConstants.Dexterity, 8245, -6)
                    .WithAbility(AbilityConstants.Intelligence, -8, 7)
                    .WithAbility(AbilityConstants.Wisdom, 0, 8)
                    .WithAbility(AbilityConstants.Charisma, 1, -9)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 - 3));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 - 1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337 - 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 420));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 69));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulGood),
                new Alignment("other Good"),
            }));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.MagicalBeast));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Animal,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1 - 9));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 - 8 + 7));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 0 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 - 6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 783 + 5));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 96 - 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralGood),
            }));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }
    }
}
