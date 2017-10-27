using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
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
    public class ForestGnomeFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.ForestGnome); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LowLightVision,
                FeatConstants.WeaponFamiliarity,
                FeatConstants.SaveBonus,
                FeatConstants.ImprovedSpell,
                FeatConstants.AttackBonus + SizeConstants.BaseRaces.Goblin,
                FeatConstants.AttackBonus + SizeConstants.BaseRaces.Kobold,
                FeatConstants.AttackBonus + SizeConstants.BaseRaces.Orc,
                FeatConstants.AttackBonus + SizeConstants.BaseRaces.Lizardfolk,
                FeatConstants.DodgeBonus,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
                FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
                FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
                FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
                FeatConstants.SpellLikeAbility + SpellConstants.PassWithoutTrace,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Hide + "Woods"
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
            CharacterClassConstants.Schools.Illusion,
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
        [TestCase(FeatConstants.AttackBonus + SizeConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + SizeConstants.BaseRaces.Kobold,
            FeatConstants.AttackBonus,
            SizeConstants.BaseRaces.Kobold,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + SizeConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus,
            SizeConstants.BaseRaces.Orc,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + SizeConstants.BaseRaces.Lizardfolk,
            FeatConstants.AttackBonus,
            "Reptilian humanoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "Giant",
            0,
            "",
            0,
            "",
            4,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DancingLights,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility,
            SpellConstants.GhostSound,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SpeakWithAnimals + " (Forest animals)",
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Prestidigitation,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PassWithoutTrace,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PassWithoutTrace,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide + "Woods",
            FeatConstants.SkillBonus,
            SkillConstants.Hide + " (in wooded areas)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
