using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.AddHitDiceToPower, "Add Hit Dice to Power")]
        [TestCase(GroupConstants.All, "All")]
        [TestCase(GroupConstants.Aquatic, "Aquatic")]
        [TestCase(GroupConstants.ArmorBonus, "Armor Bonus")]
        [TestCase(GroupConstants.ArmorCheckPenalty, "Armor Check Penalty")]
        [TestCase(GroupConstants.AttackBonus, "Attack Bonus")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.CharacterClasses, "Character Classes")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.DodgeBonus, "Dodge Bonus")]
        [TestCase(GroupConstants.Genetic, "Genetic")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.Initiative, "Initiative")]
        [TestCase(GroupConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(GroupConstants.NaturalArmor, "Natural Armor")]
        [TestCase(GroupConstants.PoorBaseAttack, "Poor Base Attack")]
        [TestCase(GroupConstants.SavingThrows, "Saving Throws")]
        [TestCase(GroupConstants.Size, "Size")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.SkillBonus, "Skill Bonus")]
        [TestCase(GroupConstants.TakenMultipleTimes, "Taken Multiple Times")]
        [TestCase(GroupConstants.Undead, "Undead")]
        [TestCase(GroupConstants.Untrained, "Untrained")]
        [TestCase(GroupConstants.WeaponProficiency, "Weapon Proficiency")]
        public void GroupConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
