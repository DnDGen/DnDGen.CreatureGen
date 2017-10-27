using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ArcaneSpellcastersClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override string classNameGroup
        {
            get { return SpellConstants.Sources.Arcane; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new ArcaneSpellcasterClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object, generator);
        }

        [Test]
        public void ClassIsAllowed()
        {
            alignmentClasses.Add(ClassName);
            groupClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item(ClassName));
        }

        [Test]
        public void ClassIsNotInAlignment()
        {
            groupClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.All.Not.EqualTo(ClassName));
        }

        [Test]
        public void ClassIsNotInGroup()
        {
            alignmentClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.All.Not.EqualTo(ClassName));
        }
    }
}