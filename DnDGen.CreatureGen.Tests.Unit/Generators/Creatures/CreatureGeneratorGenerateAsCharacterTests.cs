using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateAsCharacterTests : CreatureGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            SetUpCreature("creature", "template", true);
        }

        [Test]
        public void GenerateAsCharacter_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_IfCreatureHasNotHitDice_ChallengeRatingIsZero()
        {
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }

        [Test]
        public void GenerateAsCharacter_InvalidCreatureTemplateComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, "creature", "template", null, null)).Returns(false);

            Assert.That(() => creatureGenerator.Generate("creature", "template", true),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tCreature: creature\n\tTemplate: template"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureName()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSize()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpace()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureReach()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCanUseEquipment()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCannotUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.CanUseEquipment = false;

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureChallengeRating()
        {
            SetUpCreature("creature", "template", true);
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public void GenerateAsCharacter_GenerateNoCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLevelAdjustmentOf0()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureCasterLevel()
        {
            SetUpCreature("creature", "template", true);
            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureNumberOfHands()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureType()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureTypeWithSubtype()
        {
            SetUpCreature("creature", "template", true);
            types.Add("subtype");

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureTypeWithMultipleSubtypes()
        {
            SetUpCreature("creature", "template", true);
            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.Generate("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureHitPoints()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureMagic()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureLanguages()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GenerateAsCharacter_DoNotGenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = creatureGenerator.Generate("creature", "template", true);
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
            var advancedhitPoints = SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template", true);
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
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template", true);
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
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = creatureGenerator.Generate("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureFeats()
        {
            SetUpCreature("creature", "template", true);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public void GenerateAsCharacter_GenerateNoGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public void GenerateAsCharacter_ApplyAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateAsCharacter_ApplyAdvancedAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSpeeds()
        {
            SetUpCreature("creature", "template", true);
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAsCharacter_GenerateAdvancedCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureAlignment()
        {
            SetUpCreature("creature", "template", true);
            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public void GenerateAsCharacter_GenerateCreatureModifiedByTemplate()
        {
            SetUpCreature("creature", "template", true);
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyTo(It.IsAny<Creature>())).Returns(templateCreature);

            var creature = creatureGenerator.Generate("creature", "template", true);
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_InvalidCreatureTemplateComboThrowsException()
        {
            SetUpCreature("creature", "template", true);
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(true, "creature", "template", null, null)).Returns(false);

            Assert.That(async () => await creatureGenerator.GenerateAsync("creature", "template", true),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo($"Invalid creature:\n\tAs Character: True\n\tCreature: creature\n\tTemplate: template"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureName()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSize()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpace()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureReach()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCanUseEquipment()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCannotUseEquipment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.CanUseEquipment = false;

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureChallengeRating()
        {
            SetUpCreature("creature", "template", true);
            creatureData.ChallengeRating = "challenge rating";

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = 1234;

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateNoCreatureLevelAdjustment()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = null;

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureLevelAdjustmentOf0()
        {
            SetUpCreature("creature", "template", true);
            creatureData.LevelAdjustment = 0;

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureCasterLevel()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureNumberOfHands()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureType()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureTypeWithSubtype()
        {
            SetUpCreature("creature", "template", true);
            types.Add("subtype");

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureTypeWithMultipleSubtypes()
        {
            SetUpCreature("creature", "template", true);
            types.Add("subtype");
            types.Add("other subtype");

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureHitPoints()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureMagic()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_DoNotGenerateAdvancedCreature()
        {
            SetUpCreature("creature", "template", true);
            SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(false);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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
            var advancedhitPoints = SetUpCreatureAdvancement(true);
            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAdvancementSelector.Setup(s => s.IsAdvanced("creature")).Returns(true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSkills()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSpecialQualities()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureBaseAttackBonus()
        {
            SetUpCreature("creature", "template", true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureAttacks()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureFeats()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureHitPointsWithFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSkillsUpdatedByFeats()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            SetUpCreatureAdvancement(true);

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateNoGrappleBonus()
        {
            SetUpCreature("creature", "template", true);
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [Test]
        public async Task GenerateAsCharacterAsync_ApplyAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_ApplyAdvancedAttackBonuses()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonus()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity()
        {
            SetUpCreature("creature", "template", true);
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(true);

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSpeeds()
        {
            SetUpCreature("creature", "template", true);
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureArmorClass()
        {
            SetUpCreature("creature", "template", true);
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

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateAdvancedCreatureSaves()
        {
            SetUpCreature("creature", "template", true);
            var advancedHitPoints = SetUpCreatureAdvancement(true);

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureAlignment()
        {
            SetUpCreature("creature", "template", true);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [Test]
        public async Task GenerateAsCharacterAsync_GenerateCreatureModifiedByTemplate()
        {
            SetUpCreature("creature", "template", true);
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator.Setup(a => a.ApplyToAsync(It.IsAny<Creature>())).ReturnsAsync(templateCreature);

            var creature = await creatureGenerator.GenerateAsync("creature", "template", true);
            Assert.That(creature, Is.EqualTo(templateCreature));
        }
    }
}