using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using CreatureGen.Verifiers;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Verifiers
{
    internal class RandomizerVerifier : IRandomizerVerifier
    {
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ICollectionSelector collectionsSelector;

        public RandomizerVerifier(IAdjustmentsSelector adjustmentsSelector, ICollectionSelector collectionsSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public bool VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any(a => VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
        }

        public bool VerifyAlignmentCompatibility(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignmentPrototype);
            if (!classNames.Any())
                return false;

            var levels = levelRandomizer.GetAllPossibleResults();
            if (!levels.Any())
                return false;

            //INFO: This is for the case when a set base race does not match an alignment, so we don't need to check any character classes
            if (baseRaceRandomizer is ISetBaseRaceRandomizer)
            {
                var setBaseRaceRandomizer = baseRaceRandomizer as ISetBaseRaceRandomizer;
                var verified = VerifyBaseRace(alignmentPrototype, setBaseRaceRandomizer.SetBaseRace);
                if (!verified)
                    return false;
            }

            //INFO: This is for the case when a set metarace does not match an alignment, so we don't need to check any character classes
            if (metaraceRandomizer is ISetMetaraceRandomizer)
            {
                var setMetaraceRandomizer = metaraceRandomizer as ISetMetaraceRandomizer;
                var verified = VerifyMetarace(alignmentPrototype, setMetaraceRandomizer.SetMetarace);
                if (!verified)
                    return false;
            }

            var characterClassPrototypes = GetAllCharacterClassPrototypes(classNames, levels);

            //INFO: If all classes are NPCs, make sure that races are compatible
            if (characterClassPrototypes.All(p => p.IsNPC))
            {
                if (metaraceRandomizer is IForcableMetaraceRandomizer)
                {
                    var forceableMetaraceRandomizer = metaraceRandomizer as IForcableMetaraceRandomizer;
                    if (forceableMetaraceRandomizer.ForceMetarace)
                        return false;
                }

                //HACK: Random NPCs will never be monster base races, so explicitly filtering out those races
                if (baseRaceRandomizer is MonsterBaseRaceRandomizer)
                    return false;
            }

            return characterClassPrototypes.Any(c => VerifyCharacterClassCompatibility(alignmentPrototype, c, baseRaceRandomizer, metaraceRandomizer));
        }

        private IEnumerable<CharacterClassPrototype> GetAllCharacterClassPrototypes(IEnumerable<string> classNames, IEnumerable<int> levels)
        {
            //INFO: We only check the minimum because we only add to this level with the level adjustments in the class
            var levelToCheck = levels.Min();
            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            foreach (var className in classNames)
            {
                var prototype = new CharacterClassPrototype { Name = className, Level = levelToCheck };
                prototype.IsNPC = npcs.Contains(className);

                yield return prototype;
            }
        }

        public bool VerifyCharacterClassCompatibility(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var verified = Verify(alignmentPrototype, classPrototype);
            if (!verified)
                return false;

            var metaraces = metaraceRandomizer.GetAllPossible(alignmentPrototype, classPrototype);
            if (!metaraces.Any())
                return false;

            var validMetaraces = metaraces.Where(r => VerifyMetarace(alignmentPrototype, classPrototype, r));
            if (!validMetaraces.Any())
                return false;

            var baseRaces = baseRaceRandomizer.GetAllPossible(alignmentPrototype, classPrototype);
            if (!baseRaces.Any())
                return false;

            var validBaseRaces = baseRaces.Where(r => VerifyBaseRace(alignmentPrototype, classPrototype, r));
            if (!validBaseRaces.Any())
                return false;

            var racePrototypes = GetAllRacePrototypes(validBaseRaces, validMetaraces);
            return racePrototypes.Any(r => VerifyRaceCompatibility(alignmentPrototype, classPrototype, r));
        }

        private bool Verify(Alignment alignmentPrototype, CharacterClassPrototype classPrototype)
        {
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignmentPrototype.Full);
            return alignmentClasses.Contains(classPrototype.Name);
        }

        private IEnumerable<RacePrototype> GetAllRacePrototypes(IEnumerable<string> baseRaces, IEnumerable<string> metaraces)
        {
            //INFO: If None is an allowed metarace, test with that for expediency
            var metaracesToCheck = metaraces;
            if (metaraces.Contains(SizeConstants.Metaraces.None))
                metaracesToCheck = new[] { SizeConstants.Metaraces.None };

            foreach (var baseRace in baseRaces)
                foreach (var metarace in metaracesToCheck)
                    yield return new RacePrototype { BaseRace = baseRace, Metarace = metarace };
        }

        public bool VerifyRaceCompatibility(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RacePrototype racePrototype)
        {
            var verified = Verify(alignmentPrototype, classPrototype, racePrototype);
            if (!verified)
                return false;

            var testClass = new CharacterClass();
            testClass.Level = classPrototype.Level;
            testClass.IsNPC = classPrototype.IsNPC;
            testClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, racePrototype.BaseRace);
            testClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, racePrototype.Metarace);

            return testClass.EffectiveLevel <= 30;
        }

        private bool Verify(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RacePrototype racePrototype)
        {
            var verified = Verify(alignmentPrototype, classPrototype);
            if (!verified)
                return false;

            verified = VerifyMetarace(alignmentPrototype, classPrototype, racePrototype.Metarace);
            if (!verified)
                return false;

            verified = VerifyBaseRace(alignmentPrototype, classPrototype, racePrototype.BaseRace);
            if (!verified)
                return false;

            return true;
        }

        private bool VerifyBaseRace(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, string baseRace)
        {
            return VerifyRace(alignmentPrototype, classPrototype, baseRace, TableNameConstants.Set.Collection.BaseRaceGroups);
        }

        private bool VerifyBaseRace(Alignment alignmentPrototype, string baseRace)
        {
            return VerifyRace(alignmentPrototype, baseRace, TableNameConstants.Set.Collection.BaseRaceGroups);
        }

        private bool VerifyMetarace(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, string metarace)
        {
            return VerifyRace(alignmentPrototype, classPrototype, metarace, TableNameConstants.Set.Collection.MetaraceGroups);
        }

        private bool VerifyMetarace(Alignment alignmentPrototype, string metarace)
        {
            return VerifyRace(alignmentPrototype, metarace, TableNameConstants.Set.Collection.MetaraceGroups);
        }

        private bool VerifyRace(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, string race, string tableName)
        {
            return VerifyRace(alignmentPrototype, race, tableName) && VerifyRace(classPrototype, race, tableName);
        }

        private bool VerifyRace(Alignment alignmentPrototype, string race, string tableName)
        {
            var alignmentRaces = collectionsSelector.SelectFrom(tableName, alignmentPrototype.Full);
            return alignmentRaces.Contains(race);
        }

        private bool VerifyRace(CharacterClassPrototype classPrototype, string race, string tableName)
        {
            var classRaces = collectionsSelector.SelectFrom(tableName, classPrototype.Name);
            return classRaces.Contains(race);
        }
    }
}