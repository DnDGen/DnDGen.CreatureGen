using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class ZombieApplicator : TemplateApplicator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly Dice dice;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly IEnumerable<string> creatureTypes;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly IEnumerable<string> invalidSubtypes;
        private readonly ICreaturePrototypeFactory prototypeFactory;
        private readonly IDemographicsGenerator demographicsGenerator;
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;

        public ZombieApplicator(
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            Dice dice,
            IAttacksGenerator attacksGenerator,
            IFeatsGenerator featsGenerator,
            ISavesGenerator savesGenerator,
            IHitPointsGenerator hitPointsGenerator,
            ICreaturePrototypeFactory prototypeFactory,
            IDemographicsGenerator demographicsGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.dice = dice;
            this.attacksGenerator = attacksGenerator;
            this.featsGenerator = featsGenerator;
            this.savesGenerator = savesGenerator;
            this.hitPointsGenerator = hitPointsGenerator;
            this.prototypeFactory = prototypeFactory;
            this.demographicsGenerator = demographicsGenerator;
            this.creatureDataSelector = creatureDataSelector;

            creatureTypes =
            [
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Elemental,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Vermin,
            ];

            invalidSubtypes =
            [
                CreatureConstants.Types.Subtypes.Angel,
                CreatureConstants.Types.Subtypes.Archon,
                CreatureConstants.Types.Subtypes.Chaotic,
                CreatureConstants.Types.Subtypes.Dwarf,
                CreatureConstants.Types.Subtypes.Elf,
                CreatureConstants.Types.Subtypes.Evil,
                CreatureConstants.Types.Subtypes.Gnoll,
                CreatureConstants.Types.Subtypes.Gnome,
                CreatureConstants.Types.Subtypes.Goblinoid,
                CreatureConstants.Types.Subtypes.Good,
                CreatureConstants.Types.Subtypes.Halfling,
                CreatureConstants.Types.Subtypes.Human,
                CreatureConstants.Types.Subtypes.Lawful,
                CreatureConstants.Types.Subtypes.Orc,
                CreatureConstants.Types.Subtypes.Reptilian,
                CreatureConstants.Types.Subtypes.Shapechanger,
            ];
        }

        public Creature ApplyTo(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                creature.HasSkeleton,
                creature.HitPoints.HitDiceQuantity,
                creature.Name,
                asCharacter,
                filters);

            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([CreatureConstants.Templates.Zombie])]);
            }

            // Template
            UpdateCreatureTemplate(creature);

            //Type
            UpdateCreatureType(creature);

            // Demographics
            UpdateCreatureDemographics(creature);

            //Abilities
            UpdateCreatureAbilities(creature);

            //Speed
            UpdateCreatureSpeeds(creature);

            //Level Adjustment
            UpdateCreatureLevelAdjustment(creature);

            //Skills
            UpdateCreatureSkills(creature);

            //Armor Class
            UpdateCreatureArmorClass(creature);

            //Alignment
            UpdateCreatureAlignment(creature);

            //Magic
            UpdateCreatureMagic(creature);

            //INFO: Depends on abilities
            //Hit Points
            UpdateCreatureHitPoints(creature);

            //INFO: Depends on hit points
            //Challenge Rating
            UpdateCreatureChallengeRating(creature);

            //INFO: Depends on type, hit points, abilities, skills, alignment
            //Special Qualities
            UpdateCreatureSpecialQualitiesAndFeats(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Hit Points
            UpdateCreatureHitPointsWithSpecialQualities(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Attacks
            UpdateCreatureAttacks(creature);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Saves
            UpdateCreatureSaves(creature);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private void UpdateCreatureType(CreaturePrototype creature)
        {
            var adjustedTypes = UpdateCreatureType(creature.Type.Name, creature.Type.SubTypes);
            creature.Type = new CreatureType(adjustedTypes);
        }

        private IEnumerable<string> UpdateCreatureType(string creatureType, IEnumerable<string> subtypes)
        {
            return new[] { CreatureConstants.Types.Undead }
                .Union(subtypes)
                .Except(invalidSubtypes);
        }

        private void UpdateCreatureDemographics(Creature creature)
        {
            creature.Demographics = demographicsGenerator.UpdateByTemplate(creature.Demographics, creature.Name, CreatureConstants.Templates.Zombie);
        }

        private void UpdateCreatureHitPoints(Creature creature)
        {
            foreach (var hitDie in creature.HitPoints.HitDice)
            {
                hitDie.HitDie = 12;
                hitDie.Quantity *= 2;

                //INFO: This handles the use case where the creature would normally be compatible, but is advanced, and has more hitpoints than it normally would
                hitDie.Quantity = Math.Min(20, hitDie.Quantity);
            }

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);
        }

        private void UpdateCreatureHitPoints(CreaturePrototype creature)
        {
            creature.HitDiceQuantity *= 2;

            //INFO: This handles the use case where the creature would normally be compatible, but is advanced, and has more hitpoints than it normally would
            creature.HitDiceQuantity = Math.Min(20, creature.HitDiceQuantity);
        }

        private void UpdateCreatureHitPointsWithSpecialQualities(Creature creature)
        {
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.SpecialQualities);
        }

        private void UpdateCreatureAbilities(Creature creature) => UpdateCreatureAbilities(creature.Abilities);
        private void UpdateCreatureAbilities(CreaturePrototype creature) => UpdateCreatureAbilities(creature.Abilities);

        private void UpdateCreatureAbilities(Dictionary<string, Ability> abilities)
        {
            abilities[AbilityConstants.Dexterity].TemplateAdjustment += -2;
            abilities[AbilityConstants.Constitution].TemplateScore = 0;
            abilities[AbilityConstants.Intelligence].TemplateScore = 0;
            abilities[AbilityConstants.Wisdom].TemplateScore = 10;
            abilities[AbilityConstants.Charisma].TemplateScore = 1;

            if (abilities[AbilityConstants.Strength].HasScore)
                abilities[AbilityConstants.Strength].TemplateAdjustment += 2;
        }

        private void UpdateCreatureSpeeds(Creature creature)
        {
            if (creature.Speeds.ContainsKey(SpeedConstants.Fly))
            {
                var sections = creature.Speeds[SpeedConstants.Fly].Description.Split(' ').Skip(1);
                var descriptionString = string.Join(" ", sections);

                creature.Speeds[SpeedConstants.Fly].Description = $"Clumsy {descriptionString}";
            }
        }

        private void UpdateCreatureChallengeRating(Creature creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.HitPoints.HitDiceQuantity, creature.Summary);
        }

        private void UpdateCreatureChallengeRating(CreaturePrototype creature)
        {
            creature.ChallengeRating = UpdateCreatureChallengeRating(creature.HitDiceQuantity, creature.Name);
        }

        private string UpdateCreatureChallengeRating(double hitDiceQuantity, string creature)
        {
            if (hitDiceQuantity <= 0.5)
            {
                return ChallengeRatingConstants.CR1_8th;
            }
            else if (hitDiceQuantity <= 1)
            {
                return ChallengeRatingConstants.CR1_4th;
            }
            else if (hitDiceQuantity <= 2)
            {
                return ChallengeRatingConstants.CR1_2nd;
            }
            else if (hitDiceQuantity <= 4)
            {
                return ChallengeRatingConstants.CR1;
            }
            else if (hitDiceQuantity <= 6)
            {
                return ChallengeRatingConstants.CR2;
            }
            else if (hitDiceQuantity <= 10)
            {
                return ChallengeRatingConstants.CR3;
            }
            else if (hitDiceQuantity <= 14)
            {
                return ChallengeRatingConstants.CR4;
            }
            else if (hitDiceQuantity <= 16)
            {
                return ChallengeRatingConstants.CR5;
            }
            else if (hitDiceQuantity <= 20)
            {
                return ChallengeRatingConstants.CR6;
            }

            throw new ArgumentException($"Zombie hit dice cannot be greater than 20, but was {hitDiceQuantity} for creature {creature}");
        }

        private void UpdateCreatureLevelAdjustment(Creature creature)
        {
            creature.LevelAdjustment = null;
        }

        private void UpdateCreatureLevelAdjustment(CreaturePrototype creature)
        {
            creature.LevelAdjustment = null;
        }

        private void UpdateCreatureSkills(Creature creature)
        {
            creature.Skills = [];
        }

        private void UpdateCreatureAttacks(Creature creature)
        {
            //INFO: Zombies have a Poor base attack quality because they are undead
            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(BaseAttackQuality.Poor, creature.HitPoints);

            var zombieAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Zombie,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Demographics.Gender);

            zombieAttacks = attacksGenerator.ApplyAttackBonuses(zombieAttacks, creature.SpecialQualities, creature.Abilities);
            var newSlam = zombieAttacks.First(a => a.Name == "Slam");

            if (creature.Attacks.Any(a => a.Name == "Slam"))
            {
                var oldSlam = creature.Attacks.First(a => a.Name == "Slam");

                var oldMax = dice.Roll(oldSlam.Damages[0].Roll).AsPotentialMaximum();
                var newMax = dice.Roll(newSlam.Damages[0].Roll).AsPotentialMaximum();

                if (newMax > oldMax)
                {
                    oldSlam.Damages.Clear();
                    oldSlam.Damages.Add(newSlam.Damages[0]);
                }

                zombieAttacks = zombieAttacks.Except([newSlam]);
            }

            creature.Attacks = creature.Attacks
                .Union(zombieAttacks)
                .Where(a => !a.IsSpecial);
        }

        private void UpdateCreatureArmorClass(Creature creature)
        {
            creature.ArmorClass.RemoveBonus(ArmorClassConstants.Natural);
            var naturalArmorBonus = 0;

            switch (creature.Size)
            {
                case SizeConstants.Colossal: naturalArmorBonus = 11; break;
                case SizeConstants.Gargantuan: naturalArmorBonus = 7; break;
                case SizeConstants.Huge: naturalArmorBonus = 4; break;
                case SizeConstants.Large: naturalArmorBonus = 3; break;
                case SizeConstants.Medium: naturalArmorBonus = 2; break;
                case SizeConstants.Small: naturalArmorBonus = 1; break;
                default: break;
            }

            creature.ArmorClass.AddBonus(ArmorClassConstants.Natural, naturalArmorBonus);
        }

        private void UpdateCreatureAlignment(Creature creature)
        {
            creature.Alignment.Lawfulness = AlignmentConstants.Neutral;
            creature.Alignment.Goodness = AlignmentConstants.Evil;
        }

        private void UpdateCreatureAlignment(CreaturePrototype creature)
        {
            creature.Alignments = [new Alignment(AlignmentConstants.NeutralEvil)];
        }

        private void UpdateCreatureMagic(Creature creature)
        {
            creature.Magic = new Magic();
            creature.CasterLevel = 0;
        }

        private void UpdateCreatureMagic(CreaturePrototype creature)
        {
            creature.CasterLevel = 0;
        }

        private void UpdateCreatureSaves(Creature creature)
        {
            creature.Saves = savesGenerator.GenerateWith(
                CreatureConstants.Templates.Zombie,
                creature.Type,
                creature.HitPoints,
                creature.SpecialQualities,
                creature.Abilities);
        }

        private void UpdateCreatureSpecialQualitiesAndFeats(Creature creature)
        {
            var featNamesToKeep = new List<string>
            {
                FeatConstants.SpecialQualities.AttackBonus
            };

            var weaponProficiencies = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            featNamesToKeep.AddRange(weaponProficiencies);

            var armorProficiencies = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency);
            featNamesToKeep.AddRange(armorProficiencies);

            var zombieQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Zombie,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities
                .Where(sq => featNamesToKeep.Contains(sq.Name))
                .Union(zombieQualities);

            creature.Feats = creature.Feats.Where(f => featNamesToKeep.Contains(f.Name));
        }

        private void UpdateCreatureTemplate(Creature creature)
        {
            creature.Templates.Add(CreatureConstants.Templates.Zombie);
        }

        public async Task<Creature> ApplyToAsync(Creature creature, bool asCharacter, Filters filters = null)
        {
            var compatibility = IsCompatible(
                creature.Type.AllTypes,
                creature.HasSkeleton,
                creature.HitPoints.HitDiceQuantity,
                creature.Name,
                asCharacter,
                filters);

            if (!compatibility.Compatible)
            {
                throw new InvalidCreatureException(
                    compatibility.Reason,
                    asCharacter,
                    creature.Name,
                    filters?.Type,
                    filters?.ChallengeRating,
                    filters?.Alignment,
                    [.. creature.Templates.Union([CreatureConstants.Templates.Zombie])]);
            }

            var tasks = new List<Task>();

            // Template
            var templateTask = Task.Run(() => UpdateCreatureTemplate(creature));
            tasks.Add(templateTask);

            //Type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Demographics
            var demographicsTask = Task.Run(() => UpdateCreatureDemographics(creature));
            tasks.Add(demographicsTask);

            //Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            //Speed
            var speedTask = Task.Run(() => UpdateCreatureSpeeds(creature));
            tasks.Add(speedTask);

            //Level Adjustment
            var levelAdjustmentTask = Task.Run(() => UpdateCreatureLevelAdjustment(creature));
            tasks.Add(levelAdjustmentTask);

            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature));
            tasks.Add(skillTask);

            //Armor Class
            var armorClassTask = Task.Run(() => UpdateCreatureArmorClass(creature));
            tasks.Add(armorClassTask);

            //Alignment
            var alignmentTask = Task.Run(() => UpdateCreatureAlignment(creature));
            tasks.Add(alignmentTask);

            //Magic
            var magicTask = Task.Run(() => UpdateCreatureMagic(creature));
            tasks.Add(magicTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on abilities
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature));
            tasks.Add(hitPointTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on hit points
            //Challenge Rating
            var challengeRatingTask = Task.Run(() => UpdateCreatureChallengeRating(creature));
            tasks.Add(challengeRatingTask);

            //INFO: Depends on type, hit points, abilities, skills, alignment
            //Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualitiesAndFeats(creature));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Hit Points
            var hitPointWithQualitiesTask = Task.Run(() => UpdateCreatureHitPointsWithSpecialQualities(creature));
            tasks.Add(hitPointWithQualitiesTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature));
            tasks.Add(attackTask);

            //INFO: Depends on type, hit points, abilities, special qualities + feats
            //Saves
            var saveTask = Task.Run(() => UpdateCreatureSaves(creature));
            tasks.Add(saveTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            return creature;
        }

        public IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            //INFO: Since Zombies cannot be characters (they explicitly lose their class levels), we can return an empty enumerable
            if (asCharacter
                || (!string.IsNullOrEmpty(filters?.Alignment) && filters.Alignment != AlignmentConstants.NeutralEvil)
                || (!string.IsNullOrEmpty(filters?.Type) && invalidSubtypes.Contains(filters.Type)))
            {
                return [];
            }

            var templateCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.Zombie + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);
            if (!filteredBaseCreatures.Any())
                return [];

            if (string.IsNullOrEmpty(filters?.ChallengeRating)
                && string.IsNullOrEmpty(filters?.Type)
                && string.IsNullOrEmpty(filters?.Alignment))
                return filteredBaseCreatures;

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);

            filteredBaseCreatures = filteredBaseCreatures
                .Where(c => AreFiltersCompatible(
                    allData[c].Single().Types,
                    allData[c].Single().GetEffectiveHitDiceQuantity(asCharacter),
                    c,
                    filters).Compatible);

            return filteredBaseCreatures;
        }

        private (bool Compatible, string Reason) IsCompatible(
            IEnumerable<string> types,
            bool hasSkeleton,
            double creatureHitDiceQuantity,
            string creature,
            bool asCharacter,
            Filters filters)
        {
            var compatibility = IsCompatible(asCharacter, types, hasSkeleton, creature, creatureHitDiceQuantity);
            if (!compatibility.Compatible)
                return (false, compatibility.Reason);

            return AreFiltersCompatible(types, creatureHitDiceQuantity, creature, filters);
        }

        private (bool Compatible, string Reason) AreFiltersCompatible(
            IEnumerable<string> types,
            double creatureHitDiceQuantity,
            string creature,
            Filters filters)
        {
            if (!string.IsNullOrEmpty(filters?.Alignment) && filters.Alignment != AlignmentConstants.NeutralEvil)
            {
                return (false, $"Alignment filter '{filters.Alignment}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.Type))
            {
                if (invalidSubtypes.Contains(filters.Type))
                {
                    return (false, $"Type filter '{filters.Type}' is not valid");
                }

                var updatedTypes = UpdateCreatureType(types.First(), types.Skip(1));
                if (!updatedTypes.Contains(filters.Type))
                    return (false, $"Type filter '{filters.Type}' is not valid");
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var cr = UpdateCreatureChallengeRating(creatureHitDiceQuantity * 2, creature);
                if (cr != filters.ChallengeRating)
                    return (false, $"CR filter {filters.ChallengeRating} does not match updated creature CR {cr}");
            }

            return (true, null);
        }

        private (bool Compatible, string Reason) IsCompatible(
            bool asCharacter,
            IEnumerable<string> types,
            bool hasSkeleton,
            string creature,
            double creatureHitDiceQuantity)
        {
            if (asCharacter)
                return (false, "Zombies cannot be characters");

            if (!creatureTypes.Contains(types.First()))
                return (false, $"Type '{types.First()}' is not valid");

            if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                return (false, "Creature is Incorporeal");

            if (!hasSkeleton)
                return (false, "Creature does not have a skeleton");

            if (creatureHitDiceQuantity > 10)
                return (false, $"Creature has too many hit dice ({creatureHitDiceQuantity} > 10)");

            return (true, null);
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<string> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatibleCreatures = GetCompatibleCreatures(sourceCreatures, asCharacter, filters);
            if (!compatibleCreatures.Any())
                return Enumerable.Empty<CreaturePrototype>();

            var prototypes = prototypeFactory.Build(compatibleCreatures, asCharacter);

            //INFO: Don't need to pass in preset alignment, since Zombies can only be Neutral Evil
            var updatedPrototypes = prototypes.Select(ApplyToPrototype);

            return updatedPrototypes;
        }

        private CreaturePrototype ApplyToPrototype(CreaturePrototype prototype)
        {
            UpdateCreatureAbilities(prototype);
            UpdateCreatureHitPoints(prototype);
            UpdateCreatureChallengeRating(prototype);
            UpdateCreatureLevelAdjustment(prototype);
            UpdateCreatureType(prototype);
            UpdateCreatureAlignment(prototype);
            UpdateCreatureMagic(prototype);

            return prototype;
        }

        public IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, bool asCharacter, Filters filters = null)
        {
            var compatiblePrototypes = sourceCreatures
                .Where(p => IsCompatible(
                    p.Type.AllTypes,
                    p.HasSkeleton,
                    p.HitDiceQuantity,
                    p.Name,
                    asCharacter,
                    filters).Compatible);
            var updatedPrototypes = compatiblePrototypes.Select(ApplyToPrototype);

            return updatedPrototypes;
        }
    }
}
