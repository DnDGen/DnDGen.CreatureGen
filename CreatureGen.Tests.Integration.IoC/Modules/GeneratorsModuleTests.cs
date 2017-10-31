using CreatureGen.Creatures;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Creatures;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Verifiers;
using NUnit.Framework;
using RollGen;

namespace CreatureGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : IoCTests
    {
        [Test]
        public void AlignmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void AlignmentGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IAlignmentGenerator, AlignmentGeneratorEventGenDecorator>();
        }

        [Test]
        public void CreatureGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICreatureGenerator>();
        }

        [Test]
        public void CreatureGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ICreatureGenerator, CreatureGeneratorEventDecorator>();
        }

        [Test]
        public void HitPointsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void RandomizerVerifierIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICreatureVerifier>();
        }

        [Test]
        public void AbilitiesGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAbilitiesGenerator>();
        }

        [Test]
        public void AbilitiesGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IAbilitiesGenerator, AbilitiesGeneratorEventGenDecorator>();
        }

        [Test]
        public void SkillsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISkillsGenerator>();
        }

        [Test]
        public void SkillsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ISkillsGenerator, SkillsGeneratorEventGenDecorator>();
        }

        [Test]
        public void FeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IFeatsGenerator>();
        }

        [Test]
        public void FeatsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IFeatsGenerator, FeatsGeneratorEventDecorator>();
        }

        [Test]
        public void SavesGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISavesGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IFeatFocusGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IFeatFocusGenerator, FeatFocusGeneratorEventDecorator>();
        }

        [Test]
        public void EXTERNAL_DiceIsInjected()
        {
            AssertNotSingleton<Dice>();
        }
    }
}