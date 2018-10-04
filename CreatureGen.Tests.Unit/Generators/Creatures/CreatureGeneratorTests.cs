using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Attacks;
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
using Moq;
using NUnit.Framework;
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
        private Mock<ICreatureVerifier> mockCreatureVerifier;
        private ICreatureGenerator creatureGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<IAdvancementSelector> mockAdvancementSelector;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;

        private Dictionary<string, Ability> abilities;
        private List<Skill> skills;
        private List<Feat> specialQualities;
        private List<Feat> feats;
        private CreatureDataSelection creatureData;
        private HitPoints hitPoints;
        private List<string> types;
        private List<Attack> attacks;
        private ArmorClass armorClass;
        private Dictionary<string, Measurement> speeds;

        [SetUp]
        public void Setup()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            mockCreatureVerifier = new Mock<ICreatureVerifier>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockArmorClassGenerator = new Mock<IArmorClassGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockAdvancementSelector = new Mock<IAdvancementSelector>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();

            creatureGenerator = new CreatureGenerator(
                mockAlignmentGenerator.Object,
                mockCreatureVerifier.Object,
                mockCollectionSelector.Object,
                mockAbilitiesGenerator.Object,
                mockSkillsGenerator.Object,
                mockFeatsGenerator.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockArmorClassGenerator.Object,
                mockSavesGenerator.Object,
                mockJustInTimeFactory.Object,
                mockAdvancementSelector.Object,
                mockAttacksGenerator.Object,
                mockSpeedsGenerator.Object);

            feats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            creatureData = new CreatureDataSelection();
            hitPoints = new HitPoints();
            types = new List<string>();
            specialQualities = new List<Feat>();
            attacks = new List<Attack>();
            armorClass = new ArmorClass();
            speeds = new Dictionary<string, Measurement>();

            creatureData.Size = "size";
            creatureData.CasterLevel = 1029;
            creatureData.ChallengeRating = "challenge rating";
            creatureData.LevelAdjustment = 4567;
            creatureData.NaturalArmor = 1336;
            creatureData.NumberOfHands = 96;
            creatureData.Space = 56.78;
            creatureData.Reach = 67.89;

            types.Add("type");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            hitPoints.Constitution = abilities[AbilityConstants.Constitution];
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.DefaultTotal = 600;
            hitPoints.Total = 42;

            SetUpCreature("creature", "template");

            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, It.IsAny<IEnumerable<Feat>>(), abilities)).Returns(skills);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>())).Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(GroupConstants.PoorBaseAttack);
        }

        private void SetUpCreature(string creatureName, string templateName)
        {
            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), hitPoints)).Returns(753);
            mockAttacksGenerator.Setup(g => g.GenerateAttacks(creatureName, creatureData.Size, creatureData.Size, 753, abilities)).Returns(attacks);
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(attacks);

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, abilities, skills, creatureData.CanUseEquipment)).Returns(specialQualities);
            mockSkillsGenerator.Setup(g => g.GenerateFor(hitPoints, creatureName, It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, creatureData.Size)).Returns(skills);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(creatureName, templateName)).Returns(true);
            mockCreatureDataSelector.Setup(s => s.SelectFor(creatureName)).Returns(creatureData);

            mockFeatsGenerator.Setup(g =>
                g.GenerateFeats(
                    hitPoints,
                    753,
                    abilities,
                    skills,
                    attacks,
                    specialQualities,
                    1029,
                    It.IsAny<Dictionary<string, Measurement>>(),
                    1336,
                    96,
                    "size"
                )
            ).Returns(feats);

            var defaultTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(templateName)).Returns(defaultTemplateApplicator.Object);
            defaultTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns((Creature c) => c);

            mockAbilitiesGenerator.Setup(g => g.GenerateFor(creatureName)).Returns(abilities);
            mockHitPointsGenerator.Setup(g => g.GenerateFor(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), abilities[AbilityConstants.Constitution], creatureData.Size, 0)).Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName)).Returns(types);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AerialManeuverability, creatureName)).Returns(new[] { string.Empty });
            mockArmorClassGenerator.Setup(g => g.GenerateWith(abilities, creatureData.Size, creatureName, It.Is<CreatureType>(c => c.Name == types[0]), feats, creatureData.NaturalArmor)).Returns(armorClass);

            mockSpeedsGenerator.Setup(g => g.Generate(creatureName)).Returns(speeds);
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
        public void GenerateCreatureSpace()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void GenerateCreatureReach()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void GenerateCreatureCanUseEquipment()
        {
            creatureData.CanUseEquipment = true;
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void GenerateCreatureCannotUseEquipment()
        {
            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void GenerateCreatureChallengeRating()
        {
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
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
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateCreatureCasterLevel()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void GenerateCreatureNumberOfHands()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
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
        public void DoNotGenerateAdvancedCreature()
        {
            SetUpCreatureAdvancement();
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(90210));
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
        }

        [Test]
        public void GenerateAdvancedCreature()
        {
            var advancedhitPoints = SetUpCreatureAdvancement();
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedhitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(573));
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
        }

        [Test]
        public void GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement();

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedHitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(573));
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
        }

        [Test]
        public void GenerateAdvancedCreatureWithMissingAbilities()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement();

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedHitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(573));
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
        }

        private HitPoints SetUpCreatureAdvancement(int advancementAmount = 1337, string creature = "creature")
        {
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creature)).Returns(true);

            var advancement = new AdvancementSelection();
            advancement.AdditionalHitDice = advancementAmount;
            advancement.Reach = 98.76;
            advancement.Size = "advanced size";
            advancement.Space = 54.32;
            advancement.AdjustedChallengeRating = "adjusted challenge rating";
            advancement.CasterLevelAdjustment = 6331;
            advancement.ConstitutionAdjustment = 69;
            advancement.DexterityAdjustment = 783;
            advancement.NaturalArmorAdjustment = 8245;
            advancement.StrengthAdjustment = 3456;

            mockAdvancementSelector.Setup(s => s.SelectRandomFor(creature, It.Is<CreatureType>(c => c.Name == types[0]), creatureData.Size, creatureData.ChallengeRating)).Returns(advancement);

            var advancedHitPoints = new HitPoints();
            advancedHitPoints.Constitution = abilities[AbilityConstants.Constitution];
            advancedHitPoints.HitDiceQuantity = 681;
            advancedHitPoints.HitDie = 573;
            advancedHitPoints.DefaultTotal = 492;
            advancedHitPoints.Total = 862;

            mockHitPointsGenerator.Setup(g => g.GenerateFor(creature, It.Is<CreatureType>(c => c.Name == types[0]), abilities[AbilityConstants.Constitution], "advanced size", advancementAmount)).Returns(advancedHitPoints);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(advancedHitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(advancedHitPoints);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(999);
            mockAttacksGenerator.Setup(g => g.GenerateAttacks(creature, creatureData.Size, advancement.Size, 999, abilities)).Returns(attacks);

            var newNaturalArmor = creatureData.NaturalArmor + advancement.NaturalArmorAdjustment;

            mockFeatsGenerator.Setup(g => g.GenerateFeats(
                advancedHitPoints,
                999,
                abilities,
                skills,
                attacks,
                specialQualities,
                creatureData.CasterLevel + advancement.CasterLevelAdjustment,
                speeds,
                newNaturalArmor,
                creatureData.NumberOfHands,
                advancement.Size)).Returns(feats);

            mockArmorClassGenerator.Setup(g => g.GenerateWith(
                abilities,
                advancement.Size,
                creature,
                It.Is<CreatureType>(c => c.Name == types[0]),
                feats,
                newNaturalArmor)).Returns(armorClass);

            return advancedHitPoints;
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
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(advancedSkills, advancedFeats, abilities)).Returns(advancedSkills);

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
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public void GenerateCreatureBaseAttackBonus()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void GenerateAdvancedCreatureBaseAttackBonus()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public void GenerateCreatureAttacks()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void GenerateAdvancedCreatureAttacks()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(advancedAttacks, It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()), abilities))
                .Returns(advancedAttacks);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(advancedAttacks));
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
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

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
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            var advancedUpdatedHitPoints = new HitPoints();
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(advancedHitPoints, advancedFeats)).Returns(advancedUpdatedHitPoints);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public void GenerateCreatureSkillsUpdatedByFeats()
        {
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            var updatedSkills = new List<Skill> { new Skill("updated advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(advancedSkills, advancedFeats, abilities)).Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateCreatureGrappleBonus()
        {
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreatureAdvancement();

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateNoGrappleBonus()
        {
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void ApplyAttackBonuses()
        {
            var modifiedAttacks = new[] { new Attack() { Name = "modified attack" } };
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(modifiedAttacks);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(modifiedAttacks));
        }

        [Test]
        public void ApplyAdvancedAttackBonuses()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            var modifiedAttacks = new[] { new Attack() { Name = "modified advanced attack" } };
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(advancedAttacks, It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()), abilities))
                .Returns(modifiedAttacks);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(modifiedAttacks));
        }

        [Test]
        public void GenerateCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement();

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement();

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
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

            SetUpCreatureAdvancement();

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size")).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement();

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size")).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateCreatureSpeeds()
        {
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void GenerateCreatureArmorClass()
        {
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(abilities, "size", "creature", It.Is<CreatureType>(c => c.Name == types[0]), feats, creatureData.NaturalArmor)).Returns(armorClass);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GenerateAdvancedCreatureArmorClass()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks("creature", creatureData.Size, "advanced size", 999, abilities)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.GenerateFor(advancedHitPoints, "creature", It.Is<CreatureType>(c => c.Name == types[0]), abilities, creatureData.CanUseEquipment, "advanced size")).Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, abilities, advancedSkills, creatureData.CanUseEquipment)).Returns(advancedSpecialQualities);

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
                "advanced size")).Returns(advancedFeats);

            var advancedArmorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(abilities, "advanced size", "creature", It.Is<CreatureType>(c => c.Name == types[0]), It.Is<IEnumerable<Feat>>(f => advancedFeats.Intersect(f).Count() == advancedFeats.Count()), 1336 + 8245)).Returns(advancedArmorClass);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public void GenerateCreatureSaves()
        {
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAdvancedCreatureSaves()
        {
            var advancedHitPoints = SetUpCreatureAdvancement();

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size")).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
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