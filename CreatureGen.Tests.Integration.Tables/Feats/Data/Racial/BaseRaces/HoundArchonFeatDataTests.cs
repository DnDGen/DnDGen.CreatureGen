using CreatureGen.Combats;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class HoundArchonFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.HoundArchon); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.LowLightVision,
                FeatConstants.AuraOfMenace,
                FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
                FeatConstants.ImmuneToEffect + "Petrification",
                FeatConstants.SaveBonus,
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon + "Bite",
                FeatConstants.NaturalWeapon + "Slam",
                FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.Teleport_Greater,
                FeatConstants.SpellLikeAbility + SpellConstants.Tongues,
                FeatConstants.SpellLikeAbility + SpellConstants.Aid,
                FeatConstants.SpellLikeAbility + SpellConstants.ContinualFlame,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.Message,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.ChangeShape,
                FeatConstants.DamageReduction,
                FeatConstants.Scent,
                FeatConstants.SpellResistance,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            5,
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
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.AuraOfMenace,
            FeatConstants.AuraOfMenace,
            "Will DC 15 + CHA",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Petrification",
            FeatConstants.ImmuneToEffect,
            "Petrification",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            "Against poison",
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
            9,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.NaturalWeapon,
            "Bite (1d8)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Slam",
            FeatConstants.NaturalWeapon,
            "Slam (1d4)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.MagicCircleAgainstAlignment,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Teleport_Greater,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Teleport_Greater,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Tongues,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Tongues,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Aid,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Aid,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ContinualFlame,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ContinualFlame,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectAlignment,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Message,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Message,
            0,
            FeatConstants.Frequencies.AtWill,
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
            "Must be evil to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.Scent,
            FeatConstants.Scent,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class levels to power",
            0,
            "",
            0,
            "",
            16,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int strength, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
