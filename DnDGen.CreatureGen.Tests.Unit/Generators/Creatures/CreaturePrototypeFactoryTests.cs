using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
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
        private Mock<ICollectionDataSelector<CreatureDataSelection>> mockCreatureDataSelector;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICollectionDataSelector<CreatureDataSelection>>();
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();

            prototypeFactory = new CreaturePrototypeFactory(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockTypeAndAmountSelector.Object);
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes()
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new CreatureDataSelection
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                }],
                ["wrong creature"] = [new CreatureDataSelection
                {
                    CasterLevel = 666,
                    ChallengeRating = 666.ToString(),
                    LevelAdjustment = 666,
                    Size = "wrong size",
                }],
                ["creature 2"] = [new CreatureDataSelection
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR1,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Colossal,
                }],
                ["creature 3"] = [new CreatureDataSelection
                {
                    CasterLevel = 2,
                    ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                    LevelAdjustment = 1,
                    Size = SizeConstants.Large,
                }],
                ["creature 4"] = [new CreatureDataSelection
                {
                    CasterLevel = 3,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 2,
                    Size = SizeConstants.Fine,
                }],
                ["creature 5"] = [new CreatureDataSelection
                {
                    CasterLevel = 4,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                    LevelAdjustment = 3,
                    Size = SizeConstants.Gargantuan,
                }],
                ["creature 6"] = [new CreatureDataSelection
                {
                    CasterLevel = 5,
                    ChallengeRating = ChallengeRatingConstants.CR4,
                    LevelAdjustment = 4,
                    Size = SizeConstants.Huge,
                }],
                ["creature 7"] = [new CreatureDataSelection
                {
                    CasterLevel = 6,
                    ChallengeRating = ChallengeRatingConstants.CR1_4th,
                    LevelAdjustment = 5,
                    Size = SizeConstants.Medium,
                }]
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var hitDice = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [new TypeAndAmountDataSelection { AmountAsDouble = 0.5 }],
                ["wrong creature"] = [new TypeAndAmountDataSelection { AmountAsDouble = 666 }],
                ["creature 2"] = [new TypeAndAmountDataSelection { AmountAsDouble = 0.5 }],
                ["creature 3"] = [new TypeAndAmountDataSelection { AmountAsDouble = 1 }],
                ["creature 4"] = [new TypeAndAmountDataSelection { AmountAsDouble = 1 }],
                ["creature 5"] = [new TypeAndAmountDataSelection { AmountAsDouble = 2 }],
                ["creature 6"] = [new TypeAndAmountDataSelection { AmountAsDouble = 3 }],
                ["creature 7"] = [new TypeAndAmountDataSelection { AmountAsDouble = 4 }]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice))
                .Returns(hitDice);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = ["my creature type"],
                ["wrong creature"] = ["wrong creature type"],
                ["creature 2"] = [CreatureConstants.Types.Humanoid],
                ["creature 3"] = ["my other creature type", "my other subtype"],
                ["creature 4"] = [CreatureConstants.Types.Humanoid, "my subtype"],
                ["creature 5"] = ["creature type 2", "subtype 1", "subtype 2"],
                ["creature 6"] = ["creature type 3", "subtype 3"],
                ["creature 7"] = ["creature type 4"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["wrong creature"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                ["creature 3"] = [AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil],
                ["creature 4"] = [AlignmentConstants.LawfulGood, AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulGood],
                ["creature 5"] = [AlignmentConstants.NeutralEvil],
                ["creature 6"] = [AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood],
                ["creature 7"] = [AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood, AlignmentConstants.TrueNeutral]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);

            var abilities = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                [CreatureConstants.Human] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 1"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["wrong creature"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -666 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -3 },
                ],
                ["creature 3"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -7 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -8 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -9 },
                ],
                ["creature 4"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 5 },
                ],
                ["creature 5"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -6 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -7 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 8 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 9 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -11 },
                ],
                ["creature 6"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 12 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 13 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -14 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -15 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 16 },
                ],
                ["creature 7"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 12 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 13 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -14 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -15 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 16 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 17 },
                ]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["wrong creature"] = [],
                ["creature 2"] = [],
                ["creature 3"] = [],
                ["creature 4"] = [],
                ["creature 5"] = [],
                ["creature 6"] = [],
                ["creature 7"] = [],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

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
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].Single().CasterLevel));
            Assert.That(prototypes[0].Size, Is.EqualTo(data["creature 1"].Single().Size));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].Single().ChallengeRating));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(hitDice["creature 1"]));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].Single().LevelAdjustment));
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
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(22));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(23));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(26));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].Single().CasterLevel));
            Assert.That(prototypes[1].Size, Is.EqualTo(data["creature 6"].Single().Size));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].Single().ChallengeRating));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(hitDice["creature 6"]));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].Single().LevelAdjustment));
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
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(11));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(12));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(8));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(13));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(7));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].Single().CasterLevel));
            Assert.That(prototypes[2].Size, Is.EqualTo(data["creature 2"].Single().Size));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(data["creature 2"].Single().ChallengeRating));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(hitDice["creature 2"]));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].Single().LevelAdjustment));
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
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(4));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(3));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(18));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(19));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].Single().CasterLevel));
            Assert.That(prototypes[3].Size, Is.EqualTo(data["creature 5"].Single().Size));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].Single().ChallengeRating));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(hitDice["creature 5"]));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].Single().LevelAdjustment));
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
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(6));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(5));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(2));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].Single().CasterLevel));
            Assert.That(prototypes[4].Size, Is.EqualTo(data["creature 3"].Single().Size));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].Single().ChallengeRating));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(hitDice["creature 3"]));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].Single().LevelAdjustment));
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
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(11));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(12));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(14));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(15));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].Single().CasterLevel));
            Assert.That(prototypes[5].Size, Is.EqualTo(data["creature 4"].Single().Size));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(data["creature 4"].Single().ChallengeRating));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(hitDice["creature 4"]));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].Single().LevelAdjustment));
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
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(22));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(23));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(26));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(27));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].Single().CasterLevel));
            Assert.That(prototypes[6].Size, Is.EqualTo(data["creature 7"].Single().Size));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].Single().ChallengeRating));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(hitDice["creature 7"]));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].Single().LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(types["creature 7"]));
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes_AsCharacter()
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new CreatureDataSelection
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                }],
                ["wrong creature"] = [new CreatureDataSelection
                {
                    CasterLevel = 666,
                    ChallengeRating = 666.ToString(),
                    LevelAdjustment = 666,
                }],
                ["creature 2"] = [new CreatureDataSelection
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR1,
                    LevelAdjustment = 0,
                }],
                ["creature 3"] = [new CreatureDataSelection
                {
                    CasterLevel = 2,
                    ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                    LevelAdjustment = 1,
                }],
                ["creature 4"] = [new CreatureDataSelection
                {
                    CasterLevel = 3,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 2,
                }],
                ["creature 5"] = [new CreatureDataSelection
                {
                    CasterLevel = 4,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                    LevelAdjustment = 3,
                }],
                ["creature 6"] = [new CreatureDataSelection
                {
                    CasterLevel = 5,
                    ChallengeRating = ChallengeRatingConstants.CR4,
                    LevelAdjustment = 4,
                }],
                ["creature 7"] = [new CreatureDataSelection
                {
                    CasterLevel = 6,
                    ChallengeRating = ChallengeRatingConstants.CR1_4th,
                    LevelAdjustment = 5,
                }]
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var hitDice = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [new TypeAndAmountDataSelection { AmountAsDouble = 0.5 }],
                ["wrong creature"] = [new TypeAndAmountDataSelection { AmountAsDouble = 666 }],
                ["creature 2"] = [new TypeAndAmountDataSelection { AmountAsDouble = 0.5 }],
                ["creature 3"] = [new TypeAndAmountDataSelection { AmountAsDouble = 1 }],
                ["creature 4"] = [new TypeAndAmountDataSelection { AmountAsDouble = 1 }],
                ["creature 5"] = [new TypeAndAmountDataSelection { AmountAsDouble = 2 }],
                ["creature 6"] = [new TypeAndAmountDataSelection { AmountAsDouble = 3 }],
                ["creature 7"] = [new TypeAndAmountDataSelection { AmountAsDouble = 4 }]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice))
                .Returns(hitDice);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = ["my creature type"],
                ["wrong creature"] = ["wrong creature type"],
                ["creature 2"] = [CreatureConstants.Types.Humanoid],
                ["creature 3"] = ["my other creature type", "my other subtype"],
                ["creature 4"] = [CreatureConstants.Types.Humanoid, "my subtype"],
                ["creature 5"] = ["creature type 2", "subtype 1", "subtype 2"],
                ["creature 6"] = ["creature type 3", "subtype 3"],
                ["creature 7"] = ["creature type 4"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["wrong creature"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                ["creature 3"] = [AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil],
                ["creature 4"] = [AlignmentConstants.LawfulGood, AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulGood],
                ["creature 5"] = [AlignmentConstants.NeutralEvil],
                ["creature 6"] = [AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood],
                ["creature 7"] = [AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralGood, AlignmentConstants.TrueNeutral]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);

            var abilities = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                [CreatureConstants.Human] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 1"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["wrong creature"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -666 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -3 },
                ],
                ["creature 3"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -7 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -8 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -9 },
                ],
                ["creature 4"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 5 },
                ],
                ["creature 5"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = -6 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -7 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 8 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 9 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -11 },
                ],
                ["creature 6"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 12 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 13 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -14 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -15 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 16 },
                ],
                ["creature 7"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 12 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 13 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = -14 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -15 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 16 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 17 },
                ]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["wrong creature"] = [],
                ["creature 2"] = [],
                ["creature 3"] = [],
                ["creature 4"] = [],
                ["creature 5"] = [],
                ["creature 6"] = [],
                ["creature 7"] = []
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

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
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].Single().CasterLevel));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].Single().ChallengeRating));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(hitDice["creature 1"]));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].Single().LevelAdjustment));
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
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(22));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(23));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(26));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].Single().CasterLevel));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].Single().ChallengeRating));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(hitDice["creature 6"]));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].Single().LevelAdjustment));
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
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(11));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(12));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(8));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(13));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(7));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].Single().CasterLevel));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(hitDice["creature 2"]));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].Single().LevelAdjustment));
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
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(4));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(3));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(18));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(19));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].Single().CasterLevel));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].Single().ChallengeRating));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(hitDice["creature 5"]));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].Single().LevelAdjustment));
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
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(6));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(5));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(3));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(2));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].Single().CasterLevel));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].Single().ChallengeRating));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(hitDice["creature 3"]));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].Single().LevelAdjustment));
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
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(11));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(12));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(14));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(15));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].Single().CasterLevel));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(hitDice["creature 4"]));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].Single().LevelAdjustment));
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
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(22));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(23));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(26));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(27));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].Single().CasterLevel));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].Single().ChallengeRating));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(hitDice["creature 7"]));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].Single().LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(types["creature 7"]));
        }

        //INFO: Since prototypes are for Template validation, we only want the Maximum caster level between spellcasting and at-will abilities
        //The caster type/amount is equivalent to the Magic caster level, as opposed to the caster level on the creature data
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(0, 10, 10)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 10, 10)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 2)]
        [TestCase(2, 10, 10)]
        [TestCase(10, 0, 10)]
        [TestCase(10, 1, 10)]
        [TestCase(10, 2, 10)]
        [TestCase(10, 10, 10)]
        public void Build_ReturnsCreaturePrototypes_WithCasterLevelAndCaster(int casterLevel, int caster, int expected)
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new CreatureDataSelection
                {
                    CasterLevel = casterLevel,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                }]
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var hitDice = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [new TypeAndAmountDataSelection { AmountAsDouble = 0.5 }]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice))
                .Returns(hitDice);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = ["my creature type"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);

            var abilities = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                [CreatureConstants.Human] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 1"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] =
                [
                    new TypeAndAmountDataSelection { Type = "spellcaster", AmountAsDouble = caster },
                ]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1",
            };

            var prototypes = prototypeFactory.Build(creatures, false).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(1));
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
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(expected));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].Single().ChallengeRating));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(hitDice["creature 1"]));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].Single().LevelAdjustment));
            Assert.That(prototypes[0].Type.AllTypes, Is.EqualTo(types["creature 1"]));
        }
    }
}
