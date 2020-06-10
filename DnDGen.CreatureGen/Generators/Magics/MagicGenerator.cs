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

            //TODO: get domains

            magic = MakeSpells(magic, caster.Type, caster.Amount, alignment, abilities);

            if (equipment.Armor == null && equipment.Shield == null)
                return magic;

            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane);
            if (!arcaneSpellcasters.Contains(caster.Type))
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

        private Magic MakeSpells(Magic magic, string caster, int casterLevel, Alignment alignment, Dictionary<string, Ability> abilities, params string[] domains)
        {
            magic.SpellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, abilities, domains);
            magic.KnownSpells = spellsGenerator.GenerateKnown(caster, casterLevel, alignment, abilities, domains);

            var classesThatPrepareSpells = collectionsSelector.SelectFrom(TableNameConstants.Collection.CasterGroups, GroupConstants.PreparesSpells);
            if (classesThatPrepareSpells.Contains(caster))
                magic.PreparedSpells = spellsGenerator.GeneratePrepared(magic.KnownSpells, magic.SpellsPerDay, domains);

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
