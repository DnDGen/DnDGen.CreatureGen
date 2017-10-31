using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.AddHitDiceToPower, "Add Monster Hit Dice to Power")]
        [TestCase(GroupConstants.All, "All")]
        [TestCase(GroupConstants.Aquatic, "Aquatic")]
        [TestCase(GroupConstants.ArmorBonus, "Armor Bonus")]
        [TestCase(GroupConstants.ArmorCheckPenalty, "Armor Check Penalty")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.CharacterClasses, "Character Classes")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.DodgeBonus, "Dodge Bonus")]
        [TestCase(GroupConstants.FighterBonusFeats, "Fighter Bonus Feats")]
        [TestCase(GroupConstants.FavoredEnemies, "Favored Enemies")]
        [TestCase(GroupConstants.Genetic, "Genetic")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.HasAbilityRequirements, "Has Ability Requirements")]
        [TestCase(GroupConstants.HasClassRequirements, "Has Class Requirements")]
        [TestCase(GroupConstants.HasSkillRequirements, "Has Skill Requirements")]
        [TestCase(GroupConstants.HasWings, "Has Wings")]
        [TestCase(GroupConstants.Initiative, "Initiative")]
        [TestCase(GroupConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(GroupConstants.ManualCrossbows, "Manual Crossbows")]
        [TestCase(GroupConstants.Monsters, "Monsters")]
        [TestCase(GroupConstants.NaturalArmor, "Natural Armor")]
        [TestCase(GroupConstants.NeedsAmmunition, "Needs Ammunition")]
        [TestCase(GroupConstants.NPCs, "NPCs")]
        [TestCase(GroupConstants.PhysicalCombat, "Physical Combat")]
        [TestCase(GroupConstants.Players, "Players")]
        [TestCase(GroupConstants.PoorBaseAttack, "Poor Base Attack")]
        [TestCase(GroupConstants.PreparesSpells, "Prepares Spells")]
        [TestCase(GroupConstants.Proficiency, "Proficiency")]
        [TestCase(GroupConstants.SavingThrows, "Saving Throws")]
        [TestCase(GroupConstants.SchoolsOfMagic, "Schools of Magic")]
        [TestCase(GroupConstants.Size, "Size")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.Spellcasters, "Spellcasters")]
        [TestCase(GroupConstants.Standard, "Standard")]
        [TestCase(GroupConstants.Stealth, "Stealth")]
        [TestCase(GroupConstants.TakenMultipleTimes, "Taken Multiple Times")]
        [TestCase(GroupConstants.TwoHanded, "Two-Handed")]
        [TestCase(GroupConstants.Undead, "Undead")]
        [TestCase(GroupConstants.Untrained, "Untrained")]
        [TestCase(GroupConstants.WizardBonusFeats, "Wizard Bonus Feats")]
        public void GroupConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
