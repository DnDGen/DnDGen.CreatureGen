using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Characters
{
    [TestFixture]
    public class CharacterTests
    {
        private Character character;

        [SetUp]
        public void Setup()
        {
            character = new Character();
        }

        [Test]
        public void CharacterInitialized()
        {
            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(character.Class, Is.Not.Null);
            Assert.That(character.InterestingTrait, Is.Empty);
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Feats, Is.Not.Null);
            Assert.That(character.Languages, Is.Empty);
            Assert.That(character.Skills, Is.Empty);
            Assert.That(character.Abilities, Is.Empty);
            Assert.That(character.Combat, Is.Not.Null);
            Assert.That(character.Equipment, Is.Not.Null);
            Assert.That(character.Magic, Is.Not.Null);
            Assert.That(character.IsLeader, Is.False);
            Assert.That(character.Summary, Is.Empty);
        }

        [Test]
        public void IsLeaderIfHasLeadershipFeat()
        {
            character.Feats.Additional = new[]
            {
                new Feat { Name = FeatConstants.Leadership },
                new Feat { Name = "other feat" }
            };

            Assert.That(character.IsLeader, Is.True);
        }

        [Test]
        public void IsNotLeaderIfDoesNotHaveLeadershipFeat()
        {
            character.Feats.Additional = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" }
            };

            Assert.That(character.IsLeader, Is.False);
        }

        [Test]
        public void CharacterHasSummary()
        {
            character.Alignment.Goodness = "goodness";
            character.Alignment.Lawfulness = "lawfulness";
            character.Class.Level = 9266;
            character.Class.Name = "class name";
            character.Race.BaseRace = "base race";

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Female base race Level 9266 class name"));
        }

        [Test]
        public void CharacterHasSummaryWithMetarace()
        {
            character.Alignment.Goodness = "goodness";
            character.Alignment.Lawfulness = "lawfulness";
            character.Class.Level = 9266;
            character.Class.Name = "class name";
            character.Race.BaseRace = "base race";
            character.Race.Metarace = "metarace";
            character.Race.IsMale = true;

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Male metarace base race Level 9266 class name"));
        }

        [Test]
        public void ChallengeRatingForCharacterIsClassLevel()
        {
            character.Class.Level = 9266;
            Assert.That(character.ChallengeRating, Is.EqualTo(9266));
        }

        [Test]
        public void ChallengeRatingForCharacterIsClassLevelAndRacialChallengeRating()
        {
            character.Class.Level = 9266;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void ChallengeRatingForNPCIsHalfOfClassLevel()
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 / 2));
        }

        [Test]
        public void ChallengeRatingForNPCIsHalfOfClassLevelAndAllOfRacialChallengeRating()
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 / 2 + 90210));
        }

        [Test]
        public void ChallengeRatingForLevel1NPCIsOneHalf()
        {
            character.Class.Level = 1;
            character.Class.IsNPC = true;

            Assert.That(character.ChallengeRating, Is.EqualTo(.5));
        }

        [Test]
        public void ChallengeRatingForLevel1NPCWithRacialChallengeRatingIsRacialChallengeRating()
        {
            character.Class.Level = 1;
            character.Class.IsNPC = true;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(90210));
        }

        [TestCase(SizeConstants.BaseRaces.Drow)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(SizeConstants.BaseRaces.Githyanki)]
        [TestCase(SizeConstants.BaseRaces.Githzerai)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin)]
        public void SpecialNPCChallengeRatingIsClassLevel(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.BaseRace = baseRace;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266));
        }

        [TestCase(SizeConstants.BaseRaces.Drow)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(SizeConstants.BaseRaces.Githyanki)]
        [TestCase(SizeConstants.BaseRaces.Githzerai)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin)]
        public void SpecialCharacterChallengeRatingIsClassLevelPlus1(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = false;
            character.Race.BaseRace = baseRace;

            Assert.That(character.ChallengeRating, Is.EqualTo(9267));
        }

        [TestCase(SizeConstants.BaseRaces.Drow)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(SizeConstants.BaseRaces.Githyanki)]
        [TestCase(SizeConstants.BaseRaces.Githzerai)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin)]
        public void SpecialNPCChallengeRatingTakesRacialChallengeRatingIntoAccount(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.BaseRace = baseRace;
            character.Race.Metarace = "metarace";
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 + 90210));
        }

        [TestCase(SizeConstants.BaseRaces.Drow)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(SizeConstants.BaseRaces.Githyanki)]
        [TestCase(SizeConstants.BaseRaces.Githzerai)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin)]
        public void SpecialCharacterChallengeRatingTakesRacialChallengeRatingIntoAccount(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = false;
            character.Race.BaseRace = baseRace;
            character.Race.Metarace = "metarace";
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9267 + 90210));
        }

        [Test]
        public void CanSortAbilities()
        {
            character.Abilities["zzzz"] = new Ability("zzzz");
            character.Abilities["aaaa"] = new Ability("aaaa");
            character.Abilities["kkkk"] = new Ability("kkkk");

            var sortedAbilities = character.Abilities.Values.OrderBy(a => a.Name);
            Assert.That(sortedAbilities, Is.Ordered.By("Name"));
        }

        [Test]
        public void CanSortSkills()
        {
            var ability = new Ability("base ability");

            character.Skills = new[]
            {
                new Skill("zzzz", ability, int.MaxValue),
                new Skill("aaaa", ability, int.MaxValue),
                new Skill("kkkk", ability, int.MaxValue),
            };

            var sortedSkills = character.Skills.OrderBy(a => a.Name);
            Assert.That(sortedSkills, Is.Ordered.By("Name"));
        }

        [Test]
        public void CanSortSkillsWithFocus()
        {
            var ability = new Ability("base ability");

            character.Skills = new[]
            {
                new Skill("skill", ability, int.MaxValue, "zzzz"),
                new Skill("skill", ability, int.MaxValue, "aaaa"),
                new Skill("skill", ability, int.MaxValue, "kkkk"),
            };

            var sortedSkills = character.Skills.OrderBy(a => a.Focus);
            Assert.That(sortedSkills, Is.Ordered.By("Focus"));
        }

        [Test]
        public void CanSortSkillsWithAndWithoutFocus()
        {
            var ability = new Ability("base ability");

            character.Skills = new[]
            {
                new Skill("zzzz", ability, int.MaxValue),
                new Skill("aaaa", ability, int.MaxValue, "ffff"),
                new Skill("aaaa", ability, int.MaxValue, "ff"),
                new Skill("kkkk", ability, int.MaxValue),
            };

            var sortedSkills = character.Skills.OrderBy(a => a.Name).ThenBy(a => a.Focus);
            Assert.That(sortedSkills, Is.Ordered.By("Name").Then.By("Focus"));
        }
    }
}