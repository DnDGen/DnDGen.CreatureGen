using CreatureGen.Combats;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class GraySlaadFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.GraySlaad); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
                FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectMagic,
                FeatConstants.SpellLikeAbility + SpellConstants.Identify,
                FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
                FeatConstants.SpellLikeAbility + SpellConstants.LightningBolt,
                FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.SeeInvisibility,
                FeatConstants.SpellLikeAbility + SpellConstants.Shatter,
                FeatConstants.SpellLikeAbility + SpellConstants.AnimateObjects,
                FeatConstants.SpellLikeAbility + SpellConstants.DispelAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.Fly,
                FeatConstants.SpellLikeAbility + SpellConstants.PowerWordStun,
                FeatConstants.ChangeShape,
                FeatConstants.DamageReduction,
                FeatConstants.SummonSlaad,
                FeatConstants.FastHealing,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.Resistance + FeatConstants.Foci.Fire,
                FeatConstants.SkillBonus + SkillConstants.Survival,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            7,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            7,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            7,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            11,
            0, 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ChaosHammer,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DeeperDarkness,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectMagic,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectMagic,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Identify,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Identify,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Invisibility,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.LightningBolt,
            FeatConstants.SpellLikeAbility,
            SpellConstants.LightningBolt,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.MagicCircleAgainstAlignment,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SeeInvisibility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SeeInvisibility,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Shatter,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Shatter,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.AnimateObjects,
            FeatConstants.SpellLikeAbility,
            SpellConstants.AnimateObjects,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DispelAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DispelAlignment,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Fly,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Fly,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PowerWordStun,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PowerWordStun,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ChangeShape,
            FeatConstants.ChangeShape,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be lawful to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SummonSlaad,
            FeatConstants.SummonSlaad,
            "",
            2,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.FastHealing,
            FeatConstants.FastHealing,
            "",
            1,
            FeatConstants.Frequencies.Turn,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Sonic,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance,
            FeatConstants.Foci.Acid,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Fire,
            FeatConstants.Resistance,
            FeatConstants.Foci.Fire,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Survival,
            FeatConstants.SkillBonus,
            SkillConstants.Survival + " (when tracking)",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
