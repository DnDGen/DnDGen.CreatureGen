using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.Templates;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
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
        private Mock<Dice> mockDice;
        private const int DefaultMax = 11;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICollectionDataSelector<CreatureDataSelection>>();
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockDice = new Mock<Dice>();

            prototypeFactory = new CreaturePrototypeFactory(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockTypeAndAmountSelector.Object,
                mockDice.Object);

            mockDice.Setup(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsPotentialMaximum<int>(true)).Returns(DefaultMax);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototype_WithAsCharacterSet(bool asCharacter)
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", asCharacter);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.AsCharacter, Is.EqualTo(asCharacter));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototype_MaxFromAbilityRandomizer_Roll(bool asCharacter)
        {
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(9266);

            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", asCharacter, randomizer);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(9266));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototype_MaxFromAbilityRandomizer_Set(bool asCharacter)
        {
            var randomizer = new AbilityRandomizer();
            randomizer.SetRolls[AbilityConstants.Strength] = 9266;
            randomizer.SetRolls[AbilityConstants.Constitution] = 90210;
            randomizer.SetRolls[AbilityConstants.Dexterity] = 42;
            randomizer.SetRolls[AbilityConstants.Intelligence] = 600;
            randomizer.SetRolls[AbilityConstants.Wisdom] = 1337;
            randomizer.SetRolls[AbilityConstants.Charisma] = 1336;

            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", asCharacter, randomizer);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(90210));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(42));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(600));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(1337));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(1336));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void Build_ReturnsPrototype_HasSkeleton(bool asCharacter, bool hasSkeleton)
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
                HasSkeleton = hasSkeleton,
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", asCharacter);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.HasSkeleton, Is.EqualTo(hasSkeleton));
        }

        [Test]
        public void Build_ReturnsCreaturePrototype()
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", false);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Alignments, Is.EqualTo([new Alignment(AlignmentConstants.ChaoticEvil)]));
            Assert.That(prototype.Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.CasterLevel, Is.EqualTo(data.CasterLevel));
            Assert.That(prototype.Size, Is.EqualTo(data.Size));
            Assert.That(prototype.ChallengeRating, Is.EqualTo(data.GetEffectiveChallengeRating(false)));
            Assert.That(prototype.HitDiceQuantity, Is.EqualTo(data.GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototype.LevelAdjustment, Is.EqualTo(data.LevelAdjustment));
            Assert.That(prototype.Type.AllTypes, Is.EqualTo(data.Types));
        }

        [Test]
        public void Build_ReturnsCreaturePrototype_AsCharacter()
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build("creature 1", true);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Alignments, Is.EqualTo([new Alignment(AlignmentConstants.ChaoticEvil)]));
            Assert.That(prototype.Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.CasterLevel, Is.EqualTo(data.CasterLevel));
            Assert.That(prototype.Size, Is.EqualTo(data.Size));
            Assert.That(prototype.ChallengeRating, Is.EqualTo(data.GetEffectiveChallengeRating(true)));
            Assert.That(prototype.HitDiceQuantity, Is.EqualTo(data.GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototype.LevelAdjustment, Is.EqualTo(data.LevelAdjustment));
            Assert.That(prototype.Type.AllTypes, Is.EqualTo(data.Types));
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
        public void Build_ReturnsCreaturePrototype_WithCasterLevelAndCaster(int casterLevel, int caster, int expected)
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = casterLevel,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = "spellcaster", AmountAsDouble = caster },
                ]);

            var prototype = prototypeFactory.Build("creature 1", false);
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Alignments, Is.EqualTo([new Alignment(AlignmentConstants.ChaoticEvil)]));
            Assert.That(prototype.Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.CasterLevel, Is.EqualTo(expected));
            Assert.That(prototype.Size, Is.EqualTo(data.Size));
            Assert.That(prototype.ChallengeRating, Is.EqualTo(data.GetEffectiveChallengeRating(false)));
            Assert.That(prototype.HitDiceQuantity, Is.EqualTo(data.GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototype.LevelAdjustment, Is.EqualTo(data.LevelAdjustment));
            Assert.That(prototype.Type.AllTypes, Is.EqualTo(data.Types));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototypes_WithAsCharacterSet(bool asCharacter)
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                    HasSkeleton = true,
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my subtype"],
                    HasSkeleton = false,
                }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral],
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
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -6 },
                ],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["creature 2"] = [],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1", "creature 2"
            };

            var prototypes = prototypeFactory.Build(creatures, asCharacter).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(2));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].AsCharacter, Is.EqualTo(asCharacter));
            Assert.That(prototypes[1].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[1].AsCharacter, Is.EqualTo(asCharacter));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototypes_MaxFromAbilityRandomizer_Roll(bool asCharacter)
        {
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(9266);


            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                    HasSkeleton = true,
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my subtype"],
                    HasSkeleton = false,
                }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral],
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
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -6 },
                ],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["creature 2"] = [],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1", "creature 2"
            };

            var prototypes = prototypeFactory.Build(creatures, asCharacter, randomizer).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(2));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(9266 + 1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(9266 - 2));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(9266 + 3));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(9266 - 4));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(9266 + 5));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(9266 - 6));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsPrototypes_MaxFromAbilityRandomizer_Set(bool asCharacter)
        {
            var randomizer = new AbilityRandomizer();
            randomizer.SetRolls[AbilityConstants.Strength] = 9266;
            randomizer.SetRolls[AbilityConstants.Constitution] = 90210;
            randomizer.SetRolls[AbilityConstants.Dexterity] = 42;
            randomizer.SetRolls[AbilityConstants.Intelligence] = 600;
            randomizer.SetRolls[AbilityConstants.Wisdom] = 1337;
            randomizer.SetRolls[AbilityConstants.Charisma] = 1336;

            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                    HasSkeleton = true,
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my subtype"],
                    HasSkeleton = false,
                }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral],
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
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -6 },
                ],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["creature 2"] = [],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1", "creature 2"
            };

            var prototypes = prototypeFactory.Build(creatures, asCharacter, randomizer).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(2));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(9266));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(90210));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90210));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(42));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(600));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(1337));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1337));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(1336));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1336));
            Assert.That(prototypes[1].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(9266));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(9266 + 1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(90210));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90210 - 2));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(42));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42 + 3));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(600));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600 - 4));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(1337));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1337 + 5));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(1336));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1336 - 6));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void Build_ReturnsPrototypes_HasSkeleton(bool asCharacter, bool hasSkeleton)
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                    HasSkeleton = hasSkeleton,
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my subtype"],
                    HasSkeleton = hasSkeleton,
                }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral],
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
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -6 },
                ],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] = [],
                ["creature 2"] = [],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1", "creature 2"
            };

            var prototypes = prototypeFactory.Build(creatures, asCharacter).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(2));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].HasSkeleton, Is.EqualTo(hasSkeleton));
            Assert.That(prototypes[1].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[1].HasSkeleton, Is.EqualTo(hasSkeleton));
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes_SingleCreatureIsSinglePrototype()
        {
            var data = new CreatureDataSelection()
            {
                CasterLevel = 0,
                ChallengeRating = ChallengeRatingConstants.CR2,
                LevelAdjustment = null,
                Size = SizeConstants.Diminutive,
                HitDiceQuantity = 0.5,
                Types = ["my creature type"],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, "creature 1"))
                .Returns(data);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature 1"))
                .Returns([AlignmentConstants.ChaoticEvil]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature 1"))
                .Returns([
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 0 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = 0 },
                ]);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, "creature 1"))
                .Returns([]);

            var prototype = prototypeFactory.Build(["creature 1"], false).Single();
            Assert.That(prototype.Name, Is.EqualTo("creature 1"));
            Assert.That(prototype.Alignments, Is.EqualTo([new Alignment(AlignmentConstants.ChaoticEvil)]));
            Assert.That(prototype.Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototype.CasterLevel, Is.EqualTo(data.CasterLevel));
            Assert.That(prototype.Size, Is.EqualTo(data.Size));
            Assert.That(prototype.ChallengeRating, Is.EqualTo(data.GetEffectiveChallengeRating(false)));
            Assert.That(prototype.HitDiceQuantity, Is.EqualTo(data.GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototype.LevelAdjustment, Is.EqualTo(data.LevelAdjustment));
            Assert.That(prototype.Type.AllTypes, Is.EqualTo(data.Types));

            mockCreatureDataSelector.Verify(s => s.SelectAllFrom(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            mockCollectionSelector.Verify(s => s.SelectAllFrom(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            mockTypeAndAmountSelector.Verify(s => s.SelectAllFrom(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes()
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                }],
                ["wrong creature"] = [new()
                {
                    CasterLevel = 666,
                    ChallengeRating = 666.ToString(),
                    LevelAdjustment = 666,
                    Size = "wrong size",
                    HitDiceQuantity = 666,
                    Types = ["wrong creature type"],
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR1,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Colossal,
                    HitDiceQuantity = 0.5,
                    Types = [CreatureConstants.Types.Humanoid],
                }],
                ["creature 3"] = [new()
                {
                    CasterLevel = 2,
                    ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                    LevelAdjustment = 1,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my other subtype"],
                }],
                ["creature 4"] = [new()
                {
                    CasterLevel = 3,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 2,
                    Size = SizeConstants.Fine,
                    HitDiceQuantity = 1,
                    Types = [CreatureConstants.Types.Humanoid, "my subtype"],
                }],
                ["creature 5"] = [new()
                {
                    CasterLevel = 4,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                    LevelAdjustment = 3,
                    Size = SizeConstants.Gargantuan,
                    HitDiceQuantity = 2,
                    Types = ["creature type 2", "subtype 1", "subtype 2"],
                }],
                ["creature 6"] = [new()
                {
                    CasterLevel = 5,
                    ChallengeRating = ChallengeRatingConstants.CR4,
                    LevelAdjustment = 4,
                    Size = SizeConstants.Huge,
                    HitDiceQuantity = 3,
                    Types = ["creature type 3", "subtype 3"],
                }],
                ["creature 7"] = [new()
                {
                    CasterLevel = 6,
                    ChallengeRating = ChallengeRatingConstants.CR1_4th,
                    LevelAdjustment = 5,
                    Size = SizeConstants.Medium,
                    HitDiceQuantity = 4,
                    Types = ["creature type 4"],
                }]
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

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
            Assert.That(prototypes[0].Alignments, Is.EqualTo(alignments["creature 1"].Select(a => new Alignment(a))));
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
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].Single().CasterLevel));
            Assert.That(prototypes[0].Size, Is.EqualTo(data["creature 1"].Single().Size));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(data["creature 1"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].Single().LevelAdjustment));
            Assert.That(prototypes[0].Type.AllTypes, Is.EqualTo(data["creature 1"].Single().Types));

            Assert.That(prototypes[1].Name, Is.EqualTo("creature 6"));
            Assert.That(prototypes[1].Alignments, Is.EqualTo(alignments["creature 6"].Select(a => new Alignment(a))));
            Assert.That(prototypes[1].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 12));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 13));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 16));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].Single().CasterLevel));
            Assert.That(prototypes[1].Size, Is.EqualTo(data["creature 6"].Single().Size));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(data["creature 6"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].Single().LevelAdjustment));
            Assert.That(prototypes[1].Type.AllTypes, Is.EqualTo(data["creature 6"].Single().Types));

            Assert.That(prototypes[2].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[2].Alignments, Is.EqualTo(alignments["creature 2"].Select(a => new Alignment(a))));
            Assert.That(prototypes[2].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 1));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 2));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax - 2));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 3));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax - 3));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].Single().CasterLevel));
            Assert.That(prototypes[2].Size, Is.EqualTo(data["creature 2"].Single().Size));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(data["creature 2"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(data["creature 2"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].Single().LevelAdjustment));
            Assert.That(prototypes[2].Type.AllTypes, Is.EqualTo(data["creature 2"].Single().Types));

            Assert.That(prototypes[3].Name, Is.EqualTo("creature 5"));
            Assert.That(prototypes[3].Alignments, Is.EqualTo(alignments["creature 5"].Select(a => new Alignment(a))));
            Assert.That(prototypes[3].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax - 6));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax - 7));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 8));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax + 9));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].Single().CasterLevel));
            Assert.That(prototypes[3].Size, Is.EqualTo(data["creature 5"].Single().Size));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(data["creature 5"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].Single().LevelAdjustment));
            Assert.That(prototypes[3].Type.AllTypes, Is.EqualTo(data["creature 5"].Single().Types));

            Assert.That(prototypes[4].Name, Is.EqualTo("creature 3"));
            Assert.That(prototypes[4].Alignments, Is.EqualTo(alignments["creature 3"].Select(a => new Alignment(a))));
            Assert.That(prototypes[4].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax - 4));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax - 5));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax - 7));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax - 8));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax - 9));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].Single().CasterLevel));
            Assert.That(prototypes[4].Size, Is.EqualTo(data["creature 3"].Single().Size));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(data["creature 3"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].Single().LevelAdjustment));
            Assert.That(prototypes[4].Type.AllTypes, Is.EqualTo(data["creature 3"].Single().Types));

            Assert.That(prototypes[5].Name, Is.EqualTo("creature 4"));
            Assert.That(prototypes[5].Alignments, Is.EqualTo(alignments["creature 4"].Select(a => new Alignment(a))));
            Assert.That(prototypes[5].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 1));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 2));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 4));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax + 5));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].Single().CasterLevel));
            Assert.That(prototypes[5].Size, Is.EqualTo(data["creature 4"].Single().Size));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(data["creature 4"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(data["creature 4"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].Single().LevelAdjustment));
            Assert.That(prototypes[5].Type.AllTypes, Is.EqualTo(data["creature 4"].Single().Types));

            Assert.That(prototypes[6].Name, Is.EqualTo("creature 7"));
            Assert.That(prototypes[6].Alignments, Is.EqualTo(alignments["creature 7"].Select(a => new Alignment(a))));
            Assert.That(prototypes[6].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 12));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 13));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 16));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax + 17));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].Single().CasterLevel));
            Assert.That(prototypes[6].Size, Is.EqualTo(data["creature 7"].Single().Size));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].Single().GetEffectiveChallengeRating(false)));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(data["creature 7"].Single().GetEffectiveHitDiceQuantity(false)));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].Single().LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(data["creature 7"].Single().Types));
        }

        [Test]
        public void Build_ReturnsCreaturePrototypes_AsCharacter()
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["creature 1"] = [new()
                {
                    CasterLevel = 0,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    Size = SizeConstants.Diminutive,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                }],
                ["wrong creature"] = [new()
                {
                    CasterLevel = 666,
                    ChallengeRating = 666.ToString(),
                    LevelAdjustment = 666,
                    Size = "wrong size",
                    HitDiceQuantity = 666,
                    Types = ["wrong creature type"],
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = 1,
                    ChallengeRating = ChallengeRatingConstants.CR1,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Colossal,
                    HitDiceQuantity = 0.5,
                    Types = [CreatureConstants.Types.Humanoid],
                }],
                ["creature 3"] = [new()
                {
                    CasterLevel = 2,
                    ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                    LevelAdjustment = 1,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my other subtype"],
                }],
                ["creature 4"] = [new()
                {
                    CasterLevel = 3,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 2,
                    Size = SizeConstants.Fine,
                    HitDiceQuantity = 1,
                    Types = [CreatureConstants.Types.Humanoid, "my subtype"],
                }],
                ["creature 5"] = [new()
                {
                    CasterLevel = 4,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                    LevelAdjustment = 3,
                    Size = SizeConstants.Gargantuan,
                    HitDiceQuantity = 2,
                    Types = ["creature type 2", "subtype 1", "subtype 2"],
                }],
                ["creature 6"] = [new()
                {
                    CasterLevel = 5,
                    ChallengeRating = ChallengeRatingConstants.CR4,
                    LevelAdjustment = 4,
                    Size = SizeConstants.Huge,
                    HitDiceQuantity = 3,
                    Types = ["creature type 3", "subtype 3"],
                }],
                ["creature 7"] = [new()
                {
                    CasterLevel = 6,
                    ChallengeRating = ChallengeRatingConstants.CR1_4th,
                    LevelAdjustment = 5,
                    Size = SizeConstants.Medium,
                    HitDiceQuantity = 4,
                    Types = ["creature type 4"],
                }]
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

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
            Assert.That(prototypes[0].Alignments, Is.EqualTo(alignments["creature 1"].Select(a => new Alignment(a))));
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
            Assert.That(prototypes[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax));
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(data["creature 1"].Single().CasterLevel));
            Assert.That(prototypes[0].ChallengeRating, Is.EqualTo(data["creature 1"].Single().GetEffectiveChallengeRating(true)));
            Assert.That(prototypes[0].HitDiceQuantity, Is.EqualTo(data["creature 1"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[0].LevelAdjustment, Is.EqualTo(data["creature 1"].Single().LevelAdjustment));
            Assert.That(prototypes[0].Type.AllTypes, Is.EqualTo(data["creature 1"].Single().Types));

            Assert.That(prototypes[1].Name, Is.EqualTo("creature 6"));
            Assert.That(prototypes[1].Alignments, Is.EqualTo(alignments["creature 6"].Select(a => new Alignment(a))));
            Assert.That(prototypes[1].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 12));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 13));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 16));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(data["creature 6"].Single().CasterLevel));
            Assert.That(prototypes[1].ChallengeRating, Is.EqualTo(data["creature 6"].Single().GetEffectiveChallengeRating(true)));
            Assert.That(prototypes[1].HitDiceQuantity, Is.EqualTo(data["creature 6"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[1].LevelAdjustment, Is.EqualTo(data["creature 6"].Single().LevelAdjustment));
            Assert.That(prototypes[1].Type.AllTypes, Is.EqualTo(data["creature 6"].Single().Types));

            Assert.That(prototypes[2].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[2].Alignments, Is.EqualTo(alignments["creature 2"].Select(a => new Alignment(a))));
            Assert.That(prototypes[2].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 1));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 2));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax - 2));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 3));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax - 3));
            Assert.That(prototypes[2].CasterLevel, Is.EqualTo(data["creature 2"].Single().CasterLevel));
            Assert.That(prototypes[2].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
            Assert.That(prototypes[2].HitDiceQuantity, Is.EqualTo(data["creature 2"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[2].LevelAdjustment, Is.EqualTo(data["creature 2"].Single().LevelAdjustment));
            Assert.That(prototypes[2].Type.AllTypes, Is.EqualTo(data["creature 2"].Single().Types));

            Assert.That(prototypes[3].Name, Is.EqualTo("creature 5"));
            Assert.That(prototypes[3].Alignments, Is.EqualTo(alignments["creature 5"].Select(a => new Alignment(a))));
            Assert.That(prototypes[3].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax - 6));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax - 7));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 8));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax + 9));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[3].CasterLevel, Is.EqualTo(data["creature 5"].Single().CasterLevel));
            Assert.That(prototypes[3].ChallengeRating, Is.EqualTo(data["creature 5"].Single().GetEffectiveChallengeRating(true)));
            Assert.That(prototypes[3].HitDiceQuantity, Is.EqualTo(data["creature 5"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[3].LevelAdjustment, Is.EqualTo(data["creature 5"].Single().LevelAdjustment));
            Assert.That(prototypes[3].Type.AllTypes, Is.EqualTo(data["creature 5"].Single().Types));

            Assert.That(prototypes[4].Name, Is.EqualTo("creature 3"));
            Assert.That(prototypes[4].Alignments, Is.EqualTo(alignments["creature 3"].Select(a => new Alignment(a))));
            Assert.That(prototypes[4].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax - 4));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax - 5));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(DefaultMax - 7));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax - 8));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[4].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax - 9));
            Assert.That(prototypes[4].CasterLevel, Is.EqualTo(data["creature 3"].Single().CasterLevel));
            Assert.That(prototypes[4].ChallengeRating, Is.EqualTo(data["creature 3"].Single().GetEffectiveChallengeRating(true)));
            Assert.That(prototypes[4].HitDiceQuantity, Is.EqualTo(data["creature 3"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[4].LevelAdjustment, Is.EqualTo(data["creature 3"].Single().LevelAdjustment));
            Assert.That(prototypes[4].Type.AllTypes, Is.EqualTo(data["creature 3"].Single().Types));

            Assert.That(prototypes[5].Name, Is.EqualTo("creature 4"));
            Assert.That(prototypes[5].Alignments, Is.EqualTo(alignments["creature 4"].Select(a => new Alignment(a))));
            Assert.That(prototypes[5].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 1));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(DefaultMax + 2));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 4));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[5].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax + 5));
            Assert.That(prototypes[5].CasterLevel, Is.EqualTo(data["creature 4"].Single().CasterLevel));
            Assert.That(prototypes[5].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
            Assert.That(prototypes[5].HitDiceQuantity, Is.EqualTo(data["creature 4"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[5].LevelAdjustment, Is.EqualTo(data["creature 4"].Single().LevelAdjustment));
            Assert.That(prototypes[5].Type.AllTypes, Is.EqualTo(data["creature 4"].Single().Types));

            Assert.That(prototypes[6].Name, Is.EqualTo("creature 7"));
            Assert.That(prototypes[6].Alignments, Is.EqualTo(alignments["creature 7"].Select(a => new Alignment(a))));
            Assert.That(prototypes[6].Abilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].Name, Is.EqualTo(AbilityConstants.Strength));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(DefaultMax + 12));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].Name, Is.EqualTo(AbilityConstants.Constitution));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(DefaultMax + 13));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].Name, Is.EqualTo(AbilityConstants.Dexterity));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].Name, Is.EqualTo(AbilityConstants.Intelligence));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(1));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].Name, Is.EqualTo(AbilityConstants.Wisdom));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(DefaultMax + 16));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].Name, Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(prototypes[6].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(DefaultMax + 17));
            Assert.That(prototypes[6].CasterLevel, Is.EqualTo(data["creature 7"].Single().CasterLevel));
            Assert.That(prototypes[6].ChallengeRating, Is.EqualTo(data["creature 7"].Single().GetEffectiveChallengeRating(true)));
            Assert.That(prototypes[6].HitDiceQuantity, Is.EqualTo(data["creature 7"].Single().GetEffectiveHitDiceQuantity(true)));
            Assert.That(prototypes[6].LevelAdjustment, Is.EqualTo(data["creature 7"].Single().LevelAdjustment));
            Assert.That(prototypes[6].Type.AllTypes, Is.EqualTo(data["creature 7"].Single().Types));
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
                ["creature 1"] = [new()
                {
                    CasterLevel = casterLevel,
                    ChallengeRating = ChallengeRatingConstants.CR2,
                    LevelAdjustment = null,
                    HitDiceQuantity = 0.5,
                    Types = ["my creature type"],
                }],
                ["creature 2"] = [new()
                {
                    CasterLevel = caster,
                    ChallengeRating = ChallengeRatingConstants.CR3,
                    LevelAdjustment = 0,
                    Size = SizeConstants.Large,
                    HitDiceQuantity = 1,
                    Types = ["my other creature type", "my subtype"],
                    HasSkeleton = false,
                }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["creature 1"] = [AlignmentConstants.ChaoticEvil],
                ["creature 2"] = [AlignmentConstants.NeutralGood, AlignmentConstants.LawfulNeutral],
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
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = 1 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = -2 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = 3 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -4 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = 5 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -6 },
                ],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments))
                .Returns(abilities);

            var casters = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["creature 1"] =
                [
                    new TypeAndAmountDataSelection { Type = "spellcaster", AmountAsDouble = caster },
                ],
                ["creature 2"] =
                [
                    new TypeAndAmountDataSelection { Type = "magician", AmountAsDouble = casterLevel },
                ]
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters))
                .Returns(casters);

            var creatures = new[]
            {
                "creature 1", "creature 2"
            };

            var prototypes = prototypeFactory.Build(creatures, false).ToArray();
            Assert.That(prototypes, Has.Length.EqualTo(2));
            Assert.That(prototypes[0].Name, Is.EqualTo("creature 1"));
            Assert.That(prototypes[0].CasterLevel, Is.EqualTo(expected));
            Assert.That(prototypes[1].Name, Is.EqualTo("creature 2"));
            Assert.That(prototypes[1].CasterLevel, Is.EqualTo(expected));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void Clone_ReturnsCloneOfPrototype(bool asCharacter, bool skeleton)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithAsCharacter(asCharacter)
                .WithAbility(AbilityConstants.Strength, 9266, baseScore: 29)
                .WithAbility(AbilityConstants.Dexterity, 90210, baseScore: 1989)
                .WithAbility(AbilityConstants.Constitution, 42, baseScore: 7)
                .WithAbility(AbilityConstants.Intelligence, 600, baseScore: 17)
                .WithAbility(AbilityConstants.Wisdom, 1337, baseScore: 1991)
                .WithAbility(AbilityConstants.Charisma, 1336, baseScore: 2015)
                .WithCasterLevel(96)
                .WithChallengeRating("my cr")
                .WithSkeleton(skeleton)
                .WithLevelAdjustment(783)
                .WithSize("my size")
                .WithHitDiceQuantity(82.45)
                .Build();

            var clone = prototypeFactory.Clone(prototype);
            Assert.That(clone, Is.Not.EqualTo(prototype));
            Assert.That(clone.Abilities, Is.Not.SameAs(prototype.Abilities).And.Count.EqualTo(prototype.Abilities.Count).And.Count.EqualTo(6));

            foreach (var expected in prototype.Abilities)
            {
                Assert.That(clone.Abilities, Contains.Key(expected.Key));
                Assert.That(clone.Abilities[expected.Key], Is.Not.SameAs(expected.Value));
                Assert.That(clone.Abilities[expected.Key].Name, Is.EqualTo(expected.Value.Name).And.EqualTo(expected.Key));
                Assert.That(clone.Abilities[expected.Key].BaseScore, Is.EqualTo(expected.Value.BaseScore));
                Assert.That(clone.Abilities[expected.Key].RacialAdjustment, Is.EqualTo(expected.Value.RacialAdjustment));
            }

            Assert.That(clone.Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(29));
            Assert.That(clone.Abilities[AbilityConstants.Strength].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(clone.Abilities[AbilityConstants.Dexterity].BaseScore, Is.EqualTo(1989));
            Assert.That(clone.Abilities[AbilityConstants.Dexterity].RacialAdjustment, Is.EqualTo(90210));
            Assert.That(clone.Abilities[AbilityConstants.Constitution].BaseScore, Is.EqualTo(7));
            Assert.That(clone.Abilities[AbilityConstants.Constitution].RacialAdjustment, Is.EqualTo(42));
            Assert.That(clone.Abilities[AbilityConstants.Intelligence].BaseScore, Is.EqualTo(17));
            Assert.That(clone.Abilities[AbilityConstants.Intelligence].RacialAdjustment, Is.EqualTo(600));
            Assert.That(clone.Abilities[AbilityConstants.Wisdom].BaseScore, Is.EqualTo(1991));
            Assert.That(clone.Abilities[AbilityConstants.Wisdom].RacialAdjustment, Is.EqualTo(1337));
            Assert.That(clone.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(2015));
            Assert.That(clone.Abilities[AbilityConstants.Charisma].RacialAdjustment, Is.EqualTo(1336));

            Assert.That(clone.Alignments, Is.Not.SameAs(prototype.Alignments));
            Assert.That(clone.Alignments, Is.EquivalentTo(prototype.Alignments));
            Assert.That(clone.AsCharacter, Is.EqualTo(asCharacter).And.EqualTo(prototype.AsCharacter));
            Assert.That(clone.CasterLevel, Is.EqualTo(96).And.EqualTo(prototype.CasterLevel));
            Assert.That(clone.ChallengeRating, Is.EqualTo("my cr").And.EqualTo(prototype.ChallengeRating));
            Assert.That(clone.HasSkeleton, Is.EqualTo(skeleton).And.EqualTo(prototype.HasSkeleton));
            Assert.That(clone.HitDiceQuantity, Is.EqualTo(82.45).And.EqualTo(prototype.HitDiceQuantity));
            Assert.That(clone.LevelAdjustment, Is.EqualTo(783).And.EqualTo(prototype.LevelAdjustment));
            Assert.That(clone.Name, Is.EqualTo(prototype.Name));
            Assert.That(clone.Size, Is.EqualTo("my size").And.EqualTo(prototype.Size));
            Assert.That(clone.Type, Is.Not.EqualTo(prototype.Type));
            Assert.That(clone.Type.Name, Is.EqualTo(prototype.Type.Name));
            Assert.That(clone.Type.SubTypes, Is.Not.SameAs(prototype.Type.SubTypes));
            Assert.That(clone.Type.SubTypes, Is.EquivalentTo(prototype.Type.SubTypes));
        }

        [Test]
        public void Clone_ReturnsCloneOfPrototype_CanManipulateIndependently()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithAsCharacter(true)
                .WithAbility(AbilityConstants.Strength, 9266, baseScore: 29)
                .WithAbility(AbilityConstants.Dexterity, 90210, baseScore: 1989)
                .WithAbility(AbilityConstants.Constitution, 42, baseScore: 7)
                .WithAbility(AbilityConstants.Intelligence, 600, baseScore: 17)
                .WithAbility(AbilityConstants.Wisdom, 1337, baseScore: 1991)
                .WithAbility(AbilityConstants.Charisma, 1336, baseScore: 2015)
                .WithCasterLevel(96)
                .WithChallengeRating("my cr")
                .WithSkeleton(true)
                .WithLevelAdjustment(783)
                .WithSize("my size")
                .WithHitDiceQuantity(82.45)
                .Build();

            var clone = prototypeFactory.Clone(prototype);
            clone.Name = "new name";
            Assert.That(prototype.Name, Is.Not.EqualTo(clone.Name));

            clone.AsCharacter = false;
            Assert.That(prototype.AsCharacter, Is.True);

            clone.Abilities[AbilityConstants.Strength].BaseScore = 9;
            clone.Abilities[AbilityConstants.Dexterity].BaseScore = 22;
            clone.Abilities[AbilityConstants.Constitution].BaseScore = 2022;
            clone.Abilities[AbilityConstants.Intelligence].BaseScore = 227;
            clone.Abilities[AbilityConstants.Wisdom].BaseScore = 2;
            clone.Abilities[AbilityConstants.Charisma].BaseScore = 12;
            Assert.That(prototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(29 + 9266));
            Assert.That(prototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1989 + 90210));
            Assert.That(prototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(7 + 42));
            Assert.That(prototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(17 + 600));
            Assert.That(prototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1991 + 1337));
            Assert.That(prototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(2015 + 1336));

            clone.CasterLevel = 2025;
            Assert.That(prototype.CasterLevel, Is.EqualTo(96));

            clone.ChallengeRating = "my other cr";
            Assert.That(prototype.ChallengeRating, Is.EqualTo("my cr"));

            clone.HasSkeleton = false;
            Assert.That(prototype.HasSkeleton, Is.True);

            clone.LevelAdjustment = null;
            Assert.That(prototype.LevelAdjustment, Is.EqualTo(783));

            clone.Size = "my other size";
            Assert.That(prototype.Size, Is.EqualTo("my size"));

            clone.HitDiceQuantity = 11.23;
            Assert.That(prototype.HitDiceQuantity, Is.EqualTo(82.45));

            clone.Alignments[0].Goodness = "newgood";
            clone.Alignments[1] = new("clone alignment");
            Assert.That(prototype.Alignments[0].Goodness, Is.Not.EqualTo("newgood"));
            Assert.That(prototype.Alignments[1].Full, Is.Not.EqualTo("clone alignment"));

            clone.Type.Name = "new type";
            clone.Type.SubTypes = ["new subtype", "newer subtype"];
            Assert.That(prototype.Type.Name, Is.Not.EqualTo("new type"));
            Assert.That(prototype.Type.SubTypes, Is.Not.EquivalentTo(clone.Type.SubTypes));
        }

        [Test]
        public void Clone_ReturnsCloneOfPrototype_WithoutTemplates()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithAsCharacter(true)
                .WithAbility(AbilityConstants.Strength, 9266, 1, baseScore: 29)
                .WithAbility(AbilityConstants.Dexterity, 90210, -2, baseScore: 1989)
                .WithAbility(AbilityConstants.Constitution, 42, 3, baseScore: 7)
                .WithAbility(AbilityConstants.Intelligence, 600, -4, baseScore: 17)
                .WithAbility(AbilityConstants.Wisdom, 1337, 5, baseScore: 1991)
                .WithAbility(AbilityConstants.Charisma, 1336, -6, baseScore: 2015)
                .WithCasterLevel(96)
                .WithChallengeRating("my cr")
                .WithSkeleton(true)
                .WithLevelAdjustment(783)
                .WithSize("my size")
                .WithHitDiceQuantity(82.45)
                .Build();
            prototype.Templates = ["my template", "my other template"];

            var clone = prototypeFactory.Clone(prototype);
            Assert.That(clone.Templates, Is.Empty);
            Assert.That(clone.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(clone.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(clone.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(clone.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            Assert.That(clone.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(clone.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.Zero);
        }
    }
}
