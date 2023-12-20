using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class LycanthropeApplicatorIsCompatibleTests
    {
        private LycanthropeApplicator applicator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<Dice> mockDice;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockDice = new Mock<Dice>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();

            applicator = new LycanthropeApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object,
                mockAdjustmentSelector.Object,
                mockPrototypeFactory.Object);
            applicator.LycanthropeSpecies = "my lycanthrope";
            applicator.AnimalSpecies = "my animal";
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = 1;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible
        {
            get
            {
                var compatibilities = new[]
                {
                    (CreatureConstants.Types.Aberration, false),
                    (CreatureConstants.Types.Animal, false),
                    (CreatureConstants.Types.Construct, false),
                    (CreatureConstants.Types.Dragon, false),
                    (CreatureConstants.Types.Elemental, false),
                    (CreatureConstants.Types.Fey, false),
                    (CreatureConstants.Types.Giant, true),
                    (CreatureConstants.Types.Humanoid, true),
                    (CreatureConstants.Types.MagicalBeast, false),
                    (CreatureConstants.Types.MonstrousHumanoid, false),
                    (CreatureConstants.Types.Ooze, false),
                    (CreatureConstants.Types.Outsider, false),
                    (CreatureConstants.Types.Plant, false),
                    (CreatureConstants.Types.Undead, false),
                    (CreatureConstants.Types.Vermin, false),
                };

                foreach (var compatibility in compatibilities)
                {
                    yield return new TestCaseData(compatibility.Item1, compatibility.Item2);
                }
            }
        }

        [TestCaseSource(nameof(SizeCompatible))]
        public void IsCompatible_BySize(string creatureSize, string animalSize, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = creatureSize };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = animalSize };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = 1;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        //INFO: Reducing the number of test cases, only getting 1 size too big or too small
        private static IEnumerable SizeCompatible
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var c = 0; c < sizes.Length; c++)
                {
                    var startCompare = Math.Max(0, c - 2);
                    var endCompare = Math.Min(sizes.Length - 1, c + 2);

                    for (var a = startCompare; a <= endCompare; a++)
                    {
                        var compatible = Math.Abs(c - a) <= 1;

                        yield return new TestCaseData(sizes[c], sizes[a], compatible);
                    }
                }
            }
        }

        [TestCaseSource(nameof(CreatureTypeCompatible_Filtered))]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = 1;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible_Filtered
        {
            get
            {
                yield return new TestCaseData(null, true);
                yield return new TestCaseData(CreatureConstants.Types.Humanoid, true);
                yield return new TestCaseData(CreatureConstants.Types.Animal, false);
                yield return new TestCaseData("subtype 1", true);
                yield return new TestCaseData("subtype 2", true);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, false);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Shapechanger, true);
                yield return new TestCaseData("wrong type", false);
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_ChallengeRatingMustMatch(string original, double animalHitDiceQuantity, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = animalHitDiceQuantity;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered_HumanoidCharacter))]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(
            string original,
            double animalHitDiceQuantity,
            double creatureHitDiceQuantity,
            string challengeRating,
            bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = animalHitDiceQuantity;
            hitDice["my creature"] = creatureHitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        private static IEnumerable ChallengeRatingAdjustments_Filtered_HumanoidCharacter
        {
            get
            {
                var challengeRatings = new Dictionary<string, IEnumerable<double>>();
                challengeRatings[ChallengeRatingConstants.CR0] = new[] { 0d }; //Humanoid Character
                challengeRatings[ChallengeRatingConstants.CR1_4th] = new[] { 1d }; //Kobold
                challengeRatings[ChallengeRatingConstants.CR1_3rd] = new[] { 1d }; //Goblin
                challengeRatings[ChallengeRatingConstants.CR1_2nd] = new[] { 1d }; //Dwarf, Elf, Gnome, Halfling, Hobgoblin, Merfolk, Orc, Human
                challengeRatings[ChallengeRatingConstants.CR1] = new[] { 1d, 2d }; //Duergar, Drow, Gnoll, Svirfneblin, Lizardfolk, Troglodyte
                challengeRatings[ChallengeRatingConstants.CR3] = new[] { 4d }; //Ogre
                challengeRatings[ChallengeRatingConstants.CR5] = new[] { 6d }; //Troll
                challengeRatings[ChallengeRatingConstants.CR6] = new[] { 10d }; //Ettin
                challengeRatings[ChallengeRatingConstants.CR7] = new[] { 12d }; //Hill Giant
                challengeRatings[ChallengeRatingConstants.CR8] = new[] { 14d, 5d }; //Stone Giant, Ogre Mage
                challengeRatings[ChallengeRatingConstants.CR9] = new[] { 14d }; //Frost Giant, Stone Giant Elder
                challengeRatings[ChallengeRatingConstants.CR10] = new[] { 15d }; //Fire Giant
                challengeRatings[ChallengeRatingConstants.CR11] = new[] { 17d }; //Cloud Giant
                challengeRatings[ChallengeRatingConstants.CR13] = new[] { 19d }; //Storm Giant

                var animalHitDiceQuantities = new[]
                {
                    6, //Brown Bear, Dire Wolf, Tiger
                    3, //Boar
                    7, //Dire boar
                    1, //Dire rat
                    2, //Wolf
                };

                foreach (var animalHitDiceQuantity in animalHitDiceQuantities)
                {
                    var increase = 0;

                    if (animalHitDiceQuantity <= 2)
                        increase = 2;
                    else if (animalHitDiceQuantity <= 5)
                        increase = 3;
                    else if (animalHitDiceQuantity <= 10)
                        increase = 4;
                    else if (animalHitDiceQuantity <= 20)
                        increase = 5;
                    else if (animalHitDiceQuantity > 20)
                        increase = 6;

                    foreach (var cr in challengeRatings)
                    {
                        var creatureCr = cr.Key;

                        foreach (var creatureHitDiceQuantity in cr.Value)
                        {
                            if (creatureHitDiceQuantity <= 1)
                                creatureCr = ChallengeRatingConstants.CR0;

                            var low1Cr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase - 1);
                            var newCr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase);
                            var high1Cr = ChallengeRatingConstants.IncreaseChallengeRating(creatureCr, increase + 1);

                            if (newCr != creatureCr)
                            {
                                yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, creatureCr, false);
                            }

                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, low1Cr, false);
                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, newCr, true);
                            yield return new TestCaseData(creatureCr, animalHitDiceQuantity, creatureHitDiceQuantity, high1Cr, false);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(
            string original,
            double animalHitDiceQuantity,
            string challengeRating,
            bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = animalHitDiceQuantity;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        //Animal HD 0-2, +2
        //Animal HD 3-5, +3
        //Animal HD 6-10, +4
        //Animal HD 11-20, +5
        //Animal HD 21+, +6
        private static IEnumerable ChallengeRatingAdjustments_Filtered
        {
            get
            {
                //INFO: Doing specific numbers, instead of full range because the number of test cases explodes:
                //1. Per challenge rating
                //2. Per hit die quantity
                var challengeRatings = new[]
                {
                    ChallengeRatingConstants.CR0, //Character
                    ChallengeRatingConstants.CR1_4th, //Kobold
                    ChallengeRatingConstants.CR1_3rd, //Goblin
                    ChallengeRatingConstants.CR1_2nd, //Dwarf, Elf, Gnome, Half-Elf, Halforc, Halfling, Hobgoblin, Human, Merfolk, Orc
                    ChallengeRatingConstants.CR1, //Duergar, Drow, Gnoll, Svirfneblin, Lizardfolk, Troglodyte
                    ChallengeRatingConstants.CR3, //Ogre
                    ChallengeRatingConstants.CR5, //Troll
                    ChallengeRatingConstants.CR6, //Ettin
                    ChallengeRatingConstants.CR7, //Hill Giant
                    ChallengeRatingConstants.CR8, //Stone Giant, Ogre Mage
                    ChallengeRatingConstants.CR9, //Frost Giant, Stone Giant Elder
                    ChallengeRatingConstants.CR10, //Fire Giant
                    ChallengeRatingConstants.CR11, //Cloud Giant
                    ChallengeRatingConstants.CR13, //Storm Giant
                };

                var hitDiceQuantities = new[]
                {
                    0.5, 1, 2, 3, 4, 5, 6, 9, 10, 11, 19, 20, 21
                };

                foreach (var animalHitDiceQuantity in hitDiceQuantities)
                {
                    var increase = 0;

                    if (animalHitDiceQuantity <= 2)
                        increase = 2;
                    else if (animalHitDiceQuantity <= 5)
                        increase = 3;
                    else if (animalHitDiceQuantity <= 10)
                        increase = 4;
                    else if (animalHitDiceQuantity <= 20)
                        increase = 5;
                    else if (animalHitDiceQuantity > 20)
                        increase = 6;

                    foreach (var cr in challengeRatings)
                    {
                        var low1Cr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase - 1);
                        var newCr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase);
                        var high1Cr = ChallengeRatingConstants.IncreaseChallengeRating(cr, increase + 1);

                        if (newCr != cr)
                        {
                            yield return new TestCaseData(cr, animalHitDiceQuantity, cr, false);
                        }

                        yield return new TestCaseData(cr, animalHitDiceQuantity, low1Cr, false);
                        yield return new TestCaseData(cr, animalHitDiceQuantity, newCr, true);
                        yield return new TestCaseData(cr, animalHitDiceQuantity, high1Cr, false);
                    }
                }
            }
        }

        [TestCaseSource(nameof(Alignments_Filtered))]
        public void IsCompatible_AlignmentMustMatch(
            string creatureAlignment,
            string alignment,
            bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = 1;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", creatureAlignment, "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" });
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(new[] { "wrong creature 2", "wrong creature 1", "wrong creature 3" });

            var filters = new Filters { Alignment = alignment };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable Alignments_Filtered
        {
            get
            {
                yield return new TestCaseData("preset alignment", "preset alignment", true);
                yield return new TestCaseData("preset alignment", "wrong alignment", false);
                yield return new TestCaseData("other alignment", "preset alignment", false);
            }
        }

        [TestCaseSource(nameof(AllFilters))]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2, Size = SizeConstants.Medium };
            data["my animal"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my animal"] = 1;
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" });
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(new[] { "wrong creature 2", "wrong creature 1", "wrong creature 3" });

            var filters = new Filters { Alignment = alignment, Type = type, ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable AllFilters
        {
            get
            {
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Shapechanger, ChallengeRatingConstants.CR2, "preset alignment", false);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Shapechanger, ChallengeRatingConstants.CR2, "wrong alignment", false);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Shapechanger, ChallengeRatingConstants.CR4, "preset alignment", true);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Shapechanger, ChallengeRatingConstants.CR4, "wrong alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR2, "preset alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR2, "wrong alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR4, "preset alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR4, "wrong alignment", false);
            }
        }
    }
}
