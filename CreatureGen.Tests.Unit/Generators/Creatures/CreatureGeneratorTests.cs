using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Creatures;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using CreatureGen.Templates;
using CreatureGen.Verifiers;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests
    {
        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICreatureVerifier> mockCreatureVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private ICreatureGenerator creatureGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<IAttackSelector> mockAttackSelector;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;

        private Dictionary<string, Ability> abilities;
        private List<Skill> skills;
        private List<Feat> specialQualities;
        private List<Feat> feats;
        private CreatureDataSelection creatureData;
        private HitPoints hitPoints;
        private Dictionary<int, Mock<PartialRoll>> mockPartialRolls;
        private List<string> types;
        private List<Attack> attacks;

        [SetUp]
        public void Setup()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCreatureVerifier = new Mock<ICreatureVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockArmorClassGenerator = new Mock<IArmorClassGenerator>();
            mockAttackSelector = new Mock<IAttackSelector>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();

            creatureGenerator = new CreatureGenerator(
                mockAlignmentGenerator.Object,
                mockAdjustmentsSelector.Object,
                mockCreatureVerifier.Object,
                mockPercentileSelector.Object,
                mockCollectionSelector.Object,
                mockAbilitiesGenerator.Object,
                mockSkillsGenerator.Object,
                mockFeatsGenerator.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockArmorClassGenerator.Object,
                mockAttackSelector.Object,
                mockSavesGenerator.Object,
                mockTypeAndAmountSelector.Object,
                mockDice.Object,
                mockJustInTimeFactory.Object);

            feats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            creatureData = new CreatureDataSelection();
            hitPoints = new HitPoints();
            mockPartialRolls = new Dictionary<int, Mock<PartialRoll>>();
            types = new List<string>();
            specialQualities = new List<Feat>();
            attacks = new List<Attack>();

            creatureData.Size = "size";

            types.Add("type");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);

            hitPoints.Constitution = abilities[AbilityConstants.Constitution];
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.DefaultTotal = 600;
            hitPoints.Total = 42;

            mockAttackSelector.Setup(s => s.Select("creature")).Returns(attacks);

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", hitPoints, "size", abilities, skills)).Returns(specialQualities);
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 4633, abilities, skills, attacks, specialQualities)).Returns(feats);

            mockSkillsGenerator.Setup(g => g.GenerateFor(hitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(skills);
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, It.IsAny<IEnumerable<Feat>>())).Returns(skills);

            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(true);

            mockCreatureDataSelector.Setup(s => s.SelectFor("creature")).Returns(creatureData);

            var defaultTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(defaultTemplateApplicator.Object);
            defaultTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns((Creature c) => c);

            mockAbilitiesGenerator.Setup(g => g.GenerateFor("creature")).Returns(abilities);

            mockHitPointsGenerator.Setup(g => g.GenerateFor("creature", It.IsAny<CreatureType>(), abilities[AbilityConstants.Constitution])).Returns(hitPoints);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureTypes, "creature")).Returns(types);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, "creature")).Returns(new[] { string.Empty });
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>())).Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(GroupConstants.PoorBaseAttack);

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        private void SetUpRoll(int quantity, int die, params int[] rolls)
        {
            if (!mockPartialRolls.ContainsKey(quantity))
                mockPartialRolls[quantity] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls()).Returns(rolls);

            mockPartialRolls[quantity].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        private void SetUpAverageRoll(string roll, double value)
        {
            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsPotentialAverage()).Returns(value);

            mockDice.Setup(d => d.Roll(roll)).Returns(endRoll.Object);
        }

        [Test]
        public void InvalidCreatureTemplateComboThrowsException()
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(() => creatureGenerator.Generate("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public void GenerateCreatureName()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public void GenerateCreatureSize()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateCreatureType()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void GenerateCreatureTypeWithSubtype()
        {
            types.Add("subtype");

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateCreatureTypeWithMultipleSubtypes()
        {
            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateCreatureAbilities()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GenerateCreatureHitPoints()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void DoNotGenerateAdvancedCreatureHitPointsIfPercentileSaysNo()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(false);

            var advancements = new[]
            {
                new TypeAndAmountSelection { Type = "advanced size", Amount = 1337 }
            };
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Advancements, "creature")).Returns(advancements);

            SetUpRoll(1337, 90210, 42);
            SetUpAverageRoll("1337d90210", 12.34);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
            Assert.That(creature.Size, Is.EqualTo("size"));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()), Times.Never);
            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void DoNotGenerateAdvancedCreatureHitPointsIfCreatureCannotAdvance()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var advancements = Enumerable.Empty<TypeAndAmountSelection>();
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Advancements, "creature")).Returns(advancements);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
            Assert.That(creature.Size, Is.EqualTo("size"));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()), Times.Never);
            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GenerateAdvancedCreatureHitPoints()
        {
            SetUpCreatureAdvancement();

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(1337));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(23));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(1234));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
        }

        private void SetUpCreatureAdvancement(int hitDieQuantity = 1337)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var advancements = new[]
            {
                new TypeAndAmountSelection { Type = "advanced size", Amount = hitDieQuantity }
            };
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Advancements, "creature")).Returns(advancements);

            SetUpRoll(hitDieQuantity, 90210, 1234);

            if (hitDieQuantity > 0)
                SetUpAverageRoll($"{hitDieQuantity}d90210", 23.45);
            else
                SetUpAverageRoll($"0", 0);
        }

        [Test]
        public void GenerateRandomAdvancedCreatureHitPoints()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var advancements = new[]
            {
                new TypeAndAmountSelection { Type = "advanced size", Amount = 1337 },
                new TypeAndAmountSelection { Type = "other advanced size", Amount = 3456 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Advancements, "creature")).Returns(advancements);
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(advancements)).Returns(advancements.Last());

            SetUpRoll(1337, 90210, 1234);
            SetUpAverageRoll("1337d90210", 23.45);
            SetUpRoll(3456, 90210, 4567);
            SetUpAverageRoll("3456d90210", 56.78);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(3456));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(57));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(4567));
            Assert.That(creature.Size, Is.EqualTo("other advanced size"));
        }

        [Test]
        public void GenerateAdvancedCreatureHitPointsWithSizeChangeAndSpaceReachChange()
        {
            SetUpCreatureAdvancement();

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(1337));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(23));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(1234));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
            Assert.Fail("Need to write test to asset that space and reach are updated");
        }

        [Test]
        public void GenerateAdvancedCreatureHitPointsWithSizeChangeAndIrregularSpaceReachChange()
        {
            SetUpCreatureAdvancement();

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(1337));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(23));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(1234));
            Assert.That(creature.Size, Is.EqualTo("advanced size"));
            Assert.Fail("Need to write test to asset that irregular space and reach are updated");
        }

        [Test]
        public void GenerateCreatureSkills()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GenerateAdvancedCreatureSkills()
        {
            SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateFor(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(advancedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public void GenerateCreatureSpecialQualities()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void GenerateAdvancedCreatureSpecialQualities()
        {
            SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateFor(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "advanced size", abilities, advancedSkills)).Returns(advancedSpecialQualities);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public void GenerateCreatureAttacks()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [TestCase(0, GroupConstants.GoodBaseAttack, 0)]
        [TestCase(1, GroupConstants.GoodBaseAttack, 1)]
        [TestCase(2, GroupConstants.GoodBaseAttack, 2)]
        [TestCase(3, GroupConstants.GoodBaseAttack, 3)]
        [TestCase(4, GroupConstants.GoodBaseAttack, 4)]
        [TestCase(5, GroupConstants.GoodBaseAttack, 5)]
        [TestCase(6, GroupConstants.GoodBaseAttack, 6)]
        [TestCase(7, GroupConstants.GoodBaseAttack, 7)]
        [TestCase(8, GroupConstants.GoodBaseAttack, 8)]
        [TestCase(9, GroupConstants.GoodBaseAttack, 9)]
        [TestCase(10, GroupConstants.GoodBaseAttack, 10)]
        [TestCase(11, GroupConstants.GoodBaseAttack, 11)]
        [TestCase(12, GroupConstants.GoodBaseAttack, 12)]
        [TestCase(13, GroupConstants.GoodBaseAttack, 13)]
        [TestCase(14, GroupConstants.GoodBaseAttack, 14)]
        [TestCase(15, GroupConstants.GoodBaseAttack, 15)]
        [TestCase(16, GroupConstants.GoodBaseAttack, 16)]
        [TestCase(17, GroupConstants.GoodBaseAttack, 17)]
        [TestCase(18, GroupConstants.GoodBaseAttack, 18)]
        [TestCase(19, GroupConstants.GoodBaseAttack, 19)]
        [TestCase(20, GroupConstants.GoodBaseAttack, 20)]
        [TestCase(9266, GroupConstants.GoodBaseAttack, 9266)]
        [TestCase(0, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(1, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(2, GroupConstants.AverageBaseAttack, 1)]
        [TestCase(3, GroupConstants.AverageBaseAttack, 2)]
        [TestCase(4, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(5, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(6, GroupConstants.AverageBaseAttack, 4)]
        [TestCase(7, GroupConstants.AverageBaseAttack, 5)]
        [TestCase(8, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(9, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(10, GroupConstants.AverageBaseAttack, 7)]
        [TestCase(11, GroupConstants.AverageBaseAttack, 8)]
        [TestCase(12, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(13, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(14, GroupConstants.AverageBaseAttack, 10)]
        [TestCase(15, GroupConstants.AverageBaseAttack, 11)]
        [TestCase(16, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(17, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(18, GroupConstants.AverageBaseAttack, 13)]
        [TestCase(19, GroupConstants.AverageBaseAttack, 14)]
        [TestCase(20, GroupConstants.AverageBaseAttack, 15)]
        [TestCase(9266, GroupConstants.AverageBaseAttack, 6949)]
        [TestCase(0, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(1, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(2, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(3, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(4, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(5, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(6, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(7, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(8, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(9, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(10, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(11, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(12, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(13, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(14, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(15, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(16, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(17, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(18, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(19, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(20, GroupConstants.PoorBaseAttack, 10)]
        [TestCase(9266, GroupConstants.PoorBaseAttack, 4633)]
        public void GenerateCreatureBaseAttackBonus(int hitDiceQuantity, string bonusQuality, int bonus)
        {
            hitPoints.HitDiceQuantity = hitDiceQuantity;
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(bonusQuality);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(bonus));

        }

        [TestCase(0, GroupConstants.GoodBaseAttack, 0)]
        [TestCase(1, GroupConstants.GoodBaseAttack, 1)]
        [TestCase(2, GroupConstants.GoodBaseAttack, 2)]
        [TestCase(3, GroupConstants.GoodBaseAttack, 3)]
        [TestCase(4, GroupConstants.GoodBaseAttack, 4)]
        [TestCase(5, GroupConstants.GoodBaseAttack, 5)]
        [TestCase(6, GroupConstants.GoodBaseAttack, 6)]
        [TestCase(7, GroupConstants.GoodBaseAttack, 7)]
        [TestCase(8, GroupConstants.GoodBaseAttack, 8)]
        [TestCase(9, GroupConstants.GoodBaseAttack, 9)]
        [TestCase(10, GroupConstants.GoodBaseAttack, 10)]
        [TestCase(11, GroupConstants.GoodBaseAttack, 11)]
        [TestCase(12, GroupConstants.GoodBaseAttack, 12)]
        [TestCase(13, GroupConstants.GoodBaseAttack, 13)]
        [TestCase(14, GroupConstants.GoodBaseAttack, 14)]
        [TestCase(15, GroupConstants.GoodBaseAttack, 15)]
        [TestCase(16, GroupConstants.GoodBaseAttack, 16)]
        [TestCase(17, GroupConstants.GoodBaseAttack, 17)]
        [TestCase(18, GroupConstants.GoodBaseAttack, 18)]
        [TestCase(19, GroupConstants.GoodBaseAttack, 19)]
        [TestCase(20, GroupConstants.GoodBaseAttack, 20)]
        [TestCase(1337, GroupConstants.GoodBaseAttack, 1337)]
        [TestCase(0, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(1, GroupConstants.AverageBaseAttack, 0)]
        [TestCase(2, GroupConstants.AverageBaseAttack, 1)]
        [TestCase(3, GroupConstants.AverageBaseAttack, 2)]
        [TestCase(4, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(5, GroupConstants.AverageBaseAttack, 3)]
        [TestCase(6, GroupConstants.AverageBaseAttack, 4)]
        [TestCase(7, GroupConstants.AverageBaseAttack, 5)]
        [TestCase(8, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(9, GroupConstants.AverageBaseAttack, 6)]
        [TestCase(10, GroupConstants.AverageBaseAttack, 7)]
        [TestCase(11, GroupConstants.AverageBaseAttack, 8)]
        [TestCase(12, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(13, GroupConstants.AverageBaseAttack, 9)]
        [TestCase(14, GroupConstants.AverageBaseAttack, 10)]
        [TestCase(15, GroupConstants.AverageBaseAttack, 11)]
        [TestCase(16, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(17, GroupConstants.AverageBaseAttack, 12)]
        [TestCase(18, GroupConstants.AverageBaseAttack, 13)]
        [TestCase(19, GroupConstants.AverageBaseAttack, 14)]
        [TestCase(20, GroupConstants.AverageBaseAttack, 15)]
        [TestCase(1337, GroupConstants.AverageBaseAttack, 1002)]
        [TestCase(0, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(1, GroupConstants.PoorBaseAttack, 0)]
        [TestCase(2, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(3, GroupConstants.PoorBaseAttack, 1)]
        [TestCase(4, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(5, GroupConstants.PoorBaseAttack, 2)]
        [TestCase(6, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(7, GroupConstants.PoorBaseAttack, 3)]
        [TestCase(8, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(9, GroupConstants.PoorBaseAttack, 4)]
        [TestCase(10, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(11, GroupConstants.PoorBaseAttack, 5)]
        [TestCase(12, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(13, GroupConstants.PoorBaseAttack, 6)]
        [TestCase(14, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(15, GroupConstants.PoorBaseAttack, 7)]
        [TestCase(16, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(17, GroupConstants.PoorBaseAttack, 8)]
        [TestCase(18, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(19, GroupConstants.PoorBaseAttack, 9)]
        [TestCase(20, GroupConstants.PoorBaseAttack, 10)]
        [TestCase(1337, GroupConstants.PoorBaseAttack, 668)]
        public void GenerateAdvancedCreatureBaseAttackBonus(int advancedHitDiceQuantity, string bonusQuality, int bonus)
        {
            SetUpCreatureAdvancement(advancedHitDiceQuantity);

            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(bonusQuality);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void GenerateCreatureFeats()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void GenerateAdvancedCreatureFeats()
        {
            SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateFor(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "advanced size", abilities, advancedSkills)).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateFeats(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), 668, abilities, advancedSkills, attacks, advancedSpecialQualities)).Returns(advancedFeats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public void GenerateCreatureHitPointsWithFeats()
        {
            var updatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, feats)).Returns(updatedHitPoints);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void GenerateAdvancedCreatureHitPointsWithFeats()
        {
            SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateFor(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "advanced size", abilities, advancedSkills)).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateFeats(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), 668, abilities, advancedSkills, attacks, advancedSpecialQualities)).Returns(advancedFeats);

            var advancedUpdatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, advancedFeats)).Returns(advancedUpdatedHitPoints);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public void GenerateCreatureSkillsUpdatedByFeats()
        {
            var updatedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats)).Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateFor(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities)).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), "advanced size", abilities, advancedSkills)).Returns(advancedSpecialQualities);

            var advancedFeats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateFeats(It.Is<HitPoints>(hp => hp.HitDiceQuantity == 1337), 668, abilities, advancedSkills, attacks, advancedSpecialQualities)).Returns(advancedFeats);

            var updatedSkills = new List<Skill>();
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(advancedSkills, advancedFeats)).Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateCreatureGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1234;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, "size")).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(612 + 4633 + 2345));
        }

        [Test]
        public void GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Strength].BaseScore = 1234;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, "advanced size")).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(612 + 668 + 2345));
        }

        [Test]
        public void GenerateCreatureNegativeGrappleBonus()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, "size")).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(-5 + 4633 + 2345));
        }

        [Test]
        public void NoGrappleBonusIfNoStrengthScore()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.GrappleBonuses, "size")).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void GenerateCreaturePrimaryMeleeAttackBonuses()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.MeleeAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.MeleeAttack.TotalAttackBonus, Is.EqualTo(612 + 4633));
        }

        [Test]
        public void GenerateAdvancedCreaturePrimaryMeleeAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.MeleeAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.MeleeAttack.TotalAttackBonus, Is.EqualTo(612 + 668));
        }

        [Test]
        public void GenerateCreaturePrimaryRangedAttackBonuses()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.RangedAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.RangedAttack.TotalAttackBonus, Is.EqualTo(612 + 4633));
        }

        [Test]
        public void GenerateAdvancedCreaturePrimaryRangedAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.RangedAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.RangedAttack.TotalAttackBonus, Is.EqualTo(612 + 668));
        }

        [Test]
        public void GenerateCreatureSecondaryMeleeAttackBonuses()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryMeleeAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryRangedAttackBonuses()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryRangedAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalMeleeAttackBonuses()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalMeleeAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalRangedAttackBonuses()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalRangedAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalMeleeAttackBonusesWithMultiAttack()
        {
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 2));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalMeleeAttackBonusesWithMultiAttack()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 2));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalRangedAttackBonusesWithMultiAttack()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 2));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalRangedAttackBonusesWithMultiAttack()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 2));
        }

        [Test]
        public void GenerateCreatureSpecialAttackBonuses()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", Damage = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", Damage = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", Damage = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", Damage = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", Damage = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", Damage = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", Damage = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");

            Assert.That(creature.SpecialAttacks, Is.EquivalentTo(attacks));
            foreach (var attack in creature.SpecialAttacks)
                Assert.That(attack.TotalAttackBonus, Is.EqualTo(0));
        }

        [Test]
        public void GenerateAdvancedCreatureSpecialAttackBonuses()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 1234;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", Damage = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", Damage = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", Damage = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", Damage = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", Damage = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", Damage = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", Damage = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");

            Assert.That(creature.SpecialAttacks, Is.EquivalentTo(attacks));
            foreach (var attack in creature.SpecialAttacks)
                Assert.That(attack.TotalAttackBonus, Is.EqualTo(0));
        }

        [Test]
        public void GenerateCreaturePrimaryMeleeAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.MeleeAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.MeleeAttack.TotalAttackBonus, Is.EqualTo(612 + 4633));
        }

        [Test]
        public void GenerateAdvancedCreaturePrimaryMeleeAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.MeleeAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.MeleeAttack.TotalAttackBonus, Is.EqualTo(612 + 668));
        }

        [Test]
        public void GenerateCreaturePrimaryRangedAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.RangedAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.RangedAttack.TotalAttackBonus, Is.EqualTo(612 + 4633));
        }

        [Test]
        public void GenerateAdvancedCreaturePrimaryRangedAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.RangedAttack, Is.EqualTo(attacks[0]));
            Assert.That(creature.RangedAttack.TotalAttackBonus, Is.EqualTo(612 + 668));
        }

        [Test]
        public void GenerateCreatureSecondaryMeleeAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryMeleeAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryRangedAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryRangedAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalMeleeAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalMeleeAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalRangedAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 5));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalRangedAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = "other feat" });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 5));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalMeleeAttackBonusesWithMultiAttackWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 2));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalMeleeAttackBonusesWithMultiAttackWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = true, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullMeleeAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 2));
        }

        [Test]
        public void GenerateCreatureSecondaryNaturalRangedAttackBonusesWithMultiAttackWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 4633 - 2));
        }

        [Test]
        public void GenerateAdvancedCreatureSecondaryNaturalRangedAttackBonusesWithMultiAttackWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = true });

            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.FullRangedAttack.Single(), Is.EqualTo(attacks[0]));
            Assert.That(attacks[0].TotalAttackBonus, Is.EqualTo(612 + 668 - 2));
        }

        [Test]
        public void GenerateCreatureSpecialAttackBonusesWithNoStrengthScore()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", Damage = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", Damage = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", Damage = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", Damage = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", Damage = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", Damage = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", Damage = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = FeatConstants.MultiAttack });

            var creature = creatureGenerator.Generate("creature", "template");

            Assert.That(creature.SpecialAttacks, Is.EquivalentTo(attacks));
            foreach (var attack in creature.SpecialAttacks)
                Assert.That(attack.TotalAttackBonus, Is.EqualTo(0));
        }

        [Test]
        public void GenerateAdvancedCreatureSpecialAttackBonusesWithNoStrengthScore()
        {
            SetUpCreatureAdvancement();

            abilities[AbilityConstants.Dexterity].BaseScore = 1234;
            abilities[AbilityConstants.Strength].BaseScore = 0;

            attacks.Add(new Attack { Name = "attack 1", Damage = "damage 1", IsMelee = false, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 2", Damage = "damage 2", IsMelee = false, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 3", Damage = "damage 3", IsMelee = false, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 4", Damage = "damage 4", IsMelee = false, IsPrimary = true, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 5", Damage = "damage 5", IsMelee = true, IsPrimary = false, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 6", Damage = "damage 6", IsMelee = true, IsPrimary = false, IsNatural = true, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 7", Damage = "damage 7", IsMelee = true, IsPrimary = true, IsNatural = false, IsSpecial = true });
            attacks.Add(new Attack { Name = "attack 8", Damage = "damage 8", IsMelee = true, IsPrimary = true, IsNatural = true, IsSpecial = true });

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = FeatConstants.MultiAttack });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");

            Assert.That(creature.SpecialAttacks, Is.EquivalentTo(attacks));
            foreach (var attack in creature.SpecialAttacks)
                Assert.That(attack.TotalAttackBonus, Is.EqualTo(0));
        }

        [Test]
        public void GenerateCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.ImprovedInitiative, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.ImprovedInitiative, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.ImprovedInitiative, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.ImprovedInitiative, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateCreatureSpeeds()
        {
            var speeds = new[]
            {
                new TypeAndAmountSelection { Type = "on foot", Amount = 1234 },
                new TypeAndAmountSelection { Type = "in a car", Amount = 2345 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Speeds, "creature")).Returns(speeds);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Speeds["on foot"].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds["on foot"].Value, Is.EqualTo(1234));
            Assert.That(creature.Speeds["on foot"].Description, Is.Empty);
            Assert.That(creature.Speeds["in a car"].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds["in a car"].Value, Is.EqualTo(2345));
            Assert.That(creature.Speeds["in a car"].Description, Is.Empty);
            Assert.That(creature.Speeds.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateCreatureAerialSpeedAndDescription()
        {
            var speeds = new[]
            {
                new TypeAndAmountSelection { Type = "on foot", Amount = 1234 },
                new TypeAndAmountSelection { Type = SpeedConstants.Fly, Amount = 2345 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Speeds, "creature")).Returns(speeds);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, "creature")).Returns(new[] { "maneuverability" });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Speeds["on foot"].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds["on foot"].Value, Is.EqualTo(1234));
            Assert.That(creature.Speeds["on foot"].Description, Is.Empty);
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(2345));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("maneuverability"));
            Assert.That(creature.Speeds.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateCreatureArmorClass()
        {
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(abilities[AbilityConstants.Dexterity], "size", "creature", feats)).Returns(armorClass);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GenerateAdvancedCreatureArmorClass()
        {
            SetUpCreatureAdvancement();

            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(abilities[AbilityConstants.Dexterity], "advanced size", "creature", feats)).Returns(armorClass);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GenerateCreatureSpace()
        {
            creatureData.Space = 12.34;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(12.34));
        }

        [Test]
        public void GenerateCreatureReach()
        {
            creatureData.Reach = 12.34;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(12.34));
        }

        [Test]
        public void GenerateCreatureSaves()
        {
            var saves = new Saves();
            mockSavesGenerator.Setup(g => g.GenerateWith(It.IsAny<CreatureType>(), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAdvancedCreatureSaves()
        {
            SetUpCreatureAdvancement();

            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668, abilities, skills, attacks, specialQualities)).Returns(feats);

            var saves = new Saves();
            mockSavesGenerator.Setup(g => g.GenerateWith(It.IsAny<CreatureType>(), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateCreatureChallengeRating()
        {
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void GenerateCreatureAlignment()
        {
            var alignment = new Alignment("creature alignment");
            mockAlignmentGenerator.Setup(g => g.Generate("creature")).Returns(alignment);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void GenerateCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void GenerateNoCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void GenerateCreatureLevelAdjustmentOf0()
        {
            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(0));
        }

        [Test]
        public void GenerateCreatureModifiedByTemplate()
        {
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns(templateCreature);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [Test]
        public void GenerateCreatureNotModifiedByTemplate()
        {
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns((Creature c) => c);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature, Is.Not.EqualTo(templateCreature));
        }
    }
}