using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Templates
{
    public class CreatureBuilder
    {
        private readonly Creature creature;
        private readonly Random random;

        public CreatureBuilder()
        {
            creature = new Creature();
            random = new Random();
        }

        public Creature Build()
        {
            return creature;
        }

        public CreatureBuilder WithTestValues()
        {
            RandomizeAbilities();
            RandomizeAttacks();

            creature.Alignment = new Alignment();
            creature.Alignment.Goodness = $"good-{Guid.NewGuid()}";
            creature.Alignment.Lawfulness = $"law-{Guid.NewGuid()}";

            creature.ArmorClass = new ArmorClass();
            creature.ArmorClass.Dexterity = creature.Abilities[AbilityConstants.Dexterity];
            creature.ArmorClass.SizeModifier = random.Next(3) - 1;

            creature.BaseAttackBonus = random.Next(20) + 1;
            creature.CanUseEquipment = Convert.ToBoolean(random.Next(2));
            creature.ChallengeRating = Convert.ToString(random.Next(20) + 1);
            creature.

            return this;
        }

        private void RandomizeAbilities()
        {
            creature.Abilities = new Dictionary<string, Ability>();
            creature.Abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creature.Abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            creature.Abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            creature.Abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            creature.Abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            creature.Abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);

            creature.Abilities[AbilityConstants.Charisma].BaseScore = random.Next(20) + 3;
            creature.Abilities[AbilityConstants.Constitution].BaseScore = random.Next(20) + 3;
            creature.Abilities[AbilityConstants.Dexterity].BaseScore = random.Next(20) + 3;
            creature.Abilities[AbilityConstants.Intelligence].BaseScore = random.Next(20) + 3;
            creature.Abilities[AbilityConstants.Strength].BaseScore = random.Next(20) + 3;
            creature.Abilities[AbilityConstants.Wisdom].BaseScore = random.Next(20) + 3;

            creature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = random.Next(3);
            creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = random.Next(3);
            creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment = random.Next(3);
            creature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = random.Next(3);
            creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment = random.Next(3);
            creature.Abilities[AbilityConstants.Wisdom].AdvancementAdjustment = random.Next(3);

            creature.Abilities[AbilityConstants.Charisma].RacialAdjustment = random.Next(10) * 2 - 10;
            creature.Abilities[AbilityConstants.Constitution].RacialAdjustment = random.Next(10) * 2 - 10;
            creature.Abilities[AbilityConstants.Dexterity].RacialAdjustment = random.Next(10) * 2 - 10;
            creature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = random.Next(10) * 2 - 10;
            creature.Abilities[AbilityConstants.Strength].RacialAdjustment = random.Next(10) * 2 - 10;
            creature.Abilities[AbilityConstants.Wisdom].RacialAdjustment = random.Next(10) * 2 - 10;
        }

        private void RandomizeAttacks()
        {
            var attacks = new List<Attack>();
            var count = random.Next(5) + 1;

            while (attacks.Count < count)
            {
                var attack = new Attack();
                attack.AttackType = $"attack type {Guid.NewGuid()}";
                attack.BaseAbility = creature.Abilities[AbilityConstants.Strength];
                attack.BaseAttackBonus = random.Next(20) + 1;
                attack.DamageRoll = $"{random.Next(1000) + 1}d{random.Next(1000) + 1}";
                attack.Frequency = new Frequency();
                attack.Frequency.Quantity = random.Next(4) + 1;
                attack.Frequency.TimePeriod = $"time period {Guid.NewGuid()}";
                attack.IsMelee = Convert.ToBoolean(random.Next(2));
                attack.IsNatural = Convert.ToBoolean(random.Next(2));
                attack.IsPrimary = Convert.ToBoolean(random.Next(2));
                attack.IsSpecial = Convert.ToBoolean(random.Next(2));
                attack.Name = $"attack name {Guid.NewGuid()}";

                attacks.Add(attack);
            }

            creature.Attacks = attacks;
        }
    }
}
