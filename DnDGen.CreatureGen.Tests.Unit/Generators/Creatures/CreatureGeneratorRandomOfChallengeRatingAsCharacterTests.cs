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
    internal class CreatureGeneratorRandomOfChallengeRatingAsCharacterTests : CreatureGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpCreature("creature", "template", true, "challenge rating");
        }

        [Test]
        public void GenerateRandomName_OfChallengeRatingAsCharacter_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, null, null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(true, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomName_OfChallengeRatingAsCharacter_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, null, null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var name = creatureGenerator.GenerateRandomName(true, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomName_OfChallengeRatingAsCharacter_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, null, null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var name = creatureGenerator.GenerateRandomName(true, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomName_OfChallengeRatingAsCharacter_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, null, null, "my challenge rating")).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures
                .SelectMany(c => templates.Select(t => (c, t)))
                .Union(creatures.Select(c => (c, CreatureConstants.Templates.None)));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, template));

            var name = creatureGenerator.GenerateRandomName(true, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomName_OfChallengeRatingAsCharacter_ThrowException_WhenCreatureCannotBeCharacter()
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, null, null, "my challenge rating")).Returns(false);

            Assert.That(() => creatureGenerator.GenerateRandomName(true, challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tCR: my challenge rating"));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            SetUpCreature(creatureName, template, true, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator
                .Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName)))
                .Callback((Creature c) => c.Template = template)
                .Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", "my template", "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(creatures);

            foreach (var template in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            SetUpCreature(creatureName, template, true, "my challenge rating");

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);
                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockTemplateApplicator.Object);

                if (otherTemplate == template)
                {
                    mockTemplateApplicator
                        .Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName)))
                        .Callback((Creature c) => c.Template = template)
                        .Returns((Creature c) => c);
                }
            }

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_ThrowException_WhenCreatureCannotBeCharacter()
        {
            var nonCharacters = CreatureConstants.GetAllNonCharacters();
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(nonCharacters);

            Assert.That(() => creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tCR: my challenge rating"));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            creatureData.ChallengeRating = "my challenge rating";

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            types.Add("subtype");
            creatureData.ChallengeRating = "my challenge rating";

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            types.Add("subtype");
            types.Add("other subtype");
            creatureData.ChallengeRating = "my challenge rating";

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureLanguages()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreatureAdvancement(true, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
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
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    creatureName,
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null.And.EqualTo(armorClass));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void GenerateRandom_OfChallengeRatingAsCharacter_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = creatureGenerator.GenerateRandom(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            SetUpCreature(creatureName, template, true, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator
                .Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName)))
                .Callback((Creature c) => c.Template = template)
                .ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", "my template", "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(creatures);

            foreach (var template in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = SetUpCreature(creatureName, template, true, "my challenge rating");

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates.Except(new[] { template }))
            {
                var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
                mockOtherTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockOtherTemplateApplicator.Object);
            }

            var typePairings = creatures.SelectMany(c => templates.Select(t => (c, t)));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, template));

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_ThrowException_WhenCreatureCannotBeCharacter()
        {
            var nonCharacters = CreatureConstants.GetAllNonCharacters();
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(nonCharacters);

            Assert.That(async () => await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tCR: my challenge rating"));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            creatureData.ChallengeRating = "my challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            types.Add("subtype");
            creatureData.ChallengeRating = "my challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            types[0] = "my type";
            types.Add("subtype");
            types.Add("other subtype");
            creatureData.ChallengeRating = "my challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacter_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreatureAdvancement(true, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
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
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    creatureName,
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

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

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateRandomAsync_OfChallengeRatingAsCharacterAsync_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, true, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(true, challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }
    }
}