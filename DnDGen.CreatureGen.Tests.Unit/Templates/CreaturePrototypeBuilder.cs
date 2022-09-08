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
            InitializeAbilities();

            prototype.Alignments.Add(new Alignment($"Alignment 1-{Guid.NewGuid()}"));
            prototype.Alignments.Add(new Alignment($"Alignment 2-{Guid.NewGuid()}"));

            prototype.Size = SizeConstants.Medium;
            prototype.ChallengeRating = ChallengeRatingConstants.CR1;
            prototype.HitDiceQuantity = 1;
            prototype.Name = $"creature {Guid.NewGuid()}";
            prototype.CasterLevel = 0;
            prototype.LevelAdjustment = null;

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

        public CreaturePrototypeBuilder WithSize(string size)
        {
            prototype.Size = size;

            return this;
        }

        public CreaturePrototypeBuilder WithAlignments(params string[] alignments)
        {
            prototype.Alignments = alignments.Select(a => new Alignment(a)).ToList();

            return this;
        }

        private void InitializeAbilities()
        {
            prototype.Abilities = new Dictionary<string, Ability>();
            prototype.Abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            prototype.Abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            prototype.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            prototype.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            prototype.Abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            prototype.Abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
        }

        public CreaturePrototypeBuilder WithoutAbility(string ability)
        {
            prototype.Abilities[ability].BaseScore = 0;

            return this;
        }

        public CreaturePrototypeBuilder WithAbility(string ability, int racial, int template = 0)
        {
            prototype.Abilities[ability].RacialAdjustment = racial;
            prototype.Abilities[ability].TemplateAdjustment = template;

            return this;
        }
    }
}
