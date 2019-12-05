using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SpecialQualitySelectionTests
    {
        private SpecialQualitySelection selection;
        private Dictionary<string, Ability> abilities;
        private List<Feat> feats;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            selection = new SpecialQualitySelection();
            abilities = new Dictionary<string, Ability>();
            feats = new List<Feat>();
            alignment = new Alignment();

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 42;
        }

        [Test]
        public void SpecialQualitySelectionInitialization()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.Power, Is.Zero);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MinimumAbilities, Is.Empty);
            Assert.That(selection.RandomFociQuantity, Is.Empty);
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredSizes, Is.Empty);
            Assert.That(selection.RequiredAlignments, Is.Empty);
            Assert.That(selection.RequiresEquipment, Is.False);
        }

        [Test]
        public void SpecialQualitySelectionDivider()
        {
            Assert.That(SpecialQualitySelection.Divider, Is.EqualTo('#'));
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfDoesNotHaveMinimumStat()
        {
            selection.MinimumAbilities["ability"] = 9266;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 9267;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 9256;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfMinimumStatIsNotMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 10;
            abilities["ability"].RacialAdjustment = 9255;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAnyMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;
            selection.MinimumAbilities["ability 2"] = 600;

            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 2"].BaseScore = 600;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNoRequiredFeats()
        {
            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetWithAllRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetWithSomeRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };
            feats.Add(new Feat { Name = "feat 1" });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetWithNoRequiredFeats()
        {
            selection.RequiredFeats = new[] { new RequiredFeatSelection { Feat = "feat 1" }, new RequiredFeatSelection { Feat = "feat 2" } };

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
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

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
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

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
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

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
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

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfEquipmentNotRequiredAndCreatureCanUseEquipment()
        {
            selection.RequiresEquipment = false;

            var requirementsMet = selection.RequirementsMet(abilities, feats, true, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfEquipmentRequiredAndCreatureCanUseEquipment()
        {
            selection.RequiresEquipment = true;

            var requirementsMet = selection.RequirementsMet(abilities, feats, true, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetIfEquipmentRequiredAndCreatureCannotUseEquipment()
        {
            selection.RequiresEquipment = true;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfEquipmentNotRequiredAndCreatureCannotUseEquipment()
        {
            selection.RequiresEquipment = false;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfNoRequiredCreatureSize()
        {
            selection.RequiredSizes = Enumerable.Empty<string>();

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsRequiredCreatureSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsAnyRequiredCreatureSize()
        {
            selection.RequiredSizes = new[] { "other size", "size" };

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NoitMetIfSizeIsNotRequiredCreatureSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NoitMetIfSizeIsNotAnyRequiredCreatureSize()
        {
            selection.RequiredSizes = new[] { "other size", "size" };

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfNoRequiredAlignment()
        {
            selection.RequiredAlignments = Enumerable.Empty<string>();

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsRequiredAlignment()
        {
            selection.RequiredAlignments = new[] { "lawfulness goodness" };

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [TestCase("lawfulness", "goodness")]
        [TestCase("other lawfulness", "goodness")]
        [TestCase("lawfulness", "other goodness")]
        [TestCase("other lawfulness", "other goodness")]
        public void MetIfSizeIsAnyRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = new[] { "other lawfulness goodness", "lawfulness other goodness", "lawfulness goodness", "other lawfulness other goodness" };

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.True);
        }

        [TestCase("wrong lawfulness", "goodness")]
        [TestCase("lawfulness", "wrong goodness")]
        [TestCase("wrong lawfulness", "wrong goodness")]
        public void NoitMetIfSizeIsNotRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = new[] { "lawfulness goodness" };

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.False);
        }

        [TestCase("wrong lawfulness", "goodness")]
        [TestCase("lawfulness", "wrong goodness")]
        [TestCase("wrong lawfulness", "wrong goodness")]
        public void NoitMetIfSizeIsNotAnyRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = new[] { "other lawfulness goodness", "lawfulness other goodness", "lawfulness goodness", "other lawfulness other goodness" };

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment);
            Assert.That(requirementsMet, Is.False);
        }
    }
}