using DnDGen.CreatureGen.Abilities;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Abilities
{
    [TestFixture]
    public class AbilityTests
    {
        private Ability ability;

        [SetUp]
        public void Setup()
        {
            ability = new Ability("ability name");
        }

        [Test]
        public void DefaultScoreIs10()
        {
            Assert.That(Ability.DefaultScore, Is.EqualTo(10));
        }

        [Test]
        public void AbilityInitialized()
        {
            Assert.That(ability.Name, Is.EqualTo("ability name"));
            Assert.That(ability.BaseScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.RacialAdjustment, Is.Zero);
            Assert.That(ability.AdvancementAdjustment, Is.Zero);
            Assert.That(ability.TemplateAdjustment, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(ability.TemplateScore, Is.EqualTo(-1));
            Assert.That(ability.Bonuses, Is.Empty);
            Assert.That(ability.Bonus, Is.Zero);
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(21, 5)]
        [TestCase(22, 6)]
        [TestCase(23, 6)]
        [TestCase(24, 7)]
        [TestCase(25, 7)]
        [TestCase(26, 8)]
        [TestCase(27, 8)]
        [TestCase(28, 9)]
        [TestCase(29, 9)]
        [TestCase(30, 10)]
        [TestCase(31, 10)]
        [TestCase(32, 11)]
        [TestCase(33, 11)]
        [TestCase(34, 12)]
        [TestCase(35, 12)]
        [TestCase(36, 13)]
        [TestCase(37, 13)]
        [TestCase(38, 14)]
        [TestCase(39, 14)]
        [TestCase(40, 15)]
        [TestCase(41, 15)]
        [TestCase(42, 16)]
        [TestCase(43, 16)]
        [TestCase(44, 17)]
        [TestCase(45, 17)]
        public void AbilityModifier(int baseValue, int bonus)
        {
            ability.BaseScore = baseValue;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(21, 5)]
        [TestCase(22, 6)]
        [TestCase(23, 6)]
        [TestCase(24, 7)]
        [TestCase(25, 7)]
        [TestCase(26, 8)]
        [TestCase(27, 8)]
        [TestCase(28, 9)]
        [TestCase(29, 9)]
        [TestCase(30, 10)]
        [TestCase(31, 10)]
        [TestCase(32, 11)]
        [TestCase(33, 11)]
        [TestCase(34, 12)]
        [TestCase(35, 12)]
        [TestCase(36, 13)]
        [TestCase(37, 13)]
        [TestCase(38, 14)]
        [TestCase(39, 14)]
        [TestCase(40, 15)]
        [TestCase(41, 15)]
        [TestCase(42, 16)]
        [TestCase(43, 16)]
        [TestCase(44, 17)]
        [TestCase(45, 17)]
        public void AbilityModifier_WithTemplateScore(int templateScore, int bonus)
        {
            ability.TemplateScore = templateScore;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, 0, -5)]
        [TestCase(1, 1, -5)]
        [TestCase(1, 2, -5)]
        [TestCase(1, 10, -5)]
        [TestCase(2, 0, -4)]
        [TestCase(2, 1, -4)]
        [TestCase(2, 2, -4)]
        [TestCase(2, 10, -4)]
        [TestCase(4, 0, -3)]
        [TestCase(4, 1, -3)]
        [TestCase(4, 2, -3)]
        [TestCase(4, 10, -3)]
        [TestCase(6, 0, -2)]
        [TestCase(6, 1, -2)]
        [TestCase(6, 2, -2)]
        [TestCase(6, 10, -2)]
        [TestCase(8, 0, -1)]
        [TestCase(8, 1, -1)]
        [TestCase(8, 2, -1)]
        [TestCase(8, 10, -1)]
        [TestCase(10, 0, 0)]
        [TestCase(10, 1, 0)]
        [TestCase(10, 2, 0)]
        [TestCase(10, 10, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(12, 1, 1)]
        [TestCase(12, 2, 1)]
        [TestCase(12, 10, 1)]
        [TestCase(14, 0, 0)]
        [TestCase(14, 1, 1)]
        [TestCase(14, 2, 2)]
        [TestCase(14, 3, 2)]
        [TestCase(14, 10, 2)]
        [TestCase(16, 0, 0)]
        [TestCase(16, 1, 1)]
        [TestCase(16, 2, 2)]
        [TestCase(16, 3, 3)]
        [TestCase(16, 4, 3)]
        [TestCase(16, 10, 3)]
        [TestCase(18, 0, 0)]
        [TestCase(18, 1, 1)]
        [TestCase(18, 2, 2)]
        [TestCase(18, 3, 3)]
        [TestCase(18, 4, 4)]
        [TestCase(18, 5, 4)]
        [TestCase(18, 10, 4)]
        [TestCase(20, 0, 0)]
        [TestCase(20, 1, 1)]
        [TestCase(20, 2, 2)]
        [TestCase(20, 3, 3)]
        [TestCase(20, 4, 4)]
        [TestCase(20, 5, 5)]
        [TestCase(20, 6, 5)]
        [TestCase(20, 10, 5)]
        [TestCase(22, 0, 0)]
        [TestCase(22, 1, 1)]
        [TestCase(22, 2, 2)]
        [TestCase(22, 3, 3)]
        [TestCase(22, 4, 4)]
        [TestCase(22, 5, 5)]
        [TestCase(22, 6, 6)]
        [TestCase(22, 7, 6)]
        [TestCase(22, 10, 6)]
        [TestCase(24, 0, 0)]
        [TestCase(24, 1, 1)]
        [TestCase(24, 2, 2)]
        [TestCase(24, 3, 3)]
        [TestCase(24, 4, 4)]
        [TestCase(24, 5, 5)]
        [TestCase(24, 6, 6)]
        [TestCase(24, 7, 7)]
        [TestCase(24, 8, 7)]
        [TestCase(24, 10, 7)]
        [TestCase(26, 0, 0)]
        [TestCase(26, 1, 1)]
        [TestCase(26, 2, 2)]
        [TestCase(26, 3, 3)]
        [TestCase(26, 4, 4)]
        [TestCase(26, 5, 5)]
        [TestCase(26, 6, 6)]
        [TestCase(26, 7, 7)]
        [TestCase(26, 8, 8)]
        [TestCase(26, 9, 8)]
        [TestCase(26, 10, 8)]
        [TestCase(28, 0, 0)]
        [TestCase(28, 1, 1)]
        [TestCase(28, 2, 2)]
        [TestCase(28, 3, 3)]
        [TestCase(28, 4, 4)]
        [TestCase(28, 5, 5)]
        [TestCase(28, 6, 6)]
        [TestCase(28, 7, 7)]
        [TestCase(28, 8, 8)]
        [TestCase(28, 9, 9)]
        [TestCase(28, 10, 9)]
        [TestCase(30, 0, 0)]
        [TestCase(30, 1, 1)]
        [TestCase(30, 2, 2)]
        [TestCase(30, 3, 3)]
        [TestCase(30, 4, 4)]
        [TestCase(30, 5, 5)]
        [TestCase(30, 6, 6)]
        [TestCase(30, 7, 7)]
        [TestCase(30, 8, 8)]
        [TestCase(30, 9, 9)]
        [TestCase(30, 10, 10)]
        public void AbilityModifier_WithMaxModifier(int baseValue, int max, int bonus)
        {
            ability.BaseScore = baseValue;
            ability.MaxModifier = max;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, 0, -5)]
        [TestCase(1, 1, -5)]
        [TestCase(1, 2, -5)]
        [TestCase(1, 10, -5)]
        [TestCase(2, 0, -4)]
        [TestCase(2, 1, -4)]
        [TestCase(2, 2, -4)]
        [TestCase(2, 10, -4)]
        [TestCase(4, 0, -3)]
        [TestCase(4, 1, -3)]
        [TestCase(4, 2, -3)]
        [TestCase(4, 10, -3)]
        [TestCase(6, 0, -2)]
        [TestCase(6, 1, -2)]
        [TestCase(6, 2, -2)]
        [TestCase(6, 10, -2)]
        [TestCase(8, 0, -1)]
        [TestCase(8, 1, -1)]
        [TestCase(8, 2, -1)]
        [TestCase(8, 10, -1)]
        [TestCase(10, 0, 0)]
        [TestCase(10, 1, 0)]
        [TestCase(10, 2, 0)]
        [TestCase(10, 10, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(12, 1, 1)]
        [TestCase(12, 2, 1)]
        [TestCase(12, 10, 1)]
        [TestCase(14, 0, 0)]
        [TestCase(14, 1, 1)]
        [TestCase(14, 2, 2)]
        [TestCase(14, 3, 2)]
        [TestCase(14, 10, 2)]
        [TestCase(16, 0, 0)]
        [TestCase(16, 1, 1)]
        [TestCase(16, 2, 2)]
        [TestCase(16, 3, 3)]
        [TestCase(16, 4, 3)]
        [TestCase(16, 10, 3)]
        [TestCase(18, 0, 0)]
        [TestCase(18, 1, 1)]
        [TestCase(18, 2, 2)]
        [TestCase(18, 3, 3)]
        [TestCase(18, 4, 4)]
        [TestCase(18, 5, 4)]
        [TestCase(18, 10, 4)]
        [TestCase(20, 0, 0)]
        [TestCase(20, 1, 1)]
        [TestCase(20, 2, 2)]
        [TestCase(20, 3, 3)]
        [TestCase(20, 4, 4)]
        [TestCase(20, 5, 5)]
        [TestCase(20, 6, 5)]
        [TestCase(20, 10, 5)]
        [TestCase(22, 0, 0)]
        [TestCase(22, 1, 1)]
        [TestCase(22, 2, 2)]
        [TestCase(22, 3, 3)]
        [TestCase(22, 4, 4)]
        [TestCase(22, 5, 5)]
        [TestCase(22, 6, 6)]
        [TestCase(22, 7, 6)]
        [TestCase(22, 10, 6)]
        [TestCase(24, 0, 0)]
        [TestCase(24, 1, 1)]
        [TestCase(24, 2, 2)]
        [TestCase(24, 3, 3)]
        [TestCase(24, 4, 4)]
        [TestCase(24, 5, 5)]
        [TestCase(24, 6, 6)]
        [TestCase(24, 7, 7)]
        [TestCase(24, 8, 7)]
        [TestCase(24, 10, 7)]
        [TestCase(26, 0, 0)]
        [TestCase(26, 1, 1)]
        [TestCase(26, 2, 2)]
        [TestCase(26, 3, 3)]
        [TestCase(26, 4, 4)]
        [TestCase(26, 5, 5)]
        [TestCase(26, 6, 6)]
        [TestCase(26, 7, 7)]
        [TestCase(26, 8, 8)]
        [TestCase(26, 9, 8)]
        [TestCase(26, 10, 8)]
        [TestCase(28, 0, 0)]
        [TestCase(28, 1, 1)]
        [TestCase(28, 2, 2)]
        [TestCase(28, 3, 3)]
        [TestCase(28, 4, 4)]
        [TestCase(28, 5, 5)]
        [TestCase(28, 6, 6)]
        [TestCase(28, 7, 7)]
        [TestCase(28, 8, 8)]
        [TestCase(28, 9, 9)]
        [TestCase(28, 10, 9)]
        [TestCase(30, 0, 0)]
        [TestCase(30, 1, 1)]
        [TestCase(30, 2, 2)]
        [TestCase(30, 3, 3)]
        [TestCase(30, 4, 4)]
        [TestCase(30, 5, 5)]
        [TestCase(30, 6, 6)]
        [TestCase(30, 7, 7)]
        [TestCase(30, 8, 8)]
        [TestCase(30, 9, 9)]
        [TestCase(30, 10, 10)]
        public void AbilityModifier_WithTemplateScore_WithMaxModifier(int templateScore, int max, int bonus)
        {
            ability.TemplateScore = templateScore;
            ability.MaxModifier = max;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [Test]
        public void FullScore_UseTemplateScore()
        {
            ability.TemplateScore = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9266));
            Assert.That(ability.Modifier, Is.EqualTo(4628));
        }

        [Test]
        public void FullScore_UseTemplateScore_Zero()
        {
            ability.TemplateScore = 0;
            ability.TemplateAdjustment = 0;

            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void FullScore_UseTemplateScore_NoAdditives()
        {
            ability.TemplateScore = 9266;
            ability.BaseScore = 666;
            ability.AdvancementAdjustment = 666;
            ability.RacialAdjustment = 666;

            Assert.That(ability.FullScore, Is.EqualTo(9266));
            Assert.That(ability.Modifier, Is.EqualTo(4628));
        }

        [Test]
        public void FullScore_UseTemplateScore_AddTemplateAdjustment()
        {
            ability.TemplateScore = 9266;
            ability.TemplateAdjustment = 90210;

            Assert.That(ability.FullScore, Is.EqualTo(9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49733));
        }

        [Test]
        public void FullScore_UseTemplateScore_AddNegativeTemplateAdjustment()
        {
            ability.TemplateScore = 9266;
            ability.TemplateAdjustment = -6;

            Assert.That(ability.FullScore, Is.EqualTo(9260));
            Assert.That(ability.Modifier, Is.EqualTo(4625));
        }

        [Test]
        public void FullScore_AddRacialAdjustment()
        {
            ability.RacialAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddNegativeRacialAdjustment()
        {
            ability.RacialAdjustment = -6;
            Assert.That(ability.FullScore, Is.EqualTo(4));
            Assert.That(ability.Modifier, Is.EqualTo(-3));
        }

        [Test]
        public void FullScore_AddAgeAdjustment()
        {
            ability.AgeAdjustment = -2;
            Assert.That(ability.FullScore, Is.EqualTo(8));
            Assert.That(ability.Modifier, Is.EqualTo(-1));
        }

        [Test]
        public void FullScore_AddAdvancementAdjustment()
        {
            ability.AdvancementAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddTemplateAdjustment()
        {
            ability.TemplateAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddAllAdjustments()
        {
            ability.AdvancementAdjustment = 9266;
            ability.RacialAdjustment = 90210;
            ability.TemplateAdjustment = 42;
            ability.AgeAdjustment = -2;

            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore + 9266 + 90210 + 42 - 2));
            Assert.That(ability.Modifier, Is.EqualTo(49758));
        }

        [Test]
        public void AbilityCannotHaveFullScoreLessThan1_FromRacial()
        {
            ability.RacialAdjustment = -9266;
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityCannotHaveFullScoreLessThan1_FromAge()
        {
            ability.AgeAdjustment = -9266;
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityCanHaveScoreOfZero()
        {
            ability.BaseScore = 0;
            Assert.That(ability.BaseScore, Is.Zero);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void AbilityHasTemplateScore(int score)
        {
            ability.TemplateScore = score;

            Assert.That(ability.HasTemplateScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(score));
        }

        [Test]
        public void AbilityDoesNotHaveTemplateScore()
        {
            ability.TemplateScore = -1;

            Assert.That(ability.HasTemplateScore, Is.False);
            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasScore_HasTemplateScore()
        {
            ability.TemplateScore = 1;
            ability.BaseScore = 0;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityHasScore()
        {
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityDoesNotHaveScore_HasTemplateScore()
        {
            ability.TemplateScore = 0;
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityDoesNotHaveScore_HasTemplateScoreAndAdjustment()
        {
            ability.TemplateScore = 0;
            ability.TemplateAdjustment = 1;
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityDoesNotHaveScore()
        {
            ability.BaseScore = 0;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasScore_WithTemplateAdjustment()
        {
            ability.BaseScore = 0;
            ability.TemplateAdjustment = 1;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityHasNoScore_WithTemplateAdjustment()
        {
            ability.BaseScore = 1;
            ability.TemplateAdjustment = -1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasFullScoreOfZero()
        {
            ability.BaseScore = 0;
            ability.RacialAdjustment = 9266;
            ability.AdvancementAdjustment = 90210;

            Assert.That(ability.BaseScore, Is.Zero);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void Bonus_TotalsBonuses()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void Bonus_TotalsBonuses_WithNegative()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = -42 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210 - 42));
        }

        [Test]
        public void Bonus_TotalsBonuses_WithAllNegative()
        {
            ability.Bonuses.Add(new Bonus { Value = -9266 });
            ability.Bonuses.Add(new Bonus { Value = -42 });

            Assert.That(ability.Bonus, Is.EqualTo(-9266 - 42));
        }

        [Test]
        public void Bonus_TotalsBonuses_DoNotIncludeConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.Bonuses.Add(new Bonus { Value = 90210 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void Bonus_TotalsBonuses_AllConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266, Condition = "when I want" });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });

            Assert.That(ability.Bonus, Is.Zero);
        }

        [Test]
        public void FullScore_IncludeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
        }

        [Test]
        public void FullScore_IncludeBonus_NoConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
        }

        [Test]
        public void FullScore_IncludeNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 - 600));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 - 600));
        }

        [Test]
        public void FullScore_IncludeAllNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(42 - 4));
        }

        [Test]
        public void FullScore_IncludeNegativeBonus_StillMin1()
        {
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-600));
            Assert.That(ability.FullScore, Is.EqualTo(1));
        }

        [Test]
        public void Modifier_IncludeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49754));
        }

        [Test]
        public void Modifier_IncludeBonus_NoConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49754));
        }

        [Test]
        public void Modifier_IncludeNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 - 600));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 - 600));
            Assert.That(ability.Modifier, Is.EqualTo(4349));
        }

        [Test]
        public void Modifier_IncludeAllNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(42 - 4));
            Assert.That(ability.Modifier, Is.EqualTo(14));
        }

        [Test]
        public void Modifier_IncludeAllNegativeBonus_NegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 12;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(12 - 4));
            Assert.That(ability.Modifier, Is.EqualTo(-1));
        }
    }
}