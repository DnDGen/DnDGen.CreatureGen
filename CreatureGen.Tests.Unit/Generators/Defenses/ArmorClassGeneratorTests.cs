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
    public class ArmorClassGeneratorTests
    {
        private IArmorClassGenerator armorClassGenerator;
        private List<Feat> feats;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IBonusSelector> mockBonusSelector;
        private CreatureType creatureType;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, List<BonusSelection>> racialBonuses;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockBonusSelector = new Mock<IBonusSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);

            feats = new List<Feat>();
            creatureType = new CreatureType();
            abilities = new Dictionary<string, Ability>();
            racialBonuses = new Dictionary<string, List<BonusSelection>>();

            creatureType.Name = "creature type";
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            racialBonuses["creature"] = new List<BonusSelection>();
            racialBonuses[creatureType.Name] = new List<BonusSelection>();

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(0);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor)).Returns(Enumerable.Empty<string>());

            mockBonusSelector.Setup(s => s.SelectFor(TableNameConstants.TypeAndAmount.ArmorClassBonuses, It.IsAny<string>())).Returns((string t, string s) => racialBonuses[s]);
        }

        [Test]
        public void ArmorClassesStartsAtBase()
        {
            GenerateAndAssertArmorClass();
        }

        private ArmorClass GenerateAndAssertArmorClass(int full = ArmorClass.BaseArmorClass, int flatFooted = ArmorClass.BaseArmorClass, int touch = ArmorClass.BaseArmorClass, bool isConditional = false, int naturalArmor = 0)
        {
            var armorClass = armorClassGenerator.GenerateWith(abilities, "size", "creature", creatureType, feats, naturalArmor);

            Assert.That(armorClass.TotalBonus, Is.EqualTo(full), "full");
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(flatFooted), "flat-footed");
            Assert.That(armorClass.TouchBonus, Is.EqualTo(touch), "touch");
            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(naturalArmor));

            return armorClass;
        }

        //INFO: Example here is Githzerai's Inertial Armor
        [Test]
        public void AddArmorBonusFromFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item", "feat", "wrong feat" });

            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Power = 1;
            feats[1].Name = "other feat";
            feats[1].Power = -1;

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void DexterityBonusApplied()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 12;
            var armorClass = GenerateAndAssertArmorClass(11, touch: 11);
        }

        [Test]
        public void NegativeDexterityBonusApplied()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 9;
            var armorClass = GenerateAndAssertArmorClass(9, touch: 9);
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(10, 1)]
        [TestCase(11, 1)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        public void IncorporealCreaturesGetDeflectionBonusEqualToCharismaModifier(int charisma, int bonus)
        {
            abilities[AbilityConstants.Charisma].BaseScore = charisma;

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void IncorporealCreaturesGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var bonus = abilities[AbilityConstants.Charisma].Modifier;

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void CorporealCreaturesDoNotGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", "subtype" };

            var armorClass = GenerateAndAssertArmorClass(10, 10, 10);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
        }

        [TestCaseSource(typeof(NumericTestData), "AllTestValues")]
        public void SizeModifiesArmorClass(int modifier)
        {
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(modifier);

            var armorClass = GenerateAndAssertArmorClass(ArmorClass.BaseArmorClass + modifier, ArmorClass.BaseArmorClass + modifier, ArmorClass.BaseArmorClass + modifier);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(modifier));
        }

        [Test]
        public void NaturalArmorApplied()
        {
            var armorClass = GenerateAndAssertArmorClass(9276, 9276, naturalArmor: 9266);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(9266));
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 12;

            var feat = new Feat();
            feat.Name = "feat 1";
            feat.Power = 1;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "feat 1" });

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(1);

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var armorClass = GenerateAndAssertArmorClass(15, 14, 13, naturalArmor: 1);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(1));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(1));
            Assert.That(armorClass.Dexterity.Modifier, Is.EqualTo(1));
        }

        public class ArmorClassGeneratorTestData
        {
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

            //INFO: Null here signifies no bonus
            private static readonly IEnumerable<string> Sources = new[]
            {
                null,
                ArmorClassConstants.Dodge,
                ArmorClassConstants.Armor,
                ArmorClassConstants.Shield,
                ArmorClassConstants.Deflection,
                ArmorClassConstants.Natural,
            };

            private static IEnumerable<string> NonNullSources => Sources.Where(s => s != null);
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonus")]
        public void GetArmorClassBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
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

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.False);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonus")]
        public void GetConditionalArmorClassBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
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

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.EqualTo(armorClass.Bonuses.Any()));

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonus")]
        public void GetAllConditionalArmorClassBonusForCreature(string creatureSource, string creatureTypeSource, IEnumerable<string> subtypeSources)
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
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.EqualTo(armorClass.Bonuses.Any()));

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonuses")]
        public void GetArmorClassBonusesFromCreature(string source1, string source2)
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.False);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonuses")]
        public void GetArmorClassBonusesFromCreatureType(string source1, string source2)
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.False);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), "CreatureBonuses")]
        public void GetArmorClassBonusesFromCreatureSubtype(string source1, string source2)
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype" };

            racialBonuses["subtype"] = new List<BonusSelection>();
            racialBonuses["subtype"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["subtype"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Max(b => b.Bonus);
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Max(b => b.Bonus);
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Max(b => b.Bonus);
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Max(b => b.Bonus);
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);
            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
            Assert.That(armorClass.IsConditional, Is.False);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expectedArmor));
            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(expectedArmorCount));

            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expectedDeflection));
            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(expectedDeflectionCount));

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(expectedDodge));
            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(expectedDodgeCount));

            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expectedNatural));
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(expectedNaturalCount));

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expectedShield));
            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(expectedShieldCount));
        }

        [Test, Ignore("Have to have equipment first")]
        public void SetMaxDexterityBonusBasedOnArmor()
        {
            Assert.Fail("not yet written");
        }
    }
}