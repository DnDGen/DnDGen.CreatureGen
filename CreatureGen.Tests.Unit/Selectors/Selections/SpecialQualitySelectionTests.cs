using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SpecialQualitySelectionTests
    {
        private SpecialQualitySelection selection;
        private Dictionary<string, Ability> abilities;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            selection = new SpecialQualitySelection();
            abilities = new Dictionary<string, Ability>();
            feats = new List<Feat>();

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 42;
        }

        [Test]
        public void SpecialQualitySelectionInitialization()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.Power, Is.EqualTo(0));
            Assert.That(selection.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(selection.SizeRequirement, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(selection.MinimumAbilities, Is.Empty);
            Assert.That(selection.RandomFociQuantity, Is.Empty);
            Assert.That(selection.RequiredFeats, Is.Empty);
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfWrongSize()
        {
            selection.SizeRequirement = "other size";

            var met = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfBelowHitDiceRange()
        {
            selection.MinimumHitDieRequirement = 3;

            var met = selection.RequirementsMet("size", 2, abilities, feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfAboveHitDiceRange()
        {
            selection.MaximumHitDieRequirement = 3;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAboveMaximumOfZero()
        {
            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMinimumHitDice()
        {
            selection.MinimumHitDieRequirement = 4;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMaximumHitDice()
        {
            selection.MaximumHitDieRequirement = 4;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfDoesNotHaveMinimumStat()
        {
            selection.MinimumAbilities["ability"] = 9266;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 9267;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 9256;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfMinimumStatIsNotMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 9255;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAnyMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;
            selection.MinimumAbilities["ability 2"] = 600;

            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 2"].BaseScore = 600;

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNoRequiredFeats()
        {
            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetWithAllRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetWithSomeRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };
            feats.Add(new Feat { Name = "feat 1" });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetWithNoRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetWithAllRequiredFeatsAndFoci()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat 1", Foci = new[] { "focus 1" } },
                new RequiredFeatSelection { Feat = "feat 2", Foci = new[] { "focus 2" } }
            };

            feats.Add(new Feat { Name = "feat 1", Foci = new[] { "focus 1", "focus 3" } });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus 4", "focus 2" } });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetWithExtraRequiredFeatsAndFoci()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat 1", Foci = new[] { "focus 1" } },
                new RequiredFeatSelection { Feat = "feat 2", Foci = new[] { "focus 2" } }
            };

            feats.Add(new Feat { Name = "feat 1", Foci = new[] { "focus 1", "focus 2" } });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus 2", "focus 1", "focus 3" } });
            feats.Add(new Feat { Name = "feat 3" });
            feats.Add(new Feat { Name = "feat 4", Foci = new[] { "focus 4" } });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetWithSomeRequiredFeatsAndFoci()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat 1", Foci = new[] { "focus 1" } },
                new RequiredFeatSelection { Feat = "feat 2", Foci = new[] { "focus 2" } }
            };

            feats.Add(new Feat { Name = "feat 1", Foci = new[] { "focus 2" } });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus 2", "focus 1" } });
            feats.Add(new Feat { Name = "feat 3", Foci = new[] { "focus 1" } });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetWithNoRequiredFeatsAndFoci()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat 1", Foci = new[] { "focus 1" } },
                new RequiredFeatSelection { Feat = "feat 2", Foci = new[] { "focus 2" } }
            };

            feats.Add(new Feat { Name = "feat 1", Foci = new[] { "focus 2" } });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus 1" } });
            feats.Add(new Feat { Name = "feat 3", Foci = new[] { "focus 1", "focus 2" } });

            var requirementsMet = selection.RequirementsMet("size", 0, abilities, feats);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void AllRequirementsMet()
        {
            selection.SizeRequirement = "size";
            selection.MinimumHitDieRequirement = 3;
            selection.MaximumHitDieRequirement = 5;
            selection.MinimumAbilities["ability"] = 42;

            selection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "feat 1" },
                new RequiredFeatSelection { Feat = "feat 2", Foci = new[] { "focus 2" } }
            };

            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus 2" } });

            var met = selection.RequirementsMet("size", 4, abilities, feats);
            Assert.That(met, Is.True);
        }
    }
}