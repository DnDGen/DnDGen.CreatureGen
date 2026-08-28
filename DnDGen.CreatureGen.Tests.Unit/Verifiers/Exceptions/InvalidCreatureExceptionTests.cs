using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using NUnit.Framework;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers.Exceptions
{
    internal class InvalidCreatureExceptionTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsDefaults(bool asCharacter)
        {
            var exception = new InvalidCreatureException(null, asCharacter);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        private string GetExpectedMessage(params string[] sections)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(sections[0]);

            for (var i = 1; i < sections.Length; i++)
                stringBuilder.AppendLine($"\t{sections[i]}");

            return stringBuilder.ToString();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsReason(bool asCharacter)
        {
            var exception = new InvalidCreatureException("the reason is you", asCharacter);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                "Reason: the reason is you",
                $"As Character: {asCharacter}");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsCreature(bool asCharacter)
        {
            var exception = new InvalidCreatureException(null, asCharacter, creature: "my creature");
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                "Creature: my creature");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsTemplate(bool asCharacter)
        {
            var exception = new InvalidCreatureException(null, asCharacter, templates: ["my template"]);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                "Templates: my template");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsTemplates(bool asCharacter)
        {
            var exception = new InvalidCreatureException(null, asCharacter, templates: ["my template", "my other template"]);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                "Templates: my template, my other template");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsFilters(bool asCharacter)
        {
            var filters = new Filters { Alignments = ["my alignment"], ChallengeRatings = null, Types = [] };
            var exception = new InvalidCreatureException(null, asCharacter, filters: filters);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                $"Filters: {filters.GetDescription()}");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsAbilityRoll_FromRoll(bool asCharacter)
        {
            var exception = new InvalidCreatureException(null, asCharacter, null, "my ability roll", null);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                "Ability Roll: my ability roll");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsAbilityRoll_FromRandomizer(bool asCharacter)
        {
            var randomizer = new AbilityRandomizer("my ability roll");
            var exception = new InvalidCreatureException(null, asCharacter, abilityRandomizer: randomizer);
            var expected = GetExpectedMessage(
                "Invalid creature:",
                $"As Character: {asCharacter}",
                "Ability Roll: my ability roll");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExceptionMessage_ShowsAll(bool asCharacter)
        {
            var filters = new Filters { Alignments = ["my alignment"], ChallengeRatings = ["low cr", "medium cr", "high cr"], Types = ["my type", "my other type"] };
            var randomizer = new AbilityRandomizer("my ability roll");
            var exception = new InvalidCreatureException("this is all your fault", asCharacter, "my creature", filters, randomizer, "some template", "another template");
            var expected = GetExpectedMessage(
                "Invalid creature:",
                "Reason: this is all your fault",
                $"As Character: {asCharacter}",
                "Creature: my creature",
                "Templates: some template, another template",
                $"Filters: {filters.GetDescription()}",
                "Ability Roll: my ability roll");
            Assert.That(exception.Message, Is.EqualTo(expected));
        }
    }
}
