﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Skills;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
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

        public CreatureBuilder CanUseEquipment(bool canUseEquipment)
        {
            creature.CanUseEquipment = canUseEquipment;

            return this;
        }

        public CreatureBuilder WithCreatureType(string creatureType)
        {
            creature.Type.Name = creatureType;

            return this;
        }

        public CreatureBuilder WithLevelAdjustment(int? levelAdjustment)
        {
            creature.LevelAdjustment = levelAdjustment;

            return this;
        }

        public CreatureBuilder WithMinimumAbility(string ability, int minValue)
        {
            while (creature.Abilities[ability].FullScore < minValue)
            {
                creature.Abilities[ability].BaseScore += minValue;
            }

            return this;
        }

        public CreatureBuilder Clone(Creature source)
        {
            creature.Abilities = [];
            foreach (var kvp in source.Abilities)
            {
                creature.Abilities[kvp.Key] = new Ability(kvp.Value.Name)
                {
                    AdvancementAdjustment = kvp.Value.AdvancementAdjustment,
                    BaseScore = kvp.Value.BaseScore,
                    RacialAdjustment = kvp.Value.RacialAdjustment
                };
            }

            creature.Alignment = new Alignment { Goodness = source.Alignment.Goodness, Lawfulness = source.Alignment.Lawfulness };

            creature.ArmorClass = new ArmorClass
            {
                Dexterity = creature.Abilities[AbilityConstants.Dexterity]
            };
            creature.ArmorClass.MaxDexterityBonus = source.ArmorClass.MaxDexterityBonus;
            creature.ArmorClass.SizeModifier = source.ArmorClass.SizeModifier;

            creature.Attacks = source.Attacks.Select(a => new Attack
            {
                AttackType = a.AttackType,
                BaseAbility = creature.Abilities[a.BaseAbility.Name],
                BaseAttackBonus = a.BaseAttackBonus,
                DamageBonus = a.DamageBonus,
                DamageEffect = a.DamageEffect,
                Damages = [.. a.Damages.Select(d => new Damage { Roll = d.Roll, Type = d.Type, Condition = d.Condition })],
                Frequency = new Frequency
                {
                    Quantity = a.Frequency.Quantity,
                    TimePeriod = a.Frequency.TimePeriod,
                },
                IsMelee = a.IsMelee,
                IsNatural = a.IsNatural,
                IsPrimary = a.IsPrimary,
                IsSpecial = a.IsSpecial,
                Name = a.Name,
                Save = a.Save == null ? null : new SaveDieCheck
                {
                    BaseAbility = creature.Abilities[a.Save.BaseAbility.Name],
                    BaseValue = a.Save.BaseValue,
                    Save = a.Save.Save,
                },
                AttackBonuses = new List<int>(a.AttackBonuses),
                SizeModifier = a.SizeModifier,
                MaxNumberOfAttacks = a.MaxNumberOfAttacks,
            });

            creature.BaseAttackBonus = source.BaseAttackBonus;
            creature.CanUseEquipment = source.CanUseEquipment;
            creature.CasterLevel = source.CasterLevel;
            creature.ChallengeRating = source.ChallengeRating;
            creature.Feats = source.Feats.Select(f => new Feat
            {
                CanBeTakenMultipleTimes = f.CanBeTakenMultipleTimes,
                Foci = f.Foci,
                Frequency = f.Frequency == null ? null : new Frequency
                {
                    Quantity = f.Frequency.Quantity,
                    TimePeriod = f.Frequency.TimePeriod,
                },
                Name = f.Name,
                Power = f.Power,
                Save = f.Save == null ? null : new SaveDieCheck
                {
                    BaseAbility = creature.Abilities[f.Save.BaseAbility.Name],
                    BaseValue = f.Save.BaseValue,
                    Save = f.Save.Save,
                },
            });

            creature.GrappleBonus = source.GrappleBonus;

            creature.HitPoints = new HitPoints();
            creature.HitPoints.Bonus = source.HitPoints.Bonus;
            creature.HitPoints.Constitution = creature.Abilities[AbilityConstants.Constitution];

            foreach (var hitDice in source.HitPoints.HitDice)
            {
                creature.HitPoints.HitDice.Add(new HitDice());
                creature.HitPoints.HitDice[0].Quantity = hitDice.Quantity;
                creature.HitPoints.HitDice[0].HitDie = hitDice.HitDie;
            }

            creature.HitPoints.DefaultTotal = source.HitPoints.DefaultTotal;
            creature.HitPoints.Total = source.HitPoints.Total;

            creature.InitiativeBonus = source.InitiativeBonus;
            creature.LevelAdjustment = source.LevelAdjustment;
            creature.Name = source.Name;
            creature.NumberOfHands = source.NumberOfHands;
            creature.Reach = new Measurement(source.Reach.Unit);
            creature.Reach.Description = source.Reach.Description;
            creature.Reach.Value = source.Reach.Value;

            creature.Saves = new Dictionary<string, Save>();
            foreach (var kvp in source.Saves)
            {
                creature.Saves[kvp.Key] = new Save();
                creature.Saves[kvp.Key].BaseAbility = creature.Abilities[kvp.Value.BaseAbility.Name];
                creature.Saves[kvp.Key].BaseValue = kvp.Value.BaseValue;
                //TODO: Bonuses
            }

            creature.Size = source.Size;
            creature.Skills = source.Skills.Select(s => new Skill(s.Name, creature.Abilities[s.BaseAbility.Name], s.RankCap, s.Focus)
            {
                ArmorCheckPenalty = s.ArmorCheckPenalty,
                ClassSkill = s.ClassSkill,
                HasArmorCheckPenalty = s.HasArmorCheckPenalty,
                Ranks = s.Ranks,
            });

            creature.Space = new Measurement(source.Space.Unit);
            creature.Space.Description = source.Space.Description;
            creature.Space.Value = source.Space.Value;

            creature.SpecialQualities = source.SpecialQualities.Select(f => new Feat
            {
                CanBeTakenMultipleTimes = f.CanBeTakenMultipleTimes,
                Foci = f.Foci,
                //TODO: Frequency
                Name = f.Name,
                Power = f.Power,
                //TODO: save
            });

            creature.Speeds = new Dictionary<string, Measurement>();
            foreach (var kvp in source.Speeds)
            {
                creature.Speeds[kvp.Key] = new Measurement(kvp.Value.Unit);
                creature.Speeds[kvp.Key].Description = kvp.Value.Description;
                creature.Speeds[kvp.Key].Value = kvp.Value.Value;
            }

            creature.Templates.AddRange(source.Templates);
            creature.Type = new CreatureType();
            creature.Type.Name = source.Type.Name;
            creature.Type.SubTypes = source.Type.SubTypes.ToArray();

            return this;
        }

        public CreatureBuilder WithHitDiceQuantityNoMoreThan(int quantity)
        {
            var randomQuantity = random.Next(quantity) + 1;
            return WithHitDiceQuantity(randomQuantity);
        }

        public CreatureBuilder WithHitDiceQuantity(double quantity)
        {
            creature.HitPoints.HitDice[0].Quantity = quantity;
            creature.HitPoints.DefaultTotal = creature.HitPoints.RoundedHitDiceQuantity * creature.HitPoints.HitDice[0].HitDie / 2;
            creature.HitPoints.Total = random.Next(creature.HitPoints.RoundedHitDiceQuantity * creature.HitPoints.HitDice[0].HitDie) + creature.HitPoints.HitDice[0].RoundedQuantity;

            return this;
        }

        public CreatureBuilder WithTestValues()
        {
            RandomizeAbilities();
            RandomizeAttacks();
            RandomizeSaves();

            creature.Alignment = new Alignment();
            creature.Alignment.Goodness = $"good-{Guid.NewGuid()}";
            creature.Alignment.Lawfulness = $"law-{Guid.NewGuid()}";

            creature.ArmorClass = new ArmorClass();
            creature.ArmorClass.Dexterity = creature.Abilities[AbilityConstants.Dexterity];
            creature.ArmorClass.MaxDexterityBonus = random.Next(10) + 1;
            creature.ArmorClass.SizeModifier = random.Next(3) - 1;

            creature.BaseAttackBonus = random.Next(20) + 1;
            creature.CanUseEquipment = Convert.ToBoolean(random.Next(2));
            creature.ChallengeRating = Convert.ToString(random.Next(20) + 1);
            creature.Feats = new[]
            {
                new Feat { Name = $"feat {Guid.NewGuid()}" },
                new Feat { Name = $"feat {Guid.NewGuid()}" },
            };

            creature.HitPoints = new HitPoints();
            creature.HitPoints.Constitution = creature.Abilities[AbilityConstants.Constitution];
            creature.HitPoints.HitDice.Add(new HitDice());
            creature.HitPoints.HitDice[0].Quantity = random.Next(30) + 1;
            creature.HitPoints.HitDice[0].HitDie = random.Next(7) + 6;
            creature.HitPoints.DefaultTotal = creature.HitPoints.RoundedHitDiceQuantity * creature.HitPoints.HitDice[0].HitDie / 2;
            creature.HitPoints.Total = random.Next(creature.HitPoints.RoundedHitDiceQuantity * creature.HitPoints.HitDice[0].HitDie) + creature.HitPoints.HitDice[0].RoundedQuantity;

            creature.InitiativeBonus = random.Next(5);
            creature.Name = $"creature {Guid.NewGuid()}";
            creature.NumberOfHands = random.Next(3);

            creature.Reach = new Measurement($"reach unit {Guid.NewGuid()}");
            creature.Reach.Value = random.Next(4) * 5;
            creature.Reach.Description = $"reach description {Guid.NewGuid()}";

            creature.Size = $"size {Guid.NewGuid()}";
            creature.Skills = new[]
            {
                new Skill($"skill cha {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Charisma], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
                new Skill($"skill con {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Constitution], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
                new Skill($"skill str {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Strength], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
                new Skill($"skill int {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Intelligence], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
                new Skill($"skill wis {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Wisdom], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
                new Skill($"skill dex {Guid.NewGuid()}", creature.Abilities[AbilityConstants.Dexterity], creature.HitPoints.HitDice[0].RoundedQuantity + 3),
            };

            creature.Space = new Measurement($"space unit {Guid.NewGuid()}");
            creature.Space.Value = random.Next(4) * 5;
            creature.Space.Description = $"space description {Guid.NewGuid()}";

            creature.SpecialQualities = new[]
            {
                new Feat { Name = $"special quality {Guid.NewGuid()}" },
                new Feat { Name = $"special quality {Guid.NewGuid()}" },
            };

            creature.Speeds = new Dictionary<string, Measurement>();
            creature.Speeds[SpeedConstants.Land] = new Measurement($"speed unit {Guid.NewGuid()}");
            creature.Speeds[SpeedConstants.Land].Value = (random.Next(5) + 1) * 10;
            creature.Speeds[SpeedConstants.Land].Description = $"speed description {Guid.NewGuid()}";

            creature.Type = new CreatureType();
            creature.Type.Name = $"creature type {Guid.NewGuid()}";
            creature.Type.SubTypes = new[]
            {
                $"subtype {Guid.NewGuid()}",
                $"subtype {Guid.NewGuid()}",
            };

            if (creature.CanUseEquipment)
            {
                creature.Equipment.Armor = new Armor() { Name = "My Armor" };
                creature.Equipment.Shield = new Armor() { Name = "My Shield" };
                creature.Equipment.Items = new[]
                {
                    new Item { Name = "My Item" },
                    new Item { Name = "My Other Item" },
                };
                creature.Equipment.Weapons = new[]
                {
                    new Weapon { Name = "My Weapon" },
                    new Weapon { Name = "My Other Weapon" },
                };
            }

            creature.Magic.ArcaneSpellFailure = random.Next(101);
            creature.Magic.Caster = "my caster";
            creature.Magic.CasterLevel = random.Next(20) + 1;
            creature.Magic.Domains = new[]
            {
                "domain 1",
                "domain 2",
            };
            creature.Magic.SpellsPerDay = new[]
            {
                new SpellQuantity { Level = 0, Quantity = 2 },
                new SpellQuantity { Level = 1, Quantity = 2 },
            };
            creature.Magic.KnownSpells = new[]
            {
                new Spell { Name = "My Level 0 Spell", Level = 0 },
                new Spell { Name = "My Other Level 0 Spell", Level = 0 },
                new Spell { Name = "My Level 1 Spell", Level = 1 },
                new Spell { Name = "My Other Level 1 Spell", Level = 1 },
            };
            creature.Magic.PreparedSpells = new[]
            {
                new Spell { Name = "My Level 0 Spell", Level = 0 },
                new Spell { Name = "My Other Level 0 Spell", Level = 0 },
                new Spell { Name = "My Level 1 Spell", Level = 1 },
                new Spell { Name = "My Other Level 1 Spell", Level = 1 },
            };

            creature.Languages = new[] { "English", "Deutsch" };

            creature.Demographics.Age.Value = random.Next(100) + 20;
            creature.Demographics.Skin = $"skin {Guid.NewGuid()}";
            creature.Demographics.Hair = $"hair {Guid.NewGuid()}";
            creature.Demographics.Eyes = $"eyes {Guid.NewGuid()}";
            creature.Demographics.Other = $"other {Guid.NewGuid()}";
            creature.Demographics.Gender = $"gender {Guid.NewGuid()}";
            creature.Demographics.Height.Value = random.Next(20) + 50;
            creature.Demographics.MaximumAge.Value = creature.Demographics.Age.Value + random.Next(100);
            creature.Demographics.Weight.Value = random.Next(200) + 100;

            return this;
        }

        public CreatureBuilder AddSubtype(string subtype)
        {
            creature.Type.SubTypes = creature.Type.SubTypes.Union(new[] { subtype });

            return this;
        }

        public CreatureBuilder WithChallengeRating(string cr)
        {
            creature.ChallengeRating = cr;

            return this;
        }

        public CreatureBuilder WithAlignment(string alignment)
        {
            creature.Alignment = new Alignment(alignment);

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
                attack.Damages.Add(new Damage
                {
                    Roll = $"{random.Next(1000) + 1}d{random.Next(1000) + 1}",
                    Type = $"damage type {Guid.NewGuid()}",
                });
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

        private void RandomizeSaves()
        {
            creature.Saves = new Dictionary<string, Save>();
            creature.Saves[SaveConstants.Fortitude] = new Save();
            creature.Saves[SaveConstants.Reflex] = new Save();
            creature.Saves[SaveConstants.Will] = new Save();

            creature.Saves[SaveConstants.Fortitude].BaseAbility = creature.Abilities[AbilityConstants.Constitution];
            creature.Saves[SaveConstants.Fortitude].BaseValue = random.Next(20);

            creature.Saves[SaveConstants.Reflex].BaseAbility = creature.Abilities[AbilityConstants.Dexterity];
            creature.Saves[SaveConstants.Reflex].BaseValue = random.Next(20);

            creature.Saves[SaveConstants.Will].BaseAbility = creature.Abilities[AbilityConstants.Wisdom];
            creature.Saves[SaveConstants.Will].BaseValue = random.Next(20);
        }
    }
}
