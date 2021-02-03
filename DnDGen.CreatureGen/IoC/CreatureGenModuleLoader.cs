using DnDGen.CreatureGen.IoC.Modules;
using DnDGen.Infrastructure.IoC;
using DnDGen.RollGen.IoC;
using DnDGen.TreasureGen.IoC;
using Ninject;
using System.Linq;

namespace DnDGen.CreatureGen.IoC
{
    public class CreatureGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            //Dependencies
            var rollGenLoader = new RollGenModuleLoader();
            rollGenLoader.LoadModules(kernel);

            var infrastructureLoader = new InfrastructureModuleLoader();
            infrastructureLoader.LoadModules(kernel);

            var treasureGenLoader = new TreasureGenModuleLoader();
            treasureGenLoader.LoadModules(kernel);

            //CreatureGen
            var modules = kernel.GetModules();

            if (!modules.Any(m => m is GeneratorsModule))
                kernel.Load<GeneratorsModule>();

            if (!modules.Any(m => m is SelectorsModule))
                kernel.Load<SelectorsModule>();
        }
    }
}