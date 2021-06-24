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
    public class CreatureGeneratorRandomOfTemplateAsCharacterTests
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
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

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

            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, templateName, null, null)).Returns(true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, creatureName, templateName, null, null)).Returns(true);
            mockCreatureVerifier.Setup(v => v.CanBeCharacter(creatureName)).Returns(asCharacter);
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
        public void GenerateRandomNameOfTemplateAsCharacter_GenerateCreatureName()
        {
            var creatureName = "my creature";
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, "my template", null, null)).Returns(true);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { creatureName, "other creature name", "wrong creature name" });

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomNameOfTemplateAsCharacter("my template");
            Assert.That(name, Is.EqualTo(creatureName));
        }

        [Test]
        public void GenerateRandomNameOfTemplateAsCharacter_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, "my template", null, null)).Returns(true);

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomNameOfTemplateAsCharacter("my template");
            Assert.That(name, Is.EqualTo(creatureName));
        }

        [Test]
        public void GenerateRandomNameOfTemplateAsCharacter_ThrowException_WhenCreatureCannotBeCharacter()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, "my template", null, null)).Returns(false);

            Assert.That(() => creatureGenerator.GenerateRandomNameOfTemplateAsCharacter("my template"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tTemplate: my template"));
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", true);

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", true);

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.Is<Creature>(c => c.Name == creatureName))).Returns((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyTo(creature), Times.Once);
        }

        [Test]
        public void GenerateRandomOfTemplateAsCharacter_ThrowException_WhenCreatureCannotBeCharacter()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, "my template", null, null)).Returns(false);

            Assert.That(() => creatureGenerator.GenerateRandomOfTemplateAsCharacter("my template"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tTemplate: my template"));
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

            SetUpCreatureAdvancement(true, creatureName: creatureName);
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

            var advancedhitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);
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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));

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
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", true);

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(creatureName)).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_GenerateRandomCreatureName()
        {
            var creatureName = "my creature";
            SetUpCreature(creatureName, "my template", true);

            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator.Setup(a => a.IsCompatible(It.IsAny<string>())).Returns(true);
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.Is<Creature>(c => c.Name == creatureName))).ReturnsAsync((Creature c) => c);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo(CreatureConstants.GetAllCharacters()))))
                .Returns(creatureName);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template");
            Assert.That(creature.Name, Is.EqualTo(creatureName));

            mockTemplateApplicator.Verify(a => a.ApplyToAsync(creature), Times.Once);
        }

        [Test]
        public async Task GenerateRandomOfTemplateAsCharacterAsync_ThrowException_WhenTemplateCannotBeCharacter()
        {
            var creatureName = "my creature";
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, null, "my template", null, null)).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync("my template"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo("Invalid creature:\n\tAs Character: True\n\tTemplate: my template"));
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

            SetUpCreatureAdvancement(true, creatureName: creatureName);
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

            var advancedhitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);
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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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

            var advancedHitPoints = SetUpCreatureAdvancement(true, creatureName: creatureName);

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