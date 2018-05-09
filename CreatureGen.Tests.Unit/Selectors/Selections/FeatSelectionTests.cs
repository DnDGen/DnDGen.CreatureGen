using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class FeatSelectionTests
    {
        private FeatSelection selection;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, Measurement> speeds;
        private List<Skill> skills;
        private List<Attack> attacks;

        [SetUp]
        public void Setup()
        {
            selection = new FeatSelection();
            feats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            attacks = new List<Attack>();
            speeds = new Dictionary<string, Measurement>();

            abilities["ability"] = new Ability("ability");
            speeds["speed"] = new Measurement("mph");
        }

        [Test]
        public void FeatSelectionInitialized()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.RequiredAbilities, Is.Empty);
            Assert.That(selection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredSkills, Is.Empty);
            Assert.That(selection.RequiredSpeeds, Is.Empty);
            Assert.That(selection.RequiresSpecialAttack, Is.False);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Power, Is.EqualTo(0));
            Assert.That(selection.Frequency, Is.Not.Null);
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsMetIfFeatNotAlreadySelected()
        {
            feats.Add(new Feat());
            feats[0].Name = "other feat";
            selection.Feat = "feat";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsNotMetIfFeatAlreadySelected()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.Feat = "feat";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MutableRequirementsMetIfFeatAlreadySelectedWithFocus()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = new[] { "focus" };
            selection.Feat = "feat";
            selection.FocusType = "focus type";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsNotMetIfFeatAlreadySelectedWithFocusOfAll()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = new[] { FeatConstants.Foci.All };
            selection.Feat = "feat";
            selection.FocusType = "focus type";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MutableRequirementsMetIfFeatAlreadySelectedAndCanBeTakenMultipleTimes()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].CanBeTakenMultipleTimes = true;
            selection.Feat = "feat";
            selection.CanBeTakenMultipleTimes = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsMetIfNoRequirements()
        {
            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void BaseAttackRequirementNotMet()
        {
            selection.RequiredBaseAttack = 2;

            var met = selection.ImmutableRequirementsMet(1, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void CasterLevelRequirementNotMet()
        {
            selection.MinimumCasterLevel = 2;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 1, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AbilityRequirementsNotMetBecauseScoreNotHighEnough()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 15;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AbilityRequirementsMet()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 16;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMetWithRacialAdjustment()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 6;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMoreThanMet()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 17;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMoreThanMetWithRacialAdjustment()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 11;
            abilities["ability"].RacialAdjustment = 6;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsNotMetBecauseScoreNotHighEnoughWithRacialAdjustment()
        {
            selection.RequiredAbilities["ability"] = 16;

            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 5;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AbilityRequirementsNotMetBecauseAbilityHasNoScore()
        {
            selection.RequiredAbilities["ability"] = 1;

            abilities["ability"].BaseScore = 0;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 157;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsNotMet()
        {
            selection.RequiredSkills = new[] { new RequiredSkillSelection { Skill = "skill", Ranks = 5 } };
            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills.Add(new Skill("other skill", abilities["ability"], 10));
            skills[0].Ranks = 9;
            skills[0].ClassSkill = false;
            skills[1].Ranks = 10;
            skills[1].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AnyRequiredSkillWithSufficientRanksMeetRequirement()
        {
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "other skill", Ranks = 1 }
            };

            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills.Add(new Skill("other skill", abilities["ability"], 10));
            skills[0].Ranks = 4;
            skills[0].ClassSkill = true;
            skills[1].Ranks = 1;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MeetSkillRequirementOfZeroRanks()
        {
            selection.RequiredSkills = new[] { new RequiredSkillSelection { Skill = "skill", Ranks = 0 } };
            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills[0].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AllImmutableRequirementsMet()
        {
            selection.RequiredBaseAttack = 2;
            selection.MinimumCasterLevel = 2;

            selection.RequiredAbilities["ability"] = 16;
            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 16;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 15;

            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "class skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "cross-class skill", Ranks = 5 }
            };

            skills.Add(new Skill("class skill", abilities["ability"], 10));
            skills.Add(new Skill("other class skill", abilities["ability"], 10));
            skills.Add(new Skill("cross-class skill", abilities["ability"], 10));
            skills.Add(new Skill("other cross-class skill", abilities["ability"], 10));

            skills[0].Ranks = 10;
            skills[0].ClassSkill = false;
            skills[1].Ranks = 9;
            skills[1].ClassSkill = false;
            skills[2].Ranks = 5;
            skills[2].ClassSkill = true;
            skills[3].Ranks = 4;
            skills[3].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(2, abilities, skills, attacks, 2, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat" },
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFocusMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = new[] { "focus", "other focus" };
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat", Foci = new[] { "focus" } },
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFociMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = new[] { "focus" };
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat", Foci = new[] { "focus", "other focus" } },
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFocusNotMetByFeatName()
        {
            feats.Add(new Feat());
            feats[0].Name = "other feat";
            feats[0].Foci = new[] { "focus", "other focus" };
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat", Foci = new[] { "focus" } },
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void FeatRequirementsWithFocusNotMetByFocus()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = new[] { "wrong focus", "other focus" };
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat", Foci = new[] { "focus" } },
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void FeatRequirementsNotMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat" },
                new RequiredFeatSelection { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllMutableRequirementsMet()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat" },
                new RequiredFeatSelection { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void ExtraFeatDoNotMatter()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            feats[2].Name = "yet another feat";

            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat" },
                new RequiredFeatSelection { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = true, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpecialAttacksNotRequired()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoSpellLikeAbilities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void MetIfSpellLikeAbilities()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void MetIfSpellLikeAbilitiesNotRequired()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void NotMetIfDoesNotHaveRequiredSpeed()
        {
            selection.RequiredSpeeds["other speed"] = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void NotMetIfDoesNotHaveRequiredSpeedValue()
        {
            speeds["speed"].Value = 9265;
            selection.RequiredSpeeds["speed"] = 9266;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfHasRequiredSpeed()
        {
            speeds["speed"].Value = 9266;
            selection.RequiredSpeeds["speed"] = 9266;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfHasMoreThanRequiredSpeed()
        {
            speeds["speed"].Value = 9267;
            selection.RequiredSpeeds["speed"] = 9266;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void MetIfNaturalArmor()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void NotMetIfNaturalWeaponQuantityNotEnough()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void MetIfNaturalWeaponQuantityEnough()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void NotMetIfInsufficientHands()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void MetIfSufficfientHands()
        {
            Assert.Fail("not yet written");
        }
    }
}