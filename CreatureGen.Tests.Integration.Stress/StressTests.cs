using CreatureGen.Creatures;
using CreatureGen.Verifiers;
using DnDGen.Stress;
using DnDGen.Stress.Events;
using EventGen;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace CreatureGen.Tests.Integration.Stress
{
    [TestFixture]
    [Stress]
    public abstract class StressTests : IntegrationTests
    {
        [Inject]
        public ICreatureVerifier CreatureVerifier { get; set; }

        protected Stressor stressor;

        [OneTimeSetUp]
        public void OneTimeStressSetup()
        {
            var options = new StressorWithEventsOptions();
            options.RunningAssembly = Assembly.GetExecutingAssembly();

#if STRESS
            options.IsFullStress = true;
#else
            options.IsFullStress = false;
#endif

            options.ClientIdManager = GetNewInstanceOf<ClientIDManager>();
            options.EventQueue = GetNewInstanceOf<GenEventQueue>();
            options.Source = "CreatureGen";

            stressor = new StressorWithEvents(options);
        }

        protected IEnumerable<string> allCreatures;
        protected IEnumerable<string> allTemplates;

        [SetUp]
        public void StressSetup()
        {
            allCreatures = CreatureConstants.All();
            allTemplates = CreatureConstants.Templates.All();
        }
    }
}