using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class ArmorClassGeneratorTests
    {
        private IArmorClassGenerator armorClassGenerator;
        private List<Feat> feats;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IBonusSelector> mockBonusSelector;
        private CreatureType creatureType;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, List<BonusSelection>> racialBonuses;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockBonusSelector = new Mock<IBonusSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockBonusSelector.Object, mockAdjustmentsSelector.Object);

            feats = new List<Feat>();
            creatureType = new CreatureType();
            abilities = new Dictionary<string, Ability>();
            racialBonuses = new Dictionary<string, List<BonusSelection>>();
            equipment = new Equipment();

            creatureType.Name = "creature type";
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            racialBonuses["creature"] = new List<BonusSelection>();
            racialBonuses[creatureType.Name] = new List<BonusSelection>();

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(0);

            mockBonusSelector.Setup(s => s.SelectFor(TableNameConstants.TypeAndAmount.ArmorClassBonuses, It.IsAny<string>())).Returns((string t, string s) => racialBonuses[s]);
        }

        [Test]
        public void ArmorClassesStartsAtBase()
        {
            GenerateAndAssertArmorClass();
        }

        private ArmorClass GenerateAndAssertArmorClass(int full = ArmorClass.BaseArmorClass, int flatFooted = ArmorClass.BaseArmorClass, int touch = ArmorClass.BaseArmorClass, bool isConditional = false, int naturalArmor = 0)
        {
            var armorClass = armorClassGenerator.GenerateWith(abilities, "size", "creature", creatureType, feats, naturalArmor, equipment);

            Assert.That(armorClass.TotalBonus, Is.EqualTo(full), "full");
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(flatFooted), "flat-footed");
            Assert.That(armorClass.TouchBonus, Is.EqualTo(touch), "touch");
            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            return armorClass;
        }

        //INFO: Example here is Githzerai's Inertial Armor
        [Test]
        public void InertialArmorGrantsArmorBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.SpecialQualities.InertialArmor;
            feats[0].Power = 42;
            feats[1].Name = "other feat";
            feats[1].Power = -1;

            var armorClass = GenerateAndAssertArmorClass(52, 52);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(42));
        }

        //INFO: Example here is Githzerai's Inertial Armor
        [Test]
        public void FeatDoesNotGrantArmorBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Power = 42;
            feats[1].Name = "other feat";
            feats[1].Power = -1;

            var armorClass = GenerateAndAssertArmorClass();
            Assert.That(armorClass.ArmorBonus, Is.Zero);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.BaseAbilityTestNumbers))]
        public void DexterityBonusApplied(int abilityValue)
        {
            abilities[AbilityConstants.Dexterity].BaseScore = abilityValue;

            var expected = ArmorClass.BaseArmorClass + abilities[AbilityConstants.Dexterity].Modifier;
            GenerateAndAssertArmorClass(expected, touch: expected);
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

            racialBonuses["other subtype"] = new List<BonusSelection>();
            racialBonuses[CreatureConstants.Types.Subtypes.Incorporeal] = new List<BonusSelection>();

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void IncorporealCreaturesGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            racialBonuses["other subtype"] = new List<BonusSelection>();
            racialBonuses[CreatureConstants.Types.Subtypes.Incorporeal] = new List<BonusSelection>();

            var bonus = abilities[AbilityConstants.Charisma].Modifier;

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void CorporealCreaturesDoNotGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", "subtype" };

            racialBonuses["other subtype"] = new List<BonusSelection>();
            racialBonuses["subtype"] = new List<BonusSelection>();

            var armorClass = GenerateAndAssertArmorClass(10, 10, 10);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllTestValues))]
        public void SizeModifiesArmorClass(int modifier)
        {
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(modifier);

            var expected = Math.Max(1, ArmorClass.BaseArmorClass + modifier);
            var armorClass = GenerateAndAssertArmorClass(expected, expected, expected);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(modifier));
        }

        [Test]
        public void NaturalArmorApplied()
        {
            var armorClass = GenerateAndAssertArmorClass(9276, 9276, naturalArmor: 9266);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(9266));
            Assert.That(armorClass.NaturalArmorBonuses, Is.Not.Empty);
            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.NaturalArmorBonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void NaturalArmorNotApplied()
        {
            var armorClass = GenerateAndAssertArmorClass();
            Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(armorClass.NaturalArmorBonuses, Is.Empty);
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 12;

            var feat = new Feat();
            feat.Name = FeatConstants.SpecialQualities.InertialArmor;
            feat.Power = 1;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(1);

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            racialBonuses["other subtype"] = new List<BonusSelection>();
            racialBonuses[CreatureConstants.Types.Subtypes.Incorporeal] = new List<BonusSelection>();

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

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonus))]
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

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonus))]
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

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch, allBonuses.Any());

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonus))]
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

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch, allBonuses.Any());

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonuses))]
        public void GetArmorClassBonusesFromCreature(string source1, string source2)
        {
            var counter = 1;

            racialBonuses["creature"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["creature"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonuses))]
        public void GetArmorClassBonusesFromCreatureType(string source1, string source2)
        {
            var counter = 1;

            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses[creatureType.Name].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [TestCaseSource(typeof(ArmorClassGeneratorTestData), nameof(ArmorClassGeneratorTestData.CreatureBonuses))]
        public void GetArmorClassBonusesFromCreatureSubtype(string source1, string source2)
        {
            var counter = 1;

            creatureType.SubTypes = new[] { "subtype" };

            racialBonuses["subtype"] = new List<BonusSelection>();
            racialBonuses["subtype"].Add(new BonusSelection { Target = source1, Bonus = counter++ });
            racialBonuses["subtype"].Add(new BonusSelection { Target = source2, Bonus = counter++ });

            var allBonuses = racialBonuses.Values.SelectMany(v => v);
            var nonConditionalBonuses = allBonuses.Where(b => string.IsNullOrEmpty(b.Condition));

            var expectedArmor = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedArmorCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Armor).Count();

            var expectedDeflection = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedDeflectionCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Deflection).Count();

            var expectedDodge = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Sum(b => b.Bonus);
            var expectedDodgeCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Dodge).Count();

            var expectedNatural = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedNaturalCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Natural).Count();

            var expectedShield = nonConditionalBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Select(b => b.Bonus).DefaultIfEmpty().Max();
            var expectedShieldCount = allBonuses.Where(b => b.Target == ArmorClassConstants.Shield).Count();

            var expectedTotal = ArmorClass.BaseArmorClass + expectedArmor + expectedDeflection + expectedDodge + expectedNatural + expectedShield;
            var expectedFlatFooted = expectedTotal - expectedDodge;
            var expectedTouch = expectedTotal - expectedArmor - expectedShield - expectedNatural;

            var armorClass = GenerateAndAssertArmorClass(expectedTotal, expectedFlatFooted, expectedTouch);

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

            Assert.That(armorClass.Bonuses.Count, Is.EqualTo(expectedArmorCount + expectedDeflectionCount + expectedDodgeCount + expectedNaturalCount + expectedShieldCount));
        }

        [Test]
        public void ApplyArmorBonus_NoArmorOrShield()
        {
            equipment.Armor = null;
            equipment.Shield = null;

            var armorClass = GenerateAndAssertArmorClass();

            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.ArmorBonuses, Is.Empty);

            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonuses, Is.Empty);

        }

        [Test]
        public void ApplyArmorBonusFromArmor()
        {
            equipment.Armor = new Armor
            {
                ArmorBonus = 42
            };
            equipment.Shield = null;

            var armorClass = GenerateAndAssertArmorClass(52, 52, 10);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(42));
            Assert.That(armorClass.ArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42));
            Assert.That(bonus.IsConditional, Is.False);

            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonuses, Is.Empty);
        }

        [Test]
        public void ApplyArmorBonusFromMagicalArmor()
        {
            equipment.Armor = new Armor
            {
                ArmorBonus = 42,
                Magic = new TreasureGen.Items.Magical.Magic { Bonus = 96 }
            };
            equipment.Shield = null;

            var armorClass = GenerateAndAssertArmorClass(52 + 96, 52 + 96, 10);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(42 + 96));
            Assert.That(armorClass.ArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42 + 96));
            Assert.That(bonus.IsConditional, Is.False);

            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonuses, Is.Empty);
        }

        [Test]
        public void ApplyArmorBonusFromCursedMagicalArmor()
        {
            equipment.Armor = new Armor
            {
                ArmorBonus = 42,
                Magic = new TreasureGen.Items.Magical.Magic { Bonus = -9 }
            };
            equipment.Shield = null;

            var armorClass = GenerateAndAssertArmorClass(52 - 9, 52 - 9, 10);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(42 - 9));
            Assert.That(armorClass.ArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42 - 9));
            Assert.That(bonus.IsConditional, Is.False);

            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonuses, Is.Empty);
        }

        [Test]
        public void ApplyShieldBonusFromShield()
        {
            equipment.Armor = null;
            equipment.Shield = new Armor
            {
                ArmorBonus = 42
            }; ;

            var armorClass = GenerateAndAssertArmorClass(52, 52, 10);

            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.ArmorBonuses, Is.Empty);

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(42));
            Assert.That(armorClass.ShieldBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ShieldBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyShieldBonusFromMagicalShield()
        {
            equipment.Armor = null;
            equipment.Shield = new Armor
            {
                ArmorBonus = 42,
                Magic = new TreasureGen.Items.Magical.Magic { Bonus = 96 }
            }; ;

            var armorClass = GenerateAndAssertArmorClass(52 + 96, 52 + 96, 10);

            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.ArmorBonuses, Is.Empty);

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(42 + 96));
            Assert.That(armorClass.ShieldBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ShieldBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42 + 96));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyShieldBonusFromCursedMagicalShield()
        {
            equipment.Armor = null;
            equipment.Shield = new Armor
            {
                ArmorBonus = 42,
                Magic = new TreasureGen.Items.Magical.Magic { Bonus = -9 }
            }; ;

            var armorClass = GenerateAndAssertArmorClass(52 - 9, 52 - 9, 10);

            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.ArmorBonuses, Is.Empty);

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(42 - 9));
            Assert.That(armorClass.ShieldBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ShieldBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42 - 9));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyBonusesFromArmorAndShield()
        {
            equipment.Armor = new Armor
            {
                ArmorBonus = 42,
            };
            equipment.Shield = new Armor
            {
                ArmorBonus = 96,
            };

            var armorClass = GenerateAndAssertArmorClass(52 + 96, 52 + 96, 10);

            Assert.That(armorClass.ArmorBonus, Is.EqualTo(42));
            Assert.That(armorClass.ArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = armorClass.ArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(42));
            Assert.That(bonus.IsConditional, Is.False);

            Assert.That(armorClass.ShieldBonus, Is.EqualTo(96));
            Assert.That(armorClass.ShieldBonuses.Count(), Is.EqualTo(1));

            bonus = armorClass.ShieldBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(96));
            Assert.That(bonus.IsConditional, Is.False);
        }

        //example is Two-Weapon Defense
        [Test]
        public void ApplyShieldBonusFromFeat()
        {
            var feat = new Feat();
            feat.Name = FeatConstants.TwoWeaponDefense;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            var armorClass = GenerateAndAssertArmorClass(11, 11, 10);
            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
            Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(1));
            Assert.That(armorClass.SizeModifier, Is.Zero);
            Assert.That(armorClass.Dexterity.Modifier, Is.Zero);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.BaseAbilityTestNumbers))]
        public void UnearthlyGraceAddsCharismaBonusAsDeflectionBonus(int abilityScore)
        {
            abilities[AbilityConstants.Charisma].BaseScore = abilityScore;

            var feat = new Feat();
            feat.Name = FeatConstants.SpecialQualities.UnearthlyGrace;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            racialBonuses["other subtype"] = new List<BonusSelection>();
            racialBonuses[CreatureConstants.Types.Subtypes.Incorporeal] = new List<BonusSelection>();

            var armorClass = GenerateAndAssertArmorClass(ArmorClass.BaseArmorClass + abilities[AbilityConstants.Charisma].Modifier, ArmorClass.BaseArmorClass + abilities[AbilityConstants.Charisma].Modifier, ArmorClass.BaseArmorClass + abilities[AbilityConstants.Charisma].Modifier);
            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.SizeModifier, Is.Zero);
            Assert.That(armorClass.Dexterity.Modifier, Is.Zero);
        }
    }
}