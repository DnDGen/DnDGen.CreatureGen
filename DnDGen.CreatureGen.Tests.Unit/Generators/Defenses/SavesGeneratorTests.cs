using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class SavesGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IBonusSelector> mockBonusSelector;
        private ISavesGenerator savesGenerator;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private List<string> reflexSaveFeats;
        private List<string> fortitudeSaveFeats;
        private List<string> willSaveFeats;
        private List<string> strongSaves;
        private HitPoints hitPoints;
        private CreatureType creatureType;
        private Dictionary<string, List<BonusDataSelection>> racialBonuses;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockBonusSelector = new Mock<IBonusSelector>();
            savesGenerator = new SavesGenerator(mockCollectionsSelector.Object, mockBonusSelector.Object);
            feats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            reflexSaveFeats = new List<string>();
            fortitudeSaveFeats = new List<string>();
            willSaveFeats = new List<string>();
            strongSaves = new List<string>();
            hitPoints = new HitPoints();
            creatureType = new CreatureType();
            racialBonuses = new Dictionary<string, List<BonusDataSelection>>();

            hitPoints.HitDice.Add(new HitDice { Quantity = 1 });
            creatureType.Name = "creature type";
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            racialBonuses["creature"] = new List<BonusDataSelection>();
            racialBonuses[creatureType.Name] = new List<BonusDataSelection>();

            reflexSaveFeats.Add("other feat");
            fortitudeSaveFeats.Add("other feat");
            willSaveFeats.Add("other feat");

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, SaveConstants.Fortitude))
                .Returns(fortitudeSaveFeats);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, SaveConstants.Reflex))
                .Returns(reflexSaveFeats);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, SaveConstants.Will))
                .Returns(willSaveFeats);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SaveGroups, "creature"))
                .Returns(strongSaves);

            mockBonusSelector.Setup(s => s.SelectFor(TableNameConstants.TypeAndAmount.SaveBonuses, It.IsAny<string>())).Returns((string t, string s) => racialBonuses[s]);
        }

        [Test]
        public void ApplyAbilityBonuses()
        {
            abilities[AbilityConstants.Constitution].BaseScore = 9266;
            abilities[AbilityConstants.Dexterity].BaseScore = 90210;
            abilities[AbilityConstants.Wisdom].BaseScore = 1;

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Constitution].Modifier));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Wisdom]));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Wisdom].Modifier));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Will].Bonus, Is.Zero);
        }

        [Test]
        public void ApplyAbilityBonusesWhenAbilitiesHaveNoScore()
        {
            abilities[AbilityConstants.Constitution].BaseScore = 0;
            abilities[AbilityConstants.Dexterity].BaseScore = 0;
            abilities[AbilityConstants.Wisdom].BaseScore = 0;

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Constitution].Modifier));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Wisdom]));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Wisdom].Modifier));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].Bonus, Is.Zero);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.BaseValues))]
        public void SaveBaseValueComesFromCreature(bool isStrongFortitude, bool isStrongReflex, bool isStrongWill)
        {
            var strongSaveBonus = 2;
            var weakSaveBonus = 0;

            var expectedFortitude = weakSaveBonus;
            var expectedReflex = weakSaveBonus;
            var expectedWill = weakSaveBonus;

            if (isStrongFortitude)
            {
                strongSaves.Add(SaveConstants.Fortitude);
                expectedFortitude = strongSaveBonus;
            }

            if (isStrongReflex)
            {
                strongSaves.Add(SaveConstants.Reflex);
                expectedReflex = strongSaveBonus;
            }

            if (isStrongWill)
            {
                strongSaves.Add(SaveConstants.Will);
                expectedWill = strongSaveBonus;
            }

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(expectedFortitude));
            Assert.That(saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(expectedReflex));
            Assert.That(saves[SaveConstants.Will].BaseValue, Is.EqualTo(expectedWill));
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.BaseValuesFromHitDice))]
        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.FractionalHitDiceForBaseValues))]
        public void SaveBaseValuesBasedOnHitDice(double hitDiceQuantity, bool isStrongFortitude, bool isStrongReflex, bool isStrongWill)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;

            var strongSaveBonus = hitPoints.RoundedHitDiceQuantity / 2 + 2;
            var weakSaveBonus = hitPoints.RoundedHitDiceQuantity / 3;

            if (hitDiceQuantity == 0)
            {
                strongSaveBonus = 0;
                weakSaveBonus = 0;
            }

            var expectedFortitude = weakSaveBonus;
            var expectedReflex = weakSaveBonus;
            var expectedWill = weakSaveBonus;

            if (isStrongFortitude)
            {
                strongSaves.Add(SaveConstants.Fortitude);
                expectedFortitude = strongSaveBonus;
            }

            if (isStrongReflex)
            {
                strongSaves.Add(SaveConstants.Reflex);
                expectedReflex = strongSaveBonus;
            }

            if (isStrongWill)
            {
                strongSaves.Add(SaveConstants.Will);
                expectedWill = strongSaveBonus;
            }

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(expectedFortitude));
            Assert.That(saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(expectedReflex));
            Assert.That(saves[SaveConstants.Will].BaseValue, Is.EqualTo(expectedWill));
        }

        public class SavesGeneratorTestData
        {
            public static IEnumerable BaseValues
            {
                get
                {
                    var isStrong = new[] { true, false };

                    foreach (var fortitude in isStrong)
                    {
                        foreach (var reflex in isStrong)
                        {
                            foreach (var will in isStrong)
                            {
                                yield return new TestCaseData(fortitude, reflex, will);
                            }
                        }
                    }
                }
            }

            public static IEnumerable BaseValuesFromHitDice
            {
                get
                {
                    var isStrong = new[] { true, false };
                    var hitDiceQuantities = new[] { 0, 1, 2, 10, 20 };

                    foreach (var hitDiceQuantity in hitDiceQuantities)
                    {
                        foreach (var fortitude in isStrong)
                        {
                            foreach (var reflex in isStrong)
                            {
                                foreach (var will in isStrong)
                                {
                                    yield return new TestCaseData(hitDiceQuantity, fortitude, reflex, will);
                                }
                            }
                        }
                    }
                }
            }

            public static IEnumerable FractionalHitDiceForBaseValues
            {
                get
                {
                    var isStrong = new[] { true, false };
                    var hitDiceQuantities = new[] { 1 / 2d, 1 / 3d, 1 / 4d, 1 / 6d, 1 / 8d, 1 / 10d };

                    foreach (var hitDiceQuantity in hitDiceQuantities)
                    {
                        foreach (var fortitude in isStrong)
                        {
                            foreach (var reflex in isStrong)
                            {
                                foreach (var will in isStrong)
                                {
                                    yield return new TestCaseData(hitDiceQuantity, fortitude, reflex, will);
                                }
                            }
                        }
                    }
                }
            }

            public static IEnumerable CreatureBonusSource => NonNullSources;

            private static IEnumerable<string> Sources = new[] { null, GroupConstants.All, SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will };
            private static readonly IEnumerable<string> NonNullSources = Sources.Where(s => s != null);
        }

        [Test]
        public void ApplyFeatBonuses()
        {
            SetUpFeats();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(1));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(1));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);
            Assert.That(saves[SaveConstants.Fortitude].Bonuses, Is.Not.Empty);
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(1));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(2));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(2));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);
            Assert.That(saves[SaveConstants.Reflex].Bonuses, Is.Not.Empty);
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(1));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
            Assert.That(saves[SaveConstants.Will].Bonuses, Is.Not.Empty);
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(1));
        }

        private void SetUpFeats()
        {
            for (var i = 1; i <= 4; i++)
            {
                var feat = new Feat();
                feat.Name = $"Feat {i}";
                feat.Power = i;

                feats.Add(feat);
            }

            fortitudeSaveFeats.Add(feats[0].Name);
            reflexSaveFeats.Add(feats[1].Name);
            willSaveFeats.Add(feats[2].Name);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreature(string source)
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = source, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreature_Conditional(string source)
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = source, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.EqualTo(source == SaveConstants.Fortitude || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.EqualTo(source == SaveConstants.Reflex || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.EqualTo(source == SaveConstants.Will || source == GroupConstants.All));
        }

        [Test]
        public void GetSaveBonusesForCreature()
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [Test]
        public void GetSaveBonusesForCreature_WithSomeConditions()
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void GetSaveBonusesForCreature_WithAllConditions()
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreatureType(string source)
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = source, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreatureType_Conditional(string source)
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = source, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.EqualTo(source == SaveConstants.Fortitude || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.EqualTo(source == SaveConstants.Reflex || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.EqualTo(source == SaveConstants.Will || source == GroupConstants.All));
        }

        [Test]
        public void GetSaveBonusesForCreatureType()
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [Test]
        public void GetSaveBonusesForCreatureType_SomeConditional()
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void GetSaveBonusesForCreatureType_AllConditional()
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreatureSubtype(string source)
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = source, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), nameof(SavesGeneratorTestData.CreatureBonusSource))]
        public void GetSaveBonusForCreatureSubtype_Conditional(string source)
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = source, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.EqualTo(source == SaveConstants.Fortitude || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.EqualTo(source == SaveConstants.Reflex || source == GroupConstants.All));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.EqualTo(source == SaveConstants.Will || source == GroupConstants.All));
        }

        [Test]
        public void GetSaveBonusesForCreatureSubtype()
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.False);
        }

        [Test]
        public void GetSaveBonusesForCreatureSubtype_SomeConditional()
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void GetSaveBonusesForCreatureSubtype_AllConditional()
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void GetSaveBonusesForCreatureSubtypes()
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype 1", "subtype 2", "subtype 666" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 2"] = new List<BonusDataSelection>();
            racialBonuses["subtype 666"] = new List<BonusDataSelection>();

            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 2"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 2"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.False);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void GetSaveBonus_All()
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["creature"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++ });

            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses[creatureType.Name].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++ });

            creatureType.SubTypes = new[] { "subtype 1", "subtype 2", "subtype 666" };

            racialBonuses["subtype 1"] = new List<BonusDataSelection>();
            racialBonuses["subtype 2"] = new List<BonusDataSelection>();
            racialBonuses["subtype 666"] = new List<BonusDataSelection>();

            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = GroupConstants.All, Bonus = counter++ });
            racialBonuses["subtype 1"].Add(new BonusDataSelection { Target = SaveConstants.Fortitude, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 2"].Add(new BonusDataSelection { Target = SaveConstants.Will, Bonus = counter++, Condition = $"condition {counter++}" });
            racialBonuses["subtype 2"].Add(new BonusDataSelection { Target = SaveConstants.Reflex, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedAll = nonConditionalBonuses.Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();

            var expectedFortitude = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();

            var expectedReflex = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();

            var expectedWill = nonConditionalBonuses.Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(expectedAll + expectedFortitude));
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.True);

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.True);
        }

        [Test]
        public void IfMadness_ThenUseCharismaForWill()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;
            abilities[AbilityConstants.Constitution].BaseScore = 90210;
            abilities[AbilityConstants.Dexterity].BaseScore = 42;
            abilities[AbilityConstants.Wisdom].BaseScore = 600;

            var feat = new Feat();
            feat.Name = FeatConstants.SpecialQualities.Madness;

            feats.Add(feat);

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Constitution].Modifier));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Will].Bonus, Is.Zero);
        }

        [Test]
        public void IfNotMadness_ThenUseWisdomForWill()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;
            abilities[AbilityConstants.Constitution].BaseScore = 90210;
            abilities[AbilityConstants.Dexterity].BaseScore = 42;
            abilities[AbilityConstants.Wisdom].BaseScore = 600;

            var feat = new Feat();
            feat.Name = "not " + FeatConstants.SpecialQualities.Madness;

            feats.Add(new Feat { Name = "not " + FeatConstants.SpecialQualities.Madness });
            feats.Add(new Feat { Name = "not " + FeatConstants.SpecialQualities.Madness + " not" });
            feats.Add(new Feat { Name = FeatConstants.SpecialQualities.Madness + " not" });
            feats.Add(new Feat { Name = "other feat" });

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Constitution].Modifier));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Wisdom]));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Wisdom].Modifier));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Will].Bonus, Is.Zero);
        }

        [Test]
        public void UnearthlyGraceAddsCharismaBonusToAllSavingThrows()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;
            abilities[AbilityConstants.Constitution].BaseScore = 90210;
            abilities[AbilityConstants.Dexterity].BaseScore = 42;
            abilities[AbilityConstants.Wisdom].BaseScore = 600;

            var feat = new Feat();
            feat.Name = FeatConstants.SpecialQualities.UnearthlyGrace;

            feats.Add(feat);

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Constitution].Modifier + abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Fortitude].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.EqualTo(abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Reflex].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Dexterity].Modifier + abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Reflex].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Will].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Wisdom]));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.EqualTo(abilities[AbilityConstants.Wisdom].Modifier + abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(saves[SaveConstants.Will].TotalBonus, Is.Not.Zero);
            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(abilities[AbilityConstants.Charisma].Modifier));
        }
    }
}
