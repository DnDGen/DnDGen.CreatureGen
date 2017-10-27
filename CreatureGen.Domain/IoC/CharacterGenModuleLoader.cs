using CreatureGen.Domain.IoC.Modules;
using Ninject;

namespace CreatureGen.Domain.IoC
{
    public class CharacterGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<GeneratorsModule>();
            kernel.Load<SelectorsModule>();
        }
    }
}