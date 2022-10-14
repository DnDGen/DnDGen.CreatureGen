using DnDGen.CreatureGen.Templates;
using Ninject;
using Ninject.Activation;

namespace DnDGen.CreatureGen.IoC.Factories
{
    internal static class HalfDragonApplicatorFactory
    {
        public static TemplateApplicator Build(IContext context, string halfDragonTemplate)
        {
            var applicator = context.Kernel.Get<HalfDragonApplicator>();
            applicator.DragonSpecies = halfDragonTemplate;

            return applicator;
        }
    }
}
