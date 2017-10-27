using CharacterGen.Generators;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
{
    public static class LeadershipGeneratorFactory
    {
        public static ILeadershipGenerator Create(IKernel kernel)
        {
            var alignmentGenerator = kernel.Get<IAlignmentGenerator>();
            var percentileSelector = kernel.Get<IPercentileSelector>();
            var characterGenerator = kernel.Get<ICharacterGenerator>();
            var setLevelRandomizer = kernel.Get<ISetLevelRandomizer>();
            var setAlignmentRandomizer = kernel.Get<ISetAlignmentRandomizer>();
            var anyAlignmentRandomizer = kernel.Get<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var anyPlayerClassNameRandomizer = kernel.Get<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var anyNPCClassNameRandomizer = kernel.Get<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
            var anyBaseRaceRandomizer = kernel.Get<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            var anyMetaraceRandomizer = kernel.Get<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            var rawStatsRandomizer = kernel.Get<IStatsRandomizer>(StatsRandomizerTypeConstants.Raw);
            var adjustmentsSelector = kernel.Get<IAdjustmentsSelector>();
            var booleanPercentileSelector = kernel.Get<IBooleanPercentileSelector>();
            var leadershipSelector = kernel.Get<ILeadershipSelector>();
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var generator = kernel.Get<Generator>();

            return new LeadershipGenerator(characterGenerator, leadershipSelector, percentileSelector, adjustmentsSelector,
                setLevelRandomizer, setAlignmentRandomizer, anyAlignmentRandomizer, anyPlayerClassNameRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer,
                rawStatsRandomizer, booleanPercentileSelector, collectionsSelector, alignmentGenerator, generator, anyNPCClassNameRandomizer);
        }
    }
}
