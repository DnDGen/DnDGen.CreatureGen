using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats
{
    [TestFixture]
    public class FeatGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.FeatGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.AttackBonus,
                FeatConstants.SkillBonus,
                GroupConstants.AddHitDiceToPower,
                GroupConstants.HasAbilityRequirements,
                GroupConstants.HasSkillRequirements,
                GroupConstants.Initiative,
                GroupConstants.SavingThrows,
                GroupConstants.TakenMultipleTimes,
                SaveConstants.Fortitude,
                SaveConstants.Reflex,
                SaveConstants.Will,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AttackBonus,
            FeatConstants.AttackBonus)]
        [TestCase(GroupConstants.AddHitDiceToPower,
            FeatConstants.SpellResistance)]
        [TestCase(GroupConstants.HasSkillRequirements,
            FeatConstants.MountedArchery,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack,
            FeatConstants.SpiritedCharge,
            FeatConstants.Trample,
            FeatConstants.Acrobatic,
            FeatConstants.Agile,
            FeatConstants.Alertness,
            FeatConstants.AnimalAffinity,
            FeatConstants.Athletic,
            FeatConstants.Deceitful,
            FeatConstants.DeftHands,
            FeatConstants.Diligent,
            FeatConstants.Investigator,
            FeatConstants.MagicalAptitude,
            FeatConstants.NatureSense,
            FeatConstants.Negotiator,
            FeatConstants.NimbleFingers,
            FeatConstants.Persuasive,
            FeatConstants.SelfSufficient,
            FeatConstants.Stealthy)]
        [TestCase(GroupConstants.HasAbilityRequirements,
            FeatConstants.PowerAttack,
            FeatConstants.CombatExpertise,
            FeatConstants.DeflectArrows,
            FeatConstants.Dodge,
            FeatConstants.GreaterTwoWeaponFighting,
            FeatConstants.ImprovedGrapple,
            FeatConstants.ImprovedPreciseShot,
            FeatConstants.ImprovedTwoWeaponFighting,
            FeatConstants.Manyshot,
            FeatConstants.Mobility,
            FeatConstants.NaturalSpell,
            FeatConstants.RapidShot,
            FeatConstants.SnatchArrows,
            FeatConstants.SpellMastery,
            FeatConstants.SpringAttack,
            FeatConstants.StunningFist,
            FeatConstants.ShotOnTheRun,
            FeatConstants.TwoWeaponDefense,
            FeatConstants.TwoWeaponFighting,
            FeatConstants.WhirlwindAttack)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            FeatConstants.AttackBonus,
            FeatConstants.SpellMastery,
            FeatConstants.Toughness,
            FeatConstants.SkillMastery,
            FeatConstants.SkillBonus,
            FeatConstants.DodgeBonus,
            FeatConstants.SaveBonus,
            FeatConstants.ExtraTurning)]
        [TestCase(FeatConstants.SkillBonus,
            FeatConstants.SkillBonus,
            FeatConstants.Acrobatic,
            FeatConstants.Agile,
            FeatConstants.Alertness,
            FeatConstants.AnimalAffinity,
            FeatConstants.Athletic,
            FeatConstants.Deceitful,
            FeatConstants.DeftHands,
            FeatConstants.Diligent,
            FeatConstants.Investigator,
            FeatConstants.MagicalAptitude,
            FeatConstants.Negotiator,
            FeatConstants.NimbleFingers,
            FeatConstants.Persuasive,
            FeatConstants.SelfSufficient,
            FeatConstants.SkillFocus,
            FeatConstants.Stealthy,
            FeatConstants.NatureSense)]
        [TestCase(SaveConstants.Fortitude,
            FeatConstants.GreatFortitude)]
        [TestCase(SaveConstants.Reflex,
            FeatConstants.LightningReflexes)]
        [TestCase(SaveConstants.Will,
            FeatConstants.IronWill)]
        [TestCase(GroupConstants.SavingThrows,
            FeatConstants.SaveBonus)]
        [TestCase(GroupConstants.Initiative,
            FeatConstants.ImprovedInitiative)]
        public void FeatGroup(string name, params string[] collection)
        {
            DistinctCollection(name, collection);
        }
    }
}