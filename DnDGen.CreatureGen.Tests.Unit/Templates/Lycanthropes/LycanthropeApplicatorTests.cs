using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.Lycanthropes;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates.Lycanthropes
{
    [TestFixture]
    public class LycanthropeApplicatorTests
    {
        private Dictionary<string, TemplateApplicator> applicators;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;

        private static IEnumerable<(string Template, string Animal)> templates = new[]
        {
            (CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, CreatureConstants.Bear_Brown),
            (CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, CreatureConstants.Bear_Brown),
            (CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, CreatureConstants.Boar),
            (CreatureConstants.Templates.Lycanthrope_Boar_Natural, CreatureConstants.Boar),
            (CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, CreatureConstants.Boar_Dire),
            (CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, CreatureConstants.Boar_Dire),
            (CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, CreatureConstants.Rat_Dire),
            (CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, CreatureConstants.Rat_Dire),
            (CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, CreatureConstants.Tiger),
            (CreatureConstants.Templates.Lycanthrope_Tiger_Natural, CreatureConstants.Tiger),
            (CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, CreatureConstants.Wolf),
            (CreatureConstants.Templates.Lycanthrope_Wolf_Natural, CreatureConstants.Wolf),
        };

        private static IEnumerable AllLycanthropeTemplates
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template.Template, template.Animal);
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();

            applicators = new Dictionary<string, TemplateApplicator>();
            applicators[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted] = new LycanthropeBrownBearAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = new LycanthropeBrownBearNaturalApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted] = new LycanthropeBoarAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = new LycanthropeBoarNaturalApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted] = new LycanthropeDireBoarAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = new LycanthropeDireBoarNaturalApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted] = new LycanthropeDireRatAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = new LycanthropeDireRatNaturalApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted] = new LycanthropeTigerAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = new LycanthropeTigerNaturalApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted] = new LycanthropeWolfAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = new LycanthropeWolfNaturalApplicator();

            baseCreature = new CreatureBuilder().WithTestValues().Build();
        }

        [TestCaseSource("CreatureTypeCompatible")]
        public void IsCompatible_BasedOnCreatureType(string template, string animal, string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { Size = SizeConstants.Medium });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor(animal))
                .Returns(new CreatureDataSelection { Size = SizeConstants.Medium });

            var isCompatible = applicators[template].IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible
        {
            get
            {
                var compatibilities = new[]
                {
                    (CreatureConstants.Types.Aberration, false),
                    (CreatureConstants.Types.Animal, false),
                    (CreatureConstants.Types.Construct, false),
                    (CreatureConstants.Types.Dragon, false),
                    (CreatureConstants.Types.Elemental, false),
                    (CreatureConstants.Types.Fey, false),
                    (CreatureConstants.Types.Giant, true),
                    (CreatureConstants.Types.Humanoid, true),
                    (CreatureConstants.Types.MagicalBeast, false),
                    (CreatureConstants.Types.MonstrousHumanoid, false),
                    (CreatureConstants.Types.Ooze, false),
                    (CreatureConstants.Types.Outsider, false),
                    (CreatureConstants.Types.Plant, false),
                    (CreatureConstants.Types.Undead, false),
                    (CreatureConstants.Types.Vermin, false),
                };

                foreach (var template in templates)
                {
                    foreach (var compatibility in compatibilities)
                    {
                        yield return new TestCaseData(template.Template, template.Animal, compatibility.Item1, compatibility.Item2);
                    }
                }
            }
        }

        [TestCaseSource("SizeCompatible")]
        public void IsCompatible_BySize(string template, string animal, string creatureSize, string animalSize, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { Size = creatureSize });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor(animal))
                .Returns(new CreatureDataSelection { Size = animalSize });

            var isCompatible = applicators[template].IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        private static IEnumerable SizeCompatible
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                foreach (var template in templates)
                {
                    for (var c = 0; c < sizes.Length; c++)
                    {
                        for (var a = 0; a < sizes.Length; a++)
                        {
                            var compatible = Math.Abs(c - a) <= 1;

                            yield return new TestCaseData(template.Template, template.Animal, sizes[c], sizes[a], compatible);
                        }
                    }
                }
            }
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainShapechangerSubtype(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public async Task ApplyToAsync_Tests(string template, string animal)
        {
            Assert.Fail("need to copy");
        }
    }
}
