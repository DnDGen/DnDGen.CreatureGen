using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorRandomOfTemplateWithChallengeRatingTests : CreatureGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpCreature("creature", "template", false, crFilter: "challenge rating");
        }

        [Test]
        public void GenerateRandomName_OfTemplateAndChallengeRating_GenerateCreatureName()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, "my template", null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(false, "my template", challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo("my template"));
        }

        [Test]
        public void GenerateRandomName_OfTemplateAndChallengeRating_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, "my template", null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(false, "my template", challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo("my template"));
        }

        [Test]
        public void GenerateRandomName_OfTemplateAndChallengeRating_ThrowException_WhenNotCompatible()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, "my template", null, "my challenge rating")).Returns(false);

            Assert.That(() => creatureGenerator.GenerateRandomName(false, "my template", challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: False\n\tTemplate: my template\n\tCR: my challenge rating"));
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_ThrowException_WhenNotCompatible()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, "my template", null, "my challenge rating")).Returns(false);

            Assert.That(() => creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: False\n\tTemplate: my template\n\tCR: my challenge rating"));
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = true;

            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    true,
                    It.IsAny<IEnumerable<Feat>>(),
                    hitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureLanguages()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Languages, Is.EqualTo(languages));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
            Assert.That(creature.Size, Is.EqualTo("size"));
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
            Assert.That(creature.IsAdvanced, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var updatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, feats)).Returns(updatedHitPoints);

            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    updatedHitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    creatureName,
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            var mockTemplateApplicator = SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var modifiedAttacks = new[] { new Attack() { Name = "modified attack" } };
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(modifiedAttacks);

            var equipmentAttacks = new[] { new Attack() { Name = "equipment attack" } };
            mockEquipmentGenerator.Setup(g => g.AddAttacks(feats, modifiedAttacks, creatureData.NumberOfHands)).Returns(equipmentAttacks);

            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    creatureName,
                    creatureData.CanUseEquipment,
                    feats,
                    hitPoints.RoundedHitDiceQuantity,
                    equipmentAttacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var armorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "size",
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    feats,
                    creatureData.NaturalArmor,
                    equipment))
                .Returns(armorClass);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null.And.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfTemplateAndChallengeRating_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = creatureGenerator.GenerateRandom(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_ThrowException_WhenNotCompatible()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, "my template", null, "my challenge rating")).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: False\n\tTemplate: my template\n\tCR: my challenge rating"));
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = true;

            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    true,
                    It.IsAny<IEnumerable<Feat>>(),
                    hitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
            Assert.That(creature.Size, Is.EqualTo("size"));
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
            Assert.That(creature.IsAdvanced, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var updatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, feats)).Returns(updatedHitPoints);

            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    updatedHitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    creatureName,
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var modifiedAttacks = new[] { new Attack() { Name = "modified attack" } };
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(modifiedAttacks);

            var equipmentAttacks = new[] { new Attack() { Name = "equipment attack" } };
            mockEquipmentGenerator.Setup(g => g.AddAttacks(feats, modifiedAttacks, creatureData.NumberOfHands)).Returns(equipmentAttacks);

            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    creatureName,
                    creatureData.CanUseEquipment,
                    feats,
                    hitPoints.RoundedHitDiceQuantity,
                    equipmentAttacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var armorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "size",
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    feats,
                    creatureData.NaturalArmor,
                    equipment))
                .Returns(armorClass);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfTemplateAndChallengeRating_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, "my template", challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }
    }
}