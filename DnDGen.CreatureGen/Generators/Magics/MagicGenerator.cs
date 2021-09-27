using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Magics
{
    internal class MagicGenerator : IMagicGenerator
    {
        private readonly ISpellsGenerator spellsGenerator;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public MagicGenerator(
            ISpellsGenerator spellsGenerator,
            ICollectionSelector collectionsSelector,
            IAdjustmentsSelector adjustmentsSelector,
            ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.spellsGenerator = spellsGenerator;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public Magic GenerateWith(string creatureName, Alignment alignment, Dictionary<string, Ability> abilities, Equipment equipment)
        {
            var magic = new Magic();

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting casters for '{creatureName}'");
            var casters = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Casters, creatureName);
            if (!casters.Any())
            {
                Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Returning no magic for '{creatureName}'");
                return magic;
            }

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Setting caster and level for '{creatureName}'");
            var caster = casters.First();
            magic.Caster = caster.Type;
            magic.CasterLevel = caster.Amount;

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting spellcasting ability for '{creatureName}'");
            var spellAbility = collectionsSelector.SelectFrom(TableNameConstants.Collection.AbilityGroups, $"{magic.Caster}:Spellcaster").Single();
            magic.CastingAbility = abilities[spellAbility];

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting spell domains and amounts for '{creatureName}'");
            var domainTypesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SpellDomains, creatureName);
            var domains = new List<string>();

            if (domainTypesAndAmounts.Any())
            {
                Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting domains for '{creatureName}'");
                var domainCount = domainTypesAndAmounts.First().Amount;
                var possibleDomains = domainTypesAndAmounts
                    .Select(d => d.Type)
                    .Except(domains);

                if (domainCount >= possibleDomains.Count())
                {
                    domains.AddRange(possibleDomains);
                }

                while (domains.Count < domainCount && possibleDomains.Any())
                {
                    var domain = collectionsSelector.SelectRandomFrom(possibleDomains);
                    domains.Add(domain);
                }
            }

            magic.Domains = domains;

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Making spells for '{creatureName}'");
            magic = MakeSpells(creatureName, magic, alignment);

            if (equipment.Armor == null && equipment.Shield == null)
                return magic;

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting arcane caster groups for '{creatureName}'");
            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane);
            if (!arcaneSpellcasters.Contains(magic.Caster))
                return magic;

            if (equipment.Armor != null)
            {
                Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Setting arcane spell failure for armor '{equipment.Armor.Description}' for '{creatureName}'");
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Armor);
            }

            if (equipment.Shield != null)
            {
                Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Setting arcane spell failure for shield '{equipment.Shield.Description}' for '{creatureName}'");
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Shield);
            }

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Returning magic for '{creatureName}'");
            return magic;
        }

        private Magic MakeSpells(string creature, Magic magic, Alignment alignment)
        {
            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Generating spells per day for '{creature}' as Level {magic.CasterLevel} {magic.Caster}");
            magic.SpellsPerDay = spellsGenerator.GeneratePerDay(magic.Caster, magic.CasterLevel, magic.CastingAbility, magic.Domains.ToArray());

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Generating known spells for '{creature}' as Level {magic.CasterLevel} {magic.Caster}");
            magic.KnownSpells = spellsGenerator.GenerateKnown(creature, magic.Caster, magic.CasterLevel, alignment, magic.CastingAbility, magic.Domains.ToArray());

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Selecting casters that prepare spells");
            var classesThatPrepareSpells = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, GroupConstants.PreparesSpells);
            if (classesThatPrepareSpells.Contains(magic.Caster))
            {
                Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator: Generating prepared spells for '{creature}' as Level {magic.CasterLevel} {magic.Caster}");
                magic.PreparedSpells = spellsGenerator.GeneratePrepared(magic.KnownSpells, magic.SpellsPerDay, magic.Domains.ToArray());
            }

            Console.WriteLine($"[{DateTime.Now:O}] MagicGenerator:Returning spells for '{creature}'");
            return magic;
        }

        private int GetArcaneSpellFailure(Item item)
        {
            var arcaneSpellFailure = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.ArcaneSpellFailures, item.Name);

            if (item.Traits.Contains(TraitConstants.SpecialMaterials.Mithral))
                arcaneSpellFailure -= 10;

            return Math.Max(0, arcaneSpellFailure);
        }
    }
}
