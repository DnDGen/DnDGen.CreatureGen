using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AttackDataSelectionTests
    {
        private AttackDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new AttackDataSelection();
        }

        [Test]
        public void AttackSelectionIsInitialized()
        {
            Assert.That(selection.Damages, Is.Empty);
            Assert.That(selection.IsMelee, Is.False);
            Assert.That(selection.IsNatural, Is.False);
            Assert.That(selection.IsPrimary, Is.False);
            Assert.That(selection.IsSpecial, Is.False);
            Assert.That(selection.Name, Is.Empty);
        }

        [Test]
        public void SectionCountIs14()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(14));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.NameIndex] = "my attack";
            data[DataIndexConstants.AttackData.DamageEffectIndex] = "my damage effect";
            data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = "926.6";
            data[DataIndexConstants.AttackData.IsMeleeIndex] = bool.TrueString;
            data[DataIndexConstants.AttackData.IsNaturalIndex] = bool.FalseString;
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = bool.TrueString;
            data[DataIndexConstants.AttackData.IsSpecialIndex] = bool.FalseString;
            data[DataIndexConstants.AttackData.FrequencyQuantityIndex] = "90210";
            data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = "my time period";
            data[DataIndexConstants.AttackData.SaveIndex] = "my save";
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = "my save ability";
            data[DataIndexConstants.AttackData.AttackTypeIndex] = "my attack type";
            data[DataIndexConstants.AttackData.SaveDcBonusIndex] = "42";
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = "my required gender";

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Name, Is.EqualTo("my attack"));
            Assert.That(newSelection.DamageEffect, Is.EqualTo("my damage effect"));
            Assert.That(newSelection.DamageBonusMultiplier, Is.EqualTo(926.6));
            Assert.That(newSelection.IsMelee, Is.EqualTo(true));
            Assert.That(newSelection.IsNatural, Is.EqualTo(false));
            Assert.That(newSelection.IsPrimary, Is.EqualTo(true));
            Assert.That(newSelection.IsSpecial, Is.EqualTo(false));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my time period"));
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.AttackType, Is.EqualTo("my attack type"));
            Assert.That(newSelection.SaveDcBonus, Is.EqualTo(42));
            Assert.That(newSelection.RequiredGender, Is.EqualTo("my required gender"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_Melee(bool melee)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsMeleeIndex] = melee.ToString();

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsMelee, Is.EqualTo(melee));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_Natural(bool natural)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsNaturalIndex] = natural.ToString();

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsNatural, Is.EqualTo(natural));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_Primary(bool primary)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = primary.ToString();

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsPrimary, Is.EqualTo(primary));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_Special(bool special)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsSpecialIndex] = special.ToString();

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsSpecial, Is.EqualTo(special));
        }

        [TestCase("my save", "my save")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromString_ReturnsSelection_Save(string save, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.SaveIndex] = save;

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.EqualTo(expected));
        }

        [TestCase("my save ability", "my save ability")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromString_ReturnsSelection_SaveAbility(string saveAbility, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = saveAbility;

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.SaveAbility, Is.EqualTo(expected));
        }

        [TestCase("my required gender", "my required gender")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromString_ReturnsSelection_RequiredGender(string requiredGender, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = requiredGender;

            var newSelection = AttackDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredGender, Is.EqualTo(expected));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new AttackDataSelection
            {
                Name = "my attack",
                DamageEffect = "my damage effect",
                DamageBonusMultiplier = 926.6,
                IsMelee = true,
                IsNatural = false,
                IsPrimary = true,
                IsSpecial = false,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
                Save = "my save",
                SaveAbility = "my save ability",
                AttackType = "my attack type",
                SaveDcBonus = 42,
                RequiredGender = "my required gender",
            };

            var rawData = AttackDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("my attack"));
            Assert.That(rawData[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("my damage effect"));
            Assert.That(rawData[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("my time period"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("my attack type"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AttackData.RequiredGenderIndex], Is.EqualTo("my required gender"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_Melee(bool melee)
        {
            selection.IsMelee = melee;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(melee.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_Natural(bool natural)
        {
            selection.IsNatural = natural;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(natural.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_Primary(bool primary)
        {
            selection.IsPrimary = primary;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(primary.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_Special(bool special)
        {
            selection.IsSpecial = special;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(special.ToString()));
        }

        [TestCase("my save", "my save")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromSelection_ReturnsString_Save(string save, string expected)
        {
            selection.Save = save;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.EqualTo(expected));
        }

        [TestCase("my save ability", "my save ability")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromSelection_ReturnsString_SaveAbility(string saveAbility, string expected)
        {
            selection.SaveAbility = saveAbility;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.EqualTo(expected));
        }

        [TestCase("my required gender", "my required gender")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Map_FromSelection_ReturnsString_RequiredGender(string requiredGender, string expected)
        {
            selection.RequiredGender = requiredGender;

            var data = AttackDataSelection.Map(selection);
            Assert.That(data[DataIndexConstants.AttackData.RequiredGenderIndex], Is.EqualTo(expected));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.NameIndex] = "my attack";
            data[DataIndexConstants.AttackData.DamageEffectIndex] = "my damage effect";
            data[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = "926.6";
            data[DataIndexConstants.AttackData.IsMeleeIndex] = bool.TrueString;
            data[DataIndexConstants.AttackData.IsNaturalIndex] = bool.FalseString;
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = bool.TrueString;
            data[DataIndexConstants.AttackData.IsSpecialIndex] = bool.FalseString;
            data[DataIndexConstants.AttackData.FrequencyQuantityIndex] = "90210";
            data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = "my time period";
            data[DataIndexConstants.AttackData.SaveIndex] = "my save";
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = "my save ability";
            data[DataIndexConstants.AttackData.AttackTypeIndex] = "my attack type";
            data[DataIndexConstants.AttackData.SaveDcBonusIndex] = "42";
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = "my required gender";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Name, Is.EqualTo("my attack"));
            Assert.That(newSelection.DamageEffect, Is.EqualTo("my damage effect"));
            Assert.That(newSelection.DamageBonusMultiplier, Is.EqualTo(926.6));
            Assert.That(newSelection.IsMelee, Is.EqualTo(true));
            Assert.That(newSelection.IsNatural, Is.EqualTo(false));
            Assert.That(newSelection.IsPrimary, Is.EqualTo(true));
            Assert.That(newSelection.IsSpecial, Is.EqualTo(false));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my time period"));
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.AttackType, Is.EqualTo("my attack type"));
            Assert.That(newSelection.SaveDcBonus, Is.EqualTo(42));
            Assert.That(newSelection.RequiredGender, Is.EqualTo("my required gender"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_Melee(bool melee)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsMeleeIndex] = melee.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsMelee, Is.EqualTo(melee));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_Natural(bool natural)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsNaturalIndex] = natural.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsNatural, Is.EqualTo(natural));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_Primary(bool primary)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = primary.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsPrimary, Is.EqualTo(primary));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_Special(bool special)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.IsSpecialIndex] = special.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.IsSpecial, Is.EqualTo(special));
        }

        [TestCase("my save", "my save")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapTo_ReturnsSelection_Save(string save, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.SaveIndex] = save;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.EqualTo(expected));
        }

        [TestCase("my save ability", "my save ability")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapTo_ReturnsSelection_SaveAbility(string saveAbility, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.SaveAbilityIndex] = saveAbility;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.SaveAbility, Is.EqualTo(expected));
        }

        [TestCase("my required gender", "my required gender")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapTo_ReturnsSelection_RequiredGender(string requiredGender, string expected)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.RequiredGenderIndex] = requiredGender;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredGender, Is.EqualTo(expected));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new AttackDataSelection
            {
                Name = "my attack",
                DamageEffect = "my damage effect",
                DamageBonusMultiplier = 926.6,
                IsMelee = true,
                IsNatural = false,
                IsPrimary = true,
                IsSpecial = false,
                FrequencyQuantity = 90210,
                FrequencyTimePeriod = "my time period",
                Save = "my save",
                SaveAbility = "my save ability",
                AttackType = "my attack type",
                SaveDcBonus = 42,
                RequiredGender = "my required gender",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("my attack"));
            Assert.That(rawData[DataIndexConstants.AttackData.DamageEffectIndex], Is.EqualTo("my damage effect"));
            Assert.That(rawData[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo("my time period"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("my attack type"));
            Assert.That(rawData[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AttackData.RequiredGenderIndex], Is.EqualTo("my required gender"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_Melee(bool melee)
        {
            selection.IsMelee = melee;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(melee.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_Natural(bool natural)
        {
            selection.IsNatural = natural;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(natural.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_Primary(bool primary)
        {
            selection.IsPrimary = primary;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(primary.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_Special(bool special)
        {
            selection.IsSpecial = special;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(special.ToString()));
        }

        [TestCase("my save", "my save")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapFrom_ReturnsString_Save(string save, string expected)
        {
            selection.Save = save;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.SaveIndex], Is.EqualTo(expected));
        }

        [TestCase("my save ability", "my save ability")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapFrom_ReturnsString_SaveAbility(string saveAbility, string expected)
        {
            selection.SaveAbility = saveAbility;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.SaveAbilityIndex], Is.EqualTo(expected));
        }

        [TestCase("my required gender", "my required gender")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void MapFrom_ReturnsString_RequiredGender(string requiredGender, string expected)
        {
            selection.RequiredGender = requiredGender;

            var data = selection.MapFrom(selection);
            Assert.That(data[DataIndexConstants.AttackData.RequiredGenderIndex], Is.EqualTo(expected));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RequirementsMet_ReturnsTrue_WhenNoRequiredGender(string requiredGender)
        {
            selection.RequiredGender = requiredGender;
            var met = selection.RequirementsMet("whatever");
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMet_ReturnsTrue_WhenRequiredGenderMatches()
        {
            selection.RequiredGender = "my required gender";
            var met = selection.RequirementsMet("my required gender");
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMet_ReturnsFalse_WhenRequiredGenderDoesNotMatch()
        {
            selection.RequiredGender = "my required gender";
            var met = selection.RequirementsMet("wrong gender");
            Assert.That(met, Is.False);
        }

        [TestCase(true, "")]
        [TestCase(true, "my effect")]
        [TestCase(false, "")]
        [TestCase(false, "my effect")]
        public void BuildDamageKey_FromData(bool primary, string effect)
        {
            selection.Name = "My Attack";
            selection.IsPrimary = primary;
            selection.DamageEffect = effect;

            var key = selection.BuildDamageKey("creature", "my size");
            Assert.That(key, Is.EqualTo($"creaturemy sizeMy Attack{primary}{effect}"));
        }
    }
}
