using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateRandomAsyncTests : CreatureGeneratorTests
    {
        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateCreatureName_NoTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, null, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            SetUpCreature(creatureName, template, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateCreatureName_WithRandomTemplate_Only1TemplateCompatible(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

            SetUpCreature(creatureName, template, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, null, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateCreatureName_WithRandomTemplate_OnlyTemplatesCompatible(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature name", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { template, "other template" }))))
                .Returns(template);

            SetUpCreature(creatureName, template, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, null, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateRandomCreatureName_NoTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(new[] { template, "other template" })))))
                .Returns(creatureName);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, null, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateRandomCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            SetUpCreature(creatureName, CreatureConstants.Templates.None, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateRandomCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            SetUpCreature(creatureName, template, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_GenerateRandomCreatureName_WithRandomTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", "other wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr, alignment)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "other wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "other wrong creature",
                    template,
                    "other template"
                }))))
                .Returns(template);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "wrong creature"
                }))))
                .Returns(creatureName);

            SetUpCreature(creatureName, template, asCharacter, cr, type);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, null, type, cr);
            Assert.That(creature.Name, Is.EqualTo(creatureName));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null, null)]
        [TestCase(true, null, null, null, "my alignment")]
        [TestCase(true, null, null, "challenge rating", null)]
        [TestCase(true, null, null, "challenge rating", "my alignment")]
        [TestCase(true, null, "type", null, null)]
        [TestCase(true, null, "type", null, "my alignment")]
        [TestCase(true, null, "type", "challenge rating", null)]
        [TestCase(true, null, "type", "challenge rating", "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, null, null, null)]
        [TestCase(true, CreatureConstants.Templates.None, null, null, "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating", null)]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating", "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, "type", null, null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", null, "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating", null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating", "my alignment")]
        [TestCase(true, "template", null, null, null)]
        [TestCase(true, "template", null, null, "my alignment")]
        [TestCase(true, "template", null, "challenge rating", null)]
        [TestCase(true, "template", null, "challenge rating", "my alignment")]
        [TestCase(true, "template", "type", null, null)]
        [TestCase(true, "template", "type", null, "my alignment")]
        [TestCase(true, "template", "type", "challenge rating", null)]
        [TestCase(true, "template", "type", "challenge rating", "my alignment")]
        [TestCase(false, null, null, null, null)]
        [TestCase(false, null, null, null, "my alignment")]
        [TestCase(false, null, null, "challenge rating", null)]
        [TestCase(false, null, null, "challenge rating", "my alignment")]
        [TestCase(false, null, "type", null, null)]
        [TestCase(false, null, "type", null, "my alignment")]
        [TestCase(false, null, "type", "challenge rating", null)]
        [TestCase(false, null, "type", "challenge rating", "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, null, null, null)]
        [TestCase(false, CreatureConstants.Templates.None, null, null, "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating", null)]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating", "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, "type", null, null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", null, "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating", null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating", "my alignment")]
        [TestCase(false, "template", null, null, null)]
        [TestCase(false, "template", null, null, "my alignment")]
        [TestCase(false, "template", null, "challenge rating", null)]
        [TestCase(false, "template", null, "challenge rating", "my alignment")]
        [TestCase(false, "template", "type", null, null)]
        [TestCase(false, "template", "type", null, "my alignment")]
        [TestCase(false, "template", "type", "challenge rating", null)]
        [TestCase(false, "template", "type", "challenge rating", "my alignment")]
        public async Task GenerateRandomAsync_ThrowException_WhenNotCompatible(bool asCharacter, string template, string type, string challengeRating, string alignment)
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, challengeRating, alignment)).Returns(false);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {asCharacter}");

            if (template == CreatureConstants.Templates.None)
                message.AppendLine($"\tTemplate: None");
            else if (template != null)
                message.AppendLine($"\tTemplate: {template}");

            if (type != null)
                message.AppendLine($"\tType: {type}");

            if (challengeRating != null)
                message.AppendLine($"\tCR: {challengeRating}");

            Assert.That(async () => await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, challengeRating),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSize(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSpace(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureReach(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureCanUseEquipment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.CanUseEquipment = true;

            mockEquipmentGenerator
                .Setup(g => g.Generate("creature",
                    true,
                    It.IsAny<IEnumerable<Feat>>(),
                    hitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureCannotUseEquipment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.CanUseEquipment = false;

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureChallengeRating(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureLevelAdjustment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateNoCreatureLevelAdjustment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureLevelAdjustmentOf0(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureCasterLevel(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureNumberOfHands(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureType(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureTypeWithSubtype(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            types.Add("subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureTypeWithMultipleSubtypes(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureAbilities(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureHitPoints(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureEquipment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureMagic(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureLanguages(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "challenge rating")]
        [TestCase(true, null, "type", null)]
        [TestCase(true, null, "type", "challenge rating")]
        [TestCase(true, CreatureConstants.Templates.None, null, null)]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating")]
        [TestCase(true, CreatureConstants.Templates.None, "type", null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating")]
        [TestCase(true, "template", null, null)]
        [TestCase(true, "template", null, "challenge rating")]
        [TestCase(true, "template", "type", null)]
        [TestCase(true, "template", "type", "challenge rating")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "challenge rating")]
        [TestCase(false, null, "type", null)]
        [TestCase(false, null, "type", "challenge rating")]
        [TestCase(false, CreatureConstants.Templates.None, null, null)]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating")]
        [TestCase(false, CreatureConstants.Templates.None, "type", null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating")]
        [TestCase(false, "template", null, null)]
        [TestCase(false, "template", null, "challenge rating")]
        [TestCase(false, "template", "type", null)]
        [TestCase(false, "template", "type", "challenge rating")]
        public async Task GenerateRandomAsync_DoNotGenerateAdvancedCreature_IfChallengeRatingSet(bool asCharacter, string template, string type, string challengeRating)
        {
            SetUpCreature("creature", template ?? CreatureConstants.Templates.None, asCharacter, challengeRating, type);
            var advancedhitPoints = SetUpCreatureAdvancement(asCharacter);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, challengeRating);

            if (string.IsNullOrEmpty(challengeRating))
            {
                Assert.That(creature.HitPoints, Is.EqualTo(advancedhitPoints));
                Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
                Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
                Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(681));
                Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(573));
                Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(492));
                Assert.That(creature.HitPoints.Total, Is.EqualTo(862));
                Assert.That(creature.Size, Is.EqualTo("advanced size"));
                Assert.That(creature.Space.Value, Is.EqualTo(54.32));
                Assert.That(creature.Reach.Value, Is.EqualTo(98.76));
                Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(3456));
                Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(783));
                Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(69));
                Assert.That(creature.ChallengeRating, Is.EqualTo("adjusted challenge rating"));
                Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
                Assert.That(creature.IsAdvanced, Is.True);
            }
            else
            {
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
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_DoNotGenerateAdvancedCreature_IfSelectorSaysNo(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            SetUpCreatureAdvancement(asCharacter);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
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

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreature(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedhitPoints = SetUpCreatureAdvancement(asCharacter);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedhitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(573));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(492));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(862));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
            Assert.That(creature.Space.Value, Is.EqualTo(54.32));
            Assert.That(creature.Reach.Value, Is.EqualTo(98.76));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(3456));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(783));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(69));
            Assert.That(creature.ChallengeRating, Is.EqualTo("adjusted challenge rating"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureWithExistingRacialAdjustments(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedHitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(573));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(492));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(862));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
            Assert.That(creature.Space.Value, Is.EqualTo(54.32));
            Assert.That(creature.Reach.Value, Is.EqualTo(98.76));
            Assert.That(creature.Abilities[AbilityConstants.Strength].RacialAdjustment, Is.EqualTo(38));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].RacialAdjustment, Is.EqualTo(47));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].RacialAdjustment, Is.EqualTo(56));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(3456));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(783));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(69));
            Assert.That(creature.ChallengeRating, Is.EqualTo("adjusted challenge rating"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureWithMissingAbilities(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedHitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(573));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(492));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(862));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
            Assert.That(creature.Space.Value, Is.EqualTo(54.32));
            Assert.That(creature.Reach.Value, Is.EqualTo(98.76));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(3456));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(783));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(69));
            Assert.That(creature.Abilities[AbilityConstants.Strength].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
            Assert.That(creature.ChallengeRating, Is.EqualTo("adjusted challenge rating"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSkills(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureSkills(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(advancedSkills, advancedFeats, abilities)).Returns(advancedSkills);

            var modifiedAdvancedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAdvancedAttacks);

            var equipmentAdvancedAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAdvancedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAdvancedAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    equipmentAdvancedAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSpecialQualities(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureSpecialQualities(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureBaseAttackBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }
        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureBaseAttackBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureAttacks(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureAttacks(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            var modifiedAdvancedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAdvancedAttacks);

            var equipmentAdvancedAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAdvancedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAdvancedAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    equipmentAdvancedAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureHitPointsWithFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var updatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, feats)).Returns(updatedHitPoints);

            mockEquipmentGenerator
                .Setup(g => g.Generate("creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    updatedHitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureHitPointsWithFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            var advancedUpdatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(advancedHitPoints, advancedFeats)).Returns(advancedUpdatedHitPoints);

            var modifiedAdvancedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAdvancedAttacks);

            var equipmentAdvancedAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAdvancedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAdvancedAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedUpdatedHitPoints.RoundedHitDiceQuantity,
                    equipmentAdvancedAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSkillsUpdatedByFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            var updatedSkills = new List<Skill> { new Skill("updated advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(advancedSkills, advancedFeats, abilities)).Returns(updatedSkills);

            var modifiedAdvancedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAdvancedAttacks);

            var equipmentAdvancedAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAdvancedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAdvancedAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    equipmentAdvancedAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", updatedSkills, advancedEquipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            SetUpCreatureAdvancement(asCharacter);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateNoGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_ApplyAttackBonuses(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var modifiedAttacks = new[] { new Attack() { Name = "modified attack" } };
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(modifiedAttacks);

            var equipmentAttacks = new[] { new Attack() { Name = "equipment attack" } };
            mockEquipmentGenerator.Setup(g => g.AddAttacks(feats, modifiedAttacks, creatureData.NumberOfHands)).Returns(equipmentAttacks);

            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    feats,
                    hitPoints.RoundedHitDiceQuantity,
                    equipmentAttacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_ApplyAdvancedAttackBonuses(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    advancedHitPoints,
                    abilities,
                    advancedSkills,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    alignment))
                .Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    advancedHitPoints,
                    999,
                    abilities,
                    advancedSkills,
                    advancedAttacks,
                    advancedSpecialQualities,
                    1029 + 6331,
                    speeds,
                    1336 + 8245,
                    96,
                    "advanced size",
                    creatureData.CanUseEquipment))
                .Returns(advancedFeats);

            var modifiedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAttacks);

            var equipmentAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    equipmentAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureInitiativeBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureInitiativeBonus(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(asCharacter);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(asCharacter);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureInitiativeBonusWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(asCharacter);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSpeeds(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureArmorClass(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var armorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    feats,
                    creatureData.NaturalArmor,
                    equipment))
                .Returns(armorClass);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureArmorClass(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                "creature",
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>() { new Feat() { Name = "advanced feat" } };
            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                advancedSkills,
                advancedAttacks,
                advancedSpecialQualities,
                1029 + 6331,
                speeds,
                1336 + 8245,
                96,
                "advanced size",
                creatureData.CanUseEquipment)).Returns(advancedFeats);

            var modifiedAdvancedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    advancedAttacks,
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    abilities))
                .Returns(modifiedAdvancedAttacks);

            var equipmentAdvancedAttacks = new[] { new Attack() { Name = "equipment advanced attack" } };
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()),
                    modifiedAdvancedAttacks,
                    creatureData.NumberOfHands))
                .Returns(equipmentAdvancedAttacks);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.Generate(
                    "creature",
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    equipmentAdvancedAttacks,
                    abilities,
                    "advanced size"))
                .Returns(advancedEquipment);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    "advanced size",
                    "creature",
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties("creature", advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureSaves(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateAdvancedCreatureSaves(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureAlignment(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureModifiedByTemplate(bool asCharacter)
        {
            var mockTemplateApplicator = SetUpCreature("creature", "my template", asCharacter);

            var templates = new[] { "wrong template", "my template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, null, null, null))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>(), asCharacter, null, null, null)).ReturnsAsync(templateCreature);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_GenerateCreatureModifiedByTemplate_WithFilters(bool asCharacter)
        {
            var mockTemplateApplicator = SetUpCreature("creature", "my template", asCharacter);

            var templates = new[] { "wrong template", "my template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, "my type", "my challenge rating", "my alignment"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>(), asCharacter, null, null, null)).ReturnsAsync(templateCreature);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, type: "my type", challengeRating: "my challenge rating", alignment: "my alignment");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GenerateRandomAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero(bool asCharacter)
        {
            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", CreatureConstants.Templates.None, asCharacter);

            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter);
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }
    }
}