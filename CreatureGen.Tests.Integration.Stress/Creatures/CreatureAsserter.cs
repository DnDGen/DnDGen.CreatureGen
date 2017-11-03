using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Stress.Creatures
{
    public class CreatureAsserter
    {
        private readonly IEnumerable<string> skillsWithFoci;

        public CreatureAsserter()
        {
            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCreature(Creature creature)
        {
            VerifySummary(creature);
            VerifyAlignment(creature);
            VerifyStatistics(creature);
            VerifyAbilities(creature);
            VerifySkills(creature);
            VerifyFeats(creature);
            VerifyCombat(creature);

            Assert.That(creature.ChallengeRating, Is.Not.Empty, creature.Summary);
        }

        private void VerifySummary(Creature creature)
        {
            Assert.That(creature.Name, Is.Not.Empty);
            Assert.That(creature.Template, Is.Not.Null);
            Assert.That(creature.Summary, Is.Not.Empty);
            Assert.That(creature.Summary, Contains.Substring(creature.Name));
            Assert.That(creature.Summary, Contains.Substring(creature.Template));
        }

        private void VerifyAlignment(Creature creature)
        {
            Assert.That(creature.Alignment, Is.Not.Null);

            if (!string.IsNullOrEmpty(creature.Alignment.Full))
            {
                Assert.That(creature.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Evil), creature.Summary);
                Assert.That(creature.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Chaotic), creature.Summary);
            }
        }

        private void VerifyStatistics(Creature creature)
        {
            Assert.That(creature.ChallengeRating, Is.Not.Negative, creature.Summary);
            Assert.That(creature.Size, Is.EqualTo(SizeConstants.Large)
                .Or.EqualTo(SizeConstants.Colossal)
                .Or.EqualTo(SizeConstants.Gargantuan)
                .Or.EqualTo(SizeConstants.Huge)
                .Or.EqualTo(SizeConstants.Tiny)
                .Or.EqualTo(SizeConstants.Medium)
                .Or.EqualTo(SizeConstants.Small), creature.Summary);

            VerifyLandSpeed(creature);
            VerifyAerialSpeed(creature);
            VerifySwimSpeed(creature);
        }

        private void VerifyLandSpeed(Creature creature)
        {
            Assert.That(creature.LandSpeed.Value, Is.Positive, creature.Summary);
            Assert.That(creature.LandSpeed.Value % 5, Is.EqualTo(0), creature.Summary);

            if (creature.LandSpeed.Value >= 10)
                Assert.That(creature.LandSpeed.Value % 10, Is.EqualTo(0), creature.Summary);

            Assert.That(creature.LandSpeed.Unit, Is.EqualTo("feet per round"), creature.Summary);
            Assert.That(creature.LandSpeed.Description, Is.Empty, creature.Summary);
        }

        private void VerifyAerialSpeed(Creature creature)
        {
            Assert.That(creature.AerialSpeed.Value, Is.Not.Negative, creature.Summary);
            Assert.That(creature.AerialSpeed.Value % 5, Is.EqualTo(0), creature.Summary);

            if (creature.AerialSpeed.Value >= 10)
                Assert.That(creature.AerialSpeed.Value % 10, Is.EqualTo(0), creature.Summary);

            Assert.That(creature.AerialSpeed.Unit, Is.EqualTo("feet per round"), creature.Summary);

            if (creature.AerialSpeed.Value == 0)
                Assert.That(creature.AerialSpeed.Description, Is.Empty, creature.Summary);
            else
                Assert.That(creature.AerialSpeed.Description, Is.Not.Empty, creature.Summary);
        }

        private void VerifySwimSpeed(Creature creature)
        {
            Assert.That(creature.SwimSpeed.Value, Is.Not.Negative, creature.Summary);
            Assert.That(creature.SwimSpeed.Value % 10, Is.EqualTo(0), creature.Summary);
            Assert.That(creature.SwimSpeed.Unit, Is.EqualTo("feet per round"), creature.Summary);
            Assert.That(creature.SwimSpeed.Description, Is.Empty, creature.Summary);
        }

        private void VerifyAbilities(Creature creature)
        {
            Assert.That(creature.Abilities.Count, Is.InRange(5, 6), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Charisma), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Dexterity), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Intelligence), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Strength), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Wisdom), creature.Summary);

            if (creature.Abilities.Count == 6)
                Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Constitution), creature.Summary);

            foreach (var statKVP in creature.Abilities)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), creature.Summary);
                Assert.That(stat.BaseValue, Is.AtLeast(3), creature.Summary);
            }
        }

        private void VerifySkills(Creature creature)
        {
            Assert.That(creature.Skills, Is.Not.Empty, creature.Summary);

            foreach (var skill in creature.Skills)
            {
                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, creature.Summary);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), creature.Summary);
                Assert.That(skill.RankCap, Is.Positive, creature.Summary);
                Assert.That(skill.Bonus, Is.Not.Negative);
                Assert.That(skill.BaseAbility, Is.Not.Null);
                Assert.That(creature.Abilities.Values, Contains.Item(skill.BaseAbility));
                Assert.That(skill.Focus, Is.Not.Null);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty);
                else
                    Assert.That(skill.Focus, Is.Empty);
            }

            var skillNamesAndFoci = creature.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique);
        }

        private void VerifyFeats(Creature creature)
        {
            Assert.That(creature.Feats, Is.Not.Null, creature.Summary);
            Assert.That(creature.SpecialQualities, Is.Not.Null, creature.Summary);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);

            foreach (var feat in allFeats)
            {
                Assert.That(feat.Name, Is.Not.Empty, creature.Summary);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Turn)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.Empty, feat.Name);

                if (feat.Name == FeatConstants.SaveBonus)
                    Assert.That(feat.Foci, Is.Not.Empty, creature.Summary);
            }
        }

        private void VerifyCombat(Creature creature)
        {
            Assert.That(creature.BaseAttackBonus, Is.Not.Negative, creature.Summary);

            Assert.That(creature.HitPoints.DefaultTotal, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.HitDie, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.DefaultRoll, Is.Not.Empty, creature.Summary);
            Assert.That(creature.HitPoints.DefaultRoll, Contains.Substring($"{creature.HitPoints.HitDiceQuantity}d{creature.HitPoints.HitDie}"), creature.Summary);
            Assert.That(creature.HitPoints.Total, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.Total, Is.AtLeast(creature.HitPoints.HitDiceQuantity), creature.Summary);
            Assert.That(creature.HitPoints.DefaultTotal, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.DefaultTotal, Is.AtLeast(creature.HitPoints.HitDiceQuantity), creature.Summary);

            Assert.That(creature.Attacks, Is.Not.Empty);
            Assert.That(creature.MeleeAttack, Is.Not.Null);
            Assert.That(creature.MeleeAttack.IsMelee, Is.True);
            Assert.That(creature.MeleeAttack.IsSpecial, Is.False);
            Assert.That(creature.FullMeleeAttack, Is.Not.Empty);
            Assert.That(creature.FullMeleeAttack.All(a => a.IsMelee && !a.IsSpecial), Is.True);
            Assert.That(creature.FullRangedAttack, Is.Not.Null);

            if (creature.FullRangedAttack.Any())
            {
                Assert.That(creature.RangedAttack, Is.Not.Null);
                Assert.That(creature.RangedAttack.IsMelee, Is.False);
                Assert.That(creature.RangedAttack.IsSpecial, Is.False);
                Assert.That(creature.FullRangedAttack, Is.Not.Empty);
                Assert.That(creature.FullRangedAttack.All(a => !a.IsMelee && !a.IsSpecial), Is.True);
            }

            foreach (var attack in creature.Attacks)
                AssertAttack(attack);

            Assert.That(creature.ArmorClass.TotalBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.FlatFootedBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.TouchBonus, Is.Positive, creature.Summary);

            Assert.That(creature.InitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Bonus));

            Assert.That(creature.Saves.Reflex, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Bonus));
            Assert.That(creature.Saves.Will, Is.AtLeast(creature.Abilities[AbilityConstants.Wisdom].Bonus));
            Assert.That(creature.Saves.Fortitude, Is.AtLeast(creature.Abilities[AbilityConstants.Constitution].Bonus));
        }

        private void AssertAttack(Attack attack)
        {
            Assert.That(attack.Name, Is.Not.Empty);
            Assert.That(attack.Damage, Is.Not.Empty, attack.Name);
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}
