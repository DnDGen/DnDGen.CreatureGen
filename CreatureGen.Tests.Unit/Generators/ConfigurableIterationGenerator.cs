using DnDGen.Core.Generators;
using System;

namespace CreatureGen.Tests.Unit.Generators
{
    public class ConfigurableIterationGenerator : Generator
    {
        public int MaxAttempts { get; set; }

        public ConfigurableIterationGenerator(int maxRetries = 1)
        {
            MaxAttempts = maxRetries;
        }

        public T Generate<T>(Func<T> buildInstructions, Func<T, bool> isValid, Func<T> buildDefault, Func<T, string> failureDescription, string defaultDescription)
        {
            T builtObject;
            var retries = 1;

            do builtObject = buildInstructions();
            while (retries++ < MaxAttempts && isValid(builtObject) == false);

            if (isValid(builtObject))
                return builtObject;

            return buildDefault();
        }
    }
}
