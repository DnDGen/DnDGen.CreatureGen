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
        public void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.AddHitDiceToPower,
                GroupConstants.AttackBonus,
                GroupConstants.Initiative,
                GroupConstants.SavingThrows,
                GroupConstants.SkillBonus,
                GroupConstants.TakenMultipleTimes,
                SaveConstants.Fortitude,
                SaveConstants.Reflex,
                SaveConstants.Will,
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.AttackBonus,
            FeatConstants.SpecialQualities.AttackBonus)]
        [TestCase(GroupConstants.AddHitDiceToPower,
            FeatConstants.SpecialQualities.SpellResistance)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            FeatConstants.SpecialQualities.AttackBonus,
            FeatConstants.SpellMastery,
            FeatConstants.Toughness,
            FeatConstants.SpecialQualities.SkillBonus,
            FeatConstants.SpecialQualities.DodgeBonus,
            FeatConstants.SpecialQualities.SaveBonus,
            FeatConstants.Turning_Extra)]
        [TestCase(GroupConstants.SkillBonus,
            FeatConstants.SpecialQualities.SkillBonus,
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
            //FeatConstants.NatureSense,
            FeatConstants.Negotiator,
            FeatConstants.NimbleFingers,
            FeatConstants.Persuasive,
            FeatConstants.SelfSufficient,
            FeatConstants.SkillFocus,
            FeatConstants.Stealthy)]
        [TestCase(SaveConstants.Fortitude,
            FeatConstants.GreatFortitude)]
        [TestCase(SaveConstants.Reflex,
            FeatConstants.LightningReflexes)]
        [TestCase(SaveConstants.Will,
            FeatConstants.IronWill)]
        [TestCase(GroupConstants.SavingThrows,
            FeatConstants.SpecialQualities.SaveBonus)]
        [TestCase(GroupConstants.Initiative,
            FeatConstants.Initiative_Improved)]
        public void FeatGroup(string name, params string[] collection)
        {
            DistinctCollection(name, collection);
        }
    }
}