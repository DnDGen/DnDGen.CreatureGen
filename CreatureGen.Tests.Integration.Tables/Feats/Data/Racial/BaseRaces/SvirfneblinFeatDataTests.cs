using CreatureGen.CharacterClasses;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class SvirfneblinFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Svirfneblin); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LowLightVision,
                FeatConstants.Darkvision,
                FeatConstants.WeaponFamiliarity,
                FeatConstants.SaveBonus,
                FeatConstants.ImprovedSpell,
                FeatConstants.AttackBonus + CreatureConstants.Goblin,
                FeatConstants.AttackBonus + CreatureConstants.Kobold,
                FeatConstants.DodgeBonus,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SpellLikeAbility + SpellConstants.BlindnessDeafness,
                FeatConstants.SpellLikeAbility + SpellConstants.Blur,
                FeatConstants.SpellLikeAbility + SpellConstants.DisguiseSelf,
                FeatConstants.Stonecunning,
                FeatConstants.SpellResistance,
                FeatConstants.SpellLikeAbility + SpellConstants.Nondetection,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Hide + "Underground"
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            120,
            0, 0)]
        [TestCase(FeatConstants.WeaponFamiliarity,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.GnomeHookedHammer,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            FeatConstants.Foci.All,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.ImprovedSpell,
            FeatConstants.ImprovedSpell,
            CharacterClassConstants.Schools.Illusion,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + CreatureConstants.Goblin,
            FeatConstants.AttackBonus,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + CreatureConstants.Kobold,
            FeatConstants.AttackBonus,
            CreatureConstants.Kobold,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.Stonecunning,
            FeatConstants.Stonecunning,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "also increases by class levels",
            0,
            "",
            0,
            "",
            11,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide + "Underground",
            FeatConstants.SkillBonus,
            SkillConstants.Hide + " (Underground)",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.BlindnessDeafness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.BlindnessDeafness,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Blur,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Blur,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DisguiseSelf,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DisguiseSelf,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Nondetection,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Nondetection,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
