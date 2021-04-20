using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Generators.Languages;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
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
        private Mock<IEquipmentGenerator> mockEquipmentGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ILanguageGenerator> mockLanguageGenerator;

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
        private Alignment alignment;
        private Equipment equipment;
        private Magic magic;
        private List<string> languages;

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
            mockEquipmentGenerator = new Mock<IEquipmentGenerator>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();

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
                mockSpeedsGenerator.Object,
                mockEquipmentGenerator.Object,
                mockMagicGenerator.Object,
                mockLanguageGenerator.Object);

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
            equipment = new Equipment();
            magic = new Magic();
            languages = new List<string>();

            alignment = new Alignment("creature alignment");

            creatureData.Size = "size";
            creatureData.CasterLevel = 1029;
            creatureData.ChallengeRating = "challenge rating";
            creatureData.LevelAdjustment = 4567;
            creatureData.NaturalArmor = 1336;
            creatureData.NumberOfHands = 96;
            creatureData.Space = 56.78;
            creatureData.Reach = 67.89;

            types.Add("type");

            languages.Add("English");
            languages.Add("Deutsch");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            hitPoints.Constitution = abilities[AbilityConstants.Constitution];
            hitPoints.HitDice.Add(new HitDice { Quantity = 9266, HitDie = 90210 });
            hitPoints.DefaultTotal = 600;
            hitPoints.Total = 42;

            SetUpCreature("creature", "template", false);

            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, It.IsAny<IEnumerable<Feat>>(), abilities)).Returns(skills);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>())).Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(GroupConstants.PoorBaseAttack);
        }

        private void SetUpCreature(string creatureName, string templateName, bool asCharacter)
        {
            mockAlignmentGenerator.Setup(g => g.Generate(creatureName)).Returns(alignment);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), hitPoints)).Returns(753);
            mockAttacksGenerator.Setup(g => g.GenerateAttacks(creatureName, creatureData.Size, creatureData.Size, 753, abilities, hitPoints.RoundedHitDiceQuantity)).Returns(attacks);
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(attacks);

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
                It.Is<CreatureType>(c => c.Name == types[0]),
                hitPoints,
                abilities,
                skills,
                creatureData.CanUseEquipment,
                creatureData.Size,
                alignment)
            ).Returns(specialQualities);

            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    hitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    creatureData.Size,
                    true))
                .Returns(skills);
            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    creatureName,
                    skills,
                    equipment))
                .Returns(skills);

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
                    "size",
                    creatureData.CanUseEquipment
                )
            ).Returns(feats);

            var defaultTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(templateName)).Returns(defaultTemplateApplicator.Object);
            defaultTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns((Creature c) => c);
            defaultTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>())).ReturnsAsync((Creature c) => c);

            mockAbilitiesGenerator.Setup(g => g.GenerateFor(creatureName)).Returns(abilities);
            mockAbilitiesGenerator.Setup(g => g.SetMaxBonuses(abilities, equipment)).Returns(abilities);

            mockHitPointsGenerator
                .Setup(g => g.GenerateFor(
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities[AbilityConstants.Constitution],
                    creatureData.Size,
                    0,
                    asCharacter))
                .Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName)).Returns(types);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AerialManeuverability, creatureName)).Returns(new[] { string.Empty });
            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    creatureData.Size,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    feats,
                    creatureData.NaturalArmor,
                    equipment))
                .Returns(armorClass);

            mockSpeedsGenerator.Setup(g => g.Generate(creatureName)).Returns(speeds);

            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(feats, attacks, creatureData.NumberOfHands))
                .Returns(attacks);
            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    hitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    creatureData.Size))
                .Returns(equipment);

            mockMagicGenerator
                .Setup(g => g.GenerateWith(creatureName,
                    alignment,
                    abilities,
                    equipment))
                .Returns(magic);

            mockLanguageGenerator
                .Setup(g => g.GenerateWith(creatureName,
                    abilities,
                    skills))
                .Returns(languages);
        }

        [Test]
        public void Generate_InvalidCreatureTemplateComboThrowsException()
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(() => creatureGenerator.Generate("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public void Generate_GenerateCreatureName()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public void Generate_GenerateCreatureSize()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void Generate_GenerateCreatureSpace()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void Generate_GenerateCreatureReach()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void Generate_GenerateCreatureCanUseEquipment()
        {
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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void Generate_GenerateCreatureCannotUseEquipment()
        {
            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void Generate_GenerateCreatureChallengeRating()
        {
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void Generate_GenerateCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void Generate_GenerateNoCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void Generate_GenerateCreatureLevelAdjustmentOf0()
        {
            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void Generate_GenerateCreatureCasterLevel()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void Generate_GenerateCreatureNumberOfHands()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public void Generate_GenerateCreatureType()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void Generate_GenerateCreatureTypeWithSubtype()
        {
            types.Add("subtype");

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void Generate_GenerateCreatureTypeWithMultipleSubtypes()
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
        public void Generate_GenerateCreatureAbilities()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void Generate_GenerateCreatureHitPoints()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void Generate_GenerateCreatureEquipment()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void Generate_GenerateCreatureMagic()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void Generate_GenerateCreatureLanguages()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void Generate_DoNotGenerateAdvancedCreature()
        {
            SetUpCreatureAdvancement(false);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = creatureGenerator.Generate("creature", "template");
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
        public void Generate_GenerateAdvancedCreature()
        {
            var advancedhitPoints = SetUpCreatureAdvancement(false);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
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

        [Test]
        public void Generate_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
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

        [Test]
        public void Generate_GenerateAdvancedCreatureWithMissingAbilities()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template");
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

        private HitPoints SetUpCreatureAdvancement(bool asCharacter, int advancementAmount = 1337, string creatureName = "creature")
        {
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

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

            mockAdvancementSelector
                .Setup(s => s.SelectRandomFor(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), creatureData.Size, creatureData.ChallengeRating))
                .Returns(advancement);

            var advancedHitPoints = new HitPoints();
            advancedHitPoints.Constitution = abilities[AbilityConstants.Constitution];
            advancedHitPoints.HitDice.Add(new HitDice { Quantity = 681, HitDie = 573 });
            advancedHitPoints.DefaultTotal = 492;
            advancedHitPoints.Total = 862;

            mockHitPointsGenerator
                .Setup(g => g.GenerateFor(
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities[AbilityConstants.Constitution],
                    "advanced size",
                    advancementAmount,
                    asCharacter))
                .Returns(advancedHitPoints);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(advancedHitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(advancedHitPoints);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(999);
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(creatureName, creatureData.Size, advancement.Size, 999, abilities, advancedHitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);

            var advancedNaturalArmor = creatureData.NaturalArmor + advancement.NaturalArmorAdjustment;

            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    advancedHitPoints,
                    999,
                    abilities,
                    skills,
                    attacks,
                    specialQualities,
                    creatureData.CasterLevel + advancement.CasterLevelAdjustment,
                    speeds,
                    advancedNaturalArmor,
                    creatureData.NumberOfHands,
                    advancement.Size,
                    creatureData.CanUseEquipment))
                .Returns(feats);

            var advancedEquipment = new Equipment();
            mockEquipmentGenerator
                .Setup(g => g.AddAttacks(
                    feats,
                    attacks,
                    creatureData.NumberOfHands))
                .Returns(attacks);
            mockEquipmentGenerator
                .Setup(g => g.Generate(creatureName,
                    creatureData.CanUseEquipment,
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedHitPoints.RoundedHitDiceQuantity,
                    attacks,
                    abilities,
                    advancement.Size))
                .Returns(advancedEquipment);

            mockMagicGenerator
                .Setup(g => g.GenerateWith(creatureName,
                    alignment,
                    abilities,
                    advancedEquipment))
                .Returns(magic);

            mockArmorClassGenerator
                .Setup(g => g.GenerateWith(
                    abilities,
                    advancement.Size,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    advancedNaturalArmor,
                    advancedEquipment))
                .Returns(armorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, skills, advancedEquipment))
                .Returns(skills);

            return advancedHitPoints;
        }

        [Test]
        public void Generate_GenerateCreatureSkills()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureSkills()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public void Generate_GenerateCreatureSpecialQualities()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureSpecialQualities()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public void Generate_GenerateCreatureBaseAttackBonus()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public void Generate_GenerateCreatureAttacks()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureAttacks()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public void Generate_GenerateCreatureFeats()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public void Generate_GenerateCreatureHitPointsWithFeats()
        {
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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public void Generate_GenerateCreatureSkillsUpdatedByFeats()
        {
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void Generate_GenerateCreatureGrappleBonus()
        {
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreatureAdvancement(false);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void Generate_GenerateNoGrappleBonus()
        {
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void Generate_ApplyAttackBonuses()
        {
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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void Generate_ApplyAdvancedAttackBonuses()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void Generate_GenerateCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void Generate_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void Generate_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void Generate_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void Generate_GenerateCreatureSpeeds()
        {
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void Generate_GenerateCreatureArmorClass()
        {
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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureArmorClass()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public void Generate_GenerateCreatureSaves()
        {
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void Generate_GenerateAdvancedCreatureSaves()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void Generate_GenerateCreatureAlignment()
        {
            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void Generate_GenerateCreatureModifiedByTemplate()
        {
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns(templateCreature);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [Test]
        public void Generate_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", false);

            var creature = creatureGenerator.Generate("creature", "template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));
        }

        [Test]
        public async Task GenerateAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", false);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));
        }

        [Test]
        public void GenerateAsCharacter_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));
        }

        [Test]
        public async Task GenerateAsync_InvalidCreatureTemplateComboThrowsException()
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateAsync("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureName()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSize()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSpace()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureReach()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureCanUseEquipment()
        {
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureCannotUseEquipment()
        {
            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureChallengeRating()
        {
            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public async Task GenerateAsync_GenerateNoCreatureLevelAdjustment()
        {
            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureCasterLevel()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureNumberOfHands()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureType()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureTypeWithSubtype()
        {
            types.Add("subtype");

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureAbilities()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureHitPoints()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureEquipment()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureMagic()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public async Task GenerateAsync_DoNotGenerateAdvancedCreature()
        {
            SetUpCreatureAdvancement(false);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
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
        public async Task GenerateAsync_GenerateAdvancedCreature()
        {
            var advancedhitPoints = SetUpCreatureAdvancement(false);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureWithMissingAbilities()
        {
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsync_GenerateCreatureSkills()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureSkills()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSpecialQualities()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureSpecialQualities()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureBaseAttackBonus()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureAttacks()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureAttacks()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureFeats()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureHitPointsWithFeats()
        {
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureGrappleBonus()
        {
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreatureAdvancement(false);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsync_GenerateNoGrappleBonus()
        {
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public async Task GenerateAsync_ApplyAttackBonuses()
        {
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsync_ApplyAdvancedAttackBonuses()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureInitiativeBonus()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSpeeds()
        {
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureArmorClass()
        {
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureArmorClass()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

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

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureSaves()
        {
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsync_GenerateAdvancedCreatureSaves()
        {
            var advancedHitPoints = SetUpCreatureAdvancement(false);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureAlignment()
        {
            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public async Task GenerateAsync_GenerateCreatureModifiedByTemplate()
        {
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>())).ReturnsAsync(templateCreature);

            var creature = await creatureGenerator.GenerateAsync("creature", "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [Test]
        public void GenerateAsCharacter_InvalidCreatureCharacterComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(false);

            Assert.That(() => creatureGenerator.GenerateAsCharacter("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAsCharacterException>());
        }

        [Test]
        public void GenerateAsCharacter_InvalidCreatureTemplateComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(() => creatureGenerator.GenerateAsCharacter("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureName()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSize()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpace()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureReach()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCanUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCannotUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.CanUseEquipment = false;

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureChallengeRating()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void GenerateAsCharacter_GenerateNoCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLevelAdjustmentOf0()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCasterLevel()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureNumberOfHands()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureType()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureTypeWithSubtype()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            types.Add("subtype");

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureTypeWithMultipleSubtypes()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureAbilities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureHitPoints()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureMagic()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLanguages()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GenerateAsCharacter_DoNotGenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
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
        public void GenerateAsCharacter_GenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedhitPoints = SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
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

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
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

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureWithMissingAbilities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
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

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateAsCharacter_GenerateNoGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void GenerateAsCharacter_ApplyAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateAsCharacter_ApplyAdvancedAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpeeds()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureAlignment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureModifiedByTemplate()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns(templateCreature);

            var creature = creatureGenerator.GenerateAsCharacter("creature", "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_InvalidCreatureAsCharacterComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateAsCharacterAsync("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAsCharacterException>());
        }

        [Test]
        public async Task GenerateAsCharacterAsync_InvalidCreatureTemplateComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility("creature", "template")).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateAsCharacterAsync("creature", "template"), Throws.InstanceOf<IncompatibleCreatureAndTemplateException>());
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureName()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSize()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpace()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureReach()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCanUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCannotUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.CanUseEquipment = false;

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureChallengeRating()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateNoCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCasterLevel()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureNumberOfHands()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureType()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureTypeWithSubtype()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            types.Add("subtype");

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureAbilities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureHitPoints()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureEquipment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureMagic()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_DoNotGenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
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
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedhitPoints = SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureWithMissingAbilities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
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

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateNoGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_ApplyAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_ApplyAdvancedAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpeeds()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

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

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureAlignment()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureModifiedByTemplate()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter("creature")).Returns(true);
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>())).ReturnsAsync(templateCreature);

            var creature = await creatureGenerator.GenerateAsCharacterAsync("creature", "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void GenerateRandomOfTemplate_GenerateCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void GenerateRandomOfTemplate_GenerateRandomCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
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
        public void GenerateRandomOfTemplate_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureLanguages()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Languages, Is.EqualTo(languages));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_DoNotGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(false);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
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
        public void GenerateRandomOfTemplate_GenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedhitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureWithMissingAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, updatedSkills, advancedEquipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_ApplyAdvancedAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity))
                .Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null.And.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateAdvancedCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplate_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = creatureGenerator.GenerateRandomOfTemplate("my template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public async Task GenerateRandomOfTemplateAsync_GenerateRandomCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
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
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_DoNotGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(false);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
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
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedhitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureWithMissingAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, updatedSkills, advancedEquipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_ApplyAdvancedAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateAdvancedCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsync_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", false);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync("my template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.CharacterCreatures))]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.CharacterCreatures))]
        public void GenerateRandomOfTemplateAsCharacter_GenerateRandomCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.NonCharacterCreatures))]
        public void GenerateRandomOfTemplateAsCharacter_ThrowException_WhenCreatureCannotBeCharacter(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            Assert.That(() => creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template"),
                Throws.InstanceOf<IncompatibleCreatureAsCharacterException>().With.Message.EqualTo($"my template cannot be generated as a character"));
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
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
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureLanguages()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Languages, Is.EqualTo(languages));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_DoNotGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(false);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
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
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedhitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureWithMissingAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, updatedSkills, advancedEquipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_ApplyAdvancedAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity))
                .Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null.And.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateAdvancedCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.Zero));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.CharacterCreatures))]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.CharacterCreatures))]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateRandomCreatureName(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.NonCharacterCreatures))]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_ThrowException_WhenTemplateCannotBeCharacter(string creatureName)
        {
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAll()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            Assert.That(async () => await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template"),
                Throws.InstanceOf<IncompatibleCreatureAsCharacterException>().With.Message.EqualTo("my template cannot be generated as a character"));
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSize()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Size, Is.EqualTo("size"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSpace()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureReach()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureCanUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.CanUseEquipment, Is.True);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureCannotUseEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.CanUseEquipment = false;
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.CanUseEquipment, Is.False);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureChallengeRating()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateNoCreatureLevelAdjustment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.LevelAdjustment, Is.Zero);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureCasterLevel()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureNumberOfHands()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureType()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureTypeWithSubtype()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureHitPoints()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
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
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureEquipment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureMagic()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Magic, Is.EqualTo(magic));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_DoNotGenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(false);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
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
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreature()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedhitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureWithExistingRacialAdjustments()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureWithMissingAbilities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName)).Returns(true);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
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

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(skills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureSkills()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureSpecialQualities()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
                It.Is<CreatureType>(c => c.Name == types[0]),
                advancedHitPoints,
                abilities,
                advancedSkills,
                creatureData.CanUseEquipment,
                "advanced size",
                alignment)
            ).Returns(advancedSpecialQualities);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureBaseAttackBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureAttacks()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Feats, Is.EqualTo(feats));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, updatedSkills, advancedEquipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateNoGrappleBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus(creatureName, "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.GrappleBonus, Is.Null);

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_ApplyAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_ApplyAdvancedAttackBonuses()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonus()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSpeeds()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
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

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureArmorClass()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator.Setup(s => s.GenerateAttacks(creatureName, creatureData.Size, "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity)).Returns(advancedAttacks);

            var advancedSkills = new List<Skill>() { new Skill("advanced skill", abilities.First().Value, 1000) };
            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    advancedHitPoints,
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    abilities,
                    creatureData.CanUseEquipment,
                    "advanced size",
                    true))
                .Returns(advancedSkills);

            var advancedSpecialQualities = new List<Feat>() { new Feat() { Name = "advanced special quality" } };

            mockFeatsGenerator.Setup(g => g.GenerateSpecialQualities(
                creatureName,
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
                    creatureName,
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
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    It.IsAny<IEnumerable<Feat>>(),
                    1336 + 8245,
                    advancedEquipment))
                .Returns(advancedArmorClass);

            mockAbilitiesGenerator
                .Setup(g => g.SetMaxBonuses(abilities, advancedEquipment))
                .Returns(abilities);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(creatureName, advancedSkills, advancedEquipment))
                .Returns(advancedSkills);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateAdvancedCreatureSaves()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var advancedHitPoints = SetUpCreatureAdvancement(false, creatureName: creatureName);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith(creatureName, It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Saves, Is.EqualTo(saves));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureAlignment()
        {
            var creatureName = CreatureConstants.Human;
            SetUpCreature(creatureName, "my template", true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }
    }
}