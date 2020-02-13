using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.RollGen;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.IoC.Modules
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
            AssertIsInstanceOf<IAlignmentGenerator, AlignmentGeneratorEventDecorator>();
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
            AssertIsInstanceOf<IAbilitiesGenerator, AbilitiesGeneratorEventDecorator>();
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

        [Test]
        public void AttacksGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IAttacksGenerator>();
        }

        [Test]
        public void SpeedsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISpeedsGenerator>();
        }

        [TestCase(CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Templates.Ghost)]
        [TestCase(CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Templates.HalfDragon)]
        [TestCase(CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Templates.Lich)]
        [TestCase(CreatureConstants.Templates.None)]
        [TestCase(CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Templates.Vampire)]
        [TestCase(CreatureConstants.Templates.Werebear)]
        [TestCase(CreatureConstants.Templates.Wereboar)]
        [TestCase(CreatureConstants.Templates.Wererat)]
        [TestCase(CreatureConstants.Templates.Weretiger)]
        [TestCase(CreatureConstants.Templates.Werewolf)]
        [TestCase(CreatureConstants.Templates.Zombie)]
        public void TemplateApplicatorIsInjected(string name)
        {
            AssertNotSingleton<TemplateApplicator>(name);
        }

        [Test]
        public void CelestialCreatureApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, CelestialCreatureApplicator>(CreatureConstants.Templates.CelestialCreature);
        }

        [Test]
        public void FiendishCreatureApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, FiendishCreatureApplicator>(CreatureConstants.Templates.FiendishCreature);
        }

        [Test]
        public void GhostApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, GhostApplicator>(CreatureConstants.Templates.Ghost);
        }

        [Test]
        public void HalfCelestialApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfCelestialApplicator>(CreatureConstants.Templates.HalfCelestial);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonApplicator>(CreatureConstants.Templates.HalfDragon);
        }

        [Test]
        public void HalfFiendApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfFiendApplicator>(CreatureConstants.Templates.HalfFiend);
        }

        [Test]
        public void LichApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LichApplicator>(CreatureConstants.Templates.Lich);
        }

        [Test]
        public void NoneApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, NoneApplicator>(CreatureConstants.Templates.None);
        }

        [Test]
        public void SkeletonApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, SkeletonApplicator>(CreatureConstants.Templates.Skeleton);
        }

        [Test]
        public void VampireApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, VampireApplicator>(CreatureConstants.Templates.Vampire);
        }

        [Test]
        public void WerebearApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, WerebearApplicator>(CreatureConstants.Templates.Werebear);
        }

        [Test]
        public void WereboarApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, WereboarApplicator>(CreatureConstants.Templates.Wereboar);
        }

        [Test]
        public void WereratApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, WereratApplicator>(CreatureConstants.Templates.Wererat);
        }

        [Test]
        public void WeretigerApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, WeretigerApplicator>(CreatureConstants.Templates.Weretiger);
        }

        [Test]
        public void WerewolfApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, WerewolfApplicator>(CreatureConstants.Templates.Werewolf);
        }

        [Test]
        public void ZombieApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, ZombieApplicator>(CreatureConstants.Templates.Zombie);
        }
    }
}