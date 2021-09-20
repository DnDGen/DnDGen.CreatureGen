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
            var newSelections = specialQualitySelections
                .Where(s => s.RequirementsMet(abilities, specialQualities, canUseEquipment, size, alignment, hitPoints))
                .Where(s => !specialQualities.Any(q => q.Name == s.Feat));

            do
            {
                //INFO: Need to do this, or the foreach loop gets angry
                var setNewSelections = newSelections.ToArray();

                foreach (var selection in setNewSelections)
                {
                    var specialQuality = new Feat();
                    specialQuality.Name = selection.Feat;
                    specialQuality.Foci = GetFoci(selection, skills, abilities);

                    specialQuality.Frequency = selection.Frequency;
                    specialQuality.Power = selection.Power;

                    if (!string.IsNullOrEmpty(selection.SaveAbility))
                    {
                        specialQuality.Save = new SaveDieCheck();
                        specialQuality.Save.BaseAbility = abilities[selection.SaveAbility];
                        specialQuality.Save.Save = selection.Save;
                        specialQuality.Save.BaseValue = selection.SaveBaseValue;
                    }

                    specialQualities.Add(specialQuality);
                }
            } while (newSelections.Any());

            //HACK: Handling this usecase because the orc creature and orc creature type are identical
            if (creatureName == CreatureConstants.Orc_Half)
            {
                var lightSensitivity = specialQualities.First(f => f.Name == FeatConstants.SpecialQualities.LightSensitivity);
                specialQualities.Remove(lightSensitivity);
            }

            //HACK: Requirements can't handle "remove this", so doing so here for particular use cases
            var blindFeatNames = new[]
            {
                FeatConstants.SpecialQualities.Blindsense,
                FeatConstants.SpecialQualities.Blindsight,
            };

            var visionFeatNames = new[]
            {
                FeatConstants.SpecialQualities.Darkvision,
                FeatConstants.SpecialQualities.AllAroundVision,
                FeatConstants.SpecialQualities.LowLightVision,
                FeatConstants.SpecialQualities.LowLightVision_Superior,
            };

            var blindFeats = specialQualities.Where(f => blindFeatNames.Contains(f.Name));
            if (blindFeats.Any())
            {
                specialQualities = specialQualities
                    .Where(f => !visionFeatNames.Contains(f.Name))
                    .ToList();
            }

            return specialQualities;
        }

        private IEnumerable<string> GetFoci(SpecialQualitySelection specialQualitySelection, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            if (string.IsNullOrEmpty(specialQualitySelection.FocusType))
                return Enumerable.Empty<string>();

            var foci = new HashSet<string>();

            var fociQuantity = 1;
            if (specialQualitySelection.RandomFociQuantity.Any())
            {
                var roll = dice.Roll(specialQualitySelection.RandomFociQuantity).AsSum();
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
                return Enumerable.Empty<Feat>();

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

        private int GetFeatQuantity(HitPoints hitPoints)
        {
            return hitPoints.RoundedHitDiceQuantity / 3 + 1;
        }

        private List<Feat> PopulateFeatsRandomlyFrom(
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Feat> preselectedFeats,
            IEnumerable<FeatSelection> sourceFeatSelections,
            int quantity,
            int casterLevel,
            IEnumerable<Attack> attacks)
        {
            var feats = new List<Feat>();
            var chosenFeats = new List<Feat>(preselectedFeats);

            var chosenFeatSelections = new List<FeatSelection>();
            var preselectedFeatSelections = GetSelectedSelections(sourceFeatSelections, preselectedFeats);
            chosenFeatSelections.AddRange(preselectedFeatSelections);

            var availableFeatSelections = new List<FeatSelection>();

            var newAvailableFeatSelections = AddNewlyAvailableFeatSelections(availableFeatSelections, sourceFeatSelections, chosenFeatSelections, chosenFeats);
            availableFeatSelections.AddRange(newAvailableFeatSelections);

            while (quantity-- > 0 && availableFeatSelections.Any())
            {
                var featSelection = collectionsSelector.SelectRandomFrom(availableFeatSelections);

                var preliminaryFocus = featFocusGenerator.GenerateFrom(featSelection.Feat, featSelection.FocusType, skills, featSelection.RequiredFeats, chosenFeats, casterLevel, abilities, attacks);
                if (preliminaryFocus == FeatConstants.Foci.NoValidFociAvailable)
                {
                    quantity++;

                    chosenFeatSelections.Add(featSelection);
                    availableFeatSelections.Remove(featSelection);

                    newAvailableFeatSelections = AddNewlyAvailableFeatSelections(availableFeatSelections, sourceFeatSelections, chosenFeatSelections, chosenFeats);
                    availableFeatSelections.AddRange(newAvailableFeatSelections);

                    continue;
                }

                var feat = new Feat();
                var hasMatchingFeat = feats.Any(f => FeatsWithFociMatch(f, featSelection));

                if (hasMatchingFeat)
                {
                    feat = feats.First(f => FeatsWithFociMatch(f, featSelection));
                }
                else
                {
                    feat.Name = featSelection.Feat;
                    feat.Frequency = featSelection.Frequency;
                    feat.Power = featSelection.Power;
                    feat.CanBeTakenMultipleTimes = featSelection.CanBeTakenMultipleTimes;

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
                    feat.Foci = feat.Foci.Union(new[] { preliminaryFocus });
            }

            return feats;
        }

        private IEnumerable<FeatSelection> GetSelectedSelections(IEnumerable<FeatSelection> sourceFeatSelections, IEnumerable<Feat> preselectedFeats)
        {
            var featNames = preselectedFeats.Select(f => f.Name);
            var featSelections = sourceFeatSelections.Where(s => featNames.Contains(s.Feat));
            var nonRepeatableFeatSelections = featSelections.Where(s => !FeatSelectionCanBeSelectedAgain(s));

            return nonRepeatableFeatSelections;
        }

        private bool FeatSelectionCanBeSelectedAgain(FeatSelection featSelection)
        {
            var isEmpty = string.IsNullOrEmpty(featSelection.FocusType);

            return (!isEmpty && !featFocusGenerator.FocusTypeIsPreset(featSelection.FocusType))
                || featSelection.CanBeTakenMultipleTimes;
        }

        private bool FeatsWithFociMatch(Feat feat, FeatSelection featSelection)
        {
            return feat.Frequency.TimePeriod == string.Empty
                && feat.Name == featSelection.Feat
                && feat.Power == featSelection.Power
                && feat.Foci.Any()
                && !string.IsNullOrEmpty(featSelection.FocusType);
        }

        private IEnumerable<FeatSelection> AddNewlyAvailableFeatSelections(IEnumerable<FeatSelection> currentFeatSelections, IEnumerable<FeatSelection> sourceFeatSelections, IEnumerable<FeatSelection> chosenFeatSelections, IEnumerable<Feat> chosenFeats)
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