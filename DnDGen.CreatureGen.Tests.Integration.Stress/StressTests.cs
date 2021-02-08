using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Stress;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Integration.Stress
{
    [TestFixture]
    public abstract class StressTests : IntegrationTests
    {
        protected ICreatureVerifier creatureVerifier;
        protected IEnumerable<string> allCreatures;
        protected IEnumerable<string> allTemplates;
        protected Stressor stressor;

        [OneTimeSetUp]
        public void OneTimeStressSetup()
        {
            var options = new StressorOptions();
            options.RunningAssembly = Assembly.GetExecutingAssembly();

            //INFO: Non-stress operations take about 16 minutes, or ~27% of the total runtime
            options.TimeLimitPercentage = .73;

#if STRESS
            options.IsFullStress = true;
#else
            options.IsFullStress = false;
#endif

            stressor = new Stressor(options);
        }

        [SetUp]
        public void StressSetup()
        {
            allCreatures = CreatureConstants.GetAll();
            allTemplates = CreatureConstants.Templates.GetAll();
            creatureVerifier = GetNewInstanceOf<ICreatureVerifier>();
        }
    }
}