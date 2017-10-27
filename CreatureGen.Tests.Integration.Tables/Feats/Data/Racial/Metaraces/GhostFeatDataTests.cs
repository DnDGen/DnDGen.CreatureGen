using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class GhostFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.Metaraces.Ghost);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.GhostSpecialAttack,
                FeatConstants.CorruptingGaze,
                FeatConstants.CorruptingTouch,
                FeatConstants.DrainingTouch,
                FeatConstants.FrightfulMoan,
                FeatConstants.HorrificAppearance,
                FeatConstants.Malevolence,
                FeatConstants.Telekinesis,
                FeatConstants.Manifestation,
                FeatConstants.Rejuvenation,
                FeatConstants.TurnResistance,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.CorruptingGaze,
            FeatConstants.CorruptingGaze,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.CorruptingTouch,
            FeatConstants.CorruptingTouch,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.DrainingTouch,
            FeatConstants.DrainingTouch,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.FrightfulMoan,
            FeatConstants.FrightfulMoan,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.HorrificAppearance,
            FeatConstants.HorrificAppearance,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Malevolence,
            FeatConstants.Malevolence,
            "",
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Telekinesis,
            FeatConstants.Telekinesis,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Manifestation,
            FeatConstants.Manifestation,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Rejuvenation,
            FeatConstants.Rejuvenation,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.TurnResistance,
            FeatConstants.TurnResistance,
            "",
            0,
            "",
            0,
            "",
            4, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }

        [Test]
        public void GhostSpecialAttackData()
        {
            RacialFeatData(FeatConstants.GhostSpecialAttack, FeatConstants.GhostSpecialAttack, FeatConstants.GhostSpecialAttack, 0, string.Empty, 0, string.Empty, 0, 0, "1d3", 0);
        }

        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingGaze)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.CorruptingTouch)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.DrainingTouch)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.FrightfulMoan)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.HorrificAppearance)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.Malevolence)]
        [TestCase(FeatConstants.GhostSpecialAttack, FeatConstants.Telekinesis)]
        public void FeatGroupOrderDependency(string prerequisiteFeat, string dependentFeat)
        {
            var collection = table.Keys.ToList();
            var prereqFeatIndex = collection.IndexOf(prerequisiteFeat);
            var dependencyIndex = collection.IndexOf(dependentFeat);

            Assert.That(prereqFeatIndex, Is.Not.Negative);
            Assert.That(dependencyIndex, Is.Not.Negative);
            Assert.That(prereqFeatIndex, Is.LessThan(dependencyIndex));
        }
    }
}
