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
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    internal abstract class CreatureGeneratorTests
    {
        protected Mock<IAlignmentGenerator> mockAlignmentGenerator;
        protected Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        protected Mock<ISkillsGenerator> mockSkillsGenerator;
        protected Mock<IFeatsGenerator> mockFeatsGenerator;
        protected Mock<ICreatureVerifier> mockCreatureVerifier;
        protected ICreatureGenerator creatureGenerator;
        protected Mock<ICollectionSelector> mockCollectionSelector;
        protected Mock<IHitPointsGenerator> mockHitPointsGenerator;
        protected Mock<IArmorClassGenerator> mockArmorClassGenerator;
        protected Mock<ISavesGenerator> mockSavesGenerator;
        protected Mock<ICreatureDataSelector> mockCreatureDataSelector;
        protected Mock<JustInTimeFactory> mockJustInTimeFactory;
        protected Mock<IAdvancementSelector> mockAdvancementSelector;
        protected Mock<IAttacksGenerator> mockAttacksGenerator;
        protected Mock<ISpeedsGenerator> mockSpeedsGenerator;
        protected Mock<IEquipmentGenerator> mockEquipmentGenerator;
        protected Mock<IMagicGenerator> mockMagicGenerator;
        protected Mock<ILanguageGenerator> mockLanguageGenerator;

        protected Dictionary<string, Ability> abilities;
        protected List<Skill> skills;
        protected List<Feat> specialQualities;
        protected List<Feat> feats;
        protected CreatureDataSelection creatureData;
        protected HitPoints hitPoints;
        protected List<string> types;
        protected List<Attack> attacks;
        protected ArmorClass armorClass;
        protected Dictionary<string, Measurement> speeds;
        protected Alignment alignment;
        protected Equipment equipment;
        protected Magic magic;
        protected List<string> languages;

        [SetUp]
        public void GeneratorSetup()
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

            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, It.IsAny<IEnumerable<Feat>>(), abilities)).Returns(skills);
            mockHitPointsGenerator.Setup(g => g.RegenerateWith(hitPoints, It.IsAny<IEnumerable<Feat>>())).Returns(hitPoints);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>())).Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<(string, string)>>())).Returns((IEnumerable<(string, string)> c) => c.First());
            mockCollectionSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Collection.CreatureGroups, types[0],
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack)).Returns(GroupConstants.PoorBaseAttack);
        }

        protected List<Mock<TemplateApplicator>> SetUpCreature(
            string creatureName,
            bool asCharacter,
            string typeFilter = null,
            string crFilter = null,
            string alignmentFilter = null,
            AbilityRandomizer randomizer = null,
            params string[] templateNames)
        {
            var creatures = new[] { creatureName, "other creature name", "wrong creature name" };
            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            mockAlignmentGenerator.Setup(g => g.Generate(creatureName, templateNames, alignmentFilter)).Returns(alignment);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), hitPoints)).Returns(753);
            mockAttacksGenerator.Setup(g => g.GenerateAttacks(creatureName, creatureData.Size, creatureData.Size, 753, abilities, hitPoints.RoundedHitDiceQuantity)).Returns(attacks);
            mockAttacksGenerator.Setup(g => g.ApplyAttackBonuses(attacks, feats, abilities)).Returns(attacks);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    creatureName,
                    It.Is<CreatureType>(c => c.Name == types[0]),
                    hitPoints,
                    abilities,
                    skills,
                    creatureData.CanUseEquipment,
                    creatureData.Size,
                    alignment))
                .Returns(specialQualities);

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

            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    It.Is<string>(c => c == null || c == creatureName),
                    It.Is<Filters>(f => f == null
                        || ((!f.Templates.Except(templateNames).Any())
                            && f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter)
                        || (!f.Templates.Except(templateNames).Any()))))
                .Returns(true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, creatureName, It.Is<Filters>(f => f != null
                && !f.Templates.Except(templateNames).Any()))).Returns(true);
            mockCreatureDataSelector.Setup(s => s.SelectFor(creatureName)).Returns(creatureData);

            mockFeatsGenerator
                .Setup(g =>
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
                    ))
                .Returns(feats);

            var templateApplicators = new List<Mock<TemplateApplicator>>();
            foreach (var templateName in templateNames.Where(t => !string.IsNullOrEmpty(t)))
            {
                var defaultTemplateApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(templateName)).Returns(defaultTemplateApplicator.Object);

                defaultTemplateApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));
                defaultTemplateApplicator
                    .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc
                        .Intersect(new[] { creatureName })
                        .Select(c => new CreaturePrototype { Name = c }));
                defaultTemplateApplicator
                    .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<CreaturePrototype>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<CreaturePrototype> pp, bool asC, Filters f) => pp
                        .Where(p => p.Name == creatureName));
                defaultTemplateApplicator
                    .Setup(a => a.ApplyTo(It.IsAny<Creature>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Callback((Creature c, bool asC, Filters f) => c.Templates.Add(templateName))
                    .Returns((Creature c, bool asC, Filters f) => c);
                defaultTemplateApplicator
                    .Setup(a => a.ApplyToAsync(It.IsAny<Creature>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Callback((Creature c, bool asC, Filters f) => c.Templates.Add(templateName))
                    .ReturnsAsync((Creature c, bool asC, Filters f) => c);

                templateApplicators.Add(defaultTemplateApplicator);
            }

            if (!templateNames.Any(t => !string.IsNullOrEmpty(t) && t == CreatureConstants.Templates.None))
            {
                mockCreatureVerifier
                    .Setup(v => v.VerifyCompatibility(
                        asCharacter,
                        It.Is<string>(c => c == null || c == creatureName),
                        It.Is<Filters>(f => f == null
                            || (f.Templates.IsEquivalentTo(new[] { CreatureConstants.Templates.None })
                                && f.Type == typeFilter
                                && f.ChallengeRating == crFilter
                                && f.Alignment == alignmentFilter)
                            || f.Templates.IsEquivalentTo(new[] { CreatureConstants.Templates.None }))))
                    .Returns(true);

                var noneApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(noneApplicator.Object);

                noneApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));
                noneApplicator
                    .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc
                        .Intersect(new[] { creatureName })
                        .Select(c => new CreaturePrototype { Name = c }));
                noneApplicator
                    .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<CreaturePrototype>>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Returns((IEnumerable<CreaturePrototype> pp, bool asC, Filters f) => pp
                        .Where(p => p.Name == creatureName));
                noneApplicator
                    .Setup(a => a.ApplyTo(It.IsAny<Creature>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Callback((Creature c, bool asC, Filters f) => c.Templates.Add(CreatureConstants.Templates.None))
                    .Returns((Creature c, bool asC, Filters f) => c);
                noneApplicator
                    .Setup(a => a.ApplyToAsync(It.IsAny<Creature>(), asCharacter, It.Is<Filters>(f => f == null
                        || (f.Type == typeFilter
                            && f.ChallengeRating == crFilter
                            && f.Alignment == alignmentFilter))))
                    .Callback((Creature c, bool asC, Filters f) => c.Templates.Add(CreatureConstants.Templates.None))
                    .ReturnsAsync((Creature c, bool asC, Filters f) => c);

                templateApplicators.Add(noneApplicator);
            }

            if (randomizer == null)
            {
                mockAbilitiesGenerator
                    .Setup(g => g.GenerateFor(creatureName, It.Is<AbilityRandomizer>(r => r.Roll == AbilityConstants.RandomizerRolls.Default
                        && r.PriorityAbility == null
                        && !r.AbilityAdvancements.Any()
                        && !r.SetRolls.Any())))
                    .Returns(abilities);
            }
            else
            {
                mockAbilitiesGenerator.Setup(g => g.GenerateFor(creatureName, randomizer)).Returns(abilities);
            }

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

            return templateApplicators;
        }

        protected HitPoints SetUpCreatureAdvancement(bool asCharacter, string creatureName, string challengeRatingFilter, int advancementAmount = 1337, params string[] templates)
        {
            mockAdvancementSelector.Setup(s => s.IsAdvanced(creatureName, challengeRatingFilter)).Returns(true);

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

            var nonEmptyTemplates = templates.Where(t => !string.IsNullOrEmpty(t));
            mockAdvancementSelector
                .Setup(s => s.SelectRandomFor(creatureName, nonEmptyTemplates, It.Is<CreatureType>(c => c.Name == types[0]), creatureData.Size, creatureData.ChallengeRating))
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
    }
}
