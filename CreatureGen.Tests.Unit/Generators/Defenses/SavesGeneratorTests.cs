using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Defenses
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
        private List<string> strongReflex;
        private List<string> strongFortitude;
        private List<string> strongWill;
        private HitPoints hitPoints;
        private CreatureType creatureType;
        private Dictionary<string, List<BonusSelection>> racialBonuses;

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
            strongFortitude = new List<string>();
            strongReflex = new List<string>();
            strongWill = new List<string>();
            hitPoints = new HitPoints();
            creatureType = new CreatureType();
            racialBonuses = new Dictionary<string, List<BonusSelection>>();

            hitPoints.HitDiceQuantity = 1;
            creatureType.Name = "creature type";
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            racialBonuses["creature"] = new List<BonusSelection>();
            racialBonuses[creatureType.Name] = new List<BonusSelection>();

            reflexSaveFeats.Add("other feat");
            fortitudeSaveFeats.Add("other feat");
            willSaveFeats.Add("other feat");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, SaveConstants.Fortitude))
                .Returns(fortitudeSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, SaveConstants.Reflex))
                .Returns(reflexSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, SaveConstants.Will))
                .Returns(willSaveFeats);
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, SaveConstants.Fortitude))
                .Returns(strongFortitude);
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, SaveConstants.Reflex))
                .Returns(strongReflex);
            mockCollectionsSelector.Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, SaveConstants.Will))
                .Returns(strongWill);

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

        [TestCaseSource(typeof(SavesGeneratorTestData), "BaseValues")]
        public void SaveBaseValueComesFromCreature(bool isStrongFortitude, bool isStrongReflex, bool isStrongWill)
        {
            var strongSaveBonus = 2;
            var weakSaveBonus = 0;

            var expectedFortitude = weakSaveBonus;
            var expectedReflex = weakSaveBonus;
            var expectedWill = weakSaveBonus;

            if (isStrongFortitude)
            {
                strongFortitude.Add("creature");
                expectedFortitude = strongSaveBonus;
            }

            if (isStrongReflex)
            {
                strongReflex.Add("creature");
                expectedReflex = strongSaveBonus;
            }

            if (isStrongWill)
            {
                strongWill.Add("creature");
                expectedWill = strongSaveBonus;
            }

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));
            Assert.That(saves[SaveConstants.Fortitude].BaseValue, Is.EqualTo(expectedFortitude));
            Assert.That(saves[SaveConstants.Reflex].BaseValue, Is.EqualTo(expectedReflex));
            Assert.That(saves[SaveConstants.Will].BaseValue, Is.EqualTo(expectedWill));
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), "BaseValuesFromHitDice")]
        [TestCaseSource(typeof(SavesGeneratorTestData), "FractionalHitDiceForBaseValues")]
        public void SaveBaseValuesBasedOnHitDice(double hitDiceQuantity, bool isStrongFortitude, bool isStrongReflex, bool isStrongWill)
        {
            hitPoints.HitDiceQuantity = hitDiceQuantity;

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
                strongFortitude.Add("creature");
                expectedFortitude = strongSaveBonus;
            }

            if (isStrongReflex)
            {
                strongReflex.Add("creature");
                expectedReflex = strongSaveBonus;
            }

            if (isStrongWill)
            {
                strongWill.Add("creature");
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
                    var hitDiceQuantities = Enumerable.Range(0, 20);
                    hitDiceQuantities = hitDiceQuantities.Union(NumericTestData.NonNegativeValues);

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
                    var hitDiceQuantities = Enumerable.Range(2, 10).Select(q => 1d / q);

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

            public static IEnumerable CreatureBonus
            {
                get
                {
                    foreach (var creatureSource in Sources)
                    {
                        foreach (var creatureTypeSource in Sources)
                        {
                            yield return new TestCaseData(creatureSource, creatureTypeSource, Enumerable.Empty<string>());

                            foreach (var subtypeSource1 in Sources)
                            {
                                yield return new TestCaseData(creatureSource, creatureTypeSource, new[] { subtypeSource1 });

                                foreach (var subtypeSource2 in Sources)
                                {
                                    yield return new TestCaseData(creatureSource, creatureTypeSource, new[] { subtypeSource1, subtypeSource2 });
                                }
                            }
                        }
                    }
                }
            }

            public static IEnumerable CreatureBonuses
            {
                get
                {
                    foreach (var source1 in NonNullSources)
                    {
                        foreach (var source2 in NonNullSources)
                        {
                            yield return new[] { source1, source2 };
                        }
                    }
                }
            }

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

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonus")]
        public void GetSaveBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
        {
            var counter = 1;

            if (!string.IsNullOrEmpty(creatureSource))
                racialBonuses["creature"].Add(new BonusSelection { Target = creatureSource, Bonus = counter++ });

            if (!string.IsNullOrEmpty(creatureTypeSource))
                racialBonuses[creatureType.Name].Add(new BonusSelection { Target = creatureTypeSource, Bonus = counter++ });

            var subtypes = Enumerable.Range(1, subtypeSources.Count()).Select(i => $"subtype {i}");
            creatureType.SubTypes = subtypes;

            for (var i = 0; i < subtypeSources.Count(); i++)
            {
                var subtype = creatureType.SubTypes.ElementAt(i);
                var source = subtypeSources.ElementAt(i);

                racialBonuses[subtype] = new List<BonusSelection>();

                if (!string.IsNullOrEmpty(source))
                    racialBonuses[subtype].Add(new BonusSelection { Target = source, Bonus = counter++ });
            }

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

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonus")]
        public void GetConditionalSaveBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
        {
            var counter = 1;

            if (!string.IsNullOrEmpty(creatureSource))
                racialBonuses["creature"].Add(new BonusSelection { Target = creatureSource, Bonus = counter++ });

            if (!string.IsNullOrEmpty(creatureTypeSource))
                racialBonuses[creatureType.Name].Add(new BonusSelection { Target = creatureTypeSource, Bonus = counter++ });

            var subtypes = Enumerable.Range(1, subtypeSources.Count()).Select(i => $"subtype {i}");
            creatureType.SubTypes = subtypes;

            for (var i = 0; i < subtypeSources.Count(); i++)
            {
                var subtype = creatureType.SubTypes.ElementAt(i);
                var source = subtypeSources.ElementAt(i);

                racialBonuses[subtype] = new List<BonusSelection>();

                if (!string.IsNullOrEmpty(source))
                    racialBonuses[subtype].Add(new BonusSelection { Target = source, Bonus = counter++ });
            }

            foreach (var bonuses in racialBonuses.Values.Where(v => v.Any()))
            {
                bonuses.First().Condition = "condition";
            }

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
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.EqualTo(saves[SaveConstants.Fortitude].Bonuses.Any()));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.EqualTo(expectedAll + expectedReflex));
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.EqualTo(saves[SaveConstants.Reflex].Bonuses.Any()));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.EqualTo(expectedAll + expectedWill));
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.EqualTo(saves[SaveConstants.Will].Bonuses.Any()));
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonus")]
        public void GetAllConditionalSaveBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
        {
            var counter = 1;

            if (!string.IsNullOrEmpty(creatureSource))
                racialBonuses["creature"].Add(new BonusSelection { Target = creatureSource, Bonus = counter++, Condition = $"condition {counter}" });

            if (!string.IsNullOrEmpty(creatureTypeSource))
                racialBonuses[creatureType.Name].Add(new BonusSelection { Target = creatureTypeSource, Bonus = counter++, Condition = $"condition {counter}" });

            var subtypes = Enumerable.Range(1, subtypeSources.Count()).Select(i => $"subtype {i}");
            creatureType.SubTypes = subtypes;

            for (var i = 0; i < subtypeSources.Count(); i++)
            {
                var subtype = creatureType.SubTypes.ElementAt(i);
                var source = subtypeSources.ElementAt(i);

                racialBonuses[subtype] = new List<BonusSelection>();

                if (!string.IsNullOrEmpty(source))
                    racialBonuses[subtype].Add(new BonusSelection { Target = source, Bonus = counter++, Condition = $"condition {counter}" });
            }

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var expectedAllCount = allBonuses.Where(b => b.Target == GroupConstants.All).Count();
            var expectedFortitudeCount = allBonuses.Where(b => b.Target == SaveConstants.Fortitude).Count();
            var expectedReflexCount = allBonuses.Where(b => b.Target == SaveConstants.Reflex).Count();
            var expectedWillCount = allBonuses.Where(b => b.Target == SaveConstants.Will).Count();

            var saves = savesGenerator.GenerateWith("creature", creatureType, hitPoints, feats, abilities);
            Assert.That(saves.Count, Is.EqualTo(3));

            Assert.That(saves[SaveConstants.Fortitude].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Fortitude].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedFortitudeCount));
            Assert.That(saves[SaveConstants.Fortitude].IsConditional, Is.EqualTo(saves[SaveConstants.Fortitude].Bonuses.Any()));

            Assert.That(saves[SaveConstants.Reflex].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Reflex].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedReflexCount));
            Assert.That(saves[SaveConstants.Reflex].IsConditional, Is.EqualTo(saves[SaveConstants.Reflex].Bonuses.Any()));

            Assert.That(saves[SaveConstants.Will].Bonus, Is.Zero);
            Assert.That(saves[SaveConstants.Will].Bonuses.Count, Is.EqualTo(expectedAllCount + expectedWillCount));
            Assert.That(saves[SaveConstants.Will].IsConditional, Is.EqualTo(saves[SaveConstants.Will].Bonuses.Any()));
        }

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonuses")]
        public void GetSaveBonusesFromCreature(string source1, string source2)
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var expectedAll = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Count();
            var expectedFortitude = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Count();
            var expectedReflex = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Count();
            var expectedWill = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Count();

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

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonuses")]
        public void GetSaveBonusesFromCreatureType(string source1, string source2)
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var expectedAll = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Count();
            var expectedFortitude = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Count();
            var expectedReflex = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Count();
            var expectedWill = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Count();

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

        [TestCaseSource(typeof(SavesGeneratorTestData), "CreatureBonuses")]
        public void GetSaveBonusesFromCreatureSubtype(string source1, string source2)
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype" };

            racialBonuses["subtype"] = new List<BonusSelection>();
            racialBonuses["subtype"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["subtype"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var expectedAll = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Sum(b => b.Bonus);
            var expectedAllCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == GroupConstants.All).Count();
            var expectedFortitude = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Sum(b => b.Bonus);
            var expectedFortitudeCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Fortitude).Count();
            var expectedReflex = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Sum(b => b.Bonus);
            var expectedReflexCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Reflex).Count();
            var expectedWill = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Sum(b => b.Bonus);
            var expectedWillCount = racialBonuses.Values.SelectMany(v => v).Where(b => b.Target == SaveConstants.Will).Count();

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
    }
}
