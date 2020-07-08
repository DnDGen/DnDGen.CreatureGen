using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Templates
{
    internal class GhostApplicator : TemplateApplicator
    {
        private readonly Dice dice;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ICollectionSelector collectionSelector;
        private readonly IFeatsGenerator featsGenerator;

        public GhostApplicator(Dice dice, ISpeedsGenerator speedsGenerator, IAttacksGenerator attacksGenerator, ICollectionSelector collectionSelector, IFeatsGenerator featsGenerator)
        {
            this.dice = dice;
            this.speedsGenerator = speedsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.collectionSelector = collectionSelector;
            this.featsGenerator = featsGenerator;
        }

        public Creature ApplyTo(Creature creature)
        {
            //Type
            creature.Type.Name = CreatureConstants.Types.Undead;

            if (!creature.Type.SubTypes.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
            {
                creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
                {
                    CreatureConstants.Types.Subtypes.Incorporeal
                });
            }

            //Abilities
            creature.Abilities[AbilityConstants.Constitution].BaseScore = 0;
            creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment = 4;

            while (creature.Abilities[AbilityConstants.Charisma].FullScore < 10)
                creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment++;

            //Hit Points
            creature.HitPoints.HitDie = 12;
            creature.HitPoints.Roll(dice);
            creature.HitPoints.RollDefault(dice);

            //Speed
            var ghostSpeeds = speedsGenerator.Generate(CreatureConstants.Templates.Ghost);

            foreach (var kvp in ghostSpeeds)
            {
                if (!creature.Speeds.ContainsKey(kvp.Key))
                {
                    creature.Speeds[kvp.Key] = kvp.Value;
                }

                if (creature.Speeds[kvp.Key].Value < kvp.Value.Value)
                {
                    creature.Speeds[kvp.Key].Value = kvp.Value.Value;
                }

                creature.Speeds[kvp.Key].Description = kvp.Value.Description;
            }

            //Challenge Rating
            var challengeRatings = ChallengeRatingConstants.GetOrdered().ToList();
            var index = challengeRatings.IndexOf(creature.ChallengeRating);
            creature.ChallengeRating = challengeRatings[index + 2];

            //Level Adjustment
            if (creature.LevelAdjustment.HasValue)
            {
                creature.LevelAdjustment += 5;
            }

            //Attacks
            var ghostAttacks = attacksGenerator.GenerateAttacks(
                CreatureConstants.Templates.Ghost,
                creature.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var manifestation = ghostAttacks.First(a => a.Name == "Manifestation");
            var newAttacks = new List<Attack> { manifestation };
            var amount = dice.Roll().d3().AsSum();
            var availableAttacks = ghostAttacks.Except(newAttacks);

            while (newAttacks.Count < amount + 1)
            {
                var attack = collectionSelector.SelectRandomFrom(availableAttacks);
                newAttacks.Add(attack);
            }

            creature.Attacks = creature.Attacks.Union(newAttacks);

            //Skills
            var ghostSkills = new[] { SkillConstants.Hide, SkillConstants.Listen, SkillConstants.Search, SkillConstants.Spot };
            foreach (var skill in creature.Skills)
            {
                if (ghostSkills.Contains(skill.Name))
                {
                    skill.AddBonus(8);
                }
            }

            //Special Qualities
            var ghostQualities = featsGenerator.GenerateSpecialQualities(
                CreatureConstants.Templates.Ghost,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.SpecialQualities = creature.SpecialQualities.Union(ghostQualities);

            //Armor Class
            foreach (var naturalArmorBonus in creature.ArmorClass.NaturalArmorBonuses)
            {
                if (!naturalArmorBonus.IsConditional)
                {
                    naturalArmorBonus.Condition = "Only applies for ethereal creatures";
                }
                else
                {
                    naturalArmorBonus.Condition += " AND Only applies for ethereal creatures";
                }
            }

            var deflectionBonus = Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier);
            creature.ArmorClass.AddBonus(ArmorClassConstants.Deflection, deflectionBonus);

            return creature;
        }
    }
}
