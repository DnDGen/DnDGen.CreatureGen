using CreatureGen.Creatures;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Creatures;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Generators.Verifiers;
using CreatureGen.Verifiers;
using Ninject.Modules;

namespace CreatureGen.IoC.Modules
{
    internal class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICreatureVerifier>().To<CreatureVerifier>();

            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<IArmorClassGenerator>().To<ArmorClassGenerator>();
            Bind<ISavesGenerator>().To<SavesGenerator>();

            BindDecoratedGenerators();
        }

        private void BindDecoratedGenerators()
        {
            Bind<ICreatureGenerator>().To<CreatureGenerator>().WhenInjectedInto<CreatureGeneratorEventDecorator>();
            Bind<ICreatureGenerator>().To<CreatureGeneratorEventDecorator>();

            Bind<IAlignmentGenerator>().To<AlignmentGenerator>().WhenInjectedInto<AlignmentGeneratorEventGenDecorator>();
            Bind<IAlignmentGenerator>().To<AlignmentGeneratorEventGenDecorator>();

            Bind<IAbilitiesGenerator>().To<AbilitiesGenerator>().WhenInjectedInto<AbilitiesGeneratorEventGenDecorator>();
            Bind<IAbilitiesGenerator>().To<AbilitiesGeneratorEventGenDecorator>();

            Bind<ISkillsGenerator>().To<SkillsGenerator>().WhenInjectedInto<SkillsGeneratorEventGenDecorator>();
            Bind<ISkillsGenerator>().To<SkillsGeneratorEventGenDecorator>();

            Bind<IFeatsGenerator>().To<FeatsGenerator>().WhenInjectedInto<FeatsGeneratorEventDecorator>();
            Bind<IFeatsGenerator>().To<FeatsGeneratorEventDecorator>();

            Bind<IFeatsGenerator>().To<FeatsGenerator>().WhenInjectedInto<FeatsGeneratorEventDecorator>();
            Bind<IFeatsGenerator>().To<FeatsGeneratorEventDecorator>();

            Bind<IFeatFocusGenerator>().To<FeatFocusGenerator>().WhenInjectedInto<FeatFocusGeneratorEventDecorator>();
            Bind<IFeatFocusGenerator>().To<FeatFocusGeneratorEventDecorator>();
        }
    }
}