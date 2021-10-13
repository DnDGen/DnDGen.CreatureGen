﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorRandomOfChallengeRatingTests : CreatureGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpCreature("creature", "template", false, "challenge rating");
        }

        [Test]
        public void GenerateRandomNameOfChallengeRating_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(false, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomNameOfChallengeRating_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";
            SetUpCreature(creatureName, template, false, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var name = creatureGenerator.GenerateRandomName(false, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomNameOfChallengeRating_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var name = creatureGenerator.GenerateRandomName(false, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomNameOfChallengeRating_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";
            SetUpCreature(creatureName, template, false, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures
                .SelectMany(c => templates.Select(t => (c, t)))
                .Union(creatures.Select(c => (c, CreatureConstants.Templates.None)));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, template));

            var name = creatureGenerator.GenerateRandomName(false, challengeRating: "my challenge rating");
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = SetUpCreature(creatureName, template, false, "my challenge rating");

            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator
                .Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName)))
                .Callback((Creature c) => c.Template = template)
                .Returns((Creature c) => c);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", "my template", "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
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
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = SetUpCreature(creatureName, template, false, "my challenge rating");

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates.Except(new[] { template }))
            {
                var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
                mockOtherTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);
                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockOtherTemplateApplicator.Object);
            }

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my type";

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my type";
            types.Add("subtype");

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my type";
            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureLanguages()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        //INFO: This is because if we are setting a CR, creature advancement may change the CR, which we explicitly do not want
        [Test]
        public void GenerateRandomOfChallengeRating_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
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
        public void GenerateRandomOfChallengeRating_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void GenerateRandomOfChallengeRating_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null.And.EqualTo(armorClass));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void GenerateRandomOfChallengeRating_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = creatureGenerator.GenerateRandom(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureName_NoTemplate()
        {
            var creatureName = "my creature";

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            SetUpCreature(creatureName, template, false, "my challenge rating");

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));
            mockTemplateApplicator
                .Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName)))
                .Callback((Creature c) => c.Template = template)
                .ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template name")).Returns(mockWrongTemplateApplicator2.Object);

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateRandomCreatureName_NoTemplate()
        {
            var creatureName = "my creature";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", "my template", "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(creatures);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            foreach (var template in templates)
            {
                var mockTemplateApplicator = new Mock<TemplateApplicator>();
                mockTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);
            }

            var typePairings = creatures.Select(c => (c, CreatureConstants.Templates.None));
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<(string CreatureName, string Template)>>(c => c.IsEquivalentTo(typePairings))))
                .Returns((creatureName, CreatureConstants.Templates.None));

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateRandomCreatureName_WithTemplate()
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var templates = new[] { "other template", template, "wrong template name" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = SetUpCreature(creatureName, template, false, "my challenge rating");

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(templates)))))
                .Returns(template);

            foreach (var otherTemplate in templates.Except(new[] { template }))
            {
                var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
                mockOtherTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "my challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);
                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(otherTemplate)).Returns(mockOtherTemplateApplicator.Object);
            }

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my challenge rating"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my type";
            types.Add("subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            types[0] = "my type";
            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Type.Name, Is.EqualTo("my type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_NeverGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
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
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

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

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateRandomOfChallengeRatingAsync_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, CreatureConstants.Templates.None, false, "my challenge rating");

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "my challenge rating"))
                .Returns(new[] { creatureName });

            var creature = await creatureGenerator.GenerateRandomAsync(false, challengeRating: "my challenge rating");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }
    }
}