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

            var casters = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Casters, creatureName);
            if (!casters.Any())
            {
                return magic;
            }

            var caster = casters.First();
            magic.Caster = caster.Type;
            magic.CasterLevel = caster.Amount;

            var spellAbility = collectionsSelector.SelectFrom(TableNameConstants.Collection.AbilityGroups, $"{magic.Caster}:Spellcaster").Single();
            magic.CastingAbility = abilities[spellAbility];

            var domainTypesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SpellDomains, creatureName);
            var domains = new List<string>();

            if (domainTypesAndAmounts.Any())
            {
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

            magic = MakeSpells(creatureName, magic, alignment);

            if (equipment.Armor == null && equipment.Shield == null)
                return magic;

            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane);
            if (!arcaneSpellcasters.Contains(magic.Caster))
                return magic;

            if (equipment.Armor != null)
            {
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Armor);
            }

            if (equipment.Shield != null)
            {
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Shield);
            }

            return magic;
        }

        private Magic MakeSpells(string creature, Magic magic, Alignment alignment)
        {
            magic.SpellsPerDay = spellsGenerator.GeneratePerDay(magic.Caster, magic.CasterLevel, magic.CastingAbility, magic.Domains.ToArray());
            magic.KnownSpells = spellsGenerator.GenerateKnown(creature, magic.Caster, magic.CasterLevel, alignment, magic.CastingAbility, magic.Domains.ToArray());

            var classesThatPrepareSpells = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, GroupConstants.PreparesSpells);
            if (classesThatPrepareSpells.Contains(magic.Caster))
                magic.PreparedSpells = spellsGenerator.GeneratePrepared(magic.KnownSpells, magic.SpellsPerDay, magic.Domains.ToArray());

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
