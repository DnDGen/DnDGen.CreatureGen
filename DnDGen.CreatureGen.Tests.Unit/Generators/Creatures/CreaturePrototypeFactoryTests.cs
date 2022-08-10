using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class CreaturePrototypeFactoryTests
    {
        private ICreaturePrototypeFactory prototypeFactory;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();

            prototypeFactory = new CreaturePrototypeFactory(
                mockCollectionSelector.Object,
                mockAdjustmentSelector.Object,
                mockCreatureDataSelector.Object,
                mockTypeAndAmountSelector.Object);
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes()
        {
            var data = new Dictionary<string, CreatureDataSelection>();
            data["creature 1"] = new CreatureDataSelection
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
            };
            data["wrong creature"] = new CreatureDataSelection
            {
                CasterLevel = 666,
                ChallengeRating = 666.ToString(),
                LevelAdjustment = 666,
            };
            data["creature 2"] = new CreatureDataSelection
            {
                CasterLevel = 1,
                ChallengeRating = ChallengeRatingConstants.CR1,
                LevelAdjustment = 0,
            };
            data["creature 3"] = new CreatureDataSelection
            {
                CasterLevel = 2,
                ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                LevelAdjustment = 1,
            };
            data["creature 4"] = new CreatureDataSelection
            {
                CasterLevel = 3,
                ChallengeRating = ChallengeRatingConstants.CR3,
                LevelAdjustment = 2,
            };
            data["creature 5"] = new CreatureDataSelection
            {
                CasterLevel = 4,
                ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                LevelAdjustment = 3,
            };
            data["creature 6"] = new CreatureDataSelection
            {
                CasterLevel = 5,
                ChallengeRating = ChallengeRatingConstants.CR4,
                LevelAdjustment = 4,
            };
            data["creature 7"] = new CreatureDataSelection
            {
                CasterLevel = 6,
                ChallengeRating = ChallengeRatingConstants.CR1_4th,
                LevelAdjustment = 5,
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);

            var hitDice = new Dictionary<string, double>();
            hitDice["creature 1"] = 0.5;
            hitDice["wrong creature"] = 666;
            hitDice["creature 2"] = 0.5;
            hitDice["creature 3"] = 1;
            hitDice["creature 4"] = 1;
            hitDice["creature 5"] = 2;
            hitDice["creature 6"] = 3;
            hitDice["creature 7"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["creature 1"] = new[] { "my creature type" };
            types["wrong creature"] = new[] { "wrong creature type" };
            types["creature 2"] = new[] { CreatureConstants.Types.Humanoid };
            types["creature 3"] = new[] { "my other creature type", "my other subtype" };
            types["creature 4"] = new[] { CreatureConstants.Types.Humanoid, "my subtype" };
            types["creature 5"] = new[] { "creature type 2", "subtype 1", "subtype 2" };
            types["creature 6"] = new[] { "creature type 3", "subtype 3" };
            types["creature 7"] = new[] { "creature type 4" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["creature 1"] = new[] { AlignmentConstants.ChaoticEvil };
            alignments["wrong creature"] = new[] { AlignmentConstants.ChaoticEvil };
            alignments["creature 2"] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
            alignments["creature 3"] = new[] { AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil };
            alignments["creature 4"] = new[] { AlignmentConstants.LawfulGood, AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulGood };
            alignments["creature 5"] = new[] { AlignmentConstants.NeutralEvil };
            alignments["creature 6"] = new[] { AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood };
            alignments["creature 7"] = new[] { AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood, AlignmentConstants.TrueNeutral };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);

            var abilities = new Dictionary<string, IEnumerable<TypeAndAmountSelection>>();
            abilities[CreatureConstants.Human] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };
            abilities["creature 1"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };
            abilities["wrong creature"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -666 },
            };
            abilities["creature 2"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 1 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 3 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -3 },
            };
            abilities["creature 3"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -4 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -5 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -7 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -8 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -9 },
            };
            abilities["creature 4"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 1 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 4 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 5 },
            };
            abilities["creature 5"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -6 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -7 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 8 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 9 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -11 },
            };
            abilities["creature 6"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 12 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 13 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -14 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -15 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 16 },
            };
            abilities["creature 7"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 12 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 13 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -14 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -15 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 16 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 17 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAll(TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var creatures = new[]
            {
                "creature 1",
                "creature 6",
                "creature 2",
                "creature 5",
                "creature 3",
                "creature 4",
                "creature 7",
            };

            var prototypes = prototypeFactory.Build(creatures, false).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(7));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].Alignments, Is.EqualTo(alignments["creature 1"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[0].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].CasterLevel));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].ChallengeRating));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(hitDice["creature 1"]));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].LevelAdjustment));
            Assert.That(prototypes[0].Type.AllTypes, Is.EqualTo(types["creature 1"]));

            Assert.That(prototypes[1].Name, Is.EqualTo("creature 6"));
            Assert.That(prototypes[1].Alignments, Is.EqualTo(alignments["creature 6"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[1].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].CasterLevel));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].ChallengeRating));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(hitDice["creature 6"]));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].LevelAdjustment));
            Assert.That(prototypes[1].Type.AllTypes, Is.EqualTo(types["creature 6"]));

            Assert.That(prototypes[2].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[2].Alignments, Is.EqualTo(alignments["creature 2"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[2].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].CasterLevel));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(data["creature 2"].ChallengeRating));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(hitDice["creature 2"]));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].LevelAdjustment));
            Assert.That(prototypes[2].Type.AllTypes, Is.EqualTo(types["creature 2"]));

            Assert.That(prototypes[3].Name, Is.EqualTo("creature 5"));
            Assert.That(prototypes[3].Alignments, Is.EqualTo(alignments["creature 5"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[3].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].CasterLevel));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].ChallengeRating));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(hitDice["creature 5"]));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].LevelAdjustment));
            Assert.That(prototypes[3].Type.AllTypes, Is.EqualTo(types["creature 5"]));

            Assert.That(prototypes[4].Name, Is.EqualTo("creature 3"));
            Assert.That(prototypes[4].Alignments, Is.EqualTo(alignments["creature 3"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[4].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].CasterLevel));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].ChallengeRating));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(hitDice["creature 3"]));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].LevelAdjustment));
            Assert.That(prototypes[4].Type.AllTypes, Is.EqualTo(types["creature 3"]));

            Assert.That(prototypes[5].Name, Is.EqualTo("creature 4"));
            Assert.That(prototypes[5].Alignments, Is.EqualTo(alignments["creature 4"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[5].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].CasterLevel));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(data["creature 4"].ChallengeRating));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(hitDice["creature 4"]));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].LevelAdjustment));
            Assert.That(prototypes[5].Type.AllTypes, Is.EqualTo(types["creature 4"]));

            Assert.That(prototypes[6].Name, Is.EqualTo("creature 7"));
            Assert.That(prototypes[6].Alignments, Is.EqualTo(alignments["creature 7"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[6].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].CasterLevel));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].ChallengeRating));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(hitDice["creature 7"]));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(types["creature 7"]));
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes_AsCharacter()
        {
            var data = new Dictionary<string, CreatureDataSelection>();
            data["creature 1"] = new CreatureDataSelection
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
            };
            data["wrong creature"] = new CreatureDataSelection
            {
                CasterLevel = 666,
                ChallengeRating = 666.ToString(),
                LevelAdjustment = 666,
            };
            data["creature 2"] = new CreatureDataSelection
            {
                CasterLevel = 1,
                ChallengeRating = ChallengeRatingConstants.CR1,
                LevelAdjustment = 0,
            };
            data["creature 3"] = new CreatureDataSelection
            {
                CasterLevel = 2,
                ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                LevelAdjustment = 1,
            };
            data["creature 4"] = new CreatureDataSelection
            {
                CasterLevel = 3,
                ChallengeRating = ChallengeRatingConstants.CR3,
                LevelAdjustment = 2,
            };
            data["creature 5"] = new CreatureDataSelection
            {
                CasterLevel = 4,
                ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                LevelAdjustment = 3,
            };
            data["creature 6"] = new CreatureDataSelection
            {
                CasterLevel = 5,
                ChallengeRating = ChallengeRatingConstants.CR4,
                LevelAdjustment = 4,
            };
            data["creature 7"] = new CreatureDataSelection
            {
                CasterLevel = 6,
                ChallengeRating = ChallengeRatingConstants.CR1_4th,
                LevelAdjustment = 5,
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);

            var hitDice = new Dictionary<string, double>();
            hitDice["creature 1"] = 0.5;
            hitDice["wrong creature"] = 666;
            hitDice["creature 2"] = 0.5;
            hitDice["creature 3"] = 1;
            hitDice["creature 4"] = 1;
            hitDice["creature 5"] = 2;
            hitDice["creature 6"] = 3;
            hitDice["creature 7"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["creature 1"] = new[] { "my creature type" };
            types["wrong creature"] = new[] { "wrong creature type" };
            types["creature 2"] = new[] { CreatureConstants.Types.Humanoid };
            types["creature 3"] = new[] { "my other creature type", "my other subtype" };
            types["creature 4"] = new[] { CreatureConstants.Types.Humanoid, "my subtype" };
            types["creature 5"] = new[] { "creature type 2", "subtype 1", "subtype 2" };
            types["creature 6"] = new[] { "creature type 3", "subtype 3" };
            types["creature 7"] = new[] { "creature type 4" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["creature 1"] = new[] { AlignmentConstants.ChaoticEvil };
            alignments["wrong creature"] = new[] { AlignmentConstants.ChaoticEvil };
            alignments["creature 2"] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
            alignments["creature 3"] = new[] { AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil };
            alignments["creature 4"] = new[] { AlignmentConstants.LawfulGood, AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulGood };
            alignments["creature 5"] = new[] { AlignmentConstants.NeutralEvil };
            alignments["creature 6"] = new[] { AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood };
            alignments["creature 7"] = new[] { AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood, AlignmentConstants.TrueNeutral };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);

            var abilities = new Dictionary<string, IEnumerable<TypeAndAmountSelection>>();
            abilities[CreatureConstants.Human] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };
            abilities["creature 1"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };
            abilities["wrong creature"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -666 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -666 },
            };
            abilities["creature 2"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 1 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 3 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -3 },
            };
            abilities["creature 3"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -4 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -5 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -7 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = -8 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -9 },
            };
            abilities["creature 4"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 0 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 1 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 2 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 4 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 5 },
            };
            abilities["creature 5"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = -6 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = -7 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 8 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 9 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -11 },
            };
            abilities["creature 6"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 12 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 13 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -14 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -15 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 16 },
            };
            abilities["creature 7"] = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 12 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 13 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = -14 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = -15 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 16 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 17 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAll(TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var creatures = new[]
            {
                "creature 1",
                "creature 6",
                "creature 2",
                "creature 5",
                "creature 3",
                "creature 4",
                "creature 7",
            };

            var prototypes = prototypeFactory.Build(creatures, true).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(7));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].Alignments, Is.EqualTo(alignments["creature 1"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[0].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].CasterLevel));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].ChallengeRating));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(hitDice["creature 1"]));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].LevelAdjustment));
            Assert.That(prototypes[0].Type.AllTypes, Is.EqualTo(types["creature 1"]));

            Assert.That(prototypes[1].Name, Is.EqualTo("creature 6"));
            Assert.That(prototypes[1].Alignments, Is.EqualTo(alignments["creature 6"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[1].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].CasterLevel));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].ChallengeRating));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(hitDice["creature 6"]));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].LevelAdjustment));
            Assert.That(prototypes[1].Type.AllTypes, Is.EqualTo(types["creature 6"]));

            Assert.That(prototypes[2].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[2].Alignments, Is.EqualTo(alignments["creature 2"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[2].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].CasterLevel));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(data["creature 2"].ChallengeRating));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(hitDice["creature 2"]));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].LevelAdjustment));
            Assert.That(prototypes[2].Type.AllTypes, Is.EqualTo(types["creature 2"]));

            Assert.That(prototypes[3].Name, Is.EqualTo("creature 5"));
            Assert.That(prototypes[3].Alignments, Is.EqualTo(alignments["creature 5"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[3].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].CasterLevel));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].ChallengeRating));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(hitDice["creature 5"]));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].LevelAdjustment));
            Assert.That(prototypes[3].Type.AllTypes, Is.EqualTo(types["creature 5"]));

            Assert.That(prototypes[4].Name, Is.EqualTo("creature 3"));
            Assert.That(prototypes[4].Alignments, Is.EqualTo(alignments["creature 3"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[4].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].CasterLevel));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].ChallengeRating));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(hitDice["creature 3"]));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].LevelAdjustment));
            Assert.That(prototypes[4].Type.AllTypes, Is.EqualTo(types["creature 3"]));

            Assert.That(prototypes[5].Name, Is.EqualTo("creature 4"));
            Assert.That(prototypes[5].Alignments, Is.EqualTo(alignments["creature 4"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[5].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].CasterLevel));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(data["creature 4"].ChallengeRating));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(hitDice["creature 4"]));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].LevelAdjustment));
            Assert.That(prototypes[5].Type.AllTypes, Is.EqualTo(types["creature 4"]));

            Assert.That(prototypes[6].Name, Is.EqualTo("creature 7"));
            Assert.That(prototypes[6].Alignments, Is.EqualTo(alignments["creature 7"].Select(a => new Alignment(a)).Distinct()));
            Assert.That(prototypes[6].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].CasterLevel));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].ChallengeRating));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(hitDice["creature 7"]));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(types["creature 7"]));
        }
    }
}
