using CreatureGen.Combats;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class SahuaginFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Sahuagin); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.SkillBonus + SkillConstants.Swim,
                FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
                FeatConstants.Run + SkillConstants.Swim,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Survival,
                FeatConstants.SkillBonus + SkillConstants.Profession + SkillConstants.Foci.Profession.Hunter,
                FeatConstants.SkillBonus + SkillConstants.HandleAnimal,
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon + "Talon",
                FeatConstants.NaturalWeapon + "Bite",
                FeatConstants.Blindsense,
                FeatConstants.BloodFrenzy,
                FeatConstants.Rake,
                FeatConstants.FreshwaterSensitivity,
                FeatConstants.LightBlindness,
                FeatConstants.SpeakWithSharks,
                FeatConstants.WaterDependent,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            3,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            3,
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide + " (when underwater)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen + " (when underwater)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot + " (when underwater)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Survival,
            FeatConstants.SkillBonus,
            SkillConstants.Survival + " (within 50 miles of its home)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Profession + SkillConstants.Foci.Profession.Hunter,
            FeatConstants.SkillBonus,
            SkillConstants.Profession + "/" + SkillConstants.Foci.Profession.Hunter + " (within 50 miles of its home)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.HandleAnimal,
            FeatConstants.SkillBonus,
            SkillConstants.HandleAnimal + " (when working with sharks)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Talon",
            FeatConstants.NaturalWeapon,
            "Talon (1d4) x2",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.NaturalWeapon,
            "Bite (1d4-1)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Blindsense,
            FeatConstants.Blindsense,
            "",
            0,
            "",
            0,
            "",
            30,
            0, 0)]
        [TestCase(FeatConstants.BloodFrenzy,
            FeatConstants.BloodFrenzy,
            "",
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Rake,
            FeatConstants.Rake,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.FreshwaterSensitivity,
            FeatConstants.FreshwaterSensitivity,
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
        [TestCase(FeatConstants.SpeakWithSharks,
            FeatConstants.SpeakWithSharks,
            "",
            0,
            "",
            0,
            "",
            150,
            0, 0)]
        [TestCase(FeatConstants.WaterDependent,
            FeatConstants.WaterDependent,
            "",
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
