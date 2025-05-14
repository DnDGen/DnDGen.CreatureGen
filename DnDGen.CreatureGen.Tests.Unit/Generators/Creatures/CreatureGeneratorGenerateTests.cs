using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateTests : CreatureGeneratorTests
    {
        [Test]
        public void Generate_ReturnsHasSkeleton()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_InvalidCreatureTemplateComboThrowsException(bool asCharacter)
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, "creature", filters)).Returns(false);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine("\tCreature: creature");
            message.AppendLine("\tTemplate: template");

            Assert.That(() => creatureGenerator.Generate(asCharacter, "creature", null, "template"),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureName(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Name, Is.EqualTo("creature"));
        }


        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSize(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Size, Is.EqualTo("size"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSpace(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Space.Value, Is.EqualTo(56.78));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureReach(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Reach.Value, Is.EqualTo(67.89));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureCanUseEquipment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.CanUseEquipment, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureCannotUseEquipment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            creatureData.CanUseEquipment = false;

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.CanUseEquipment, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureChallengeRating(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            creatureData.ChallengeRating = "challenge rating";

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureLevelAdjustment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            creatureData.LevelAdjustment = 1234;

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.LevelAdjustment, Is.EqualTo(1234));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateNoCreatureLevelAdjustment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            creatureData.LevelAdjustment = null;

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureLevelAdjustmentOf0(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            creatureData.LevelAdjustment = 0;

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.LevelAdjustment, Is.Zero);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureCasterLevel(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.CasterLevel, Is.EqualTo(1029));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureNumberOfHands(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.NumberOfHands, Is.EqualTo(96));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureType(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureTypeWithSubtype(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            types.Add("subtype");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(1));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureTypeWithMultipleSubtypes(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            types.Add("subtype");
            types.Add("other subtype");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Type.Name, Is.EqualTo("type"));
            Assert.That(creature.Type.SubTypes, Is.Not.Empty);
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype"));
            Assert.That(creature.Type.SubTypes, Contains.Item("other subtype"));
            Assert.That(creature.Type.SubTypes.Count, Is.EqualTo(2));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateDemographics(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Demographics, Is.Not.Null.And.EqualTo(demographics));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureAbilities(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Abilities, Is.EqualTo(abilities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureHitPoints(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.HitPoints, Is.EqualTo(hitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(9266));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(42));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureEquipment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Equipment, Is.EqualTo(equipment));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureMagic(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Magic, Is.EqualTo(magic));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureLanguages(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Languages, Is.EqualTo(languages));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_DoNotGenerateAdvancedCreature(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");
            mockAdvancementSelector
                .Setup(s => s.IsAdvanced("creature", It.Is<IEnumerable<string>>(t => t.IsEquivalentTo(new[] { "template" })), null))
                .Returns(false);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
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

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreature(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
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
            Assert.That(creature.ChallengeRating, Is.EqualTo("2683"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.SizeIncreases))]
        public void Generate_GenerateAdvancedCreature_AdjustDemographics(string originalSize, string advancedSize, int heightMultiplier, int weightMultiplier)
        {
            creatureData.Size = originalSize;
            demographics.Weight.Value = 2;
            demographics.Height.Value = 2435;
            demographics.Length.Value = 922;
            demographics.Wingspan.Value = 2022;
            demographics.Age.Value = 3546;
            demographics.MaximumAge.Value = 4657;

            SetUpCreature("creature", false, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(false, "creature", null, 1337, advancedSize, "template");

            var creature = creatureGenerator.Generate(false, "creature", null, "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedHitPoints));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(681));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(573));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(492));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(862));
            Assert.That(creature.Size, Is.EqualTo(advancedSize));
            Assert.That(creature.Space.Value, Is.EqualTo(54.32));
            Assert.That(creature.Reach.Value, Is.EqualTo(98.76));
            Assert.That(creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment, Is.EqualTo(3456));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment, Is.EqualTo(783));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment, Is.EqualTo(69));
            Assert.That(creature.ChallengeRating, Is.EqualTo("2683"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);

            Assert.That(creature.Demographics.Age.Value, Is.EqualTo(3546));
            Assert.That(creature.Demographics.MaximumAge.Value, Is.EqualTo(4657));
            Assert.That(creature.Demographics.Height.Value, Is.EqualTo(2435 * heightMultiplier));
            Assert.That(creature.Demographics.Length.Value, Is.EqualTo(922 * heightMultiplier));
            Assert.That(creature.Demographics.Wingspan.Value, Is.EqualTo(2022 * heightMultiplier));
            Assert.That(creature.Demographics.Weight.Value, Is.EqualTo(2 * weightMultiplier));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureWithExistingRacialAdjustments(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Strength].RacialAdjustment = 38;
            abilities[AbilityConstants.Dexterity].RacialAdjustment = 47;
            abilities[AbilityConstants.Constitution].RacialAdjustment = 56;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
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
            Assert.That(creature.ChallengeRating, Is.EqualTo("2683"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureWithMissingAbilities(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Strength].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
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
            Assert.That(creature.ChallengeRating, Is.EqualTo("2683"));
            Assert.That(creature.CasterLevel, Is.EqualTo(1029 + 6331));
            Assert.That(creature.IsAdvanced, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSkills(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Skills, Is.EqualTo(skills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureSkills(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Skills, Is.EqualTo(advancedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSpecialQualities(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(specialQualities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureSpecialQualities(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.SpecialQualities, Is.EqualTo(advancedSpecialQualities));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureBaseAttackBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(753));
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureBaseAttackBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            mockAttacksGenerator.Setup(g => g.GenerateBaseAttackBonus(It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints)).Returns(951);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(951));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureAttacks(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Attacks, Is.EqualTo(attacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureAttacks(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAdvancedAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Feats, Is.EqualTo(feats));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Feats, Is.EqualTo(advancedFeats));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureHitPointsWithFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureHitPointsWithFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.HitPoints, Is.EqualTo(advancedUpdatedHitPoints));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSkillsUpdatedByFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var updatedSkills = new List<Skill>() { new Skill("updated skill", abilities.First().Value, 1000) };
            mockSkillsGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            mockSkillsGenerator
                .Setup(g => g.SetArmorCheckPenalties(
                    "creature",
                    updatedSkills,
                    equipment))
                .Returns(updatedSkills);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureSkillsUpdatedByFeats(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Skills, Is.EqualTo(updatedSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "advanced size", 999, abilities[AbilityConstants.Strength])).Returns(2345);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.GrappleBonus, Is.EqualTo(2345));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateNoGrappleBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            int? noBonus = null;
            mockAttacksGenerator.Setup(s => s.GenerateGrappleBonus("creature", "size", 753, abilities[AbilityConstants.Strength])).Returns(noBonus);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.GrappleBonus, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_ApplyAttackBonuses(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_ApplyAdvancedAttackBonuses(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Attacks, Is.EqualTo(equipmentAttacks));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureInitiativeBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureInitiativeBonus(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureInitiativeBonusWithImprovedInitiative(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiative(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 4132;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + 4));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureInitiativeBonusWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(hitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.Zero);
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(612));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureInitiativeBonusWithImprovedInitiativeWithoutDexterity(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Intelligence].BaseScore = 1234;

            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            feats.Add(new Feat { Name = "other feat", Power = 4 });
            feats.Add(new Feat { Name = FeatConstants.Initiative_Improved, Power = 4 });
            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.InitiativeBonus, Is.EqualTo(4));
            Assert.That(creature.TotalInitiativeBonus, Is.EqualTo(616));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSpeeds(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            speeds["on foot"] = new Measurement("feet per round");
            speeds["in a car"] = new Measurement("feet per round");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Speeds, Is.EqualTo(speeds));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureArmorClass(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(armorClass));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureArmorClass(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            var advancedAttacks = new[] { new Attack() { Name = "advanced attack" } };
            mockAttacksGenerator
                .Setup(s => s.GenerateAttacks("creature", "advanced size", 999, abilities, advancedHitPoints.RoundedHitDiceQuantity, demographics.Gender))
                .Returns(advancedAttacks);

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

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.EqualTo(advancedArmorClass));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureSaves(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), hitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateAdvancedCreatureSaves(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var advancedHitPoints = SetUpCreatureAdvancement(asCharacter, "creature", null, 1337, null, "template");

            mockFeatsGenerator.Setup(g => g.GenerateFeats(advancedHitPoints, 668 + 4633, abilities, skills, attacks, specialQualities, 1029 + 6331, speeds, 1336, 96, "advanced size", creatureData.CanUseEquipment)).Returns(feats);

            var saves = new Dictionary<string, Save>();
            saves["save name"] = new Save();

            mockSavesGenerator.Setup(g => g.GenerateWith("creature", It.Is<CreatureType>(c => c.Name == types[0]), advancedHitPoints, feats, abilities)).Returns(saves);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Saves, Is.EqualTo(saves));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureAlignment(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.Alignment, Is.EqualTo(alignment));
            Assert.That(creature.Alignment.Full, Is.EqualTo("creature alignment"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureWithoutTemplate(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter);

            var creature = creatureGenerator.Generate(asCharacter, "creature");
            Assert.That(creature.Templates, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureModifiedByTemplate(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template")).Returns(mockTemplateApplicator.Object);

            var templateCreature = new Creature();
            mockTemplateApplicator
                .Setup(a => a.ApplyTo(It.IsAny<Creature>(), asCharacter, It.Is<Filters>(f => f != null
                    && f.Templates.Single() == "template"
                    && f.ChallengeRating == null
                    && f.Type == null
                    && f.Alignment == null)))
                .Returns(templateCreature);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature, Is.EqualTo(templateCreature));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_GenerateCreatureModifiedByMultipleTemplates(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template 1", "template 2");

            var mockTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template 1")).Returns(mockTemplateApplicator1.Object);

            var mockTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("template 2")).Returns(mockTemplateApplicator2.Object);

            var templateCreature1 = new Creature();
            mockTemplateApplicator1
                .Setup(a => a.ApplyTo(It.IsAny<Creature>(), asCharacter, null))
                .Returns(templateCreature1);

            var templateCreature2 = new Creature();
            mockTemplateApplicator2
                .Setup(a => a.ApplyTo(templateCreature1, asCharacter, It.Is<Filters>(f => f != null
                    && f.Templates.IsEquivalentTo(new[] { "template 1", "template 2" })
                    && f.ChallengeRating == null
                    && f.Type == null
                    && f.Alignment == null)))
                .Returns(templateCreature2);

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template 1", "template 2");
            Assert.That(creature, Is.EqualTo(templateCreature2));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Generate_IfCreatureHasNotHitDice_ChallengeRatingIsZero(bool asCharacter)
        {
            SetUpCreature("creature", asCharacter, null, null, null, null, "template");
            hitPoints.HitDice.Clear();
            hitPoints.DefaultTotal = 0;
            hitPoints.Total = 0;

            SetUpCreature("creature", asCharacter, null, null, null, null, "template");

            var creature = creatureGenerator.Generate(asCharacter, "creature", null, "template");
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
        }
    }
}