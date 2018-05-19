using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            Assert.That(selection.RequiredHands, Is.EqualTo(0));
            Assert.That(selection.RequiredNaturalWeapons, Is.EqualTo(0));
            Assert.That(selection.RequiredSkills, Is.Empty);
            Assert.That(selection.RequiredSpeeds, Is.Empty);
            Assert.That(selection.RequiresNaturalArmor, Is.False);
            Assert.That(selection.RequiresSpecialAttack, Is.False);
            Assert.That(selection.RequiresSpellLikeAbility, Is.False);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Power, Is.EqualTo(0));
            Assert.That(selection.Frequency, Is.Not.Null);
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
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
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
        public void BaseAttackRequirementNotMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        private class NumericTestData
        {
            private static IEnumerable<int> testValues = new[]
            {
                -90210,
                -9266,
                -1337,
                -1336,
                -620,
                -600,
                -96,
                -42,
                -2,
                -1,
                0,
                1,
                2,
                42,
                96,
                600,
                620,
                1336,
                1337,
                9266,
                90210
            };

            public static IEnumerable ValueLessThanPositiveRequirement
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in positive)
                    {
                        var values = testValues.Where(v => v < requirement);

                        foreach (var value in values)
                        {
                            yield return new TestCaseData(requirement, value);
                        }
                    }
                }
            }

            public static IEnumerable ValueGreaterThanOrEqualToPositiveRequirement
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in testValues)
                    {
                        var values = testValues.Where(v => v >= requirement);

                        foreach (var value in values)
                        {
                            yield return new TestCaseData(requirement, value);
                        }
                    }
                }
            }

            public static IEnumerable SumOfValuesLessThanPositiveRequirement
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in positive)
                    {
                        foreach (var value1 in positive)
                        {
                            foreach (var value2 in testValues)
                            {
                                if (value1 + value2 < requirement)
                                    yield return new TestCaseData(requirement, value1, value2);
                            }
                        }
                    }
                }
            }

            public static IEnumerable SumOfValuesLessThanPositiveRequirementWithMinimumOne
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in positive)
                    {
                        foreach (var value1 in positive)
                        {
                            foreach (var value2 in testValues)
                            {
                                var sum = Math.Max(value1 + value2, 1);

                                if (sum < requirement)
                                    yield return new TestCaseData(requirement, value1, value2);
                            }
                        }
                    }
                }
            }

            public static IEnumerable SumOfValuesGreaterThanOrEqualToPositiveRequirement
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in positive)
                    {
                        foreach (var value1 in positive)
                        {
                            foreach (var value2 in testValues)
                            {
                                if (value1 + value2 >= requirement)
                                    yield return new TestCaseData(requirement, value1, value2);
                            }
                        }
                    }
                }
            }

            public static IEnumerable AllValuesAndAllPositiveRequirements
            {
                get
                {
                    var positive = testValues.Where(v => v > 0);

                    foreach (var requirement in positive)
                    {
                        foreach (var value in testValues)
                        {
                            yield return new TestCaseData(requirement, value);
                        }
                    }
                }
            }
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void BaseAttackRequirementMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
        public void CasterLevelRequirementNotMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void CasterLevelRequirementMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "SumOfValuesLessThanPositiveRequirementWithMinimumOne")]
        public void AbilityRequirementsNotMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MaxValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "AllValuesAndAllPositiveRequirements")]
        public void AbilityRequirementsNotMetBecauseBaseScoreOfZero(int requiredScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MaxValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "SumOfValuesGreaterThanOrEqualToPositiveRequirement")]
        public void AbilityRequirementsMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = int.MinValue;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMetIfNotRequired()
        {
            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = 0;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void AnyRequiredSkillWithSufficientRanksMeetRequirement(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "skill", Ranks = requiredRanks },
                new RequiredSkillSelection { Skill = "other skill", Ranks = int.MaxValue }
            };

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = true;
            skills[1].Ranks = int.MinValue;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MeetSkillRequirementOfZeroRanks()
        {
            selection.RequiredSkills = new[] { new RequiredSkillSelection { Skill = "skill", Ranks = 0 } };
            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills[0].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = true, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpecialAttacksNotRequired()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
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

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
        public void NotMetIfDoesNotHaveRequiredSpeedValue(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void MetIfHasRequiredSpeed(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [TestCase(-2)]
        [TestCase(-1)]
        [TestCase(0)]
        public void NotMetIfNoNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0);
            Assert.That(met, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(42)]
        [TestCase(96)]
        [TestCase(600)]
        [TestCase(620)]
        [TestCase(1336)]
        [TestCase(1337)]
        [TestCase(9266)]
        [TestCase(90210)]
        public void MetIfNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalArmorNotRequired()
        {
            selection.RequiresNaturalArmor = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
        public void NotMetIfNaturalWeaponQuantityNotEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void MetIfNaturalWeaponQuantityEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalWeaponQuantityNotRequired()
        {
            selection.RequiredNaturalWeapons = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueLessThanPositiveRequirement")]
        public void NotMetIfInsufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands);
            Assert.That(met, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(NumericTestData), "ValueGreaterThanOrEqualToPositiveRequirement")]
        public void MetIfSufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSufficientHandsNotRequired()
        {
            selection.RequiredHands = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0);
            Assert.That(met, Is.True);
        }
    }
}