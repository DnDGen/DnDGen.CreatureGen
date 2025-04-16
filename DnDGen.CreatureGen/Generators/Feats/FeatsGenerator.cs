using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Feats
{
    internal class FeatsGenerator : IFeatsGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IFeatsSelector featsSelector;
        private readonly IFeatFocusGenerator featFocusGenerator;
        private readonly Dice dice;

        public FeatsGenerator(ICollectionSelector collectionsSelector, IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator, Dice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateSpecialQualities(
            string creatureName,
            CreatureType creatureType,
            HitPoints hitPoints,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            bool canUseEquipment,
            string size,
            Alignment alignment)
        {
            var specialQualitySelections = featsSelector.SelectSpecialQualities(creatureName, creatureType);
            var specialQualities = new List<Feat>();
            var addedNames = new HashSet<string>();

            var newSelections = specialQualitySelections
                .Where(s => s.RequirementsMet(abilities, specialQualities, canUseEquipment, size, alignment, hitPoints)
                    && !addedNames.Contains(s.Feat));

            do
            {
                //INFO: Need to do this, or the foreach loop gets angry
                var setNewSelections = newSelections.ToArray();

                foreach (var selection in setNewSelections)
                {
                    var specialQuality = Feat.From(selection, abilities);
                    specialQuality.Foci = GetFoci(selection, skills, abilities);

                    specialQualities.Add(specialQuality);
                    addedNames.Add(specialQuality.Name);
                }
            } while (newSelections.Any());

            //HACK: Handling this usecase because the orc creature and orc creature type are identical
            if (creatureName == CreatureConstants.Orc_Half)
            {
                var lightSensitivity = specialQualities.First(f => f.Name == FeatConstants.SpecialQualities.LightSensitivity);
                specialQualities.Remove(lightSensitivity);
            }

            //HACK: Requirements can't handle "remove this", so doing so here for particular use cases
            var visionFeatNames = new[]
            {
                FeatConstants.SpecialQualities.Darkvision,
                FeatConstants.SpecialQualities.AllAroundVision,
                FeatConstants.SpecialQualities.LowLightVision,
                FeatConstants.SpecialQualities.LowLightVision_Superior,
            };

            if (specialQualities.Any(f => f.Name == FeatConstants.SpecialQualities.Blind))
            {
                specialQualities = [.. specialQualities.Where(f => !visionFeatNames.Contains(f.Name))];
            }

            return specialQualities;
        }

        private HashSet<string> GetFoci(SpecialQualityDataSelection specialQualitySelection, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            if (string.IsNullOrEmpty(specialQualitySelection.FocusType))
                return [];

            var foci = new HashSet<string>();

            var fociQuantity = 1;
            if (specialQualitySelection.RandomFociQuantityRoll.Any())
            {
                var roll = dice.Roll(specialQualitySelection.RandomFociQuantityRoll).AsSum();
                if (roll > fociQuantity)
                    fociQuantity = roll;
            }

            while (fociQuantity > foci.Count)
            {
                var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom(specialQualitySelection.Feat, specialQualitySelection.FocusType, skills, abilities);
                if (!string.IsNullOrEmpty(focus))
                    foci.Add(focus);
            }

            return foci;
        }

        public IEnumerable<Feat> GenerateFeats(
            HitPoints hitPoints,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            IEnumerable<Feat> specialQualities,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size,
            bool canUseEquipment)
        {
            if (!abilities[AbilityConstants.Intelligence].HasScore || hitPoints.HitDiceQuantity == 0)
                return [];

            var numberOfAdditionalFeats = GetFeatQuantity(hitPoints);

            var feats = GetFeats(
                numberOfAdditionalFeats,
                baseAttackBonus,
                abilities,
                skills,
                attacks,
                specialQualities,
                casterLevel,
                speeds,
                naturalArmor,
                hands,
                size,
                canUseEquipment);

            return feats;
        }

        private int GetFeatQuantity(HitPoints hitPoints) => hitPoints.RoundedHitDiceQuantity / 3 + 1;

        private List<Feat> PopulateFeatsRandomlyFrom(
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Feat> preselectedFeats,
            IEnumerable<FeatDataSelection> sourceFeatSelections,
            int quantity,
            int casterLevel,
            IEnumerable<Attack> attacks)
        {
            var feats = new List<Feat>();
            var chosenFeats = new List<Feat>(preselectedFeats);

            var chosenFeatSelections = new List<FeatDataSelection>();
            var preselectedFeatSelections = GetSelectedSelections(sourceFeatSelections, preselectedFeats);
            chosenFeatSelections.AddRange(preselectedFeatSelections);

            var availableFeatSelections = new List<FeatDataSelection>();

            var newAvailableFeatSelections = AddNewlyAvailableFeatSelections(availableFeatSelections, sourceFeatSelections, chosenFeatSelections, chosenFeats);
            availableFeatSelections.AddRange(newAvailableFeatSelections);

            while (quantity-- > 0 && availableFeatSelections.Any())
            {
                var featSelection = collectionsSelector.SelectRandomFrom(availableFeatSelections);

                var preliminaryFocus = featFocusGenerator.GenerateFrom(
                    featSelection.Feat,
                    featSelection.FocusType,
                    skills,
                    featSelection.RequiredFeats,
                    chosenFeats,
                    casterLevel,
                    abilities,
                    attacks);

                if (preliminaryFocus == FeatConstants.Foci.NoValidFociAvailable)
                {
                    quantity++;

                    chosenFeatSelections.Add(featSelection);
                    availableFeatSelections.Remove(featSelection);

                    newAvailableFeatSelections = AddNewlyAvailableFeatSelections(availableFeatSelections, sourceFeatSelections, chosenFeatSelections, chosenFeats);
                    availableFeatSelections.AddRange(newAvailableFeatSelections);

                    continue;
                }

                var feat = feats.FirstOrDefault(f => f.FociMatch(featSelection));
                if (feat == null)
                {
                    feat = Feat.From(featSelection);
                    feats.Add(feat);
                    chosenFeats.Add(feat);
                }

                if (!FeatSelectionCanBeSelectedAgain(featSelection))
                {
                    chosenFeatSelections.Add(featSelection);
                    availableFeatSelections.Remove(featSelection);
                }

                newAvailableFeatSelections = AddNewlyAvailableFeatSelections(availableFeatSelections, sourceFeatSelections, chosenFeatSelections, chosenFeats);
                availableFeatSelections.AddRange(newAvailableFeatSelections);

                if (!string.IsNullOrEmpty(preliminaryFocus))
                    feat.Foci = feat.Foci.Union([preliminaryFocus]);
            }

            return feats;
        }

        private IEnumerable<FeatDataSelection> GetSelectedSelections(IEnumerable<FeatDataSelection> sourceFeatSelections, IEnumerable<Feat> preselectedFeats)
        {
            var featNames = preselectedFeats.Select(f => f.Name);
            var featSelections = sourceFeatSelections.Where(s => featNames.Contains(s.Feat));
            var nonRepeatableFeatSelections = featSelections.Where(s => !FeatSelectionCanBeSelectedAgain(s));

            return nonRepeatableFeatSelections;
        }

        private bool FeatSelectionCanBeSelectedAgain(FeatDataSelection featSelection)
        {
            var isEmpty = string.IsNullOrEmpty(featSelection.FocusType);

            return (!isEmpty && !featFocusGenerator.FocusTypeIsPreset(featSelection.FocusType))
                || featSelection.CanBeTakenMultipleTimes;
        }

        private IEnumerable<FeatDataSelection> AddNewlyAvailableFeatSelections(IEnumerable<FeatDataSelection> currentFeatSelections, IEnumerable<FeatDataSelection> sourceFeatSelections, IEnumerable<FeatDataSelection> chosenFeatSelections, IEnumerable<Feat> chosenFeats)
        {
            var missingSelections = sourceFeatSelections.Except(currentFeatSelections);
            var newPossibleSelections = missingSelections.Except(chosenFeatSelections);
            var newFeatSelectionsWithRequirementsMet = newPossibleSelections.Where(f => f.MutableRequirementsMet(chosenFeats));

            return newFeatSelectionsWithRequirementsMet;
        }

        private IEnumerable<Feat> GetFeats(
            int quantity,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            IEnumerable<Feat> specialQualities,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size,
            bool canUseEquipment)
        {
            var featSelections = featsSelector.SelectFeats();

            //INFO: Calling immediate execution, so this doesn't reevaluate every time the collection is called
            var availableFeats = featSelections
                .Where(f => f.ImmutableRequirementsMet(
                    baseAttackBonus,
                    abilities,
                    skills,
                    attacks,
                    casterLevel,
                    speeds,
                    naturalArmor,
                    hands,
                    size,
                    canUseEquipment))
                .ToArray();

            var feats = PopulateFeatsRandomlyFrom(abilities, skills, specialQualities, availableFeats, quantity, casterLevel, attacks);
            return feats;
        }
    }
}