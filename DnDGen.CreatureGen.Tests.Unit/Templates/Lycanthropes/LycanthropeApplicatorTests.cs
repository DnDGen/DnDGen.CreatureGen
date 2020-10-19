using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.Lycanthropes;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using Moq.Language;
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
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<Dice> mockDice;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;

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

        private HitPoints animalHitPoints;
        private Random random;
        private CreatureDataSelection animalData;
        private int baseRoll;
        private double baseAverage;
        private List<Skill> animalSkills;
        private List<Attack> animalAttacks;
        private List<Feat> animalSpecialQualities;
        private List<Feat> animalFeats;
        private int animalBaseAttack;
        private Dictionary<string, Save> animalSaves;
        private Dictionary<string, Measurement> animalSpeeds;
        private Dictionary<string, ISetupSequentialResult<IEnumerable<int>>> rollSequences;
        private Dictionary<string, ISetupSequentialResult<double>> averageSequences;

        private static IEnumerable AllLycanthropeTemplates => templates.Select(t => new TestCaseData(t.Template, t.Animal));

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockDice = new Mock<Dice>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();

            applicators = new Dictionary<string, TemplateApplicator>();
            applicators[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted] = new LycanthropeBrownBearAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = new LycanthropeBrownBearNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted] = new LycanthropeBoarAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = new LycanthropeBoarNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted] = new LycanthropeDireBoarAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = new LycanthropeDireBoarNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted] = new LycanthropeDireRatAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = new LycanthropeDireRatNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted] = new LycanthropeTigerAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = new LycanthropeTigerNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted] = new LycanthropeWolfAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = new LycanthropeWolfNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted] = new LycanthropeDireWolfAfflictedApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);
            applicators[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = new LycanthropeDireWolfNaturalApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();

            animalHitPoints = new HitPoints();
            random = new Random();
            animalData = new CreatureDataSelection();
            animalSkills = new List<Skill>();
            animalAttacks = new List<Attack>();
            animalSpecialQualities = new List<Feat>();
            animalFeats = new List<Feat>();
            animalSaves = new Dictionary<string, Save>();
            animalSpeeds = new Dictionary<string, Measurement>();
            rollSequences = new Dictionary<string, ISetupSequentialResult<IEnumerable<int>>>();
            averageSequences = new Dictionary<string, ISetupSequentialResult<double>>();

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, It.IsAny<IEnumerable<Feat>>()))
                .Returns(baseCreature.HitPoints);

            baseRoll = random.Next(baseCreature.HitPoints.HitDice[0].RoundedQuantity * baseCreature.HitPoints.HitDice[0].HitDie) + baseCreature.HitPoints.HitDice[0].RoundedQuantity;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = baseCreature.HitPoints.HitDice[0].RoundedQuantity * baseCreature.HitPoints.HitDice[0].HitDie / 2d + baseCreature.HitPoints.HitDice[0].RoundedQuantity;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
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

        [TestCaseSource(nameof(SizeCompatible))]
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

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainShapechangerSubtype(string template, string animal)
        {
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Shapechanger));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalHitPoints_NoConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}"));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(baseAverage + 90210.42)));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(baseRoll + 9266));
        }

        private void SetUpAnimal(
            string animal,
            int naturalArmor = -1,
            string size = null,
            int hitDiceQuantity = 0,
            int hitDiceDie = 0,
            int roll = 0,
            double average = 0)
        {
            //Data
            animalData.Size = size ?? "animal size";
            animalData.CasterLevel = 0;
            animalData.NumberOfHands = random.Next(3);
            animalData.CanUseEquipment = false;
            animalData.NaturalArmor = naturalArmor > -1 ? naturalArmor : random.Next(20);

            mockCreatureDataSelector
                .Setup(s => s.SelectFor(animal))
                .Returns(animalData);

            //Hit points
            var hitDie = new HitDice();
            hitDie.Quantity = hitDiceQuantity > 0 ? hitDiceQuantity : random.Next(30) + 1;
            hitDie.HitDie = hitDiceDie > 0 ? hitDiceDie : random.Next(7) + 6;
            animalHitPoints.HitDice.Add(hitDie);

            mockHitPointsGenerator
                .Setup(g => g.GenerateFor(
                    animal,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities[AbilityConstants.Constitution],
                    animalData.Size,
                    0))
                .Returns(animalHitPoints);

            if (roll == 0)
                roll = random.Next(hitDie.RoundedQuantity * hitDie.HitDie) + hitDie.RoundedQuantity;

            SetUpRoll(hitDie, roll);

            if (average == 0)
                average = hitDie.RoundedQuantity * hitDie.HitDie / 2d + hitDie.RoundedQuantity;

            SetUpRoll(hitDie, average);

            //Skills
            animalSkills.Add(new Skill("animal skill 1", baseCreature.Abilities[AbilityConstants.Strength], hitDie.RoundedQuantity + 3)
            {
                ClassSkill = true,
                Ranks = random.Next(hitDie.RoundedQuantity)
            });
            animalSkills.Add(new Skill("animal skill 2", baseCreature.Abilities[AbilityConstants.Strength], hitDie.RoundedQuantity + 3)
            {
                ClassSkill = true,
                Ranks = random.Next(hitDie.RoundedQuantity)
            });

            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    animalHitPoints,
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    baseCreature.CanUseEquipment,
                    animalData.Size,
                    false))
                .Returns(animalSkills);

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.IsAny<IEnumerable<Skill>>(),
                    animalHitPoints,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    false))
                .Returns(animalSkills);

            //Special Qualities
            animalSpecialQualities.Add(new Feat { Name = "animal special quality 1" });
            animalSpecialQualities.Add(new Feat { Name = "animal special quality 2" });

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    baseCreature.Abilities,
                    animalSkills,
                    animalData.CanUseEquipment,
                    animalData.Size,
                    baseCreature.Alignment))
                .Returns(animalSpecialQualities);

            //Attacks
            animalBaseAttack = random.Next(100);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints))
                .Returns(animalBaseAttack);

            animalAttacks.Add(new Attack { Name = "animal attack 1" });
            animalAttacks.Add(new Attack { Name = "animal attack 2" });

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    animal,
                    animalData.Size,
                    animalData.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + hitDie.RoundedQuantity))
                .Returns(animalAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    animalAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns(animalAttacks);

            //Feats
            animalFeats.Add(new Feat { Name = "animal feat 1" });
            animalFeats.Add(new Feat { Name = "animal feat 2" });

            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    animalHitPoints,
                    animalBaseAttack,
                    baseCreature.Abilities,
                    animalSkills,
                    animalAttacks,
                    animalSpecialQualities,
                    animalData.CasterLevel,
                    baseCreature.Speeds,
                    animalData.NaturalArmor,
                    animalData.NumberOfHands,
                    animalData.Size,
                    animalData.CanUseEquipment))
                .Returns(animalFeats);

            //Saves
            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = random.Next(20) + 1 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = random.Next(20) + 1 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = random.Next(20) + 1 };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(animalSpecialQualities.Union(animalFeats))),
                    baseCreature.Abilities))
                .Returns(animalSaves);

            //Speeds
            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = random.Next(100) + 1 };

            mockSpeedsGenerator
                .Setup(g => g.Generate(animal))
                .Returns(animalSpeeds);
        }

        private void SetUpRoll(HitDice hitDice, int roll)
        {
            if (!rollSequences.ContainsKey(hitDice.DefaultRoll))
                rollSequences[hitDice.DefaultRoll] = mockDice.SetupSequence(d => d.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsIndividualRolls<int>());

            rollSequences[hitDice.DefaultRoll] = rollSequences[hitDice.DefaultRoll].Returns(new[] { roll });
        }

        private void SetUpRoll(HitDice hitDice, double average)
        {
            if (!averageSequences.ContainsKey(hitDice.DefaultRoll))
                averageSequences[hitDice.DefaultRoll] = mockDice.SetupSequence(d => d.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsPotentialAverage());

            averageSequences[hitDice.DefaultRoll] = averageSequences[hitDice.DefaultRoll].Returns(average);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalHitPoints_WithConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 14;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * 2;
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}+{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(Math.Floor(baseAverage + 90210.42) + bonus),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(baseRoll + 9266 + 4),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalHitPoints_NegativeConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * -2;
            var roll = Math.Max(baseRoll - 2, 1) + 9266 - 2;
            var average = Math.Floor(baseAverage + 90210.42) + bonus;

            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(average),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(roll),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(BUG_HitPointTotals))]
        public void BUG_ApplyTo_AddAnimalHitPoints_NegativeConstitutionBonus(string template, string animal, int bQ, int bD, int bR, double bA, int aQ, int aD)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = bQ;
            baseCreature.HitPoints.HitDice[0].HitDie = bD;

            mockDice.Reset();
            rollSequences.Clear();
            averageSequences.Clear();

            baseRoll = bR;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = bA;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * -2;
            var roll = Math.Max(baseRoll - 2, 1) + 9266 - 2;
            var average = Math.Floor(baseAverage + 90210.42) + bonus;

            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(average),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(roll),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        private static IEnumerable BUG_HitPointTotals
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template.Template, template.Animal, 1, 4, 1, 3, 76, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 4, 2, 3, 76, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 4, 3, 3, 76, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 5, 1, 3.5, 4, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 5, 2, 3.5, 4, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 5, 3, 3.5, 4, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 9, 1, 5.5, 44, 4);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 9, 2, 5.5, 44, 4);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 9, 5, 5.5, 44, 4);
                    yield return new TestCaseData(template.Template, template.Animal, 1, 10, 6, 6, 7, 9);
                    yield return new TestCaseData(template.Template, template.Animal, 2, 6, 8, 8, 71, 10);
                    yield return new TestCaseData(template.Template, template.Animal, 8, 11, 52, 52, 8, 11);
                }
            }
        }

        [TestCaseSource(nameof(BUG_HitPointTotals))]
        public void BUG_ApplyTo_AddAnimalHitPoints_WithConstitutionBonus(string template, string animal, int bQ, int bD, int bR, double bA, int aQ, int aD)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = bQ;
            baseCreature.HitPoints.HitDice[0].HitDie = bD;

            mockDice.Reset();
            rollSequences.Clear();
            averageSequences.Clear();

            baseRoll = bR;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = bA;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 14;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * 2;
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}+{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(Math.Floor(baseAverage + 90210.42) + bonus),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(baseRoll + 9266 + 4),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(BUG_HitPointTotals))]
        public async Task BUG_ApplyToAsync_AddAnimalHitPoints_NegativeConstitutionBonus(string template, string animal, int bQ, int bD, int bR, double bA, int aQ, int aD)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = bQ;
            baseCreature.HitPoints.HitDice[0].HitDie = bD;

            mockDice.Reset();
            rollSequences.Clear();
            averageSequences.Clear();

            baseRoll = bR;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = bA;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * -2;
            var roll = Math.Max(baseRoll - 2, 1) + 9266 - 2;
            var average = Math.Floor(baseAverage + 90210.42) + bonus;

            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(average),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(roll),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(BUG_HitPointTotals))]
        public async Task BUG_ApplyToAsync_AddAnimalHitPoints_WithConstitutionBonus(string template, string animal, int bQ, int bD, int bR, double bA, int aQ, int aD)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = bQ;
            baseCreature.HitPoints.HitDice[0].HitDie = bD;

            mockDice.Reset();
            rollSequences.Clear();
            averageSequences.Clear();

            baseRoll = bR;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = bA;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 14;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * 2;
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}+{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(Math.Floor(baseAverage + 90210.42) + bonus),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(baseRoll + 9266 + 4),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Roll: {baseRoll}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalHitPoints_WithConditionalBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, animal))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 8245 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -666 },
                });

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}"));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(baseAverage + 90210.42)));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(baseRoll + 9266));
            Assert.That(creature.HitPoints.ConditionalBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.HitPoints.ConditionalBonuses.Single();
            Assert.That(bonus.Bonus, Is.EqualTo(8245 / 2 * (animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity)));
            Assert.That(bonus.Condition, Is.EqualTo("In Animal or Hybrid form"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalHitPoints_WithFeats(string template, string animal)
        {
            SetUpAnimal(animal);

            var regeneratedHitPoints = new HitPoints();
            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, animalFeats))
                .Returns(regeneratedHitPoints);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(regeneratedHitPoints));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSpeeds_AnimalFaster(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Climb)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Climb].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSpeeds_AnimalSlower(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 42 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Climb)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Negative.And.EqualTo(42 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Climb].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSpeeds_WithManeuverability_AsBonus(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 1337, Description = "Decent Maneuverability" };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("Decent Maneuverability"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses[0].Value, Is.Negative.And.EqualTo(42 - 1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSpeeds_WithManeuverability_AsNew(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("OK Maneuverability, In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 0);

            baseCreature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);

            //New for base and animal
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainBaseNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 9266);

            baseCreature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);

            //New for base
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 0);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //New for animal
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_ImproveNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 9266);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //Both new for base and from animal
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalBaseAttack(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttack = baseCreature.BaseAttackBonus;

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_RecomputeGrappleBonus(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttack = baseCreature.BaseAttackBonus;

            mockAttacksGenerator
                .Setup(g => g.GenerateGrappleBonus(
                    baseCreature.Name,
                    baseCreature.Size,
                    baseAttack + animalBaseAttack,
                    baseCreature.Abilities[AbilityConstants.Strength]))
                .Returns(1337);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
            Assert.That(creature.GrappleBonus, Is.EqualTo(1337));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalAttacks(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalAttacks_WithBonuses(string template, string animal)
        {
            SetUpAnimal(animal);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    animalAttacks,
                    It.Is<IEnumerable<Feat>>(fs =>
                        fs.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(animalSpecialQualities))),
                    baseCreature.Abilities))
                .Returns(animalAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public void ApplyTo_AddLycanthropeAttacks_BaseIsBigger(string template, string animal, string smallerSize, string biggerSize)
        {
            baseCreature.Size = biggerSize;

            SetUpAnimal(animal, size: smallerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        private static IEnumerable SizeComparisons
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                foreach (var template in templates)
                {
                    for (var i = 1; i < sizes.Length; i++)
                    {
                        yield return new TestCaseData(template.Template, template.Animal, sizes[i - 1], sizes[i]);
                    }
                }
            }
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public void ApplyTo_AddLycanthropeAttacks_AnimalIsBigger(string template, string animal, string smallerSize, string biggerSize)
        {
            baseCreature.Size = smallerSize;

            SetUpAnimal(animal, size: biggerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(Sizes))]
        public void ApplyTo_AddLycanthropeAttacks_AnimalAndBaseAreSameSize(string template, string animal, string size)
        {
            baseCreature.Size = size;

            SetUpAnimal(animal, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        private static IEnumerable Sizes
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                foreach (var template in templates)
                {
                    foreach (var size in sizes)
                    {
                        yield return new TestCaseData(template.Template, template.Animal, size);
                    }
                }
            }
        }

        [TestCaseSource(nameof(Sizes))]
        public void ApplyTo_AddLycanthropeAttacks_WithBonuses(string template, string animal, string size)
        {
            baseCreature.Size = size;

            SetUpAnimal(animal, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    lycanthropeAttacks,
                    It.Is<IEnumerable<Feat>>(fs =>
                        fs.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(animalSpecialQualities))),
                    baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalAttacks_WithLycanthropy(string template, string animal)
        {
            SetUpAnimal(animal);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
                new Attack { Name = $"{animalAttacks[1].Name} (in Hybrid form)", DamageEffect = "my damage effect" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks)
                .And.SupersetOf(lycanthropeAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[0].DamageEffect, Is.Empty);
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
            Assert.That(animalAttacks[1].DamageEffect, Is.EqualTo("my damage effect"));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[0].DamageEffect, Is.Empty);
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
            Assert.That(lycanthropeAttacks[1].DamageEffect, Is.Empty);
            Assert.That(lycanthropeAttacks[2].Name, Is.EqualTo("animal attack 2 (in Hybrid form)"));
            Assert.That(lycanthropeAttacks[2].DamageEffect, Is.EqualTo("my damage effect"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_ModifyBaseCreatureAttacks_Humanoid(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Humanoid form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Humanoid form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_ModifyBaseCreatureAttacks_Giant(string template, string animal)
        {
            SetUpAnimal(animal);

            baseCreature.Type.Name = CreatureConstants.Types.Giant;

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Giant form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Giant form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalSpecialQualities(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(animalSpecialQualities));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalSpecialQualities_RemoveDuplicates(string template, string animal)
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { baseSpecialQuality });

            SetUpAnimal(animal);

            animalSpecialQualities.Add(new Feat { Name = "my special quality" });

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(animalSpecialQualities[0])
                .And.Contains(animalSpecialQualities[1])
                .And.Not.Contains(animalSpecialQualities[2])
                .And.Contains(baseSpecialQuality));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddLycanthropeSpecialQualities(string template, string animal)
        {
            SetUpAnimal(animal);

            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lycanthropeSpecialQualities));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddLycanthropeSpecialQualities_RemoveDuplicates(string template, string animal)
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { baseSpecialQuality });

            SetUpAnimal(animal);

            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "my special quality" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(lycanthropeSpecialQualities[0])
                .And.Contains(lycanthropeSpecialQualities[2])
                .And.Not.Contains(lycanthropeSpecialQualities[1])
                .And.Contains(baseSpecialQuality));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalSaveBonuses(string template, string animal)
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal(animal);

            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = 600 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = 1337 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = 42 };

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(1336 + 600));
            Assert.That(creature.Saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(96 + 1337));
            Assert.That(creature.Saves[SaveConstants.Will].BaseValue, Is.EqualTo(783 + 42));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_WisdomIncreasesBy2(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_ConditionalBonusesForHybridAndAnimalForms_FromAnimalBonuses(string template, string animal)
        {
            SetUpAnimal(animal);

            var animalAbilityAdjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 666 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, animal))
                .Returns(animalAbilityAdjustments);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].Value, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].Value, Is.EqualTo(9266));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].Value, Is.EqualTo(90210));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSkills(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalSkills_CombineWithBaseCreatureSkills(string template, string animal)
        {
            var isAfflicted = template.Contains("Afflicted");

            var baseSkills = new[]
            {
                new Skill("skill 1", baseCreature.Abilities[AbilityConstants.Strength], 666) { ClassSkill = true, Ranks = 1 },
                new Skill("untrained skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 666) { ClassSkill = false, Ranks = 2 },
                new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], 666) { ClassSkill = true, Ranks = 3 },
                new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 4 },
                new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 5 },
            };
            baseCreature.Skills = baseSkills;

            SetUpAnimal(animal, hitDiceQuantity: 11);

            animalSkills.Add(new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], 666) { ClassSkill = true, Ranks = 6 });
            animalSkills.Add(new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 7 });
            animalSkills.Add(new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Intelligence], 666) { ClassSkill = true, Ranks = 8 });

            var newCap = baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity + 3;

            if (isAfflicted)
            {
                var rankedSkills = new[]
                {
                    new Skill(
                        "animal skill 1",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 10 },
                    new Skill(
                        "animal skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 11 },
                    new Skill(
                        "skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 6 },
                    new Skill(
                        "untrained skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = false, Ranks = 7 },
                    new Skill(
                        "animal skill 3",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 8 },
                    new Skill(
                        SkillConstants.Special.ControlShape,
                        baseCreature.Abilities[AbilityConstants.Wisdom],
                        newCap)
                    { ClassSkill = true, Ranks = 9 },
                };

                mockSkillsGenerator
                    .Setup(g => g.ApplySkillPointsAsRanks(
                        It.Is<IEnumerable<Skill>>(ss => ss.All(s => s.Ranks == 0)
                            && ss.Any(s => s.Name == SkillConstants.Special.ControlShape
                                && s.BaseAbility == baseCreature.Abilities[AbilityConstants.Wisdom]
                                && s.ClassSkill
                                && s.RankCap == animalHitPoints.RoundedHitDiceQuantity)),
                        animalHitPoints,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        baseCreature.Abilities,
                        false))
                    .Returns(rankedSkills);

                mockFeatsGenerator
                    .Setup(g => g.GenerateSpecialQualities(
                        animal,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        animalHitPoints,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalData.CanUseEquipment,
                        animalData.Size,
                        baseCreature.Alignment))
                    .Returns(animalSpecialQualities);

                mockFeatsGenerator
                    .Setup(g => g.GenerateFeats(
                        animalHitPoints,
                        animalBaseAttack,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalAttacks,
                        animalSpecialQualities,
                        animalData.CasterLevel,
                        baseCreature.Speeds,
                        animalData.NaturalArmor,
                        animalData.NumberOfHands,
                        animalData.Size,
                        animalData.CanUseEquipment))
                    .Returns(animalFeats);
            }

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(baseSkills));

            var skills = creature.Skills.ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.EqualTo(1));
            Assert.That(skills[0].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[2].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.EqualTo(3 + 6));
            Assert.That(skills[2].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.EqualTo(4 + 7));
            Assert.That(skills[3].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[4].Name, Is.EqualTo("animal skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(5 + 8));
            Assert.That(skills[4].RankCap, Is.EqualTo(newCap));

            if (isAfflicted)
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(10));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(11));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Special.ControlShape));
                Assert.That(skills[7].ClassSkill, Is.True);
                Assert.That(skills[7].Ranks, Is.EqualTo(9));
                Assert.That(skills[7].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(8));
            }
            else
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(animalSkills[0].Ranks));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(animalSkills[1].Ranks));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(7));
            }
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void BUG_ApplyTo_GainAnimalSkills_CombineWithBaseCreatureSkills_TooManyRanks(string template, string animal)
        {
            var isAfflicted = template.Contains("Afflicted");

            var oldCap = baseCreature.HitPoints.HitDice[0].RoundedQuantity + 3;
            var baseSkills = new[]
            {
                new Skill("skill 1", baseCreature.Abilities[AbilityConstants.Strength], oldCap) { ClassSkill = true, Ranks = oldCap - 4 },
                new Skill("untrained skill 1", baseCreature.Abilities[AbilityConstants.Constitution], oldCap) { ClassSkill = false, Ranks = oldCap - 3 },
                new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], oldCap) { ClassSkill = true, Ranks = oldCap - 2 },
                new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], oldCap) { ClassSkill = false, Ranks = oldCap - 1 },
                new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Wisdom], oldCap) { ClassSkill = false, Ranks = oldCap },
            };
            baseCreature.Skills = baseSkills;

            SetUpAnimal(animal, hitDiceQuantity: 11);

            var animalCap = animalHitPoints.HitDice[0].RoundedQuantity;
            animalSkills.Add(new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], animalCap) { ClassSkill = true, Ranks = animalCap - 2 });
            animalSkills.Add(new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], animalCap) { ClassSkill = false, Ranks = animalCap - 1 });
            animalSkills.Add(new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Intelligence], animalCap) { ClassSkill = true, Ranks = animalCap });

            var newCap = oldCap + animalHitPoints.HitDice[0].RoundedQuantity;

            if (isAfflicted)
            {
                var rankedSkills = new[]
                {
                    new Skill(
                        "animal skill 1",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 10 },
                    new Skill(
                        "animal skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 11 },
                    new Skill(
                        "skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = animalCap - 2 },
                    new Skill(
                        "untrained skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = false, Ranks = animalCap - 1 },
                    new Skill(
                        "animal skill 3",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = animalCap },
                    new Skill(
                        SkillConstants.Special.ControlShape,
                        baseCreature.Abilities[AbilityConstants.Wisdom],
                        newCap)
                    { ClassSkill = true, Ranks = 9 },
                };

                mockSkillsGenerator
                    .Setup(g => g.ApplySkillPointsAsRanks(
                        It.Is<IEnumerable<Skill>>(ss => ss.All(s => s.Ranks == 0)
                            && ss.Any(s => s.Name == SkillConstants.Special.ControlShape
                                && s.BaseAbility == baseCreature.Abilities[AbilityConstants.Wisdom]
                                && s.ClassSkill
                                && s.RankCap == animalHitPoints.RoundedHitDiceQuantity)),
                        animalHitPoints,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        baseCreature.Abilities,
                        false))
                    .Returns(rankedSkills);

                mockFeatsGenerator
                    .Setup(g => g.GenerateSpecialQualities(
                        animal,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        animalHitPoints,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalData.CanUseEquipment,
                        animalData.Size,
                        baseCreature.Alignment))
                    .Returns(animalSpecialQualities);

                mockFeatsGenerator
                    .Setup(g => g.GenerateFeats(
                        animalHitPoints,
                        animalBaseAttack,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalAttacks,
                        animalSpecialQualities,
                        animalData.CasterLevel,
                        baseCreature.Speeds,
                        animalData.NaturalArmor,
                        animalData.NumberOfHands,
                        animalData.Size,
                        animalData.CanUseEquipment))
                    .Returns(animalFeats);
            }

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(baseSkills));

            var skills = creature.Skills.ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.EqualTo(oldCap - 4));
            Assert.That(skills[0].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(oldCap - 3));
            Assert.That(skills[1].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[2].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.EqualTo(oldCap - 2 + animalCap - 2));
            Assert.That(skills[2].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.EqualTo(oldCap - 1 + animalCap - 1));
            Assert.That(skills[3].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[4].Name, Is.EqualTo("animal skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(oldCap + animalCap));
            Assert.That(skills[4].RankCap, Is.EqualTo(newCap));

            if (isAfflicted)
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(10));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(11));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Special.ControlShape));
                Assert.That(skills[7].ClassSkill, Is.True);
                Assert.That(skills[7].Ranks, Is.EqualTo(9));
                Assert.That(skills[7].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(8));
            }
            else
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(animalSkills[0].Ranks));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(animalSkills[1].Ranks));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(7));
            }
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainControlShapeSkill_Afflicted(string template, string animal)
        {
            if (!template.Contains("Afflicted"))
            {
                Assert.Pass($"{template} is not an Afflicted lycanthrope template");
            }

            SetUpAnimal(animal);

            var rankedSkills = new[]
            {
                new Skill("ranked skill 1", baseCreature.Abilities[AbilityConstants.Strength], int.MaxValue) { ClassSkill = true, Ranks = 1337 },
                new Skill("ranked skill 2", baseCreature.Abilities[AbilityConstants.Strength], int.MaxValue) { ClassSkill = true, Ranks = 1336 },
                new Skill(SkillConstants.Special.ControlShape, baseCreature.Abilities[AbilityConstants.Wisdom], int.MaxValue) { ClassSkill = true, Ranks = 96 },
            };

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss => ss.All(s => s.Ranks == 0)
                        && ss.Any(s => s.Name == SkillConstants.Special.ControlShape
                            && s.BaseAbility == baseCreature.Abilities[AbilityConstants.Wisdom]
                            && s.ClassSkill
                            && s.RankCap == animalHitPoints.RoundedHitDiceQuantity)),
                    animalHitPoints,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    false))
                .Returns(rankedSkills);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    baseCreature.Abilities,
                    rankedSkills,
                    animalData.CanUseEquipment,
                    animalData.Size,
                    baseCreature.Alignment))
                .Returns(animalSpecialQualities);

            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    animalHitPoints,
                    animalBaseAttack,
                    baseCreature.Abilities,
                    rankedSkills,
                    animalAttacks,
                    animalSpecialQualities,
                    animalData.CasterLevel,
                    baseCreature.Speeds,
                    animalData.NaturalArmor,
                    animalData.NumberOfHands,
                    animalData.Size,
                    animalData.CanUseEquipment))
                .Returns(animalFeats);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(rankedSkills));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_DoNotGainControlShapeSkill_Natural(string template, string animal)
        {
            if (!template.Contains("Natural"))
            {
                Assert.Pass($"{template} is not a Natural lycanthrope template");
            }

            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));

            var skillNames = creature.Skills.Select(s => s.Name);
            Assert.That(skillNames, Does.Not.Contain(SkillConstants.Special.ControlShape));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalFeats(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.SupersetOf(animalFeats));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalFeats_RemoveDuplicates(string template, string animal)
        {
            baseCreature.Feats = baseCreature.Feats.Union(new[] { new Feat { Name = "my feat" } });

            SetUpAnimal(animal);

            animalFeats.Add(new Feat { Name = "my feat" });

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(animalFeats[0])
                .And.Contains(animalFeats[1])
                .And.Not.Contains(animalFeats[2]));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_GainAnimalFeats_KeepDuplicates_IfCanBeTakenMultipleTimes(string template, string animal)
        {
            var creatureFeat = new Feat { Name = "my feat", CanBeTakenMultipleTimes = true };
            baseCreature.Feats = baseCreature.Feats.Union(new[] { creatureFeat });

            SetUpAnimal(animal);

            animalFeats.Add(new Feat { Name = "my feat", CanBeTakenMultipleTimes = true });

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(creatureFeat)
                .And.SupersetOf(animalFeats));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_AddAnimalSaveBonuses_WithBonusesFromNewFeats(string template, string animal)
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal(animal);

            var animalSaves = new Dictionary<string, Save>();
            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = 600 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = 1337 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = 42 };

            animalSaves[SaveConstants.Fortitude].AddBonus(9266);
            animalSaves[SaveConstants.Fortitude].AddBonus(1234, "with a condition");
            animalSaves[SaveConstants.Reflex].AddBonus(90210);
            animalSaves[SaveConstants.Reflex].AddBonus(2345, "with another condition");
            animalSaves[SaveConstants.Will].AddBonus(8245);
            animalSaves[SaveConstants.Will].AddBonus(3456, "with yet another condition");

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(animalSpecialQualities.Union(animalFeats))),
                    baseCreature.Abilities))
                .Returns(animalSaves);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(1336 + 600));
            Assert.That(creature.Saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(9266));
            Assert.That(creature.Saves[SaveConstants.Fortitude].IsConditional, Is.True);
            Assert.That(creature.Saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(96 + 1337));
            Assert.That(creature.Saves[SaveConstants.Reflex].Bonus, Is.EqualTo(90210));
            Assert.That(creature.Saves[SaveConstants.Reflex].IsConditional, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].BaseValue, Is.EqualTo(783 + 42));
            Assert.That(creature.Saves[SaveConstants.Will].Bonus, Is.EqualTo(8245));
            Assert.That(creature.Saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void ApplyTo_IncreaseChallengeRating(string template, string animal, string originalChallengeRating, int animalHitDiceQuantity, string updatedChallengeRating)
        {
            baseCreature.ChallengeRating = originalChallengeRating;

            SetUpAnimal(animal, hitDiceQuantity: animalHitDiceQuantity);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(updatedChallengeRating));
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        private static IEnumerable ChallengeRatings
        {
            get
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                var animalHitDiceQuantities = Enumerable.Range(1, 30);

                foreach (var template in templates)
                {
                    foreach (var animalQuantity in animalHitDiceQuantities)
                    {
                        var increase = 0;

                        if (animalQuantity <= 2)
                            increase = 2;
                        else if (animalQuantity <= 5)
                            increase = 3;
                        else if (animalQuantity <= 10)
                            increase = 4;
                        else if (animalQuantity <= 20)
                            increase = 5;
                        else if (animalQuantity > 20)
                            increase = 6;

                        foreach (var cr in challengeRatings)
                        {
                            var newCr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase);
                            yield return new TestCaseData(template.Template, template.Animal, cr, animalQuantity, newCr);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(LevelAdjustments))]
        public void ApplyTo_IncreaseLevelAdjustment(string template, string animal, int? oldLevelAdjustment, int? newLevelAdjustment)
        {
            baseCreature.LevelAdjustment = oldLevelAdjustment;

            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(newLevelAdjustment));
        }

        //Afflicted, +2
        //Natural, +3
        private static IEnumerable LevelAdjustments
        {
            get
            {
                var levelAdjustments = new int?[]
                {
                    null,
                    0,
                    1,
                    2,
                    10,
                    100,
                };

                foreach (var template in templates)
                {
                    var increase = 2;
                    if (template.Template.Contains("Natural"))
                        increase = 3;

                    foreach (var levelAdjustment in levelAdjustments)
                    {
                        if (levelAdjustment == null)
                            yield return new TestCaseData(template.Template, template.Animal, levelAdjustment, levelAdjustment);
                        else
                            yield return new TestCaseData(template.Template, template.Animal, levelAdjustment, levelAdjustment + increase);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainShapechangerSubtype(string template, string animal)
        {
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Shapechanger));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalHitPoints_NoConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}"));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(baseAverage + 90210.42)));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(baseRoll + 9266));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 14;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * 2;
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}+{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(Math.Floor(baseAverage + 90210.42) + bonus),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(baseRoll + 9266 + 4),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalHitPoints_NegativeConstitutionBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));

            var bonus = (animalHitPoints.HitDice[0].Quantity + creature.HitPoints.HitDice[0].RoundedQuantity) * -2;
            var roll = Math.Max(baseRoll - 2, 1) + 9266 - 2;
            var average = Math.Floor(baseAverage + 90210.42) + bonus;

            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}{bonus}"));
            Assert.That(
                creature.HitPoints.DefaultTotal,
                Is.EqualTo(average),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(
                creature.HitPoints.Total,
                Is.EqualTo(roll),
                $"Base roll: {creature.HitPoints.HitDice[0].DefaultRoll}; Base Average: {baseAverage}; Animal Roll: {animalHitPoints.HitDice[0].DefaultRoll}, Bonus: {bonus}");
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithConditionalBonus(string template, string animal)
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal(animal, roll: 9266, average: 90210.42);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, animal))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 8245 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -666 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -666 },
                });

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(2)
                .And.Contains(animalHitPoints.HitDice[0]));
            Assert.That(creature.HitPoints.Constitution, Is.EqualTo(baseCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(creature.HitPoints.DefaultRoll, Is.EqualTo($"{creature.HitPoints.HitDice[0].DefaultRoll}+{animalHitPoints.HitDice[0].DefaultRoll}"));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(baseAverage + 90210.42)));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].Quantity));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(baseRoll + 9266));
            Assert.That(creature.HitPoints.ConditionalBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.HitPoints.ConditionalBonuses.Single();
            Assert.That(bonus.Bonus, Is.EqualTo(8245 / 2 * (animalHitPoints.HitDice[0].Quantity + baseCreature.HitPoints.HitDice[0].RoundedQuantity)));
            Assert.That(bonus.Condition, Is.EqualTo("In Animal or Hybrid form"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithFeats(string template, string animal)
        {
            SetUpAnimal(animal);

            var regeneratedHitPoints = new HitPoints();
            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, animalFeats))
                .Returns(regeneratedHitPoints);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(regeneratedHitPoints));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSpeeds_AnimalFaster(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Climb)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Climb].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSpeeds_AnimalSlower(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 42 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Climb)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Negative.And.EqualTo(42 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Climb].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Climb].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSpeeds_WithManeuverability_AsBonus(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 1337, Description = "Decent Maneuverability" };

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("Decent Maneuverability"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses[0].Value, Is.Negative.And.EqualTo(42 - 1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSpeeds_WithManeuverability_AsNew(string template, string animal)
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;

            SetUpAnimal(animal);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Has.Count.EqualTo(3)
                .And.ContainKey(SpeedConstants.Land)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Burrow));

            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Value, Is.Positive.And.EqualTo(9266 - 600));
            Assert.That(creature.Speeds[SpeedConstants.Land].Bonuses[0].Condition, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("OK Maneuverability, In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Bonuses, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Value, Is.EqualTo(90210));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Description, Is.EqualTo("In Animal Form"));
            Assert.That(creature.Speeds[SpeedConstants.Burrow].Bonuses, Is.Empty);
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 0);

            baseCreature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);

            //New for base and animal
            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainBaseNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 9266);

            baseCreature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);

            //New for base
            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 0);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //New for animal
            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_ImproveNaturalArmor(string template, string animal)
        {
            SetUpAnimal(animal, 9266);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //Both new for base and from animal
            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalBaseAttack(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttack = baseCreature.BaseAttackBonus;

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_RecomputeGrappleBonus(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttack = baseCreature.BaseAttackBonus;

            mockAttacksGenerator
                .Setup(g => g.GenerateGrappleBonus(
                    baseCreature.Name,
                    baseCreature.Size,
                    baseAttack + animalBaseAttack,
                    baseCreature.Abilities[AbilityConstants.Strength]))
                .Returns(1337);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
            Assert.That(creature.GrappleBonus, Is.EqualTo(1337));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalAttacks(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalAttacks_WithBonuses(string template, string animal)
        {
            SetUpAnimal(animal);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    animalAttacks,
                    It.Is<IEnumerable<Feat>>(fs =>
                        fs.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(animalSpecialQualities))),
                    baseCreature.Abilities))
                .Returns(animalAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_BaseIsBigger(string template, string animal, string smallerSize, string biggerSize)
        {
            baseCreature.Size = biggerSize;

            SetUpAnimal(animal, size: smallerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_AnimalIsBigger(string template, string animal, string smallerSize, string biggerSize)
        {
            baseCreature.Size = smallerSize;

            SetUpAnimal(animal, size: biggerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(Sizes))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_AnimalAndBaseAreSameSize(string template, string animal, string size)
        {
            baseCreature.Size = size;

            SetUpAnimal(animal, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(Sizes))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_WithBonuses(string template, string animal, string size)
        {
            baseCreature.Size = size;

            SetUpAnimal(animal, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    lycanthropeAttacks,
                    It.Is<IEnumerable<Feat>>(fs =>
                        fs.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(animalSpecialQualities))),
                    baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalAttacks_WithLycanthropy(string template, string animal)
        {
            SetUpAnimal(animal);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
                new Attack { Name = $"{animalAttacks[1].Name} (in Hybrid form)", DamageEffect = "my damage effect" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks)
                .And.SupersetOf(lycanthropeAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[0].DamageEffect, Is.Empty);
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
            Assert.That(animalAttacks[1].DamageEffect, Is.EqualTo("my damage effect"));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[0].DamageEffect, Is.Empty);
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
            Assert.That(lycanthropeAttacks[1].DamageEffect, Is.Empty);
            Assert.That(lycanthropeAttacks[2].Name, Is.EqualTo("animal attack 2 (in Hybrid form)"));
            Assert.That(lycanthropeAttacks[2].DamageEffect, Is.EqualTo("my damage effect"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_ModifyBaseCreatureAttacks_Humanoid(string template, string animal)
        {
            SetUpAnimal(animal);

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Humanoid form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Humanoid form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_ModifyBaseCreatureAttacks_Giant(string template, string animal)
        {
            SetUpAnimal(animal);

            baseCreature.Type.Name = CreatureConstants.Types.Giant;

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Giant form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Giant form)"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalSpecialQualities(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(animalSpecialQualities));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalSpecialQualities_RemoveDuplicates(string template, string animal)
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { baseSpecialQuality });

            SetUpAnimal(animal);

            animalSpecialQualities.Add(new Feat { Name = "my special quality" });

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(animalSpecialQualities[0])
                .And.Contains(animalSpecialQualities[1])
                .And.Not.Contains(animalSpecialQualities[2])
                .And.Contains(baseSpecialQuality));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddLycanthropeSpecialQualities(string template, string animal)
        {
            SetUpAnimal(animal);

            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lycanthropeSpecialQualities));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddLycanthropeSpecialQualities_RemoveDuplicates(string template, string animal)
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union(new[] { baseSpecialQuality });

            SetUpAnimal(animal);

            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "my special quality" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(lycanthropeSpecialQualities[0])
                .And.Contains(lycanthropeSpecialQualities[2])
                .And.Not.Contains(lycanthropeSpecialQualities[1])
                .And.Contains(baseSpecialQuality));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalSaveBonuses(string template, string animal)
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal(animal);

            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = 600 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = 1337 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = 42 };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(1336 + 600));
            Assert.That(creature.Saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(96 + 1337));
            Assert.That(creature.Saves[SaveConstants.Will].BaseValue, Is.EqualTo(783 + 42));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_WisdomIncreasesBy2(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_ConditionalBonusesForHybridAndAnimalForms_FromAnimalBonuses(string template, string animal)
        {
            SetUpAnimal(animal);

            var animalAbilityAdjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 666 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, animal))
                .Returns(animalAbilityAdjustments);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].Bonuses, Is.Empty);
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].Value, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Strength].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].Value, Is.EqualTo(9266));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses, Has.Count.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].Value, Is.EqualTo(90210));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].IsConditional, Is.True);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSkills(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalSkills_CombineWithBaseCreatureSkills(string template, string animal)
        {
            var isAfflicted = template.Contains("Afflicted");

            var baseSkills = new[]
            {
                new Skill("skill 1", baseCreature.Abilities[AbilityConstants.Strength], 666) { ClassSkill = true, Ranks = 1 },
                new Skill("untrained skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 666) { ClassSkill = false, Ranks = 2 },
                new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], 666) { ClassSkill = true, Ranks = 3 },
                new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 4 },
                new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 5 },
            };
            baseCreature.Skills = baseSkills;

            SetUpAnimal(animal, hitDiceQuantity: 11);

            animalSkills.Add(new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], 666) { ClassSkill = true, Ranks = 6 });
            animalSkills.Add(new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 7 });
            animalSkills.Add(new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Intelligence], 666) { ClassSkill = true, Ranks = 8 });

            var newCap = baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity + 3;

            if (isAfflicted)
            {
                var rankedSkills = new[]
                {
                    new Skill(
                        "animal skill 1",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 10 },
                    new Skill(
                        "animal skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 11 },
                    new Skill(
                        "skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 6 },
                    new Skill(
                        "untrained skill 2",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = false, Ranks = 7 },
                    new Skill(
                        "animal skill 3",
                        baseCreature.Abilities[AbilityConstants.Strength],
                        newCap)
                    { ClassSkill = true, Ranks = 8 },
                    new Skill(
                        SkillConstants.Special.ControlShape,
                        baseCreature.Abilities[AbilityConstants.Wisdom],
                        newCap)
                    { ClassSkill = true, Ranks = 9 },
                };

                mockSkillsGenerator
                    .Setup(g => g.ApplySkillPointsAsRanks(
                        It.Is<IEnumerable<Skill>>(ss => ss.All(s => s.Ranks == 0)
                            && ss.Any(s => s.Name == SkillConstants.Special.ControlShape
                                && s.BaseAbility == baseCreature.Abilities[AbilityConstants.Wisdom]
                                && s.ClassSkill
                                && s.RankCap == 11)),
                        animalHitPoints,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        baseCreature.Abilities,
                        false))
                    .Returns(rankedSkills);

                mockFeatsGenerator
                    .Setup(g => g.GenerateSpecialQualities(
                        animal,
                        It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                        animalHitPoints,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalData.CanUseEquipment,
                        animalData.Size,
                        baseCreature.Alignment))
                    .Returns(animalSpecialQualities);

                mockFeatsGenerator
                    .Setup(g => g.GenerateFeats(
                        animalHitPoints,
                        animalBaseAttack,
                        baseCreature.Abilities,
                        rankedSkills,
                        animalAttacks,
                        animalSpecialQualities,
                        animalData.CasterLevel,
                        baseCreature.Speeds,
                        animalData.NaturalArmor,
                        animalData.NumberOfHands,
                        animalData.Size,
                        animalData.CanUseEquipment))
                    .Returns(animalFeats);
            }

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(baseSkills));

            var skills = creature.Skills.ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.EqualTo(1));
            Assert.That(skills[0].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[2].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.EqualTo(3 + 6));
            Assert.That(skills[2].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.EqualTo(4 + 7));
            Assert.That(skills[3].RankCap, Is.EqualTo(newCap));
            Assert.That(skills[4].Name, Is.EqualTo("animal skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(5 + 8));
            Assert.That(skills[4].RankCap, Is.EqualTo(newCap));

            if (isAfflicted)
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(10));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(11));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Special.ControlShape));
                Assert.That(skills[7].ClassSkill, Is.True);
                Assert.That(skills[7].Ranks, Is.EqualTo(9));
                Assert.That(skills[7].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(8));
            }
            else
            {
                Assert.That(skills[5].Name, Is.EqualTo("animal skill 1"));
                Assert.That(skills[5].ClassSkill, Is.True);
                Assert.That(skills[5].Ranks, Is.EqualTo(animalSkills[0].Ranks));
                Assert.That(skills[5].RankCap, Is.EqualTo(newCap));
                Assert.That(skills[6].Name, Is.EqualTo("animal skill 2"));
                Assert.That(skills[6].ClassSkill, Is.True);
                Assert.That(skills[6].Ranks, Is.EqualTo(animalSkills[1].Ranks));
                Assert.That(skills[6].RankCap, Is.EqualTo(newCap));
                Assert.That(skills, Has.Length.EqualTo(7));
            }
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainControlShapeSkill_Afflicted(string template, string animal)
        {
            if (!template.Contains("Afflicted"))
            {
                Assert.Pass($"{template} is not an Afflicted lycanthrope template");
            }

            SetUpAnimal(animal);

            var rankedSkills = new[]
            {
                new Skill("ranked skill 1", baseCreature.Abilities[AbilityConstants.Strength], int.MaxValue) { ClassSkill = true, Ranks = 1337 },
                new Skill("ranked skill 2", baseCreature.Abilities[AbilityConstants.Strength], int.MaxValue) { ClassSkill = true, Ranks = 1336 },
                new Skill(SkillConstants.Special.ControlShape, baseCreature.Abilities[AbilityConstants.Wisdom], int.MaxValue) { ClassSkill = true, Ranks = 96 },
            };

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss => ss.All(s => s.Ranks == 0)
                        && ss.Any(s => s.Name == SkillConstants.Special.ControlShape
                            && s.BaseAbility == baseCreature.Abilities[AbilityConstants.Wisdom]
                            && s.ClassSkill
                            && s.RankCap == animalHitPoints.RoundedHitDiceQuantity)),
                    animalHitPoints,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    false))
                .Returns(rankedSkills);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    baseCreature.Abilities,
                    rankedSkills,
                    animalData.CanUseEquipment,
                    animalData.Size,
                    baseCreature.Alignment))
                .Returns(animalSpecialQualities);

            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    animalHitPoints,
                    animalBaseAttack,
                    baseCreature.Abilities,
                    rankedSkills,
                    animalAttacks,
                    animalSpecialQualities,
                    animalData.CasterLevel,
                    baseCreature.Speeds,
                    animalData.NaturalArmor,
                    animalData.NumberOfHands,
                    animalData.Size,
                    animalData.CanUseEquipment))
                .Returns(animalFeats);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(rankedSkills));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_DoNotGainControlShapeSkill_Natural(string template, string animal)
        {
            if (!template.Contains("Natural"))
            {
                Assert.Pass($"{template} is not a Natural lycanthrope template");
            }

            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));

            var skillNames = creature.Skills.Select(s => s.Name);
            Assert.That(skillNames, Does.Not.Contain(SkillConstants.Special.ControlShape));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalFeats(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.SupersetOf(animalFeats));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalFeats_RemoveDuplicates(string template, string animal)
        {
            baseCreature.Feats = baseCreature.Feats.Union(new[] { new Feat { Name = "my feat" } });

            SetUpAnimal(animal);

            animalFeats.Add(new Feat { Name = "my feat" });

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(animalFeats[0])
                .And.Contains(animalFeats[1])
                .And.Not.Contains(animalFeats[2]));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_GainAnimalFeats_KeepDuplicates_IfCanBeTakenMultipleTimes(string template, string animal)
        {
            var creatureFeat = new Feat { Name = "my feat", CanBeTakenMultipleTimes = true };
            baseCreature.Feats = baseCreature.Feats.Union(new[] { creatureFeat });

            SetUpAnimal(animal);

            animalFeats.Add(new Feat { Name = "my feat", CanBeTakenMultipleTimes = true });

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(creatureFeat)
                .And.SupersetOf(animalFeats));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_AddAnimalSaveBonuses_WithBonusesFromNewFeats(string template, string animal)
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal(animal);

            var animalSaves = new Dictionary<string, Save>();
            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = 600 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = 1337 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = 42 };

            animalSaves[SaveConstants.Fortitude].AddBonus(9266);
            animalSaves[SaveConstants.Fortitude].AddBonus(1234, "with a condition");
            animalSaves[SaveConstants.Reflex].AddBonus(90210);
            animalSaves[SaveConstants.Reflex].AddBonus(2345, "with another condition");
            animalSaves[SaveConstants.Will].AddBonus(8245);
            animalSaves[SaveConstants.Will].AddBonus(3456, "with yet another condition");

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(animalSpecialQualities.Union(animalFeats))),
                    baseCreature.Abilities))
                .Returns(animalSaves);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(1336 + 600));
            Assert.That(creature.Saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(9266));
            Assert.That(creature.Saves[SaveConstants.Fortitude].IsConditional, Is.True);
            Assert.That(creature.Saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(96 + 1337));
            Assert.That(creature.Saves[SaveConstants.Reflex].Bonus, Is.EqualTo(90210));
            Assert.That(creature.Saves[SaveConstants.Reflex].IsConditional, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].BaseValue, Is.EqualTo(783 + 42));
            Assert.That(creature.Saves[SaveConstants.Will].Bonus, Is.EqualTo(8245));
            Assert.That(creature.Saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public async Task ApplyToAsync_IncreaseChallengeRating(string template, string animal, string originalChallengeRating, int animalHitDiceQuantity, string updatedChallengeRating)
        {
            baseCreature.ChallengeRating = originalChallengeRating;

            SetUpAnimal(animal, hitDiceQuantity: animalHitDiceQuantity);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(updatedChallengeRating));
        }

        [TestCaseSource(nameof(LevelAdjustments))]
        public async Task ApplyToAsync_IncreaseLevelAdjustment(string template, string animal, int? oldLevelAdjustment, int? newLevelAdjustment)
        {
            baseCreature.LevelAdjustment = oldLevelAdjustment;

            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(newLevelAdjustment));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public void ApplyTo_SetsTemplate(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(AllLycanthropeTemplates))]
        public async Task ApplyToAsync_SetsTemplate(string template, string animal)
        {
            SetUpAnimal(animal);

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(template));
        }
    }
}
