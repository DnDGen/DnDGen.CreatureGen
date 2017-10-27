using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using CreatureGen.Combats;
using CreatureGen.Domain.Generators.Abilities;
using CreatureGen.Domain.Generators.Alignments;
using CreatureGen.Domain.Generators.Creatures;
using CreatureGen.Domain.Generators.Classes;
using CreatureGen.Domain.Generators.Combats;
using CreatureGen.Domain.Generators.Feats;
using CreatureGen.Domain.Generators.Items;
using CreatureGen.Domain.Generators.Languages;
using CreatureGen.Domain.Generators.Magics;
using CreatureGen.Domain.Generators.Races;
using CreatureGen.Domain.Generators.Skills;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Items;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Abilities;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using CreatureGen.Skills;
using CreatureGen.Verifiers;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CreatureGen.Tests.Unit.Generators.Characters
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private const string BaseRace = "baserace";
        private const string BaseRacePlusOne = "baserace+1";
        private const string Metarace = "metarace";

        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<ICharacterClassGenerator> mockCharacterClassGenerator;
        private Mock<IRaceGenerator> mockRaceGenerator;
        private Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        private Mock<ILanguageGenerator> mockLanguageGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ICombatGenerator> mockCombatGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IEquipmentGenerator> mockTreasureGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Generator generator;
        private ICharacterGenerator characterGenerator;
        private Mock<ICollectionSelector> mockCollectionsSelector;

        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private Mock<IAbilitiesRandomizer> mockAbilitiesRandomizer;
        private Mock<ISetLevelRandomizer> mockSetLevelRandomizer;

        private Alignment alignment;
        private Alignment alignmentPrototype;
        private CharacterClass characterClass;
        private CharacterClassPrototype expectedCharacterClassPrototype;
        private Race race;
        private RacePrototype expectedRacePrototype;
        private Dictionary<string, int> levelAdjustments;
        private Combat combat;
        private Equipment equipment;
        private BaseAttack baseAttack;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private List<Skill> skills;
        private List<string> languages;
        private Magic magic;
        private List<string> featSkillFoci;
        private FeatCollections featCollections;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            levelAdjustments = new Dictionary<string, int>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();

            characterGenerator = new CharacterGenerator(
                mockAlignmentGenerator.Object,
                mockCharacterClassGenerator.Object,
                mockRaceGenerator.Object,
                mockAdjustmentsSelector.Object,
                mockRandomizerVerifier.Object,
                mockPercentileSelector.Object,
                mockCombatGenerator.Object,
                mockTreasureGenerator.Object,
                mockMagicGenerator.Object,
                generator,
                mockCollectionsSelector.Object,
                mockAbilitiesGenerator.Object,
                mockLanguageGenerator.Object,
                mockSkillsGenerator.Object,
                mockFeatsGenerator.Object);

            levelAdjustments[BaseRace] = 0;
            levelAdjustments[BaseRacePlusOne] = 1;
            levelAdjustments[SizeConstants.Metaraces.None] = 0;
            levelAdjustments[Metarace] = 1;

            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => levelAdjustments[name]);

            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");
            featSkillFoci.Add("skill 3");
            featSkillFoci.Add("skill 4");
            featSkillFoci.Add("skill 5");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.Skills)).Returns(featSkillFoci);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<Alignment>(), It.IsAny<CharacterClassPrototype>(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyRaceCompatibility(It.IsAny<Alignment>(), It.IsAny<CharacterClassPrototype>(), It.IsAny<RacePrototype>())).Returns(true);
        }

        private void SetUpMockRandomizers()
        {
            alignmentPrototype = new Alignment("prototype alignment");
            alignment = new Alignment();
            expectedCharacterClassPrototype = new CharacterClassPrototype();
            characterClass = new CharacterClass();
            expectedRacePrototype = new RacePrototype();
            race = new Race();

            expectedCharacterClassPrototype.Name = "prototype class";
            expectedCharacterClassPrototype.Level = 9266;
            expectedRacePrototype.BaseRace = BaseRace;
            expectedRacePrototype.Metarace = Metarace;

            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockAbilitiesRandomizer = new Mock<IAbilitiesRandomizer>();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();

            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(() => alignmentPrototype);
            mockClassNameRandomizer.Setup(r => r.Randomize(alignmentPrototype)).Returns(() => expectedCharacterClassPrototype.Name);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(() => expectedCharacterClassPrototype.Level);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignmentPrototype, It.Is<CharacterClassPrototype>(p => p.Name == expectedCharacterClassPrototype.Name && p.Level == expectedCharacterClassPrototype.Level))).Returns(() => expectedRacePrototype.BaseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignmentPrototype, It.Is<CharacterClassPrototype>(p => p.Name == expectedCharacterClassPrototype.Name && p.Level == expectedCharacterClassPrototype.Level))).Returns(() => expectedRacePrototype.Metarace);

            mockSetLevelRandomizer.SetupAllProperties();
            mockSetLevelRandomizer.Setup(r => r.Randomize()).Returns(() => mockSetLevelRandomizer.Object.SetLevel);
        }

        private void SetUpGenerators()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockCombatGenerator = new Mock<ICombatGenerator>();
            mockTreasureGenerator = new Mock<IEquipmentGenerator>();
            mockCharacterClassGenerator = new Mock<ICharacterClassGenerator>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            generator = new ConfigurableIterationGenerator(4);
            equipment = new Equipment();
            combat = new Combat();
            baseAttack = new BaseAttack();
            feats = new List<Feat>();
            magic = new Magic();
            abilities = new Dictionary<string, Ability>();
            languages = new List<string>();
            skills = new List<Skill>();
            featSkillFoci = new List<string>();
            featCollections = new FeatCollections();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.Level = 1;
            characterClass.Name = "class name";
            race.BaseRace = BaseRace;
            race.Metarace = SizeConstants.Metaraces.None;
            abilities["ability"] = new Ability("ability");
            languages.Add("1337");
            languages.Add("Dothraki");
            featCollections.Additional = feats;

            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<Dictionary<string, Ability>>())).Returns(() => new BaseAttack());
            mockTreasureGenerator.Setup(g => g.GenerateWith(It.IsAny<IEnumerable<Feat>>(), It.IsAny<CharacterClass>(), It.IsAny<Race>())).Returns(() => new Equipment());
            mockCombatGenerator.Setup(g => g.GenerateWith(It.IsAny<BaseAttack>(), It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Dictionary<string, Ability>>(), It.IsAny<Equipment>())).Returns(() => new Combat());
            mockMagicGenerator.Setup(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<Dictionary<string, Ability>>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Equipment>())).Returns(() => new Magic());

            mockAlignmentGenerator.Setup(g => g.GeneratePrototype(mockAlignmentRandomizer.Object)).Returns(alignmentPrototype);
            mockAlignmentGenerator.Setup(g => g.GenerateWith(alignmentPrototype)).Returns(alignment);

            mockCharacterClassGenerator.Setup(g => g.GeneratePrototype(alignmentPrototype, mockClassNameRandomizer.Object, mockLevelRandomizer.Object)).Returns(expectedCharacterClassPrototype);
            mockCharacterClassGenerator.Setup(g => g.GenerateWith(alignment, expectedCharacterClassPrototype)).Returns(characterClass);

            mockRaceGenerator.Setup(g => g.GeneratePrototype(alignmentPrototype, expectedCharacterClassPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(expectedRacePrototype);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, expectedRacePrototype)).Returns(race);

            mockAbilitiesGenerator.Setup(g => g.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race)).Returns(abilities);
            mockLanguageGenerator.Setup(g => g.GenerateWith(race, characterClass, abilities, skills)).Returns(languages);
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, abilities)).Returns(baseAttack);
            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities, skills, baseAttack)).Returns(featCollections);

            //INFO: Becaue the "All" on feat collections is dynamic, we can't test for it specifically
            mockTreasureGenerator.Setup(g => g.GenerateWith(It.IsAny<IEnumerable<Feat>>(), characterClass, race)).Returns(equipment);
            mockCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, It.IsAny<IEnumerable<Feat>>(), abilities, equipment)).Returns(combat);
            mockMagicGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, abilities, It.IsAny<IEnumerable<Feat>>(), equipment)).Returns(magic);
        }

        [Test]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false);

            Assert.That(() => GenerateCharacter(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        private Character GenerateCharacter()
        {
            return characterGenerator.GenerateWith(
                mockAlignmentRandomizer.Object,
                mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object,
                mockAbilitiesRandomizer.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyAlignmentCompatibility(
                    alignmentPrototype,
                    mockClassNameRandomizer.Object,
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockAlignmentGenerator.Verify(f => f.GeneratePrototype(It.IsAny<IAlignmentRandomizer>()), Times.Exactly(2));
            mockAlignmentGenerator.Verify(f => f.GeneratePrototype(mockAlignmentRandomizer.Object), Times.Exactly(2));
            mockAlignmentGenerator.Verify(f => f.GenerateWith(It.IsAny<Alignment>()), Times.Once);
        }

        [Test]
        public void NullAlignmentIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyAlignmentCompatibility(
                    It.IsAny<Alignment>(),
                    mockClassNameRandomizer.Object,
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false);

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyCharacterClassCompatibility(
                    alignmentPrototype,
                    It.Is<CharacterClassPrototype>(p => p.Name == expectedCharacterClassPrototype.Name && p.Level == expectedCharacterClassPrototype.Level),
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockCharacterClassGenerator.Verify(f => f.GeneratePrototype(It.IsAny<Alignment>(), It.IsAny<IClassNameRandomizer>(), It.IsAny<ILevelRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(f => f.GeneratePrototype(alignmentPrototype, mockClassNameRandomizer.Object, mockLevelRandomizer.Object), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(f => f.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClassPrototype>()), Times.Once);
        }

        [Test]
        public void NullCharacterClassIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyCharacterClassCompatibility(
                    It.IsAny<Alignment>(),
                    It.IsAny<CharacterClassPrototype>(),
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false);

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void IncompatibleRaceIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyRaceCompatibility(
                    alignmentPrototype,
                    It.Is<CharacterClassPrototype>(p => p.Name == expectedCharacterClassPrototype.Name && p.Level == expectedCharacterClassPrototype.Level),
                    It.Is<RacePrototype>(p => p.BaseRace == expectedRacePrototype.BaseRace && p.Metarace == expectedRacePrototype.Metarace)))
                .Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GeneratePrototype(It.IsAny<Alignment>(), It.IsAny<CharacterClassPrototype>(), It.IsAny<RaceRandomizer>(), It.IsAny<RaceRandomizer>()), Times.Exactly(2));
            mockRaceGenerator.Verify(f => f.GeneratePrototype(alignmentPrototype, expectedCharacterClassPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object), Times.Exactly(2));
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), It.IsAny<RacePrototype>()), Times.Once);
        }

        [Test]
        public void NullRaceIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyRaceCompatibility(
                    alignmentPrototype,
                    It.IsAny<CharacterClassPrototype>(),
                    It.IsAny<RacePrototype>()))
                .Returns(false);

            characterClass.Level = 2;

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(2));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(1));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(3));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.Metarace = Metarace;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(2));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(1));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(3));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(3));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(2));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(5));
        }

        [Test]
        public void RegenerateSpecialistFields()
        {
            mockCharacterClassGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(new[] { "new specialist field", "other new specialist field" });
            GenerateCharacter();
            Assert.That(characterClass.SpecialistFields, Contains.Item("new specialist field"));
            Assert.That(characterClass.SpecialistFields, Contains.Item("other new specialist field"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultSelector()
        {
            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.Traits)).Returns("interesting trait");
            var character = GenerateCharacter();
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }

        [Test]
        public void GetBaseAttackFromCombatGenerator()
        {
            GenerateCharacter();
            mockCombatGenerator.Verify(g => g.GenerateBaseAttackWith(characterClass, race, abilities), Times.Once);
        }

        [Test]
        public void GetEquipmentFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GetCombatFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Combat, Is.EqualTo(combat));
        }

        [Test]
        public void GetMagicFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void DoNotAdjustLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(3));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(5));
        }

        [Test]
        public void DoNotAdjustNPCLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;
            characterClass.IsNPC = true;

            mockSetLevelRandomizer.Object.SetLevel = 42;
            expectedCharacterClassPrototype.Level = 42;

            mockCharacterClassGenerator.Setup(g => g.GeneratePrototype(alignmentPrototype, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object)).Returns(expectedCharacterClassPrototype);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(3));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(2));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForArmor()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -9266 };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
        }

        [Test]
        public void DoNotApplyPenaltyCheckForArmorIfNoArmor()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = null;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForShield()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
        }

        [Test]
        public void DoNotApplyPenaltyCheckForNonShields()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.OffHand = new Item { Name = "shield" };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForArmorAndShield()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -42 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9308));
        }

        [Test]
        public void SwimTakesDoubleArmorCheckPenalty()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });
            skills.Add(new Skill(SkillConstants.Swim, abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -42 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9308));
            Assert.That(character.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(-9308 * 2));
        }

        [Test]
        public void SwimTakesNoPenaltyForPlateArmorOfTheDeep()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });
            skills.Add(new Skill(SkillConstants.Swim, abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = new Armor { Name = ArmorConstants.PlateArmorOfTheDeep, ArmorCheckPenalty = -9266 };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
            Assert.That(character.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void SwimTakesPenaltyForShieldWithPlateArmorOfTheDeep()
        {
            skills.Add(new Skill("skill", abilities["ability"], 1) { HasArmorCheckPenalty = false });
            skills.Add(new Skill("other skill", abilities["ability"], 1) { HasArmorCheckPenalty = true });
            skills.Add(new Skill(SkillConstants.Swim, abilities["ability"], 1) { HasArmorCheckPenalty = true });

            equipment.Armor = new Armor { Name = ArmorConstants.PlateArmorOfTheDeep, ArmorCheckPenalty = -9266 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -90210 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);

            Assert.That(character.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266 - 90210));
            Assert.That(character.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(-90210 * 2));
        }

        [Test]
        public void GetAbilitiesFromAbilitiesGenerator()
        {
            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GetSkillsFromSkillGenerator()
        {
            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GetLanguagesFromLanguageGenerator()
        {
            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GetFeatsFromFeatGenerator()
        {
            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Feats, Is.EqualTo(featCollections));
        }

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkills()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1));
            skills.Add(new Skill("skill 4", baseAbility, 1));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            skills[2].Bonus = 3;
            skills[3].Bonus = 4;

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Power = 2;
            feats[2].Name = "feat3";
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 4" });

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[0])).Bonus, Is.EqualTo(2));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[1])).Bonus, Is.EqualTo(5));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[2])).Bonus, Is.EqualTo(3));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[3])).Bonus, Is.EqualTo(7));
        }

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkillsWithFocus()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1, "other focus"));
            skills.Add(new Skill("skill 3", baseAbility, 1, "focus"));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            skills[2].Bonus = 3;
            skills[3].Bonus = 4;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Power = 2;
            feats[2].Name = "feat3";
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 3/focus" });

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[0])).Bonus, Is.EqualTo(2));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[1])).Bonus, Is.EqualTo(5));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[2])).Bonus, Is.EqualTo(3));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[3])).Bonus, Is.EqualTo(7));
        }

        [Test]
        public void IfFocusIsSkill_ApplyBonusToThatSkill()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            skills[2].Bonus = 3;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 2", "skill 3", "non-skill focus" };
            feats[0].Power = 4;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 3", "non-skill focus" };
            feats[1].Power = 1;
            feats[2].Name = "feat1";
            feats[2].Foci = new[] { "skill 2", "non-skill focus" };
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").Bonus, Is.EqualTo(1));
            Assert.That(character.Skills.First(s => s.Name == "skill 2").Bonus, Is.EqualTo(9));
            Assert.That(character.Skills.First(s => s.Name == "skill 3").Bonus, Is.EqualTo(8));
        }

        [Test]
        public void IfFocusIsSkillWithFocus_ApplyBonusToThatSkill()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 1"));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 2"));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            skills[2].Bonus = 3;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 2", "skill 1/focus 2", "non-skill focus" };
            feats[0].Power = 4;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 1/focus 2", "non-skill focus" };
            feats[1].Power = 1;
            feats[2].Name = "feat1";
            feats[2].Foci = new[] { "skill 2", "non-skill focus" };
            feats[2].Power = 3;

            featSkillFoci.Add("skill 1/focus 1");
            featSkillFoci.Add("skill 1/focus 2");
            featSkillFoci.Add("skill 1/focus 3");
            featSkillFoci.Remove("skill 1"); //INFO: Doing this because a skill either has focus all the time or never

            featCollections.Additional = feats;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[0])).Bonus, Is.EqualTo(1));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[1])).Bonus, Is.EqualTo(9));
            Assert.That(character.Skills.First(s => s.IsEqualTo(skills[2])).Bonus, Is.EqualTo(8));
        }

        [Test]
        public void OnlyApplySkillFeatToSkillsIfSkillFocusIsPurelySkill()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills[0].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "non-skill focus" };
            feats[0].Power = 1;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            featCollections.Additional = feats;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").Bonus, Is.EqualTo(1));
        }

        [Test]
        public void NoCircumstantialBonusIfBonusApplied()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 2", "non-skill focus" };
            feats[1].Power = 2;

            featCollections.Additional = feats;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").Bonus, Is.EqualTo(2));
            Assert.That(character.Skills.First(s => s.Name == "skill 1").CircumstantialBonus, Is.False);
            Assert.That(character.Skills.First(s => s.Name == "skill 2").Bonus, Is.EqualTo(4));
            Assert.That(character.Skills.First(s => s.Name == "skill 2").CircumstantialBonus, Is.False);
        }

        [Test]
        public void IfSkillBonusFocusIsNotPurelySkill_MarkSkillAsHavingCircumstantialBonus()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills[0].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "non-skill focus" };
            feats[0].Power = 1;

            featCollections.Additional = feats;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").CircumstantialBonus, Is.True);
        }

        [Test]
        public void MarkSkillWithCircumstantialBonusWhenOtherFociDoNotHaveCircumstantialBonus()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "skill 2" };
            feats[0].Power = 1;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").CircumstantialBonus, Is.True);
            Assert.That(character.Skills.First(s => s.Name == "skill 2").CircumstantialBonus, Is.False);
        }

        [Test]
        public void CircumstantialBonusIsNotOverwritten()
        {
            var baseAbility = new Ability("base ability");

            skills.Clear();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].Bonus = 1;
            skills[1].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, abilities)).Returns(skills);

            feats.Clear();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "skill 2" };
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 1" };
            feats[1].Power = 1;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(character.Skills.First(s => s.Name == "skill 1").CircumstantialBonus, Is.True);
            Assert.That(character.Skills.First(s => s.Name == "skill 2").CircumstantialBonus, Is.False);
        }
    }
}