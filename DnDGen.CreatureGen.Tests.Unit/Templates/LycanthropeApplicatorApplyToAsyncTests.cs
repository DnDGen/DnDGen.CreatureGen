using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    internal class LycanthropeApplicatorApplyToAsyncTests : LycanthropeApplicatorTestsBase
    {
        private Creature baseCreature;

        [SetUp]
        public void Setup()
        {
            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, It.IsAny<IEnumerable<Feat>>()))
                .Returns(baseCreature.HitPoints);

            var rounded = baseCreature.HitPoints.HitDice[0].RoundedQuantity;
            baseRoll = random.Next(rounded * baseCreature.HitPoints.HitDice[0].HitDie) + rounded;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);

            baseAverage = rounded * baseCreature.HitPoints.HitDice[0].HitDie / 2d + rounded;
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, applicator.LycanthropeSpecies, false, false))
                .Returns(baseCreature.Demographics);
        }

        [TestCaseSource(nameof(BUG_HitPointTotals))]
        public async Task BUG_ApplyToAsync_AddAnimalHitPoints_NegativeConstitutionBonus(int bQ, int bD, int bR, double bA, int aQ, int aD)
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

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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
        public async Task BUG_ApplyToAsync_AddAnimalHitPoints_WithConstitutionBonus(int bQ, int bD, int bR, double bA, int aQ, int aD)
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

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: aQ, hitDiceDie: aD, roll: 9266, average: 90210.42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: 1);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: my lycanthrope");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(IncompatibleFilters))]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(
            bool asCharacter,
            string type,
            string challengeRating,
            string alignment,
            string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: 1);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: my lycanthrope");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: 1);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo("my lycanthrope"));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            mockDice.Reset();
            rollSequences.Clear();
            averageSequences.Clear();

            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseRoll);
            SetUpRoll(baseCreature.HitPoints.HitDice[0], baseAverage);

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: 1);

            var filters = new Filters
            {
                Types = ["subtype 1"],
                ChallengeRatings = [ChallengeRatingConstants.CR3],
                Alignments = ["original alignment"]
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo("my lycanthrope"));
        }

        [Test]
        public async Task ApplyToAsync_GainShapechangerSubtype()
        {
            baseCreature.Type.SubTypes =
            [
                "subtype 1",
                "subtype 2",
            ];

            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Shapechanger));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithAdjustedDemographics()
        {
            var templateDemographics = new Demographics
            {
                Skin = "furry",
                Gender = "wild gender"
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, applicator.LycanthropeSpecies, false, false))
                .Returns(templateDemographics);

            SetUpAnimal("my animal", baseCreature);

            var hitDie = animalHitPoints.HitDice.Last();
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my animal",
                    animalData.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + hitDie.RoundedQuantity,
                    templateDemographics.Gender))
                .Returns(animalAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalHitPoints_NoConstitutionBonus()
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal("my animal", baseCreature, roll: 9266, average: 90210.42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithConstitutionBonus()
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 14;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal("my animal", baseCreature, roll: 9266, average: 90210.42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_AddAnimalHitPoints_NegativeConstitutionBonus()
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal("my animal", baseCreature, roll: 9266, average: 90210.42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithConditionalBonus()
        {
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            SetUpAnimal("my animal", baseCreature, roll: 9266, average: 90210.42);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "my animal"))
                .Returns(
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 8245 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -666 },
                ]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_AddAnimalHitPoints_WithFeats()
        {
            SetUpAnimal("my animal", baseCreature);

            var regeneratedHitPoints = new HitPoints();
            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, animalFeats))
                .Returns(regeneratedHitPoints);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(regeneratedHitPoints));
        }

        [Test]
        public async Task ApplyToAsync_GainAnimalSpeeds_AnimalFaster()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal("my animal", baseCreature);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainAnimalSpeeds_AnimalSlower()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Climb] = new Measurement("feet per round") { Value = 1337 };

            SetUpAnimal("my animal", baseCreature);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 42 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainAnimalSpeeds_WithManeuverability_AsBonus()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 1337, Description = "Decent Maneuverability" };

            SetUpAnimal("my animal", baseCreature);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainAnimalSpeeds_WithManeuverability_AsNew()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Land].Description = string.Empty;

            SetUpAnimal("my animal", baseCreature);

            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = 9266 };
            animalSpeeds[SpeedConstants.Burrow] = new Measurement("feet per round") { Value = 90210 };
            animalSpeeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "OK Maneuverability" };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainNaturalArmor()
        {
            SetUpAnimal("my animal", baseCreature, 0);

            baseCreature.ArmorClass.RemoveAllBonuses(ArmorClassConstants.Natural);

            //New for base and animal
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_GainBaseNaturalArmor()
        {
            SetUpAnimal("my animal", baseCreature, 9266);

            baseCreature.ArmorClass.RemoveAllBonuses(ArmorClassConstants.Natural);

            //New for base
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [Test]
        public async Task ApplyToAsync_GainAnimalNaturalArmor()
        {
            SetUpAnimal("my animal", baseCreature, 0);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //New for animal
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_ImproveNaturalArmor()
        {
            SetUpAnimal("my animal", baseCreature, 9266);

            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            //Both new for base and from animal
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(bonuses[0].Condition, Is.EqualTo("In base or hybrid form"));
            Assert.That(bonuses[0].IsConditional, Is.True);
            Assert.That(bonuses[0].Value, Is.EqualTo(90210 + 2));
            Assert.That(bonuses[1].Condition, Is.EqualTo("In animal or hybrid form"));
            Assert.That(bonuses[1].IsConditional, Is.True);
            Assert.That(bonuses[1].Value, Is.EqualTo(9266 + 2));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalBaseAttack()
        {
            SetUpAnimal("my animal", baseCreature);

            var baseAttack = baseCreature.BaseAttackBonus;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
        }

        [Test]
        public async Task ApplyToAsync_RecomputeGrappleBonus()
        {
            SetUpAnimal("my animal", baseCreature);

            var baseAttack = baseCreature.BaseAttackBonus;

            mockAttacksGenerator
                .Setup(g => g.GenerateGrappleBonus(
                    baseCreature.Name,
                    baseCreature.Size,
                    baseAttack + animalBaseAttack,
                    baseCreature.Abilities[AbilityConstants.Strength]))
                .Returns(1337);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(baseAttack + animalBaseAttack));
            Assert.That(creature.GrappleBonus, Is.EqualTo(1337));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalAttacks()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalAttacks_WithBonuses()
        {
            SetUpAnimal("my animal", baseCreature);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    animalAttacks,
                    It.Is<IEnumerable<Feat>>(fs =>
                        fs.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(animalSpecialQualities))),
                    baseCreature.Abilities))
                .Returns(animalAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(animalAttacks));
            Assert.That(animalAttacks[0].Name, Is.EqualTo("animal attack 1 (in Animal form)"));
            Assert.That(animalAttacks[1].Name, Is.EqualTo("animal attack 2 (in Animal form)"));
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_BaseIsBigger(string smallerSize, string biggerSize)
        {
            baseCreature.Size = biggerSize;

            SetUpAnimal("my animal", baseCreature, size: smallerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my lycanthrope",
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity, baseCreature.Demographics.Gender))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(SizeComparisons))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_AnimalIsBigger(string smallerSize, string biggerSize)
        {
            baseCreature.Size = smallerSize;

            SetUpAnimal("my animal", baseCreature, size: biggerSize);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my lycanthrope",
                    biggerSize,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity, baseCreature.Demographics.Gender))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(Sizes))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_AnimalAndBaseAreSameSize(string size)
        {
            baseCreature.Size = size;

            SetUpAnimal("my animal", baseCreature, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my lycanthrope",
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity,
                    baseCreature.Demographics.Gender))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [TestCaseSource(nameof(Sizes))]
        public async Task ApplyToAsync_AddLycanthropeAttacks_WithBonuses(string size)
        {
            baseCreature.Size = size;

            SetUpAnimal("my animal", baseCreature, size: size);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my lycanthrope",
                    size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity, baseCreature.Demographics.Gender))
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(lycanthropeAttacks));
            Assert.That(lycanthropeAttacks[0].Name, Is.EqualTo("lycanthrope attack 1"));
            Assert.That(lycanthropeAttacks[1].Name, Is.EqualTo("lycanthrope attack 2"));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalAttacks_WithLycanthropy()
        {
            SetUpAnimal("my animal", baseCreature);

            var lycanthropeAttacks = new[]
            {
                new Attack { Name = "lycanthrope attack 1" },
                new Attack { Name = "lycanthrope attack 2" },
                new Attack { Name = $"{animalAttacks[1].Name} (in Hybrid form)", DamageEffect = "my damage effect" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my lycanthrope",
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + animalHitPoints.HitDice[0].RoundedQuantity, baseCreature.Demographics.Gender))
                .Returns(lycanthropeAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(lycanthropeAttacks, It.IsAny<IEnumerable<Feat>>(), baseCreature.Abilities))
                .Returns(lycanthropeAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_ModifyBaseCreatureAttacks_Humanoid()
        {
            SetUpAnimal("my animal", baseCreature);

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Humanoid or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Humanoid form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Humanoid form)"));
        }

        [Test]
        public async Task ApplyToAsync_ModifyBaseCreatureAttacks_Giant()
        {
            SetUpAnimal("my animal", baseCreature);

            baseCreature.Type.Name = CreatureConstants.Types.Giant;

            var baseAttacks = new[]
            {
                new Attack { Name = "melee attack", IsMelee = true, IsSpecial = false },
                new Attack { Name = "ranged attack", IsMelee = false, IsSpecial = false },
                new Attack { Name = "special melee attack", IsMelee = true, IsSpecial = true },
                new Attack { Name = "special ranged attack", IsMelee = false, IsSpecial = true },
            };
            baseCreature.Attacks = baseAttacks;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks, Is.SupersetOf(baseAttacks));
            Assert.That(baseAttacks[0].Name, Is.EqualTo("melee attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[1].Name, Is.EqualTo("ranged attack (in Giant or Hybrid form)"));
            Assert.That(baseAttacks[2].Name, Is.EqualTo("special melee attack (in Giant form)"));
            Assert.That(baseAttacks[3].Name, Is.EqualTo("special ranged attack (in Giant form)"));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalSpecialQualities()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(animalSpecialQualities));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalSpecialQualities_RemoveDuplicates()
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union([baseSpecialQuality]);

            SetUpAnimal("my animal", baseCreature);

            animalSpecialQualities.Add(new Feat { Name = "my special quality" });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(animalSpecialQualities[0])
                .And.Contains(animalSpecialQualities[1])
                .And.Contains(baseSpecialQuality));
            Assert.That(creature.SpecialQualities.Count(sq => sq.Name == "my special quality"), Is.EqualTo(1));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(5));
        }

        [Test]
        public async Task ApplyToAsync_AddLycanthropeSpecialQualities()
        {
            SetUpAnimal("my animal", baseCreature);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    "my lycanthrope",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Shapechanger,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lycanthropeSpecialQualities));
        }

        [Test]
        public async Task ApplyToAsync_AddLycanthropeSpecialQualities_RemoveDuplicates()
        {
            var baseSpecialQuality = new Feat { Name = "my special quality" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities
                .Union([baseSpecialQuality]);

            SetUpAnimal("my animal", baseCreature);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var lycanthropeSpecialQualities = new[]
            {
                new Feat { Name = "lycanthrope special quality 1" },
                new Feat { Name = "my special quality" },
                new Feat { Name = "lycanthrope special quality 2" },
            };
            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    "my lycanthrope",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Humanoid
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Shapechanger,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    It.Is<IEnumerable<Skill>>(ss => ss.IsEquivalentTo(baseCreature.Skills.Union(animalSkills))),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lycanthropeSpecialQualities);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Contains.Item(lycanthropeSpecialQualities[0])
                .And.Contains(lycanthropeSpecialQualities[2])
                .And.Contains(baseSpecialQuality));
            Assert.That(creature.SpecialQualities.Count(sq => sq.Name == "my special quality"), Is.EqualTo(1));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(7));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalSaveBonuses()
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal("my animal", baseCreature);

            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = 600 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = 1337 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = 42 };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(1336 + 600));
            Assert.That(creature.Saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(96 + 1337));
            Assert.That(creature.Saves[SaveConstants.Will].BaseValue, Is.EqualTo(783 + 42));
        }

        [Test]
        public async Task ApplyToAsync_WisdomIncreasesBy2()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_ConditionalBonusesForHybridAndAnimalForms_FromAnimalBonuses()
        {
            SetUpAnimal("my animal", baseCreature);

            var animalAbilityAdjustments = new[]
            {
                new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 666 },
                new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 9266 },
                new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 90210 },
                new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 666 },
                new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 42 },
                new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 666 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "my animal"))
                .Returns(animalAbilityAdjustments);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainAnimalSkills()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task ApplyToAsync_GainAnimalSkills_CombineWithBaseCreatureSkills(bool isAfflicted)
        {
            applicator.IsNatural = !isAfflicted;

            var baseSkills = new[]
            {
                new Skill("skill 1", baseCreature.Abilities[AbilityConstants.Strength], 666) { ClassSkill = true, Ranks = 1 },
                new Skill("untrained skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 666) { ClassSkill = false, Ranks = 2 },
                new Skill("skill 2", baseCreature.Abilities[AbilityConstants.Dexterity], 666) { ClassSkill = true, Ranks = 3 },
                new Skill("untrained skill 2", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 4 },
                new Skill("animal skill 3", baseCreature.Abilities[AbilityConstants.Wisdom], 666) { ClassSkill = false, Ranks = 5 },
            };
            baseCreature.Skills = baseSkills;

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: 11);

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
                        "my animal",
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

        [Test]
        public async Task ApplyToAsync_GainControlShapeSkill_Afflicted()
        {
            applicator.IsNatural = false;

            SetUpAnimal("my animal", baseCreature);

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
                    "my animal",
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(rankedSkills));
        }

        [Test]
        public async Task ApplyToAsync_DoNotGainControlShapeSkill_Natural()
        {
            applicator.IsNatural = true;

            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.SupersetOf(animalSkills));

            var skillNames = creature.Skills.Select(s => s.Name);
            Assert.That(skillNames, Does.Not.Contain(SkillConstants.Special.ControlShape));
        }

        [Test]
        public async Task ApplyToAsync_GainAnimalFeats()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.SupersetOf(animalFeats));
        }

        [Test]
        public async Task ApplyToAsync_GainAnimalFeats_RemoveDuplicates()
        {
            baseCreature.Feats = baseCreature.Feats.Union([new Feat { Name = "my feat" }]);

            SetUpAnimal("my animal", baseCreature);

            animalFeats.Add(new Feat { Name = "my feat" });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(animalFeats[0])
                .And.Contains(animalFeats[1]));
            Assert.That(creature.Feats.Count(sq => sq.Name == "my feat"), Is.EqualTo(1));
            Assert.That(creature.Feats.Count(), Is.EqualTo(5));
        }

        [Test]
        public async Task ApplyToAsync_GainAnimalFeats_KeepDuplicates_IfCanBeTakenMultipleTimes()
        {
            var creatureFeat = new Feat { Name = "my feat", CanBeTakenMultipleTimes = true };
            baseCreature.Feats = baseCreature.Feats.Union([creatureFeat]);

            SetUpAnimal("my animal", baseCreature);

            animalFeats.Add(new Feat { Name = "my feat", CanBeTakenMultipleTimes = true });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Contains.Item(creatureFeat)
                .And.SupersetOf(animalFeats));
        }

        [Test]
        public async Task ApplyToAsync_AddAnimalSaveBonuses_WithBonusesFromNewFeats()
        {
            baseCreature.Saves[SaveConstants.Fortitude].BaseValue = 1336;
            baseCreature.Saves[SaveConstants.Reflex].BaseValue = 96;
            baseCreature.Saves[SaveConstants.Will].BaseValue = 783;

            SetUpAnimal("my animal", baseCreature);

            var animalSaves = new Dictionary<string, Save>
            {
                [SaveConstants.Fortitude] = new Save { BaseValue = 600 },
                [SaveConstants.Reflex] = new Save { BaseValue = 1337 },
                [SaveConstants.Will] = new Save { BaseValue = 42 }
            };

            animalSaves[SaveConstants.Fortitude].AddBonus(9266);
            animalSaves[SaveConstants.Fortitude].AddBonus(1234, "with a condition");
            animalSaves[SaveConstants.Reflex].AddBonus(90210);
            animalSaves[SaveConstants.Reflex].AddBonus(2345, "with another condition");
            animalSaves[SaveConstants.Will].AddBonus(8245);
            animalSaves[SaveConstants.Will].AddBonus(3456, "with yet another condition");

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    "my animal",
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(animalSpecialQualities.Union(animalFeats))),
                    baseCreature.Abilities))
                .Returns(animalSaves);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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
        public async Task ApplyToAsync_IncreaseChallengeRating(string originalChallengeRating, double animalHitDiceQuantity, string updatedChallengeRating)
        {
            baseCreature.ChallengeRating = originalChallengeRating;

            SetUpAnimal("my animal", baseCreature, hitDiceQuantity: animalHitDiceQuantity);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(updatedChallengeRating));
        }

        [TestCaseSource(nameof(LevelAdjustments))]
        public async Task ApplyToAsync_IncreaseLevelAdjustment(int? oldLevelAdjustment, int? newLevelAdjustment, bool isNatural)
        {
            baseCreature.LevelAdjustment = oldLevelAdjustment;

            applicator.IsNatural = isNatural;

            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(newLevelAdjustment));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            SetUpAnimal("my animal", baseCreature);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo("my lycanthrope"));
        }
    }
}
