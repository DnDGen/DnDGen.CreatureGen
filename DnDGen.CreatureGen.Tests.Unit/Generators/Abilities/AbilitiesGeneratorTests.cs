using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private IAbilitiesGenerator abilitiesGenerator;
        private List<TypeAndAmountSelection> abilitySelections;
        private Mock<PartialRoll> mockPartialTotal;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            abilitiesGenerator = new AbilitiesGenerator(mockTypeAndAmountSelector.Object, mockDice.Object);

            abilitySelections = new List<TypeAndAmountSelection>();
            mockPartialTotal = new Mock<PartialRoll>();

            abilitySelections.Add(new TypeAndAmountSelection { Type = "ability", Amount = 0 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "other ability", Amount = 9266 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "last ability", Amount = -90210 });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature name")).Returns(abilitySelections);
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All)).Returns(abilitySelections);

            var mockPartialDie = new Mock<PartialRoll>();
            mockDice.Setup(d => d.Roll(3)).Returns(mockPartialDie.Object);
            mockPartialDie.Setup(d => d.d(6)).Returns(mockPartialTotal.Object);

            mockPartialTotal.SetupSequence(d => d.AsSum<int>()).Returns(42).Returns(600).Returns(1337);
        }

        [Test]
        public void GetAbilitiesFromSelections()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name");
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
        }

        [Test]
        public void RollBaseScoresForAbilities()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name");
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void MissingAbilitiesHaveNoScore()
        {
            var allAbilities = new[]
            {
                new TypeAndAmountSelection { Type = "ability" },
                new TypeAndAmountSelection { Type = "other ability" },
                new TypeAndAmountSelection { Type = "last ability" }
            };

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All)).Returns(allAbilities);

            abilitySelections.RemoveAt(1);

            var abilities = abilitiesGenerator.GenerateFor("creature name");
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(0));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].FullScore, Is.Zero);
            Assert.That(abilities["other ability"].HasScore, Is.False);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(600)); //INFO: 600 instead of 1337 because we never rolled for the ability without a score
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void ApplyMaxModifier_NoEquipment()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = null;

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorOnly()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = new Armor
            {
                MaxDexterityBonus = 9266,
            };
            equipment.Shield = null;

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ShieldOnly_NoMax()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = new Armor();

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ShieldOnly_WithMax()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = new Armor
            {
                MaxDexterityBonus = 9266,
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_NoShieldMax()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = new Armor
            {
                MaxDexterityBonus = 9266
            };
            equipment.Shield = new Armor();

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_WithShieldMax_Higher()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = new Armor
            {
                MaxDexterityBonus = 9266
            };
            equipment.Shield = new Armor
            {
                MaxDexterityBonus = 90210
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_WithShieldMax_Lower()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var equipment = new Equipment();
            equipment.Armor = new Armor
            {
                MaxDexterityBonus = 9266
            };
            equipment.Shield = new Armor
            {
                MaxDexterityBonus = 42
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(42));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }
    }
}