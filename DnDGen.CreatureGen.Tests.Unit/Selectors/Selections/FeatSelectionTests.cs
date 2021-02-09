using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
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
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.Power, Is.Zero);
            Assert.That(selection.RequiredAbilities, Is.Empty);
            Assert.That(selection.RequiredBaseAttack, Is.Zero);
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredHands, Is.Zero);
            Assert.That(selection.RequiredNaturalWeapons, Is.Zero);
            Assert.That(selection.RequiredSizes, Is.Empty);
            Assert.That(selection.RequiredSkills, Is.Empty);
            Assert.That(selection.RequiredSpeeds, Is.Empty);
            Assert.That(selection.RequiresNaturalArmor, Is.False);
            Assert.That(selection.RequiresSpecialAttack, Is.False);
            Assert.That(selection.RequiresSpellLikeAbility, Is.False);
            Assert.That(selection.RequiresEquipment, Is.False);
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
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
            feats[0].Foci = new[] { GroupConstants.All };
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

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void BaseAttackRequirementNotMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void BaseAttackRequirementMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void CasterLevelRequirementNotMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void CasterLevelRequirementMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.SumOfValuesLessThanPositiveRequirementWithMinimumOne))]
        public void AbilityRequirementsNotMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MaxValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesAndAllPositiveRequirements))]
        public void AbilityRequirementsNotMetBecauseBaseScoreOfZero(int requiredScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MaxValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.SumOfValuesGreaterThanOrEqualToPositiveRequirement))]
        public void AbilityRequirementsMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MinValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMetIfNotRequired()
        {
            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = 0;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void ClassSkillRequirementsNotMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = new[] {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = true;
            skills[1].Ranks = int.MaxValue;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void CrossClassSkillRequirementsNotMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = new[] {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks * 2;
            skills[0].ClassSkill = false;
            skills[1].Ranks = int.MaxValue;
            skills[1].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void ClassSkillRequirementsMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = new[] {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = true;
            skills[1].Ranks = int.MinValue;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void CrossClassSkillRequirementsMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = new[] {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks * 2;
            skills[0].ClassSkill = false;
            skills[1].Ranks = int.MinValue;
            skills[1].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCase(0, 0, 0, true, 0, true, true)]
        [TestCase(0, 0, 0, true, 0, false, true)]
        [TestCase(0, 0, 0, false, 0, true, true)]
        [TestCase(0, 0, 0, false, 0, false, true)]
        [TestCase(9266, 90210, 42, true, 600, true, false)]
        [TestCase(9266, 90210, 42, true, 600, false, false)]
        [TestCase(9266, 90210, 42, false, 600, true, false)]
        [TestCase(9266, 90210, 42, false, 600, false, false)]
        [TestCase(42, 600, 1337, true, 1336, true, true)]
        [TestCase(42, 600, 1337, true, 1336, false, true)]
        [TestCase(42, 600, 1337, false, 1336, true, true)]
        [TestCase(42, 600, 1337, false, 1336, false, true)]
        [TestCase(1337, 1336, 96, true, 783, true, false)]
        [TestCase(1337, 1336, 96, true, 783, false, false)]
        [TestCase(1337, 1336, 96, false, 783, true, false)]
        [TestCase(1337, 1336, 96, false, 783, false, false)]
        [TestCase(96, 783, 8245, true, 1234, true, true)]
        [TestCase(96, 783, 8245, true, 1234, false, false)]
        [TestCase(96, 783, 8245, false, 1234, true, true)]
        [TestCase(96, 783, 8245, false, 1234, false, false)]
        [TestCase(600, 1336, 783, true, 1337, true, true)]
        [TestCase(600, 1336, 783, true, 1337, false, false)]
        [TestCase(600, 1336, 783, false, 1337, true, false)]
        [TestCase(600, 1336, 783, false, 1337, false, false)]
        [TestCase(8245, 42, 9266, true, 1337, true, true)]
        [TestCase(8245, 42, 9266, true, 1337, false, true)]
        [TestCase(8245, 42, 9266, false, 1337, true, false)]
        [TestCase(8245, 42, 9266, false, 1337, false, false)]
        public void AllRequiredSkillWithSufficientRanksMeetRequirement(
            int requiredRanks1,
            int requiredRanks2,
            int ranks1,
            bool classSkill1,
            int ranks2,
            bool classSkill2,
            bool isMet)
        {
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks1 },
                new RequiredSkillSelection { Skill = "other skill", Ranks = requiredRanks2 }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks1;
            skills[0].ClassSkill = classSkill1;
            skills[1].Ranks = ranks2;
            skills[1].ClassSkill = classSkill2;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.EqualTo(isMet));
        }

        [Test]
        public void MeetSkillRequirementOfZeroRanks()
        {
            selection.RequiredSkills = new[] { new RequiredSkillSelection { Skill = "skill", Ranks = 0 } };
            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills[0].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = true, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpecialAttacksNotRequired()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoSpellLikeAbilities()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";

            selection.RequiresSpellLikeAbility = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpellLikeAbilities()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = FeatConstants.SpecialQualities.SpellLikeAbility;

            selection.RequiresSpellLikeAbility = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpellLikeAbilitiesNotRequired()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";

            selection.RequiresSpellLikeAbility = false;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfDoesNotHaveRequiredSpeed()
        {
            selection.RequiredSpeeds["other speed"] = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfDoesNotHaveRequiredSpeedValue(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfHasRequiredSpeed(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllNonPositiveValues))]
        public void NotMetIfNoNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllPositiveValues))]
        public void MetIfNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalArmorNotRequired()
        {
            selection.RequiresNaturalArmor = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfNaturalWeaponQuantityNotEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfNaturalWeaponQuantityEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalWeaponQuantityNotRequired()
        {
            selection.RequiredNaturalWeapons = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfInsufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfSufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSufficientHandsNotRequired()
        {
            selection.RequiredHands = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfDoesNotHaveSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void NotMetIfDoesNotHaveAnySize()
        {
            selection.RequiredSizes = new[] { "size", "other size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfHasSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "size", false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfHasAnySize()
        {
            selection.RequiredSizes = new[] { "size", "other size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "other size", false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSizeNotRequired()
        {
            Assert.That(selection.RequiredSizes, Is.Empty);

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.True);
        }

        [TestCase(true, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, false, true)]
        public void HonorEquipmentRequirement(bool canUseEquipment, bool requiresEquipment, bool shouldBeMet)
        {
            selection.RequiresEquipment = requiresEquipment;
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, canUseEquipment);
            Assert.That(met, Is.EqualTo(shouldBeMet));
        }
    }
}