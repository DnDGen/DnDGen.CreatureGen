using CreatureGen.Combats;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class BlueSlaadFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Slaad_Blue); }
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
                FeatConstants.SpellLikeAbility + SpellConstants.HoldPerson,
                FeatConstants.SpellLikeAbility + SpellConstants.Passwall,
                FeatConstants.SpellLikeAbility + SpellConstants.Telekinesis,
                FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
                FeatConstants.SlaadFever,
                FeatConstants.SummonSlaad,
                FeatConstants.FastHealing,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.Resistance + FeatConstants.Foci.Fire,
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
            6,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            9,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.HoldPerson,
            FeatConstants.SpellLikeAbility,
            SpellConstants.HoldPerson,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Passwall,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Passwall,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Telekinesis,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Telekinesis,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ChaosHammer,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SlaadFever,
            FeatConstants.SlaadFever,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SummonSlaad,
            FeatConstants.SummonSlaad,
            "",
            1,
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
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
