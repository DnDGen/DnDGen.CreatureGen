using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.All, "All")]
        [TestCase(GroupConstants.ArmorBonus, "Armor Bonus")]
        [TestCase(GroupConstants.ArmorCheckPenalty, "Armor Check Penalty")]
        [TestCase(GroupConstants.ArmorProficiency, "Armor Proficiency")]
        [TestCase(GroupConstants.AttackBonus, "Attack Bonus")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.DodgeBonus, "Dodge Bonus")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.Initiative, "Initiative")]
        [TestCase(GroupConstants.ManualCrossbows, "Manual Crossbows")]
        [TestCase(GroupConstants.NaturalArmor, "Natural Armor")]
        [TestCase(GroupConstants.PoorBaseAttack, "Poor Base Attack")]
        [TestCase(GroupConstants.PreparesSpells, "Prepares Spells")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.SkillSynergy, "Skill Synergy")]
        [TestCase(GroupConstants.TakenMultipleTimes, "Taken Multiple Times")]
        [TestCase(GroupConstants.Unnatural, "Unnatural")]
        [TestCase(GroupConstants.Untrained, "Untrained")]
        [TestCase(GroupConstants.WeaponProficiency, "Weapon Proficiency")]
        public void GroupConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
