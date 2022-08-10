using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    public class CreaturePrototypeBuilder
    {
        private readonly CreaturePrototype prototype;
        private readonly Random random;

        public CreaturePrototypeBuilder()
        {
            prototype = new CreaturePrototype();
            random = new Random();
        }

        public CreaturePrototype Build()
        {
            return prototype;
        }

        public CreaturePrototypeBuilder WithCreatureType(params string[] types)
        {
            prototype.Type = new CreatureType(types);

            return this;
        }

        public CreaturePrototypeBuilder WithLevelAdjustment(int? levelAdjustment)
        {
            prototype.LevelAdjustment = levelAdjustment;

            return this;
        }

        public CreaturePrototypeBuilder WithCasterLevel(int casterLevel)
        {
            prototype.CasterLevel = casterLevel;

            return this;
        }

        public CreaturePrototypeBuilder WithHitDiceQuantity(double quantity)
        {
            prototype.HitDiceQuantity = quantity;

            return this;
        }

        public CreaturePrototypeBuilder WithMinimumAbility(string ability, int minValue)
        {
            while (prototype.Abilities[ability].FullScore < minValue)
            {
                prototype.Abilities[ability].BaseScore += minValue;
            }

            return this;
        }

        public CreaturePrototypeBuilder WithTestValues()
        {
            RandomizeAbilities();

            prototype.Alignments.Add(new Alignment($"Alignment 1-{Guid.NewGuid()}"));
            prototype.Alignments.Add(new Alignment($"Alignment 2-{Guid.NewGuid()}"));

            prototype.ChallengeRating = Convert.ToString(random.Next(20) + 1);
            prototype.HitDiceQuantity = random.Next(30) + 1;
            prototype.Name = $"creature {Guid.NewGuid()}";
            prototype.CasterLevel = 0;
            prototype.LevelAdjustment = null;

            if (Convert.ToBoolean(random.Next(2)))
                prototype.CasterLevel = random.Next(20) + 1;

            if (Convert.ToBoolean(random.Next(2)))
                prototype.LevelAdjustment = random.Next(20) + 1;

            prototype.Type = new CreatureType();
            prototype.Type.Name = $"creature type {Guid.NewGuid()}";
            prototype.Type.SubTypes = new[]
            {
                $"subtype {Guid.NewGuid()}",
                $"subtype {Guid.NewGuid()}",
            };

            return this;
        }

        public CreaturePrototypeBuilder WithName(string name)
        {
            prototype.Name = name;

            return this;
        }

        public CreaturePrototypeBuilder WithChallengeRating(string cr)
        {
            prototype.ChallengeRating = cr;

            return this;
        }

        public CreaturePrototypeBuilder WithAlignments(params string[] alignments)
        {
            prototype.Alignments = alignments.Select(a => new Alignment(a)).ToList();

            return this;
        }

        private void RandomizeAbilities()
        {
            prototype.Abilities = new Dictionary<string, Ability>();
            prototype.Abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            prototype.Abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            prototype.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            prototype.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            prototype.Abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            prototype.Abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);

            prototype.Abilities[AbilityConstants.Charisma].BaseScore = random.Next(20) + 3;
            prototype.Abilities[AbilityConstants.Constitution].BaseScore = random.Next(20) + 3;
            prototype.Abilities[AbilityConstants.Dexterity].BaseScore = random.Next(20) + 3;
            prototype.Abilities[AbilityConstants.Intelligence].BaseScore = random.Next(20) + 3;
            prototype.Abilities[AbilityConstants.Strength].BaseScore = random.Next(20) + 3;
            prototype.Abilities[AbilityConstants.Wisdom].BaseScore = random.Next(20) + 3;

            prototype.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = random.Next(3);
            prototype.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = random.Next(3);
            prototype.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment = random.Next(3);
            prototype.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = random.Next(3);
            prototype.Abilities[AbilityConstants.Strength].AdvancementAdjustment = random.Next(3);
            prototype.Abilities[AbilityConstants.Wisdom].AdvancementAdjustment = random.Next(3);

            prototype.Abilities[AbilityConstants.Charisma].RacialAdjustment = random.Next(10) * 2 - 10;
            prototype.Abilities[AbilityConstants.Constitution].RacialAdjustment = random.Next(10) * 2 - 10;
            prototype.Abilities[AbilityConstants.Dexterity].RacialAdjustment = random.Next(10) * 2 - 10;
            prototype.Abilities[AbilityConstants.Intelligence].RacialAdjustment = random.Next(10) * 2 - 10;
            prototype.Abilities[AbilityConstants.Strength].RacialAdjustment = random.Next(10) * 2 - 10;
            prototype.Abilities[AbilityConstants.Wisdom].RacialAdjustment = random.Next(10) * 2 - 10;
        }
    }
}
