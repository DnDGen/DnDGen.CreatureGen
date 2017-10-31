using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class KuoToaFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.KuoToa); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.NaturalArmor,
                FeatConstants.LightningBolt,
                FeatConstants.Adhesive,
                FeatConstants.Amphibious,
                FeatConstants.KeenSight,
                FeatConstants.LightBlindness,
                FeatConstants.Slippery,
                FeatConstants.Resistance,
                FeatConstants.SkillBonus + SkillConstants.EscapeArtist,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Swim,
                FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
                FeatConstants.Run + SkillConstants.Swim,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.Amphibious,
            FeatConstants.Amphibious,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Adhesive,
            FeatConstants.Adhesive,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.LightningBolt,
            FeatConstants.LightningBolt,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.KeenSight,
            FeatConstants.KeenSight,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Slippery,
            FeatConstants.Slippery,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.LightBlindness,
            FeatConstants.LightBlindness,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Resistance,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.EscapeArtist,
            FeatConstants.SkillBonus,
            SkillConstants.EscapeArtist,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim,
            FeatConstants.SkillBonus,
            SkillConstants.Swim,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
            FeatConstants.SkillBonus,
            SkillConstants.Swim + " (can always take 10)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Run + SkillConstants.Swim,
            FeatConstants.Run,
            "Can use while swimming in a straight line",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
