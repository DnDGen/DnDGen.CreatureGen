using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
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
        private List<TypeAndAmountSelection> ageAbilitySelections;
        private Mock<PartialRoll> mockPartialTotal;
        private AbilityRandomizer randomizer;
        private Demographics demographics;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            abilitiesGenerator = new AbilitiesGenerator(mockTypeAndAmountSelector.Object, mockDice.Object);
            randomizer = new AbilityRandomizer();
            demographics = new Demographics();

            randomizer.Roll = "my roll";
            demographics.Age.Description = "my age category";

            abilitySelections = new List<TypeAndAmountSelection>();
            abilitySelections.Add(new TypeAndAmountSelection { Type = "ability", Amount = 0 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "other ability", Amount = 9266 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "last ability", Amount = -90210 });

            ageAbilitySelections = new List<TypeAndAmountSelection>();
            ageAbilitySelections.Add(new TypeAndAmountSelection { Type = "ability", Amount = 0 });
            ageAbilitySelections.Add(new TypeAndAmountSelection { Type = "other ability", Amount = 0 });
            ageAbilitySelections.Add(new TypeAndAmountSelection { Type = "last ability", Amount = 0 });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature name")).Returns(abilitySelections);
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All)).Returns(abilitySelections);
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my age category")).Returns(ageAbilitySelections);

            mockPartialTotal = new Mock<PartialRoll>();
            mockDice.Setup(d => d.Roll("my roll")).Returns(mockPartialTotal.Object);

            mockPartialTotal.SetupSequence(d => d.AsSum<int>()).Returns(42).Returns(600).Returns(1337);
        }

        [Test]
        public void GenerateFor_GetAbilitiesFromSelections()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_RollBaseScoresForAbilities()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
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
        public void GenerateFor_MissingAbilitiesHaveNoScore()
        {
            var allAbilities = new[]
            {
                new TypeAndAmountSelection { Type = "ability" },
                new TypeAndAmountSelection { Type = "other ability" },
                new TypeAndAmountSelection { Type = "last ability" }
            };

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All)).Returns(allAbilities);

            abilitySelections.RemoveAt(1);

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.Zero);
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
        public void GenerateFor_ApplyAbilityAdvancement()
        {
            randomizer.AbilityAdvancements["other ability"] = 1336;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_ApplyAbilityAdvancements()
        {
            randomizer.AbilityAdvancements["ability"] = 1336;
            randomizer.AbilityAdvancements["other ability"] = 96;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(96));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_ApplySetAbilityScore()
        {
            randomizer.SetRolls["other ability"] = 1336;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(1336));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(1336 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplySetAbilityScores()
        {
            randomizer.SetRolls["ability"] = 1336;
            randomizer.SetRolls["other ability"] = 96;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(96));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(96 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyPriorityAbility()
        {
            randomizer.PriorityAbility = "ability";

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(1337));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyPriorityAbility_PriorityIsHighest()
        {
            randomizer.PriorityAbility = "last ability";

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyAgeCategoryModifiers()
        {
            ageAbilitySelections[0].Amount = -1;
            ageAbilitySelections[1].Amount = -2;
            ageAbilitySelections[2].Amount = -3;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
        }

        [Test]
        public void GenerateFor_ApplyAllModifiers()
        {
            randomizer.AbilityAdvancements["ability"] = 1336;
            randomizer.AbilityAdvancements["other ability"] = -96;
            randomizer.SetRolls["last ability"] = 783;
            randomizer.SetRolls["other ability"] = 8245;
            randomizer.PriorityAbility = "last ability";

            ageAbilitySelections[0].Amount = -1;
            ageAbilitySelections[1].Amount = -2;
            ageAbilitySelections[2].Amount = -3;

            var abilities = abilitiesGenerator.GenerateFor("creature name", randomizer, demographics);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42 + 1336 - 1));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(783));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(-96));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(783 + 9266 - 96 - 2));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(8245));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
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
            equipment.Shield = new Armor
            {
                MaxDexterityBonus = int.MaxValue
            };

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
            equipment.Shield = new Armor
            {
                MaxDexterityBonus = int.MaxValue
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