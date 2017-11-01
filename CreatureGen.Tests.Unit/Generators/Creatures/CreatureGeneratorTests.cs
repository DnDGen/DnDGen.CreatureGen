using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Creatures;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Selectors.Collections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using CreatureGen.Verifiers;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

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
        private Generator generator;
        private ICreatureGenerator creatureGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<IAttackSelector> mockAttackSelector;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;

        private Alignment alignment;
        private Alignment alignmentPrototype;
        private Dictionary<string, int> levelAdjustments;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private List<Skill> skills;
        private List<string> languages;
        private List<string> featSkillFoci;

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
                mockDice.Object);

            levelAdjustments = new Dictionary<string, int>();

            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => levelAdjustments[name]);

            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");
            featSkillFoci.Add("skill 3");
            featSkillFoci.Add("skill 4");
            featSkillFoci.Add("skill 5");

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.Skills)).Returns(featSkillFoci);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(true);
        }

        [Test]
        public void InvalidCreatureTemplateComboThrowsException()
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(() => creatureGenerator.Generate("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public void GenerateCreature()
        {
            Assert.Fail("Not yet written");
        }
    }
}