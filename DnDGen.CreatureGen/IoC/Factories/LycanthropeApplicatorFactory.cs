using DnDGen.CreatureGen.Templates;
using Ninject;
using Ninject.Activation;

namespace DnDGen.CreatureGen.IoC.Factories
{
    internal static class LycanthropeApplicatorFactory
    {
        public static TemplateApplicator Build(IContext context, string lycanthropeTemplate, string animal, bool isNatural)
        {
            var applicator = context.Kernel.Get<LycanthropeApplicator>();
            applicator.LycanthropeSpecies = lycanthropeTemplate;
            applicator.AnimalSpecies = animal;
            applicator.IsNatural = isNatural;

            return applicator;
        }
    }
}
