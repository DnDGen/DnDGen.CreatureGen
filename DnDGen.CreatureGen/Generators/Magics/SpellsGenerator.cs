using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Magics
{
    internal class SpellsGenerator : ISpellsGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public SpellsGenerator(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<SpellQuantity> GeneratePerDay(string caster, int casterLevel, Dictionary<string, Ability> abilities, params string[] domains)
        {
            var spellsPerDay = GetSpellsPerDay(caster, casterLevel, abilities, domains);
            return spellsPerDay.Where(s => s.TotalQuantity > 0 || s.HasDomainSpell);
        }

        private IEnumerable<SpellQuantity> GetSpellsPerDay(string caster, int casterLevel, Dictionary<string, Ability> abilities, IEnumerable<string> domains)
        {
            var spellsForClass = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SpellsPerDay, $"{caster}:{casterLevel}");

            var spellAbility = collectionsSelector.SelectFrom(TableNameConstants.Collection.AbilityGroups, $"{caster}:Spellcaster").Single();
            var maxSpellLevel = abilities[spellAbility].FullScore - 10;
            var spellsForCharacter = spellsForClass.Where(s => Convert.ToInt32(s.Type) <= maxSpellLevel);

            var spellsPerDay = new List<SpellQuantity>();

            foreach (var typeAndAmount in spellsForCharacter)
            {
                var spellQuantity = new SpellQuantity();
                spellQuantity.Level = Convert.ToInt32(typeAndAmount.Type);
                spellQuantity.Quantity = typeAndAmount.Amount;
                spellQuantity.Source = caster;
                spellQuantity.HasDomainSpell = domains.Any() && spellQuantity.Level > 0;

                if (spellQuantity.Level > 0 && spellQuantity.Level <= abilities[spellAbility].Modifier)
                {
                    spellQuantity.BonusSpells = (abilities[spellAbility].Modifier - spellQuantity.Level) / 4 + 1;
                }

                spellsPerDay.Add(spellQuantity);
            }

            return spellsPerDay;
        }

        public IEnumerable<Spell> GenerateKnown(string creature, string caster, int casterLevel, Alignment alignment, Dictionary<string, Ability> abilities, params string[] domains)
        {
            var spells = new List<Spell>();

            var divineCasters = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Divine);
            if (divineCasters.Contains(caster))
                return GetAllKnownSpells(creature, caster, casterLevel, alignment, abilities, domains);

            var quantities = GetKnownSpellQuantities(caster, casterLevel, abilities, domains);

            foreach (var spellQuantity in quantities)
            {
                var levelSpells = GetRandomKnownSpellsForLevel(creature, spellQuantity, caster, alignment, domains);
                spells.AddRange(levelSpells);
            }

            return spells;
        }

        private IEnumerable<Spell> GetAllKnownSpells(string creature, string caster, int casterLevel, Alignment alignment, Dictionary<string, Ability> abilities, IEnumerable<string> domains)
        {
            var knownSpellQuantities = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.KnownSpells, $"{caster}:{casterLevel}");

            var spellAbility = collectionsSelector.SelectFrom(TableNameConstants.Collection.AbilityGroups, $"{caster}:Spellcaster").Single();
            var maxSpellLevel = abilities[spellAbility].FullScore - 10;
            var knownSpellQuantitiesForCreature = knownSpellQuantities.Where(s => Convert.ToInt32(s.Type) <= maxSpellLevel);
            var spells = new List<Spell>();

            foreach (var typeAndAmount in knownSpellQuantitiesForCreature)
            {
                var spellLevel = Convert.ToInt32(typeAndAmount.Type);
                var spellNames = GetSpellNamesForCaster(creature, caster, alignment, spellLevel, domains);
                var specialistSpells = GetSpellNamesForFields(domains, spellLevel);

                spellNames = spellNames.Union(specialistSpells);

                foreach (var spellName in spellNames)
                {
                    var spell = BuildSpell(spellName, spellLevel, caster);
                    spells.Add(spell);
                }
            }

            return spells;
        }

        private IEnumerable<string> GetSpellNames(string creature, string caster, Alignment alignment, IEnumerable<string> domains)
        {
            var spellNames = new List<string>();

            var casterSpellNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, caster);
            spellNames.AddRange(casterSpellNames);

            var prohibited = new List<string>();

            var creatureProhibited = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, $"{creature}:Prohibited");
            prohibited.AddRange(creatureProhibited);

            var prohibitedByGoodness = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, $"{alignment.Goodness}:Prohibited");
            var prohibitedByLawfulness = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, $"{alignment.Lawfulness}:Prohibited");

            prohibited.AddRange(prohibitedByGoodness);
            prohibited.AddRange(prohibitedByLawfulness);

            foreach (var domain in domains)
            {
                var domainSpellNames = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, domain);
                spellNames.AddRange(domainSpellNames);

                var prohibitedByDomain = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, $"{domain}:Prohibited");
                prohibited.AddRange(prohibitedByDomain);
            }

            spellNames = spellNames.Except(prohibited).ToList();

            return spellNames;
        }

        private IEnumerable<string> GetSpellNamesForCaster(string creature, string caster, Alignment alignment, int level, IEnumerable<string> domains)
        {
            var spellLevels = new List<TypeAndAmountSelection>();

            var casterSpellLevels = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SpellLevels, caster);
            spellLevels.AddRange(casterSpellLevels);

            var spellNamesOfLevel = spellLevels
                .Where(s => s.Amount == level)
                .Select(s => s.Type);

            var spellNames = GetSpellNames(creature, caster, alignment, domains);
            spellNames = spellNames.Intersect(spellNamesOfLevel);

            return spellNames;
        }

        private Spell BuildSpell(string name, int level, string source)
        {
            var spell = new Spell();
            spell.Name = name;
            spell.Level = level;
            spell.Source = source;

            return spell;
        }

        private IEnumerable<SpellQuantity> GetKnownSpellQuantities(string caster, int casterLevel, Dictionary<string, Ability> abilities, IEnumerable<string> domains)
        {
            var spellsForClass = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.KnownSpells, $"{caster}:{casterLevel}");

            var spellAbility = collectionsSelector.SelectFrom(TableNameConstants.Collection.AbilityGroups, $"{caster}:Spellcaster").Single();
            var maxSpellLevel = abilities[spellAbility].FullScore - Ability.DefaultScore;
            var spellQuantitiesForCharacter = spellsForClass.Where(s => Convert.ToInt32(s.Type) <= maxSpellLevel);

            var spellQuantities = new List<SpellQuantity>();

            foreach (var typeAndAmount in spellQuantitiesForCharacter)
            {
                var spellQuantity = new SpellQuantity();
                spellQuantity.Level = Convert.ToInt32(typeAndAmount.Type);
                spellQuantity.HasDomainSpell = domains.Any() && spellQuantity.Level > 0;
                spellQuantity.Quantity = typeAndAmount.Amount;

                spellQuantities.Add(spellQuantity);
            }

            return spellQuantities;
        }

        private IEnumerable<Spell> GetRandomKnownSpellsForLevel(string creature, SpellQuantity spellQuantity, string caster, Alignment alignment, IEnumerable<string> domains)
        {
            var spellNames = GetSpellNamesForCaster(creature, caster, alignment, spellQuantity.Level, domains);
            var knownSpells = new HashSet<Spell>();

            if (spellQuantity.Quantity >= spellNames.Count())
            {
                foreach (var spellName in spellNames)
                {
                    var spell = BuildSpell(spellName, spellQuantity.Level, caster);
                    knownSpells.Add(spell);
                }

                return knownSpells;
            }

            while (spellQuantity.Quantity > knownSpells.Count)
            {
                var spellName = collectionsSelector.SelectRandomFrom(spellNames);
                var spell = BuildSpell(spellName, spellQuantity.Level, caster);
                knownSpells.Add(spell);
            }

            var specialistSpellsForLevel = GetSpellNamesForFields(domains, spellQuantity.Level);
            var knownSpellNames = knownSpells.Select(s => s.Name);
            var unknownSpecialistSpells = specialistSpellsForLevel.Except(knownSpellNames);

            if (spellQuantity.HasDomainSpell && unknownSpecialistSpells.Any())
            {
                while (spellQuantity.Quantity + 1 > knownSpells.Count)
                {
                    var spellName = collectionsSelector.SelectRandomFrom(specialistSpellsForLevel);
                    var spell = BuildSpell(spellName, spellQuantity.Level, caster);

                    knownSpells.Add(spell);
                }
            }
            else if (spellQuantity.HasDomainSpell)
            {
                while (spellQuantity.Quantity + 1 > knownSpells.Count)
                {
                    var spellName = collectionsSelector.SelectRandomFrom(spellNames);
                    var spell = BuildSpell(spellName, spellQuantity.Level, caster);
                    knownSpells.Add(spell);
                }
            }

            return knownSpells;
        }

        public IEnumerable<Spell> GeneratePrepared(IEnumerable<Spell> knownSpells, IEnumerable<SpellQuantity> spellsPerDay, params string[] domains)
        {
            var spells = new List<Spell>();

            foreach (var spellQuantity in spellsPerDay)
            {
                var levelSpells = GetPreparedSpellsForLevel(spellQuantity, knownSpells, domains);
                spells.AddRange(levelSpells);
            }

            return spells;
        }

        private IEnumerable<Spell> GetPreparedSpellsForLevel(SpellQuantity spellQuantity, IEnumerable<Spell> knownSpells, IEnumerable<string> domains)
        {
            var preparedSpells = new List<Spell>();
            var knownSpellsForLevel = knownSpells.Where(s => s.Level == spellQuantity.Level && s.Source == spellQuantity.Source);

            while (spellQuantity.Quantity > preparedSpells.Count)
            {
                var spell = collectionsSelector.SelectRandomFrom(knownSpellsForLevel);
                preparedSpells.Add(spell);
            }

            if (spellQuantity.HasDomainSpell)
            {
                var specialistSpells = GetSpellNamesForFields(domains);
                var specialistSpellsForLevel = knownSpellsForLevel.Where(s => specialistSpells.Contains(s.Name));
                var spell = collectionsSelector.SelectRandomFrom(specialistSpellsForLevel);

                preparedSpells.Add(spell);
            }

            return preparedSpells;
        }

        private IEnumerable<string> GetSpellNamesForFields(IEnumerable<string> fields)
        {
            var fieldSpellNames = new List<string>();

            foreach (var field in fields)
            {
                var fieldSpells = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, field);
                fieldSpellNames.AddRange(fieldSpells);
            }

            return fieldSpellNames;
        }

        private IEnumerable<string> GetSpellNamesForFields(IEnumerable<string> fields, int spellLevel)
        {
            var fieldSpellNames = new List<string>();

            foreach (var field in fields)
            {
                var spellLevels = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SpellLevels, field);

                var spellNamesOfLevel = spellLevels
                    .Where(s => s.Amount == spellLevel)
                    .Select(s => s.Type);

                var fieldSpells = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpellGroups, field);
                fieldSpells = fieldSpells.Intersect(spellNamesOfLevel);

                fieldSpellNames.AddRange(fieldSpells);
            }

            return fieldSpellNames;
        }
    }
}
