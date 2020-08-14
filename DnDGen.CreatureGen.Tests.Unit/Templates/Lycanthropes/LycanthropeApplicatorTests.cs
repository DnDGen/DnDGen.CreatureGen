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
using System.Linq;
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
            (CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, CreatureConstants.Wolf_Dire),
            (CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, CreatureConstants.Wolf_Dire),
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
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted] = new LycanthropeDireWolfAfflictedApplicator();
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = new LycanthropeDireWolfNaturalApplicator();

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();
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
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Shapechanger));
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainConditionalSize(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_SizeSameForAnimalAndBaseCreature(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalHitPoints(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalSpeed(string template, string animal)
        {
            //TODO: Animal speed is conditional, only in animal form
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainNaturalArmor(string template, string animal)
        {
            //Both new for base and from animal
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_ImproveNaturalArmor(string template, string animal)
        {
            //TODO: Include conditions (2 overall, animal form, hybrid form, etc.)
            //Hybrid form is one of animal (with bonus), one of base (with bonus), since natural armor is max
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalBaseAttack(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_RecomputeGrappleBonus(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_RecomputeGrappleBonus_WithConditionalForms(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalAttacks(string template, string animal)
        {
            //TODO: Notate in animal form on attacks
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalAttacks_WithLycanthropy(string template, string animal)
        {
            //TODO: Get the lycanthrope attacks.  If any animal attacks match the lycanthrope attack with "Curse of Lycanthropy",
            //add the curse to the animal attack as well
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_ModifyBaseCreatureAttacks(string template, string animal)
        {
            //TODO: Notate in {creature type} or Hybrid form on attacks
            //Special attacks don't get the Hybrid addition
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddLycanthropeAttacks(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalSpecialQualities(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalSpecialQualities_RemoveDuplicates(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddLycanthropeSpecialQualities(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalSaveBonuses(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_AddAnimalSaveBonuses_WithBonusesFromNewFeats(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_WisdomIncreasesBy2(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_ConditionalBonusesForHybridAndAnimalForms_FromAnimalBonuses(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_IncreaseAbilityScoresFromExtraHitDice(string template, string animal)
        {
            //INFO: Probably want to confirm 1 per 4 with test cases
            //Also include "original" - so 3 original + 1 bonus = 4, get ability boost (1)
            //Also include "original" - so 11 original + 1 bonus = 4, get ability boost (1)
            //Also include "original" - so 7 original + 5 bonus = 4, get ability boost (2)
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainAnimalSkills(string template, string animal)
        {
            //Make sure don't get the X4 bonus for first hit die
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainControlShapeSkill_Afflicted(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_DoNotGainControlShapeSkill_Natural(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainAnimalFeats(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_GainAnimalFeats_RemoveDuplicates(string template, string animal)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_IncreaseChallengeRating(string template, string animal)
        {
            //Animal HD 0-2, +2
            //Animal HD 3-5, +3
            //Animal HD 6-10, +4
            //Animal HD 11-20, +5
            //Animal HD 21+, +6
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public void ApplyTo_IncreaseLevelAdjustment(string template, string animal)
        {
            //Afflicted, +2
            //Natural, +3
            //Probably just pull from table, easiest
            Assert.Fail("not yet written");
        }

        [TestCaseSource("AllLycanthropeTemplates")]
        public async Task ApplyToAsync_Tests(string template, string animal)
        {
            Assert.Fail("need to copy");
        }
    }
}
